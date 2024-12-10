using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetSession.Classes
{
    internal class Adherents : INotifyPropertyChanged
    {
        string noIdentification;
        string nom;
        string prenom;
        string adresse;
        string dateNaissances;

        public Adherents()
        {
            noIdentification = "";
            nom = "";
            prenom = "";
            adresse = "";
            dateNaissances = "";
        }

        public Adherents(string noIdentification, string nom, string prenom, string adresse, string dateNaissances)
        {
            this.noIdentification = noIdentification;
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.dateNaissances = dateNaissances;
        }

        public Adherents(string nom, string prenom, string adresse, string dateNaissances)
        {

            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.dateNaissances = dateNaissances;

        }


        public string NoIdentification
        {
            get
            {
                return noIdentification;
            }

            set
            {
                noIdentification = value;


            }
        }

        public string Nom
        {
            get
            {
                return nom;
            }

            set
            {
                nom = value;


            }
        }

        public string Prenom
        {
            get
            {
                return prenom;
            }

            set
            {
                prenom = value;


            }
        }


        public string Adresse
        {
            get
            {
                return adresse;
            }

            set
            {
                adresse = value;


            }
        }

        public string DateNaissances
        {
            get
            {
                return dateNaissances;
            }

            set
            {
                dateNaissances = value;


            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return prenom + " " +  nom ;
        }
    }
}
