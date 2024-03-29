using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.IO;

namespace LAV
{
    public partial class Usluga : Form
    {
        DB.Usluga DBUsluga = new DB.Usluga();

        TextBox UslugaTextBox = null;
        TextBox CenaTextBox = null;
        TextBox CenaUgradnjeTextBox = null;
        TextBox NormaSatiTextBox = null;
        ErrorProvider PonudaErrorProvider = null;

        public Usluga()
        {
            InitializeComponent();

            Inicijalizuj();
        }
        public Usluga(ErrorProvider ponudaErrorProvider, TextBox uslugaTextBox, TextBox cena, TextBox cenaUgradnje, TextBox normaSati)
            : this()
        {
            this.ControlBox = true;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            UslugaTextBox = uslugaTextBox;
            CenaTextBox = cena;
            CenaUgradnjeTextBox = cenaUgradnje;
            NormaSatiTextBox = normaSati;
            PonudaErrorProvider = ponudaErrorProvider;


            this.uslugaDataGridView.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(uslugaDataGridView_CellMouseDoubleClick);
        }

        public Usluga(ErrorProvider ponudaErrorProvider, TextBox artikalTextBox, TextBox cena, TextBox cenaUgradnje, TextBox normaSati, Int32 usluga_ID)
            : this()
        {
            this.ControlBox = true;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            UslugaTextBox = artikalTextBox;
            CenaTextBox = cena;
            CenaUgradnjeTextBox = cenaUgradnje;
            NormaSatiTextBox = normaSati;
            PonudaErrorProvider = ponudaErrorProvider;


            this.uslugaDataGridView.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(uslugaDataGridView_CellMouseDoubleClick);

            try
            {
                DBUsluga.NadjiUslugu(uslugaDataSet.vwUsluga, usluga_ID);
            }
            catch (Exception)
            {
                //nista
            }
        }


        void uslugaDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((UslugaTextBox != null) && (!uslugaDataGridView.Rows.Count.Equals(0)))
            {
                UslugaTextBox.Text = uslugaDataGridView.Rows[uslugaDataGridView.CurrentCell.RowIndex].Cells["Naziv"].Value.ToString(); ;
                CenaTextBox.Text = uslugaDataGridView.Rows[uslugaDataGridView.CurrentCell.RowIndex].Cells["Cena"].Value.ToString(); ;
                NormaSatiTextBox.Text = uslugaDataGridView.Rows[uslugaDataGridView.CurrentCell.RowIndex].Cells["Norma sati"].Value.ToString(); ;

                UslugaTextBox.Tag = uslugaDataGridView.Rows[uslugaDataGridView.CurrentCell.RowIndex].Cells["Usluga_ID"].Value.ToString();
                PonudaErrorProvider.Clear();

            }

            uslugaDataSet.Clear();
            uslugaDataSet.Dispose();
            this.Close();
        }

        #region Inicijalizuj()

