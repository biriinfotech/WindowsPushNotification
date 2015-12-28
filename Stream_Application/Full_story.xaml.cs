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
    public sealed partial class Full_story : Page
    {
        public Full_story()
        {
            this.InitializeComponent();
        }
        string[] event_details, news_detals;
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            try
            {
                string events = (App.Current as App).events;
                event_details = Regex.Split(events, "&");
                string news_data = e.Parameter as string;
                news_detals = Regex.Split(news_data, "&");
                title.Text = news_detals[1];
                full_data.Text = news_detals[0];


            }
            catch (Exception EX)
            {


            }


        }

        private void back_press_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(News), null);
        }
    }
}
