using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using System.IO;
using System.Threading;

namespace LAV
{
    public partial class Artikal : Form
    {
        DB.Artikal DBPretraga = new DB.Artikal();
        DB.Cenovnik DBCenovnik = new DB.Cenovnik();
        LavSettings _LavSettings = new LavSettings();

        TextBox ArtikalTextBox = null;
        TextBox CenaTextBox = null;
        TextBox CenaUgradnjeTextBox = null;
        TextBox NormaSatiTextBox = null;
        ErrorProvider PonudaErrorProvider = null;

        public Artikal()
        {
            InitializeComponent();
            Inicijalizuj();
        }
        public Artikal(ErrorProvider ponudaErrorProvider ,TextBox artikalTextBox, TextBox cena, TextBox cenaUgradnje, TextBox normaSati):this()
        {
            this.ControlBox = true;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            ArtikalTextBox = artikalTextBox;
            CenaTextBox = cena;
            CenaUgradnjeTextBox = cenaUgradnje;
            NormaSatiTextBox = normaSati;
            PonudaErrorProvider = ponudaErrorProvider;

            this.artikalDataGridView.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(artikalDataGridView_CellMouseDoubleClick);
        }
        
        public Artikal(ErrorProvider ponudaErrorProvider ,TextBox artikalTextBox, TextBox cena, TextBox cenaUgradnje, TextBox normaSati, Int32 artikal_ID):this()
        {
            this.ControlBox = true;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            ArtikalTextBox = artikalTextBox;
            CenaTextBox = cena;
            CenaUgradnjeTextBox = cenaUgradnje;
            NormaSatiTextBox = normaSati;
            PonudaErrorProvider = ponudaErrorProvider;

            this.artikalDataGridView.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(artikalDataGridView_CellMouseDoubleClick);

            try
            {
                DBPretraga.NadjiArtikal(artikalDataSet.vwArtikal, artikal_ID);
            }
            catch (Exception)
            {
                //nista
            }
        }

        void artikalDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            #region DajDobavljaceZaArtikal - radi na SPACE a u ovoj verziji izbaceno sa ovog dogadjaja
            //if (!artikalDataGridView.RowCount.Equals(0))
            //{
            //    this.Cursor = Cursors.WaitCursor;
            //    try
            //    {
            //        DBPretraga.DajDobavljaceZaArtikal(artikalDataSet.vwVezaArtikalDobavljac, Int32.Parse(artikalDataGridView.CurrentRow.Cells["Artikal_ID"].Value.ToString()));

            //        DBPretraga.PovecajBrojac(Int32.Parse(artikalDataGridView.CurrentRow.Cells["Artikal_ID"].Value.ToString()));
            //    }
            //    catch (Exception ex)
            //    {
            //        this.Cursor = Cursors.Default;

            //        MessageBox.Show(ex.Message, "LAV", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //    this.Cursor = Cursors.Default;
            //}
            #endregion       

            if ((ArtikalTextBox != null) && (!artikalDataGridView.Rows.Count.Equals(0)))
            {
                ArtikalTextBox.Text = artikalDataGridView.Rows[artikalDataGridView.CurrentCell.RowIndex].Cells["Naziv proizvoda"].Value.ToString(); ;
                CenaTextBox.Text = artikalDataGridView.Rows[artikalDataGridView.CurrentCell.RowIndex].Cells["Cena"].Value.ToString(); ;
                CenaUgradnjeTextBox.Text = artikalDataGridView.Rows[artikalDataGridView.CurrentCell.RowIndex].Cells["Cena ugradnje"].Value.ToString(); ;
                NormaSatiTextBox.Text = artikalDataGridView.Rows[artikalDataGridView.CurrentCell.RowIndex].Cells["Norma sati"].Value.ToString(); ;

                ArtikalTextBox.Tag = artikalDataGridView.Rows[artikalDataGridView.CurrentCell.RowIndex].Cells["Artikal_ID"].Value.ToString();
                PonudaErrorProvider.Clear();
            }

            artikalDataSet.Clear();
            artikalDataSet.Dispose();

            this.Close();
        }

