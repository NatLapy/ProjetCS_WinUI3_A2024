using Microsoft.UI;
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
    public sealed partial class PageStatistique : Page
    {
        ObservableCollection<Adherents> listeAdherent = singletonBD.getInstance().getListeAdherent();
        ObservableCollection<Activites> listeActivite = singletonBD.getInstance().getListe();
        public PageStatistique()
        {
            this.InitializeComponent();

            tb_nbTotalAdherent.Text = singletonBD.getInstance().getListeAdherent().Count.ToString();

            tb_nbTotalActivite.Text = singletonBD.getInstance().getListe().Count.ToString();

            ObservableCollection<Activites> listeActivite = singletonBD.getInstance().getListe();

            nbAdherentParActivite();

            moyenneNoteParActivite();

            nombreActiviteParCategorie();

            nombrePlaceDispoParActivite();



        }

        public void nombrePlaceDispoParActivite()
        {
            string[] nomActivites3 = singletonBD.getInstance().getActiviteNom();
            int[] idActivites3 = singletonBD.getInstance().getActiviteId();
            int totalActivite3 = singletonBD.getInstance().getListe().Count;

            for (int i = 0; i < (totalActivite3 - 1); i++)
            {
                StackPanel stk = new StackPanel();
                TextBlock textBlock = new TextBlock();
                TextBlock nombre = new TextBlock();
                stk.HorizontalAlignment = HorizontalAlignment.Stretch;
                stk.BorderThickness = new Thickness(4.00);
                stk.BorderBrush = new SolidColorBrush(Colors.Red);
                stk.Padding = new Thickness(20);

                textBlock.Text = nomActivites3[i].ToString();
                textBlock.FontSize = 15;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;

                nombre.Text = singletonBD.getInstance().getNbPlaceParActivite(idActivites3[i]).ToString() + " Places";


                nombre.FontSize = 15;
                nombre.HorizontalAlignment = HorizontalAlignment.Center;


                stk.Children.Add(textBlock);
                stk.Children.Add(nombre);
                stk_nbPlaceParActivite.Children.Add(stk);

            }
        }

        public void nombreActiviteParCategorie()
        {
            string[] nomCategorie = singletonBD.getInstance().getCategorieNom();
            int[] idCategorie = singletonBD.getInstance().getCategorieId();
            int totalCategorie = singletonBD.getInstance().getCategoriesListe().Count;

            for (int i = 0; i < (totalCategorie - 1); i++)
            {
                StackPanel stk = new StackPanel();
                TextBlock textBlock = new TextBlock();
                TextBlock nombre = new TextBlock();
                stk.HorizontalAlignment = HorizontalAlignment.Stretch;
                stk.BorderThickness = new Thickness(4.00);
                stk.BorderBrush = new SolidColorBrush(Colors.Red);
                stk.Padding = new Thickness(20);

                textBlock.Text = nomCategorie[i].ToString();
                textBlock.FontSize = 15;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;

                nombre.Text = singletonBD.getInstance().getNbActiviteParCategorie(idCategorie[i]).ToString() + " Activités";


                nombre.FontSize = 15;
                nombre.HorizontalAlignment = HorizontalAlignment.Center;


                stk.Children.Add(textBlock);
                stk.Children.Add(nombre);
                stk_nbActiviteParCategorie.Children.Add(stk);

            }
        }

        public void moyenneNoteParActivite()
        {
            string[] nomActivites2 = singletonBD.getInstance().getActiviteNom();
            int[] idActivites2 = singletonBD.getInstance().getActiviteId();
            int totalActivite2 = singletonBD.getInstance().getListe().Count;

            for (int i = 0; i < (totalActivite2 - 1); i++)
            {
                StackPanel stk = new StackPanel();
                TextBlock textBlock = new TextBlock();
                TextBlock nombre = new TextBlock();

                stk.HorizontalAlignment = HorizontalAlignment.Stretch;
                stk.BorderThickness = new Thickness(4.00);
                stk.BorderBrush = new SolidColorBrush(Colors.Red);
                stk.Padding = new Thickness(20);

                textBlock.Text = nomActivites2[i].ToString();
                textBlock.FontSize = 15;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;

                nombre.Text = singletonBD.getInstance().getMoyenneParActivite(idActivites2[i]).ToString() + " Étoiles";


                nombre.FontSize = 15;
                nombre.HorizontalAlignment = HorizontalAlignment.Center;


                stk.Children.Add(textBlock);
                stk.Children.Add(nombre);
                stk_moyenneParActivite.Children.Add(stk);

            }
        }



        public void nbAdherentParActivite()
        {
            string[] nomActivites = singletonBD.getInstance().getActiviteNom();
            int[] idActivites = singletonBD.getInstance().getActiviteId();
            int totalActivite = singletonBD.getInstance().getListe().Count;

            for (int i = 0; i < (totalActivite - 1); i++)
            {
                StackPanel stk = new StackPanel();
                TextBlock textBlock = new TextBlock();
                TextBlock nombre = new TextBlock();
                stk.HorizontalAlignment = HorizontalAlignment.Stretch;
                stk.BorderThickness = new Thickness(4.00);
                stk.BorderBrush = new SolidColorBrush(Colors.Red);
                stk.Padding = new Thickness(20);

                textBlock.Text = nomActivites[i].ToString();
                textBlock.FontSize = 15;
                textBlock.HorizontalAlignment = HorizontalAlignment.Center;

                nombre.Text = singletonBD.getInstance().getCountAdherentsActivite(idActivites[i]).ToString() + " Adherents";


                nombre.FontSize = 15;
                nombre.HorizontalAlignment = HorizontalAlignment.Center;


                stk.Children.Add(textBlock);
                stk.Children.Add(nombre);
                stk_nbAdherentParActivite.Children.Add(stk);

            }
        }

        private void btnChercherSeance_Click(object sender, RoutedEventArgs e)
        {
            if(cbx_Activite.SelectedItem is not null && cbx_adherent.SelectedItem is not null)
            {
                int idActivite = (int)cbx_Activite.SelectedValue;
                string idAdherent = cbx_adherent.SelectedValue as string;
                lvSeances.ItemsSource = singletonBD.getInstance().getListeSeanceAdherent(idActivite,idAdherent);
            }
        }
    }
}
