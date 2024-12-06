using Microsoft.UI.Input;
using Microsoft.UI;
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
using System.Data;
using projetSession.Singletons;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace projetSession.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PageConnection : Page
    {
        
        Boolean validation = true;

        public PageConnection()
        {
            this.InitializeComponent();
        }


        private void btnConnection_Click(object sender, RoutedEventArgs e)
        {
            validation = true;
            string idAdherent = tbx_idAdherent.Text;

            if (string.IsNullOrWhiteSpace(idAdherent))
            {
                validation = false;
                idAdherentErr.Text = "Veuillez entrez le numéro d'identification";
            }
            else
            {
                if (!singletonBD.getInstance().estAdherent(idAdherent)){
                    validation = false;
                    idAdherentErr.Text = "Aucun Adherent existe avec ce numéro d'identification";
                }
            }

            

            if (validation)
            {
                singletonBD.getInstance().connexionAdherent(idAdherent);
                Frame.Navigate(typeof(PageAccueil));
            }
        }


        private async void lienAdmin_Click(Microsoft.UI.Xaml.Documents.Hyperlink sender, Microsoft.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            Hyperlink btn = sender as Hyperlink;

            DialogueAdmin dialog = new DialogueAdmin();
            dialog.XamlRoot = this.XamlRoot;
            dialog.Foreground = new SolidColorBrush(Colors.White);

            dialog.Title = "Authentification";
            dialog.PrimaryButtonText = "Se connecter";

            dialog.CloseButtonText = "Annuler";
            dialog.DefaultButton = ContentDialogButton.Close;

            ContentDialogResult resultat = await dialog.ShowAsync();


            if (resultat == ContentDialogResult.Primary)
            {

                Frame.Navigate(typeof(PageAccueil));
            }

            else
            {

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
