using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class MyAppUWPDownload : Page, INotifyPropertyChanged
    {
            // TODO WTS: Set the URI of the page to show by default
            private const string DefaultUrl = "https://occoam.com/jpb/wp-content/uploads/2021/10/MyApp-UWP_2.1.0.0_Test-20211003T155636Z-001.zip";

            private Uri _source;

            public Uri Source
            {
                get { return _source; }
                set { Set(ref _source, value); }
            }

            private bool _isLoading;

            public bool IsLoading
            {
                get
                {
                    return _isLoading;
                }

                set
                {
                    if (value)
                    {
                        IsShowingFailedMessage = false;
                    }

                    Set(ref _isLoading, value);
                    IsLoadingVisibility = value ? Visibility.Visible : Visibility.Collapsed;
                }
            }

            private Visibility _isLoadingVisibility;

            public Visibility IsLoadingVisibility
            {
                get { return _isLoadingVisibility; }
                set { Set(ref _isLoadingVisibility, value); }
            }

            private bool _isShowingFailedMessage;

            public bool IsShowingFailedMessage
            {
                get
                {
                    return _isShowingFailedMessage;
                }

                set
                {
                    if (value)
                    {
                        IsLoading = false;
                    }

                    Set(ref _isShowingFailedMessage, value);
                    FailedMesageVisibility = value ? Visibility.Visible : Visibility.Collapsed;
                }
            }

            private Visibility _failedMesageVisibility;

            public Visibility FailedMesageVisibility
            {
                get { return _failedMesageVisibility; }
                set { Set(ref _failedMesageVisibility, value); }
            }

            private void OnNavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
            {
                IsLoading = false;
                OnPropertyChanged(nameof(IsBackEnabled));
                OnPropertyChanged(nameof(IsForwardEnabled));
            }

            private void OnNavigationFailed(object sender, WebViewNavigationFailedEventArgs e)
            {
                // Use `e.WebErrorStatus` to vary the displayed message based on the error reason
                IsShowingFailedMessage = true;
            }

            private void OnRetry(object sender, RoutedEventArgs e)
            {
                IsShowingFailedMessage = false;
                IsLoading = true;

                webView.Refresh();
            }

            public bool IsBackEnabled
            {
                get { return webView.CanGoBack; }
            }

            public bool IsForwardEnabled
            {
                get { return webView.CanGoForward; }
            }

            private void OnGoBack(object sender, RoutedEventArgs e)
            {
                webView.GoBack();
            }

            private void OnGoForward(object sender, RoutedEventArgs e)
            {
                webView.GoForward();
            }

            private void OnRefresh(object sender, RoutedEventArgs e)
            {
                webView.Refresh();
            }

            private async void OnOpenInBrowser(object sender, RoutedEventArgs e)
            {
                await Windows.System.Launcher.LaunchUriAsync(webView.Source);
            }

            public MyAppUWPDownload()
            {
                Source = new Uri(DefaultUrl);
                InitializeComponent();
                IsLoading = true;
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

            private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

