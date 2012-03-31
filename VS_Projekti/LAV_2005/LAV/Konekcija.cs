using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Configuration;
using System.Data.SqlClient;

namespace LAV
{
    public partial class Konekcija : Form 

    {
        LavSettings _LavSettings = new LavSettings(); 

        public Konekcija()
        {
            InitializeComponent();

            Inicijalizuj();
        }

        #region Inicijalizuj()

        private void Inicijalizuj()
        {
            autentifikacijaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            autentifikacijaComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            autentifikacijaComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

        }

        #endregion

        private void testKonekcijaButton_Click(object sender, EventArgs e)
        {
            string _konekcioniString = "";

            if (autentifikacijaComboBox.SelectedItem.ToString() == "Windows")
            {
                _konekcioniString =
                            "Data Source=" + SQLServerTextBox.Text + ";" +
                            "Initial Catalog =" + SQLBazaTextBox.Text + ";" +
                            "Integrated Security = True;" +
                            "Persist Security Info = False";

            }
            else
            {
                _konekcioniString =
                            "Data Source=" + SQLServerTextBox.Text + ";" +
                            "Initial Catalog =" + SQLBazaTextBox.Text + ";" +
                            "Integrated Security = False;" +
                            "User ID=" + korisnikTextBox.Text + ";" +
                            "Password=" + lozinkaTextBox.Text + ";" +
                            "Persist Security Info = False";
            }

            SqlConnection _konekcija =  new SqlConnection(_konekcioniString);
                
            //proveri konekciju sa serverom
            using (_konekcija)
            {
                try
                {
                    this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                    _konekcija.Open();
                    _konekcija.Close();

                    MessageBox.Show("Test konekcije uspešan.", "Informacija", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Neuspešno povezivanje sa serverom. \n\nPoruka o grešci: \n" + ex.Message + "\n\nPodesite parametre konekcije na formi Konekcija.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    this.Cursor = System.Windows.Forms.Cursors.Default;

                }
            }
        }

        private void Konekcija_Load(object sender, EventArgs e)
        {
            autentifikacijaComboBox.SelectedIndex = 0;

            SQLServerTextBox.Text = _LavSettings._Server;
            SQLBazaTextBox.Text = _LavSettings._Baza;
            autentifikacijaComboBox.SelectedIndex = (int)autentifikacijaComboBox.FindString(_LavSettings._Autentifikacija);
            korisnikTextBox.Text = _LavSettings._Korisnik;
            lozinkaTextBox.Text = _LavSettings._Lozinka;

        }

        private void autentifikacijaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (autentifikacijaComboBox.SelectedIndex.Equals(0))
            {
                korisnikTextBox.Text = "";
                korisnikTextBox.Enabled = false;
                lozinkaTextBox.Text = "";
                lozinkaTextBox.Enabled = false;
            }
            else 
            {
                korisnikTextBox.Enabled = true;
                lozinkaTextBox.Enabled = true;
            }
        }

        private void sacuvajButton_Click(object sender, EventArgs e)
        {
            DB.Konekcija.SQLServer = SQLServerTextBox.Text;
            DB.Konekcija.SQLBaza = SQLBazaTextBox.Text;
            DB.Konekcija.Autentifikacija = autentifikacijaComboBox.SelectedItem.ToString();
            DB.Konekcija.KorisnickoIme = korisnikTextBox.Text;
            DB.Konekcija.Lozinka = lozinkaTextBox.Text;

            _LavSettings._Server = SQLServerTextBox.Text ;
            _LavSettings._Baza = SQLBazaTextBox.Text;
            _LavSettings._Autentifikacija = autentifikacijaComboBox.SelectedItem.ToString();
            _LavSettings._Korisnik = korisnikTextBox.Text;
            _LavSettings._Lozinka = lozinkaTextBox.Text;

            _LavSettings.Save();
            this.Close();
        }

        
    }
}