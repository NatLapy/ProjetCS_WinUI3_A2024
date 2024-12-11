using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml.Media.Imaging;
using MySql.Data.MySqlClient;
using projetSession.Classes;
using projetSession.Singletons;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace projetSession.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AjouterActivite : Page
    {
        Boolean validation = true;
        ObservableCollection<Categories> listeCategories = singletonBD.getInstance().getCategoriesListe();

        public AjouterActivite()
        {
            this.InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

            validation = true;
            String nom = "";

            double coutOrganisation = 0;

            double prixVente = 0;

          
            int idCategorie = 0;


            string urlImage = "";



            //----------------------------------------------------------------------//

            if (string.IsNullOrWhiteSpace(tbx_nom.Text))
            {
                nomError.Text = "Vous devez entrer un nom.";
                validation = false;
            }
            else
            {
                nomError.Text = "";
                nom = tbx_nom.Text;
            }

            //------------------------------------------------------------------------//

            if (string.IsNullOrWhiteSpace(tbx_Cout_organisation.Text))
            {
                Cout_organisationErrror.Text = "Vous devez Entrer un cout d'organisation.";
                validation = false;
            }
            else
            {
                coutOrganisation = tbx_Cout_organisation.Value;
            }


            //------------------------------------------------------------------------//


            if (string.IsNullOrWhiteSpace(tbx_prixDeVente.Text))
            {
                prixDeVenteErrror.Text = "Vous devez Entrer un prix de vente.";
                validation = false;
            }
            else
            {
                prixVente = tbx_prixDeVente.Value;
            }
            
            //------------------------------------------------------------------------//

            if(prixVente < coutOrganisation ){
                prixDeVenteErrror.Text = "Le prix de vente doit être supérieur au cout d'organisation.";
                Cout_organisationErrror.Text = "Le cout d'organisation doit être inférieur au prix de vente.";
                validation = false;
            }


            //------------------------------------------------------------------------//

            if (validation)
            {

              

               

             
                idCategorie = (int)cbx_idCategorie.SelectedValue;

                if (string.IsNullOrEmpty(tbx_urlImage.Text))
                {
                    urlImage = "Assets\\Square44x44Logo.scale-200.png";
                }
                else
                {
                    urlImage = tbx_urlImage.Text;
                }
                

                    singletonBD.getInstance().addActivites( nom , coutOrganisation, prixVente,  idCategorie, urlImage);
                
                
                Frame.Navigate(typeof(PageAccueil));
            }
        }

        private async void buttonAjouterListe_Click(object sender, RoutedEventArgs e)
        {
            int _idActivite = 0;
            String _nom = "";
            double _coutOrganisation = 0;
            double _prixVente = 0;
            int _idAmin = 0;
            int _idCategorie = 0;
            string _urlImage = "";


            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.FileTypeFilter.Add(".csv");

            var hWnd = WinRT.Interop.WindowNative.GetWindowHandle(SingletonHelper.getInstance().Window);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hWnd);



            //sélectionne le fichier à lire
            Windows.Storage.StorageFile monFichier = await picker.PickSingleFileAsync();

            //ouvre le fichier et lit le contenu

            if (monFichier != null)
            {
                var lignes = await Windows.Storage.FileIO.ReadLinesAsync(monFichier);

                foreach (var ligne in lignes)
                {
                    var v = ligne.Split(";");

                    _idActivite = Convert.ToInt16(v[0]);
                    _nom = v[1];
                    _coutOrganisation = Convert.ToInt16(v[2]) ;
                    _prixVente = Convert.ToInt16(v[3]) ;
                    _idAmin = Convert.ToInt16(v[4]) ;
                    _idCategorie = Convert.ToInt16(v[5]) ;
                    _urlImage = v[6];

                    singletonBD.getInstance().addActivites( _nom, _coutOrganisation, _prixVente , _idCategorie, _urlImage);
                    // Joueur joueur = new Joueur(_matricule , _nom , _prenom , _dateNaissance , _nomEquipe);
                }
            }
        }

        private void tbx_urlImage_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(tbx_urlImage.Text) && new Uri(tbx_urlImage.Text, UriKind.RelativeOrAbsolute) is not null && Uri.IsWellFormedUriString(tbx_urlImage.Text,UriKind.Relative))
            {
                try
                {
                    imageActivite.Source = new BitmapImage(new Uri(tbx_urlImage.Text));
                }
                catch (Exception ex)
                {

                }
                
            }
            
        }
    }
}
