using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
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
    public sealed partial class Question : Page
    {
        public Question()
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
              
                questionservice();
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
            timer.Start();
        }
        private void Timer_Tick(object sender, object e)
        {
            marquee.Text = marquee.Text.Substring(1) + marquee.Text.Substring(0, 1);
        }
        private async Task questionservice()
        {
            await PostKeyValueData();
        }
        public class RootObject
        {
            //public List<Stream_data> stream_details { get; set; }

            public string id { get; set; }
            public string question { get; set; }
            public string answer1 { get; set; }
            public string answer2 { get; set; }
            public string answer3 { get; set; }
            public string answer4 { get; set; }
            public string event_id { get; set; }
            public string answered1 { get; set; }
            public string answered2 { get; set; }
            public string answered3 { get; set; }
            public string answered4 { get; set; }

        }

        public class ques_listitem : INotifyPropertyChanged
        {
            public string color { get; set; }
            public string ques_id { get; set; }
            public string ques_question { get; set; }
            public string ques_answer1 { get; set; }
            public string ques_answer2 { get; set; }
            public string ques_answer3 { get; set; }
            public string ques_answer4 { get; set; }
            public string ques_event_id { get; set; }
            public string ques_answered1 { get; set; }
            public string ques_answered2 { get; set; }
            public string ques_answered3 { get; set; }
            public string ques_answered4 { get; set; }



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

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync((App.Current as App).ip_address + "api.php?action=GetAllQuestions&eventId=" + event_details[0] + "");
            var responseString = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(responseString))
            {
                List<RootObject> root1 = JsonConvert.DeserializeObject<List<RootObject>>(responseString);
                this.DataContext = root1;
                List<ques_listitem> lst = new List<ques_listitem>();
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

                    lst.Add(new ques_listitem { ques_question = item.question, ques_answer1 = item.answer1, ques_answer2 = item.answer2, ques_answer3 = item.answer3, ques_answer4 = item.answer4, ques_answered1 = item.answered1, ques_answered2 = item.answered2, ques_answered3 = item.answered3, ques_answered4 = item.answered4, ques_id = item.id, ques_event_id = item.event_id, color = color_code });
                    c_code++;
                }
                Question_list.ItemsSource = lst;

            }


        }

        private void Question_list_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {


                if (e.AddedItems.Count > 0)
                {


                    ques_listitem myobject = (sender as ListBox).SelectedItem as ques_listitem;

                    try
                    {
                       
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
        public void code_apply(string value)
        {
            Language_integration lang = new Language_integration();
            List<string> language_data = lang.lang_integration(value);
            question_header_text.Text = language_data[13];
            help.Text = language_data[8];
            logout.Text = language_data[7];
            fb_menu.Text = language_data[9];
            send_button.Text = language_data[14];
            send_text=language_data[15];

        }
        public class user_question_deatails
        {
            public string ID_QUESTION { get; set; }
            public string answer { get; set; }
        }
        string user_ans;
        private void ans1_Checked(object sender, RoutedEventArgs e)
        {
            

            RadioButton rd= (RadioButton)sender;
            string result=rd.Content.ToString();
            ques_listitem obj = rd.DataContext as ques_listitem;
            string ids = obj.ques_id;

            user_ans = result;

            lst.Add(new user_question_deatails { ID_QUESTION = ids, answer = user_ans });
        }

        private void ans2_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rd = (RadioButton)sender;
            string result = rd.Content.ToString();
            ques_listitem obj=rd.DataContext as ques_listitem;
            string ids = obj.ques_id;
            user_ans = result;

            lst.Add(new user_question_deatails { ID_QUESTION = ids, answer = user_ans });
        }

        private void ans3_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rd = (RadioButton)sender;
            string result = rd.Content.ToString();
            ques_listitem obj = rd.DataContext as ques_listitem;
            string ids = obj.ques_id;
            user_ans = result;

            lst.Add(new user_question_deatails { ID_QUESTION = ids, answer = user_ans });
        }
        List<user_question_deatails> lst = new List<user_question_deatails>();
        private void ans4_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton rd = (RadioButton)sender;
            string result = rd.Content.ToString();
            ques_listitem obj = rd.DataContext as ques_listitem;
            string ids = obj.ques_id;
            user_ans = result;

            lst.Add(new user_question_deatails { ID_QUESTION = ids, answer = user_ans });
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
        public class ques_log
        {
            public bool Success { get; set; }
          

        }
        string user_anser;
        private async void ans_send_Tapped(object sender, TappedRoutedEventArgs e)
        {


            foreach (var item in lst)
            {
                user_anser += item.ID_QUESTION + ":" + item.answer+"'";              
            }

            await PostKeyValueData112(user_anser);

           
         
           
        }
        string send_text;
        private async Task PostKeyValueData112(string ans)
        {
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync((App.Current as App).ip_address + "api.php?action=SendAnswers&answer=" + ans + "");
            var responseString = await response.Content.ReadAsStringAsync();
            if (!string.IsNullOrEmpty(responseString))
            {
                var root1 = JsonConvert.DeserializeObject<ques_log>(responseString);
                this.DataContext = root1;
                if (root1.Success == false)
                {
                }
                else
                {
                     code_apply(App.language_check_code);
                     var messageDialog = new MessageDialog(send_text);
                    await messageDialog.ShowAsync();
                }
            }
        }
        private void help_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Help), null);
        }
    }
}