        #region Inicijalizuj
        void Inicijalizuj()
        {
            biloKojiBrojCheckBox.Checked = true;
            brojProizvodjacaCheckBox.Enabled = false;
            koriscenBrojCheckBox.Enabled = false;
            oEBrojCheckBox.Enabled = false;
            uporedniBrojCheckBox.Enabled = false;
            eANBrojCheckBoxe.Enabled = false;

            artikalDataGridView.DataSource = artikalDataSet.vwArtikal;

            //DataGridView podesavanja
            artikalDataGridView.MultiSelect = false;
            artikalDataGridView.AllowUserToAddRows = false;
            artikalDataGridView.AllowUserToResizeRows = false;
            artikalDataGridView.AllowUserToDeleteRows = false;
            artikalDataGridView.ReadOnly = true;
            //artikalDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            artikalDataGridView.AllowUserToOrderColumns = true;
            artikalDataGridView.Columns["Artikal_ID"].Visible = false;
            artikalDataGridView.Columns["Korisnik programa"].Visible = false;
            artikalDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            vezaArtikalDobavljacDataGridView.DataSource = artikalDataSet.vwVezaArtikalDobavljac;

            //DataGridView podesavanja
            vezaArtikalDobavljacDataGridView.MultiSelect = false;
            vezaArtikalDobavljacDataGridView.AllowUserToAddRows = false;
            vezaArtikalDobavljacDataGridView.AllowUserToResizeRows = false;
            vezaArtikalDobavljacDataGridView.AllowUserToDeleteRows = false;
            vezaArtikalDobavljacDataGridView.ReadOnly = true;
            //vezaArtikalDobavljacDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            vezaArtikalDobavljacDataGridView.AllowUserToOrderColumns = true;
            vezaArtikalDobavljacDataGridView.Columns["Artikal_ID"].Visible = false;
            vezaArtikalDobavljacDataGridView.Columns["PoslovniPartner_ID"].Visible = false;
            vezaArtikalDobavljacDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            brojZaPretraguComboBox.MaxDropDownItems = 10;

            slicnoTrazenjeCheckBox.Checked = true;
        }
        
        #endregion

        #region Dogadjaji

