﻿#pragma checksum "..\..\..\..\Views\Modal\UsersProfileViews.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9D11A0D5C7C7348EAD11C19308ECB6531CD8AE03"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AllTech.FrameWork.Converter;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Controls;
using Infragistics.Windows.DockManager;
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


namespace AllTech.FacturationModule.Views.Modal {
    
    
    /// <summary>
    /// UsersProfileViews
    /// </summary>
    public partial class UsersProfileViews : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\..\Views\Modal\UsersProfileViews.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Infragistics.Windows.DockManager.XamDockManager dockManager;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\Views\Modal\UsersProfileViews.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Infragistics.Windows.DockManager.ContentPane document1;
        
        #line default
        #line hidden
        
        
        #line 178 "..\..\..\..\Views\Modal\UsersProfileViews.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Infragistics.Windows.DockManager.ContentPane rightEdgeDock;
        
        #line default
        #line hidden
        
        
        #line 242 "..\..\..\..\Views\Modal\UsersProfileViews.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Infragistics.Windows.DockManager.ContentPane rightEdgeBack;
        
        #line default
        #line hidden
        
        
        #line 243 "..\..\..\..\Views\Modal\UsersProfileViews.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid profileVuesGrid;
        
        #line default
        #line hidden
        
        
        #line 324 "..\..\..\..\Views\Modal\UsersProfileViews.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Infragistics.Windows.DockManager.ContentPane rightEdgeLast;
        
        #line default
        #line hidden
        
        
        #line 325 "..\..\..\..\Views\Modal\UsersProfileViews.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid profileSousVuesGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/AllTech.FacturationModule;component/views/modal/usersprofileviews.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Views\Modal\UsersProfileViews.xaml"
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
            this.dockManager = ((Infragistics.Windows.DockManager.XamDockManager)(target));
            return;
            case 2:
            this.document1 = ((Infragistics.Windows.DockManager.ContentPane)(target));
            return;
            case 3:
            this.rightEdgeDock = ((Infragistics.Windows.DockManager.ContentPane)(target));
            return;
            case 4:
            
            #line 196 "..\..\..\..\Views\Modal\UsersProfileViews.xaml"
            ((System.Windows.Controls.ComboBox)(target)).SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBox_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.rightEdgeBack = ((Infragistics.Windows.DockManager.ContentPane)(target));
            return;
            case 6:
            this.profileVuesGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 246 "..\..\..\..\Views\Modal\UsersProfileViews.xaml"
            this.profileVuesGrid.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.profileVuesGrid_MouseDown);
            
            #line default
            #line hidden
            return;
            case 7:
            this.rightEdgeLast = ((Infragistics.Windows.DockManager.ContentPane)(target));
            return;
            case 8:
            this.profileSousVuesGrid = ((System.Windows.Controls.DataGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

