﻿#pragma checksum "..\..\..\..\Views\UserLoggin.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "144FB3F160E6772717A416CF4F9DE760F5EFE3DD"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
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
using Infragistics;
using Infragistics.Collections;
using Infragistics.Controls;
using Infragistics.Controls.Charts;
using Infragistics.Controls.Editors;
using Infragistics.Controls.Grids;
using Infragistics.Controls.Interactions;
using Infragistics.Controls.Maps;
using Infragistics.Controls.Menus;
using Infragistics.Themes;
using Microsoft.Practices.Prism.Commands;
using Multilingue.Resources;
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
    public partial class UserLoggin : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 69 "..\..\..\..\Views\UserLoggin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbInfos;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\..\Views\UserLoggin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PasswordBox;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\..\..\Views\UserLoggin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox textBox1;
        
        #line default
        #line hidden
        
        
        #line 113 "..\..\..\..\Views\UserLoggin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Infragistics.Controls.Editors.XamComboEditor cmbDatabes;
        
        #line default
        #line hidden
        
        
        #line 117 "..\..\..\..\Views\UserLoggin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ProgressBar progressBar;
        
        #line default
        #line hidden
        
        
        #line 120 "..\..\..\..\Views\UserLoggin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblWarning;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\..\..\Views\UserLoggin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnLoggin;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
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
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 17 "..\..\..\..\Views\UserLoggin.xaml"
            ((AllTech_Facturation.Views.UserLoggin)(target)).Loaded += new System.Windows.RoutedEventHandler(this.UserControl_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lbInfos = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.PasswordBox = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 4:
            this.textBox1 = ((System.Windows.Controls.PasswordBox)(target));
            return;
            case 5:
            this.cmbDatabes = ((Infragistics.Controls.Editors.XamComboEditor)(target));
            
            #line 115 "..\..\..\..\Views\UserLoggin.xaml"
            this.cmbDatabes.SelectionChanged += new System.EventHandler<Infragistics.Controls.Editors.SelectionChangedEventArgs>(this.cmbDatabes_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.progressBar = ((System.Windows.Controls.ProgressBar)(target));
            return;
            case 7:
            this.lblWarning = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.btnLoggin = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

