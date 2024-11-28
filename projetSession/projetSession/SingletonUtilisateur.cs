using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetSession
{
    internal class SingletonUtilisateur
    {
        MySqlConnection con;
        static SingletonUtilisateur instance;

        public SingletonUtilisateur()
        {
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2024_420335ri_eq3;Uid=6269818;Pwd=6269818;");
            //Utilisateur user = new Utilisateur();
        }

        public static SingletonUtilisateur getInstance()
        {
            if (instance == null)
            {
                instance = new SingletonUtilisateur();
                instance.Role = "nonConnecter";
            }
                

            return instance;
        }

        // SECTION VERIFICATION ROLE


        public Utilisateur User{
            get;

            set;
        }
        public string Role { get; set; }
    }
}
