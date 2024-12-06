using Microsoft.UI;
using Microsoft.UI.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Documents;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using projetSession.Classes;
using projetSession.Singletons;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace projetSession.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageAccueil : Page
    {
        
        
        public PageAccueil()
        {
            this.InitializeComponent();
            lv_Activites.ItemsSource = singletonBD.getInstance().getListe();
            //lv_Adherents.ItemsSource = singletonBD.getInstance().getListeAdherents();
        }

        


        private void tbx_recherche_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tbx_recherche.Text.Trim() != "")
                singletonBD.getInstance().recherche(tbx_recherche.Text);
            else
                singletonBD.getInstance().getActivites();
        }

        private async void btn_supprimer_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Activites a = btn.DataContext as Activites;

            lv_Activites.SelectedItem = a;

            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "Suppression d'équipe";
            dialog.PrimaryButtonText = "Supprimer";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = $"Voulez vous supprimer l'équipe: {a.Nom} ?";

            ContentDialogResult resultat = await dialog.ShowAsync();

            if (resultat == ContentDialogResult.Primary)
            {
                singletonBD.getInstance().supprimerActivites(a.Nom);
            }

            else
            {
                lv_Activites.SelectedItem = null;
            }
           
        }

        private async void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            DialogueActivites dialogue = new DialogueActivites();
            dialogue.XamlRoot = this.XamlRoot;
            dialogue.Title = "Nouvel article";
            dialogue.PrimaryButtonText = "Ajouter";
            dialogue.CloseButtonText = "Annuler";
            dialogue.DefaultButton = ContentDialogButton.Close;

            var resultat = await dialogue.ShowAsync();



            //ContentDialogResult resultat = await dialog.ShowAsync();



        }


        public Visibility VisibilityAdmin
        {
            get
            {
                return SingletonUtilisateur.getInstance().User.Role == "Admin" ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is not null)
            {
                SingletonUtilisateur.getInstance().User.Role = "nonConnecter";
            }



            NavigationViewItem tempType = new NavigationViewItem();
            foreach (Control item in ServiceNavigation.getInstance().NavigationView.MenuItems)
            {
                if (item.GetType() == tempType.GetType())
                {
                    NavigationViewItem navViewItem = (NavigationViewItem)item;
                    if (item.Name == "iInscription")
                    {
                        item.Visibility = VisibilityAdmin;
                    }
                    if (item.Name == "iActivite")
                    {
                        item.Visibility = VisibilityAdmin;
                    }
                    if (item.Name == "iPageAdherents")
                    {
                        item.Visibility = VisibilityAdmin;
                    }
                    if (item.Name == "iStatistique")
                    {
                        item.Visibility = VisibilityAdmin;
                    }
                    if (item.Name == "iConnecter")
                    {
                        navViewItem.Visibility = VisibilityAdmin;
                        item.Visibility = navViewItem.Visibility;
                    }
                    if (item.Name == "iDeconnection")
                    {
                        navViewItem.Visibility = VisibilityAdmin;
                        item.Visibility = navViewItem.Visibility;
                    }
                }

            }foreach (Control item in ServiceNavigation.getInstance().NavigationView.FooterMenuItems)
            {
                if (item.GetType() == tempType.GetType())
                {
                    NavigationViewItem navViewItem = (NavigationViewItem)item;
                    if (item.Name == "iConnecter")
                    {
                        item.Visibility = SingletonUtilisateur.getInstance().User.Role == "Admin" ? Visibility.Collapsed : Visibility.Visible;
                    }
                    if (item.Name == "iDeconnection")
                    {
                        item.Visibility = VisibilityAdmin;
                    }
                }

            }
        }


        private void BtnHover(object sender, PointerRoutedEventArgs e)
        {
            Button btn = sender as Button;
            var cursor1 = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
            var cursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 0);
            InputCursor inputCurs = InputCursor.CreateFromCoreCursor(cursor);
            this.ProtectedCursor = inputCurs;

        }

        private void BtnSortie(object sender, PointerRoutedEventArgs e)
        {
            Button btn = sender as Button;
            var cursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);
            InputCursor inputCurs = InputCursor.CreateFromCoreCursor(cursor);
            this.ProtectedCursor = inputCurs;
        }

        private async void btn_ajouter_liste_Click(object sender, RoutedEventArgs e)
        {
            int _idActivite = 0;
            String s_idActivite = "";

            String _nom = "";

            int _coutOrganisation =0;
            String s_coutOrganisation = "";

            int _prixDeVente = 0;
            String s_prixDeVente = "";

            int _idAdmin = 0;
            String S_idAdmin = "";

            int _idCategorie = 0;
            String s_idCategorie = "";

            string s_urlImage = "";

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

                    s_idActivite = v[0];
                    _idActivite = Convert.ToInt32(s_idActivite);

                    _nom = v[1];

                    s_coutOrganisation = v[2];
                    _coutOrganisation = Convert.ToInt32(s_coutOrganisation);

                    s_prixDeVente = v[3];
                    _prixDeVente = Convert.ToInt32(s_prixDeVente);

                    S_idAdmin = v[4];
                    _idAdmin = Convert.ToInt32(S_idAdmin);

                    s_idCategorie = v[5];
                    _idCategorie = Convert.ToInt32(s_idCategorie);

                    s_urlImage = v[6];

                    singletonBD.getInstance().addActivites(_nom, _coutOrganisation, _prixDeVente, _idCategorie, s_urlImage);
                    // Joueur joueur = new Joueur(_matricule , _nom , _prenom , _dateNaissance , _nomEquipe);

                }

            }
        }

        private void btn_seances_admin_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Activites activite = btn.DataContext as Activites;

            lv_Activites.SelectedItem = activite;

            Frame.Navigate(typeof(PageSeance), activite.IdActivite);
        }
    }
}
