using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Windows.Forms;
using System.Data.Entity;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace Akinator_Peintures
{
    public partial class MenuQuestion : Form
    {
        private GestionQuestionsOeuvres gq;
        private int numEtape, numReponse, numQuestion, numOeuvre;
        private int[,] oeuvres;
        private string tableau;
        private bool aucuneProposition, victoire, derniereChance, echecApres2Essais;

        public MenuQuestion()
        {
            InitializeComponent();
            gq = new GestionQuestionsOeuvres();

            // Collection de toutes les oeuvres de la BDD en attente d'affectation d'une criticité
            oeuvres = gq.InstancierTableauOeuvres();
            gq.RecenserGenresPeintresPossibles();
            gq.RecenserGenresPeintresPossibles();

            aucuneProposition = true;

            // Affichage du texte
            var pos = this.PointToScreen(QuestionLbl.Location);
            pos = BullePBox.PointToClient(pos);
            QuestionLbl.Parent = BullePBox;
            QuestionLbl.Location = pos;
            QuestionLbl.BackColor = Color.Transparent;
        }

        private void MenuQuestion_Load(object sender, EventArgs e)
        {
            Initialiser();
        }

        private void Initialiser()
        {
            MenuLbl.Text = (numEtape+1).ToString();

            if (string.IsNullOrEmpty(tableau) && numEtape <= 25) // 25 tours maximum
            {
                //gq.PoserQuestionV1(out numQuestion);
                gq.PoserQuestionV2(out numQuestion, numEtape);

                if (numQuestion != 0)
                {
                    QuestionLbl.Text = gq.FormulerQuestion(numQuestion);
                }
                else
                {
                    MenuResolutionEnigme fin = new MenuResolutionEnigme(gq.dictionnaireQR);
                    this.Hide();
                    fin.Show();
                }

                numEtape++;

            }
            else
            {
                if (numEtape > 25)
                {
                    MenuResolutionEnigme fin = new MenuResolutionEnigme(gq.dictionnaireQR);
                    this.Hide();
                    fin.Show();
                }
            }
        }

        private void ChoisirReponse(object sender, EventArgs e)
        {
            // Identification du choix de réponse (1 : oui; 2 : non; 3 : ne sais pas)
            Button choix = (Button) sender;
            numReponse = int.Parse(choix.Tag.ToString());

            if (aucuneProposition || string.IsNullOrEmpty(tableau))
            {
                /* Elimination des oeuvres qui ne satisfont pas la réponse donnée par l'utilisateur 
                + de la question qui vient d'être posée */
                gq.EliminerQuestionsOeuvres(numQuestion, numReponse);
                // Affectation criticité
                oeuvres = gq.AffecterCriticiteOeuvre(numReponse, numQuestion, oeuvres);
                // Appel de la fonction ArreterPartie() qui vérifie si un tableau est d'ores et déjà candidat
                tableau = gq.ArreterPartie(oeuvres, numEtape, out numOeuvre);

                if (!string.IsNullOrEmpty(tableau))
                {
                    var peintre = (from o in gq.dc.Oeuvre
                        where o.ID_O == numOeuvre
                        select o.Artiste).First();
                    DessinerInterfaceProposition(peintre);
                    aucuneProposition = false;
                }

            }

            else
            {
                if (!aucuneProposition)
                {
                    victoire = true;
                }
                else
                {
                    tableau = gq.ArreterPartie(oeuvres, numEtape, out numOeuvre);

                    var peintre = (from o in gq.dc.Oeuvre
                        where o.ID_O == numOeuvre
                        select o.Artiste).First();

                    DessinerInterfaceProposition(peintre);
                }
            }

            if (!aucuneProposition && victoire) // Il y a une proposition d'Akinator
            {
                if (numReponse != 1 && gq.nbTentatives < 2) // La proposition n'est pas la bonne
                {
                    // On élimine l'oeuvre puisqu'il y a eu confirmation par le joueur
                    gq.EliminerTableau(numOeuvre);
                    tableau = null;
                    aucuneProposition = true;
                    ReinitialiserInterface();
                }
                else
                {
                    if (gq.nbTentatives >= 2 && !derniereChance)
                    {
                        derniereChance = true;
                    }

                    else
                    {
                        if (gq.nbTentatives == 2 && numReponse == 2)
                        {
                            echecApres2Essais = true;
                        }

                        if (victoire)
                        {
                            MenuResolutionEnigme menuRes = new MenuResolutionEnigme(tableau, gq.nbTentatives,
                                gq.dictionnaireQR, echecApres2Essais);
                            Hide();
                            menuRes.Show();
                        }
                    }
                }
            }

            Refresh();
            Initialiser();
        }

        private void DessinerInterfaceProposition(string peintre) // Affichage de la solution
        {
            NspBtn.Visible = false;
            QuestionLbl.Text = tableau + "\n" + "de " + peintre;
        }

        private void ReinitialiserInterface()
        {
            NspBtn.Visible = true;
        }

    }

}


