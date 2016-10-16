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
using Microsoft.Phone.Shell;

namespace _7Drum
{
    public partial class Training : PhoneApplicationPage
    {
        public Training()
        {
            InitializeComponent();
            btnNew = ApplicationBar.Buttons[0] as ApplicationBarIconButton;
            btnPlay = ApplicationBar.Buttons[1] as ApplicationBarIconButton;
            btnEdit = ApplicationBar.Buttons[2] as ApplicationBarIconButton;
            btnDelete = ApplicationBar.Buttons[3] as ApplicationBarIconButton;
        }

        private void ApplicationBarIconNewButton_Click(object sender, EventArgs e)
        {
            this.NavigationService.Navigate(new Uri("/Exercise.xaml", UriKind.Relative));
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            App app = Application.Current as App;
            if (app != null && app.Exercises.Count > 0)
            {
                // Refresh ListBox items
                lbExercise.ItemsSource = null;
                lbExercise.ItemsSource = app.Exercises.OrderBy(o => o.Name);
            }
        }

        private void lbExercise_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lbExercise.SelectedItems.Count > 0)
            {
                btnPlay.IsEnabled = true;
                btnEdit.IsEnabled = true;
                btnDelete.IsEnabled = true;
            }
            else
            {
                btnPlay.IsEnabled = false;
                btnEdit.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }

        }

        private void ApplicationBarIconPlayButton_Click(object sender, EventArgs e)
        {
            if (lbExercise.SelectedItem != null)
            {
                int duration = ((ExerciseSettings)lbExercise.SelectedItem).Duration;
                this.NavigationService.Navigate(new Uri("/TimeCounter.xaml?Duration=" + duration.ToString(), UriKind.Relative));
            }
        }

        private void ApplicationBarIconEditButton_Click(object sender, EventArgs e)
        {
            if (lbExercise.SelectedItem != null)
            {
                Guid id = ((ExerciseSettings)lbExercise.SelectedItem).id;
                this.NavigationService.Navigate(new Uri("/Exercise.xaml?Id=" + id.ToString(), UriKind.Relative));
            }
        }

        private void ApplicationBarIconDeleteButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Do you really want to delete this exercise?", "7Drum", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
            {
                ExerciseSettings ex = (ExerciseSettings)lbExercise.SelectedItem;
                (Application.Current as App).Exercises.Remove(ex);

                // Refresh ListBox items
                lbExercise.ItemsSource = null;
                lbExercise.ItemsSource = (Application.Current as App).Exercises.OrderBy(o => o.Name);
            }
        }

        private void ApplicationBarMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}