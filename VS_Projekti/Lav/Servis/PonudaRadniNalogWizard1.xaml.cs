using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Servis
{
    /// <summary>
    /// Interaction logic for CarobnjakPonudaRadniNalogStrana1.xaml
    /// </summary>
    public partial class PonudaRadniNalogWizard1 : Page
    {
        DB.RadniNalog radniNalog;
        DB.Ponuda ponuda;

        

        public PonudaRadniNalogWizard1(Servis.PonudaDetaljno ponudaDetaljno)
        {
            InitializeComponent();


            this.ponuda = (DB.Ponuda)ponudaDetaljno.gridPonuda.DataContext;
            radniNalog = new DB.RadniNalog
            {
                KorisnikProgramaID = ponuda.KorisnikProgramaID,
                ServisnaKnjizica = ponuda.ServisnaKnjizica,
                RadnikID = App.Radnik.RadnikID
            };

            gridRadniNalog.DataContext = radniNalog;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void buttonNazad_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonDalje_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int _kilometraza;

                if (textBoxKilometraza.Text.Trim() != "" && !Int32.TryParse(textBoxKilometraza.Text, out _kilometraza))
                {
                    Dijalog _dialog = new Dijalog("Pogrešan format", "Unesi broj za polje Kilometraza.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return;
                }
                else
                {
                    radniNalog = new DB.RadniNalog
                    {
                        KorisnikProgramaID = ponuda.KorisnikProgramaID,
                        ServisnaKnjizica = ponuda.ServisnaKnjizica,
                        RadnikID = App.Radnik.RadnikID
                    };

                    if (textBoxKilometraza.Text.Trim() != "")
                    {
                        radniNalog.Kilometraza = Convert.ToInt32(textBoxKilometraza.Text.Trim());
                    }
                    if (textBoxRegistarskiBroj.Text.Trim() != "")
                    {
                        radniNalog.RegistarskiBroj = textBoxRegistarskiBroj.Text.Trim();
                    }
                    if (datePickerDatumRegistracije.SelectedDate != null)
                    {
                        radniNalog.DatumRegistracije = datePickerDatumRegistracije.SelectedDate;
                    }
                    if (textBoxNapomena.Text.Trim() != "")
                    {
                        radniNalog.Napomena = textBoxNapomena.Text.Trim();
                    }


                    PonudaRadniNalogWizard2 _strana2 = new PonudaRadniNalogWizard2(radniNalog, ponuda);
                    _strana2.Return += new ReturnEventHandler<string>(_strana2_Return);
                    this.NavigationService.Navigate(_strana2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void _strana2_Return(object sender, ReturnEventArgs<String> e)
        {
            string _s = e.Result;
        }

        private void buttonOtkazi_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }
    }
}
