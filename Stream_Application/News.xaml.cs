using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Data.Json;
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
    public sealed partial class News : Page
    {
        public News()
        {
            this.InitializeComponent();
        }
        private void back_press_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Home), null);
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
                Newsservice();
                
               

            }
            catch (Exception EX)
            {
               

            }


        }
        public void marquee_effect()
        {
         DispatcherTimer timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(.08);
                timer.Tick += this.Timer_Tick;
                timer.Start();}
        private void Timer_Tick(object sender, object e)
        {
            marquee.Text = marquee.Text.Substring(1) + marquee.Text.Substring(0, 1);
        }
        private async Task Newsservice()
        {
            await PostKeyValueData();
        }
        
        public class RootObject
        {
           public string id { get; set; }
            public string title { get; set; }
            public string story { get; set; }
            public string event_id { get; set; }
        }

        public class news_listitem : INotifyPropertyChanged
        {
            public string color { get; set; }
            public string news_id { get; set; }
            public string news_title { get; set; }
            public string news_story { get; set; }
            public string news_event_id { get; set; }
          


            public event PropertyChangedEventHandler PropertyChanged;
            void RaiseEvent(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
                }
            }


        }
        private async Task PostKeyValueData()
        {
            List<news_listitem> lst = new List<news_listitem>();
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync((App.Current as App).ip_address + "api.php?action=GetAllNews&eventId=" + event_details[0] + "");
            var responseString = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(responseString))
            {
            
                List<RootObject> root1 = JsonConvert.DeserializeObject<List<RootObject>>(responseString);
                this.DataContext = root1;              
                int c_code = 0;
                string color_code;
                foreach (var item in root1)
                {
                    if (c_code % 2 == 0)
                    {
                        color_code = "#333333";
                    }
                    else
                    {
                        color_code = "#1a1a1a";
                    }

                    lst.Add(new news_listitem { news_title =item.title, news_story = item.story, news_id = item.id, news_event_id = item.event_id, color = color_code });
                    c_code++;
                }
                news_list.ItemsSource = lst;

            }


        }

        private void news_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (e.AddedItems.Count > 0)
                { 
                    try
                    {
                        news_listitem myobject = (sender as ListBox).SelectedItem as news_listitem;
                        string full_stor = myobject.news_story+"&"+myobject.news_title;
                        this.Frame.Navigate(typeof(Full_story), full_stor);

                    }
                    catch (Exception ex)
                    {
                       

                    }

                }
            }
            catch (Exception ex)
            {

            }

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
        public void code_apply(string value)
        {
            Language_integration lang = new Language_integration();
            List<string> language_data = lang.lang_integration(value);
            new_header_text.Text = language_data[11];
            help.Text = language_data[8];
            logout.Text = language_data[7];
            fb_menu.Text = language_data[9];
            

        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {

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
