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
            getActivites();
            return liste;
        }

        public void getActivites()
        {
            liste.Clear();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from activites";
            con.Open();

            //reader est utiliser pour les select //---// Scalar pour les fonction comme count et nonQuery pour les modify , update create , 
            MySqlDataReader r = commande.ExecuteReader();


            while (r.Read())
            {
                String s_idActivite = r["idActivite"].ToString();
                int idActivite = Convert.ToInt16(s_idActivite);

                String nom = r["nom"].ToString();

                String s_coutOrganisation = r["coutOrganisation"].ToString();
                int coutOrganisation = Convert.ToInt16(s_coutOrganisation);

                String s_prixVente = r["prixDeVente"].ToString();
                int prixVente = Convert.ToInt16(s_prixVente);

                String s_idCategorie = r["idCategorie"].ToString();
                int idCategorie = Convert.ToInt16(s_idCategorie);

                Activites activite = new Activites(idActivite, nom, coutOrganisation , prixVente , idCategorie);



                liste.Add(activite);
            }

            r.Close();
            con.Close();
        }

    }
}
