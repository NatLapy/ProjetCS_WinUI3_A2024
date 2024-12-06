﻿using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using MySqlX.XDevAPI.Common;
using projetSession.Pages;
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

namespace projetSession
{
    public sealed partial class DialogueAdmin : ContentDialog
    {
        public String Nom { get; set; }
        public String Mdp { get; set; }

        bool fermer = false;
        Boolean validation = true;


        public DialogueAdmin()
        {
            this.InitializeComponent();
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            Nom = tbx_user.Text;
            Mdp = pwd_user.Password;
        }

        private void ContentDialog_Closing(ContentDialog sender, ContentDialogClosingEventArgs args)
        {
            if (args.Result == ContentDialogResult.Primary)
            {
                validation = true;

                if (string.IsNullOrWhiteSpace(Nom))
                {
                    errNom.Text = "Le nom d'utilisateur est requis";
                    args.Cancel = true;
                    validation = false;
                }

                if (string.IsNullOrWhiteSpace(Mdp)){
                    errMdp.Text = "Le mot de passe est requis";
                    args.Cancel = true;
                    validation = false;
                }
                

                if (!singletonBD.getInstance().estAdmin(Nom,Mdp) && !string.IsNullOrWhiteSpace(Mdp) && !string.IsNullOrWhiteSpace(Nom) )
                {
                    validation = true;
                }



                if (validation)
                {
                    singletonBD.getInstance().connexionAdmin(Nom,Mdp);
                    args.Cancel = false;
                }


                
            }
            else{
                args.Cancel = false;
            }
        }


    }
}
