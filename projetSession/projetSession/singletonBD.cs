using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetSession
{
    internal class singletonBD
    {

        MySqlConnection con;
        ObservableCollection<Activites> liste;
        static singletonBD instance = null;


        public singletonBD()
        {
            liste = new ObservableCollection<Activites>();

            //con est déclarer plus haut comme variable globale. et est initialiser ici dans le constructeur
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2024_420335ri_eq3;Uid=6269818;Pwd=6269818;");


        }

        public static singletonBD getInstance()
        {
            if (instance == null)
                instance = new singletonBD();

            return instance;
        }

        public ObservableCollection<Activites> getListe()
        {
            getProduits();
            return liste;
        }

        public void getActivites()
        {
            liste.Clear();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from produits";
            con.Open();

            //reader est utiliser pour les select //---// Scalar pour les fonction comme count et nonQuery pour les modify , update create , 
            MySqlDataReader r = commande.ExecuteReader();


            while (r.Read())
            {

                String nom = r["nom"].ToString();
                String categorie = r["categorie"].ToString();
                String prix = r["prix"].ToString();
                double iPrix = Convert.ToDouble(prix);
                Produits produit = new Produits(nom, iPrix, categorie);



                liste.Add(produit);
            }

            r.Close();
            con.Close();
        }

    }
}
