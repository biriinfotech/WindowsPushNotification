using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources.Core;

namespace Stream_Application
{
    class Language_integration
    {
        List<string> return_data;
        public List<string> lang_integration(string data)
        {
                    ResourceContext ctx = new Windows.ApplicationModel.Resources.Core.ResourceContext();
                   ctx.Languages = new string[] { data };
                    ResourceMap rmap = ResourceManager.Current.MainResourceMap.GetSubtree("Resources");
            ////login_data
                   string Enter_code_text = rmap.GetValue("LoginScreen_EnterCode", ctx).ValueAsString;
                   string pass_code_text = rmap.GetValue("LoginScreen_Code", ctx).ValueAsString;
            ////menu_data
            string Stream_text = rmap.GetValue("MenuScreen_Stream", ctx).ValueAsString;
                   string News_text = rmap.GetValue("MenuScreen_News", ctx).ValueAsString;
            string question_text = rmap.GetValue("MenuScreen_Question", ctx).ValueAsString;
                   string Doc_text = rmap.GetValue("MenuScreen_Documents", ctx).ValueAsString;
             string sponsor_text = rmap.GetValue("MenuScreen_Sponsor", ctx).ValueAsString;
            ///menu_list_data
             string log_text = rmap.GetValue("MenuScreen_Logout", ctx).ValueAsString;
             string help_text = rmap.GetValue("MenuScreen_Help", ctx).ValueAsString;
            string facebook_text = rmap.GetValue("MenuScreen_Facebook", ctx).ValueAsString;
            /////stream_data
            string stream_header_text = rmap.GetValue("StreamScreen_Title", ctx).ValueAsString;
            ///news_data
            string news_header_text = rmap.GetValue("NewsScreen_Title", ctx).ValueAsString;
             string news_full_story = rmap.GetValue("NewsScreen_FullStory", ctx).ValueAsString;
            //question_data
             string ques_header_text = rmap.GetValue("QuestionScreen_Title", ctx).ValueAsString;
             string ques_send_title = rmap.GetValue("QuestionScreen_Send", ctx).ValueAsString;
             string ques_sended_text = rmap.GetValue("QuestionScreen_Sended", ctx).ValueAsString;
            ///doc_data
            string doc_header_text = rmap.GetValue("DocumentScreen_Title", ctx).ValueAsString;
            ///spons_data
            string spons_header_text = rmap.GetValue("SponsorScreen_Title", ctx).ValueAsString;
            string spons_add_text = rmap.GetValue("SponsorScreen_AddSponsor", ctx).ValueAsString;
            ///help_data
            string help_title_text = rmap.GetValue("Help_title", ctx).ValueAsString;
            string help_full_data = rmap.GetValue("Help_full_data", ctx).ValueAsString;
            return_data = new List<string>();               
            return_data.Add(Enter_code_text);//0
            return_data.Add(pass_code_text);//1
            return_data.Add(Stream_text);//2
            return_data.Add(News_text);//3
            return_data.Add(question_text);//4
            return_data.Add(Doc_text);//5
            return_data.Add(sponsor_text);//6
            return_data.Add(log_text);//7
            return_data.Add(help_text);//8
            return_data.Add(facebook_text);//9
            return_data.Add(stream_header_text);//10
            return_data.Add(news_header_text);//11
            return_data.Add(news_full_story);//12
            return_data.Add(ques_header_text);//13
            return_data.Add(ques_send_title);//14
            return_data.Add(ques_sended_text);//15
            return_data.Add(doc_header_text);//16
            return_data.Add(spons_header_text);//17
            return_data.Add(spons_add_text);//18
            return_data.Add(help_title_text);//19
            return_data.Add(help_full_data);//20
               return return_data;
        }

        

    }
}
