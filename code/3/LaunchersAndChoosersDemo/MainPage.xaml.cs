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
using Microsoft.Phone.Tasks;

namespace LaunchersAndChoosersDemo
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void FirstListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((e.AddedItems[0] as ListBoxItem).Name)
            {
                case "iBingMapsDirectionsTask":
                    BingMapsDirectionsTask bmdt = new BingMapsDirectionsTask();
                    bmdt.Start = new LabeledMapLocation("Rome, Italy", null);
                    bmdt.End = new LabeledMapLocation("Milan, Italy", null);
                    bmdt.Show();
                    break;
                case "iBingMapsTask":
                    BingMapsTask bmt = new BingMapsTask();
                    bmt.SearchTerm = "Buckingham Palace, London";
                    bmt.ZoomLevel = 400;
                    bmt.Show();
                    break;
                case "iConnectionSettingsTask":
                    ConnectionSettingsTask cst = new ConnectionSettingsTask();
                    cst.ConnectionSettingsType = ConnectionSettingsType.WiFi;
                    cst.Show();
                    break;
                case "iEmailComposeTask":
                    EmailComposeTask ect = new EmailComposeTask();
                    ect.To = "some.email@address.com";
                    ect.Subject = "Some subject";
                    ect.Body = "email content here";
                    ect.Show();
                    break;
                case "iMarketplaceDetailTask":
                    MarketplaceDetailTask mdt = new MarketplaceDetailTask();
                    mdt.ContentType = MarketplaceContentType.Applications;
                    mdt.Show();
                    break;
                case "iMarketplaceHubTask":
                    MarketplaceHubTask mht = new MarketplaceHubTask();
                    mht.ContentType = MarketplaceContentType.Applications;
                    mht.Show();
                    break;
                case "iMarketplaceReviewTask":
                    MarketplaceReviewTask mrt = new MarketplaceReviewTask();
                    mrt.Show();
                    break;
                case "iMarketplaceSearchTask":
                    MarketplaceSearchTask mst = new MarketplaceSearchTask();
                    mst.ContentType = MarketplaceContentType.Music;
                    mst.SearchTerms = "Radiohead";
                    mst.Show();
                    break;
                case "iMediaPlayerLauncher":
                    MediaPlayerLauncher mpl = new MediaPlayerLauncher();
                    mpl.Media = new Uri("Notes_INTRO_BG.wmv", UriKind.Relative);
                    mpl.Location = MediaLocationType.Install;
                    mpl.Controls = MediaPlaybackControls.Pause | 
                                   MediaPlaybackControls.Stop | 
                                   MediaPlaybackControls.Rewind;
                    mpl.Show();
                    break;
                case "iPhoneCallTask":
                    PhoneCallTask pct = new PhoneCallTask();
                    pct.DisplayName = "My contact";
                    pct.PhoneNumber = "55512345678";
                    pct.Show();
                    break;
                case "iSearchTask":
                    SearchTask st = new SearchTask();
                    st.SearchQuery = "Facebook";
                    st.Show();
                    break;
                case "iShareLinkTask":
                    ShareLinkTask slt = new ShareLinkTask();
                    slt.Title = "AS Roma scored";
                    slt.Message = "What a shot!";
                    slt.LinkUri = new Uri("http://www.youtube.com/", UriKind.Absolute);
                    slt.Show();
                    break;
                case "iShareStatusTask":
                    ShareStatusTask sst = new ShareStatusTask();
                    sst.Status = "I'm writing Windows Phone Recipe book!";
                    sst.Show();
                    break;
                case "iSmsComposeTask":
                    SmsComposeTask sct = new SmsComposeTask();
                    sct.To = "My contact";
                    sct.Body = "sms content";
                    sct.Show();
                    break;
                case "iWebBrowserTask":
                    WebBrowserTask wbt = new WebBrowserTask();
                    wbt.Uri = new Uri("http://www.apress.com", UriKind.Absolute);
                    wbt.Show();
                    break;
            }
        }

        private void SecondListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch ((e.AddedItems[0] as ListBoxItem).Name)
            {
                case "iAddressChooserTask":
                    AddressChooserTask act = new AddressChooserTask();
                    act.Completed += new EventHandler<AddressResult>(AddressChooserTask_Completed);
                    act.Show();
                    break;
                case "iCameraCaptureTask":
                    MessageBox.Show("See recipe 8-1 for CameraCaptureTask chooser explanation.");
                    break;
                case "iPhotoChooserTask":
                    MessageBox.Show("See recipe 8-3 for PhotoChooserTask chooser explanation.");
                    break;
                case "iEmailAddressChooserTask":
                    EmailAddressChooserTask eact = new EmailAddressChooserTask();
                    eact.Completed += new EventHandler<EmailResult>(EmailAddressChooserTask_Completed);
                    eact.Show();
                    break;
                case "iGameInviteTask":
                    GameInviteTask git = new GameInviteTask();
                    git.Completed += new EventHandler<TaskEventArgs>(GameInviteTask_Completed);
                    git.SessionId = "Join my game network";
                    git.Show();
                    break;
                case "iPhoneNumberChooserTask":
                    PhoneNumberChooserTask pnct = new PhoneNumberChooserTask();
                    pnct.Completed += new EventHandler<PhoneNumberResult>(PhoneNumberChooserTask_Completed);
                    pnct.Show();
                    break;
                case "iSaveContactTask":
                    SaveContactTask sct = new SaveContactTask();
                    sct.Completed += new EventHandler<SaveContactResult>(SaveContactTask_Completed);
                    sct.FirstName = "Ewan";
                    sct.LastName = "Buckingham";
                    sct.Company = "Apress";
                    sct.Show();
                    break;
                case "iSaveEmailAddressTask":
                    SaveEmailAddressTask seat = new SaveEmailAddressTask();
                    seat.Completed += new EventHandler<TaskEventArgs>(SaveEmailAddressTask_Completed);
                    seat.Email = "some.email@address.com";
                    seat.Show();
                    break;
                case "iSavePhoneNumberTask":
                    SavePhoneNumberTask spnt = new SavePhoneNumberTask();
                    spnt.PhoneNumber = "55512345678";
                    spnt.Completed += new EventHandler<TaskEventArgs>(SavePhoneNumberTask_Completed);
                    spnt.Show();
                    break;
                case "iSaveRingtoneTask":
                    SaveRingtoneTask srt = new SaveRingtoneTask();
                    srt.Completed += new EventHandler<TaskEventArgs>(SaveRingtoneTask_Completed);
                    srt.DisplayName = "Ringtone from code";
                    srt.IsShareable = true;
                    srt.Source = new Uri("appdata:/Ringtone 10.wma");
                    srt.Show();
                    break;
            }
        }

        void SaveRingtoneTask_Completed(object sender, TaskEventArgs e)
        {
            if (e.TaskResult == TaskResult.OK)
                MessageBox.Show("Ringtone added successfully");
            else
                MessageBox.Show("Ringtone not added");
        }

        void SaveContactTask_Completed(object sender, SaveContactResult e)
        {
            if (e.TaskResult == TaskResult.OK)
                MessageBox.Show("Contact added successfully");
            else
                MessageBox.Show("Contact not added");
        }

        void GameInviteTask_Completed(object sender, TaskEventArgs e)
        {
            if (e.TaskResult == TaskResult.OK)
                MessageBox.Show("Invite sent successfully");
            else
                MessageBox.Show("Invite not sent");
        }

        void AddressChooserTask_Completed(object sender, AddressResult e)
        {
            if (e.TaskResult == TaskResult.OK)
            {
                MessageBox.Show(e.DisplayName);
            }
        }

        void SavePhoneNumberTask_Completed(object sender, TaskEventArgs e)
        {
            if (e.TaskResult == TaskResult.OK)
                MessageBox.Show("Phone numeber saved correctly");
            else if (e.TaskResult == TaskResult.Cancel)
                MessageBox.Show("Save operation cancelled");
        }

        void SaveEmailAddressTask_Completed(object sender, TaskEventArgs e)
        {
            if (e.TaskResult == TaskResult.OK)
                MessageBox.Show("Email saved correctly");
            else if (e.TaskResult == TaskResult.Cancel)
                MessageBox.Show("Save operation cancelled");
        }

        void PhoneNumberChooserTask_Completed(object sender, PhoneNumberResult e)
        {
            if (e.TaskResult == TaskResult.OK)
                MessageBox.Show(e.PhoneNumber);
        }

        void EmailAddressChooserTask_Completed(object sender, EmailResult e)
        {
            if (e.TaskResult == TaskResult.OK)
                MessageBox.Show(e.Email);
        }

    }
}