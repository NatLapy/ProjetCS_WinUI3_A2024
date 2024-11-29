using Microsoft.UI.Input;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace projetSession
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        

        public MainWindow()
        {
            ApplicationData.Current.LocalSettings.Values["themeSetting"] = 2;
            //App.Current.RequestedTheme = ApplicationTheme.Light;




            this.InitializeComponent();
            ServiceNavigation.getInstance().NavigationView = navView;
            SingletonUtilisateur.getInstance();
            SingletonHelper.getInstance().Window = this;
            mainFrame.Navigate(typeof(PageAccueil));
            

            
        }

        private void Themes_Light(object sender, SelectionChangedEventArgs e)
        {
            string stTheme = "Dark";
            if (Enum.TryParse(stTheme, out ElementTheme theme) is true)
            {


            }
        }

        public Visibility VisibilityAdmin
        {
            get
            {
                return SingletonUtilisateur.getInstance().User.Role == "Admin" ? Visibility.Visible : Visibility.Collapsed;
            }
        }



        private void navView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            var item = (NavigationViewItem)args.InvokedItemContainer;
            switch (item.Name)
            {
                case "iAccueil":
                    mainFrame.Navigate(typeof(PageAccueil));
                    break;
                case "iInscription":
                    mainFrame.Navigate(typeof(pageInscription));
                    break;
                case "iActivite":
                    mainFrame.Navigate(typeof(AjouterActivite));
                    break;
                case "iPageStatistique":
                    mainFrame.Navigate(typeof(PageStatistique));
                    break;

                case "iConnecter":
                    mainFrame.Navigate(typeof(PageConnection));
                    break;
                case "iDeconnection":
                    bool deconnection = true;
                    mainFrame.Navigate(typeof(PageAccueil),deconnection);
                    break;


                default:
                    mainFrame.Navigate(typeof(PageAccueil));
                    break;
            }
        }



        
    }
}
