﻿#pragma checksum "c:\users\leviathan\documents\visual studio 2010\Projects\Wp7Recipe 08-03 Settings\Wp7Recipe 08-03 Settings\Settings.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "01EACF6ADAC6EEB35D21305AAB10B244"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace Wp7Recipe_08_03_Settings {
    
    
    public partial class Settings : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal Microsoft.Phone.Controls.PhoneApplicationPage SaveButton;
        
        internal Microsoft.Phone.Shell.ApplicationBarIconButton SaveSettingsButton;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.TextBlock PageTitle;
        
        internal System.Windows.Controls.ScrollViewer ContentPanel;
        
        internal Microsoft.Phone.Controls.ToggleSwitch ToastSwitch;
        
        internal Microsoft.Phone.Controls.ToggleSwitch SoundsSwitch;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/Wp7Recipe%2008-03%20Settings;component/Settings.xaml", System.UriKind.Relative));
            this.SaveButton = ((Microsoft.Phone.Controls.PhoneApplicationPage)(this.FindName("SaveButton")));
            this.SaveSettingsButton = ((Microsoft.Phone.Shell.ApplicationBarIconButton)(this.FindName("SaveSettingsButton")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.PageTitle = ((System.Windows.Controls.TextBlock)(this.FindName("PageTitle")));
            this.ContentPanel = ((System.Windows.Controls.ScrollViewer)(this.FindName("ContentPanel")));
            this.ToastSwitch = ((Microsoft.Phone.Controls.ToggleSwitch)(this.FindName("ToastSwitch")));
            this.SoundsSwitch = ((Microsoft.Phone.Controls.ToggleSwitch)(this.FindName("SoundsSwitch")));
        }
    }
}

