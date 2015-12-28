using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;


// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Stream_Application
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }
        public async Task login_service()
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync((App.Current as App).ip_address + "api.php?action=EventLogin&code=" + pwd.Password + "");
            var responseString = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(responseString))
            {
                var root1 = JsonConvert.DeserializeObject<check_data_log>(responseString);
                this.DataContext = root1;
                if (root1.exist == false)
                {
                   

                }
                else
                {
                    string Event_data = root1.Id+"&"+root1.EventName+"&"+root1.EventLogo;
                    (App.Current as App).events = Event_data;
                    this.Frame.Navigate(typeof(Home), Event_data);
                }
            }
        }
        private async void log_border_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
            if(pwd.Password=="")
            {
                var messageDialog = new MessageDialog("Please enter the Event code");
              
                await messageDialog.ShowAsync();
            }
            else
            {
                login_service();
            }


                
            

          
            
        }

        private void pwd_GotFocus(object sender, RoutedEventArgs e)
        {
            Passtpinwatermark.Opacity = 0;
            pwd.Opacity = 100;
        }

        private void pwd_LostFocus(object sender, RoutedEventArgs e)
        {
            stpinchkpassword();
            if(pwd.Password.Count()<=0)
            {
                green_false.Visibility = Visibility.Collapsed;
                green_true.Visibility = Visibility.Collapsed;
            }
            
        }
        private void stpinchkpassword()
        {
            var passwordEmpty = string.IsNullOrEmpty(pwd.Password);
            Passtpinwatermark.Opacity = passwordEmpty ? 100 : 0;
            pwd.Opacity = passwordEmpty ? 0 : 100;
        }

        private void pwd_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (pwd.Password.Count() < 4)
            {
                green_false.Visibility = Visibility.Collapsed;
                green_true.Visibility = Visibility.Collapsed;
            }
            else
            {
                loginservice();
            }        
           
        }
        private async Task loginservice()
        {
            await PostKeyValueData();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {

            try
            {
                if (App.language_check_code != null)
                { }
                else
                {
                    App.language_check_code = "en";
                }
                language_change_code(App.language_check_code);

            }
            catch (Exception)
            {
              

            }


        }
        private void Timer_Tick(object sender, object e)
        {
            enter_code.Text = enter_code.Text.Substring(1) + enter_code.Text.Substring(0, 1);
        }

        public void language_change_code(string language_code)
        {
            Language_integration lang = new Language_integration();
            List<string> language_data=lang.lang_integration(language_code);
            enter_code.Text=language_data[0];
            Passtpinwatermark.Text = language_data[1];
        }

        public class check_data_log
        {
            public bool exist { get; set; }
            public string EventName { get; set; }
            public string Id { get; set; }
            public string EventLogo { get; set; }
           
        }
        private async Task PostKeyValueData()
        {
           
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync((App.Current as App).ip_address + "api.php?action=CheckEventCode&code="+pwd.Password+"");
            var responseString = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(responseString))
            {
                var root1 = JsonConvert.DeserializeObject<check_data_log>(responseString);
                this.DataContext = root1;
                if (root1.exist == false)
                {
                    green_false.Visibility = Visibility.Visible;
                    green_true.Visibility = Visibility.Collapsed;
             
                }
                else
                {
                  
                    green_true.Visibility = Visibility.Visible;
                    green_false.Visibility = Visibility.Collapsed;
                   
                   
                }
            }

        

        }

        private void pl_lang_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.language_check_code = "pl";
            language_change_code(App.language_check_code);
        }

        private void en_lang_Tapped(object sender, TappedRoutedEventArgs e)
        {

            App.language_check_code = "en";
            language_change_code(App.language_check_code);
        }

        private void de_lang_Tapped(object sender, TappedRoutedEventArgs e)
        {
        
            App.language_check_code = "de";
            language_change_code(App.language_check_code);
        }


      
    }
}
