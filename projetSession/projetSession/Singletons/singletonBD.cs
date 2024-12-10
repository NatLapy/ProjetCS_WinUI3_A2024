using Microsoft.UI.Xaml.Controls.Primitives;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Tls.Crypto;
using projetSession.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using Windows.System;

namespace projetSession.Singletons
{
    internal class singletonBD
    {

        MySqlConnection con;
        ObservableCollection<Activites> liste;
        ObservableCollection<Adherents> listeAdherents;
        ObservableCollection<Categories> listeCategories;
        ObservableCollection<Seances> listeSeances;
        static singletonBD instance = null;


        public singletonBD()
        {
            liste = new ObservableCollection<Activites>();
            listeAdherents = new ObservableCollection<Adherents>();
            listeCategories = new ObservableCollection<Categories>();
            listeSeances = new ObservableCollection<Seances>();
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

        public ObservableCollection<Adherents> Adherents { get { return listeAdherents; } }



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
                string s_idActivite = r["idActivite"].ToString();
                int idActivite = Convert.ToInt16(s_idActivite);

                string nom = r["nom"].ToString();

                string s_coutOrganisation = r["coutOrganisation"].ToString();
                int coutOrganisation = Convert.ToInt16(s_coutOrganisation);

                string s_prixVente = r["prixDeVente"].ToString();
                int prixVente = Convert.ToInt16(s_prixVente);




                string s_idCategorie = r["idCategorie"].ToString();
                int idCategorie = Convert.ToInt16(s_idCategorie);

                string s_urlImage = r["urlImage"].ToString();





                Activites activite = new Activites(idActivite, nom, coutOrganisation, prixVente, idCategorie, s_urlImage);



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

                string s_idActivite = r["idActivite"].ToString();
                int idActivite = Convert.ToInt16(s_idActivite);

                string nom = r["nom"].ToString();

                string s_coutOrganisation = r["coutOrganisation"].ToString();
                int coutOrganisation = Convert.ToInt16(s_coutOrganisation);

                string s_prixVente = r["prixDeVente"].ToString();
                int prixVente = Convert.ToInt16(s_prixVente);


                string s_idCategorie = r["idCategorie"].ToString();
                int idCategorie = Convert.ToInt16(s_idCategorie);

                string s_urlImage = r["urlImage"].ToString();

                Activites activite = new Activites(idActivite, nom, coutOrganisation, prixVente, idCategorie, s_urlImage);



                liste.Add(activite);
            }

            r.Close();
            con.Close();
        }

        /*supprimer un activité*/

        public void supprimerActivites(string nom)
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

        public void modifierActivites(int _idActivite, string _nom, int _coutOrganisation, int _prixDeVente, int _idAdmin, int _idCategorie)
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

        public void addActivites(string _nom, double _coutOrganisation, double _prixDeVente, int _idCategorie, string _urlImage)
        {

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;


                commande.CommandText = "insert into activites(nom, coutOrganisation, prixDeVente, idCategorie,urlImage) values( @nom, @coutOrganisation , @prixDeVente , @idCategorie, @urlImage ) ";

                commande.Parameters.AddWithValue("@nom", _nom);
                commande.Parameters.AddWithValue("@coutOrganisation", _coutOrganisation);
                commande.Parameters.AddWithValue("@prixDeVente", _prixDeVente);
                commande.Parameters.AddWithValue("@idCategorie", _idCategorie);

                if (!string.IsNullOrWhiteSpace(_urlImage))
                {
                    commande.Parameters.AddWithValue("@urlImage", _urlImage);
                }
                else
                {
                    commande.Parameters.AddWithValue("@idCategorie", "Assets\\Square44x44Logo.scale-200.png");
                }

                con.Open();
                commande.ExecuteNonQuery();

                con.Close();
            }
            catch (MySqlException ex)
            {
                //Le if vérifie si la connection est bien ouverte avant d'essayer de la fermer
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        public int[] getCountAdherentsParCategorie()
        {
            List<int> listeCountActivite = new List<int>();


            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "SELECT COUNT(noIdentificationAdherent) FROM inscription INNER JOIN seances s on inscription.idSeance = s.idSeances GROUP BY idActivite ORDER BY s.idActivite;;";
            con.Open();

            //reader est utiliser pour les select //---// Scalar pour les fonction comme count et nonQuery pour les modify , update , 

            MySqlDataReader reader = commande.ExecuteReader();


            while (reader.Read())
            {

                listeCountActivite.Add(reader.GetInt32(0));
            }



            //place où utiliser le code après la query

            reader.Close();
            con.Close();

            return listeCountActivite.ToArray();
        }

        public string[] getActiviteNom()
        {
            List<string> listeNom = new List<string>();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            //commande.CommandText = "SELECT nom FROM inscription INNER JOIN seances s on inscription.idSeance = s.idSeances INNER JOIN activites a on s.idActivite = a.idActivite GROUP BY s.idActivite ORDER BY s.idActivite;";
            commande.CommandText = "SELECT nom FROM activites;";
            con.Open();

            MySqlDataReader reader = commande.ExecuteReader();


            while (reader.Read())
            {
                listeNom.Add(reader[0].ToString());
            }

            reader.Close();
            con.Close();

            return listeNom.ToArray();
        }


        //----------------------------------------------------------------Parti Adherents------------------------------------------------------------------/


        public ObservableCollection<Adherents> getListeAdherent()
        {
            getAdherents();

            return listeAdherents;
        }

        /* Get TOUT les adherents */
        public void getAdherents()
        {
            listeAdherents.Clear();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "SELECT * FROM adherents";
            con.Open();

            //reader est utiliser pour les select //---// Scalar pour les fonction comme count et nonQuery pour les modify , update create , 
            MySqlDataReader r = commande.ExecuteReader();


            while (r.Read())
            {


                string nom = r["nom"].ToString();

                string prenom = r["prenom"].ToString();


                string adresse = r["adresse"].ToString();

                string dateNaissance = r["dateNaissance"].ToString();



                Adherents adherents = new Adherents(nom, prenom, adresse, dateNaissance);



                listeAdherents.Add(adherents);
            }

            r.Close();
            con.Close();
        }

        /* connexion d'un adherent avec le numéro d'identification */
        public void connexionAdherent(string idUser)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"SELECT * FROM adherents WHERE noIdentification = '{idUser}'";
            con.Open();

            //reader est utiliser pour les select //---// Scalar pour les fonction comme count et nonQuery pour les modify , update create , 
            MySqlDataReader r = commande.ExecuteReader();

            while (r.Read())
            {
                string noIdentification = r["noIdentification"].ToString();

                string nom = r["nom"].ToString();

                string prenom = r["prenom"].ToString();


                string adresse = r["adresse"].ToString();

                string dateNaissance = r["dateNaissance"].ToString();

                SingletonUtilisateur.getInstance().User = new Utilisateur(noIdentification, nom, prenom, adresse, dateNaissance);
            }




            r.Close();
            con.Close();



        }

