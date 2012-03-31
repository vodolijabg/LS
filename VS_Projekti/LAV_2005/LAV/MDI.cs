using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace LAV
{
    public partial class MDI : Form
    {
        public MDI()
        {
            InitializeComponent();
        }

        #region DaLiJeFormaOtvorena()
        private bool DaLiJeFormaOtvorena(string imeForme)
        {
            bool _otvorena = false;

            foreach (Form _forma in this.MdiChildren)
            {
                if (_forma.Name == imeForme)
                {
                    _otvorena = true;
                    break;
                }
            }

            return _otvorena;
        }
        #endregion

        #region AktivirajFormu()
        private void AktivirajFormu(string imeForme)
        {
            foreach (Form _forma in this.MdiChildren)
            {
                if (_forma.Name == imeForme)
                {
                    _forma.WindowState = FormWindowState.Maximized;
                    _forma.Activate();

                }
            }
        }
        #endregion

        private void MDI_Load(object sender, EventArgs e)
        {
            //podesi konekcioni string
            LavSettings _lavSettings = new LavSettings();

            DB.Konekcija.SQLServer = _lavSettings._Server;
            DB.Konekcija.SQLBaza = _lavSettings._Baza;
            DB.Konekcija.Autentifikacija = _lavSettings._Autentifikacija;
            DB.Konekcija.KorisnickoIme = _lavSettings._Korisnik;
            DB.Konekcija.Lozinka = _lavSettings._Lozinka;

            //proveri konekciju sa serverom
            SqlConnection _konekcijaSqlConnection = new SqlConnection();
            using (_konekcijaSqlConnection = DB.Konekcija.DajKonekciju())
            {
                try
                {
                    _konekcijaSqlConnection.Open();
                    _konekcijaSqlConnection.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Neuspešno povezivanje sa serverom. \n\nPoruka o grešci: \n" + ex.Message + "\n\nPodesite parametre konekcije na formi Konekcija.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }

        private void artikalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!DaLiJeFormaOtvorena("Artikal"))
                
            {
                Form artikal = new Artikal();
                artikal.MdiParent = this;
                artikal.WindowState = FormWindowState.Maximized;
                artikal.Show();
            }
            else
            {
                AktivirajFormu("Artikal");

            }
        }

        private void poslovniPartnerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!DaLiJeFormaOtvorena("PoslovniPartner"))
            {
                Form poslovniPartner = new PoslovniPartner();
                poslovniPartner.MdiParent = this;
                poslovniPartner.WindowState = FormWindowState.Maximized;
                poslovniPartner.Show();
            }
            else
            {
                AktivirajFormu("PoslovniPartner");
            }
        }

        private void mestoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!DaLiJeFormaOtvorena("Mesto"))
            {
                Form mesto = new Mesto();
                mesto.MdiParent = this;
                mesto.WindowState = FormWindowState.Maximized;
                mesto.Show();
            }
            else
            {
                AktivirajFormu("Mesto");

            }
        }

        private void radnikToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!DaLiJeFormaOtvorena("Radnik"))
            {
                Form radnik = new Radnik();
                radnik.MdiParent = this;
                radnik.WindowState = FormWindowState.Maximized;
                radnik.Show();
            }
            else
            {
                AktivirajFormu("Radnik");

            }
        }

        private void robaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!DaLiJeFormaOtvorena("Roba"))
            {
                Form roba = new Roba();
                roba.MdiParent = this;
                roba.WindowState = FormWindowState.Maximized;
                roba.Show();
            }
            else
            {
                AktivirajFormu("Roba");

            }
        }

        private void uslugeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!DaLiJeFormaOtvorena("Usluga"))
            {
                Form usluga = new Usluga();
                usluga.MdiParent = this;
                usluga.WindowState = FormWindowState.Maximized;
                usluga.Show();
            }
            else
            {
                AktivirajFormu("Usluga");

            }
        }

        private void korisnikProgramaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!DaLiJeFormaOtvorena("KorisnikPrograma"))
            {
                Form korisnikPrograma = new KorisnikPrograma();
                korisnikPrograma.MdiParent = this;
                korisnikPrograma.WindowState = FormWindowState.Maximized;
                korisnikPrograma.Show();
            }
            else
            {
                AktivirajFormu("KorisnikPrograma");

            }
        }

        private void konekcijaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (!DaLiJeFormaOtvorena("Konekcija"))
            //{
                Form konekcija = new Konekcija();
                //konekcija.MdiParent = this;
                //konekcija.WindowState = FormWindowState.Maximized;
                konekcija.StartPosition = FormStartPosition.CenterParent;
                konekcija.ShowDialog();
            //}
            //else
            //{
            //    AktivirajFormu("Konekcija");

            //}
        }

        private void automobilToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!DaLiJeFormaOtvorena("Automobil"))
            {
                Form automobil = new Automobil();
                automobil.MdiParent = this;
                automobil.WindowState = FormWindowState.Maximized;
                automobil.Show();
            }
            else
            {
                AktivirajFormu("Automobil");

            }
        }

        private void ponudaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!DaLiJeFormaOtvorena("Ponuda"))
            {
                Form ponuda = new Ponuda();
                ponuda.MdiParent = this;
                ponuda.WindowState = FormWindowState.Maximized;
                ponuda.Show();
            }
            else
            {
                AktivirajFormu("Ponuda");

            }
        }

    }
}