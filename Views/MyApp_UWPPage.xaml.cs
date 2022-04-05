﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Windows.UI.Xaml.Controls;

namespace AppStore.Views
{
    public sealed partial class MyApp_UWPPage : Page, INotifyPropertyChanged
    {
        public MyApp_UWPPage()
        {
            InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        private void hyperlinkclick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(jpb_AppsPage));
        }

        private void download(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MyAppUWPDownload));
        }
    }
}