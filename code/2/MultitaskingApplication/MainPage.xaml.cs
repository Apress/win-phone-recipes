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
using Microsoft.Phone.Scheduler;
using System.IO.IsolatedStorage;
using System.IO;

namespace MultitaskingApplication
{
    public partial class MainPage : PhoneApplicationPage
    {
        ResourceIntensiveTask resourceIntensiveTask;
        string resourceIntensiveTaskName = "ResourceIntensiveAgent";

        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            try
            {
                tbMain.Text = "No new files arrived";

                using (IsolatedStorageFile file = IsolatedStorageFile.GetUserStoreForApplication())
                {
                    foreach (string filename in file.GetFileNames(@"\ProcessFile\*.xml"))
                    {
                        string f = @"\ProcessFile\" + filename;
                        using (var stream = new IsolatedStorageFileStream(f, FileMode.Open, file))
                        {
                            StreamReader reader = new StreamReader(stream);
                            tbMain.Text = reader.ReadToEnd();
                            tbMain.Text += "\n-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-="; // text content separator
                        }

                        file.DeleteFile(f);
                    }
                }
            }
            catch(Exception ex)
            {
                tbMain.Text = ex.Message;
            }

        }
    }
}