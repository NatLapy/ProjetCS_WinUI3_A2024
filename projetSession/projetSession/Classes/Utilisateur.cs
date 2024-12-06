using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetSession.Classes
{
    internal class Utilisateur : INotifyPropertyChanged
    {
        int idAdmin = 0;
        string nomUtilisateur = "";
        string motDePasse = "";
        string nom = "";
        string prenom = "";
        string adresse = "";
        string dateNaissances = "";
        string role;

        public Utilisateur()
        {
            IdAdmin = 0;
            NomUtilisateur = "";
            MotDePasse = "";
            Nom = "";
            Prenom = prenom;
            Adresse = "";
            DateNaissances = "0000-00-00";
            Role = "nonConnecter";
        }
        public Utilisateur(int idAdmin, string nomUtilisateur, string motDePasse)
        {
            this.idAdmin = idAdmin;
            this.nomUtilisateur = nomUtilisateur;
            this.motDePasse = motDePasse;
            role = "Admin";
        }

        public Utilisateur(string noIdentification, string nom, string prenom, string adresse, string dateNaissances)
        {
            nomUtilisateur = noIdentification;
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.dateNaissances = dateNaissances;
            role = "adherent";
        }






        public int IdAdmin { get => idAdmin; set => idAdmin = value; }
        public string NomUtilisateur { get => nomUtilisateur; set => nomUtilisateur = value; }
        public string MotDePasse { get => motDePasse; set => motDePasse = value; }
        public string Nom { get => nom; set => nom = value; }
        public string Prenom { get => prenom; set => prenom = value; }
        public string Adresse { get => adresse; set => adresse = value; }
        public string DateNaissances { get => dateNaissances; set => dateNaissances = value; }
        public string Role { get => role; set => role = value; }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
