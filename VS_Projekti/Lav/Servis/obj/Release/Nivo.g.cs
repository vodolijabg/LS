﻿#pragma checksum "..\..\Nivo.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "333CEAA7914E50F07A3D7FB221760ECC"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.225
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
    /// Nivo
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class Nivo : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 44 "..\..\Nivo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonDodaj;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\Nivo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonIzmeni;
        
        #line default
        #line hidden
        
        
        #line 50 "..\..\Nivo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonObrisi;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\Nivo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonOsvezi;
        
        #line default
        #line hidden
        
        
        #line 57 "..\..\Nivo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxTraziZa;
        
        #line default
        #line hidden
        
        
        #line 59 "..\..\Nivo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox comboBoxNivoKolone;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\Nivo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button buttonNadji;
        
        #line default
        #line hidden
        
        
        #line 69 "..\..\Nivo.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListView listViewNivo;
        
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
            System.Uri resourceLocater = new System.Uri("/Servis;component/nivo.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Nivo.xaml"
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
            
            #line 4 "..\..\Nivo.xaml"
            ((Servis.Nivo)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Page_Loaded);
            
            #line default
            #line hidden
            return;
            case 3:
            this.buttonDodaj = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\Nivo.xaml"
            this.buttonDodaj.Click += new System.Windows.RoutedEventHandler(this.buttonDodaj_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.buttonIzmeni = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\Nivo.xaml"
            this.buttonIzmeni.Click += new System.Windows.RoutedEventHandler(this.buttonIzmeni_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.buttonObrisi = ((System.Windows.Controls.Button)(target));
            
            #line 50 "..\..\Nivo.xaml"
            this.buttonObrisi.Click += new System.Windows.RoutedEventHandler(this.buttonObrisi_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.buttonOsvezi = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\Nivo.xaml"
            this.buttonOsvezi.Click += new System.Windows.RoutedEventHandler(this.buttonOsvezi_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.textBoxTraziZa = ((System.Windows.Controls.TextBox)(target));
            return;
            case 8:
            this.comboBoxNivoKolone = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 9:
            this.buttonNadji = ((System.Windows.Controls.Button)(target));
            
            #line 64 "..\..\Nivo.xaml"
            this.buttonNadji.Click += new System.Windows.RoutedEventHandler(this.buttonNadji_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.listViewNivo = ((System.Windows.Controls.ListView)(target));
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 2:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.Controls.Control.MouseDoubleClickEvent;
            
            #line 7 "..\..\Nivo.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.listViewItem_MouseDoubleClick);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}