        private void biloKojiBrojCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (biloKojiBrojCheckBox.Checked)
            {
                brojProizvodjacaCheckBox.Enabled = false;
                koriscenBrojCheckBox.Enabled = false;
                oEBrojCheckBox.Enabled = false;
                uporedniBrojCheckBox.Enabled = false;
                eANBrojCheckBoxe.Enabled = false;

                brojProizvodjacaCheckBox.Checked = false;
                koriscenBrojCheckBox.Checked = false;
                oEBrojCheckBox.Checked = false;
                uporedniBrojCheckBox.Checked = false;
                eANBrojCheckBoxe.Checked = false;
            }
            else
            {
                brojProizvodjacaCheckBox.Enabled = true;
                koriscenBrojCheckBox.Enabled = true;
                oEBrojCheckBox.Enabled = true;
                uporedniBrojCheckBox.Enabled = true;
                eANBrojCheckBoxe.Enabled = true;

                brojProizvodjacaCheckBox.Checked = true;

            }

        }

        private void brojeviCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if ((!brojProizvodjacaCheckBox.Checked) && (!koriscenBrojCheckBox.Checked) && (!oEBrojCheckBox.Checked) && (!uporedniBrojCheckBox.Checked) && (!eANBrojCheckBoxe.Checked))
            {
                brojProizvodjacaCheckBox.Enabled = false;
                koriscenBrojCheckBox.Enabled = false;
                oEBrojCheckBox.Enabled = false;
                uporedniBrojCheckBox.Enabled = false;
                eANBrojCheckBoxe.Enabled = false;

                biloKojiBrojCheckBox.Checked = true;
            }

        }

        private void artikalDataGridView_KeyUp(object sender, KeyEventArgs e)
        {
            //MessageBox.Show(e.KeyData.ToString());

            if (e.KeyData.ToString() == "Escape")
            {
                brojZaPretraguComboBox.Select();
                brojZaPretraguComboBox.SelectAll();
            }
            else if (e.KeyData.ToString() == "Space")
            {
                if (!artikalDataGridView.RowCount.Equals(0))
                {
                    this.Cursor = Cursors.WaitCursor;
                    try
                    {
                        DBPretraga.DajDobavljaceZaArtikal(artikalDataSet.vwVezaArtikalDobavljac, Int32.Parse(artikalDataGridView.CurrentRow.Cells["Artikal_ID"].Value.ToString()));

                        DBPretraga.PovecajBrojac(Int32.Parse(artikalDataGridView.CurrentRow.Cells["Artikal_ID"].Value.ToString()));
                    }
                    catch (Exception ex)
                    {
                        this.Cursor = Cursors.Default;

                        MessageBox.Show(ex.Message, "LAV", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    this.Cursor = Cursors.Default;
                }
            }
        }

        private void artikalDataGridView_Paint(object sender, PaintEventArgs e)
        {
            if (!artikalDataGridView.RowCount.Equals(0))
            {
                foreach (DataGridViewRow var in artikalDataGridView.Rows)
                {
                    if (var.Cells["Korisnik programa"].Value.ToString() != "")
                    {
                        var.DefaultCellStyle.BackColor = Color.Bisque;

                        //var.Cells["Najpovoljniji dobavljač"].Style.BackColor = Color.Bisque;
                    }
                }
            }
        }

        #endregion

        #region DajOciscenString()
        private string DajOciscenString(string stringZaCiscenje)
        {
            string[] _karakteriZaIzbacivanje = { ".", ",", "-", " ", "/" };

            StringBuilder _sb = new StringBuilder(stringZaCiscenje);

            foreach (string _s in _karakteriZaIzbacivanje)
            {
                _sb.Replace(_s, "");
            }

            return _sb.ToString();
        } 
        #endregion

        #region DajSQLUpit

        private string DajSQLUpit()
        {
            string _uslovBrojZaPretragu = "";

            if (slicnoTrazenjeCheckBox.Checked)
            {
                _uslovBrojZaPretragu = " like '%" + DajOciscenString(brojZaPretraguComboBox.Text) + "%'";
            }
            else
            {
                _uslovBrojZaPretragu = " = '" + DajOciscenString(brojZaPretraguComboBox.Text) + "'";
            }

            //string _sQLUpit =
            //    " SELECT vwArtikal.Artikal_ID, vwArtikal.Proizvođač, vwArtikal.[Broj proizvođača], vwArtikal.Naziv, vwArtikal.Napomena " +
            //        " FROM vwArtikal INNER JOIN " +
            //          " BrojZaPretragu ON vwArtikal.Artikal_ID = BrojZaPretragu.Artikal_ID " +
            //            " WHERE  (BrojZaPretragu.Broj " + _uslovBrojZaPretragu + " ) ";

            string _sQLUpit =
                " SELECT vwArtikal.Artikal_ID, vwArtikal.Proizvođač, vwArtikal.[Broj proizvoda], vwArtikal.[Naziv proizvoda], vwArtikal.[Cena ugradnje], vwArtikal.[Norma sati], vwArtikal.Cena, vwArtikal.[Najpovoljniji dobavljač], vwArtikal.[Korisnik programa], vwArtikal.Napomena " +
                    " FROM  vwArtikal INNER JOIN " + 
                      " BrojZaPretragu ON vwArtikal.Artikal_ID = BrojZaPretragu.Artikal_ID " +
                       " WHERE  (BrojZaPretragu.Broj " + _uslovBrojZaPretragu + " ) ";

            string _uslovVrstaBrojaZaPretragu = "";
            //AND (BrojZaPretragu.VrstaBrojaZaPretragu_ID IN ("++")) 

            if (!biloKojiBrojCheckBox.Checked)
            {
                bool _prvi = true;


                if (brojProizvodjacaCheckBox.Checked)
                {
                    if (_prvi)
                    {
                        _uslovVrstaBrojaZaPretragu = _uslovVrstaBrojaZaPretragu + " 1 ";
                        _prvi = false;
                    }
                    else
                    {
                        _uslovVrstaBrojaZaPretragu = _uslovVrstaBrojaZaPretragu + " ,1 ";
                    }
                }

                if (koriscenBrojCheckBox.Checked)
                {
                    if (_prvi)
                    {
                        _uslovVrstaBrojaZaPretragu = _uslovVrstaBrojaZaPretragu + " 2 ";
                        _prvi = false;
                    }
                    else
                    {
                        _uslovVrstaBrojaZaPretragu = _uslovVrstaBrojaZaPretragu + " ,2 ";
                    }
                }

                if (oEBrojCheckBox.Checked)
                {
                    if (_prvi)
                    {
                        _uslovVrstaBrojaZaPretragu = _uslovVrstaBrojaZaPretragu + " 3 ";
                        _prvi = false;
                    }
                    else
                    {
                        _uslovVrstaBrojaZaPretragu = _uslovVrstaBrojaZaPretragu + " ,3 ";
                    }
                }

                if (uporedniBrojCheckBox.Checked)
                {
                    if (_prvi)
                    {
                        _uslovVrstaBrojaZaPretragu = _uslovVrstaBrojaZaPretragu + " 4 ";
                        _prvi = false;
                    }
                    else
                    {
                        _uslovVrstaBrojaZaPretragu = _uslovVrstaBrojaZaPretragu + " ,4 ";
                    }
                }

                if (eANBrojCheckBoxe.Checked)
                {
                    if (_prvi)
                    {
                        _uslovVrstaBrojaZaPretragu = _uslovVrstaBrojaZaPretragu + " 5 ";
                        _prvi = false;
                    }
                    else
                    {
                        _uslovVrstaBrojaZaPretragu = _uslovVrstaBrojaZaPretragu + " ,5 ";
                    }
                }

            }

            if (_uslovVrstaBrojaZaPretragu != "")
            {
                _sQLUpit = _sQLUpit + " AND (BrojZaPretragu.VrstaBrojaZaPretragu_ID IN (" + _uslovVrstaBrojaZaPretragu + ")) ";
            }

            //_sQLUpit = _sQLUpit +
            //    " group by vwArtikal.Artikal_ID, vwArtikal.Proizvođač, vwArtikal.[Broj proizvođača], vwArtikal.Naziv, vwArtikal.Napomena " +
            //            " order by vwArtikal.Proizvođač ";
            _sQLUpit = _sQLUpit +
                " group by vwArtikal.Artikal_ID, vwArtikal.Proizvođač, vwArtikal.[Broj proizvoda], vwArtikal.[Naziv proizvoda], vwArtikal.[Cena ugradnje], vwArtikal.[Norma sati], vwArtikal.Cena, vwArtikal.[Najpovoljniji dobavljač],vwArtikal.[Korisnik programa] , vwArtikal.Napomena " +
                        " order by vwArtikal.Cena desc, vwArtikal.Proizvođač ";


            return _sQLUpit;
        }

        #endregion

        #region Nadji
        private void nadjiButton_Click(object sender, EventArgs e)
        {
            if (brojZaPretraguComboBox.Text != "")
            {
                artikalDataSet.vwVezaArtikalDobavljac.Clear();

                this.Cursor = Cursors.WaitCursor;
                try
                {
                    DBPretraga.NadjiArtikal(artikalDataSet.vwArtikal, DajSQLUpit());
                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Default;

                    MessageBox.Show(ex.Message, "LAV", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                this.Cursor = Cursors.Default;

                //ako je rezultat nula artikala obavesti korisnika
                if (artikalDataSet.vwArtikal.Rows.Count.Equals(0))
                {
                    MessageBox.Show("Za zadati broj nije nađen ni jedan artikal. ", "LAV", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    brojZaPretraguComboBox.Select();
                }
                else
                {
                    if (brojZaPretraguComboBox.Items.IndexOf(brojZaPretraguComboBox.Text).Equals(-1))
                    {
                        brojZaPretraguComboBox.Items.Insert(0, brojZaPretraguComboBox.Text);
                    }
                    else
                    {
                        string _s = brojZaPretraguComboBox.Text;

                        brojZaPretraguComboBox.Items.RemoveAt(brojZaPretraguComboBox.Items.IndexOf(brojZaPretraguComboBox.Text));

                        brojZaPretraguComboBox.Text = _s;
                        brojZaPretraguComboBox.Items.Insert(0, brojZaPretraguComboBox.Text);

                    }
                    //ako ima 11 obrisi 10
                    if(brojZaPretraguComboBox.Items.Count.Equals(11))
                    {
                        brojZaPretraguComboBox.Items.RemoveAt(10);
                    }

                    artikalDataGridView.Select();
                }
            }
            else
            {
                MessageBox.Show("Unesi broj za pretragu. ", "LAV", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


        } 
        #endregion

        #region napomena
        private void dodatniPodaciToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!artikalDataGridView.RowCount.Equals(0))
            {
                DodatniPodaci dodatniPodaci = new DodatniPodaci(artikalDataGridView.CurrentRow);
                dodatniPodaci.ShowDialog();
            }
        } 
        #endregion

        #region Brojac

        private void brojacToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                DBPretraga.DajBrojac(artikalDataSet.uspDajBrojac);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "LAV", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Izvestaji.Brojac brojac = new LAV.Izvestaji.Brojac(artikalDataSet.uspDajBrojac);
            brojac.WindowState = FormWindowState.Maximized;
            brojac.Show();
        } 
        #endregion

        #region UveziCenovnik

        private void uveziCenovnikToolStripButton_Click(object sender, EventArgs e)
        {
            int _ukupanBrojRedova = 0;
            int _brojUnetih = 0;
            int _brojGresaka = 0;
            int _brojNeNadjenih = 0;

            char _karakter;

            int _kolona = 1;

            string _poslovniPartner = "";//_kolona = 1
            string _proizvodjac = "";//_kolona = 2
            string _brojProizvodjaca = "";//_kolona = 3
            string _cena = "";//_kolona = 4
            string _cenaUgradnje = "";//_kolona = 5
            string _normaSati = "";//_kolona = 6
            string _napomena = "";//_kolona = 7


            OpenFileDialog otvoriCenovnikOpenFileDialog = new OpenFileDialog();
            otvoriCenovnikOpenFileDialog.Filter = "TXT fajl (*.txt)|*.txt";
            otvoriCenovnikOpenFileDialog.Title = "Otvori cenovnik";
            otvoriCenovnikOpenFileDialog.RestoreDirectory = true;

            if (otvoriCenovnikOpenFileDialog.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = System.Windows.Forms.Cursors.WaitCursor;

                this.Refresh();

                DateTime _pocetakUpita;
                TimeSpan _vremeTrajanja;
                _pocetakUpita = DateTime.Now;


                StringBuilder _putanjaZaGresku = new StringBuilder(otvoriCenovnikOpenFileDialog.FileName);
                StringBuilder _putanjaZaNeNadjeno = new StringBuilder(otvoriCenovnikOpenFileDialog.FileName);

                StreamReader _cenovnikStreamReader = new StreamReader(otvoriCenovnikOpenFileDialog.FileName);

                StreamWriter _greskaStreamWriter = new StreamWriter(_putanjaZaGresku.Insert(_putanjaZaGresku.Length - 4, "_Greska").ToString());
                StreamWriter _nijeNadjenoStreamWriter = new StreamWriter(_putanjaZaNeNadjeno.Insert(_putanjaZaNeNadjeno.Length - 4, "_NijeNadjeno").ToString());

                while (_cenovnikStreamReader.Peek() >= 0)
                {
                    _karakter = (char)_cenovnikStreamReader.Read();

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
                            if (_poslovniPartner != "")
                            {
                                string _c = _poslovniPartner + "\t" + _proizvodjac + "\t" + _brojProizvodjaca + "\t" + _cena + "\t" + _cenaUgradnje + "\t" + _normaSati + "\t" + _napomena;
                                //TODO upisi vrednost u bazu  

                                _ukupanBrojRedova++;

                                try
                                {
                                    //ili oba ili ni jedan - ako je bar jedan prazan nece uci u obradu ni jedan
                                    if (_cenaUgradnje == "" || _normaSati == "")
                                    {
                                        if (DBCenovnik.ImportCenovnika(_poslovniPartner, _proizvodjac, _brojProizvodjaca, Decimal.Parse(_cena), _napomena) == -1)
                                        {
                                            _nijeNadjenoStreamWriter.WriteLine(_c.ToCharArray());
                                            _brojNeNadjenih++;
                                        }
                                        else
                                        {
                                            _brojUnetih++;
                                        }
                                    }
                                    // ako su oba puna
                                    else
                                    {
                                        if (DBCenovnik.ImportCenovnika(_poslovniPartner, _proizvodjac, _brojProizvodjaca, Decimal.Parse(_cena), Decimal.Parse(_cenaUgradnje), Decimal.Parse(_normaSati), _napomena) == -1)
                                        {
                                            _nijeNadjenoStreamWriter.WriteLine(_c.ToCharArray());
                                            _brojNeNadjenih++;
                                        }
                                        else
                                        {
                                            _brojUnetih++;
                                        }
                                    }

                                }
                                catch (Exception ex)
                                {
                                    _brojGresaka++;

                                    _c = _c + "\t" + ex.Message;

                                    _greskaStreamWriter.WriteLine(_c.ToCharArray());
                                }
                            }
                            _poslovniPartner = "";//_kolona = 1
                            _proizvodjac = "";//_kolona = 2
                            _brojProizvodjaca = "";//_kolona = 3
                            _cena = "";//_kolona = 4
                            _cenaUgradnje = "";//_kolona = 5
                            _normaSati = "";//_kolona = 6
                            _napomena = "";//_kolona = 7


                            _kolona = 1;
                        }
                        else
                        {
                            switch (_kolona)
                            {
                                case 1:
                                    _poslovniPartner = _poslovniPartner + _karakter.ToString();
                                    break;
                                case 2:
                                    _proizvodjac = _proizvodjac + _karakter.ToString();
                                    break;
                                case 3:
                                    _brojProizvodjaca = _brojProizvodjaca + _karakter.ToString();
                                    break;
                                case 4:
                                    _cena = _cena + _karakter.ToString();
                                    break;
                                case 5:
                                    _cenaUgradnje = _cenaUgradnje + _karakter.ToString();
                                    break;
                                case 6:
                                    _normaSati = _normaSati + _karakter.ToString();
                                    break;
                                case 7:
                                    _napomena = _napomena + _karakter.ToString();
                                    break;

                            }
                        }
                    }
                }

                _greskaStreamWriter.Close();
                _nijeNadjenoStreamWriter.Close();

                _vremeTrajanja = DateTime.Now - _pocetakUpita;
                this.Cursor = System.Windows.Forms.Cursors.Default;

                MessageBox.Show("Ukupan broj redova = " + _ukupanBrojRedova + "\nBroj unetih = " + _brojUnetih + "\nBroj ne nađenih = " + _brojNeNadjenih + "\nBroj grešaka = " + _brojGresaka + "\n\nVreme unosa cenovnika = " + _vremeTrajanja + "                                ", "LAV", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        #endregion

        #region Export
        private void exportToolStripButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog exportSaveFileDialog = new SaveFileDialog();
            exportSaveFileDialog.Filter = "TXT fajl (*.txt)|*.txt";
            exportSaveFileDialog.Title = "Sačuvaj cenovnik";
            exportSaveFileDialog.RestoreDirectory = true;

            if (exportSaveFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    int _brojRedova = DBCenovnik.ExportCenovnika(exportSaveFileDialog.FileName);

                    MessageBox.Show("Uspešno je sačuvano " + _brojRedova.ToString() + " redova.     ", "LAV", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "LAV", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        } 
        #endregion

        private void Artikal_Load(object sender, EventArgs e)
        {
            //ocisti podatke o uspesnim pretragama
            //_LavSettings._UspesnePretrage.Clear();
            //_LavSettings.Save();
            ////ako ima samo jedan i ako je on "1" onda ga obrisi
            //if (_LavSettings._UspesnePretrage.Count.Equals(1) && _LavSettings._UspesnePretrage[0].ToString() == "1")
            //{
            //    _LavSettings._UspesnePretrage.RemoveAt(0);
            //    _LavSettings.Save();
            //}

            int _i = 0;

            foreach (string var in _LavSettings._UspesnePretrage)
            {
                brojZaPretraguComboBox.Items.Insert(_i, var);
                _i++;
            }
        }

        private void Artikal_FormClosed(object sender, FormClosedEventArgs e)
        {
            _LavSettings._UspesnePretrage.Clear();
            _LavSettings.Save();

            foreach (object var in brojZaPretraguComboBox.Items)
            {
                _LavSettings._UspesnePretrage.Add(var.ToString());
            }

            _LavSettings.Save();

        }

        
    }
}