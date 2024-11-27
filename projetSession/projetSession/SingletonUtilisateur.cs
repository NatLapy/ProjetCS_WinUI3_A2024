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
        static SingletonUtilisateur instance = null;

        public SingletonUtilisateur()
        {
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2024_420335ri_eq3;Uid=6269818;Pwd=6269818;");
            Utilisateur user = new Utilisateur();
            user.Role = "nonConnecter";
        }

        public static SingletonUtilisateur getInstance()
        {
            if (instance == null)
                instance = new SingletonUtilisateur();

            return instance;
        }

        // SECTION VERIFICATION ROLE


        public Utilisateur User{
            get;

            set;
        }
    }
}
