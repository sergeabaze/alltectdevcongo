﻿#pragma checksum "..\..\..\Views\Admin.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "543D931B96532D3D08CCE94987AD2ED21DE3499B"
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
using Infragistics;
using Infragistics.Collections;
using Infragistics.Controls;
using Infragistics.Controls.Editors;
using Infragistics.Controls.Editors.Primitives;
using Infragistics.Controls.Grids;
using Infragistics.Controls.Grids.Primitives;
using Infragistics.Controls.Interactions;
using Infragistics.Controls.Interactions.Primitives;
using Infragistics.Controls.Maps;
using Infragistics.Controls.Menus;
using Infragistics.Controls.Menus.Primitives;
using Infragistics.Controls.Primitives;
using Infragistics.Samples.Framework;
using Infragistics.Samples.Shared.Controls;
using Infragistics.Themes;
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
    /// Admin
    /// </summary>
    public partial class Admin : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\Views\Admin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ToolBar toolbAdmin;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\Views\Admin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dateFrom;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\..\Views\Admin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker dateTo;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\..\Views\Admin.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Infragistics.Controls.Grids.XamGrid GridHistorique;
        
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
            System.Uri resourceLocater = new System.Uri("/AllTech.FacturationModule;component/views/admin.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\Admin.xaml"
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
            this.toolbAdmin = ((System.Windows.Controls.ToolBar)(target));
            return;
            case 2:
            this.dateFrom = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 3:
            this.dateTo = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 4:
            this.GridHistorique = ((Infragistics.Controls.Grids.XamGrid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
