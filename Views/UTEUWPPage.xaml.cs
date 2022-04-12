using Microsoft.Toolkit.Uwp.Notifications;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.BackgroundTransfer;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AppStore.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class UTEUWPPage : Page
    {
        public UTEUWPPage()
        {
            this.InitializeComponent();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
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
                var myappuri = new Uri("https://occoam.com/jpb/wp-content/uploads/UTEUWP_1.0.19044.194.zip");
                BackgroundDownloader downloader = new BackgroundDownloader();
                Uri source = myappuri;
                FolderPicker picker = new FolderPicker { SuggestedStartLocation = PickerLocationId.Downloads };
                picker.FileTypeFilter.Add("*");
                StorageFolder folder = await picker.PickSingleFolderAsync();
                if (folder != null)
                {
                    StorageFile testfile = await folder.CreateFileAsync("UTEUWP.zip", CreationCollisionOption.ReplaceExisting);
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
                .AddText("UltraTextEdit UWP download complete")
                .AddText("Take a look!")
                .Show(); // Not seeing the Show() method? Make sure you have version 7.0, and if you're using .NET 5, your TFM must be net5.0-windows10.0.17763.0 or greater
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
