﻿#pragma checksum "..\..\VrstaUsluge.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "33A1584B9F73BFE1ABC60ADB4D5284FE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.4927
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AutoServis;
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


namespace AutoServis {
    
    
    /// <summary>
    /// VrstaUsluge
    /// </summary>
    public partial class VrstaUsluge : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\VrstaUsluge.xaml"
        internal System.Windows.Controls.ListBox listBoxLista;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\VrstaUsluge.xaml"
        internal System.Windows.Controls.Grid gridDetaljno;
        
        #line default
        #line hidden
        
        
        #line 49 "..\..\VrstaUsluge.xaml"
        internal System.Windows.Controls.TextBox textBoxID;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\VrstaUsluge.xaml"
        internal System.Windows.Controls.TextBox textBoxSifra;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\VrstaUsluge.xaml"
        internal System.Windows.Controls.TextBox textBoxNaziv;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\VrstaUsluge.xaml"
        internal System.Windows.Controls.Button buttonUnesi;
        
        #line default
        #line hidden
        
        
        #line 72 "..\..\VrstaUsluge.xaml"
        internal System.Windows.Controls.Button buttonIzmeni;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\VrstaUsluge.xaml"
        internal System.Windows.Controls.Button buttonPotvrdi;
        
        #line default
        #line hidden
        
        
        #line 74 "..\..\VrstaUsluge.xaml"
        internal System.Windows.Controls.Button buttonOdustani;
        
        #line default
        #line hidden
        
        
        #line 75 "..\..\VrstaUsluge.xaml"
        internal System.Windows.Controls.Button buttonObrisi;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\VrstaUsluge.xaml"
        internal System.Windows.Controls.Button buttonOsvezi;
        
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
            System.Uri resourceLocater = new System.Uri("/AutoServis;component/vrstausluge.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\VrstaUsluge.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 5 "..\..\VrstaUsluge.xaml"
            ((AutoServis.VrstaUsluge)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.listBoxLista = ((System.Windows.Controls.ListBox)(target));
            return;
            case 3:
            this.gridDetaljno = ((System.Windows.Controls.Grid)(target));
            return;
            case 4:
            this.textBoxID = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.textBoxSifra = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.textBoxNaziv = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.buttonUnesi = ((System.Windows.Controls.Button)(target));
            
            #line 71 "..\..\VrstaUsluge.xaml"
            this.buttonUnesi.Click += new System.Windows.RoutedEventHandler(this.buttonUnesi_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.buttonIzmeni = ((System.Windows.Controls.Button)(target));
            
            #line 72 "..\..\VrstaUsluge.xaml"
            this.buttonIzmeni.Click += new System.Windows.RoutedEventHandler(this.buttonIzmeni_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.buttonPotvrdi = ((System.Windows.Controls.Button)(target));
            
            #line 73 "..\..\VrstaUsluge.xaml"
            this.buttonPotvrdi.Click += new System.Windows.RoutedEventHandler(this.buttonPotvrdi_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.buttonOdustani = ((System.Windows.Controls.Button)(target));
            
            #line 74 "..\..\VrstaUsluge.xaml"
            this.buttonOdustani.Click += new System.Windows.RoutedEventHandler(this.buttonOdustani_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.buttonObrisi = ((System.Windows.Controls.Button)(target));
            
            #line 75 "..\..\VrstaUsluge.xaml"
            this.buttonObrisi.Click += new System.Windows.RoutedEventHandler(this.buttonObrisi_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.buttonOsvezi = ((System.Windows.Controls.Button)(target));
            
            #line 76 "..\..\VrstaUsluge.xaml"
            this.buttonOsvezi.Click += new System.Windows.RoutedEventHandler(this.buttonOsvezi_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}