using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetSession.Classes
{
    internal class Administrateurs : INotifyPropertyChanged
    {
        int idAdmin = 0;
        string nomUtilisateur = "";
        string motDePasse = "";

        public Administrateurs(int idAdmin, string nomUtilisateur, string motDePasse)
        {
            this.idAdmin = idAdmin;
            this.nomUtilisateur = nomUtilisateur;
            this.motDePasse = motDePasse;
        }

        public int IdAdmin
        {
            get
            {
                return idAdmin;
            }

            set
            {
                idAdmin = value;


            }
        }

        public string NomUtilisateur
        {
            get
            {
                return nomUtilisateur;
            }

            set
            {
                nomUtilisateur = value;


            }
        }

        public string MotDePasse
        {
            get
            {
                return motDePasse;
            }

            set
            {
                motDePasse = value;


            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return idAdmin + " " + nomUtilisateur + " " + motDePasse;
        }
    }
}