        private void Inicijalizuj()
        {
            //Bindovanje DataGridView-a
            uslugaBindingNavigator.BindingSource = uslugaBindingSource;
            uslugaBindingSource.DataMember = uslugaDataSet.vwUsluga.DefaultView.ToString();
            uslugaBindingSource.DataSource = uslugaDataSet.vwUsluga.DefaultView;
            uslugaDataGridView.DataSource = uslugaBindingSource;

            //DataGridView podesavanja
            uslugaDataGridView.MultiSelect = false;
            uslugaDataGridView.AllowUserToAddRows = false;
            uslugaDataGridView.AllowUserToResizeRows = false;
            uslugaDataGridView.AllowUserToDeleteRows = false;
            uslugaDataGridView.Enabled = false;
            uslugaDataGridView.ReadOnly = true;
            uslugaDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            uslugaDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //TabStop=false
            uslugaDataGridView.TabStop = false;
            uslugaBindingNavigator.TabStop = false;
            obrisiButton.TabStop = false;
            krajButton.TabStop = false;
            uslugeDetaljnoTabControl.TabStop = false;

            uslovNazivTextBox.TabStop = false;
            uslovSlicnoTrazenjeSadrziCheckBox.TabStop = false;
            nadjiButton.TabStop = false;


            //TabIndex
            staraSifraTextBox.TabIndex = 0;
            nazivTextBox.TabIndex = 1;
            cenaUslugeTextBox.TabIndex = 2;
            normaSatiTextBox.TabIndex = 3;
            napomenaRichTextBox.TabIndex = 4;

            potvrdiButton.TabIndex = 5;
            odustaniButton.TabIndex = 6;

            //MaxLenght
            staraSifraTextBox.MaxLength = 15;
            nazivTextBox.MaxLength = 60;
            cenaUslugeTextBox.MaxLength = 19;
            normaSatiTextBox.MaxLength = 6;
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
            cenaUslugeTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            normaSatiTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            napomenaRichTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");

            uslovNazivTextBox.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");
            uslovSlicnoTrazenjeSadrziCheckBox.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");
            nadjiButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");

            importToolStripButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");
            exportToolStripButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");

            unesiButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");
            izmeniButton.Enabled = (stanje == "Detaljno");
            potvrdiButton.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            odustaniButton.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            obrisiButton.Enabled = (stanje == "Detaljno");
            krajButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");

            uslugaDataGridView.Enabled = (stanje == "Detaljno");

            uslugaBindingNavigator.Enabled = (stanje == "Detaljno");

            if ((stanje == "Unos") || (stanje == "Izmena"))
            {
                staraSifraTextBox.Select();
            }
        }

        #endregion

        #region PrikaziUsluguDetaljno (indexReda)

        private void PrikaziUsluguDetaljno(int indexReda)
        {
            usluga_IDTextBox.Text = uslugaDataGridView.Rows[indexReda].Cells["Usluga_ID"].Value.ToString();
            staraSifraTextBox.Text = uslugaDataGridView.Rows[indexReda].Cells["Stara šifra"].Value.ToString();
            nazivTextBox.Text = uslugaDataGridView.Rows[indexReda].Cells["Naziv"].Value.ToString();
            cenaUslugeTextBox.Text = uslugaDataGridView.Rows[indexReda].Cells["Cena"].Value.ToString();
            normaSatiTextBox.Text = uslugaDataGridView.Rows[indexReda].Cells["Norma sati"].Value.ToString();
            napomenaRichTextBox.Text = uslugaDataGridView.Rows[indexReda].Cells["Napomena"].Value.ToString();

        }

        #endregion

        #region PrikaziUsluguDetaljno()

        private void PrikaziUsluguDetaljno()
        {
            if (!uslugaDataGridView.RowCount.Equals(0))
            {
                usluga_IDTextBox.Text = uslugaDataGridView.CurrentRow.Cells["Usluga_ID"].Value.ToString();
                staraSifraTextBox.Text = uslugaDataGridView.CurrentRow.Cells["Stara šifra"].Value.ToString();
                nazivTextBox.Text = uslugaDataGridView.CurrentRow.Cells["Naziv"].Value.ToString();
                cenaUslugeTextBox.Text = uslugaDataGridView.CurrentRow.Cells["Cena"].Value.ToString();
                normaSatiTextBox.Text = uslugaDataGridView.CurrentRow.Cells["Norma sati"].Value.ToString();
                napomenaRichTextBox.Text = uslugaDataGridView.CurrentRow.Cells["Napomena"].Value.ToString();
            }
        }

        #endregion

        #region IsprazniUsluguDetaljno()

        private void IsprazniUsluguDetaljno()
        {
            usluga_IDTextBox.Text = "";
            staraSifraTextBox.Text = "";
            nazivTextBox.Text = "";
            cenaUslugeTextBox.Text = "";
            normaSatiTextBox.Text = "";
            napomenaRichTextBox.Text = "";
        }

