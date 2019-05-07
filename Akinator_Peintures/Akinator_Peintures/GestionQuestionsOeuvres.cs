using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Windows.Forms;


namespace Akinator_Peintures
{
    public class GestionQuestionsOeuvres:Question
    {
        #region DECLARATION, CONSTRUCTEUR
        public LINQDataContext dc { get; protected set; }
        public List<int> peintres { get; protected set; }
        public List<int> genres { get; protected set; }

        //Dictionnaire des questions posées et réponses données
        public Dictionary<int, int> dictionnaireQR { get; set; }
        public List<int> idQuestionsPoses { get; protected set; }
        public List<int> oeuvresCandidates { get; protected set; }
        public List<int> oeuvresProbables { get; protected set; }
        public int nbTentatives { get; protected set; }
        public int[,]oeuvresEnCriticite { get; set; }

        private bool doublonPeintres, doublonGenres, candidat;
        private int oeuvreProposeEnCours = 0 ;

        public GestionQuestionsOeuvres()
        {
            dc = new LINQDataContext();
            dictionnaireQR = new Dictionary<int, int>();
            idQuestionsPoses = new List<int>(dictionnaireQR.Keys); /*On extrait les clefs du dictionnaire (id des questions)
                                                                   dans une liste pour pouvoir ensuite l'intégrer au requêtage*/
            oeuvresCandidates = new List<int>();
            oeuvresProbables = new List<int>();
            peintres = new List<int>();
            genres = new List<int>();
        }
        #endregion

        #region FONCTIONS D'INITIALISATION
        public int[,] InstancierTableauOeuvres()  /*Instancie un tableau associant à chaque oeuvre une criticité
                                                    qui sera fonction des réponses données par le joueur*/
        {
            // taille du tableau d'oeuvres = nombre d'oeuvres dans la base
            var idOeuvres = dc.Oeuvre.Select(o => o.ID_O).Distinct();
            int nbOeuvres = idOeuvres.Count();
           
            int[,] tableauOeuvres = new int[nbOeuvres, 3]; /*0 = ID de l'oeuvre,
                                                              1 = Poids par question renseignée dans la BDD (%)
                                                              2 = Indice de correspondance de l'oeuvre à deviner/identifiée (%)*/

            foreach (var id in idOeuvres.Select((position) => new { position }))
            {
                tableauOeuvres[id.position - 1, 0] = id.position;
                var indiceCriticiteOui = (from oeuvre in dc.Connaissance
                                          where oeuvre.ID_Oeuvre == id.position
                                          where oeuvre.ID_Reponse == 1
                                          select oeuvre.ID_Reponse).Count();
                var indiceCriticiteNon = (from oeuvre in dc.Connaissance
                                          where oeuvre.ID_Oeuvre == id.position
                                          where oeuvre.ID_Reponse == 2
                                          select oeuvre.ID_Reponse).Count();

                tableauOeuvres[id.position - 1, 1] = 100/(indiceCriticiteOui + indiceCriticiteNon);
            }

            oeuvresEnCriticite = tableauOeuvres;

            return tableauOeuvres;
        }
        public void RecenserGenresPeintresPossibles()
        {
            // On récupère tous les peintres connus dans la base pour les ajouter à la liste "peintres"
            var idPeintres = (from q in dc.Question
                              join c in dc.Connaissance on q.ID_Q equals c.ID_Question
                              join o in dc.Oeuvre on c.ID_Oeuvre equals o.ID_O
                              where q.Libelle == o.Artiste
                              select q.ID_Q).Distinct();

            foreach (int idP in idPeintres)
            {
                peintres.Add(idP);
            }

            // On récupère tous les genres connus dans la base pour les ajouter à la liste "genres"
            var idGenres = (from q in dc.Question
                            join c in dc.Connaissance on q.ID_Q equals c.ID_Question
                            join o in dc.Oeuvre on c.ID_Oeuvre equals o.ID_O
                            where q.Libelle == o.Genre
                            select q.ID_Q).Distinct();

            foreach (int idG in idGenres)
            {
                genres.Add(idG);
            }
        } /*Recense tous les genres et peintres de la base,
                                                          puis les ajoute aux liste "genres" et "peintres"*/
     
