using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AlphaLogi.Views
{
    public partial class HomePage : ContentPage
    {
        bool isCaptured = false;

        public HomePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (isCaptured == false)
            {
                TakePhoto();
                isCaptured = true;
            }
        }

        void takePhotoButton_Clicked(System.Object sender, System.EventArgs e)
        {
            TakePhoto();
        }

        private async void TakePhoto()
        {
            try
            {
                var result = await MediaPicker.CapturePhotoAsync(new MediaPickerOptions
                {
                    Title = "Please take a photo"
                });

                var stream = await result.OpenReadAsync();

                if (ImageSource.FromStream(() => stream) != null)
                {
                    takenImage.Source = ImageSource.FromStream(() => stream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async void PickPhoto()
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                {
                    Title = "Please pick a photo"
                });

                var stream = await result.OpenReadAsync();

                takenImage.Source = ImageSource.FromStream(() => stream);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
