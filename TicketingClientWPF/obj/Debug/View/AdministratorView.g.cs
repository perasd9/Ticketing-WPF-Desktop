﻿#pragma checksum "..\..\..\View\AdministratorView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "75696AB02FF4C89661CD4CBBE876D828537FA21A371045639BFA434E964CB535"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

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
using TicketingClientWPF.View;
using TicketingClientWPF.ViewModel;


namespace TicketingClientWPF.View {
    
    
    /// <summary>
    /// AdministratorView
    /// </summary>
    public partial class AdministratorView : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 161 "..\..\..\View\AdministratorView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem searchReservationsMenu;
        
        #line default
        #line hidden
        
        
        #line 199 "..\..\..\View\AdministratorView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem searchEventsMenu;
        
        #line default
        #line hidden
        
        
        #line 206 "..\..\..\View\AdministratorView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem createEventMenu;
        
        #line default
        #line hidden
        
        
        #line 243 "..\..\..\View\AdministratorView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem searchUsersMenu;
        
        #line default
        #line hidden
        
        
        #line 250 "..\..\..\View\AdministratorView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem createUserMenu;
        
        #line default
        #line hidden
        
        
        #line 264 "..\..\..\View\AdministratorView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem odjaviSeMenu;
        
        #line default
        #line hidden
        
        
        #line 289 "..\..\..\View\AdministratorView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid mainGrid;
        
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
            System.Uri resourceLocater = new System.Uri("/TicketingClientWPF;component/view/administratorview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\View\AdministratorView.xaml"
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
            
            #line 15 "..\..\..\View\AdministratorView.xaml"
            ((TicketingClientWPF.View.AdministratorView)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 60 "..\..\..\View\AdministratorView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_MiniMaximize);
            
            #line default
            #line hidden
            return;
            case 3:
            
            #line 85 "..\..\..\View\AdministratorView.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.searchReservationsMenu = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 5:
            this.searchEventsMenu = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 6:
            this.createEventMenu = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 7:
            this.searchUsersMenu = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 8:
            this.createUserMenu = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 9:
            this.odjaviSeMenu = ((System.Windows.Controls.MenuItem)(target));
            return;
            case 10:
            this.mainGrid = ((System.Windows.Controls.Grid)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
