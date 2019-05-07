using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Akinator_Peintures
{
    public partial class MenuResolutionEnigme : Form
    {
        private LINQDataContext dc;
        private string oeuvre;
        private int nbTentatives, numRefTableau;
        private bool echec;
        private Dictionary<int, int> QuestionsReponses;
        public MenuResolutionEnigme(string oeuvre, int nbTentatives, Dictionary<int, int> QR, bool echec)
        {
            InitializeComponent();
            dc = new LINQDataContext();
            this.oeuvre = oeuvre;
            this.nbTentatives = nbTentatives;
            QuestionsReponses = QR;
            this.echec = echec;
        }

        public MenuResolutionEnigme(Dictionary<int, int> QR)
        {
            InitializeComponent();
            dc = new LINQDataContext();
            QuestionsReponses = QR;
            nbTentatives = 2;
            echec = true;
        }

        private void MenuResolutionEnigme_Load(object sender, EventArgs e)
        {

            PeintreCBox.DataSource = dc.Question.Where(x => x.Type == "peintre").OrderBy(x => x.Libelle).Select(x => x.Libelle);
            GenreCbox.DataSource = dc.Question.Where(x => x.Type == "genre").OrderBy(x => x.Libelle).Select(x => x.Libelle);
            var questions = (from q in dc.Question
                            where !dc.Oeuvre.Select(x => x.Genre).Distinct().ToList().Contains(q.Libelle)
                            && !dc.Oeuvre.Select(x => x.Artiste).Distinct().ToList().Contains(q.Libelle)
                            select q.Libelle).Distinct();

            GenreCbox.SelectedIndex = -1;
            PeintreCBox.SelectedIndex = -1;
            QuestionCBox.DataSource = questions;
            QuestionCBox.SelectedIndex = -1;


            if (nbTentatives == 2 && echec)
                {
                    DaliPanel.Visible = false;
                    OeuvreLbl.Text = "Grrr... J'ai été battu... \nEt si vous m'éclairiez un peu ?";
                    OuiBtn.Visible = NonBtn.Visible = JouerNvllePartieLbl.Visible = false;
                }

            else if ((nbTentatives <= 2 && !echec))
                {

                foreach (Control c in InfosGBox.Controls) c.Visible = false;

                InfosGBox.Visible = false;
                OkBtn.Visible = false;
                OeuvreLbl.Text = "J'ai gagné !" + "\nJ'ai deviné l'oeuvre : " + oeuvre;


                // Affichage du tableau
                int idTab = dc.Oeuvre.Where(w => w.Nom == oeuvre).Select(w => w.ID_O).First();
                var tableau = dc.Oeuvre.Where(x => x.ID_O == idTab).Select(x => x.Img).First();
                PeinturePBox.Image = ConvertirImage(tableau);
                
                /* On incrémente le champ occurence du tableau déjà connu dans la base */
                var idO = (from o in dc.Oeuvre
                           where o.Nom == oeuvre
                           select o).FirstOrDefault();

                idO.Occurence++;
                dc.SubmitChanges();

                }
        }

        private void OeuvreTBox_TextChanged(object sender, EventArgs e)
        {
            TextBox T = sender as TextBox;

            if (!Regex.IsMatch(T.Text, @"^[a-zA-Z\s\w]+$"))
            {
                T.BackColor = Color.LightCoral;
                OkBtn.Enabled = false;
            }
            else
            {
                T.BackColor = Color.DarkSeaGreen;
                OkBtn.Enabled = true;
            }
        }

        private void OuiBtn_Click(object sender, EventArgs e)
        {
            List<int> peintres = new List<int>();
            List<int> genres = new List<int>();

            var idPeintres = (from q in dc.Question
                                                   join c in dc.Connaissance on q.ID_Q equals c.ID_Question
                                                   join o in dc.Oeuvre on c.ID_Oeuvre equals o.ID_O
                                                   where q.Libelle == o.Artiste
                                                   select q.ID_Q).Distinct();
            foreach (var p in idPeintres)
            {
                peintres.Add(p);
            }

            var idGenres = (from q in dc.Question
                            join c in dc.Connaissance on q.ID_Q equals c.ID_Question
                            join o in dc.Oeuvre on c.ID_Oeuvre equals o.ID_O
                            where q.Libelle == o.Genre
                            select q.ID_Q).Distinct();

            foreach (int idG in idGenres)
            {
                genres.Add(idG);
            }

            int tableau = (from o in dc.Oeuvre
                               where o.Nom == oeuvre
                               select o.ID_O).Distinct().FirstOrDefault();

            foreach (KeyValuePair<int, int> quesres in QuestionsReponses)
            {

                var req = from c in dc.Connaissance
                          where c.ID_Question == quesres.Key
                                && c.ID_Reponse == quesres.Value
                                && c.ID_Oeuvre == tableau
                          select c.ID_C;

                if (!req.Any())
                {
                    if (!peintres.Contains(quesres.Key) && !genres.Contains(quesres.Key))
                    {
                        Connaissance nouvelleConnaissance = new Connaissance
                        {
                            ID_Oeuvre = tableau,
                            ID_Question = quesres.Key,
                            ID_Reponse = quesres.Value,
                        };
                        dc.Connaissance.InsertOnSubmit(nouvelleConnaissance);
                        dc.SubmitChanges();
                    }
                }
            }

            ReinitialiserJeu();
        }

        private void OkBtn_Click(object sender, EventArgs e)
        {
            /* Sauvegarde en mémoire de la nouvelle oeuvre */
            bool tableauDejaEnMemoire = dc.Oeuvre.Any(tab => tab.Nom == OeuvreTBox.Text);
            bool peintreDejaEnMemoire = dc.Oeuvre.Any(tab => tab.Artiste == PeintreCBox.Text);
            bool genreDejaEnMemoire = dc.Oeuvre.Any(tab => tab.Genre == GenreCbox.Text);

            if (!tableauDejaEnMemoire)
            {
                Oeuvre nouveauTableau = new Oeuvre
                {
                    Nom = OeuvreTBox.Text,
                    Artiste = PeintreCBox.Text,
                    Genre = GenreCbox.Text,
                    Occurence = 1,
                    Flag = true
                };

                numRefTableau = nouveauTableau.ID_O;
                Console.WriteLine(numRefTableau);
                dc.Oeuvre.InsertOnSubmit(nouveauTableau);
                dc.SubmitChanges();

                /* Ajout des questions et réponses posées/données pour le tableau dans la base de connaissance */

                foreach (KeyValuePair<int, int> quesres in QuestionsReponses)
                {
                    Connaissance nouvelleConnaissance = new Connaissance
                    {
                        ID_Oeuvre = nouveauTableau.ID_O,
                        ID_Question = quesres.Key,
                        ID_Reponse = quesres.Value
                    };

                    dc.Connaissance.InsertOnSubmit(nouvelleConnaissance);
                    dc.SubmitChanges();
                }
                if (!peintreDejaEnMemoire)
                {
                    Question q1 = new Question
                    {
                        Libelle = PeintreCBox.Text
                    };

                    dc.Question.InsertOnSubmit(q1);
                    dc.SubmitChanges();

                    Connaissance nouvelleConnaissancePeintre = new Connaissance()
                    {
                        ID_Oeuvre = nouveauTableau.ID_O,
                        ID_Question = q1.ID_Q,
                        ID_Reponse = 1
                    };

                    dc.Connaissance.InsertOnSubmit(nouvelleConnaissancePeintre);
                    dc.SubmitChanges();

                    if (!genreDejaEnMemoire)
                    {
                        Question q2 = new Question
                        {
                            Libelle = PeintreCBox.Text
                        };

                        dc.Question.InsertOnSubmit(q2);
                        dc.SubmitChanges();

                        Connaissance nouvelleConnaissanceGenre = new Connaissance()
                        {
                            ID_Oeuvre = nouveauTableau.ID_O,
                            ID_Question = q2.ID_Q,
                            ID_Reponse = 1
                        };

                        dc.Connaissance.InsertOnSubmit(nouvelleConnaissanceGenre);
                        dc.SubmitChanges();
                    }
                }

            }
            
            /* Ajout du mot clef en tant que question et connaissance */

            string motClef1 = MotClef1TBox.Text;

            if (!String.IsNullOrEmpty(motClef1))
            {
                var doublonMotClef1= from q in dc.Question
                        where q.Libelle == motClef1
                        select q.Libelle;

                    if (!doublonMotClef1.Any()) // Si les mots clefs ne sont pas déjà connu
                    {
                            Question q = new Question()
                            {
                                Libelle = motClef1
                            };

                            dc.Question.InsertOnSubmit(q); dc.SubmitChanges();

                            Connaissance c = new Connaissance()
                            {
                                ID_Question = q.ID_Q,
                                ID_Reponse = 1,
                                ID_Oeuvre = numRefTableau
                            };
                            dc.Connaissance.InsertOnSubmit(c); dc.SubmitChanges();
                    }
                        
             }

            if (QuestionCBox.SelectedIndex != -1) // Un mot parmi la liste a été choisi
            {
                Connaissance c = new Connaissance()
                {
                    ID_Oeuvre = numRefTableau,
                    ID_Question = (from q in dc.Question
                        where q.Libelle == (string)QuestionCBox.SelectedItem
                        select q.ID_Q).FirstOrDefault(),
                    ID_Reponse = 1
                };

                dc.Connaissance.InsertOnSubmit(c); dc.SubmitChanges();
            }

            ReinitialiserJeu();
        }

        private void ReinitialiserJeu()
        {
            foreach (var peinture in dc.Oeuvre)
            {
                peinture.Flag = true;
                dc.SubmitChanges();
            }

            MenuPrincipal menuP = new MenuPrincipal();
            Hide();
            menuP.Show();
        }

        private void ObtenirInformationsTableau(object sender, EventArgs e)
        {
            PeinturePBox.SizeMode = PictureBoxSizeMode.CenterImage;
        }

        private void PeinturePBox_MouseLeave(object sender, EventArgs e)
        {
            PeinturePBox.SizeMode = PictureBoxSizeMode.Zoom;

        }

        private void AfficherInformations(object sender, MouseEventArgs e)
        {
            int idTab = dc.Oeuvre.Where(w => w.Nom == oeuvre).Select(w => w.ID_O).First();
            InformationsTableau infos = new InformationsTableau(idTab);
            infos.Show();
        }

        public static Image ConvertirImage(System.Data.Linq.Binary iBinary)
        {
            var arrayBinary = iBinary.ToArray();
            Image rImage = null;

            using (MemoryStream ms = new MemoryStream(arrayBinary))
            {
                rImage = Image.FromStream(ms);
            }
            return rImage;
        }

    }
}
