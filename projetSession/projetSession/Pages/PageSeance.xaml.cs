using Microsoft.UI.Input;
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
        Adherents test = new Adherents();
        public PageSeance()
        {

            this.InitializeComponent();
            lvSeances.ItemsSource = singletonBD.getInstance().getListeAdherent();
            ObservableCollection<Adherents> listeAdherents = singletonBD.getInstance().getListeAdherent();

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

        private async void btn_Edit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Seances a = btn.DataContext as Seances;
            Inscriptions inscription = new Inscriptions();
            

            lvSeances.SelectedItem = a;

            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "inscription adhérent :";
            dialog.PrimaryButtonText = "Inscrire";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;
            //dialog.Content = $"Voulez vous supprimer l'adherent ?";
            AutoSuggestBox autoSuggestBox = new AutoSuggestBox();
            autoSuggestBox.TextChanged += AutoSuggestBox_TextChanged;
            autoSuggestBox.SuggestionChosen += AutoSuggestBox_SuggestionChosen;
            
            //new System.Linq.SystemCore_EnumerableDebugView<object>(autoSuggestBox.Items).Items[5]

            autoSuggestBox.Header = "Rechercher un adhérents.";
            //ComboBox comboBox = new ComboBox();
            //comboBox.Items.Add("River Lions de Niagara");
            //comboBox.Items.Add("Chicago bulls");
            //comboBox.Items.Add("Memphis Grizzlies");
            //comboBox.Items.Add("Lakers");
            //comboBox.Items.Add("Brooklyn Nets");
            //comboBox.Items.Add("Atlanta Hawks");
            //comboBox.Header = "Choisir l'adherent";
            //comboBox.SelectedIndex = 0;
            //dialog.Content = comboBox as ComboBox;

            dialog.Content = autoSuggestBox;

            ContentDialogResult resultat = await dialog.ShowAsync();

            if (resultat == ContentDialogResult.Primary)
            {
                string noIdentification = test.NoIdentification.ToUpper();
                singletonBD.getInstance().ajouterAdherentsSeance(noIdentification,a.IdSceances);
                // singletonBD.getInstance().modifierNomEquipeJoueur(a.Matricule, comboBox.SelectedValue as string);

            }

            else
            {
                lvSeances.SelectedItem = null;
            }
        }

        private void AutoSuggestBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            test = (Adherents)args.SelectedItem;
        }

        private void AutoSuggestBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            AutoSuggestBox autoSuggestBox = sender as AutoSuggestBox;

            if (autoSuggestBox.Text == "")
            {
                autoSuggestBox.ItemsSource = null;
                return;
            }
            singletonBD.getInstance().rechercheAdherent(autoSuggestBox.Text);
            ObservableCollection<Adherents> suggestions = singletonBD.getInstance().Adherents;

            if (suggestions.Count == 0)
                return;

            autoSuggestBox.ItemsSource = suggestions;

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

        private void btn_add_adherent_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Seances seance = btn.DataContext as Seances;

            singletonBD.getInstance().ajouterAdherentsSeance(SingletonUtilisateur.getInstance().User.NomUtilisateur, seance.IdSceances);

            btn.Visibility = Visibility.Collapsed;
            Frame.Navigate(typeof(PageAccueil));
        }

        private void btn_add_adherent_Loaded(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            Seances seance = btn.DataContext as Seances;

            bool estInscris = singletonBD.getInstance().estInscris(SingletonUtilisateur.getInstance().User.NomUtilisateur, seance.IdSceances);

            if (estInscris == true)
            {
                btn.Visibility = Visibility.Collapsed;
            }
            else
            {
                btn.Visibility = Visibility.Visible;
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
            if(sender is not null && e is not null)
            {
                Button btn = sender as Button;
                var cursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 0);
                InputCursor inputCurs = InputCursor.CreateFromCoreCursor(cursor);
                this.ProtectedCursor = inputCurs;
            }
            
        }

        private void tb_note_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            Seances seance = textBlock.DataContext as Seances;

            bool estInscris = singletonBD.getInstance().estInscris(SingletonUtilisateur.getInstance().User.NomUtilisateur, seance.IdSceances);

            if (estInscris == true)
            {
                textBlock.Visibility = Visibility.Visible;

                if (singletonBD.getInstance().aNoter(SingletonUtilisateur.getInstance().User.NomUtilisateur, seance.IdSceances) == true)
                {
                    textBlock.Text = singletonBD.getInstance().getNoteAppreciation(SingletonUtilisateur.getInstance().User.NomUtilisateur, seance.IdSceances).ToString();
                }
                else
                {
                    textBlock.Text = "Pas de notes fait encore";
                    
                }
            }
            else
            {
                textBlock.Visibility = Visibility.Collapsed;
            }

        }

        private async void rc_note_Tapped(object sender, TappedRoutedEventArgs e)
        {
            RatingControl ratingControl = sender as RatingControl;
            Seances seance = ratingControl.DataContext as Seances;

            ContentDialog dialog = new ContentDialog();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Title = "Note d'appréciation";
            dialog.PrimaryButtonText = "Confirmer";
            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Primary;
            dialog.Content = "Voulez vous noter cette séance?";

            ContentDialogResult resultat = await dialog.ShowAsync();

            if (resultat == ContentDialogResult.Primary)
            {
                singletonBD.getInstance().ajouterNote(SingletonUtilisateur.getInstance().User.NomUtilisateur, seance.IdSceances, ratingControl.Value);
                Frame.Navigate(typeof(PageAccueil));            
            }


        }

        private void rc_note_Loaded(object sender, RoutedEventArgs e)
        {

            RatingControl ratingControl = sender as RatingControl;
            Seances seance = ratingControl.DataContext as Seances;

            bool estInscris = singletonBD.getInstance().estInscris(SingletonUtilisateur.getInstance().User.NomUtilisateur, seance.IdSceances);

            if (estInscris == true && singletonBD.getInstance().aNoter(SingletonUtilisateur.getInstance().User.NomUtilisateur, seance.IdSceances) == false)
            {
                ratingControl.Visibility = Visibility.Visible;
            }
            else
            {
                ratingControl.Visibility = Visibility.Collapsed;
            }
        }
    }
}
