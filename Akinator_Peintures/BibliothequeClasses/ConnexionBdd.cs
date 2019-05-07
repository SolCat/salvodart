using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Akinator_Peintures
{
    class ConnexionBdd
    {
        public SqlConnection connexion = new SqlConnection();
        public SqlCommand commande = new SqlCommand();
        public SqlDataReader dr;

        public void Connecter()
        {
            if (connexion.State == ConnectionState.Closed)
            {
                connexion.ConnectionString = @"Initial Catalog = Akina_peintures; Data source = DESKTOP-OK6SV0P\SQLEXPRESS; integrated security=true";
                connexion.Open();
            }
        }

        public void Deconnecter()
        {
            if (connexion.State == ConnectionState.Open)
            {
                connexion.Close();
            }
        }

        public SqlDataReader Afficher(string requete)
        {
            Connecter();
            commande.Connection = connexion;
            commande.CommandText = requete;
            SqlDataReader lecteur = commande.ExecuteReader();
            return lecteur;
        }

    }
}
