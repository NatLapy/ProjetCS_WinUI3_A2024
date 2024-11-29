using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetSession
{
    internal class Seances
    {
        int idSceances = 0;
        String dateOrganisation = "";
        int nbPlaceDispo = 0;
        int idActivite = 0;

        public Seances(int idSceances, string dateOrganisation , int nbPlaceDispo , int idActivite)
        {
            this.idSceances = idSceances;
            this.dateOrganisation = dateOrganisation;
            this.nbPlaceDispo = nbPlaceDispo;
            this.idActivite = idActivite;
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

        public String DateOrganisation
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

        public override string ToString()
        {
            return idSceances + " " + dateOrganisation + " " + nbPlaceDispo;
        }
    }
}
