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
using System.Windows.Shapes;
using Microsoft.Reporting.WinForms;
using System.Net.Mail;
using System.Net;

namespace Servis
{
    /// <summary>
    /// Interaction logic for EMail.xaml
    /// </summary>
    public partial class EMail : Window
    {
        Podesavanja podesavanja;

        public EMail()
        {
            InitializeComponent();
        }

        public EMail(Podesavanja podesavanja) : this()
        {
            this.podesavanja = podesavanja;
        }

        private void buttonTest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Wait;

                //MailMessage message = new MailMessage("oliver.bradonjic@gmail.com", "oliver.bradonjic@gmail.com",
                //                                "Test poruka!", "Nalog je uspešno konfigurisan");
                MailMessage message = new MailMessage(new MailAddress(textBoxEmailAdresa.Text, textBoxIme.Text), new MailAddress(textBoxEmailAdresa.Text));
                message.Subject = "Test poruka!";
                message.Body = "Nalog je uspešno konfigurisan.";                
                message.Bcc.Add(textBoxEmailAdresa.Text);

                //SmtpClient ss = new SmtpClient("smtp.gmail.com", 587);
                SmtpClient ss = new SmtpClient("mail.lavauto.rs", 25);
                
                //ss.EnableSsl = true;
                ss.Timeout = 100000;
                ss.DeliveryMethod = SmtpDeliveryMethod.Network;
                ss.UseDefaultCredentials = false;

                //ss.Credentials = new NetworkCredential("oliver.bradonjic", "Zl@tib0r");
                ss.Credentials = new NetworkCredential(textBoxEmailAdresa.Text, textBoxLozinka.Password);


                message.BodyEncoding = UTF8Encoding.UTF8;
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

                ss.Send(message);

                MessageBox.Show("Test mail je poslat.\nUkoliko ste dobili poruku, nalog je konfigurisan.\nU suprotnom - obratite se administratoru!", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        private void buttonSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            Konfiguracija.EMailAdresa = textBoxEmailAdresa.Text;
            Konfiguracija.EMailIme = textBoxIme.Text;
            Konfiguracija.EMailLozinka = textBoxLozinka.Password;

            if (podesavanja != null)
            {
                podesavanja.textBoxEMail.Text = Konfiguracija.EMailAdresa;
            }

            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBoxEmailAdresa.Text = Konfiguracija.EMailAdresa;
            textBoxIme.Text = Konfiguracija.EMailIme;
            textBoxLozinka.Password = Konfiguracija.EMailLozinka;
        }
    }
}