        #endregion

        #region FONCTIONS CHOIX ET FORMULATION DE LA PROCHAINE QUESTION
        public int PoserQuestionV1(out int numQ)
        {

            var idQ = dc.Question.Distinct();
            int nbQuestionsBase = idQ.Count();
            int[,] poidsQuestion = new int[nbQuestionsBase, 3]; /*où poidsQuestion[,0] = numéro question
                                                    poidsQuestion[,1] = nombre de oui
                                                    poidsQuestion[,2] = nombre de non*/

            // Nombre de OUI + JSP pour chaque question dans la base
            var compteOuiQues = from c in dc.Connaissance
                                where !idQuestionsPoses.Contains(c.ID_Question)
                                join a in dc.Oeuvre on c.ID_Oeuvre equals a.ID_O
                                where c.Oeuvre.Flag.Value &&
                                c.ID_Reponse == 1 || c.ID_Reponse == 3
                                group c by c.ID_Question
                                into gro
                                select new
                                {
                                    clef = gro.Key,
                                    compte = gro.Count(),
                                };

            int compteur = 0;
            foreach (var rep in compteOuiQues)
            {
                poidsQuestion[compteur, 0] = rep.clef;
                poidsQuestion[compteur, 1] = rep.compte;
                compteur++;
            }

            compteur = 0;

            // Nombre de NON + JSP pour chaque question dans la base
            var compteNonQues = from c in dc.Connaissance
                                where !idQuestionsPoses.Contains(c.ID_Question)
                                join a in dc.Oeuvre on c.ID_Oeuvre equals a.ID_O
                                where c.Oeuvre.Flag.Value &&
                                c.ID_Reponse == 2 || c.ID_Reponse == 3
                                group c by c.ID_Question
                                into grn
                                select new
                                {
                                    clef = grn.Key,
                                    compte = grn.Count(),
                                };

            foreach (var rep in compteNonQues)
            {
                poidsQuestion[compteur, 2] = rep.compte;
                compteur++;
            }

            int maxOui = 0;
            int maxNon = 0;

            if (compteOuiQues.Any() || compteNonQues.Any())
            {
                if (compteOuiQues.Any())
                {
                    maxOui =
                        (from coq in compteOuiQues
                            select coq.compte).Max();
                }
                else
                {
                
                    maxNon = (from cnq in compteNonQues
                    select cnq.compte).Max();

                }
                
                /* On choisit la question qui permet d'éliminer le plus d'oeuvres,
                   donc qui enregistre le plus grand compteur */
            if (maxOui > maxNon)
                {
                    numQ =
                    (from c in compteOuiQues
                     where c.compte == maxOui
                     select c.clef).FirstOrDefault();
                }
                else
                {
                    numQ =
                    (from c in compteNonQues
                     where c.compte == maxNon
                     select c.clef).FirstOrDefault();
                }

                idQuestionsPoses.Add(numQ); // On ajoute la question présente à la liste des questions déjà posées
            }

            else
            {
                numQ = 0;
            }
            return numQ;
        }
        public int PoserQuestionV2(out int numQ, int numEtape)
        {
            var idQ = dc.Question.Distinct();
            int nbQuestionsBase = idQ.Count();
            int nbOeuvres = dc.Oeuvre.Where(c => c.Flag.Value).Count();

            //Détermination du nb d'oeuvres minimal à éliminer à chaque tour sur 25
            int tauxCouvertureMin = nbOeuvres/(25 - numEtape + 1);


            if (numEtape > 22)
            {
                if (numEtape == 24) // Si avant dernière Q° a été répondue NON => suppression de l'oeuvre des probables
                {
                    int valRep;
                    var repDerQues = dictionnaireQR.TryGetValue(idQuestionsPoses.Last(), out valRep);
                        if (repDerQues && valRep == 2)
                        {
                        oeuvresProbables.Remove(oeuvreProposeEnCours);
                        oeuvresEnCriticite[oeuvreProposeEnCours - 1, 2] = 0;
                        }
                }

                    int max = 0;
                    foreach (var tableau in oeuvresProbables)
                    {
                        if (oeuvresEnCriticite[tableau - 1, 2] > max)
                        {
                            max = oeuvresEnCriticite[tableau - 1, 2];
                            oeuvreProposeEnCours = tableau;
                        }
                    }
                    if (!oeuvresProbables.Any())
                    {
                        max = 0;
                        for (int i = 0; i < oeuvresEnCriticite.GetLength(0); i++)
                        {
                            if (oeuvresEnCriticite[i, 2] > max)
                            {
                                max = oeuvresEnCriticite[i, 2];
                                oeuvreProposeEnCours = oeuvresEnCriticite[i, 0];
                            }
                        }

                    }
                    var questions = from c in dc.Connaissance
                        where c.ID_Oeuvre == oeuvreProposeEnCours && !idQuestionsPoses.Contains(c.ID_Question)
                        select c.ID_Question;

                    numQ = questions.FirstOrDefault();
                }
            
            else
            {
                #region Comptage NB Oui/NSP et Non/NSP par question

                // Nombre de OUI + JSP pour chaque question dans la base
                var compteOuiNSPQues = from c in dc.Connaissance
                    where !idQuestionsPoses.Contains(c.ID_Question)
                    join a in dc.Oeuvre on c.ID_Oeuvre equals a.ID_O
                    where c.Oeuvre.Flag.Value &&
                          c.ID_Reponse == 1 || c.ID_Reponse == 3
                    group c by c.ID_Question
                    into gro
                    select new
                    {
                        clef = gro.Key,
                        compte = gro.Count(),
                    };

                // Nombre de NON + JSP pour chaque question dans la base
                var compteNonNSPQues = from c in dc.Connaissance
                    where !idQuestionsPoses.Contains(c.ID_Question)
                    join a in dc.Oeuvre on c.ID_Oeuvre equals a.ID_O
                    where c.Oeuvre.Flag.Value &&
                          c.ID_Reponse == 2 || c.ID_Reponse == 3
                    group c by c.ID_Question
                    into grn
                    select new
                    {
                        clef = grn.Key,
                        compte = grn.Count(),
                    };

                #endregion

                Dictionary<int, int> dicoOuiNSP = new Dictionary<int, int>(); 
                Dictionary<int, int> dicoNonNSP = new Dictionary<int, int>(); 

                foreach (var rep in compteOuiNSPQues) dicoOuiNSP.Add(rep.clef, rep.compte);
                foreach (var rep in compteNonNSPQues) dicoNonNSP.Add(rep.clef, rep.compte);
                List<Question> Q = new List<Question>();

                for (int i = 1; i <= nbQuestionsBase; i++)
                {
                    var nbOui = (from d in dicoOuiNSP
                        where d.Key == i
                        select d.Value).FirstOrDefault();
                    var nbNon = (from d in dicoNonNSP
                        where d.Key == i
                        select d.Value).FirstOrDefault();

                    if (!idQuestionsPoses.Contains(i))
                    {
                        Q.Add(new Question(i, nbOui, nbNon));
                    }
                }

                // ETAPE 1
                // On trie les questions par nombre de tableaux éliminés (ordre descendant) => nouvelle liste
                var QuesOrdonnees = Q.OrderByDescending(i => i.CalculerSommeOuiNon()).ToList();
                Q = QuesOrdonnees;

                // ETAPE 2
                // On s'intéresse désormais aux questions "équilibrées", avec un taux de couverture suffisamment intéressant
                // La question idéale est bien évidemment celle pour laquelle la différenceOuiNon = 0 et le nombre de tableaux
                // éliminés à l'issue de la question >= taux de couverture (nbOeuvres/nbQuestionsMax)
                List<Question> questionsCandidates = new List<Question>();

                foreach (var q in Q)
                {

                    if (q.sommeOuiNonNSP > tauxCouvertureMin && q.diffOuiNonNSP <= tauxCouvertureMin)
                    {
                        pertinence = true; // Question candidate
                        questionsCandidates.Add(q);
                    }
                    if (tauxCouvertureMin == 0 && q.sommeOuiNonNSP >= tauxCouvertureMin)
                    {
                        pertinence = true; // Question candidate
                        questionsCandidates.Add(q);
                    }
                }



                // Deuxième tri pour ordonner les questions à égalité sur le nb d'oeuvres couvertes
                int compteur1 = 0;
                int compteur2;
                foreach (var w in Q.Where(t => t.pertinence))
                {
                    compteur2 = compteur1 + 1;
                    while (compteur2 < questionsCandidates.Count)
                    {
                        if (w.sommeOuiNonNSP == questionsCandidates[compteur2].sommeOuiNonNSP)
                        {
                            if (w.diffOuiNonNSP > questionsCandidates[compteur2].diffOuiNonNSP)
                            {
                                int indexMauvaiseQuestion = questionsCandidates.IndexOf(w);
                                int indexBonneQuestion = questionsCandidates.IndexOf(questionsCandidates[compteur2]);
                                Question tmp = questionsCandidates[indexMauvaiseQuestion];
                                questionsCandidates[indexMauvaiseQuestion] = questionsCandidates[indexBonneQuestion];
                                questionsCandidates[indexBonneQuestion] = tmp;
                            }
                        }
                        compteur2++;
                    }
                    compteur1++;
                }

                /* Pour visualiser le processus d'"écrémage des questions, décommentez le code ci-dessous*/
                /*foreach (var w in questionsCandidates) 
                {
                    Console.WriteLine("ID : " + w.id + "; Nb OUI : " + w.nbOuiNSP + "Nb NON: " + w.nbNonNSP + " ; Diff : " +
                                      w.diffOuiNonNSP);
                }*/

                // Sélection de la meilleure question
                if (!questionsCandidates.Any()) numQ = QuesOrdonnees[0].id; 
                else
                {
                    Question QAP;
                    int compteur = 0;

                    if (questionsCandidates.Count>1)
                    {
                        while (questionsCandidates[compteur].nbNonNSP == questionsCandidates[compteur + 1].nbNonNSP && questionsCandidates[compteur].nbOuiNSP == questionsCandidates[compteur + 1].nbOuiNSP && compteur < questionsCandidates.Count - 2)
                        {
                            compteur++;
                        }
                    }

                    if (compteur > 0)
                    {
                        Random k = new Random();
                        k.Next(0, compteur + 1);
                        QAP = questionsCandidates[compteur];
                    }
                    else
                    {
                        QAP = questionsCandidates.First();
                    }
                    numQ = QAP.id;
                }
            
          }
            idQuestionsPoses.Add(numQ); // On ajoute la question sélectionnée à la liste des questions déjà posées
            return numQ;
        }
        public string FormulerQuestion(int numeroQ)
        {
            Random k = new Random();

            string libelleQ = (from q in dc.Question // On récupère le libellé associé au numéro de la question
                               where q.ID_Q == numeroQ
                               select q.Libelle).FirstOrDefault();

            string questionAFormuler = null;

            string plurielSingulier = libelleQ.Substring(libelleQ.Length - 1, 1);

            string type = (from q in dc.Question
                where q.ID_Q == numeroQ
                select q.Type).FirstOrDefault();

            #region formulation par type de questions

            switch (type)
            {
                case "genre":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "S'agit-il d'un(e) ";
                            break;
                        case 2:
                            questionAFormuler = "Peut-on classer ce tableau comme ";
                            break;
                    }
                    break;

                case "peintre":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "Ce tableau est-il de ";
                            break;
                        case 2:
                            questionAFormuler = "Ce tableau a-t-il été peint par ";
                            break;
                    }
                    break;

