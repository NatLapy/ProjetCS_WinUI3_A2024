﻿using Microsoft.UI.Xaml;
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
        double coutOrganisation = 0;
        double prixVente = 0;
        
        int idCategorie = 0;

        public Activites(int idActivite, string nom, double coutOrganisation, double prixVente  , int idCategorie)
        {
            this.idActivite = idActivite;
            this.nom = nom;
            this.coutOrganisation = coutOrganisation;
            this.prixVente = prixVente;
            
            this.idCategorie = idCategorie;
        }



        public Visibility VisibilityAdmin
        {
            get
            {
                return SingletonUtilisateur.getInstance().User.Role == "Admin" ? Visibility.Visible : Visibility.Collapsed;
            }
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

        public double CoutOrganisation
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


        public double PrixVente
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

        public string s_PrixVente
        {
            get
            {
                return prixVente.ToString("0.00$");
            }
        }

       


        public int IdCategorie
        {
            get
            {
                return idCategorie;
            }

            set
            {
                idCategorie = value;


            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public override string ToString()
        {
            return idActivite + " " + nom + " " + coutOrganisation + " " + prixVente;
        }
    }
}
