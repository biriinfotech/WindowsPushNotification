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
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Stream_Application
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Home : Page
    {
        public Home()
        {
            this.InitializeComponent();
        }
        string[] event_details;
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            try
            {
                if ((App.Current as App).stream != null)
                {
                    marquee.Text = (App.Current as App).stream;
                    marquee_effect();
                }
                else
                {
                    marquee.Text = "";
                }
                string events = (App.Current as App).events;
                event_details = Regex.Split(events, "&");
                code_apply(App.language_check_code);
              
                change_selection();
              set_details();
                
            }
            catch (Exception EX)
            {
               // marquee.Text = EX.Message;
               
            }
           

        }
        public void marquee_effect()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(.08);
            timer.Tick += this.Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, object e)
        {
            marquee.Text = marquee.Text.Substring(1) + marquee.Text.Substring(0, 1);
        }
     
        public void code_apply(string value)
        {
            Language_integration lang = new Language_integration();
            List<string> language_data = lang.lang_integration(value);
            stream_list.Text=language_data[2];
            news_text.Text=language_data[3];
            question_text.Text=language_data[4];
            document_text.Text=language_data[5];
            sponsor_text.Text=language_data[6];
            fb_menu.Text=language_data[9];
            help.Text=language_data[8];
            logout.Text=language_data[7];

        }

      
        public void set_details()
        {
           
           event_name.Text = event_details[1];
           string url="http://217.149.242.190/events/events/"+event_details[2];
           event_logo.Source = new BitmapImage(new Uri(url, UriKind.Absolute));
            
            
          
        }
        public void change_selection()
        {
            switch (App.val)
            {
                case 1:
                    stram_img.Visibility = Visibility.Visible;
                    news_img.Visibility = Visibility.Collapsed;
                    doc_img.Visibility = Visibility.Collapsed;
                    ques_img.Visibility = Visibility.Collapsed;
                    spon_img.Visibility = Visibility.Collapsed;
                    break;
                case 2:
                    news_img.Visibility = Visibility.Visible;
                    doc_img.Visibility = Visibility.Collapsed;
                    ques_img.Visibility = Visibility.Collapsed;
                    spon_img.Visibility = Visibility.Collapsed;
                    stram_img.Visibility = Visibility.Collapsed;
                    break;
                case 3:
                    ques_img.Visibility = Visibility.Visible;
                    news_img.Visibility = Visibility.Collapsed;
                    doc_img.Visibility = Visibility.Collapsed;
                    spon_img.Visibility = Visibility.Collapsed;
                    stram_img.Visibility = Visibility.Collapsed;
                    break;
                case 4:
                    doc_img.Visibility = Visibility.Visible;
                    news_img.Visibility = Visibility.Collapsed;
                    ques_img.Visibility = Visibility.Collapsed;
                    spon_img.Visibility = Visibility.Collapsed;
                    stram_img.Visibility = Visibility.Collapsed;
                    break;
                case 5:
                    spon_img.Visibility = Visibility.Visible;
                    news_img.Visibility = Visibility.Collapsed;
                    doc_img.Visibility = Visibility.Collapsed;
                    ques_img.Visibility = Visibility.Collapsed;
                    stram_img.Visibility = Visibility.Collapsed;
                    break;
                default:
                    spon_img.Visibility = Visibility.Collapsed;
                    news_img.Visibility = Visibility.Collapsed;
                    doc_img.Visibility = Visibility.Collapsed;
                    ques_img.Visibility = Visibility.Collapsed;
                    stram_img.Visibility = Visibility.Collapsed;
                    break;


            }
        }

        private void Stream_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.val = 1;
            this.Frame.Navigate(typeof(stream), null);
        }

        private void News_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.val = 2;
            this.Frame.Navigate(typeof(News), null);
        }

        private void Question_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.val = 3;
            this.Frame.Navigate(typeof(Question), null);
        }

        private void Document_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.val = 4;
            this.Frame.Navigate(typeof(Document_page), null);
        }

        private void Sponsor_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.val = 5;
            this.Frame.Navigate(typeof(Sponsors), null);

        }

        private void back_press_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Home), null);
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

        private void menu_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!logincontrol1.IsOpen)
            {
                logincontrol1.HorizontalOffset = Window.Current.Bounds.Width - 330;
                logincontrol1.VerticalOffset = Window.Current.Bounds.Height - 748;
                logincontrol1.IsOpen = true;
            }
        }

        private void logout_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.language_check_code = null;
            App.val = 0;
            (App.Current as App).events = null;
            (App.Current as App).stream = null;
            this.Frame.Navigate(typeof(MainPage), null);
        }

        private void help_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Help), null);
        }
    }
}
