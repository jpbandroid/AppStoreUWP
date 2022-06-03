using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;

namespace AppStore.Views
{
    public sealed partial class MyAppPage : Page, INotifyPropertyChanged
    {
        public MyAppPage()
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

        private void hyperlinkclick(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(jpb_AppsPage));
        }

        private async void download(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            try
            {
                var myappuri = new Uri("https://occoam.com/jpb/wp-content/uploads/MyApp1.0.2.zip");
                BackgroundDownloader downloader = new BackgroundDownloader();
                Uri source = myappuri;
                FolderPicker picker = new FolderPicker { SuggestedStartLocation = PickerLocationId.Downloads };
                picker.FileTypeFilter.Add("*");
                StorageFolder folder = await picker.PickSingleFolderAsync();
                if (folder != null)
                {
                    StorageFile testfile = await folder.CreateFileAsync("MyApp.zip", CreationCollisionOption.ReplaceExisting);
                    DownloadOperation download = downloader.CreateDownload(source, testfile);
                    await download.StartAsync();
                }
            }
            catch (Exception ex)
            {
            }
            new ToastContentBuilder()
                .AddArgument("action", "viewConversation")
                .AddArgument("conversationId", 9813)
                .AddText("MyApp download complete")
                .AddText("Take a look!")
                .Show(); // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 5, your TFM must be net5.0-windows10.0.17763.0 or greater
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
