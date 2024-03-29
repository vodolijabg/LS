using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Data.SqlClient;

namespace LAV
{
    public partial class PoslovniPartner : Form
    {
        DB.PoslovniPartner DBPoslovniPartner = new DB.PoslovniPartner();
        DB.Mesto DBMesto = new DB.Mesto();

        public PoslovniPartner()
        {
            InitializeComponent();

            Inicijalizuj();
        }

        #region Inicijalizuj

        private void Inicijalizuj()
        {

            //Bindovanje DataGridView-a
            poslovniPartnerBindingNavigator.BindingSource = poslovniPartnerBindingSource;
            poslovniPartnerBindingSource.DataMember = poslovniPartnerDataSet.vwPoslovniPartner.DefaultView.ToString();
            poslovniPartnerBindingSource.DataSource = poslovniPartnerDataSet.vwPoslovniPartner.DefaultView;
            poslovniPartnerDataGridView.DataSource = poslovniPartnerBindingSource;

            //DataGridView podesavanja
            poslovniPartnerDataGridView.MultiSelect = false;
            poslovniPartnerDataGridView.AllowUserToAddRows = false;
            poslovniPartnerDataGridView.AllowUserToResizeRows = false;
            poslovniPartnerDataGridView.AllowUserToDeleteRows = false;
            poslovniPartnerDataGridView.Enabled = false;
            poslovniPartnerDataGridView.ReadOnly = true;
            poslovniPartnerDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            poslovniPartnerDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            poslovniPartnerDataGridView.Columns["Mesto_ID"].Visible = false;


            //Bindovanje Padajuce liste
            mestoComboBox.DataSource = poslovniPartnerDataSet.mestoDataTable;
            mestoComboBox.DisplayMember = "Naziv";
            mestoComboBox.ValueMember = "Mesto_ID";
            //
            mestoComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            mestoComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            mestoComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            //TabStop=false
            poslovniPartnerDataGridView.TabStop = false;
            poslovniPartnerBindingNavigator.TabStop = false;
            obrisiButton.TabStop = false;
            krajButton.TabStop = false;
            //poslovniPartnerDetaljnoTabControl.TabStop = false;

            uslovNazivTextBox.TabStop = false;
            uslovPIBTextBox.TabStop = false;
            uslovStaraSifraTextBox.TabStop = false;
            uslovSlicnoTrazenjeSadrziCheckBox.TabStop = false;
            nadjiButton.TabStop = false;

            //TabIndex
            staraSifraTextBox.TabIndex = 0;
            nazivTextBox.TabIndex = 1;
            PIBTextBox.TabIndex = 2;
            mestoComboBox.TabIndex = 3;
            adresaTextBox.TabIndex = 4;

            kontaktOsobaTextBox.TabIndex = 5;
            telefonTextBox.TabIndex = 6;
            emailTextBox.TabIndex = 7;
            napomenaRichTextBox.TabIndex = 8;

            potvrdiButton.TabIndex = 9;
            odustaniButton.TabIndex = 10;

            poslovniPartnerDetaljnoTabControl.TabIndex = 11;

            //MaxLenght
            staraSifraTextBox.MaxLength = 15;
            nazivTextBox.MaxLength = 60;
            PIBTextBox.MaxLength = 15;
            adresaTextBox.MaxLength = 50;

            kontaktOsobaTextBox.MaxLength = 50;
            telefonTextBox.MaxLength = 50;
            emailTextBox.MaxLength = 50;
            napomenaRichTextBox.MaxLength = 200;

            uslovSlicnoTrazenjeSadrziCheckBox.Checked = true;
        }

        #endregion

        #region UStanje()

