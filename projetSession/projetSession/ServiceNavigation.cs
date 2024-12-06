using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using projetSession.Singletons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace projetSession
{
    internal class ServiceNavigation
    {
        NavigationView navigationView;

        static ServiceNavigation instance;

        public static ServiceNavigation getInstance()
        {
            if (instance == null)
                instance = new ServiceNavigation();

            return instance;
        }

        public NavigationView NavigationView
        {
            get { return navigationView; }
            set { navigationView = value; }
        }

        public Visibility VisibilityAdmin
        {
            get
            {
                return SingletonUtilisateur.getInstance().User.Role == "Admin" ? Visibility.Visible : Visibility.Collapsed;
            }
        }
    }
}
