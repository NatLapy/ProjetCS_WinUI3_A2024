﻿using Microsoft.UI.Xaml;
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
        static singletonBD instance = null;


        public singletonBD()
        {
            liste = new ObservableCollection<Activites>();
            listeAdherents = new ObservableCollection<Adherents>();
            //Role = "nonConnecter";
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

                String s_idCategorie = r["idCategorie"].ToString();
                int idCategorie = Convert.ToInt16(s_idCategorie);

                Activites activite = new Activites(idActivite, nom, coutOrganisation, prixVente, idCategorie);



                liste.Add(activite);
            }

            r.Close();
            con.Close();
        }

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







        
        





        public void modifierAdherents(String noIdentification, String nom, String prenom, String adresse, String dateNaissance)
        {
            try
            {
                MySqlCommand commande = new MySqlCommand("p_modifierAdherent");
                commande.Connection = con;
                commande.CommandType = System.Data.CommandType.StoredProcedure;

                commande.Parameters.AddWithValue("noIdentification", noIdentification);
                commande.Parameters.AddWithValue("nom", nom);
                commande.Parameters.AddWithValue("prenom", prenom);
                commande.Parameters.AddWithValue("adresse", adresse);
                commande.Parameters.AddWithValue("dateNaissance", dateNaissance);

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


        public void modifierActivites(int _idActivite, String _nom, int _coutOrganisation, int _prixDeVente, int _idAdmin ,int  _idCategorie)
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
    }
}