                case "personnage":

                   switch (k.Next(1, 3))
                        {
                            case 1:
                                questionAFormuler = "Voit-on dans le tableau un(e) ou plusieurs ";
                                break;
                            case 2:
                                questionAFormuler = "Y a-t-il dans le tableau un(e) ou plusieurs ";
                                break;
                        }
                    break;

                case "personnage religieux":
                   switch (k.Next(1, 3))
                        {
                            case 1:
                                questionAFormuler = "Voit-on dans le tableau ";
                                break;
                            case 2:
                                questionAFormuler = "Y a-t-il dans le tableau ";
                                break;
                        }
                    break;

                case "accessoire":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "Le(s) personnage(s) porte(nt)-t-il(s) un(e)/des ";
                            break;
                        case 2:
                            questionAFormuler = "Les personnage(s) est/sont-il(s) doté(s) d'un(e)/de ";
                            break;
                    }
                    break;

                case "attribut":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "Le(s) personnage(s) a/ont-il(s) un(e) ";
                            break;
                        case 2:
                            questionAFormuler = "Distingue-t-on sur le(s) personnage(s) un(e) ";
                            break;
                    }
                    break;

                case "profession":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "Le(s) personnage(s) exerce(nt)-t-il(s) la profession d(')(e) ";
                            break;
                        case 2:
                            questionAFormuler = "Le/un des personnage(s) travaille(nt)-t-il(s) en tant que ";
                            break;
                    }
                    break;

                case "apparence":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "Le(s) personnage(s) est/sont-il(s) ";
                            break;
                        case 2:
                            questionAFormuler = "Y-a-t-il un des personnages ";
                            break;
                    }
                    break;

                case "relation":
                         questionAFormuler = "Le(s) personnage(s) est/sont-il(s) ";
                         break;

                case "classe":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "Le(s) personnage(s) appartien(nen)t-il(s) à la classe ";
                            break;
                        case 2:
                            questionAFormuler = "Le(s) personnage(s) fait/font-il(s) partie de la classe ";
                            break;
                    }
                    break;
                case "visage":
                    questionAFormuler = "Le visage du personnage est-il ";
                    break;

                case "attitude":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "Distingue-t-on dans l'attitude des personnages du/de la ";
                            break;
                        case 2:
                            questionAFormuler = "Le(s) personnage(s) font-ils preuve de/d' ";
                            break;
                    }
                    break;

                case "regard":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "Discerne-t-on dans le regard du/des personnage(s) de l'";
                            break;
                        case 2:
                            questionAFormuler = "Le regard du/des personnage(s) est-il porteur de/d'";
                            break;
                    }
                    break;

                case "action":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "Le(s) personnage(s) est/sont-il(s) en train de ";
                            break;
                        case 2:
                            questionAFormuler = "Aperçoit-on le(s) personnage(s) en train de ";
                            break;
                    }
                    break;

                case "lieu":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "La scène se déroule-t-elle dans un(e) ";
                            break;
                        case 2:
                            questionAFormuler = "La scène prend-t-elle place dans un(e) ";
                            break;
                    }
                    break;

                case "moment":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "La scène se déroule-t-elle durant le/la ";
                            break;
                        case 2:
                            questionAFormuler = "La scène prend-t-elle pendant le/la ";
                            break;
                    }
                    break;

                case "atmosphère":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "L'atmosphère est-elle ";
                            break;
                        case 2:
                            questionAFormuler = "Se joue-t-il dans le tableau une atmosphère ";
                            break;
                    }
                    break;

                case "sensation":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "Se dégage-t-il du tableau une impression d'/de ";
                            break;
                        case 2:
                            questionAFormuler = "Ce tableau invite-t-il à la/au ";
                            break;
                    }
                    break;

                case "tons":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "Les tons dominants du tableau sont-ils ";
                            break;
                        case 2:
                            questionAFormuler = "Le tableau est-il baigné de tons ";
                            break;
                    }
                    break;

                case "formes":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "Au niveau des formes, distingue-t-on des ";
                            break;
                        case 2:
                            questionAFormuler = "Le tableau est-il composé de ";
                            break;
                    }
                    break;

                case "thème":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "Est-ce une ode à la/au ";
                            break;
                        case 2:
                            questionAFormuler = "Le thème du tableau se rapporte-t-il à la/au ";
                            break;
                    }
                    break;

                case "allégorie":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "Voit on dans le tableau l'allégorie du/de la ";
                            break;
                        case 2:
                            questionAFormuler = "Distingue-t-on dans le tableau la figure allégorique du/de la ";
                            break;
                    }
                    break;

                case "fond":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "Le tableau est-il de fond ";
                            break;
                        case 2:
                            questionAFormuler = "L'arrière-plan est-il ";
                            break;
                    }
                    break;

                case "réplique":
                    questionAFormuler = "S'agit-il d'une ";
                    break;

                case "mouvement":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "Le tableau appartient-il au mouvement du ";
                            break;
                        case 2:
                            questionAFormuler = "Le peintre appartient-il au mouvement du ";
                            break;
                    }
                    break;

                case "caractéristique":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "Ce tableau est-il particulièrement ";
                            break;
                        case 2:
                            questionAFormuler = "S'agit-il d'un tableau particulièrement ";
                            break;
                    }
                    break;
                
                case "technique":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "La technique utilisées par le peintre est-elle celle de (la) ";
                            break;
                        case 2:
                            questionAFormuler = "Le tableau a-t-il été réalisé à l'aide de ";
                            break;
                    }
                    break;

                case "lieu d'exposition":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "Ce tableau est-il exposé au/à la  ";
                            break;
                        case 2:
                            questionAFormuler = "Le lieu d'exposition du tableau est-il le/la ";
                            break;
                    }
                    break;

                case "élément naturel":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "Discerne-t-on dans le tableau le/la ";
                            break;
                        case 2:
                            questionAFormuler = "Voit-on dans le tableau le/la ";
                            break;
                    }
                    break;

                case "ciel":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "Peut-on dire du ciel qu'il est ";
                            break;
                        case 2:
                            questionAFormuler = "Le ciel du tableau est-il ";
                            break;
                    }
                    break;

                case "instrument de musique":
                    switch (k.Next(1, 3))
                    {
                        case 1:
                            questionAFormuler = "S'agit-il de l'instrument de musique : ";
                            break;
                        case 2:
                            questionAFormuler = "L'instrument de musique représenté est-il un(e) ";
                            break;
                    }
                    break;

                case null :

                    if (plurielSingulier != "s" && plurielSingulier != "x" && plurielSingulier != ")") // singulier
                    {
                        switch (k.Next(1, 3))
                        {
                            case 1:
                                questionAFormuler = "Voit-on dans le tableau un(e) ";
                                break;
                            case 2:
                                questionAFormuler = "Y a-t-il dans le tableau un(e) ";
                                break;
                        }
                    }
                    else
                    {
                        switch (k.Next(1, 3))
                        {
                            case 1:
                                questionAFormuler = "Voit-on dans le tableau un(e) ou plusieurs ";
                                break;
                            case 2:
                                questionAFormuler = "Y a-t-il dans le tableau un(e) ou plusieurs ";
                                break;
                        }
                    }
                    break;
                    #endregion

            }

            return questionAFormuler+libelleQ +" ?";
        }
        #endregion

        #region SAUVEGARDE DES QUESTIONS/REPONSES DE LA PARTIE
        public void MemoriserQuestionReponse(int numQ, int numR)
        {
            dictionnaireQR.Add(numQ, numR); // On ajoute question + réponse au dictionnaire QR

            if (VerifierQuestionDoublon()) // On vérifie si on peut éliminer des questions doublons sur le peintre/genre
            {
                if (doublonGenres && !doublonPeintres)
                {
                    EliminerQuestionsOeuvresDoublons(genres, numQ);
                }
                else
                {
                    EliminerQuestionsOeuvresDoublons(peintres, numQ);
                }
            }

        } /*Enregistre la réponse donnée par le joueur.
                                                                      Si le joueur a répondu positivement à une question portant sur un
                                                                      genre ou un peintre, élimination des questions relatives aux autres genres
                                                                      et peintres*/
        public bool VerifierQuestionDoublon()
        {
            bool estDoublon = false;

            if (!doublonGenres || !doublonPeintres)
            {
                foreach (KeyValuePair<int, int> qR in dictionnaireQR)
                {
                    if (!doublonPeintres)
                    {
                        foreach (int p in peintres)
                        {
                            /* Si la réponse donnée par le joueur sur le peintre est "OUI"
                           alors inutile d'interroger à nouveau le joueur sur un autre peintre"*/
                            if (qR.Key == p && qR.Value == 1)
                            {
                                doublonPeintres = true;
                                estDoublon = true;
                            }
                        }
                    }

                    if (!doublonGenres)
                    {
                        foreach (int g in genres)
                        {
                            if (qR.Key == g && qR.Value == 1)
                            {
                                doublonGenres = true;
                                estDoublon = true;
                            }
                        }
                    }
                }
            }

            return estDoublon;
        }

        #endregion

        #region FONCTIONS DE REACTUALISATION DES QUESTIONS A POSER/ DE REAFFECTATION DE LA CRITICITE DES OEUVRES CANDIDATES
        public void EliminerQuestionsOeuvres(int numQ, int numRep)
        {
            MemoriserQuestionReponse(numQ, numRep);

            bool retireOeuvre = true;

            if (numRep == 1 || numRep == 2) // On ne prend pas en compte la réponse "Je ne sais pas"
            {
                var oeuvres = (from co in dc.Connaissance
                    join o in dc.Oeuvre on co.ID_Oeuvre equals o.ID_O
                    where co.ID_Question == numQ && (co.ID_Reponse != numRep)
                    where o.Flag == true
                    select o).Distinct();

                if (!oeuvres.Any()) /* Si aucune oeuvre n'a été trouvée, la question doit toutefois être éliminée*/
                {
                    retireOeuvre = false;
                }

                /* On récupère l'ID de la question posée et celui/ceux des oeuvres qui ne vérifient pas
            la réponse donnée par l'utilisateur à la question en cours*/

                if (retireOeuvre)
                {
                    foreach (var o in oeuvres)
                    {
                        // Le flag des oeuvres passe à faux : elles sont considérées hors jeu
                        o.Flag = false;
                        dc.SubmitChanges();
                    }
                }
            }

            AfficherOeuvresRestantes();
        }
        public void EliminerQuestionsOeuvresDoublons(List<int> peintresOuGenres, int num)
        {
            List<int> collection = peintresOuGenres;
            // Les questions doublons sont éliminées
            foreach (int numero in collection) 
            {
                if (numero != num)
                {
                    idQuestionsPoses.Add(numero);
                }

            }

            // Les oeuvres hors genre/peintre sont éliminées
            string reference = (from q in dc.Question
                                where q.ID_Q == num
                                select q.Libelle).FirstOrDefault();

            if (Enumerable.SequenceEqual(collection.OrderBy(t => t), genres.OrderBy(t => t)))
            {
                var oeuvresAEliminer = from o in dc.Oeuvre
                where o.Genre != reference && o.Flag == true
                select o;

                foreach (var peinture in oeuvresAEliminer)
                {
                    peinture.Flag = false;
                }
            }
            else
            {
                var oeuvresAEliminer = from o in dc.Oeuvre
                                       where o.Artiste!= reference && o.Flag == true
                                       select o;

                foreach (var peinture in oeuvresAEliminer)
                {
                    peinture.Flag = false;
                }
            }

            dc.SubmitChanges();
        }
        public int[,] AffecterCriticiteOeuvre(int reponse, int question, int[,] oeuvres)
        {
            var RechercheOeuvreCritique = (from oeuvrePotentiel in dc.Connaissance
                                           where oeuvrePotentiel.ID_Question == question && oeuvrePotentiel.ID_Reponse == reponse && oeuvrePotentiel.ID_Reponse != 3
                                           select oeuvrePotentiel.ID_Oeuvre).Distinct();

            // Affectation de la criticité pour les oeuvres candidates
            foreach (var idOeuvres in RechercheOeuvreCritique.Select((iter) => new { iter }))
            {
                oeuvres[idOeuvres.iter.Value - 1, 2] = oeuvres[idOeuvres.iter.Value - 1, 2] + oeuvres[idOeuvres.iter.Value - 1, 1];

                var flag = (from o in dc.Oeuvre
                    where o.ID_O == oeuvres[idOeuvres.iter.Value - 1, 0]
                    select o.Flag).FirstOrDefault();

                // Préselection des oeuvres critiques à 60%
                if (oeuvres[idOeuvres.iter.Value - 1, 2] >= 60 && (bool)flag)
                {
                    oeuvresProbables.Add(idOeuvres.iter.Value);
                    bool candidat = true;
                }
            }
            return oeuvres;
        }
        #endregion

        #region FONCTIONS FIN PARTIE
        public int DeciderOeuvreCandidate()
        {
            int oeuvre = 0;
            Random k = new Random();

            // On récupère le nombre de fois qu'une oeuvre a été jouée
            var occur = (from o in dc.Oeuvre
                         where oeuvresCandidates.Contains(o.ID_O)
                         select o.Occurence).ToList();

            var groupement = occur
                            .GroupBy(gr => gr)
                            .Select(group => new { Count = group.Count(), key = group.Key });

            // On identifie la plus forte occurence parmi toutes les oeuvres candidates
            var occurMax = occur.Max();

            foreach (var i in groupement)
            {
                if (i.Count == 1 && i.key == occurMax) // Il y a une unique occurence maximum
                {
                   oeuvre = ChoisirOeuvreParOccurence((int)occurMax);
                }

                else // Si elles ont toutes la même occurence, on tire au hasard parmi les plus fortes occurrences
                {
                    var maxs = (from g in groupement
                        where g.Count == occurMax
                        select g.key).ToList();

                    int nbOeuvresConcernees = groupement.Count();
                    int numO = ChoisirOeuvreParOccurence(k.Next(1, nbOeuvresConcernees + 1));
                    oeuvre = (int)maxs[numO];
                }
            }
            return oeuvre;
        }
        public int ChoisirOeuvreParOccurence(int occurence)
        {
            int oeuvreAQuestionner = (from o in dc.Oeuvre
                                      where oeuvresProbables.Contains(o.ID_O)
                                            && o.Occurence == occurence
                                      select o.ID_O).FirstOrDefault();

            return oeuvreAQuestionner;
        }
        public string ArreterPartie(int[,] tab, int numEtape, out int numTab) /* Renvoie dans le cas d'un succès le nom du tableau;
                                                                                 sinon une chaîne de caractères à valeur NULL*/
        {
            int numPeinture = 0;
            string peinture;
            int compteur = (from o in dc.Oeuvre where o.Flag == true select o).Count();

            if (compteur >= 1) // S'il reste plus d'une oeuvre candidate
                {
                    int id = dc.Oeuvre.Where(x => x.Flag == true).Select(x => x.ID_O).First();
                    if (compteur == 1 && tab[id-1, 2] >= 75)
                    {
                        numPeinture = id;
                    }
                    else
                    {
                        var numTableauxElimines = (from t in dc.Oeuvre where t.Flag == false select t.ID_O).Distinct();

                        for (int i = 0; i < tab.GetLength(0); i++)
                        {
                            if (!numTableauxElimines.Contains(tab[i, 0]))
                                // Les tableaux déjà éliminés ne peuvent pas être proposés
                            {
                                // 75% des propriétés du tableau sont vérifiées et l'oeuvre n'a pas déjà été éliminée
                                if (tab[i, 2] >= 75)
                                {
                                    numPeinture = tab[i, 0];
                                    oeuvresCandidates.Add(numPeinture);
                                }
                            }
                        }
                    }
                }

            else
            {
                    int indice = 0;

                    for (int i = 0; i < tab.GetLength(0); i++)
                    {
                        if (tab[i, 2] > indice && tab[i, 2] > 75)
                        {
                            indice = tab[i, 2];
                            numPeinture = tab[i, 0];
                        }
                    }
                
            }

            numTab = numPeinture;

            // Recherche du nom de l'oeuvre d'identifiant numPeinture
            var NomOeuvre = from oeuvre in dc.Oeuvre
                            where oeuvre.ID_O == numPeinture
                            select oeuvre.Nom;

            peinture = NomOeuvre.FirstOrDefault();

            if (!string.IsNullOrEmpty(peinture))
            {
                nbTentatives++;
            }
            return peinture;
        }
        public void EliminerTableau(int oeuvre)
        {
            
            var oeuvreSuppr = (from o in dc.Oeuvre
                               where o.ID_O == oeuvre
                               select o).FirstOrDefault();
            oeuvreSuppr.Flag = false;

            dc.SubmitChanges();
            oeuvresCandidates.Remove(oeuvreSuppr.ID_O);
        }
        #endregion
        public void AfficherOeuvresRestantes()
        {
            var p = from o in dc.Oeuvre
                where o.Flag == true
                select o;
            Console.WriteLine("OEUVRES RESTANTES :");
            foreach (var x in p)
            {
                Console.WriteLine("Oeuvre : " +x.Nom +" - Criticité : " +oeuvresEnCriticite[x.ID_O-1,2]);
            }
            Console.WriteLine("------------------");
        } /*Fonction annexe d'affichage des oeuvres encore en lice
                                                        (test sur la performance de l'algo)*/
    }
}
