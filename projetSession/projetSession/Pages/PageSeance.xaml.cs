using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using Microsoft.UI.Xaml.Navigation;
using projetSession.Classes;
using projetSession.Singletons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public sealed partial class PageSeance : Page
    {
        ObservableCollection<Seances> listeSeances;
        public PageSeance()
        {

            this.InitializeComponent();
            lvSeances.ItemsSource = singletonBD.getInstance().getListeAdherent();
        }

        private async void btn_supprimer_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Seances a = btn.DataContext as Seances;

            lvSeances.SelectedItem = a;

            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "Suppression de séances";
            dialog.PrimaryButtonText = "Supprimer";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = $"Voulez vous supprimer le joueur: '{a.IdSceances}'  ?";

            ContentDialogResult resultat = await dialog.ShowAsync();

            if (resultat == ContentDialogResult.Primary)
            {
                singletonBD.getInstance().supprimerSeance(a.IdSceances);
            }

            else
            {
                lvSeances.SelectedItem = null;
            }
        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tbx_recherche_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //parametre contient mon index , on l'attribut a e 
            if (e.Parameter != null)
            {
                //on cast en int le parametre qui est de type objet a la base 
                int index = (int)e.Parameter;

                listeSeances = singletonBD.getInstance().getListeSeance(index);
                lvSeances.ItemsSource = listeSeances;


            }
        }
    }
}
