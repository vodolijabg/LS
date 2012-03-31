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
    public partial class Radnik : Form
    {
        DB.Radnik DBRadnik = new DB.Radnik();

        public Radnik()
        {
            InitializeComponent();

            Inicijalizuj();
        }

        #region Inicijalizuj()

        private void Inicijalizuj()
        {
            //Bindovanje DataGridView-a
            radnikBindingNavigator.BindingSource = radnikBindingSource;
            radnikBindingSource.DataMember = radnikDataSet.vwRadnik.DefaultView.ToString();
            radnikBindingSource.DataSource = radnikDataSet.vwRadnik.DefaultView;
            radnikDataGridView.DataSource = radnikBindingSource;

            //DataGridView podesavanja
            radnikDataGridView.MultiSelect = false;
            radnikDataGridView.AllowUserToAddRows = false;
            radnikDataGridView.AllowUserToResizeRows = false;
            radnikDataGridView.AllowUserToDeleteRows = false;
            radnikDataGridView.Enabled = false;
            radnikDataGridView.ReadOnly = true;
            radnikDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            radnikDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


            //TabStop=false
            radnikDataGridView.TabStop = false;
            radnikBindingNavigator.TabStop = false;
            obrisiButton.TabStop = false;
            krajButton.TabStop = false;
            radnikDetaljnoTabControl.TabStop = false;

            //TabIndex
            staraSifraTextBox.TabIndex = 0;
            imeTextBox.TabIndex = 1;
            prezimeTextBox.TabIndex = 2;
            potvrdiButton.TabIndex = 3;
            odustaniButton.TabIndex = 4;

            //MaxLenght
            staraSifraTextBox.MaxLength = 10;
            imeTextBox.MaxLength = 20;
            prezimeTextBox.MaxLength = 20;

        }

        #endregion

        #region UStanje()

        private void UStanje(string stanje)
        {
            //mora ici pre dugmica
            staraSifraTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            imeTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            prezimeTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");

            importToolStripButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");
            exportToolStripButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");


            unesiButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");
            izmeniButton.Enabled = (stanje == "Detaljno");
            potvrdiButton.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            odustaniButton.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            obrisiButton.Enabled = (stanje == "Detaljno");
            krajButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");

            radnikDataGridView.Enabled = (stanje == "Detaljno");

            radnikBindingNavigator.Enabled = (stanje == "Detaljno");

            if ((stanje == "Unos") || (stanje == "Izmena"))
            {
                staraSifraTextBox.Select();
            }
        }

        #endregion

        #region PrikaziRadnikaDetaljno (indexReda)

        private void PrikaziRadnikaDetaljno(int indexReda)
        {
            radnik_IDTextBox.Text = radnikDataGridView.Rows[indexReda].Cells["Radnik_ID"].Value.ToString();
            staraSifraTextBox.Text = radnikDataGridView.Rows[indexReda].Cells["Stara šifra"].Value.ToString();
            imeTextBox.Text = radnikDataGridView.Rows[indexReda].Cells["Ime"].Value.ToString();
            prezimeTextBox.Text = radnikDataGridView.Rows[indexReda].Cells["Prezime"].Value.ToString();

        }

        #endregion

        #region PrikaziRadnikaDetaljno()

        private void PrikaziRadnikaDetaljno()
        {
            if (!radnikDataGridView.RowCount.Equals(0))
            {
                radnik_IDTextBox.Text = radnikDataGridView.CurrentRow.Cells["Radnik_ID"].Value.ToString();
                staraSifraTextBox.Text = radnikDataGridView.CurrentRow.Cells["Stara šifra"].Value.ToString();
                imeTextBox.Text = radnikDataGridView.CurrentRow.Cells["Ime"].Value.ToString();
                prezimeTextBox.Text = radnikDataGridView.CurrentRow.Cells["Prezime"].Value.ToString();

            }
        }

        #endregion

        #region IsprazniRadnikaDetaljno()

        private void IsprazniRadnikaDetaljno()
        {
            radnik_IDTextBox.Text = "";
            staraSifraTextBox.Text = "";
            imeTextBox.Text = "";
            prezimeTextBox.Text = "";
        }

        #endregion

        #region Dogadjaji

        private void radnikDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            PrikaziRadnikaDetaljno(e.RowIndex);
        }

        private void radnikDataGridView_Sorted(object sender, EventArgs e)
        {
            if (!radnikDataGridView.RowCount.Equals(0))
            {
                this.radnikDataGridView.FirstDisplayedCell = this.radnikDataGridView.CurrentCell;
                radnikDataGridView.CurrentCell = radnikDataGridView[0, 0];
            }
        }

        #endregion


        #region Load

        private void Radnik_Load(object sender, EventArgs e)
        {
            try
            {
                DBRadnik.DajSveRadnike(radnikDataSet.vwRadnik);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!radnikDataGridView.Rows.Count.Equals(0))
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
            IsprazniRadnikaDetaljno();
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
            radnikErrorProvider.Clear();

            #region Provera obaveznih polja

            if (staraSifraTextBox.Text == "")
            {
                radnikErrorProvider.SetError(staraSifraTextBox, "Obavezan podatak.");
                staraSifraTextBox.Select();
            }
            else if (imeTextBox.Text == "")
            {
                radnikErrorProvider.SetError(imeTextBox, "Obavezan podatak.");
                imeTextBox.Select();
            }
            else if (prezimeTextBox.Text == "")
            {
                radnikErrorProvider.SetError(prezimeTextBox, "Obavezan podatak.");
                prezimeTextBox.Select();
            }


            #endregion

            //ako su podaci na formi ispravno uneti
            else
            {

                try
                {
                    //ako je polje Sifra prazno onda je Insert 
                    if (radnik_IDTextBox.Text == "")
                    {
                        #region Unos

                        //unesi red i uzmi ID unetog reda
                        Int16 _radnik_ID = DBRadnik.UnesiRadnika(staraSifraTextBox.Text, imeTextBox.Text, prezimeTextBox.Text);

                        DBRadnik.DajSveRadnike(radnikDataSet.vwRadnik);

                        //poziv metode koja vraca ID unetog reda ili -1
                        int _indexUnetogReda = PomocneKlase.Index.DajIndexReda(radnikDataGridView, "Radnik_ID", _radnik_ID.ToString());

                        //ako je nasao index upravo unetog reda  i ako je taj index validan.
                        if ((_indexUnetogReda != -1) && (_indexUnetogReda < radnikDataGridView.RowCount))
                        {
                            //podesava da CurrentCell bude na redu koji je unet
                            this.radnikDataGridView.CurrentCell = this.radnikDataGridView[0, _indexUnetogReda];
                        }
                        #endregion

                    }
                    //ako polje Sifra dokumenta nije prazno onda Update
                    else
                    {
                        #region Izmena

                        DBRadnik.IzmeniRadnika(Int16.Parse(radnik_IDTextBox.Text), staraSifraTextBox.Text, imeTextBox.Text, prezimeTextBox.Text);

                        Int16 _sifraIzmenjenogRadnika = Int16.Parse(radnik_IDTextBox.Text);


                        DBRadnik.DajSveRadnike(radnikDataSet.vwRadnik);

                        //poziv metode koja vraca index promenjenog  reda ili -1
                        int _indexIzmenjenogReda = PomocneKlase.Index.DajIndexReda(radnikDataGridView, "Radnik_ID", _sifraIzmenjenogRadnika.ToString());

                        //ako je nasao index upravo izmenjenog reda i ako je taj index validan. Ako prvi uslov nije tacan drugi se ne proverava
                        if ((_indexIzmenjenogReda != -1) && (_indexIzmenjenogReda < radnikDataGridView.RowCount))
                        {
                            //podesava da CurrentCell bude na redu koji je izmenjen
                            this.radnikDataGridView.CurrentCell = this.radnikDataGridView[0, _indexIzmenjenogReda];
                        }

                        #endregion

                    }

                    if (!radnikDataGridView.Rows.Count.Equals(0))
                    {
                        UStanje("Detaljno");
                    }
                    else
                    {
                        IsprazniRadnikaDetaljno();
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
            radnikErrorProvider.Clear();

            if (!radnikDataGridView.Rows.Count.Equals(0))
            {
                PrikaziRadnikaDetaljno();
                UStanje("Detaljno");
            }
            else
            {
                IsprazniRadnikaDetaljno();
                UStanje("Osnovno");
            }
        }

        #endregion

        #region Obrisi

        private void obrisiButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da obrišete radnika?",
                    "Potvrdi brisanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    DBRadnik.ObrisiRadnika(Int16.Parse(radnik_IDTextBox.Text));

                    DBRadnik.DajSveRadnike(radnikDataSet.vwRadnik);

                    if (!radnikDataGridView.Rows.Count.Equals(0))
                    {
                        UStanje("Detaljno");
                    }
                    else
                    {
                        IsprazniRadnikaDetaljno();
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
                    int _brojRedova = DBRadnik.ExportRadnika(exportSaveFileDialog.FileName);

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
            string _ime = "";//_kolona = 2
            string _prezime = "";//_kolona = 3

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
                        DB.InkrementalniKljuc.ResetujInkrementalniKljuc("Radnik_ID", "Radnik", _konekcijaSqlConnection);
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
                                string _c = _staraSifra + "\t" + _ime + "\t" + _prezime;

                                _ukupanBrojRedova++;

                                try
                                {
                                    DBRadnik.ImportRadnika(_staraSifra, _ime, _prezime);
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
                            _ime = "";//_kolona = 2
                            _prezime = "";//_kolona = 3


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
                                    _ime = _ime + _karakter.ToString();
                                    break;
                                case 3:
                                    _prezime = _prezime + _karakter.ToString();
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
                    DBRadnik.DajSveRadnike(radnikDataSet.vwRadnik);

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (!radnikDataGridView.Rows.Count.Equals(0))
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




    }
}