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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace projetSession
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class pageInscription : Page
    {

        
        String nom = "";
        String prenom = "";
        String adresse = "";
        String dateDeNaissance = "";
        Boolean validation = true;
        


        public pageInscription()
        {
            this.InitializeComponent();

            calendrier.MaxDate = new DateTimeOffset(new DateTime(2006, 12, 31));
            calendrier.SelectedDates.Add(new DateTimeOffset(new DateTime(2014, 12, 31)));

           
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //----------------------------------------------------------------------//

            if (string.IsNullOrWhiteSpace(tbx_nom.Text))
            {
                nomError.Text = "Vous devez Entrer un nom .";
                validation = false;
            }
            else
            {
                nomError.Text = "";
                nom = tbx_nom.Text;
            }

            //------------------------------------------------------------------------//

            if (string.IsNullOrWhiteSpace(tbx_prenom.Text))
            {
                prenomErrror.Text = "Vous devez Entrer un prénom.";
                validation = false;
            }
            else
            {
                prenomErrror.Text = "";
                prenom = tbx_prenom.Text;
            }

            //------------------------------------------------------------------------//

  

            if (string.IsNullOrWhiteSpace(tbx_adresse.Text))
            {
                adresseErrror.Text = "Vous devez Entrer une adresse.";
                validation = false;
            }
            else
            {
                adresseErrror.Text = "";
                adresse = tbx_adresse.Text;
            }

            //------------------------------------------------------------------------//


            var select = calendrier.SelectedDates.Count;

            if (select != 1)
            {
                validation = false;
                errorDate.Text = "Veillez seletionner une date.";
            }
            else
            {
                validation = true;
                errorDate.Text = "";
            }


            if (validation)
            {
                
                dateDeNaissance = calendrier.SelectedDates[0].Date.ToString("d");
                //j'appele ma fonction pour mon matricule 
                
                singletonBD.getInstance().addAdherents( nom , prenom , adresse, dateDeNaissance);
                Frame.Navigate(typeof(PageAffichageAdherents));
            }
        }

        private async void buttonAjouterListe_Click(object sender, RoutedEventArgs e)
        {

            String _noIdentification = "";
            String _nom = "";
            String _prenom = "";
            String _adresse = "";
            String _dateDeNaissance = "";


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

                    
                    _nom = v[0];
                    _prenom = v[1];
                    _adresse = v[2];
                    _dateDeNaissance = v[3];
                    singletonBD.getInstance().addAdherents( _nom, _prenom, _adresse, _dateDeNaissance);
                    Frame.Navigate(typeof(PageAffichageAdherents));
                }
            }

        }


    }
}
