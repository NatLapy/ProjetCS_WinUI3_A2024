using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetSession
{
    internal class SingletonHelper
    {


        static SingletonHelper instance = null;

        public SingletonHelper()
        {

        }

        public static SingletonHelper getInstance()
        {
            if (instance == null)
                instance = new SingletonHelper();

            return instance;
        }

        public MainWindow Window { get; set; }
    }
}
