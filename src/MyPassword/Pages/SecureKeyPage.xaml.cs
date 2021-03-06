﻿using MyPassword.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static MyPassword.ViewModels.SecureKeyViewModel;

namespace MyPassword.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SecureKeyPage : BaseContentPage
	{
        SecureKeyViewModel viewModel;
		public SecureKeyPage (Action KeySaveEvent)
		{
			InitializeComponent ();
            NavigationPage.SetHasBackButton(this, false);
            InitViewModel(KeySaveEvent);
        }
        

        private void InitViewModel(Action keySaveEvent)
        {
            viewModel = App.Locator.GetViewModel<SecureKeyViewModel, Action>(keySaveEvent);
            BindingContext = viewModel;
        }

        protected override void OnAppear()
        {
        }

        protected override void OnFirstAppear()
        {
        }
    }
}