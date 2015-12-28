using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
    public sealed partial class stream : Page
    {
        public stream()
        {
            this.InitializeComponent();
        }
        private void back_press_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Home), null);
        }

        private async Task streamservice()
        {
            await PostKeyValueData();
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
                streamservice();
                code_apply(App.language_check_code);
               
            }
            catch (Exception)
            {


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
            help.Text = language_data[8];
            logout.Text = language_data[7];
            fb_menu.Text = language_data[9];

        }

        public class RootObject
        {
            //public List<Stream_data> stream_details { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string url { get; set; }
            public string event_id { get; set; }
            public string info { get; set; }

        }
      
        public class Stream_listitem : INotifyPropertyChanged
        {
            public string color { get; set; }
            public string stream_id { get; set; }
            public string stream_name { get; set; }
            public string stream_url { get; set; }
            public string stream_event_id { get; set; }
            public string stream_info { get; set; }


            public event PropertyChangedEventHandler PropertyChanged;
            void RaiseEvent(string propertyName)
            {
                if (PropertyChanged != null)
                {
                    PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
                }
            }


        }
        public class RootObject112
        {
            public List<string> links { get; set; }
        }
        public class urls
        {
            public string url { get; set; }

        }
        List<urls> lst;
        private async Task parsepls()
        {

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://silentstream.pl/events/api.php?action=ParsePls&url="+filename);
            var responseString = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(responseString))
            {
                var root1 = JsonConvert.DeserializeObject<RootObject112>(responseString);
                this.DataContext = root1;
          
              for (int i = 0; i < root1.links.Count(); i++)
              {
                  MediaElement rootMediaElement;
                  DependencyObject rootGrid = VisualTreeHelper.GetChild(Window.Current.Content, 0);
                  rootMediaElement = (MediaElement)VisualTreeHelper.GetChild(rootGrid, 0);
                  rootMediaElement.Source = new Uri(root1.links[i], UriKind.Absolute);
                  rootMediaElement.Play();
              }
                
            }


        }

        private async Task PostKeyValueData()
        {

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync((App.Current as App).ip_address + "api.php?action=GetAllStreams&eventId=" + event_details[0] + "");
            var responseString = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(responseString))
            {
                List<RootObject> root1 = JsonConvert.DeserializeObject<List<RootObject>>(responseString);
                this.DataContext = root1;
                List<Stream_listitem> lst = new List<Stream_listitem>();
                int c_code = 0;
                string color_code;
                

                foreach (var item in root1)
                {
                    if(c_code%2==0)
                    {
                        color_code = "#333333";
                    }
                    else
                    {
                        color_code = "#1a1a1a";
                    }


               lst.Add(new Stream_listitem { stream_name = item.name, stream_event_id = item.event_id, stream_info = item.info, stream_url = item.url, stream_id = item.id, color = color_code });
                    c_code++;
                }
               stream_list.ItemsSource = lst;
               
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
        string filename;
        private void stream_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {


                if (e.AddedItems.Count > 0)
                {

                    stream_list.Background = new SolidColorBrush( Windows.UI.Colors.Transparent);
                    Stream_listitem myobject = (sender as ListBox).SelectedItem as Stream_listitem;
                    
                    try
                 {

                        (App.Current as App).stream = myobject.stream_name.PadRight(170,' ');
                        marquee.Text = myobject.stream_name.PadRight(170, ' ');

                        marquee_effect();
                         filename = myobject.stream_url;
                           
                           parsepls();
                            
                          
                        
  
                            
                   
                       
                       

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
