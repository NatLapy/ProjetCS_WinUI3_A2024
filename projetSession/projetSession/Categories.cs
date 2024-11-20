using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetSession
{
    internal class Categories
    {
        int idCategorie = 0;
        String nom = "";

        public Categories(int idCategorie, string nom)
        {
            this.idCategorie = idCategorie;
            this.nom = nom;
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
    }
}