        private void UStanje(string stanje)
        {
            //mora ici pre dugmica
            staraSifraTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            nazivTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            mestoComboBox.Enabled = (!mestoComboBox.Items.Count.Equals(0)) && (stanje == "Unos") || (stanje == "Izmena");
            PIBTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            adresaTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");

            kontaktOsobaTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            telefonTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            emailTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            napomenaRichTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");

            uslovNazivTextBox.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");
            uslovPIBTextBox.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");
            uslovStaraSifraTextBox.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");
            uslovSlicnoTrazenjeSadrziCheckBox.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");
            nadjiButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");


            importToolStripButton.Enabled =  (stanje == "Osnovno") || (stanje == "Detaljno");
            exportToolStripButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");
            artikliDobavljacaToolStripButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");

            unesiButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");
            izmeniButton.Enabled = (stanje == "Detaljno");
            potvrdiButton.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            odustaniButton.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            obrisiButton.Enabled = (stanje == "Detaljno");
            krajButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");

            poslovniPartnerDataGridView.Enabled = (stanje == "Detaljno");

            poslovniPartnerBindingNavigator.Enabled = (stanje == "Detaljno");

            if (stanje == "Unos")
            {
                //ako je stanje Unos prikazi tab sa osnovnim podacima
                poslovniPartnerDetaljnoTabControl.SelectedTab = osnovniPodaciTabPage;
                staraSifraTextBox.Select();

            }
            else if (stanje == "Izmena")
            {
                //ako je stanje Izmena ostani na trenutnom tabu
                if (poslovniPartnerDetaljnoTabControl.SelectedTab == osnovniPodaciTabPage)
                {
                    staraSifraTextBox.Select();
                }
                else if (poslovniPartnerDetaljnoTabControl.SelectedTab == dopunskiPodaciTabPage)
                {
                    kontaktOsobaTextBox.Select();
                }
            }
            else if (stanje == "Osnovno")
            {
                //ako je stanje Osnovno prikazi tab sa osnovnim podacima
                poslovniPartnerDetaljnoTabControl.SelectedTab = osnovniPodaciTabPage;

                if (!mestoComboBox.Items.Count.Equals(0))
                {
                    mestoComboBox.SelectedIndex = 0;
                }

            }
        }

        #endregion

        #region PrikaziPoslovnogPartneraDetaljno(indexReda)

        public void PrikaziPoslovnogPartneraDetaljno(int indexReda)
        {

            poslovniPartner_IDTextBox.Text = poslovniPartnerDataGridView.Rows[indexReda].Cells["PoslovniPartner_ID"].Value.ToString();
            staraSifraTextBox.Text = poslovniPartnerDataGridView.Rows[indexReda].Cells["Stara šifra"].Value.ToString();
            nazivTextBox.Text = poslovniPartnerDataGridView.Rows[indexReda].Cells["Naziv"].Value.ToString();
            PIBTextBox.Text = poslovniPartnerDataGridView.Rows[indexReda].Cells["PIB"].Value.ToString();
            mestoComboBox.SelectedIndex = (int)mestoComboBox.FindString(poslovniPartnerDataGridView.Rows[indexReda].Cells["Mesto"].Value.ToString());
            adresaTextBox.Text = poslovniPartnerDataGridView.Rows[indexReda].Cells["Adresa"].Value.ToString();

            kontaktOsobaTextBox.Text = poslovniPartnerDataGridView.Rows[indexReda].Cells["Kontakt osoba"].Value.ToString();
            telefonTextBox.Text = poslovniPartnerDataGridView.Rows[indexReda].Cells["Telefon"].Value.ToString();
            emailTextBox.Text = poslovniPartnerDataGridView.Rows[indexReda].Cells["E-mail"].Value.ToString();

            napomenaRichTextBox.Text = poslovniPartnerDataGridView.Rows[indexReda].Cells["Napomena"].Value.ToString();

        }

        #endregion

        #region PrikaziPoslovnogPartneraDetaljno()

