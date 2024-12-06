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
using static System.Net.Mime.MediaTypeNames;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace projetSession
{
    public sealed partial class DialogueActivites : ContentDialog
    {

        ObservableCollection<Categories> listeCategories = singletonBD.getInstance().getCategoriesListe();
        

        Boolean validation;


        public DialogueActivites()
        {
            this.InitializeComponent();
            cbx_idCategorie.ItemsSource = singletonBD.getInstance().getCategoriesListe();
        }
        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            tbl_error_Categorie.Text = cbx_idCategorie.SelectedValue.ToString();
            if (string.IsNullOrWhiteSpace(tbx_id.Text))
            {
                /*< StackPanel >
                    < TextBox x: Name = "tbx_idCategorie" Margin = "5" Header = "Id Categorie :" />
                    < TextBlock x: Name = "tbl_error_idCategorie" Text = "" Foreground = "Red" ></ TextBlock >
                </ StackPanel >*/
                tbl_error_id.Text = "Vous devez entrez un id.";
                validation = false;
            }
            else
            {
                tbl_error_id.Text = "";
                validation = true;
            }
        }

        private void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if (args.Result == ContentDialogResult.Primary)
            {
                if (validation == false)
                    args.Cancel = true;
            }
        }
    }
}
