﻿#pragma checksum "..\..\BodDetaljno.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1B151D0F3C37B7DE1C39858B93F83906"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Servis;
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
    /// BodDetaljno
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class BodDetaljno : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\BodDetaljno.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid gridBod;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\BodDetaljno.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox checkBoxGenerisiSifru;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\BodDetaljno.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxID;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\BodDetaljno.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxSifra;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\BodDetaljno.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxNaziv;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\BodDetaljno.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxVrednost;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\BodDetaljno.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonSacuvajINovi;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\BodDetaljno.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonSacuvaj;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\BodDetaljno.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonSacuvajIZatvori;
        
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
            System.Uri resourceLocater = new System.Uri("/Servis;component/boddetaljno.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\BodDetaljno.xaml"
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
            this.gridBod = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.checkBoxGenerisiSifru = ((System.Windows.Controls.CheckBox)(target));
            return;
            case 3:
            this.textBoxID = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.textBoxSifra = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.textBoxNaziv = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.textBoxVrednost = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.buttonSacuvajINovi = ((System.Windows.Controls.Button)(target));
            
            #line 61 "..\..\BodDetaljno.xaml"
            this.buttonSacuvajINovi.Click += new System.Windows.RoutedEventHandler(this.buttonSacuvajINovi_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.buttonSacuvaj = ((System.Windows.Controls.Button)(target));
            
            #line 62 "..\..\BodDetaljno.xaml"
            this.buttonSacuvaj.Click += new System.Windows.RoutedEventHandler(this.buttonSacuvaj_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.buttonSacuvajIZatvori = ((System.Windows.Controls.Button)(target));
            
            #line 63 "..\..\BodDetaljno.xaml"
            this.buttonSacuvajIZatvori.Click += new System.Windows.RoutedEventHandler(this.buttonSacuvajIZatvori_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
