﻿

#pragma checksum "C:\Users\Atuls\documents\visual studio 2013\Projects\Stream_Application\Stream_Application\Document_page.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "58D8103A05C5E9F3FE5EEBF00E6ABE2E"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Stream_Application
{
    partial class Document_page : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 191 "..\..\Document_page.xaml"
                ((global::Windows.UI.Xaml.Controls.WebView)(target)).LoadCompleted += this.webview1_LoadCompleted;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 154 "..\..\Document_page.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.logout_Tapped;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 156 "..\..\Document_page.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.help_Tapped;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 106 "..\..\Document_page.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.doc_list_SelectionChanged;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 87 "..\..\Document_page.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.back_press_Tapped;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 90 "..\..\Document_page.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.menu_Tapped;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


