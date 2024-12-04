using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetSession
{
    internal class Adherents : INotifyPropertyChanged
    {
        String noIdentification = "";
        String nom = "";
        String prenom = "";
        String adresse = "";
        String dateNaissances = "";
        int age = 0;


        public Adherents(string noIdentification, string nom, string prenom, string adresse, string dateNaissances, int age)
        {
            this.noIdentification = noIdentification;
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.dateNaissances = dateNaissances;
            this.age = age;
        }

        public Adherents( string nom, string prenom, string adresse, string dateNaissances)
        {
            
            this.nom = nom;
            this.prenom = prenom;
            this.adresse = adresse;
            this.dateNaissances = dateNaissances;
            
        }


        public String NoIdentification
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

        public String Nom
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

        public String Prenom
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


        public String Adresse
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

        public String DateNaissances
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
            return noIdentification + " " + nom + " " + prenom + " " + adresse + " " + dateNaissances + age;
        }
    }
}