        public void PrikaziPoslovnogPartneraDetaljno()
        {
            if (!poslovniPartnerDataGridView.RowCount.Equals(0))
            {
                poslovniPartner_IDTextBox.Text = poslovniPartnerDataGridView.CurrentRow.Cells["PoslovniPartner_ID"].Value.ToString();
                staraSifraTextBox.Text = poslovniPartnerDataGridView.CurrentRow.Cells["Stara šifra"].Value.ToString();
                nazivTextBox.Text = poslovniPartnerDataGridView.CurrentRow.Cells["Naziv"].Value.ToString();
                PIBTextBox.Text = poslovniPartnerDataGridView.CurrentRow.Cells["PIB"].Value.ToString();
                mestoComboBox.SelectedIndex = (int)mestoComboBox.FindString(poslovniPartnerDataGridView.CurrentRow.Cells["Mesto"].Value.ToString());
                adresaTextBox.Text = poslovniPartnerDataGridView.CurrentRow.Cells["Adresa"].Value.ToString();

                kontaktOsobaTextBox.Text = poslovniPartnerDataGridView.CurrentRow.Cells["Kontakt osoba"].Value.ToString();
                telefonTextBox.Text = poslovniPartnerDataGridView.CurrentRow.Cells["Telefon"].Value.ToString();
                emailTextBox.Text = poslovniPartnerDataGridView.CurrentRow.Cells["E-mail"].Value.ToString();

                napomenaRichTextBox.Text = poslovniPartnerDataGridView.CurrentRow.Cells["Napomena"].Value.ToString();
            }
        }

        #endregion

        #region IsprazniPoslovnogPartneraDetaljno()

        private void IsprazniPoslovnogPartneraDetaljno()
        {
            poslovniPartner_IDTextBox.Text = "";
            staraSifraTextBox.Text = "";
            nazivTextBox.Text = "";
            PIBTextBox.Text = "";
            if (!mestoComboBox.Items.Count.Equals(0))
            {
                mestoComboBox.SelectedIndex = 0;
            }
            adresaTextBox.Text = "";

            kontaktOsobaTextBox.Text = "";
            telefonTextBox.Text = "";
            emailTextBox.Text = "";

            napomenaRichTextBox.Text = "";
        }

        #endregion

        #region Dogadjaji

        private void poslovniPartnerDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            PrikaziPoslovnogPartneraDetaljno(e.RowIndex);
        }

        private void poslovniPartnerDataGridView_Sorted(object sender, EventArgs e)
        {
            if (!poslovniPartnerDataGridView.RowCount.Equals(0))
            {
                this.poslovniPartnerDataGridView.FirstDisplayedCell = this.poslovniPartnerDataGridView.CurrentCell;
                poslovniPartnerDataGridView.CurrentCell = poslovniPartnerDataGridView[0, 0];
            }
        }

