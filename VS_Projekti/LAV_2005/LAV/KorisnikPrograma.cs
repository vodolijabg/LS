using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAV
{
    public partial class KorisnikPrograma : Form
    {
        DB.KorisnikPrograma DBKorisnikPrograma = new DB.KorisnikPrograma();
        DB.Mesto DBMesto = new DB.Mesto();

        public KorisnikPrograma()
        {
            InitializeComponent();

            Inicijalizuj();
        }

        #region Inicijalizuj()

        private void Inicijalizuj()
        {
            //Bindovanje Padajuce liste
            mestoComboBox.DataSource = korisnikProgramaDataSet.mestoDataTable;
            mestoComboBox.DisplayMember = "Naziv";
            mestoComboBox.ValueMember = "Mesto_ID";
            //
            mestoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            mestoComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            mestoComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //TabStop=false
            obrisiButton.TabStop = false;
            krajButton.TabStop = false;

            //TabIndex
            nazivTextBox.TabIndex = 0;
            adresaTextBox.TabIndex = 1;
            mestoComboBox.TabIndex = 2;
            telefonTextBox.TabIndex = 3;
            PIBTextBox.TabIndex = 4;
            ziroRacunTextBox.TabIndex = 5;

            potvrdiButton.TabIndex = 6;
            odustaniButton.TabIndex = 7;

            //MaxLenght
            nazivTextBox.MaxLength = 60;
            adresaTextBox.MaxLength = 50;
            telefonTextBox.MaxLength = 50;
            PIBTextBox.MaxLength = 15;
            ziroRacunTextBox.MaxLength = 50;

        }

        #endregion

        #region UStanje()

        private void UStanje(string stanje)
        {
            //mora ici pre dugmica
            nazivTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            adresaTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            mestoComboBox.Enabled = (!mestoComboBox.Items.Count.Equals(0)) && (stanje == "Unos") || (stanje == "Izmena");
            telefonTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            PIBTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            ziroRacunTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");

            unesiButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");
            izmeniButton.Enabled = (stanje == "Detaljno");
            potvrdiButton.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            odustaniButton.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            obrisiButton.Enabled = (stanje == "Detaljno");
            krajButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");


            if ((stanje == "Unos") || (stanje == "Izmena"))
            {
                nazivTextBox.Select();
            }
        }

        #endregion

        #region PrikaziKorisnikaProgramaDetaljno()

        private void PrikaziKorisnikaProgramaDetaljno()
        {
            if (!korisnikProgramaDataSet.vwKorisnikPrograma.Rows.Count.Equals(0))
            {               
                korisnikPrograma_IDTextBox.Text = korisnikProgramaDataSet.vwKorisnikPrograma.Rows[0]["KorisnikPrograma_ID"].ToString();
                nazivTextBox.Text = korisnikProgramaDataSet.vwKorisnikPrograma.Rows[0]["Naziv"].ToString();
                adresaTextBox.Text = korisnikProgramaDataSet.vwKorisnikPrograma.Rows[0]["Adresa"].ToString();
                if (!mestoComboBox.Items.Count.Equals(0))
                {
                    mestoComboBox.SelectedIndex = (int)mestoComboBox.FindString(korisnikProgramaDataSet.vwKorisnikPrograma.Rows[0]["Mesto"].ToString());
                }
                telefonTextBox.Text = korisnikProgramaDataSet.vwKorisnikPrograma.Rows[0]["Telefon"].ToString();
                PIBTextBox.Text = korisnikProgramaDataSet.vwKorisnikPrograma.Rows[0]["PIB"].ToString();
                ziroRacunTextBox.Text = korisnikProgramaDataSet.vwKorisnikPrograma.Rows[0]["Žiro račun"].ToString();

            }
        }

        #endregion

        #region IsprazniKorisnikaProgramaDetaljno()

        private void IsprazniKorisnikaProgramaDetaljno()
        {
            korisnikPrograma_IDTextBox.Text = "";
            nazivTextBox.Text = "";
            adresaTextBox.Text = "";
            if (!mestoComboBox.Items.Count.Equals(0))
            {
                mestoComboBox.SelectedIndex = 0;
            }
            telefonTextBox.Text = "";
            PIBTextBox.Text = "";
            ziroRacunTextBox.Text = "";
        }

        #endregion

        #region Load

        private void KorisnikPrograma_Load(object sender, EventArgs e)
        {
            try
            {
                DBMesto.DajPadajucuListuMesto(korisnikProgramaDataSet.mestoDataTable, false);

                DBKorisnikPrograma.DajKorisnikaPrograma(korisnikProgramaDataSet.vwKorisnikPrograma);
                PrikaziKorisnikaProgramaDetaljno();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!korisnikProgramaDataSet.vwKorisnikPrograma.Rows.Count.Equals(0))
                {
                    UStanje("Detaljno");
                }
                else
                {
                    UStanje("Osnovno");
                }
            }

        }

        #endregion

        #region Dugmici

        #region Unesi

        private void unesiButton_Click(object sender, EventArgs e)
        {
            IsprazniKorisnikaProgramaDetaljno();
            UStanje("Unos");
        }

        #endregion

        #region Izmeni

        private void izmeniButton_Click(object sender, EventArgs e)
        {
            UStanje("Izmena");
        }

        #endregion

        #region Potvrdi

        private void potvrdiButton_Click(object sender, EventArgs e)
        {

            korisnikProgramaErrorProvider.Clear();

            #region Provera obaveznih polja

            if (nazivTextBox.Text == "")
            {
                korisnikProgramaErrorProvider.SetError(nazivTextBox, "Obavezan podatak.");
                nazivTextBox.Select();
            }
            else if (adresaTextBox.Text == "")
            {
                korisnikProgramaErrorProvider.SetError(adresaTextBox, "Obavezan podatak.");
                adresaTextBox.Select();
            }
            else if (mestoComboBox.SelectedValue == null)
            {
                korisnikProgramaErrorProvider.SetError(mestoComboBox, "Obavezan podatak.");
            }
            else if (telefonTextBox.Text == "")
            {
                korisnikProgramaErrorProvider.SetError(telefonTextBox, "Obavezan podatak.");
                telefonTextBox.Select();
            }
            else if (PIBTextBox.Text == "")
            {
                korisnikProgramaErrorProvider.SetError(PIBTextBox, "Obavezan podatak.");
                PIBTextBox.Select();
            }
            else if (ziroRacunTextBox.Text == "")
            {
                korisnikProgramaErrorProvider.SetError(ziroRacunTextBox, "Obavezan podatak.");
                ziroRacunTextBox.Select();
            }
            #endregion

            //ako su podaci na formi ispravno uneti
            else
            {
                try
                {
                    //ako je polje Sifra prazno onda je Insert 
                    if (korisnikPrograma_IDTextBox.Text == "")
                    {
                        #region Unos

                        //unesi red 
                        DBKorisnikPrograma.UnesiKorisnikaPrograma(1, nazivTextBox.Text, Byte.Parse(mestoComboBox.SelectedValue.ToString()), adresaTextBox.Text, PIBTextBox.Text, ziroRacunTextBox.Text, telefonTextBox.Text);

                        DBKorisnikPrograma.DajKorisnikaPrograma(korisnikProgramaDataSet.vwKorisnikPrograma);


                        #endregion
                    }
                    //ako polje Sifra dokumenta nije prazno onda Update
                    else
                    {
                        #region Izmena

                        DBKorisnikPrograma.IzmeniKorisnikaPrograma(Byte.Parse(korisnikPrograma_IDTextBox.Text), nazivTextBox.Text, Byte.Parse(mestoComboBox.SelectedValue.ToString()), adresaTextBox.Text, PIBTextBox.Text, ziroRacunTextBox.Text, telefonTextBox.Text);

                        DBKorisnikPrograma.DajKorisnikaPrograma(korisnikProgramaDataSet.vwKorisnikPrograma);

                        #endregion
                    }

                    if (!korisnikProgramaDataSet.vwKorisnikPrograma.Rows.Count.Equals(0))
                    {
                        PrikaziKorisnikaProgramaDetaljno();
                        UStanje("Detaljno");
                    }
                    else
                    {
                        IsprazniKorisnikaProgramaDetaljno();
                        UStanje("Osnovno");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Odustani

        private void odustaniButton_Click(object sender, EventArgs e)
        {
            korisnikProgramaErrorProvider.Clear();

            if (!korisnikProgramaDataSet.vwKorisnikPrograma.Rows.Count.Equals(0))
            {
                PrikaziKorisnikaProgramaDetaljno();
                UStanje("Detaljno");
            }
            else
            {
                IsprazniKorisnikaProgramaDetaljno();
                UStanje("Osnovno");
            }
        }

        #endregion

        #region Obrisi

        private void obrisiButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da obrišete korisnika?",
                    "Potvrdi brisanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    DBKorisnikPrograma.ObrisiKorisnikaPrograma(Byte.Parse(korisnikPrograma_IDTextBox.Text));

                    DBKorisnikPrograma.DajKorisnikaPrograma(korisnikProgramaDataSet.vwKorisnikPrograma);

                    if (!korisnikProgramaDataSet.vwKorisnikPrograma.Rows.Count.Equals(0))
                    {
                        PrikaziKorisnikaProgramaDetaljno();
                        UStanje("Detaljno");
                    }
                    else
                    {
                        IsprazniKorisnikaProgramaDetaljno();
                        UStanje("Osnovno");
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region Kraj

        private void krajButton_Click(object sender, EventArgs e)
        {
            korisnikProgramaDataSet.Clear();
            korisnikProgramaDataSet.Dispose();
            this.Close();
        }

        #endregion

        #endregion
    }
}