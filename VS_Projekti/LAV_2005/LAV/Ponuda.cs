using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAV
{
    public partial class Ponuda : Form
    {
        DB.PoslovniPartner DBPoslovniPartner = new DB.PoslovniPartner();
        DB.Radnik DBRadnik = new DB.Radnik();
        DB.Ponuda DBPonuda = new DB.Ponuda();
        DB.Stavka DBStavka = new DB.Stavka();
        DB.KorisnikPrograma DBKorisnikPrograma = new DB.KorisnikPrograma();

        string StanjeStavke = "";

        public Ponuda()
        {
            InitializeComponent();

            InicijalizujDokument();
            InicijalizujStavku();

        }

        #region InicijalizujDokument

        private void InicijalizujDokument()
        {
            poslovniPartnerComboBox.DataSource = ponudaDataSet.poslovniPartnerDataTable;
            poslovniPartnerComboBox.DisplayMember = "Naziv";
            poslovniPartnerComboBox.ValueMember = "PoslovniPartner_ID";
            //
            poslovniPartnerComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            poslovniPartnerComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            poslovniPartnerComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            radnikComboBox.DataSource = ponudaDataSet.radnikDataTable;
            radnikComboBox.DisplayMember = "ImePrezime";
            radnikComboBox.ValueMember = "Radnik_ID";
            //
            radnikComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            radnikComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            radnikComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //TabStop=false
            obrisiDokumentButton.TabStop = false;
            krajDokumentaButton.TabStop = false;
            
            nadjiDokumentButton.TabStop = false;
            nadjiDokumentTextBox.TabStop = false;
            
            tipAutomobilaTextBox.TabStop = false;

            ////TabIndex
            datumDateTimePicker.TabIndex = 0;
            poslovniPartnerComboBox.TabIndex = 1;
            radnikComboBox.TabIndex = 2;
            zakljucenaCheckBox.TabIndex = 3;
            napomenaRichTextBox.TabIndex = 4;
            brojServisneKnjiziceTextBox.TabIndex = 5;
            brojMotoraTextBox.TabIndex = 6;
            brojSasijeTextBox.TabIndex = 7;
            registarskiBrojTextBox.TabIndex = 8;
            nadjiTipAutomobilaButton.TabIndex = 9;
            servoCheckBox.TabIndex = 10;
            ABSCheckBox.TabIndex = 11;
            klimaCheckBox.TabIndex = 12;

            unesiDokumentButton.TabIndex = 13;
            izmeniDokumentButton.TabIndex = 14;
            potvrdiDokumentButton.TabIndex = 15;
            odustaniDokumentaButton.TabIndex = 16;

            napomenaRichTextBox.MaxLength = 200;
            brojServisneKnjiziceTextBox.MaxLength = 20;
            brojMotoraTextBox.MaxLength = 20;
            brojSasijeTextBox.MaxLength = 20;
            registarskiBrojTextBox.MaxLength = 20;

            tipAutomobilaTextBox.ReadOnly = true;


            
        }

        #endregion

        #region InicijalizujStavku
        void InicijalizujStavku()
        {
            vrstaStavkeComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            vrstaStavkeComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            vrstaStavkeComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            if (!vrstaStavkeComboBox.Items.Count.Equals(0))
            {
                vrstaStavkeComboBox.SelectedIndex = 0;
            }

            nazivStavkeTextBox.ReadOnly = true;

            //formula za Izracunjive kolone
            stavkaDataSet.uspDajStavkeZaPonudu.UkupnoColumn.Expression = "isnull((Cena * Količina),0) + isnull(([Cena ugradnje] * Količina),0)";

            //Binduj grid
            stavkaBindingNavigator.BindingSource = stavkaBindingSource;
            stavkaBindingSource.DataMember = stavkaDataSet.uspDajStavkeZaPonudu.DefaultView.ToString();
            stavkaBindingSource.DataSource = stavkaDataSet.uspDajStavkeZaPonudu.DefaultView;
            stavkaDataGridView.DataSource = stavkaBindingSource;

            //sakri kolone u gridu
            stavkaDataGridView.Columns["Stavka_ID"].Visible = false;
            stavkaDataGridView.Columns["Ponuda_ID"].Visible = false;
            stavkaDataGridView.Columns["ID"].Visible = false;

            //formatira vrednosti u kolonama na dve decimale. Mora ici posle bindovanja grida
            stavkaDataGridView.Columns["Ukupno"].DefaultCellStyle.Format = ".00";

            //DataGridView podesavanja
            stavkaDataGridView.MultiSelect = false;
            stavkaDataGridView.AllowUserToAddRows = false;
            stavkaDataGridView.AllowUserToResizeRows = false;
            stavkaDataGridView.AllowUserToDeleteRows = false;
            stavkaDataGridView.Enabled = false;
            stavkaDataGridView.ReadOnly = true;
            stavkaDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            stavkaDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //TabStop=false
            stavka_IDTextBox.TabStop = false;
            stavkaDataGridView.TabStop = false;
            stavkaBindingNavigator.TabStop = false;
            obrisiStavkuButton.TabStop = false;
            artikalDetaljnoTabControl.TabStop = false;
            ukupnoTextBox.TabStop = false;
            ukupnoTextBox.Enabled = false;

            nazivStavkeTextBox.TabStop = false;
            stavka_IDTextBox.Visible = false;

            //TabIndex
            vrstaStavkeComboBox.TabIndex = 100;
            nadjiStavkuButton.TabIndex = 101;
            kolicinaTextBox.TabIndex = 101;
            cenaTextBox.TabIndex = 102;
            cenaUgradnjeTextBox.TabIndex = 103;
            normaSatiTextBox.TabIndex = 104;

            potvrdiStavkuButton.TabIndex = 105;
            odustaniStavkeButton.TabIndex = 106;

            kolicinaTextBox.MaxLength = 5;
            cenaTextBox.MaxLength = 19;
            cenaUgradnjeTextBox.MaxLength = 19;
            normaSatiTextBox.MaxLength = 6;
        } 
        #endregion

        #region UStanjeZaglavljeDokumenta

        protected void UStanjeZaglavljeDokumenta(string stanje)
        {
            //                      ako je padajuca lista prazna dalji uslovi se nece uzimati u obzir 
            datumDateTimePicker.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            poslovniPartnerComboBox.Enabled = (!poslovniPartnerComboBox.Items.Count.Equals(0)) && (stanje == "Unos") || (stanje == "Izmena");
            radnikComboBox.Enabled = (!poslovniPartnerComboBox.Items.Count.Equals(0)) && (stanje == "Unos") || (stanje == "Izmena");
            zakljucenaCheckBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            napomenaRichTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            brojServisneKnjiziceTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            brojMotoraTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            brojSasijeTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            registarskiBrojTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            tipAutomobilaTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            nadjiTipAutomobilaButton.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            servoCheckBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            ABSCheckBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            klimaCheckBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");

            nadjiDokumentTextBox.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");
            nadjiDokumentButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");

            stampajPonuduToolStripButton.Enabled = (stanje == "Detaljno");
            
            unesiDokumentButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");
            izmeniDokumentButton.Enabled = (stanje == "Detaljno");
            potvrdiDokumentButton.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            odustaniDokumentaButton.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            obrisiDokumentButton.Enabled = (stanje == "Detaljno");
            krajDokumentaButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");

            if (zaglavljeTabControl.SelectedTab == osnovniPodaciTabPage)
            {
                datumDateTimePicker.Select();
            }
            else if (zaglavljeTabControl.SelectedTab == podaciOVoziluTabPage)
            {
                brojServisneKnjiziceTextBox.Select();
            }
        }

        #endregion

        #region UStanjeStavka

        protected void UStanjeStavka(string stanje)
        {
            StanjeStavke = stanje;

            izmeniStavkuButton.Enabled = (stanje == "Detaljno");
            obrisiStavkuButton.Enabled = (stanje == "Detaljno");
            odustaniStavkeButton.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            potvrdiStavkuButton.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            unesiStavkuButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");

            //                      ako je padajuca lista prazna dalji uslovi se nece uzimati u obzir 
            vrstaStavkeComboBox.Enabled = (!vrstaStavkeComboBox.Items.Count.Equals(0)) && (stanje == "Unos") || (stanje == "Izmena");
            nadjiStavkuButton.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            nazivStavkeTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            kolicinaTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            cenaTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            cenaUgradnjeTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            normaSatiTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");

            stavkaDataGridView.Enabled = (stanje == "Detaljno");
            stavkaBindingNavigator.Enabled = (stanje == "Detaljno");

            if (stanje == "Unos")
            {
                vrstaStavkeComboBox.Select();
            }
            else if (stanje == "Izmena")
            {
                kolicinaTextBox.Select();
            }
        }

        #endregion

        #region UStanjeDugmiciAcceptCancell
        protected void UStanjeDugmiciAcceptCancell(string stanje)
        {
            if (stanje == "UnosIzmenaStavke")
            {
                this.AcceptButton = potvrdiStavkuButton;
                this.CancelButton = odustaniStavkeButton;
            }
            else if (stanje == "UnosIzmenaZaglavlja")
            {
                this.AcceptButton = potvrdiDokumentButton;
                this.CancelButton = odustaniDokumentaButton;
            }
            else
            {
                this.AcceptButton = null;
                this.CancelButton = null;
            }

        }
        #endregion

        #region PrikaziZaglavlje
        private void PrikaziZaglavlje()
        {
            if (!ponudaDataSet.vwPonudaZaglavlje.Rows.Count.Equals(0))
            {
                ponuda_IDTextBox.Text = ponudaDataSet.vwPonudaZaglavlje[0]["Ponuda_ID"].ToString();
                datumDateTimePicker.Value = DateTime.Parse(ponudaDataSet.vwPonudaZaglavlje[0]["Datum"].ToString());
                poslovniPartnerComboBox.SelectedIndex = (int)poslovniPartnerComboBox.FindString(ponudaDataSet.vwPonudaZaglavlje[0]["Poslovni partner"].ToString());
                radnikComboBox.SelectedIndex = (int)radnikComboBox.FindString(ponudaDataSet.vwPonudaZaglavlje[0]["Radnik"].ToString());
                zakljucenaCheckBox.Checked = Boolean.Parse(ponudaDataSet.vwPonudaZaglavlje[0]["Zaključana"].ToString());
                napomenaRichTextBox.Text = ponudaDataSet.vwPonudaZaglavlje[0]["Napomena"].ToString();
                brojServisneKnjiziceTextBox.Text = ponudaDataSet.vwPonudaZaglavlje[0]["Broj servisne knjižice"].ToString();
                brojMotoraTextBox.Text = ponudaDataSet.vwPonudaZaglavlje[0]["Broj motora"].ToString();
                brojSasijeTextBox.Text = ponudaDataSet.vwPonudaZaglavlje[0]["Broj šasije"].ToString();
                registarskiBrojTextBox.Text = ponudaDataSet.vwPonudaZaglavlje[0]["Registarski broj"].ToString();

                if (ponudaDataSet.vwPonudaZaglavlje[0]["TipAutomobila_ID"].ToString()!="")
                {
                    tipAutomobilaTextBox.Text = ponudaDataSet.vwPonudaZaglavlje[0]["Proizvođač automobila"].ToString() + " - " + ponudaDataSet.vwPonudaZaglavlje[0]["Model automobila"].ToString() + " - " + ponudaDataSet.vwPonudaZaglavlje[0]["Tip automobila"].ToString();
                }   
                    tipAutomobilaTextBox.Tag = ponudaDataSet.vwPonudaZaglavlje[0]["TipAutomobila_ID"].ToString();
             
                servoCheckBox.Checked = Boolean.Parse(ponudaDataSet.vwPonudaZaglavlje[0]["Servo"].ToString());
                ABSCheckBox.Checked = Boolean.Parse(ponudaDataSet.vwPonudaZaglavlje[0]["ABS"].ToString());
                klimaCheckBox.Checked = Boolean.Parse(ponudaDataSet.vwPonudaZaglavlje[0]["Klima"].ToString());

            }
        }

        #endregion

        #region IsprazniZaglavlje
        private void IsprazniZaglavlje()
        {
            ponuda_IDTextBox.Text = "";
            datumDateTimePicker.Value = DateTime.Now;
            if (!poslovniPartnerComboBox.Items.Count.Equals(0))
            {
                poslovniPartnerComboBox.SelectedIndex = 0;
            }
            if (!radnikComboBox.Items.Count.Equals(0))
            {
                radnikComboBox.SelectedIndex = 0;
            }
            zakljucenaCheckBox.Checked = false;
            napomenaRichTextBox.Text = "";
            brojServisneKnjiziceTextBox.Text = "";
            brojMotoraTextBox.Text = "";
            brojSasijeTextBox.Text = "";
            registarskiBrojTextBox.Text = "";

            tipAutomobilaTextBox.Text = "";
            tipAutomobilaTextBox.Tag = "";

            servoCheckBox.Checked = false;
            ABSCheckBox.Checked = false;
            klimaCheckBox.Checked = false;

        }

        #endregion

        #region PrikaziStavkuDetaljno

        protected void PrikaziStavkuDetaljno()
        {
            vrstaStavkeComboBox.SelectedIndex = (int)vrstaStavkeComboBox.FindString(stavkaDataGridView.CurrentRow.Cells["Vrsta stavke"].Value.ToString());

            nazivStavkeTextBox.Text = stavkaDataGridView.CurrentRow.Cells["Naziv"].Value.ToString();
            nazivStavkeTextBox.Tag = stavkaDataGridView.CurrentRow.Cells["ID"].Value.ToString();

            kolicinaTextBox.Text = stavkaDataGridView.CurrentRow.Cells["Količina"].Value.ToString();
            cenaTextBox.Text = stavkaDataGridView.CurrentRow.Cells["Cena"].Value.ToString();
            cenaUgradnjeTextBox.Text = stavkaDataGridView.CurrentRow.Cells["Cena ugradnje"].Value.ToString();
            normaSatiTextBox.Text = stavkaDataGridView.CurrentRow.Cells["Norma sati"].Value.ToString();

            //ne vidi se na interfejsu a koristim ga kao parametar u unosu i izmeni
            stavka_IDTextBox.Text = stavkaDataGridView.CurrentRow.Cells["Stavka_ID"].Value.ToString();
        }

        #endregion

        #region PrikaziStavkuDetaljno (indexReda)

        protected void PrikaziStavkuDetaljno(int indexReda)
        {
            vrstaStavkeComboBox.SelectedIndex = (int)vrstaStavkeComboBox.FindString(stavkaDataGridView.Rows[indexReda].Cells["Vrsta stavke"].Value.ToString());

            nazivStavkeTextBox.Text = stavkaDataGridView.Rows[indexReda].Cells["Naziv"].Value.ToString();
            nazivStavkeTextBox.Tag = stavkaDataGridView.Rows[indexReda].Cells["ID"].Value.ToString();

            kolicinaTextBox.Text = stavkaDataGridView.Rows[indexReda].Cells["Količina"].Value.ToString();
            cenaTextBox.Text = stavkaDataGridView.Rows[indexReda].Cells["Cena"].Value.ToString();
            cenaUgradnjeTextBox.Text = stavkaDataGridView.Rows[indexReda].Cells["Cena ugradnje"].Value.ToString();
            normaSatiTextBox.Text = stavkaDataGridView.Rows[indexReda].Cells["Norma sati"].Value.ToString();

            //ne vidi se na interfejsu a koristim ga kao parametar u unosu i izmeni
            stavka_IDTextBox.Text = stavkaDataGridView.Rows[indexReda].Cells["Stavka_ID"].Value.ToString();
        }

        #endregion

        #region IsprazniStavkuDetaljno

        protected void IsprazniStavkuDetaljno()
        {
            vrstaStavkeComboBox.SelectedIndex = 0;

            nazivStavkeTextBox.Text = "";
            nazivStavkeTextBox.Tag = "";

            kolicinaTextBox.Text = "";
            cenaTextBox.Text = "";
            cenaUgradnjeTextBox.Text = "";
            normaSatiTextBox.Text = "";

            //ne vidi se na interfejsu a koristim ga kao parametar u unosu i izmeni
            stavka_IDTextBox.Text = "";
        }

        #endregion

        #region NapuniKontroleDokumenta
        private void NapuniKontroleDokumenta()
        {
            if (!ponudaDataSet.vwPonudaZaglavlje.Rows.Count.Equals(0))
            {
                PrikaziZaglavlje();
                UStanjeZaglavljeDokumenta("Detaljno");
            }
            else
            {
                IsprazniZaglavlje();
                UStanjeZaglavljeDokumenta("Osnovno");
            }

            NapuniKontroleStavke();
        }

        #endregion

        #region NapuniKontroleStavke
        /// <summary>
        /// Poziva se nakon metoda koje pune Zaglavlje, Padajuce liste i Stavke. Puni kontrole na nivou stavke i zajedno sa kontrolama DugmiciZaglavlja postavlja u potrebno stanje
        /// </summary>
        protected void NapuniKontroleStavke()
        {
            //ako postoji dokument
            if (ponuda_IDTextBox.Text != "")
            {
                //ako dokument ima stavku
                if (!stavkaDataGridView.RowCount.Equals(0))
                {
                    PrikaziStavkuDetaljno();
                    NapuniUkupnoDokumenta();

                    UStanjeStavka("Detaljno");
                }
                //ako dokument nema stavku
                else
                {
                    IsprazniStavkuDetaljno();
                    ukupnoTextBox.Text = "";

                    UStanjeStavka("Osnovno");
                }
            }
            //ako nema dokumenta
            else
            {
                stavkaDataSet.uspDajStavkeZaPonudu.Clear();
                IsprazniStavkuDetaljno();
                ukupnoTextBox.Text = "";

                UStanjeStavka("IzgasiSve");
            }
        }


        #endregion

        #region NapuniUkupnoDokumenta
        protected void NapuniUkupnoDokumenta()
        {
            decimal _ukupnoDokumenta = 0;

            for (int i = 0; i < stavkaDataGridView.RowCount; i++)
            {
                //napuni polje redni broj u gridu
                //moze i sa autoinkrementom ali kod praznjenja grida pa njegovog punjanja nece da se resetuje brojac
                stavkaDataGridView.Rows[i].Cells["RB"].Value = i + 1;

                _ukupnoDokumenta = _ukupnoDokumenta + (decimal.Parse(stavkaDataGridView.Rows[i].Cells["Ukupno"].Value.ToString()));

            }

            ukupnoTextBox.Text = _ukupnoDokumenta.ToString(".00");
        }
        #endregion

        #region Load

        private void Ponuda_Load(object sender, EventArgs e)
        {
            try
            {
                DBPoslovniPartner.DajPadajucuListuPoslovniPartner(ponudaDataSet.poslovniPartnerDataTable);
                DBRadnik.DajPadajucuListuRadnik(ponudaDataSet.radnikDataTable);

                DBPonuda.DajPoslednjuPonudu(ponudaDataSet.vwPonudaZaglavlje);

                if (!ponudaDataSet.vwPonudaZaglavlje.Rows.Count.Equals(0))
                {
                    DBStavka.DajStavkeZaPonudu(stavkaDataSet.uspDajStavkeZaPonudu, Int32.Parse(ponudaDataSet.vwPonudaZaglavlje.Rows[0]["Ponuda_ID"].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                NapuniKontroleDokumenta();
            }
        } 

        #endregion

        #region Dogadjaji

        private void zaglavljeTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (zaglavljeTabControl.SelectedTab == osnovniPodaciTabPage)
            {
                datumDateTimePicker.Select();
            }
            else if (zaglavljeTabControl.SelectedTab == podaciOVoziluTabPage)
            {
                brojServisneKnjiziceTextBox.Select();
            }
        }

        private void napomenaRichTextBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Napomena napomena = new Napomena(napomenaRichTextBox);
            napomena.ShowDialog();

        }

        private void stavkaDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            PrikaziStavkuDetaljno(e.RowIndex);
        }

        private void stavkaDataGridView_Sorted(object sender, EventArgs e)
        {
            if (!stavkaDataGridView.RowCount.Equals(0))
            {
                this.stavkaDataGridView.FirstDisplayedCell = this.stavkaDataGridView.CurrentCell;
                stavkaDataGridView.CurrentCell = stavkaDataGridView["Vrsta stavke", 0];
            }
        }

        private void vrstaStavkeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            cenaUgradnjeTextBox.Visible = (vrstaStavkeComboBox.SelectedItem.ToString() == "Artikal") || (vrstaStavkeComboBox.SelectedItem.ToString() == "Roba");
            cenaUgradnjeLabel.Visible = (vrstaStavkeComboBox.SelectedItem.ToString() == "Artikal") || (vrstaStavkeComboBox.SelectedItem.ToString() == "Roba");

            if ((!stavkaDataGridView.Rows.Count.Equals(0)) && (StanjeStavke == "Izmena"))
            {
                int _indexReda = stavkaDataGridView.CurrentRow.Index;

                string _vrstaStavkeTrenutnogReda = stavkaDataGridView.Rows[_indexReda].Cells["Vrsta stavke"].Value.ToString();

                if (vrstaStavkeComboBox.SelectedItem.ToString() == _vrstaStavkeTrenutnogReda)
                {
                    PrikaziStavkuDetaljno();
                }
                else
                {
                    //ne ovde IsprazniStavkuDetaljno() zato sto ova metoda podesava VrstuStavke na index nula(Artikal)
                    nazivStavkeTextBox.Text = "";
                    nazivStavkeTextBox.Tag = "";

                    kolicinaTextBox.Text = "";
                    cenaTextBox.Text = "";
                    cenaUgradnjeTextBox.Text = "";
                    normaSatiTextBox.Text = "";

                    //ne vidi se na interfejsu a koristim ga kao parametar u unosu i izmeni
                    //stavka_IDTextBox.Text = "";

                }
            }
            else if (StanjeStavke == "Unos")
            {
                //ne ovde IsprazniStavkuDetaljno() zato sto ova metoda podesava VrstuStavke na index nula(Artikal)
                nazivStavkeTextBox.Text = "";
                nazivStavkeTextBox.Tag = "";

                kolicinaTextBox.Text = "";
                cenaTextBox.Text = "";
                cenaUgradnjeTextBox.Text = "";
                normaSatiTextBox.Text = "";

                //ne vidi se na interfejsu a koristim ga kao parametar u unosu i izmeni
                //stavka_IDTextBox.Text = "";
            }
        }

        #endregion

        #region DugmiciZaglavlje

        #region unesiDokumentButton
        private void unesiDokumentButton_Click(object sender, EventArgs e)
        {
            zaglavljeTabControl.SelectedTab = osnovniPodaciTabPage;
            datumDateTimePicker.Select();


            UStanjeDugmiciAcceptCancell("UnosIzmenaZaglavlja");

            IsprazniZaglavlje();

            datumDateTimePicker.Value = DateTime.Now;

            //ovim ispraznim Grid a u data setu podaci ostaju za slucaj odustajanja
            DataTable newUspDajStavkeZaPonudu = new DS.StavkaDataSet.uspDajStavkeZaPonuduDataTable();
            stavkaDataGridView.DataSource = newUspDajStavkeZaPonudu;

            IsprazniStavkuDetaljno();

            //disable sve kontrole na nivou stavke
            UStanjeStavka("IzgasiSve");

            UStanjeZaglavljeDokumenta("Unos");
            //UstanjeDugmiciZaglavlja("Unos");
        }
          #endregion

        #region zmeniDokumentButton
        private void izmeniDokumentButton_Click(object sender, EventArgs e)
        {
            UStanjeDugmiciAcceptCancell("UnosIzmenaZaglavlja");

            UStanjeStavka("IzgasiSve");
            UStanjeZaglavljeDokumenta("Izmena");

        }
        #endregion

        #region odustaniDokumentaButton
        private void odustaniDokumentaButton_Click(object sender, EventArgs e)
        {
            ponudaErrorProvider.Clear();

            //vrati vrednosti u grid
            stavkaDataGridView.DataSource = stavkaBindingSource;

            NapuniKontroleDokumenta();

        }
        #endregion

        #region krajDokumentaButton
        private void krajDokumentaButton_Click(object sender, EventArgs e)
        {
            ponudaDataSet.Clear();
            this.Close();
        }
        #endregion

        #region obrisiDokumentButton
        private void obrisiDokumentButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da obrišete dokument?",
                "Potvrdi brisanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //ako korisnik potvrdi brisanje 
            {
                try
                {
                    DBPonuda.ObrisiPonudu(Int32.Parse(ponuda_IDTextBox.Text));

                    DBPoslovniPartner.DajPadajucuListuPoslovniPartner(ponudaDataSet.poslovniPartnerDataTable);
                    DBRadnik.DajPadajucuListuRadnik(ponudaDataSet.radnikDataTable);

                    DBPonuda.DajPoslednjuPonudu(ponudaDataSet.vwPonudaZaglavlje);

                    if (!ponudaDataSet.vwPonudaZaglavlje.Rows.Count.Equals(0))
                    {
                        DBStavka.DajStavkeZaPonudu(stavkaDataSet.uspDajStavkeZaPonudu, Int32.Parse(ponudaDataSet.vwPonudaZaglavlje.Rows[0]["Ponuda_ID"].ToString()));
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    NapuniKontroleDokumenta();
                }
            }
        }
        #endregion

        #region potvrdiDokumentButton
        private void potvrdiDokumentButton_Click(object sender, EventArgs e)
        {
            ponudaErrorProvider.Clear();

            #region Provera obaveznih polja
            if (poslovniPartnerComboBox.SelectedValue == null)
            {
                ponudaErrorProvider.SetError(poslovniPartnerComboBox, "Obavezan podatak.");
            }
            if (radnikComboBox.SelectedValue == null)
            {
                ponudaErrorProvider.SetError(radnikComboBox, "Obavezan podatak.");
            }
            #endregion

            //ako su podaci na formi ispravno uneti
            else
            {
                try
                {
                    Int32 _tipAutomobila_ID = -1;

                    if (tipAutomobilaTextBox.Text != "")
                    {
                        _tipAutomobila_ID = Int32.Parse(tipAutomobilaTextBox.Tag.ToString());
                    }

                    //ako je polje Sifra prazno onda je Insert 
                    if (ponuda_IDTextBox.Text == "")
                    {
                        #region Unos

                        //unesi red i uzmi ID unetog reda
                        DBPonuda.DajPonudu(ponudaDataSet.vwPonudaZaglavlje, DBPonuda.UnesiPonudu(Int16.Parse(poslovniPartnerComboBox.SelectedValue.ToString()), _tipAutomobila_ID, Int16.Parse(radnikComboBox.SelectedValue.ToString()), datumDateTimePicker.Value, zakljucenaCheckBox.Checked, brojServisneKnjiziceTextBox.Text, brojMotoraTextBox.Text, brojSasijeTextBox.Text, registarskiBrojTextBox.Text, servoCheckBox.Checked, klimaCheckBox.Checked, ABSCheckBox.Checked, napomenaRichTextBox.Text));

                        #endregion
                    }
                    //ako polje Sifra dokumenta nije prazno onda Update
                    else
                    {
                        DBPonuda.IzmeniPonudu(Int32.Parse(ponuda_IDTextBox.Text), Int16.Parse(poslovniPartnerComboBox.SelectedValue.ToString()), _tipAutomobila_ID, Int16.Parse(radnikComboBox.SelectedValue.ToString()), datumDateTimePicker.Value, zakljucenaCheckBox.Checked, brojServisneKnjiziceTextBox.Text, brojMotoraTextBox.Text, brojSasijeTextBox.Text, registarskiBrojTextBox.Text, servoCheckBox.Checked, klimaCheckBox.Checked, ABSCheckBox.Checked, napomenaRichTextBox.Text);
                        DBPonuda.DajPonudu(ponudaDataSet.vwPonudaZaglavlje, Int32.Parse(ponuda_IDTextBox.Text));
                    }
                    //osvezi padajuce liste
                    DBPoslovniPartner.DajPadajucuListuPoslovniPartner(ponudaDataSet.poslovniPartnerDataTable);
                    DBRadnik.DajPadajucuListuRadnik(ponudaDataSet.radnikDataTable);

                    if (!ponudaDataSet.vwPonudaZaglavlje.Rows.Count.Equals(0))
                    {
                        DBStavka.DajStavkeZaPonudu(stavkaDataSet.uspDajStavkeZaPonudu, Int32.Parse(ponudaDataSet.vwPonudaZaglavlje.Rows[0]["Ponuda_ID"].ToString()));
                    }

                    //menja se u unosu da bi se ispraznio grid a vrednost ostala u data setu da bi se mogla 
                    //prikazati u slucaju odustajanja. Da li ovde ili gore
                    stavkaDataGridView.DataSource = stavkaBindingSource;


                    NapuniKontroleDokumenta();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion 

        #endregion

        #region DugmiciStavka

        #region unesiStavkuButton
        private void unesiStavkuButton_Click(object sender, EventArgs e)
        {
            UStanjeDugmiciAcceptCancell("UnosIzmenaStavke");

            IsprazniStavkuDetaljno();
            UStanjeStavka("Unos");
            UStanjeZaglavljeDokumenta("IzgasiSve");

        }
                #endregion

        #region izmeniStavkuButton
        private void izmeniStavkuButton_Click(object sender, EventArgs e)
        {
            UStanjeDugmiciAcceptCancell("UnosIzmenaStavke");

            UStanjeStavka("Izmena");
            UStanjeZaglavljeDokumenta("IzgasiSve");

        }
        #endregion

        #region odustaniStavkeButton
        private void odustaniStavkeButton_Click(object sender, EventArgs e)
        {
            ponudaErrorProvider.Clear();

            NapuniKontroleDokumenta();

        }
        #endregion

        #region obrisiStavkuButton
        private void obrisiStavkuButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da obrišete stavku?",
            "Potvrdi brisanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //ako korisnik potvrdi brisanje 
            {
                try
                {
                    DBStavka.ObrisiStavku(Int32.Parse(stavka_IDTextBox.Text));
                    DBStavka.DajStavkeZaPonudu(stavkaDataSet.uspDajStavkeZaPonudu, Int32.Parse(ponuda_IDTextBox.Text));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    NapuniKontroleDokumenta();
                }
            }
        }
        #endregion

        #region potvrdiStavkuButton
        private void potvrdiStavkuButton_Click(object sender, EventArgs e)
        {
            StanjeStavke = "";

            //koristi se kod provere da li je vrednost polja CenaBezPoreza decimal (TryParse)
            Int16 _kolicina;
            Decimal _cena;
            Decimal _cenaUgradnje;
            Decimal _normaSati;

            ponudaErrorProvider.Clear();

            #region Provera obaveznih polja
            //ako u katalogu nisu uneti podaci za padajuce liste
            if (nazivStavkeTextBox.Text == "")
            {
                ponudaErrorProvider.SetError(nadjiStavkuButton, "Obavezan podatak.");
                nadjiStavkuButton.Select();
            }
            else if (kolicinaTextBox.Text == "")
            {
                ponudaErrorProvider.SetError(kolicinaTextBox, "Obavezan podatak.");
                kolicinaTextBox.Select();
            }
            else if (cenaTextBox.Text == "")
            {
                ponudaErrorProvider.SetError(cenaTextBox, "Obavezan podatak.");
                cenaTextBox.Select();
            }
            //else if (cenaUgradnjeTextBox.Text == "")
            //{
            //    ponudaErrorProvider.SetError(cenaUgradnjeTextBox, "Obavezan podatak.");
            //    cenaUgradnjeTextBox.Select();
            //}
            else if (normaSatiTextBox.Text == "")
            {
                ponudaErrorProvider.SetError(normaSatiTextBox, "Obavezan podatak.");
                normaSatiTextBox.Select();
            }

            #endregion

            #region Provera tipa podataka
            //provera da li poslata vrednost broj
            else if (!Int16.TryParse(kolicinaTextBox.Text, out _kolicina))
            {
                ponudaErrorProvider.SetError(kolicinaTextBox, "Vrednost mora biti broj.");
                kolicinaTextBox.Select();
            }

            //provera da li poslata vrednost decimal
            else if (!Decimal.TryParse(cenaTextBox.Text, out _cena))
            {
                ponudaErrorProvider.SetError(cenaTextBox, "Vrednost mora biti broj.");
                cenaTextBox.Select();
            }
            //provera da li poslata vrednost ima vise od 16 cifara i dve decimale
            else if ((Decimal.Parse(cenaTextBox.Text)) > (decimal.Parse("9999999999999999,99")))
            {
                ponudaErrorProvider.SetError(cenaTextBox, "Vrednost može imati najviše 16 cifara i dve decimale.");
                cenaTextBox.Select();
            }

            //provera da li poslata vrednost decimal
            else if ((cenaUgradnjeTextBox.Text != "") && (!Decimal.TryParse(cenaUgradnjeTextBox.Text, out _cenaUgradnje)))
            {
                ponudaErrorProvider.SetError(cenaUgradnjeTextBox, "Vrednost mora biti broj.");
                cenaUgradnjeTextBox.Select();
            }
            //provera da li poslata vrednost ima vise od 16 cifara i dve decimale
            else if ((cenaUgradnjeTextBox.Text != "") && ((Decimal.Parse(cenaUgradnjeTextBox.Text)) > (decimal.Parse("9999999999999999,99"))))
            {
                ponudaErrorProvider.SetError(cenaUgradnjeTextBox, "Vrednost može imati najviše 16 cifara i dve decimale.");
                cenaUgradnjeTextBox.Select();
            }

            //provera da li poslata vrednost decimal
            else if (!Decimal.TryParse(normaSatiTextBox.Text, out _normaSati))
            {
                ponudaErrorProvider.SetError(normaSatiTextBox, "Vrednost mora biti broj.");
                normaSatiTextBox.Select();
            }
            //provera da li poslata vrednost ima vise od 16 cifara i dve decimale
            else if ((Decimal.Parse(normaSatiTextBox.Text)) > (decimal.Parse("999,99")))
            {
                ponudaErrorProvider.SetError(normaSatiTextBox, "Vrednost može imati najviše 3 cifre i dve decimale.");
                normaSatiTextBox.Select();
            }

            #endregion

            #region Provera ChkConnstraintia
            //provera da li je poslata vrednost veca od nule
            else if (decimal.Parse(kolicinaTextBox.Text) < decimal.Parse("0"))
            {
                ponudaErrorProvider.SetError(kolicinaTextBox, "Vrednost ne moze biti manja od nule.");
                kolicinaTextBox.Select();
            }
            else if (decimal.Parse(cenaTextBox.Text) < decimal.Parse("0"))
            {
                ponudaErrorProvider.SetError(cenaTextBox, "Vrednost ne moze biti manja od nule.");
                cenaTextBox.Select();
            }
            else if ((cenaUgradnjeTextBox.Visible == true)&&(cenaUgradnjeTextBox.Text != "") && (decimal.Parse(cenaUgradnjeTextBox.Text) < decimal.Parse("0")))
            {
                ponudaErrorProvider.SetError(cenaUgradnjeTextBox, "Vrednost ne moze biti manja od nule.");
                cenaUgradnjeTextBox.Select();
            }
            else if (decimal.Parse(normaSatiTextBox.Text) < decimal.Parse("0"))
            {
                ponudaErrorProvider.SetError(normaSatiTextBox, "Vrednost ne moze biti manja od nule.");
                normaSatiTextBox.Select();
            }


            #endregion

            //ako su podaci na formi ispravno uneti
            else
            {
                try
                {
                    Int32 _artikal_ID = -1;
                    Int32 _roba_ID = -1;
                    Int32 _usluga_ID = -1;
                    Decimal _cenaUgradnje1 = -1;

                    if (vrstaStavkeComboBox.SelectedItem.ToString() == "Artikal")
                    {
                        _artikal_ID = Int32.Parse(nazivStavkeTextBox.Tag.ToString());
                    }
                    else if (vrstaStavkeComboBox.SelectedItem.ToString() == "Roba")
                    {
                        _roba_ID = Int32.Parse(nazivStavkeTextBox.Tag.ToString());
                    }
                    else if (vrstaStavkeComboBox.SelectedItem.ToString() == "Usluga")
                    {
                        _usluga_ID = Int32.Parse(nazivStavkeTextBox.Tag.ToString());
                    }
                    if ((cenaUgradnjeTextBox.Visible == true)&&(cenaUgradnjeTextBox.Text != ""))
                    {
                        _cenaUgradnje1 = Decimal.Parse(cenaUgradnjeTextBox.Text);
                    }

                    //ako je polje Sifra dokumenta prazno onda je Insert 
                    if (stavka_IDTextBox.Text == "")
                    {
                        #region Unos

                        //neobavezna polja
                        //Int32 _artikal_ID = -1;
                        //Int32 _roba_ID = -1;
                        //Int32 _usluga_ID = -1;
                        //Decimal _cenaUgradnje1 = -1;

                        //if (vrstaStavkeComboBox.SelectedItem.ToString() == "Artikal")
                        //{
                        //    _artikal_ID = Int32.Parse(nazivStavkeTextBox.Tag.ToString());
                        //}
                        //else if (vrstaStavkeComboBox.SelectedItem.ToString() == "Roba")
                        //{
                        //    _roba_ID = Int32.Parse(nazivStavkeTextBox.Tag.ToString());
                        //}
                        //else if (vrstaStavkeComboBox.SelectedItem.ToString() == "Usluga")
                        //{
                        //    _usluga_ID = Int32.Parse(nazivStavkeTextBox.Tag.ToString());
                        //}
                        //if (cenaUgradnjeTextBox.Text != "")
                        //{
                        //    _cenaUgradnje1 = Decimal.Parse(cenaUgradnjeTextBox.Text);
                        //}

                        //unesi red i uzmi ID unetog reda
                        Int32 _stavka_ID = DBStavka.UnesiStavku(_artikal_ID, _roba_ID, _usluga_ID, Decimal.Parse(cenaTextBox.Text), _cenaUgradnje1, Decimal.Parse(normaSatiTextBox.Text), Int16.Parse(kolicinaTextBox.Text), Int32.Parse(ponuda_IDTextBox.Text));

                        DBStavka.DajStavkeZaPonudu(stavkaDataSet.uspDajStavkeZaPonudu, Int32.Parse(ponuda_IDTextBox.Text));

                        //poziv metode koja vraca ID unetog reda ili -1
                        int _indexUnetogReda = PomocneKlase.Index.DajIndexReda(stavkaDataGridView, "Stavka_ID", _stavka_ID.ToString());

                        //ako je nasao index upravo unetog reda  i ako je taj index validan. Ako prvi uslov nije tacan drugi se ne proverava
                        if ((_indexUnetogReda != -1) && (_indexUnetogReda < stavkaDataGridView.RowCount))
                        {
                            //podesava da CurrentCell bude na redu koji je unet
                            this.stavkaDataGridView.CurrentCell = this.stavkaDataGridView["Vrsta stavke", _indexUnetogReda];
                        }
                        #endregion

                    }
                    //Izmena
                    else
                    {
                        #region Izmena

                        //BazaRobnaStavka.IzmeniRobnuStavku(Int32.Parse(sifraStavkeTextBox.Text), Int16.Parse(artikalComboBox.SelectedValue.ToString()), Decimal.Parse(cenaBazPorezaTextBox.Text), Decimal.Parse(kolicinaTextBox.Text), Decimal.Parse(rabatStavkeProcenataTextBox.Text), Int32.Parse(sifraDokumentaTextBox.Text));
                        DBStavka.IzmeniStavku(Int32.Parse(stavka_IDTextBox.Text), _artikal_ID, _roba_ID, _usluga_ID, Decimal.Parse(cenaTextBox.Text), _cenaUgradnje1, Decimal.Parse(normaSatiTextBox.Text), Int16.Parse(kolicinaTextBox.Text), Int32.Parse(ponuda_IDTextBox.Text));

                        Int32 _sifraIzmenjeStavke = Int32.Parse(stavka_IDTextBox.Text);

                        DBStavka.DajStavkeZaPonudu(stavkaDataSet.uspDajStavkeZaPonudu, Int32.Parse(ponuda_IDTextBox.Text));

                        //poziv metode koja vraca index promenjenog  reda ili -1
                        int _indexIzmenjenogReda = PomocneKlase.Index.DajIndexReda(stavkaDataGridView, "Stavka_ID", _sifraIzmenjeStavke.ToString());

                        //ako je nasao index upravo izmenjenog reda i ako je taj index validan. Ako prvi uslov nije tacan drugi se ne proverava
                        if ((_indexIzmenjenogReda != -1) && (_indexIzmenjenogReda < stavkaDataGridView.RowCount))
                        {
                            this.stavkaDataGridView.CurrentCell = this.stavkaDataGridView["Vrsta stavke", _indexIzmenjenogReda];
                        }

                        #endregion

                    }

                    NapuniKontroleDokumenta();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        #endregion

        #endregion

        #region nadjiStavkuButton
        private void nadjiStavkuButton_Click(object sender, EventArgs e)
        {
            if (vrstaStavkeComboBox.SelectedItem.ToString() == "Artikal")
            {
                if (nazivStavkeTextBox.Tag.ToString() != "")
                {
                    Artikal artikal = new Artikal(ponudaErrorProvider, nazivStavkeTextBox, cenaTextBox, cenaUgradnjeTextBox, normaSatiTextBox, Int32.Parse(nazivStavkeTextBox.Tag.ToString()));
                    artikal.ShowDialog();
                }
                else
                {
                    Artikal artikal = new Artikal(ponudaErrorProvider, nazivStavkeTextBox, cenaTextBox, cenaUgradnjeTextBox, normaSatiTextBox);
                    artikal.ShowDialog();
                }

            }
            else if (vrstaStavkeComboBox.SelectedItem.ToString() == "Roba")
            {
                if (nazivStavkeTextBox.Tag.ToString() != "")
                {
                    Roba roba = new Roba(ponudaErrorProvider, nazivStavkeTextBox, cenaTextBox, cenaUgradnjeTextBox, normaSatiTextBox, Int32.Parse(nazivStavkeTextBox.Tag.ToString()));
                    roba.ShowDialog();
                }
                else
                {
                    Roba roba = new Roba(ponudaErrorProvider, nazivStavkeTextBox, cenaTextBox, cenaUgradnjeTextBox, normaSatiTextBox);
                    roba.ShowDialog();
                }
            }
            else if (vrstaStavkeComboBox.SelectedItem.ToString() == "Usluga")
            {
                if (nazivStavkeTextBox.Tag.ToString() != "")
                {
                    Usluga usluga = new Usluga(ponudaErrorProvider, nazivStavkeTextBox, cenaTextBox, cenaUgradnjeTextBox, normaSatiTextBox, Int32.Parse(nazivStavkeTextBox.Tag.ToString()));
                    usluga.ShowDialog();
                }
                else
                {
                    Usluga usluga = new Usluga(ponudaErrorProvider, nazivStavkeTextBox, cenaTextBox, cenaUgradnjeTextBox, normaSatiTextBox);
                    usluga.ShowDialog();
                }
            }

        }
        #endregion

        #region isprazniStavkuDesniKlik
        private void isprazniStavkuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            nazivStavkeTextBox.Text = "";
            nazivStavkeTextBox.Tag = "";
        } 
        #endregion

        #region nadjiTipAutomobilaButton
        private void nadjiTipAutomobilaButton_Click(object sender, EventArgs e)
        {
            if (tipAutomobilaTextBox.Tag.ToString() != "")
            {
                Automobil automobil = new Automobil(tipAutomobilaTextBox, Int32.Parse(tipAutomobilaTextBox.Tag.ToString()));
                automobil.ShowDialog();

            }
            else
            {
                Automobil automobil = new Automobil(tipAutomobilaTextBox);
                automobil.ShowDialog();
            }




        }
        #endregion

        #region ispraniTipAutomobilaDesniKlik
        private void isprazniTipAutomobilaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tipAutomobilaTextBox.Text = "";
            tipAutomobilaTextBox.Tag = "";
        }
        #endregion

        #region nadjiDokumentButton

        private void nadjiDokumentButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (nadjiDokumentTextBox.Text == "")
                {
                    NadjiPonudu nadjiPonudu = new NadjiPonudu(this);
                    nadjiPonudu.ShowDialog();
                }
                else
                {
                    //ako vrednost upisanu u polje nadji moze da parsira u int vratice vrednost 
                    //koju je pokusao da parsira a ako ne moze vratice nulu
                    int _i;
                    int.TryParse(nadjiDokumentTextBox.Text, out _i);

                    //ako je u polju nadji broj tipa int
                    if (_i != 0)
                    {
                        //ako postoji Racun
                        if (DBPonuda.PostojiLiPonuda(Int32.Parse(nadjiDokumentTextBox.Text)))
                        {
                            try
                            {
                                DBPoslovniPartner.DajPadajucuListuPoslovniPartner(ponudaDataSet.poslovniPartnerDataTable);
                                DBRadnik.DajPadajucuListuRadnik(ponudaDataSet.radnikDataTable);

                                DBPonuda.DajPonudu(ponudaDataSet.vwPonudaZaglavlje, Int32.Parse(nadjiDokumentTextBox.Text));

                                if (!ponudaDataSet.vwPonudaZaglavlje.Rows.Count.Equals(0))
                                {
                                    DBStavka.DajStavkeZaPonudu(stavkaDataSet.uspDajStavkeZaPonudu, Int32.Parse(ponudaDataSet.vwPonudaZaglavlje.Rows[0]["Ponuda_ID"].ToString()));
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally
                            {
                                NapuniKontroleDokumenta();
                            }
                        }
                        //ako ne postoji RacunOtpremnica
                        else
                        {
                            NadjiPonudu nadjiPonudu = new NadjiPonudu(this);
                            nadjiPonudu.ShowDialog();
                        }

                    }
                    //ako u polju nadji nije broj 
                    else
                    {
                        NadjiPonudu nadjiPonudu = new NadjiPonudu(this);
                        nadjiPonudu.ShowDialog();
                    }

                    nadjiDokumentTextBox.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        } 
        #endregion

        #region PrikaziPonudu

        internal void PrikaziPonudu(Int32 ponuda_ID)
        {
            try
            {
                DBPoslovniPartner.DajPadajucuListuPoslovniPartner(ponudaDataSet.poslovniPartnerDataTable);
                DBRadnik.DajPadajucuListuRadnik(ponudaDataSet.radnikDataTable);

                DBPonuda.DajPonudu(ponudaDataSet.vwPonudaZaglavlje, ponuda_ID);

                if (!ponudaDataSet.vwPonudaZaglavlje.Rows.Count.Equals(0))
                {
                    DBStavka.DajStavkeZaPonudu(stavkaDataSet.uspDajStavkeZaPonudu, Int32.Parse(ponudaDataSet.vwPonudaZaglavlje.Rows[0]["Ponuda_ID"].ToString()));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                NapuniKontroleDokumenta();
            }
        } 

        #endregion

        #region stampajPonudu

        private void stampajPonuduToolStripButton_Click(object sender, EventArgs e)
        {
            if (ponuda_IDTextBox.Text != "")
            {
                DataSet _ponuda = new DataSet();
                DS.StampajPonuduDataSet stampajPonuduDataSet = new LAV.DS.StampajPonuduDataSet();
                try
                {
                    DBPoslovniPartner.DajPoslovnogPartnere(stampajPonuduDataSet.vwPoslovniPartner, Int16.Parse(poslovniPartnerComboBox.SelectedValue.ToString()));
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                _ponuda.Tables.Add(ponudaDataSet.vwPonudaZaglavlje.Copy());
                _ponuda.Tables.Add(stavkaDataSet.uspDajStavkeZaPonudu.Copy());
                _ponuda.Tables.Add(stampajPonuduDataSet.vwPoslovniPartner.Copy());

                stampajPonuduDataSet.Clear();
                stampajPonuduDataSet.Dispose();
                
                try
                {
                    //DBKorisnikPrograma.DajKorisnikaPrograma(stampajPodatkeOKorisnikuDataSet.vwKorisnikPrograma);
                    
                    StampajPonudu stampajPonudu = new StampajPonudu(_ponuda);
                    stampajPonudu.WindowState = FormWindowState.Maximized;
                    stampajPonudu.Show();

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion
    }
}