        private void napomenaRichTextBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Napomena napomena = new Napomena(napomenaRichTextBox);
            napomena.ShowDialog();
        }

        private void poslovniPartnerDetaljnoTabControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (poslovniPartnerDetaljnoTabControl.SelectedTab == osnovniPodaciTabPage)
            {
                staraSifraTextBox.Select();
            }
            else if (poslovniPartnerDetaljnoTabControl.SelectedTab == dopunskiPodaciTabPage)
            {
                kontaktOsobaTextBox.Select();
            }
        }

        #endregion

        #region Load

        private void PoslovniPartner_Load(object sender, EventArgs e)
        {
            try
            {
                DBMesto.DajPadajucuListuMesto(poslovniPartnerDataSet.mestoDataTable, true);
               
                //DBPoslovniPartner.DajSvePoslovnePartnere(poslovniPartnerDataSet.vwPoslovniPartner);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!poslovniPartnerDataGridView.Rows.Count.Equals(0))
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
            IsprazniPoslovnogPartneraDetaljno();
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
            poslovniPartnerErrorProvider.Clear();

            #region Provera obaveznih polja
            if (nazivTextBox.Text == "")
            {
                poslovniPartnerErrorProvider.SetError(nazivTextBox, "Obavezan podatak.");
                poslovniPartnerDetaljnoTabControl.SelectedTab = osnovniPodaciTabPage;
                nazivTextBox.Select();
            }
            #endregion

            //ako su podaci na formi ispravno uneti
            else
            {
                try
                {
                    //ako je polje Sifra prazno onda je Insert 
                    if (poslovniPartner_IDTextBox.Text == "")
                    {
                        #region Unos

                        //unesi red i uzmi ID unetog reda
                        Int16 _poslovniPartner_ID = DBPoslovniPartner.UnesiPoslovnogPartnera(staraSifraTextBox.Text, nazivTextBox.Text, PIBTextBox.Text, Byte.Parse(mestoComboBox.SelectedValue.ToString()), adresaTextBox.Text, telefonTextBox.Text,emailTextBox.Text,kontaktOsobaTextBox.Text, napomenaRichTextBox.Text);

                        //osvezi padajuce liste
                        DBMesto.DajPadajucuListuMesto(poslovniPartnerDataSet.mestoDataTable,true);

                        //DBPoslovniPartner.DajSvePoslovnePartnere(poslovniPartnerDataSet.vwPoslovniPartner);
                        string _SQLUpit = DajSQLUpit() + " union  SELECT PoslovniPartner_ID, [Stara šifra], Naziv, PIB, Mesto_ID,  Mesto, Adresa, Telefon, [E-mail],[Kontakt osoba], Napomena FROM vwPoslovniPartner where PoslovniPartner_ID = " + _poslovniPartner_ID;
                        DBPoslovniPartner.NadjiPoslovnogPartnera(poslovniPartnerDataSet.vwPoslovniPartner, _SQLUpit);

                        //poziv metode koja vraca ID unetog reda ili -1
                        int _indexUnetogReda = PomocneKlase.Index.DajIndexReda(poslovniPartnerDataGridView, "PoslovniPartner_ID", _poslovniPartner_ID.ToString());

                        //ako je nasao index upravo unetog reda  i ako je taj index validan. Ako prvi uslov nije tacan drugi se ne proverava
                        if ((_indexUnetogReda != -1) && (_indexUnetogReda < poslovniPartnerDataGridView.RowCount))
                        {
                            //podesava da CurrentCell bude na redu koji je unet
                            this.poslovniPartnerDataGridView.CurrentCell = this.poslovniPartnerDataGridView[0, _indexUnetogReda];
                        }
                        #endregion
                    }
                    //ako polje Sifra dokumenta nije prazno onda Update
                    else
                    {
                        #region Izmena

                        DBPoslovniPartner.IzmeniPoslovnogPartnera(Int16.Parse(poslovniPartner_IDTextBox.Text),staraSifraTextBox.Text, nazivTextBox.Text, PIBTextBox.Text, Byte.Parse(mestoComboBox.SelectedValue.ToString()), adresaTextBox.Text, telefonTextBox.Text,emailTextBox.Text,kontaktOsobaTextBox.Text, napomenaRichTextBox.Text);

                        Int16 _sifraIzmenjenogPoslovnogPartnera = Int16.Parse(poslovniPartner_IDTextBox.Text);

                        //osvezi padajuce liste
                        DBMesto.DajPadajucuListuMesto(poslovniPartnerDataSet.mestoDataTable,true);

                        //DBPoslovniPartner.DajSvePoslovnePartnere(poslovniPartnerDataSet.vwPoslovniPartner);
                        string _SQLUpit = DajSQLUpit() + " union  SELECT PoslovniPartner_ID, [Stara šifra], Naziv, PIB, Mesto_ID,  Mesto, Adresa, Telefon, [E-mail],[Kontakt osoba], Napomena FROM vwPoslovniPartner where PoslovniPartner_ID = " + _sifraIzmenjenogPoslovnogPartnera;
                        DBPoslovniPartner.NadjiPoslovnogPartnera(poslovniPartnerDataSet.vwPoslovniPartner, _SQLUpit);

                        //poziv metode koja vraca index promenjenog  reda ili -1
                        int _indexIzmenjenogReda = PomocneKlase.Index.DajIndexReda(poslovniPartnerDataGridView, "PoslovniPartner_ID", _sifraIzmenjenogPoslovnogPartnera.ToString());

                        //ako je nasao index upravo izmenjenog reda i ako je taj index validan. Ako prvi uslov nije tacan drugi se ne proverava
                        if ((_indexIzmenjenogReda != -1) && (_indexIzmenjenogReda < poslovniPartnerDataGridView.RowCount))
                        {
                            this.poslovniPartnerDataGridView.CurrentCell = this.poslovniPartnerDataGridView[0, _indexIzmenjenogReda];
                        }

                        #endregion
                    }


                    if (!poslovniPartnerDataGridView.Rows.Count.Equals(0))
                    {
                        UStanje("Detaljno");
                    }
                    else
                    {
                        IsprazniPoslovnogPartneraDetaljno();
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
            poslovniPartnerErrorProvider.Clear();

            if (!poslovniPartnerDataGridView.Rows.Count.Equals(0))
            {
                PrikaziPoslovnogPartneraDetaljno();
                UStanje("Detaljno");
            }
            else
            {
                IsprazniPoslovnogPartneraDetaljno();
                UStanje("Osnovno");
            }
        }

        #endregion

        #region Obrisi

        private void obrisiButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da obrišete poslovnog partnera?",
                    "Potvrdi brisanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    DBPoslovniPartner.ObrisiPoslovnogPartnera(Int16.Parse(poslovniPartner_IDTextBox.Text));

                    DBMesto.DajPadajucuListuMesto(poslovniPartnerDataSet.mestoDataTable, true);

                    //DBPoslovniPartner.DajSvePoslovnePartnere(poslovniPartnerDataSet.vwPoslovniPartner);
                    DBPoslovniPartner.NadjiPoslovnogPartnera(poslovniPartnerDataSet.vwPoslovniPartner, DajSQLUpit());

                    if (!poslovniPartnerDataGridView.Rows.Count.Equals(0))
                    {
                        UStanje("Detaljno");
                    }
                    else
                    {
                        IsprazniPoslovnogPartneraDetaljno();
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
            this.Close();
        }

        #endregion

        #endregion

        #region Export
        private void exportToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog exportSaveFileDialog = new SaveFileDialog();
            exportSaveFileDialog.Filter = "TXT fajl (*.txt)|*.txt";
            exportSaveFileDialog.Title = "Export";
            exportSaveFileDialog.RestoreDirectory = true;

            if (exportSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    int _brojRedova = DBPoslovniPartner.IzveziPoslovnePartnere(exportSaveFileDialog.FileName);

                    MessageBox.Show("Uspešno je sačuvano " + _brojRedova.ToString() + " redova.     ", "LAV", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "LAV", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        } 
        #endregion

        #region Import
        private void importToolStripButton_Click(object sender, EventArgs e)
        {
            int _ukupanBrojRedova = 0;
            int _brojUnetih = 0;
            int _brojGresaka = 0;

            char _karakter;

            int _kolona = 1;

            string _staraSifra = "";//_kolona = 1
            string _naziv = "";//_kolona = 2
            string _PIB = "";//_kolona = 3
            string _mesto = "";//_kolona = 4
            string _adresa = "";//_kolona = 5
            string _telefon = "";//_kolona = 6
            string _email = "";//_kolona = 7
            string _kontaktOsoba = "";//_kolona = 8
            string _napomena = "";//_kolona = 9


            OpenFileDialog importOpenFileDialog = new OpenFileDialog();
            importOpenFileDialog.Filter = "TXT fajl (*.txt)|*.txt";
            importOpenFileDialog.Title = "Import";
            importOpenFileDialog.RestoreDirectory = true;

            if (importOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                //prvo resetuj brojac
                SqlConnection _konekcijaSqlConnection = new SqlConnection();
                using (_konekcijaSqlConnection = DB.Konekcija.DajKonekciju())
                {
                    try
                    {
                        _konekcijaSqlConnection.Open();
                        DB.InkrementalniKljuc.ResetujInkrementalniKljuc("PoslovniPartner_ID", "PoslovniPartner", _konekcijaSqlConnection);
                    }
                    catch (Exception)
                    {
                        //throw;
                    }
                    finally
                    {
                        _konekcijaSqlConnection.Close();
                    }
                }

                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                this.Refresh();

                DateTime _pocetakUpita;
                TimeSpan _vremeTrajanja;
                _pocetakUpita = DateTime.Now;


                StringBuilder _putanjaZaGresku = new StringBuilder(importOpenFileDialog.FileName);

                StreamReader _importStreamReader = new StreamReader(importOpenFileDialog.FileName);

                StreamWriter _greskaStreamWriter = new StreamWriter(_putanjaZaGresku.Insert(_putanjaZaGresku.Length - 4, "_Greska").ToString());

                while (_importStreamReader.Peek() >= 0)
                {
                    _karakter = (char)_importStreamReader.Read();

                    if (_karakter.Equals('\t'))
                    {
                        _kolona++;
                    }
                    else
                    {
                        if (_karakter.Equals('\r') || _karakter.Equals('\n'))
                        {
                            //zato sto posle \r dolazi \n pa vec prazne stringove prazni ponovo
                            //proverava se kolona preko koje se vrsi update
                            if (_naziv != "")
                            {
                                string _c = _staraSifra + "\t" + _naziv + "\t" + _PIB + "\t" + _mesto + "\t" + _adresa + "\t" + _telefon + "\t" + _email + "\t" + _kontaktOsoba + "\t" + _napomena;

                                _ukupanBrojRedova++;

                                try
                                {
                                    DBPoslovniPartner.UveziPoslovnogPartnera(_staraSifra, _naziv, _PIB, _mesto, _adresa, _telefon, _email, _kontaktOsoba, _napomena);
                                    _brojUnetih++;
                                }
                                catch (Exception ex)
                                {
                                    _brojGresaka++;

                                    _c = _c + "\t" + ex.Message;

                                    _greskaStreamWriter.WriteLine(_c.ToCharArray());
                                }
                            }

                            _staraSifra = "";//_kolona = 1
                            _naziv = "";//_kolona = 2
                            _PIB = "";//_kolona = 3
                            _mesto = "";//_kolona = 4
                            _adresa = "";//_kolona = 5
                            _telefon = "";//_kolona = 6
                            _email = "";//_kolona = 7
                            _kontaktOsoba = "";//_kolona = 8
                            _napomena = "";//_kolona = 9


                            _kolona = 1;
                        }
                        else
                        {
                            switch (_kolona)
                            {
                                case 1:
                                    _staraSifra = _staraSifra + _karakter.ToString();
                                    break;
                                case 2:
                                    _naziv = _naziv + _karakter.ToString();
                                    break;
                                case 3:
                                    _PIB = _PIB + _karakter.ToString();
                                    break;
                                case 4:
                                    _mesto = _mesto + _karakter.ToString();
                                    break;
                                case 5:
                                    _adresa = _adresa + _karakter.ToString();
                                    break;
                                case 6:
                                    _telefon = _telefon + _karakter.ToString();
                                    break;
                                case 7:
                                    _email = _email + _karakter.ToString();
                                    break;
                                case 8:
                                    _kontaktOsoba = _kontaktOsoba + _karakter.ToString();
                                    break;
                                case 9:
                                    _napomena = _napomena + _karakter.ToString();
                                    break;

                            }
                        }
                    }
                }

                _greskaStreamWriter.Close();

                _vremeTrajanja = DateTime.Now - _pocetakUpita;
                this.Cursor = System.Windows.Forms.Cursors.Default;

                MessageBox.Show("Ukupan broj redova = " + _ukupanBrojRedova + "\nBroj unetih = " + _brojUnetih + "\nBroj grešaka = " + _brojGresaka + "\n\nVreme importa = " + _vremeTrajanja + "                                ", "LAV", MessageBoxButtons.OK, MessageBoxIcon.Information);

                try
                {
                    DBMesto.DajPadajucuListuMesto(poslovniPartnerDataSet.mestoDataTable, true);

                    DBPoslovniPartner.NadjiPoslovnogPartnera(poslovniPartnerDataSet.vwPoslovniPartner, DajSQLUpit());

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (!poslovniPartnerDataGridView.Rows.Count.Equals(0))
                    {
                        UStanje("Detaljno");
                    }
                    else
                    {
                        UStanje("Osnovno");
                    }
                }
            }
        } 
        #endregion

        #region StampaArtikliDobavljaca
        private void artikliDobavljacaToolStripButton_Click(object sender, EventArgs e)
        {
            if (poslovniPartner_IDTextBox.Text != "")
            {
                try
                {
                    DBPoslovniPartner.DajArtikleDobavljace(poslovniPartnerDataSet.uspDajArtikleDobavljaca, Int16.Parse(poslovniPartner_IDTextBox.Text));
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "LAV", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                Izvestaji.ArtikliDobavljaca artikliDobavljaca = new LAV.Izvestaji.ArtikliDobavljaca(poslovniPartnerDataSet.uspDajArtikleDobavljaca, nazivTextBox.Text);
                artikliDobavljaca.WindowState = FormWindowState.Maximized;
                artikliDobavljaca.Show();

            }
            else
            { }
        } 
        #endregion

        #region DajSQLUpit

        private string DajSQLUpit()
        {
            string _SQLUpit = " SELECT PoslovniPartner_ID, [Stara šifra], Naziv, PIB, Mesto_ID,  Mesto, Adresa, Telefon, [E-mail],[Kontakt osoba], Napomena FROM vwPoslovniPartner ";

            string _uslovStaraSifra = "";
            string _uslovNaziv = "";
            string _uslovPIB = "";

            bool _prviUslov = true;

            if ((uslovSlicnoTrazenjeSadrziCheckBox.Checked) && (uslovStaraSifraTextBox.Text != ""))
            {
                _uslovStaraSifra = " [Stara šifra] like '%" + uslovStaraSifraTextBox.Text + "%'";
            }
            else if ((!uslovSlicnoTrazenjeSadrziCheckBox.Checked) && (uslovStaraSifraTextBox.Text != ""))
            {
                _uslovStaraSifra = " [Stara šifra] = '" + uslovStaraSifraTextBox.Text + "'";
            }

            if ((uslovSlicnoTrazenjeSadrziCheckBox.Checked) && (uslovNazivTextBox.Text != ""))
            {
                _uslovNaziv = " Naziv like '%" + uslovNazivTextBox.Text + "%'";
            }
            else if ((!uslovSlicnoTrazenjeSadrziCheckBox.Checked) && (uslovNazivTextBox.Text != ""))
            {
                _uslovNaziv = " Naziv = '" + uslovNazivTextBox.Text + "'";
            }

            if ((uslovSlicnoTrazenjeSadrziCheckBox.Checked) && (uslovPIBTextBox.Text != ""))
            {
                _uslovPIB = " PIB like '%" + uslovPIBTextBox.Text + "%'";
            }
            else if ((!uslovSlicnoTrazenjeSadrziCheckBox.Checked) && (uslovPIBTextBox.Text != ""))
            {
                _uslovPIB = " PIB = '" + uslovPIBTextBox.Text + "'";
            }

            if (_uslovStaraSifra != "") 
            { 
                if(_prviUslov)
                {
                    _SQLUpit = _SQLUpit + " where ";
                    _prviUslov = false;
                }
                else
                {
                     _SQLUpit = _SQLUpit + " and ";
                }

                _SQLUpit = _SQLUpit + _uslovStaraSifra;
            }

            if (_uslovNaziv != "") 
            { 
                if(_prviUslov)
                {
                    _SQLUpit = _SQLUpit + " where ";
                    _prviUslov = false;
                }
                else
                {
                     _SQLUpit = _SQLUpit + " and ";
                }

                _SQLUpit = _SQLUpit + _uslovNaziv;
            }

            if (_uslovPIB != "") 
            { 
                if(_prviUslov)
                {
                    _SQLUpit = _SQLUpit + " where ";
                    _prviUslov = false;
                }
                else
                {
                     _SQLUpit = _SQLUpit + " and ";
                }

                _SQLUpit = _SQLUpit + _uslovPIB;
            }


            return _SQLUpit;
        }

        #endregion

        #region Nadji
        private void nadjiButton_Click(object sender, EventArgs e)
        {
            try
            {
                DBPoslovniPartner.NadjiPoslovnogPartnera(poslovniPartnerDataSet.vwPoslovniPartner, DajSQLUpit());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!poslovniPartnerDataGridView.Rows.Count.Equals(0))
                {
                    UStanje("Detaljno");
                }
                else
                {
                    IsprazniPoslovnogPartneraDetaljno();
                    UStanje("Osnovno");
                }
            }

        } 
        #endregion

        #region Resetuj
        private void resetujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uslovStaraSifraTextBox.Text = "";
            uslovPIBTextBox.Text = "";
            uslovNazivTextBox.Text = "";
            uslovSlicnoTrazenjeSadrziCheckBox.Checked = false;
        } 
        #endregion

    }
}