using System;
using AlphaLogi.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AlphaLogi
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new HomePage()) { BarBackgroundColor = Color.Purple };
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
