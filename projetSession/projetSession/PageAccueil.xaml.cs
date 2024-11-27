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
    public sealed partial class PageAccueil : Page
    {
        //Visibility visibilityAdmin;

        public Visibility VisibilityAdmin { 
            get {
                return SingletonUtilisateur.getInstance().User.Role == "admin" ? Visibility.Visible : Visibility.Collapsed;
            } 
        }
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
            dialogActivites dialogue = new dialogActivites();
            dialogue.XamlRoot = this.XamlRoot;
            dialogue.Title = "Nouvel article";
            dialogue.PrimaryButtonText = "Ajouter";
            dialogue.CloseButtonText = "Annuler";
            dialogue.DefaultButton = ContentDialogButton.Close;

            var resultat = await dialogue.ShowAsync();

<<<<<<< HEAD
            //Button btn = sender as Button;
            //Activites a = btn.DataContext as Activites;

            //lv_Activites.SelectedItem = a;

            //ContentDialog dialog = new ContentDialog();
            //dialog.XamlRoot = this.XamlRoot;
            //dialog.Title = "Modification";
            //dialog.PrimaryButtonText = "Modifier";
            //dialog.CloseButtonText = "Annuler";
            //dialog.DefaultButton = ContentDialogButton.Primary;
            ////dialog.Content = $"Voulez vous supprimer le joueur: '{a.Prenom}' '{a.Nom}'  ?";

            //TextBox textBoxidActivite = new TextBox();
            ////textBoxidActivite.Header
            // //comboBox.Header = "Choisir l'équipe";
            // //comboBox.SelectedIndex = 0;
            // //dialog.Content = comboBox as ComboBox;
=======
            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "Modification";
            dialog.PrimaryButtonText = "Modifier";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;
            //dialog.Content = $"Voulez vous supprimer le joueur: '{a.Prenom}' '{a.Nom}'  ?";
            
            TextBox textBoxidActivite = new TextBox();
            //textBoxidActivite.Header
            //comboBox.Header = "Choisir l'équipe";
            //comboBox.SelectedIndex = 0;
            //dialog.Content = comboBox as ComboBox;

            ContentDialogResult resultat = await dialog.ShowAsync();

            if (resultat == ContentDialogResult.Primary)
            {
                // singletonBD.getInstance().modifierActivites(a.Matricule, comboBox.SelectedValue as string);
                // singletonBD.getInstance().modifierNomEquipeJoueur(a.Matricule, comboBox.SelectedValue as string);
            }

            // ContentDialogResult resultat = await dialog.ShowAsync();

            //if (resultat == ContentDialogResult.Primary)
            //{
            //   // singletonBD.getInstance().modifierActivites(a.Matricule, comboBox.SelectedValue as string);
            //    // singletonBD.getInstance().modifierNomEquipeJoueur(a.Matricule, comboBox.SelectedValue as string);

            //}

            //else
            //{
            //    lv_Activites.SelectedItem = null;
            //}
        }

        private void btn_Add_Click(object sender, RoutedEventArgs e)
        {

        }


        public void setPermission(int role)
        {
            stkpnl.Visibility = Visibility.Collapsed;
            switch (role)
            {
                case 3:
                    stkpnl.Visibility = Visibility.Collapsed;
                break;
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
    }
}
