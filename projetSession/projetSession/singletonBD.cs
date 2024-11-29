using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
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
        ObservableCollection<Adherents> listeAdherents;
        ObservableCollection<Categories> listeCategories;
        static singletonBD instance = null;


        public singletonBD()
        {
            liste = new ObservableCollection<Activites>();
            listeAdherents = new ObservableCollection<Adherents>();
            listeCategories = new ObservableCollection<Categories>();
            //Role = "nonConnecter";
            //con est déclarer plus haut comme variable globale. et est initialiser ici dans le constructeur
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2024_420-345-ri_eq2;Uid=6269818;Pwd=6269818;");


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

               

                

                Activites activite = new Activites(idActivite, nom, coutOrganisation , prixVente  , idCategorie);



                liste.Add(activite);
            }

            r.Close();
            con.Close();
        }

        /*rechercher un activité*/

        public void recherche(string v)
        {
            liste.Clear();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"Select * from activites where nom like  '%{v}%'";
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

                String s_idAmin = r["idAdmin"].ToString();
                int _idAmin = Convert.ToInt16(s_idAmin);

                String s_idCategorie = r["idCategorie"].ToString();
                int idCategorie = Convert.ToInt16(s_idCategorie);

                Activites activite = new Activites(idActivite, nom, coutOrganisation, prixVente, idCategorie);



                liste.Add(activite);
            }

            r.Close();
            con.Close();
        }

        /*supprimer un activité*/

        public void supprimerActivites(String nom)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = $"delete from activites where nom = '{nom}'";

                con.Open();
                int i = commande.ExecuteNonQuery();

                con.Close();

                if (i == 1)
                {
                    getActivites();
                }
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        /*modifier un activité*/

        public void modifierActivites(int _idActivite, String _nom, int _coutOrganisation, int _prixDeVente, int _idAdmin, int _idCategorie)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("p_modifierActivite");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("idActivite", _idActivite);
                commande.Parameters.AddWithValue("nom", _nom);
                commande.Parameters.AddWithValue("coutOrganisation", _coutOrganisation);
                commande.Parameters.AddWithValue("prixDeVente", _prixDeVente);
                commande.Parameters.AddWithValue("idAdmin", _idAdmin);
                commande.Parameters.AddWithValue("idCategorie", _idCategorie);

                con.Open();
                commande.Prepare();
                int i = commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        /*ajouter un activité*/

        public void addActivites( String _nom, double _coutOrganisation, double _prixDeVente, int _idCategorie)
        {

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "insert into activites values( @nom, @coutOrganisation , @prixDeVente , @idCategorie ) ";
          
                commande.Parameters.AddWithValue("@nom", _nom);
                commande.Parameters.AddWithValue("@coutOrganisation", _coutOrganisation);
                commande.Parameters.AddWithValue("@prixDeVente", _prixDeVente);   
                commande.Parameters.AddWithValue("@idCategorie", _idCategorie);


                con.Open();
                commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                //Le if vérifie si la connection est bien ouverte avant d'essayer de la fermer
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }


        //----------------------------------------------------------------Parti Adherents------------------------------------------------------------------/

        public void getAdherents()
        {
            listeAdherents.Clear();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "SELECT* FROM adherents;";
            con.Open();

            //reader est utiliser pour les select //---// Scalar pour les fonction comme count et nonQuery pour les modify , update create , 
            MySqlDataReader r = commande.ExecuteReader();


            while (r.Read())
            {
                String noIdentification = r["noIdentification"].ToString();

                String nom = r["nom"].ToString();

                String prenom = r["prenom"].ToString();


                String adresse = r["adresse"].ToString();

                String dateNaissance = r["dateNaissance"].ToString();

                String s_age = r["age"].ToString();
                int age = Convert.ToInt16(s_age);

                Adherents adherents = new Adherents(noIdentification,nom,prenom,adresse,dateNaissance,age);



                listeAdherents.Add(adherents);
            }

            r.Close();
            con.Close();
        }

        /*Modifier un adhérent*/

        public void modifierAdherents(String _noIdentification, String _nom, String _prenom, String _adresse, String _dateNaissance)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("p_modifierAdherent");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("noIdentification", _noIdentification);
                commande.Parameters.AddWithValue("nom", _nom);
                commande.Parameters.AddWithValue("prenom", _prenom);
                commande.Parameters.AddWithValue("adresse", _adresse);
                commande.Parameters.AddWithValue("dateNaissance", _dateNaissance);

                con.Open();
                commande.Prepare();
                int i = commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }


        /*ajouter un adhérent*/

        public void addAdherents( String _nom, String _prenom, String _adresse, String _dateNaissance)
        {

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "insert into adherents values( @nom, @prenom , @adresse ,@dateNaissance ) ";
                commande.Parameters.AddWithValue("@nom", _nom);
                commande.Parameters.AddWithValue("@prenom", _prenom);
                commande.Parameters.AddWithValue("@adresse", _adresse);
                commande.Parameters.AddWithValue("@dateNaissance", _dateNaissance);
                


                con.Open();
                commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                //Le if vérifie si la connection est bien ouverte avant d'essayer de la fermer
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        /*Supprimer un adhérent*/

        public void supprimerAdherents(String nom)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = $"delete from adherents where nom = '{nom}'";

                con.Open();
                int i = commande.ExecuteNonQuery();

                con.Close();

                if (i == 1)
                {
                    getActivites();
                }
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }


        /*----------------------------------------------------------------Partie Seances------------------------------------------------------------*/


        /*Ajouter une séances*/

        public void addSeances(int _idSceances, String _dateOrganisation, int _nbPlaceDispo, int _idActivite)
        {

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "insert into seances values( @idSeances ,@dateOrganisation, @nbPlaceDispo , @idActivite ) ";
                commande.Parameters.AddWithValue("@idSeances", _idSceances);
                commande.Parameters.AddWithValue("@dateOrganisation", _dateOrganisation);
                commande.Parameters.AddWithValue("@nbPlaceDispo", _nbPlaceDispo);
                commande.Parameters.AddWithValue("@idActivite", _idActivite);
                



                con.Open();
                commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                //Le if vérifie si la connection est bien ouverte avant d'essayer de la fermer
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        /*Modifier une séances*/
        public void modifierSeances(int _idSceances, String _dateOrganisation, int _nbPlaceDispo, int _idActivite)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("p_modifierSeance");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("idSeances", _idSceances);
                commande.Parameters.AddWithValue("dateOrganisation", _dateOrganisation);
                commande.Parameters.AddWithValue("nbPlaceDispo", _nbPlaceDispo);
                commande.Parameters.AddWithValue("idActivite", _idActivite);
               

                con.Open();
                commande.Prepare();
                int i = commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }



        /*Supprimer une séances*/

        public void supprimerSeance(String nom)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = $"delete from seances where nom = '{nom}'";

                con.Open();
                int i = commande.ExecuteNonQuery();

                con.Close();

                if (i == 1)
                {
                    getActivites();
                }
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }






        //----------------------------------------------------------------Parti Categories------------------------------------------------------------------/


        public void getCategories()
        {
            liste.Clear();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "Select * from categories";
            con.Open();

            //reader est utiliser pour les select //---// Scalar pour les fonction comme count et nonQuery pour les modify , update create , 
            MySqlDataReader r = commande.ExecuteReader();


            while (r.Read())
            {
                String s_idActivite = r["idCategorie"].ToString();
                int idCategorie = Convert.ToInt16(s_idActivite);

                String nom = r["nom"].ToString();

                Categories categorie = new Categories(idCategorie, nom);



                listeCategories.Add(categorie);
            }

            r.Close();
            con.Close();
        }

        public ObservableCollection<Categories> getCategoriesListe()
        {
            getCategories();

            return listeCategories;
        }


        //----------------------------------------------------------------Parti Statistique------------------------------------------------------------------/



    }
}
