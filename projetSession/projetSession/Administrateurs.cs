﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetSession
{
    internal class Administrateurs : INotifyPropertyChanged
    {
        int idAdmin = 0;
        String nomUtilisateur = "";
        String motDePasse = "";

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

        public String NomUtilisateur
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

        public String MotDePasse
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

        public override string ToString()
        {
            return idAdmin + " " + nomUtilisateur + " " + motDePasse;   
        }
    }
}
