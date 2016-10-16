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

namespace _7Drum
{
    public partial class Exercise : PhoneApplicationPage
    {
        public Exercise()
        {
            InitializeComponent();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.NavigationContext.QueryString.ContainsKey("Id"))
            {
                Guid id = new Guid(this.NavigationContext.QueryString["Id"]);

                ExerciseSettings exercise = (from ex in (Application.Current as App).Exercises
                                             where ex.id == id
                                             select ex).SingleOrDefault<ExerciseSettings>();

                txtName.Text = exercise.Name;
                txtDuration.Text = exercise.Duration.ToString();
                txtDescription.Text = exercise.Description;
            }
        }

        private void ApplicationBarIconSaveButton_Click(object sender, EventArgs e)
        {
            bool bNew = true;

            if (this.NavigationContext.QueryString.ContainsKey("Id"))
                bNew = false;
            else
                bNew = true;

            if (txtName.Text != string.Empty && txtDuration.Text != string.Empty)
            {
                ExerciseSettings exercise = null;

                if (!bNew)
                {
                    exercise = (from ex in (Application.Current as App).Exercises
                                where ex.id == new Guid(this.NavigationContext.QueryString["Id"])
                                select ex).SingleOrDefault<ExerciseSettings>();
                }
                else
                {
                    exercise = new ExerciseSettings();
                    exercise.id = Guid.NewGuid();
                }

                exercise.Description = txtDescription.Text;
                exercise.Name = txtName.Text;
                exercise.Duration = int.Parse(txtDuration.Text);

                if (bNew)
                    (Application.Current as App).Exercises.Add(exercise);

                Clean();
            }
            else
                MessageBox.Show("Name and duration fields are mandatory.", "7Drum", MessageBoxButton.OK);
        }

        private void Clean()
        {
            txtDuration.Text = string.Empty;
            txtName.Text = string.Empty;
            txtDescription.Text = string.Empty;
        }

        private void txtDuration_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space && e.Key != Key.D8 && e.Key != Key.D3 && e.Key != Key.Unknown)
                e.Handled = false;
            else
                e.Handled = true;
        }

    }
}