        /*Modifier un adhérent*/

        public void modifierAdherents(string _noIdentification, string _nom, string _prenom, string _adresse, string _dateNaissance)
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

        public void addAdherents(string _nom, string _prenom, string _adresse, string _dateNaissance)
        {

            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = "insert into adherents(nom, prenom, adresse, dateNaissance) values( @nom, @prenom , @adresse, @dateNaissance ) ";
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

        public void supprimerAdherents(string nom)
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

        /*Trouver si un adhérent existe avec le numéro d'identification*/
        public bool estAdherent(string idUser)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"SELECT * FROM adherents WHERE noIdentification = '{idUser}'";
            con.Open();

            //reader est utiliser pour les select //---// Scalar pour les fonction comme count et nonQuery pour les modify , update create , 
            MySqlDataReader r = commande.ExecuteReader();

            if (r.HasRows)
            {
                r.Close();
                con.Close();
                return true;
            }
            else
            {
                r.Close();
                con.Close();
                return false;
            }

            r.Close();
            con.Close();

            return false;
        }

        /*----------------------------------------------------------------Partie Admin------------------------------------------------------------*/

        public bool estAdmin(string name, string password)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"SELECT* FROM administrateurs WHERE nomUtilisateur = '{name}' AND motDePasse = '{password}';";
            con.Open();

            //reader est utiliser pour les select //---// Scalar pour les fonction comme count et nonQuery pour les modify , update create , 
            MySqlDataReader r = commande.ExecuteReader();

            if (r.HasRows)
            {
                r.Close();
                con.Close();
                return true;
            }
            else
            {
                r.Close();
                con.Close();
                return false;
            }

            r.Close();
            con.Close();

            return false;
        }


        public void connexionAdmin(string name, string password)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"SELECT* FROM administrateurs WHERE nomUtilisateur = '{name}' AND motDePasse = '{password}';";
            con.Open();

            //reader est utiliser pour les select //---// Scalar pour les fonction comme count et nonQuery pour les modify , update create , 
            MySqlDataReader r = commande.ExecuteReader();

            while (r.Read())
            {

                int id = Convert.ToInt16(r["idAdmin"].ToString());

                string nom = r["nomUtilisateur"].ToString();


                string mdp = r["motDePasse"].ToString();

                SingletonUtilisateur.getInstance().User = new Utilisateur(id,nom,mdp);
            }

            r.Close();
            con.Close();
        }



        /*----------------------------------------------------------------Partie Seances------------------------------------------------------------*/


        public ObservableCollection<Seances> getListeSeance(int v)
        {
            getSeance(v);

            return listeSeances;
        }

        /*Afficher les Séances*/

        public void getSeance(int v)
        {
            listeSeances.Clear();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"SELECT * FROM Seances WHERE idActivite = {v}";
            con.Open();

            //reader est utiliser pour les select //---// Scalar pour les fonction comme count et nonQuery pour les modify , update create , 
            MySqlDataReader r = commande.ExecuteReader();


            while (r.Read())
            {


                int idSeances = Convert.ToInt16(r["idSeances"].ToString());

                string dateOrganisation = r["dateOrganisation"].ToString();


                int nbPlaceDispo = Convert.ToInt16(r["nbPlaceDispo"].ToString());

                int idActivite = Convert.ToInt16(r["nbPlaceDispo"].ToString());



                Seances seance = new Seances(idSeances, dateOrganisation, nbPlaceDispo, idActivite);



                listeSeances.Add(seance);
            }

            r.Close();
            con.Close();
        }

        /*Ajouter une séances*/

        public void addSeances(int _idSceances, string _dateOrganisation, int _nbPlaceDispo, int _idActivite)
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
        public void modifierSeances(int _idSceances, string _dateOrganisation, int _nbPlaceDispo, int _idActivite)
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

        public void supprimerSeance(int _id)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = $"delete from seances where idSeances = '{_id}'";

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


        public void ajouterAdherentsSeance( string  _noIdentificationAdherent, int _idSeances)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("p_ajouterAdherentsSeance");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("noIdentificationAdherent", _noIdentificationAdherent);
                commande.Parameters.AddWithValue("idSeances", _idSeances);
              


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
                string s_idActivite = r["idCategorie"].ToString();
                int idCategorie = Convert.ToInt16(s_idActivite);

                string nom = r["nom"].ToString();

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
