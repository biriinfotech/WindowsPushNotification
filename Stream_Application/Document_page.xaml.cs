
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Data.Pdf;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Newtonsoft.Json;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Stream_Application
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Document_page : Page
    {
        public Document_page()
        {
            this.InitializeComponent();
        }
        private void back_press_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Home), null);
        }
        private async Task Docservice()
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
                Docservice();
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
            Doc_text.Text=language_data[16];
            help.Text = language_data[8];
            logout.Text = language_data[7];
            fb_menu.Text = language_data[9];

        }

        public class RootObject
        {
            
            public string id { get; set; }
            public string name { get; set; }
            public string path_file { get; set; }
            public string event_id { get; set; }

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
        private async Task PostKeyValueData()
        {
            List<Stream_listitem> lst = new List<Stream_listitem>();
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync((App.Current as App).ip_address + "api.php?action=GetAllDocuments&eventId=" + event_details[0] + "");
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
                        color_code = "#333333";
                    }

                    lst.Add(new Stream_listitem { stream_name = item.name, stream_event_id = item.event_id, stream_info = item.path_file, stream_id = item.id, color = color_code });
                    c_code++;
                }
                Document_list.ItemsSource = lst;

            }


        }
        private void doc_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {


                if (e.AddedItems.Count > 0)
                {


                    Stream_listitem myobject = (sender as ListBox).SelectedItem as Stream_listitem;

                    try
                    {
                        if (myobject.stream_info.Contains(".png"))
                        {
                            if (!logincontrol1.IsOpen)
                            {
                                string url = "http://217.149.242.190/events/documents/";
                                image_full_view.Source = new BitmapImage(new Uri(url + myobject.stream_info, UriKind.Absolute));
                                logincontrol1.IsOpen = true;
                            }
                        }
                        else
                        {
                            if (!logincontrol4.IsOpen)
                            {
                                pgbar1.Visibility = Visibility.Visible;
                                pgbar1.IsIndeterminate = true;
                                string url = "http://docs.google.com/gview?embedded=true&url=http://217.149.242.190/events/documents/";
                                Uri msgview = new Uri(url + myobject.stream_info);
                                 wbLogin.Navigate(msgview);
                                wbLogin.LoadCompleted += new Windows.UI.Xaml.Navigation.LoadCompletedEventHandler(webview1_LoadCompleted);
                                logincontrol4.IsOpen = true;
                            }
                        }
                  
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
        public delegate void LoadCompletedEventHandler(object sender, NavigationEventArgs e);
        private void webview1_LoadCompleted(object sender, NavigationEventArgs e)
        {
            pgbar1.IsIndeterminate = false;
            pgbar1.Visibility = Visibility.Collapsed;
        }
        private void menu_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (!logincontrol2.IsOpen)
            {
                logincontrol2.HorizontalOffset = Window.Current.Bounds.Width - 330;
                logincontrol2.VerticalOffset = Window.Current.Bounds.Height - 748;
                logincontrol2.IsOpen = true;
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
