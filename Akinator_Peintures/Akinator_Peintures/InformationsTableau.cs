using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Akinator_Peintures
{
    public partial class InformationsTableau : Form
    {
        private LINQDataContext dc;
        private int tableau;

        public InformationsTableau(int tableau)
        {
            InitializeComponent();
            dc = new LINQDataContext();
            this.tableau = tableau;
        }
        private void InformationsTableau_Load(object sender, EventArgs e)
        {
            var oeuvre = dc.Oeuvre.Where(id => id.ID_O == tableau).FirstOrDefault();
            NomEntreeLbl.Text = oeuvre.Nom;
            PeintreEntreeLbl.Text = oeuvre.Artiste;
            DescriptifEntreeLbl.Text = oeuvre.Descriptif;
        }
    }
}
