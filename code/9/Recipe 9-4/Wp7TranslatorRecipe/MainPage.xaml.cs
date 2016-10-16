using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using System.IO;
using System.Runtime.Serialization.Json;
using Wp7TranslatorRecipe.Entities;
using System.Net.NetworkInformation;

namespace Wp7TranslatorRecipe
{
    public partial class MainPage : PhoneApplicationPage
    {
        WebClient googleWebProxy = null;
        
        // Constructor
        public MainPage()
        {
            InitializeComponent();
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
        }

        void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            System.Resources.ResourceManager rm = new System.Resources.ResourceManager("Wp7TranslatorRecipe.Messages", System.Reflection.Assembly.GetExecutingAssembly());

            if (!NetworkInterface.GetIsNetworkAvailable())
                MessageBox.Show(rm.GetString("NoInternetConnection"));


            googleWebProxy = new WebClient();
            googleWebProxy.OpenReadCompleted += new OpenReadCompletedEventHandler(proxy_OpenReadCompleted);

        }

        private Uri buildGoogleTranslateRequest(string textToTranslate)
        {
            return new Uri("https://www.googleapis.com/language/translate/v2?key=AIzaSyARZUY_siigknrhcrbM05M2rf_gZvaueDw&q=" + textToTranslate + "&source=en&target=it&prettyprint=false");
        }

        private void TranslateButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(TextToTranslateTextbox.Text))
                {
                    Uri request = buildGoogleTranslateRequest(TextToTranslateTextbox.Text);
                    googleWebProxy.OpenReadAsync(request);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        void proxy_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            var ser = new DataContractJsonSerializer(typeof(GoogleTranslation));
            GoogleTranslation ppl = ser.ReadObject(e.Result) as GoogleTranslation;

            this.TranslatedTextTextbox.Text = ppl.Data.Translations.FirstOrDefault().TranslatedText;

        }
    }
}