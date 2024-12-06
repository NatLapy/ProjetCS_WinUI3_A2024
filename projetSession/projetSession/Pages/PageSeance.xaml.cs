using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
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
    public sealed partial class PageSeance : Page
    {
        public PageSeance()
        {
            this.InitializeComponent();
            lvSeances.ItemsSource = singletonBD.getInstance().getListeAdherent();
        }

        private void btn_supprimer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btn_Edit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void tbx_recherche_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
