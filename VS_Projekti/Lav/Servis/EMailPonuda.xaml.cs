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
using System.IO;
using System.Net.Mail;
using System.Net;

namespace Servis
{
    /// <summary>
    /// Interaction logic for EMailPonuda.xaml
    /// </summary>
    public partial class EMailPonuda : Window
    {
        PonudaDetaljno ponudaDetaljno;
        DB.Ponuda ponuda;

        public EMailPonuda()
        {
            InitializeComponent();
        }

        public EMailPonuda(PonudaDetaljno ponudaDetaljno) : this()
        {
            this.ponudaDetaljno = ponudaDetaljno;
        }

        private void posaljiEmail()
        {
            
            try
            {

                Warning[] warnings;
                string[] streamids;
                string mimeType;
                string encoding;
                string filenameExtension;
                byte[] bytes = ponudaDetaljno.reportViewerPonuda.LocalReport.Render("PDF", null, out mimeType, out encoding,
                    out filenameExtension, out streamids, out warnings);

                //save to disk
                //using (FileStream fs = new FileStream(@"d:\output.pdf", FileMode.Create))
                //{ 
                //    fs.Write(bytes, 0, bytes.Length);
                //    fs.Close();
                //}



                //send via email
                MemoryStream s = new MemoryStream(bytes);
                s.Seek(0, SeekOrigin.Begin);

                string _partner = ponuda.ServisnaKnjizica.FizickoLice != null ? ponuda.ServisnaKnjizica.FizickoLice.Ime : ponuda.ServisnaKnjizica.PoslovniPartner.SkracenNaziv;
                string _imeFajla = "Ponuda " + ponuda.PonudaID.ToString() + " (" + _partner + ")" + ".pdf";

                Attachment a = new Attachment(s, _imeFajla);

                //MailMessage message = new MailMessage("oliver.bradonjic@gmail.com", "oliverbradonjic@yahoo.com",
                //                                "A report for you!", "Here is a report for you");

                MailMessage message = new MailMessage(new MailAddress(Konfiguracija.EMailAdresa, Konfiguracija.EMailIme), new MailAddress(comboBoxEmailAdresa.Text.Trim()));
                message.Subject = _imeFajla;
                message.Body = "Poštovani, \n\nNa Vaš zahtev dostavljamo ponudu.\n\n\nLAV AUTO d.o.o.\n\na. Jablanička 1 • 11030 Beograd\nt. +381 11 239 00 09\ne. office@lavauto.co.rs\nw. www.lavauto.rs";
                message.Bcc.Add(textBoxEmailAdresa.Text);
                message.Bcc.Add("predrag.gordic@lavauto.co.rs");

                message.Attachments.Add(a);

                //SmtpClient client = new SmtpClient();
                //client.Send(message);


                //SmtpClient ss = new SmtpClient("smtp.gmail.com", 587);
                SmtpClient ss = new SmtpClient("mail.lavauto.rs", 25);
                //ss.EnableSsl = true;
                ss.Timeout = 100000;
                ss.DeliveryMethod = SmtpDeliveryMethod.Network;
                ss.UseDefaultCredentials = false;
                //ss.Credentials = new NetworkCredential("oliver.bradonjic", "Zl@tib0r");
                ss.Credentials = new NetworkCredential(Konfiguracija.EMailAdresa, Konfiguracija.EMailLozinka);


                message.BodyEncoding = UTF8Encoding.UTF8;
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;


                ss.Send(message);

                MessageBox.Show("E-Mail je poslat.", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);


            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ponuda = (DB.Ponuda)ponudaDetaljno.gridPonuda.DataContext;

            List<string> _eMailAdresaLista = new List<string>();

            if (ponuda != null)
            {
                if (ponuda.ServisnaKnjizica.FizickoLice != null)
                {
                    if (ponuda.ServisnaKnjizica.FizickoLice.EMail != null)
                    {
                        _eMailAdresaLista.Add(ponuda.ServisnaKnjizica.FizickoLice.EMail);
                    }
                }
                else
                {
                    if (ponuda.ServisnaKnjizica.PoslovniPartner.EMail1 != null)
                    {
                        _eMailAdresaLista.Add(ponuda.ServisnaKnjizica.PoslovniPartner.EMail1);
                    }
                    if (ponuda.ServisnaKnjizica.PoslovniPartner.EMail2 != null)
                    {
                        _eMailAdresaLista.Add(ponuda.ServisnaKnjizica.PoslovniPartner.EMail2);
                    }
                }


            }

            comboBoxEmailAdresa.ItemsSource = _eMailAdresaLista;

            textBoxEmailAdresa.Text = Konfiguracija.EMailAdresa;
        }

        private void buttonPosalji_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                this.Cursor = Cursors.Wait;

                if (ponuda != null)
                {
                    
                    posaljiEmail();
                   
                }
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
    }
}
