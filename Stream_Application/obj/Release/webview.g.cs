﻿

#pragma checksum "C:\Users\Atuls\documents\visual studio 2013\Projects\Stream_Application\Stream_Application\webview.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F4B6465C8C22B462D3FE5E31D7873EC3"
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
    partial class webview : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 39 "..\..\webview.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.Grid_Tapped_1;
                 #line default
                 #line hidden
                #line 39 "..\..\webview.xaml"
                ((global::Windows.UI.Xaml.Controls.WebView)(target)).LoadCompleted += this.webview1_LoadCompleted;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 28 "..\..\webview.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.back_press_Tapped;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


