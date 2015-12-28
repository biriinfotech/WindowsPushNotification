using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Stream_Application
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class webview : Page
    {
        public webview()
        {
            this.InitializeComponent();
        }
        string[] event_details;
          protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            try
            {
                string events = (App.Current as App).events;
                event_details = Regex.Split(events, "&");
                pgbar1.Visibility = Visibility.Visible;
                string url = e.Parameter as string;
                Uri msgview = new Uri(url);
                webview1.Navigate(msgview);
                pgbar1.IsIndeterminate = true;
                webview1.LoadCompleted += new Windows.UI.Xaml.Navigation.LoadCompletedEventHandler(webview1_LoadCompleted);

            }
            catch (Exception)
            {


            }


        }
          public delegate void LoadCompletedEventHandler(object sender, NavigationEventArgs e);
          private void webview1_LoadCompleted(object sender, NavigationEventArgs e)
          {
              pgbar1.IsIndeterminate = false;
              pgbar1.Visibility = Visibility.Collapsed;
          }
          private void Grid_Tapped(object sender, TappedRoutedEventArgs e)
          {
              show();
          }
          private void show()
          {
              if (TopAppBar.IsOpen)
                  TopAppBar.IsOpen = false;
              else TopAppBar.IsOpen = true;
          }

          private void back_press_Tapped(object sender, TappedRoutedEventArgs e)
          {
              this.Frame.Navigate(typeof(Sponsors), null);
          }

          private void Grid_Tapped_1(object sender, TappedRoutedEventArgs e)
          {
              show();
          }

        
    
    }
}
