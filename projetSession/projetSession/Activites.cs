using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetSession
{
    internal class Activites : INotifyPropertyChanged
    {
        int idActivite = 0;
        String nom = "";
        int coutOrganisation = 0;
        int prixVente = 0;

        public Activites(int idActivite, string nom, int coutOrganisation, int prixVente)
        {
            this.idActivite = idActivite;
            this.nom = nom;
            this.coutOrganisation = coutOrganisation;
            this.prixVente = prixVente;
        }


        public int IdActivite
        {
            get
            {
                return idActivite;
            }

            set
            {
                idActivite = value;


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

        public int CoutOrganisation
        {
            get
            {
                return coutOrganisation;
            }

            set
            {
                coutOrganisation = value;


            }
        }


        public int PrixVente
        {
            get
            {
                return prixVente;
            }

            set
            {
                prixVente = value;


            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return idActivite + " " + nom + " " + coutOrganisation + " " + prixVente;
        }
    }
}
