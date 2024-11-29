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
using Windows.Devices.Enumeration;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace projetSession
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AjouterActivite : Page
    {
        Boolean validation = true;

        public AjouterActivite()
        {
            this.InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            String s_idActivite = "";

            int idActivite = 0;

            String nom = "";

            String s_coutOrganisation = "";
            double coutOrganisation = 0;

            String s_prixVente = "";
            double prixVente = 0;

            String s_idAmin = "";
            int idAmin = 0;

            String s_idCategorie = "";
            int idCategorie = 0;


            //----------------------------------------------------------------------//

            if (string.IsNullOrWhiteSpace(tbx_idActivite.Text))
            {
                idActiviteError.Text = "Vous devez Entrer un id d'activité.";
                validation = false;
            }
            else
            {
                idActiviteError.Text = "";
                s_idActivite = tbx_idActivite.Text;
            }


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

            if (string.IsNullOrWhiteSpace(tbx_Cout_organisation.Text))
            {
                Cout_organisationErrror.Text = "Vous devez Entrer un cout d'organisation.";
                validation = false;
            }
            else
            {
                Cout_organisationErrror.Text = "";
                s_coutOrganisation = tbx_Cout_organisation.Text;
            }

            //------------------------------------------------------------------------//


            if (string.IsNullOrWhiteSpace(tbx_prixDeVente.Text))
            {
                prixDeVenteErrror.Text = "Vous devez Entrer prix de vente.";
                validation = false;
            }
            else
            {
                prixDeVenteErrror.Text = "";
                s_prixVente = tbx_prixDeVente.Text;
            }

            //------------------------------------------------------------------------//


            if (string.IsNullOrWhiteSpace(tbx_idAdmin.Text))
            {
                idAdminErrror.Text = "Vous devez Entrer un id d'Administrateur.";
                validation = false;
            }
            else
            {
                idAdminErrror.Text = "";
                s_idAmin = tbx_idAdmin.Text;
            }

            //------------------------------------------------------------------------//

            if (string.IsNullOrWhiteSpace(tbx_idCategorie.Text))
            {
                idCategorieErrror.Text = "Vous devez Entrer un id de catégorie.";
                validation = false;
            }
            else
            {
                idCategorieErrror.Text = "";
                s_idCategorie = tbx_idCategorie.Text;
            }

            //------------------------------------------------------------------------//

            if (validation)
            {

                idActivite = Convert.ToInt16(s_idActivite);

                

                
                coutOrganisation = Convert.ToDouble(s_coutOrganisation);

               
                prixVente = Convert.ToDouble(s_prixVente);

               
                idAmin = Convert.ToInt16(s_idAmin);

                  
                idCategorie = Convert.ToInt16(s_idCategorie);

                singletonBD.getInstance().addActivites(idActivite , nom , coutOrganisation , prixVente , idAmin , idCategorie);
               // Frame.Navigate(typeof(page1));
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

                    singletonBD.getInstance().addActivites(_idActivite, _nom, _coutOrganisation, _prixVente, _idAmin , _idCategorie);
                    // Joueur joueur = new Joueur(_matricule , _nom , _prenom , _dateNaissance , _nomEquipe);
                }
            }
        }
    }
}
