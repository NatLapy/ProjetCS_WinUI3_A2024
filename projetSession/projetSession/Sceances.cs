using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetSession
{
    internal class Sceances
    {
        int idSceances = 0;
        String dateOrganisation = "";
        String heureOrganisation = "";
        int nbPlaceDispo = 0;

        public Sceances(int idSceances, string dateOrganisation, string heureOrganisation, int nbPlaceDispo)
        {
            this.idSceances = idSceances;
            this.dateOrganisation = dateOrganisation;
            this.heureOrganisation = heureOrganisation;
            this.nbPlaceDispo = nbPlaceDispo;
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

        public String HeureOrganisation
        {
            get
            {
                return heureOrganisation;
            }

            set
            {
                heureOrganisation = value;


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

        public override string ToString()
        {
            return idSceances + " " + dateOrganisation + " " + heureOrganisation + " " + nbPlaceDispo ;
        }
    }
}
