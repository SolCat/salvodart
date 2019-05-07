using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Akinator_Peintures
{
    public partial class MenuPrincipal : Form
    {
        public MenuPrincipal()
        {
            InitializeComponent();
        }

        private void CommencerBtn_Click(object sender, EventArgs e)
        {
            MenuQuestion enigme = new MenuQuestion();
            this.Hide();
            enigme.Show();
        }
    }
}
