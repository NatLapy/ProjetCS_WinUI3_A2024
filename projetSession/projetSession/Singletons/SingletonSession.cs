using MySql.Data.MySqlClient;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetSession.Singletons
{
    internal class SingletonSession
    {
        MySqlConnection con;
        static SingletonSession instance = null;

        public SingletonSession()
        {
            con = new MySqlConnection("Server=cours.cegep3r.info;Database=a2024_420-345-ri_eq2;Uid=6269818;Pwd=6269818;");

        }

        public static SingletonSession getInstance()
        {
            if (instance == null)
            {
                instance = new SingletonSession();
                instance.Role = "nonConnecter";
            }


            return instance;
        }

        // SECTION VERIFICATION ROLE
        public string Role { get; set; }


    }
}
