using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
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
    public sealed partial class PageAffichageAdherents : Page
    {
        public PageAffichageAdherents()
        {
            this.InitializeComponent();
            lvAdherent.ItemsSource = singletonBD.getInstance().getListeAdherent();
        }

        private async void btn_supprimer_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Adherents a = btn.DataContext as Adherents;

            lvAdherent.SelectedItem = a;

            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "Suppression d'adhérent";
            dialog.PrimaryButtonText = "Supprimer";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = $"Voulez vous supprimer l'adherent: {a.Prenom} ?";

            ContentDialogResult resultat = await dialog.ShowAsync();

            if (resultat == ContentDialogResult.Primary)
            {
                singletonBD.getInstance().supprimerAdherents(a.Nom);
            }

            else
            {
                lvAdherent.SelectedItem = null;
            }

        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tbx_recherche_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
