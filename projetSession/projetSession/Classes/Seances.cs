using Microsoft.UI.Xaml;
using projetSession.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetSession.Classes
{
    internal class Seances
    {
        int idSceances = 0;
        string dateOrganisation = "";
        int nbPlaceDispo = 0;
        int idActivite = 0;
        int noteAppreciation = 0;

        public Seances(int idSceances, string dateOrganisation, int nbPlaceDispo, int idActivite)
        {
            this.idSceances = idSceances;
            this.dateOrganisation = dateOrganisation;
            this.nbPlaceDispo = nbPlaceDispo;
            this.idActivite = idActivite;
        }

        public Seances(int idSceances, string dateOrganisation, int nbPlaceDispo, int idActivite , int noteAppreciation)
        {
            this.idSceances = idSceances;
            this.dateOrganisation = dateOrganisation;
            this.nbPlaceDispo = nbPlaceDispo;
            this.idActivite = idActivite;
            this.noteAppreciation = noteAppreciation;
        }

        public Visibility VisibilityInscription
        {
            get
            {
                return SingletonUtilisateur.getInstance().User.Role == "Admin" ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility VisibilityAdmin
        {
            get
            {
                return SingletonUtilisateur.getInstance().User.Role == "Admin" ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public Visibility VisibilityAdherent
        {
            get
            {
                return SingletonUtilisateur.getInstance().User.Role == "Adherent" ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        



        public int IdSceances
        {
            get
            {
                return idSceances;
            }

            set
            {
                idSceances = value;


            }
        }

        public string DateOrganisation
        {
            get
            {
                return dateOrganisation;
            }

            set
            {
                dateOrganisation = value;


            }
        }


        public int NbPlaceDispo
        {
            get
            {
                return nbPlaceDispo;
            }

            set
            {
                nbPlaceDispo = value;


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

        public int NoteAppreciation
        {
            get
            {
                return noteAppreciation;
            }

            set
            {
                noteAppreciation = value;


            }
        }

        public override string ToString()
        {
            return idSceances + " " + dateOrganisation + " " + nbPlaceDispo;
        }
    }
}
