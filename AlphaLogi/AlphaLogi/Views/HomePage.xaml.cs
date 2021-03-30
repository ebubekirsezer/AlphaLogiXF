using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using AlphaLogi.Models;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AlphaLogi.Views
{
    public partial class HomePage : ContentPage
    {
        bool isCaptured = false;
        MediaFile mediaFile;

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

        private async void GetPredictions(string imageFilePath)
        {
            predictionIndicator.IsVisible = true;
            predictionIndicator.IsRunning = true;

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Prediction-Key", Constants.predictionKey);

            HttpResponseMessage response;
            byte[] byteData = GetImageAsByteArray(imageFilePath);

            using(ByteArrayContent content = new ByteArrayContent(byteData))
            {
                content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response = await client.PostAsync(Constants.uriBase, content);
                string contentString = await response.Content.ReadAsStringAsync();

                AzureModel model = JsonConvert.DeserializeObject<AzureModel>(contentString);
                var prediction = model.Predictions[0];
                labelPredictionName.Text = "Obje: " + prediction.TagName;
                labelPrediction.Text = "Olasılık: " + prediction.Probability;
            }

            predictionIndicator.IsVisible = false;
            predictionIndicator.IsRunning = false;
        }

        public byte[] GetImageAsByteArray(string imageFilePath)
        {
            using (FileStream fileStream =
                new FileStream(imageFilePath, FileMode.Open, FileAccess.Read))
            {
                BinaryReader binaryReader = new BinaryReader(fileStream);
                return binaryReader.ReadBytes((int)fileStream.Length);
            }
        }

        private async void TakePhoto()
        {
            await CrossMedia.Current.Initialize();

            if (!CrossMedia.Current.IsTakePhotoSupported || !CrossMedia.Current.IsCameraAvailable)
            {
                await DisplayAlert("ERROR", "Camera is NOT available", "OK");
                return;
            }

            mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "myImage.jpg",
                PhotoSize = PhotoSize.Medium
            });

            if (mediaFile == null)
            {
                return;
            }

            takenImage.Source = ImageSource.FromStream(() =>
            {
                return mediaFile.GetStream();
            });

            GetPredictions(mediaFile.Path);
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
