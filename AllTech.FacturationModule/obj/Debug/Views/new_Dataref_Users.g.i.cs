﻿#pragma checksum "..\..\..\Views\new_Dataref_Users.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1B762AB8FAF49301F924261DB4EDA0F4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.296
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AllTech.FrameWork.Converter;
using AllTech.FrameWork.UIControl;
using Infragistics;
using Infragistics.Collections;
using Infragistics.Controls;
using Infragistics.Controls.Editors;
using Infragistics.Controls.Editors.Primitives;
using Infragistics.Controls.Grids;
using Infragistics.Controls.Grids.Primitives;
using Infragistics.Controls.Maps;
using Infragistics.Controls.Menus;
using Infragistics.Controls.Menus.Primitives;
using Infragistics.Controls.Primitives;
using Infragistics.Samples.Framework;
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


namespace AllTech.FacturationModule.Views {
    
    
    /// <summary>
    /// new_Dataref_Users
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class new_Dataref_Users : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 64 "..\..\..\Views\new_Dataref_Users.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid LayoutRoot;
        
        #line default
        #line hidden
        
        
        #line 209 "..\..\..\Views\new_Dataref_Users.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cmbprofile;
        
        #line default
        #line hidden
        
        
        #line 233 "..\..\..\Views\new_Dataref_Users.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnadPhoto;
        
        #line default
        #line hidden
        
        
        #line 317 "..\..\..\Views\new_Dataref_Users.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Infragistics.Controls.Grids.XamGrid userGrid;
        
        #line default
        #line hidden
        
        
        #line 325 "..\..\..\Views\new_Dataref_Users.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Infragistics.Controls.Grids.RowSelectorSettings rowSelectorSettings;
        
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
            System.Uri resourceLocater = new System.Uri("/AllTech.FacturationModule;component/views/new_dataref_users.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\new_Dataref_Users.xaml"
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
            this.LayoutRoot = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            
            #line 130 "..\..\..\Views\new_Dataref_Users.xaml"
            ((System.Windows.Controls.TextBox)(target)).KeyUp += new System.Windows.Input.KeyEventHandler(this.search_KeyUp);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 139 "..\..\..\Views\new_Dataref_Users.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.cmbprofile = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.btnadPhoto = ((System.Windows.Controls.Button)(target));
            
            #line 233 "..\..\..\Views\new_Dataref_Users.xaml"
            this.btnadPhoto.Click += new System.Windows.RoutedEventHandler(this.btnadPhoto_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.userGrid = ((Infragistics.Controls.Grids.XamGrid)(target));
            
            #line 318 "..\..\..\Views\new_Dataref_Users.xaml"
            this.userGrid.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.userGrid_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            case 7:
            this.rowSelectorSettings = ((Infragistics.Controls.Grids.RowSelectorSettings)(target));
            return;
            case 8:
            
            #line 398 "..\..\..\Views\new_Dataref_Users.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 735 "..\..\..\Views\new_Dataref_Users.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
