﻿#pragma checksum "..\..\PonudaWizard1Old.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "6D338AC6517DC2F2B041D546D61BCDB5"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
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
using System.Windows.Forms.Integration;
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


namespace Servis {
    
    
    /// <summary>
    /// PonudaWizard1
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class PonudaWizard1 : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 9 "..\..\PonudaWizard1Old.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridFizickoLice;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\PonudaWizard1Old.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxIme;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\PonudaWizard1Old.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxTelefon;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\PonudaWizard1Old.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonDalje;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\PonudaWizard1Old.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonOtkazi;
        
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
            System.Uri resourceLocater = new System.Uri("/Servis;component/ponudawizard1old.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\PonudaWizard1Old.xaml"
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
            
            #line 7 "..\..\PonudaWizard1Old.xaml"
            ((Servis.PonudaWizard1)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.gridFizickoLice = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.textBoxIme = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.textBoxTelefon = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.buttonDalje = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\PonudaWizard1Old.xaml"
            this.buttonDalje.Click += new System.Windows.RoutedEventHandler(this.buttonDalje_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.buttonOtkazi = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\PonudaWizard1Old.xaml"
            this.buttonOtkazi.Click += new System.Windows.RoutedEventHandler(this.buttonOtkazi_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
