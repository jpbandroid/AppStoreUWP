﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using AppStore.Core.Models;
using AppStore.Core.Services;
using AppStore.Services;

using Microsoft.Toolkit.Uwp.UI.Animations;

using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace AppStore.Views
{
    public sealed partial class ContentGridPage : Page, INotifyPropertyChanged
    {
        public ObservableCollection<SampleOrder> Source { get; } = new ObservableCollection<SampleOrder>();

        public ContentGridPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            Source.Clear();

            // Replace this with your actual data
            var data = await SampleDataService.GetContentGridDataAsync();
            foreach (var item in data)
            {
                Source.Add(item);
            }
        }

        private void OnItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is SampleOrder item)
            {
                NavigationService.Frame.SetListDataItemForNextConnectedAnimation(item);
                NavigationService.Navigate<ContentGridDetailPage>(item.OrderID);
            }
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
    }
}