        #endregion

        #region Dogadjaji

        private void uslugaDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            PrikaziUsluguDetaljno(e.RowIndex);
        }

        private void uslugaDataGridView_Sorted(object sender, EventArgs e)
        {
            if (!uslugaDataGridView.RowCount.Equals(0))
            {
                this.uslugaDataGridView.FirstDisplayedCell = this.uslugaDataGridView.CurrentCell;
                uslugaDataGridView.CurrentCell = uslugaDataGridView[0, 0];
            }
        }

        private void napomenaRichTextBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Napomena napomena = new Napomena(napomenaRichTextBox);
            napomena.ShowDialog();

        }

        #endregion

        #region Load

        private void Usluga_Load(object sender, EventArgs e)
        {
            try
            {
                //DBUsluga.DajSveUsluge(uslugaDataSet.vwUsluga);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!uslugaDataGridView.Rows.Count.Equals(0))
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
            IsprazniUsluguDetaljno();
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
            //koristi se kod provere da li je vrednost polja CenaBezPoreza decimal (TryParse)
            Decimal _d;

            uslugaErrorProvider.Clear();

            #region Provera obaveznih polja

            if (staraSifraTextBox.Text == "")
            {
                uslugaErrorProvider.SetError(staraSifraTextBox, "Obavezan podatak.");
                staraSifraTextBox.Select();
            }
            else if (nazivTextBox.Text == "")
            {
                uslugaErrorProvider.SetError(nazivTextBox, "Obavezan podatak.");
                nazivTextBox.Select();
            }
            else if (cenaUslugeTextBox.Text == "")
            {
                uslugaErrorProvider.SetError(cenaUslugeTextBox, "Obavezan podatak.");
                cenaUslugeTextBox.Select();
            }
            else if (normaSatiTextBox.Text == "")
            {
                uslugaErrorProvider.SetError(normaSatiTextBox, "Obavezan podatak.");
                normaSatiTextBox.Select();
            }

            #endregion

            #region ProveraTipaPodataka

            //provera da li poslata vrednost decimal
            else if (!Decimal.TryParse(cenaUslugeTextBox.Text, out _d))
            {
                uslugaErrorProvider.SetError(cenaUslugeTextBox, "Vrednost uneta u polje Cena usluge mora biti broj.");
                cenaUslugeTextBox.Select();
            }
            //provera da li poslata vrednost ima vise od 16 cifara i dve decimale 
            else if (decimal.Parse(cenaUslugeTextBox.Text) > decimal.Parse("9999999999999999,99"))
            {
                uslugaErrorProvider.SetError(cenaUslugeTextBox, "Vrednost uneta u polje Cena usluge može imati najviše 16 cifara i dve decimale.");
                cenaUslugeTextBox.Select();
            }

            //provera da li poslata vrednost decimal
            else if (!Decimal.TryParse(normaSatiTextBox.Text, out _d))
            {
                uslugaErrorProvider.SetError(normaSatiTextBox, "Vrednost uneta u polje Norma sati mora biti broj.");
                normaSatiTextBox.Select();
            }
            //provera da li poslata vrednost ima vise od 16 cifara i dve decimale 
            else if (decimal.Parse(normaSatiTextBox.Text) > decimal.Parse("999,99"))
            {
                uslugaErrorProvider.SetError(normaSatiTextBox, "Vrednost uneta u polje Norma sati može imati najviše 3 cifre i dve decimale.");
                normaSatiTextBox.Select();
            }

            #endregion

            #region Provera ChkConnstraintia

            //provera da li je poslata vrednost pozitivan broj 
            else if (decimal.Parse(cenaUslugeTextBox.Text) < 0)
            {
                uslugaErrorProvider.SetError(cenaUslugeTextBox, "Vrednost uneta u polje Cena usluge ne može biti negativan broj.");
                cenaUslugeTextBox.Select();
            }
            //provera da li je poslata vrednost pozitivan broj 
            else if (decimal.Parse(normaSatiTextBox.Text) < 0)
            {
                uslugaErrorProvider.SetError(normaSatiTextBox, "Vrednost uneta u polje Norma sati ne može biti negativan broj.");
                normaSatiTextBox.Select();
            }

            #endregion

            //ako su podaci na formi ispravno uneti
            else
            {
                try
                {
                    //ako je polje Sifra prazno onda je Insert 
                    if (usluga_IDTextBox.Text == "")
                    {
                        #region Unos

                        //unesi red i uzmi ID unetog reda
                        Int32 _usluga_ID = DBUsluga.UnesiUslugu(staraSifraTextBox.Text, nazivTextBox.Text, Decimal.Parse(cenaUslugeTextBox.Text), Decimal.Parse(normaSatiTextBox.Text), napomenaRichTextBox.Text);

                        //DBUsluga.DajSveUsluge(uslugaDataSet.vwUsluga);
                        string _SQLUpit = DajSQLUpit() + " union  SELECT Usluga_ID, [Stara šifra], Naziv, Cena, [Norma sati], Napomena FROM vwUsluga where Usluga_ID = " + _usluga_ID;
                        DBUsluga.NadjiUslugu(uslugaDataSet.vwUsluga, _SQLUpit);

                        //poziv metode koja vraca ID unetog reda ili -1
                        int _indexUnetogReda = PomocneKlase.Index.DajIndexReda(uslugaDataGridView, "Usluga_ID", _usluga_ID.ToString());

                        //ako je nasao index upravo unetog reda  i ako je taj index validan.
                        if ((_indexUnetogReda != -1) && (_indexUnetogReda < uslugaDataGridView.RowCount))
                        {
                            //podesava da CurrentCell bude na redu koji je unet
                            this.uslugaDataGridView.CurrentCell = this.uslugaDataGridView[0, _indexUnetogReda];
                        }
                        #endregion
                    }
                    //ako polje Sifra dokumenta nije prazno onda Update
                    else
                    {
                        #region Izmena

                        DBUsluga.IzmeniUslugu(Int32.Parse(usluga_IDTextBox.Text) , staraSifraTextBox.Text, nazivTextBox.Text, Decimal.Parse(cenaUslugeTextBox.Text), Decimal.Parse(normaSatiTextBox.Text), napomenaRichTextBox.Text);

                        Int32 _sifraIzmenjeneUsluge = Int32.Parse(usluga_IDTextBox.Text);


                        //DBUsluga.DajSveUsluge(uslugaDataSet.vwUsluga);
                        string _SQLUpit = DajSQLUpit() + " union  SELECT Usluga_ID, [Stara šifra], Naziv, Cena, [Norma sati], Napomena FROM vwUsluga where Usluga_ID = " + _sifraIzmenjeneUsluge;
                        DBUsluga.NadjiUslugu(uslugaDataSet.vwUsluga, _SQLUpit);

                        //poziv metode koja vraca index promenjenog  reda ili -1
                        int _indexIzmenjenogReda = PomocneKlase.Index.DajIndexReda(uslugaDataGridView, "Usluga_ID", _sifraIzmenjeneUsluge.ToString());

                        //ako je nasao index upravo izmenjenog reda i ako je taj index validan. Ako prvi uslov nije tacan drugi se ne proverava
                        if ((_indexIzmenjenogReda != -1) && (_indexIzmenjenogReda < uslugaDataGridView.RowCount))
                        {
                            //podesava da CurrentCell bude na redu koji je izmenjen
                            this.uslugaDataGridView.CurrentCell = this.uslugaDataGridView[0, _indexIzmenjenogReda];
                        }

                        #endregion
                    }

                    if (!uslugaDataGridView.Rows.Count.Equals(0))
                    {
                        UStanje("Detaljno");
                    }
                    else
                    {
                        IsprazniUsluguDetaljno();
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
            uslugaErrorProvider.Clear();

            if (!uslugaDataGridView.Rows.Count.Equals(0))
            {
                PrikaziUsluguDetaljno();
                UStanje("Detaljno");
            }
            else
            {
                IsprazniUsluguDetaljno();
                UStanje("Osnovno");
            }
        }

        #endregion

        #region Obrisi

        private void obrisiButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da obrišete uslugu?",
                    "Potvrdi brisanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    DBUsluga.ObrisiUslugu(Int16.Parse(usluga_IDTextBox.Text));

                    //DBUsluga.DajSveUsluge(uslugaDataSet.vwUsluga);
                    DBUsluga.NadjiUslugu(uslugaDataSet.vwUsluga, DajSQLUpit());

                    if (!uslugaDataGridView.Rows.Count.Equals(0))
                    {
                        UStanje("Detaljno");
                    }
                    else
                    {
                        IsprazniUsluguDetaljno();
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
                    int _brojRedova = DBUsluga.IzveziUsluge(exportSaveFileDialog.FileName);

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
            string _cenaUsluge = "";//_kolona = 3
            string _normaSati = "";//_kolona = 4
            string _napomena = "";//_kolona = 5


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
                        DB.InkrementalniKljuc.ResetujInkrementalniKljuc("Usluga_ID", "Usluga", _konekcijaSqlConnection);
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
                            if (_staraSifra != "")
                            {
                                string _c = _staraSifra + "\t" + _naziv + "\t" + _cenaUsluge + "\t" + _normaSati + "\t" + _napomena;

                                _ukupanBrojRedova++;

                                try
                                {
                                    DBUsluga.UveziUslugu(_staraSifra, _naziv, Decimal.Parse(_cenaUsluge), Decimal.Parse(_normaSati), _napomena);
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
                            _cenaUsluge = "";//_kolona = 3
                            _normaSati = "";//_kolona = 4
                            _napomena = "";//_kolona = 5


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
                                    _cenaUsluge = _cenaUsluge + _karakter.ToString();
                                    break;
                                case 4:
                                    _normaSati = _normaSati + _karakter.ToString();
                                    break;
                                case 5:
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
                    DBUsluga.NadjiUslugu(uslugaDataSet.vwUsluga, DajSQLUpit());

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (!uslugaDataGridView.Rows.Count.Equals(0))
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

        #endregion

        #region DajSQLUpit

        private string DajSQLUpit()
        {
            string _SQLUpit = " SELECT Usluga_ID, [Stara šifra], Naziv, Cena, [Norma sati], Napomena FROM vwUsluga ";

            string _uslovNaziv = "";

            bool _prviUslov = true;


            if ((uslovSlicnoTrazenjeSadrziCheckBox.Checked) && (uslovNazivTextBox.Text != ""))
            {
                _uslovNaziv = " Naziv like '%" + uslovNazivTextBox.Text + "%'";
            }
            else if ((!uslovSlicnoTrazenjeSadrziCheckBox.Checked) && (uslovNazivTextBox.Text != ""))
            {
                _uslovNaziv = " Naziv = '" + uslovNazivTextBox.Text + "'";
            }


            if (_uslovNaziv != "")
            {
                if (_prviUslov)
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



            return _SQLUpit;
        }

        #endregion


        #region Nadji
        private void nadjiButton_Click(object sender, EventArgs e)
        {
            try
            {
                DBUsluga.NadjiUslugu(uslugaDataSet.vwUsluga, DajSQLUpit());

            }
            catch (Exception ex)
            {
                MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!uslugaDataGridView.Rows.Count.Equals(0))
                {
                    UStanje("Detaljno");
                }
                else
                {
                    IsprazniUsluguDetaljno();
                    UStanje("Osnovno");
                }
            }
        } 
	    #endregion

        #region Resetuj
        private void resetujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uslovNazivTextBox.Text = "";
            uslovSlicnoTrazenjeSadrziCheckBox.Checked = false;
        } 
        #endregion

              
    }
}