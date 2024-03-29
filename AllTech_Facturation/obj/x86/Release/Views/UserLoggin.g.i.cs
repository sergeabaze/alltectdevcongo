﻿#pragma checksum "..\..\..\..\Views\UserLoggin.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "792C8A5A8B036D96D58DA6D00A550759"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AdornedControl;
using AllTech.FrameWork.Converter;
using AllTech.FrameWork.UIControl;
using AllTech.FrameWork.Utils;
using AllTech.FrameWork.ValidationRules;
using AllTech.FrameWork.Views;
using Microsoft.Practices.Prism.Commands;
using RootLibrary.WPF.Localization;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace AllTech_Facturation.Views {
    
    
    /// <summary>
    /// UserLoggin
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class UserLoggin : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 85 "..\..\..\..\Views\UserLoggin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordBox;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\..\Views\UserLoggin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLoggin;
        
        #line default
        #line hidden
        
        
        #line 133 "..\..\..\..\Views\UserLoggin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar progressBar;
        
        #line default
        #line hidden
        
        
        #line 135 "..\..\..\..\Views\UserLoggin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbInfos;
        
        #line default
        #line hidden
        
        
        #line 137 "..\..\..\..\Views\UserLoggin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblWarning;
        
        #line default
        #line hidden
        
        
        #line 138 "..\..\..\..\Views\UserLoggin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox textBox1;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AllTech_Facturation;component/views/userloggin.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\UserLoggin.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 15 "..\..\..\..\Views\UserLoggin.xaml"
            ((AllTech_Facturation.Views.UserLoggin)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.PasswordBox = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 3:
            this.btnLoggin = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.progressBar = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 5:
            this.lbInfos = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.lblWarning = ((System.Windows.Controls.Label)(target));
            return;
            case 7:
            this.textBox1 = ((System.Windows.Controls.PasswordBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

