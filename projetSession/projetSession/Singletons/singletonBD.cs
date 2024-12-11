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
        ObservableCollection<Seances> listeSeancesAdherent;
        static singletonBD instance = null;


        public singletonBD()
        {
            liste = new ObservableCollection<Activites>();
            listeAdherents = new ObservableCollection<Adherents>();
            listeCategories = new ObservableCollection<Categories>();
            listeSeances = new ObservableCollection<Seances>();
            listeSeancesAdherent = new ObservableCollection<Seances>();
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

        public int getCountAdherentsActivite(int id)
        {
            int counter = 0;
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"SELECT COUNT(*) FROM inscription INNER JOIN seances s on inscription.idSeance = s.idSeances WHERE idActivite = {id};";
            con.Open();

            //reader est utiliser pour les select //---// Scalar pour les fonction comme count et nonQuery pour les modify , update , 

            MySqlDataReader reader = commande.ExecuteReader();


            while (reader.Read())
            {
                counter = reader.GetInt32(0);
            }



            //place où utiliser le code après la query

            reader.Close();
            con.Close();
            return counter;

        }

        public string[] getActiviteNom()
        {
            List<string> listeNom = new List<string>();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            //commande.CommandText = "SELECT nom FROM inscription INNER JOIN seances s on inscription.idSeance = s.idSeances INNER JOIN activites a on s.idActivite = a.idActivite GROUP BY s.idActivite ORDER BY s.idActivite;";
            commande.CommandText = "SELECT nom FROM activites order by idActivite;";
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

        public int[] getActiviteId()
        {
            List<int> listeId = new List<int>();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            //commande.CommandText = "SELECT nom FROM inscription INNER JOIN seances s on inscription.idSeance = s.idSeances INNER JOIN activites a on s.idActivite = a.idActivite GROUP BY s.idActivite ORDER BY s.idActivite;";
            commande.CommandText = "SELECT idActivite FROM activites order by idActivite;";
            con.Open();

            MySqlDataReader reader = commande.ExecuteReader();


            while (reader.Read())
            {
                listeId.Add(reader.GetInt32(0));
            }

            reader.Close();
            con.Close();

            return listeId.ToArray();
        }

        public double getMoyenneParActivite(int id)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("moyenneActivite");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("id",id );

                con.Open();
                commande.Prepare();
                double i;

                if (commande.ExecuteScalar() is not null)
                {
                    i = (double)commande.ExecuteScalar();
                }
                else
                {
                    i = 0;
                }
                

                con.Close();
                return i;
            }
            catch (Exception ex)
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }

                con.Close();
                return -1;
            }
        }

        public double getNbPlaceParActivite(int id)
        {
            double counter= 0;
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"SELECT SUM(nbPlaceDispo) FROM seances WHERE idActivite={id} GROUP BY idActivite;";
            con.Open();

            //reader est utiliser pour les select //---// Scalar pour les fonction comme count et nonQuery pour les modify , update , 
            
            if(commande.ExecuteScalar() is not null)
            {
                counter = Convert.ToDouble((decimal)commande.ExecuteScalar());
            }

            con.Close();
            return counter;
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

                string idAdherent = r["noIdentification"].ToString();


                string nom = r["nom"].ToString();

                string prenom = r["prenom"].ToString();


                string adresse = r["adresse"].ToString();

                string dateNaissance = r["dateNaissance"].ToString();



                Adherents adherents = new Adherents(idAdherent,nom, prenom, adresse, dateNaissance);



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

        }

        public void rechercheAdherent(string v)
        {
            listeAdherents.Clear();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"Select * from adherents where nom like  '%{v}%' OR prenom like '%{v}%'";
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

                Adherents adherent = new Adherents(noIdentification, nom, prenom, adresse, dateNaissance);



                listeAdherents.Add(adherent);
            }

            r.Close();
            con.Close();
        }

        public bool estInscris(string idAd, int idSe)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"SELECT* FROM inscription where idSeance = {idSe} and noIdentificationAdherent = '{idAd}';";
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

        public ObservableCollection<Seances> getListeVraiSeance()
        {
            getVraiSeance();

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

        public void getVraiSeance()
        {
            listeSeances.Clear();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"SELECT * FROM Seances ";
            con.Open();

            //reader est utiliser pour les select //---// Scalar pour les fonction comme count et nonQuery pour les modify , update create , 
            MySqlDataReader r = commande.ExecuteReader();


            while (r.Read())
            {


                int idSeances = Convert.ToInt16(r["idSeances"].ToString());

                string dateOrganisation = r["dateOrganisation"].ToString();


                int nbPlaceDispo = Convert.ToInt16(r["nbPlaceDispo"].ToString());

                int idActivite = Convert.ToInt16(r["idActivite"].ToString());

                int noteAppreciation = Convert.ToInt16(r["noteAppreciation"].ToString());

                Seances seance = new Seances(idSeances, dateOrganisation, nbPlaceDispo, idActivite , noteAppreciation);



                listeSeances.Add(seance);
            }

            r.Close();
            con.Close();
        }


        public ObservableCollection<Seances> getListeSeanceAdherent(int v, string ad)
        {
            getSeanceAdherent( v, ad);

            return listeSeancesAdherent;
        }

        public void getSeanceAdherent(int v, string ad)
        {
            listeSeancesAdherent.Clear();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"SELECT* FROM seances INNER JOIN inscription i on seances.idSeances = i.idSeance WHERE idActivite= {v} AND noIdentificationAdherent = '{ad}';";
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



                listeSeancesAdherent.Add(seance);
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
                MySqlCommand commande = new MySqlCommand("p_ajouterInscription");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("_noidentificationAdherents", _noIdentificationAdherent);
                commande.Parameters.AddWithValue("_idSeances", _idSeances);
              


                con.Open();

                commande.Prepare();
                int i = commande.ExecuteNonQuery();

                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
            }
        }



        //----------------------------------------------------------------Parti Categories------------------------------------------------------------------/


        public void getCategories()
        {
            listeCategories.Clear();

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

        public int getNbActiviteParCategorie(int id)
        {
            int counter = 0;
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            //commande.CommandText = "SELECT nom FROM inscription INNER JOIN seances s on inscription.idSeance = s.idSeances INNER JOIN activites a on s.idActivite = a.idActivite GROUP BY s.idActivite ORDER BY s.idActivite;";
            commande.CommandText = $"SELECT COUNT(*) AS nbActivite FROM activites INNER JOIN categories c on activites.idCategorie = c.idCategorie WHERE c.idCategorie = {id} GROUP BY c.idCategorie ORDER BY c.idCategorie;";
            con.Open();

            MySqlDataReader reader = commande.ExecuteReader();


            while (reader.Read())
            {
                counter = reader.GetInt32(0);
            }

            reader.Close();
            con.Close();

            return counter;

        }


        public string[] getCategorieNom()
        {
            List<string> listeNom = new List<string>();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            //commande.CommandText = "SELECT nom FROM inscription INNER JOIN seances s on inscription.idSeance = s.idSeances INNER JOIN activites a on s.idActivite = a.idActivite GROUP BY s.idActivite ORDER BY s.idActivite;";
            commande.CommandText = "SELECT nom FROM categories order by idCategorie;";
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

        public int[] getCategorieId()
        {
            List<int> listeId = new List<int>();

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            //commande.CommandText = "SELECT nom FROM inscription INNER JOIN seances s on inscription.idSeance = s.idSeances INNER JOIN activites a on s.idActivite = a.idActivite GROUP BY s.idActivite ORDER BY s.idActivite;";
            commande.CommandText = "SELECT idCategorie FROM categories order by idCategorie;";
            con.Open();

            MySqlDataReader reader = commande.ExecuteReader();


            while (reader.Read())
            {
                listeId.Add(reader.GetInt32(0));
            }

            reader.Close();
            con.Close();

            return listeId.ToArray();
        }


        //----------------------------------------------------------------Parti Statistique------------------------------------------------------------------/



    }
}
