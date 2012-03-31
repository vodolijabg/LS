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
    public partial class Roba : Form
    {
        DB.Roba DBRoba = new DB.Roba();

        TextBox RobaTextBox = null;
        TextBox CenaTextBox = null;
        TextBox CenaUgradnjeTextBox = null;
        TextBox NormaSatiTextBox = null;
        ErrorProvider PonudaErrorProvider = null;


        public Roba()
        {
            InitializeComponent();

            Inicijalizuj();
        }

        public Roba(ErrorProvider ponudaErrorProvider, TextBox robaTextBox, TextBox cena, TextBox cenaUgradnje, TextBox normaSati)
            : this()
        {
            this.ControlBox = true;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            RobaTextBox = robaTextBox;
            CenaTextBox = cena;
            CenaUgradnjeTextBox = cenaUgradnje;
            NormaSatiTextBox = normaSati;
            PonudaErrorProvider = ponudaErrorProvider;

            robaDataGridView.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(robaDataGridView_CellMouseDoubleClick);
        }

        public Roba(ErrorProvider ponudaErrorProvider, TextBox robaTextBox, TextBox cena, TextBox cenaUgradnje, TextBox normaSati, Int32 roba_ID)
            : this()
        {
            this.ControlBox = true;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;

            RobaTextBox = robaTextBox;
            CenaTextBox = cena;
            CenaUgradnjeTextBox = cenaUgradnje;
            NormaSatiTextBox = normaSati;
            PonudaErrorProvider = ponudaErrorProvider;


            robaDataGridView.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(robaDataGridView_CellMouseDoubleClick);

            try
            {
                DBRoba.NadjiRobu(robaDataSet.vwRoba, roba_ID);
            }
            catch (Exception)
            {
                //nista
            }
        }

        void robaDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if ((RobaTextBox != null) && (!robaDataGridView.Rows.Count.Equals(0)))
            {
                RobaTextBox.Text = robaDataGridView.Rows[robaDataGridView.CurrentCell.RowIndex].Cells["Naziv"].Value.ToString(); ;
                CenaTextBox.Text = robaDataGridView.Rows[robaDataGridView.CurrentCell.RowIndex].Cells["Cena"].Value.ToString(); ;
                CenaUgradnjeTextBox.Text = robaDataGridView.Rows[robaDataGridView.CurrentCell.RowIndex].Cells["Cena ugradnje"].Value.ToString(); ;
                NormaSatiTextBox.Text = robaDataGridView.Rows[robaDataGridView.CurrentCell.RowIndex].Cells["Norma sati"].Value.ToString(); ;

                RobaTextBox.Tag = robaDataGridView.Rows[robaDataGridView.CurrentCell.RowIndex].Cells["Roba_ID"].Value.ToString();
                PonudaErrorProvider.Clear();

            }

            robaDataSet.Clear();
            robaDataSet.Dispose();
            this.Close();
        }


        #region Inicijalizuj()

        private void Inicijalizuj()
        {
            //Bindovanje DataGridView-a
            robaBindingNavigator.BindingSource = robaBindingSource;
            robaBindingSource.DataMember = robaDataSet.vwRoba.DefaultView.ToString();
            robaBindingSource.DataSource = robaDataSet.vwRoba.DefaultView;
            robaDataGridView.DataSource = robaBindingSource;

            //DataGridView podesavanja
            robaDataGridView.MultiSelect = false;
            robaDataGridView.AllowUserToAddRows = false;
            robaDataGridView.AllowUserToResizeRows = false;
            robaDataGridView.AllowUserToDeleteRows = false;
            robaDataGridView.Enabled = false;
            robaDataGridView.ReadOnly = true;
            robaDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            robaDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


            //TabStop=false
            robaDataGridView.TabStop = false;
            robaBindingNavigator.TabStop = false;
            obrisiButton.TabStop = false;
            krajButton.TabStop = false;
            robaDetaljnoTabControl.TabStop = false;

            uslovNazivTextBox.TabStop = false;
            uslovInternaSifraTextBox.TabStop = false;
            uslovStaraSifraTextBox.TabStop = false;
            uslovSlicnoTrazenjeSadrziCheckBox.TabStop = false;
            nadjiButton.TabStop = false;


            //TabIndex
            staraSifraTextBox.TabIndex = 0;
            internaSifraTextBox.TabIndex = 1;
            nazivTextBox.TabIndex = 2;
            cenaDelaTextBox.TabIndex = 3;
            cenaUgradnjeTextBox.TabIndex = 4;
            normaSatiTextBox.TabIndex = 5;
            napomenaRichTextBox.TabIndex = 6;

            potvrdiButton.TabIndex = 7;
            odustaniButton.TabIndex = 8;

            //MaxLenght
            staraSifraTextBox.MaxLength = 15;
            nazivTextBox.MaxLength = 60;
            internaSifraTextBox.MaxLength = 50;
            cenaDelaTextBox.MaxLength = 19;
            cenaUgradnjeTextBox.MaxLength = 19;
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
            internaSifraTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            nazivTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            cenaDelaTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            cenaUgradnjeTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            normaSatiTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            napomenaRichTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");

            importToolStripButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");
            exportToolStripButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");

            uslovNazivTextBox.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");
            uslovInternaSifraTextBox.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");
            uslovStaraSifraTextBox.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");
            uslovSlicnoTrazenjeSadrziCheckBox.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");
            nadjiButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");


            unesiButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");
            izmeniButton.Enabled = (stanje == "Detaljno");
            potvrdiButton.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            odustaniButton.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            obrisiButton.Enabled = (stanje == "Detaljno");
            krajButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");

            robaDataGridView.Enabled = (stanje == "Detaljno");

            robaBindingNavigator.Enabled = (stanje == "Detaljno");

            if ((stanje == "Unos") || (stanje == "Izmena"))
            {
                staraSifraTextBox.Select();
            }
        }

        #endregion

        #region PrikaziRobuDetaljno (indexReda)

        private void PrikaziRobuDetaljno(int indexReda)
        {
            roba_IDTextBox.Text = robaDataGridView.Rows[indexReda].Cells["Roba_ID"].Value.ToString();
            staraSifraTextBox.Text = robaDataGridView.Rows[indexReda].Cells["Stara šifra"].Value.ToString();
            internaSifraTextBox.Text = robaDataGridView.Rows[indexReda].Cells["Interna šifra"].Value.ToString();
            nazivTextBox.Text = robaDataGridView.Rows[indexReda].Cells["Naziv"].Value.ToString();
            cenaDelaTextBox.Text = robaDataGridView.Rows[indexReda].Cells["Cena"].Value.ToString();
            cenaUgradnjeTextBox.Text = robaDataGridView.Rows[indexReda].Cells["Cena ugradnje"].Value.ToString();
            normaSatiTextBox.Text = robaDataGridView.Rows[indexReda].Cells["Norma sati"].Value.ToString();
            napomenaRichTextBox.Text = robaDataGridView.Rows[indexReda].Cells["Napomena"].Value.ToString();

        }

        #endregion

        #region PrikaziRobuDetaljno()

        private void PrikaziRobuDetaljno()
        {
            if (!robaDataGridView.RowCount.Equals(0))
            {
                roba_IDTextBox.Text = robaDataGridView.CurrentRow.Cells["Roba_ID"].Value.ToString();
                staraSifraTextBox.Text = robaDataGridView.CurrentRow.Cells["Stara šifra"].Value.ToString();
                internaSifraTextBox.Text = robaDataGridView.CurrentRow.Cells["Interna šifra"].Value.ToString();
                nazivTextBox.Text = robaDataGridView.CurrentRow.Cells["Naziv"].Value.ToString();
                cenaDelaTextBox.Text = robaDataGridView.CurrentRow.Cells["Cena"].Value.ToString();
                cenaUgradnjeTextBox.Text = robaDataGridView.CurrentRow.Cells["Cena ugradnje"].Value.ToString();
                normaSatiTextBox.Text = robaDataGridView.CurrentRow.Cells["Norma sati"].Value.ToString();
                napomenaRichTextBox.Text = robaDataGridView.CurrentRow.Cells["Napomena"].Value.ToString();
            }
        }

        #endregion

        #region IsprazniRobuDetaljno()

        private void IsprazniRobuDetaljno()
        {
            roba_IDTextBox.Text = "";
            staraSifraTextBox.Text = "";
            internaSifraTextBox.Text = "";
            nazivTextBox.Text = "";
            cenaDelaTextBox.Text = "";
            cenaUgradnjeTextBox.Text = "";
            normaSatiTextBox.Text = "";
            napomenaRichTextBox.Text = "";
        }

        #endregion

        #region Dogadjaji

        private void robaDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            PrikaziRobuDetaljno(e.RowIndex);
        }

        private void robaDataGridView_Sorted(object sender, EventArgs e)
        {
            if (!robaDataGridView.RowCount.Equals(0))
            {
                this.robaDataGridView.FirstDisplayedCell = this.robaDataGridView.CurrentCell;
                robaDataGridView.CurrentCell = robaDataGridView[0, 0];
            }
        }

        private void napomenaRichTextBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Napomena napomena = new Napomena(napomenaRichTextBox);
            napomena.ShowDialog();
        }

        #endregion

        #region Load

        private void Roba_Load(object sender, EventArgs e)
        {
            try
            {
                //DBRoba.DajSvuRobu(robaDataSet.vwRoba);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!robaDataGridView.Rows.Count.Equals(0))
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
            IsprazniRobuDetaljno();
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

            robaErrorProvider.Clear();

            #region Provera obaveznih polja

            if (staraSifraTextBox.Text == "")
            {
                robaErrorProvider.SetError(staraSifraTextBox, "Obavezan podatak.");
                staraSifraTextBox.Select();
            }
            else if (internaSifraTextBox.Text == "")
            {
                robaErrorProvider.SetError(internaSifraTextBox, "Obavezan podatak.");
                internaSifraTextBox.Select();
            }
            else if (nazivTextBox.Text == "")
            {
                robaErrorProvider.SetError(nazivTextBox, "Obavezan podatak.");
                nazivTextBox.Select();
            }
            else if (cenaDelaTextBox.Text == "")
            {
                robaErrorProvider.SetError(cenaDelaTextBox, "Obavezan podatak.");
                cenaDelaTextBox.Select();
            }
            else if (cenaUgradnjeTextBox.Text == "")
            {
                robaErrorProvider.SetError(cenaUgradnjeTextBox, "Obavezan podatak.");
                cenaUgradnjeTextBox.Select();
            }
            else if (normaSatiTextBox.Text == "")
            {
                robaErrorProvider.SetError(normaSatiTextBox, "Obavezan podatak.");
                normaSatiTextBox.Select();
            }

            #endregion

            #region ProveraTipaPodataka

            //provera da li poslata vrednost decimal
            else if (!Decimal.TryParse(cenaDelaTextBox.Text, out _d))
            {
                robaErrorProvider.SetError(cenaDelaTextBox, "Vrednost uneta u polje Cena dela mora biti broj.");
                cenaDelaTextBox.Select();
            }
            //provera da li poslata vrednost ima vise od 16 cifara i dve decimale 
            else if (decimal.Parse(cenaDelaTextBox.Text) > decimal.Parse("9999999999999999,99"))
            {
                robaErrorProvider.SetError(cenaDelaTextBox, "Vrednost uneta u polje Cena dela može imati najviše 16 cifara i dve decimale.");
                cenaDelaTextBox.Select();
            }

            //provera da li poslata vrednost decimal
            else if (!Decimal.TryParse(cenaUgradnjeTextBox.Text, out _d))
            {
                robaErrorProvider.SetError(cenaUgradnjeTextBox, "Vrednost uneta u polje Cena ugradnje mora biti broj.");
                cenaUgradnjeTextBox.Select();
            }
            //provera da li poslata vrednost ima vise od 16 cifara i dve decimale 
            else if (decimal.Parse(cenaUgradnjeTextBox.Text) > decimal.Parse("9999999999999999,99"))
            {
                robaErrorProvider.SetError(cenaUgradnjeTextBox, "Vrednost uneta u polje Cena ugradnje može imati najviše 16 cifara i dve decimale.");
                cenaUgradnjeTextBox.Select();
            }

                //provera da li poslata vrednost decimal
            else if (!Decimal.TryParse(normaSatiTextBox.Text, out _d))
            {
                robaErrorProvider.SetError(normaSatiTextBox, "Vrednost uneta u polje Norma sati mora biti broj.");
                normaSatiTextBox.Select();
            }
            //provera da li poslata vrednost ima vise od 16 cifara i dve decimale 
            else if (decimal.Parse(normaSatiTextBox.Text) > decimal.Parse("999,99"))
            {
                robaErrorProvider.SetError(normaSatiTextBox, "Vrednost uneta u polje Norma sati može imati najviše 3 cifre i dve decimale.");
                normaSatiTextBox.Select();
            }

            #endregion

            #region Provera ChkConnstraintia

            //provera da li je poslata vrednost pozitivan broj 
            else if (decimal.Parse(cenaDelaTextBox.Text) < 0)
            {
                robaErrorProvider.SetError(cenaDelaTextBox, "Vrednost uneta u polje Cena dela ne može biti negativan broj.");
                cenaDelaTextBox.Select();
            }
            //provera da li je poslata vrednost pozitivan broj 
            else if (decimal.Parse(cenaUgradnjeTextBox.Text) < 0)
            {
                robaErrorProvider.SetError(cenaUgradnjeTextBox, "Vrednost uneta u polje Cena ugradnje ne može biti negativan broj.");
                cenaUgradnjeTextBox.Select();
            }
            //provera da li je poslata vrednost pozitivan broj 
            else if (decimal.Parse(normaSatiTextBox.Text) < 0)
            {
                robaErrorProvider.SetError(normaSatiTextBox, "Vrednost uneta u polje Norma sati ne može biti negativan broj.");
                normaSatiTextBox.Select();
            }

            #endregion

            //ako su podaci na formi ispravno uneti
            else
            {
                try
                {
                    //ako je polje Sifra prazno onda je Insert 
                    if (roba_IDTextBox.Text == "")
                    {
                        #region Unos

                        //unesi red i uzmi ID unetog reda
                        Int32 _roba_ID = DBRoba.UnesiRobu(staraSifraTextBox.Text, internaSifraTextBox.Text, nazivTextBox.Text, decimal.Parse(cenaDelaTextBox.Text), decimal.Parse(cenaUgradnjeTextBox.Text), decimal.Parse(normaSatiTextBox.Text), napomenaRichTextBox.Text);

                        //DBRoba.DajSvuRobu(robaDataSet.vwRoba);
                        string _SQLUpit = DajSQLUpit() + " union  SELECT  Roba_ID, [Stara šifra], [Interna šifra], Naziv, Cena, [Cena ugradnje], [Norma sati], Napomena FROM vwRoba  where Roba_ID = " + _roba_ID;
                        DBRoba.NadjiRobu(robaDataSet.vwRoba, _SQLUpit);

                        //poziv metode koja vraca ID unetog reda ili -1
                        int _indexUnetogReda = PomocneKlase.Index.DajIndexReda(robaDataGridView, "Roba_ID", _roba_ID.ToString());

                        //ako je nasao index upravo unetog reda  i ako je taj index validan.
                        if ((_indexUnetogReda != -1) && (_indexUnetogReda < robaDataGridView.RowCount))
                        {
                            //podesava da CurrentCell bude na redu koji je unet
                            this.robaDataGridView.CurrentCell = this.robaDataGridView[0, _indexUnetogReda];
                        }
                        #endregion

                    }
                    //ako polje Sifra dokumenta nije prazno onda Update
                    else
                    {
                        #region Izmena

                        DBRoba.IzmeniRobu(Int32.Parse(roba_IDTextBox.Text),staraSifraTextBox.Text, internaSifraTextBox.Text, nazivTextBox.Text, decimal.Parse(cenaDelaTextBox.Text), decimal.Parse(cenaUgradnjeTextBox.Text), decimal.Parse(normaSatiTextBox.Text), napomenaRichTextBox.Text);

                        Int32 _sifraIzmenjeneRobe = Int32.Parse(roba_IDTextBox.Text);


                        //DBRoba.DajSvuRobu(robaDataSet.vwRoba);
                        string _SQLUpit = DajSQLUpit() + " union  SELECT  Roba_ID, [Stara šifra], [Interna šifra], Naziv, Cena, [Cena ugradnje], [Norma sati], Napomena FROM vwRoba  where Roba_ID = " + _sifraIzmenjeneRobe;
                        DBRoba.NadjiRobu(robaDataSet.vwRoba, _SQLUpit);

                        //poziv metode koja vraca index promenjenog  reda ili -1
                        int _indexIzmenjenogReda = PomocneKlase.Index.DajIndexReda(robaDataGridView, "Roba_ID", _sifraIzmenjeneRobe.ToString());

                        //ako je nasao index upravo izmenjenog reda i ako je taj index validan. Ako prvi uslov nije tacan drugi se ne proverava
                        if ((_indexIzmenjenogReda != -1) && (_indexIzmenjenogReda < robaDataGridView.RowCount))
                        {
                            //podesava da CurrentCell bude na redu koji je izmenjen
                            this.robaDataGridView.CurrentCell = this.robaDataGridView[0, _indexIzmenjenogReda];
                        }

                        #endregion

                    }

                    if (!robaDataGridView.Rows.Count.Equals(0))
                    {
                        UStanje("Detaljno");
                    }
                    else
                    {
                        IsprazniRobuDetaljno();
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
            robaErrorProvider.Clear();

            if (!robaDataGridView.Rows.Count.Equals(0))
            {
                PrikaziRobuDetaljno();
                UStanje("Detaljno");
            }
            else
            {
                IsprazniRobuDetaljno();
                UStanje("Osnovno");
            }
        }

        #endregion

        #region Obrisi

        private void obrisiButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da obrišete robu?",
                    "Potvrdi brisanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    DBRoba.ObrisiRobu(Int32.Parse(roba_IDTextBox.Text));

                    //DBRoba.DajSvuRobu(robaDataSet.vwRoba);
                    DBRoba.NadjiRobu(robaDataSet.vwRoba, DajSQLUpit());

                    if (!robaDataGridView.Rows.Count.Equals(0))
                    {
                        UStanje("Detaljno");
                    }
                    else
                    {
                        IsprazniRobuDetaljno();
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
                    int _brojRedova = DBRoba.IzveziRobu(exportSaveFileDialog.FileName);

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
            string _internaSifra = "";//_kolona = 2
            string _naziv = "";//_kolona = 3
            string _cenaDela = "";//_kolona = 4
            string _cenaUgradnje = "";//_kolona = 5
            string _normaSati = "";//_kolona = 6
            string _napomena = "";//_kolona = 7


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
                        DB.InkrementalniKljuc.ResetujInkrementalniKljuc("Roba_ID", "Roba", _konekcijaSqlConnection);
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
                                string _c = _staraSifra + "\t" + _internaSifra + "\t" + _naziv + "\t" + _cenaDela + "\t" + _cenaUgradnje + "\t" + _normaSati + "\t" + _napomena;

                                _ukupanBrojRedova++;

                                try
                                {
                                    DBRoba.UveziRobu(_staraSifra, _internaSifra, _naziv, Decimal.Parse(_cenaDela), Decimal.Parse(_cenaUgradnje), Decimal.Parse(_normaSati), _napomena);
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
                            _internaSifra = "";//_kolona = 2
                            _naziv = "";//_kolona = 3
                            _cenaDela = "";//_kolona = 4
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
                                    _staraSifra = _staraSifra + _karakter.ToString();
                                    break;
                                case 2:
                                    _internaSifra = _internaSifra + _karakter.ToString();
                                    break;
                                case 3:
                                    _naziv = _naziv + _karakter.ToString();
                                    break;
                                case 4:
                                    _cenaDela = _cenaDela + _karakter.ToString();
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

                _vremeTrajanja = DateTime.Now - _pocetakUpita;
                this.Cursor = System.Windows.Forms.Cursors.Default;

                MessageBox.Show("Ukupan broj redova = " + _ukupanBrojRedova + "\nBroj unetih = " + _brojUnetih + "\nBroj grešaka = " + _brojGresaka + "\n\nVreme importa = " + _vremeTrajanja + "                                ", "LAV", MessageBoxButtons.OK, MessageBoxIcon.Information);

                try
                {
                    DBRoba.NadjiRobu(robaDataSet.vwRoba, DajSQLUpit());

                }
                catch (Exception ex)
                {
                    MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    if (!robaDataGridView.Rows.Count.Equals(0))
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
            string _SQLUpit = " SELECT  Roba_ID, [Stara šifra], [Interna šifra], Naziv, Cena, [Cena ugradnje], [Norma sati], Napomena FROM vwRoba ";

            string _uslovStaraSifra = "";
            string _uslovInternaSifra = "";
            string _uslovNaziv = "";

            bool _prviUslov = true;

            if ((uslovSlicnoTrazenjeSadrziCheckBox.Checked) && (uslovStaraSifraTextBox.Text != ""))
            {
                _uslovStaraSifra = " [Stara šifra] like '%" + uslovStaraSifraTextBox.Text + "%'";
            }
            else if ((!uslovSlicnoTrazenjeSadrziCheckBox.Checked) && (uslovStaraSifraTextBox.Text != ""))
            {
                _uslovStaraSifra = " [Stara šifra] = '" + uslovStaraSifraTextBox.Text + "'";
            }

            if ((uslovSlicnoTrazenjeSadrziCheckBox.Checked) && (uslovInternaSifraTextBox.Text != ""))
            {
                _uslovInternaSifra = " [Interna šifra] like '%" + uslovInternaSifraTextBox.Text + "%'";
            }
            else if ((!uslovSlicnoTrazenjeSadrziCheckBox.Checked) && (uslovInternaSifraTextBox.Text != ""))
            {
                _uslovInternaSifra = " [Interna šifra] = '" + uslovInternaSifraTextBox.Text + "'";
            }

            if ((uslovSlicnoTrazenjeSadrziCheckBox.Checked) && (uslovNazivTextBox.Text != ""))
            {
                _uslovNaziv = " Naziv like '%" + uslovNazivTextBox.Text + "%'";
            }
            else if ((!uslovSlicnoTrazenjeSadrziCheckBox.Checked) && (uslovNazivTextBox.Text != ""))
            {
                _uslovNaziv = " Naziv = '" + uslovNazivTextBox.Text + "'";
            }


            if (_uslovStaraSifra != "")
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

                _SQLUpit = _SQLUpit + _uslovStaraSifra;
            }

            if (_uslovInternaSifra != "")
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

                _SQLUpit = _SQLUpit + _uslovInternaSifra;
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
                //DBRoba.DajSvuRobu(robaDataSet.vwRoba);
                DBRoba.NadjiRobu(robaDataSet.vwRoba, DajSQLUpit());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!robaDataGridView.Rows.Count.Equals(0))
                {
                    UStanje("Detaljno");
                }
                else
                {
                    IsprazniRobuDetaljno();
                    UStanje("Osnovno");
                }
            }
        } 
        #endregion

        #region ResetujUslove
        private void resetujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uslovStaraSifraTextBox.Text = "";
            uslovInternaSifraTextBox.Text = "";
            uslovNazivTextBox.Text = "";
            uslovSlicnoTrazenjeSadrziCheckBox.Checked = false;
        } 
        #endregion

 
        
    }
}