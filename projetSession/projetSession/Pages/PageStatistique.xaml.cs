﻿using Microsoft.UI;
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
        public PageStatistique()
        {
            this.InitializeComponent();

            tb_nbTotalAdherent.Text = singletonBD.getInstance().getListeAdherent().Count.ToString();

            tb_nbTotalActivite.Text = singletonBD.getInstance().getListe().Count.ToString();

            ObservableCollection<Activites> listeActivite = singletonBD.getInstance().getListe();

            if(listeActivite is not null)
            {
                StackPanel stk = new StackPanel();
                TextBlock textBlock = new TextBlock();
                TextBlock nombre = new TextBlock();


                //exercice basket a compléter

                stk.HorizontalAlignment = HorizontalAlignment.Stretch;
                stk.BorderThickness = new Thickness(4.00);
                stk.BorderBrush = new SolidColorBrush(Colors.Red);
                stk.Padding = new Thickness(20);



                stk.Children.Add(textBlock);
                stk.Children.Add(nombre);
                stk_nbAdherentParActivite.Children.Add(stk);
            }
        }
    }
}
