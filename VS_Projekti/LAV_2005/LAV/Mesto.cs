using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAV
{
    public partial class Mesto : Form
    {
        DB.Mesto DBMesto = new DB.Mesto();

        public Mesto()
        {
            InitializeComponent();

            Inicijalizuj();
        }

        #region Inicijalizuj()

        private void Inicijalizuj()
        {
            //Bindovanje DataGridView-a
            mestoBindingNavigator.BindingSource = mestoBindingSource;
            mestoBindingSource.DataMember = mestoDataSet.vwMesto.DefaultView.ToString();
            mestoBindingSource.DataSource = mestoDataSet.vwMesto.DefaultView;
            mestoDataGridView.DataSource = mestoBindingSource;

            //DataGridView podesavanja
            mestoDataGridView.MultiSelect = false;
            mestoDataGridView.AllowUserToAddRows = false;
            mestoDataGridView.AllowUserToResizeRows = false;
            mestoDataGridView.AllowUserToDeleteRows = false;
            mestoDataGridView.Enabled = false;
            mestoDataGridView.ReadOnly = true;
            mestoDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            mestoDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;


            //TabStop=false
            mestoDataGridView.TabStop = false;
            mestoBindingNavigator.TabStop = false;
            obrisiButton.TabStop = false;
            krajButton.TabStop = false;
            mestoDetaljnoTabControl.TabStop = false;

            //TabIndex
            nazivTextBox.TabIndex = 0;
            pozivniBrojTextBox.TabIndex = 1;
            postanskiBrojTextBox.TabIndex = 2;
            potvrdiButton.TabIndex = 3;
            odustaniButton.TabIndex = 4;

            //MaxLenght
            nazivTextBox.MaxLength = 30;
            pozivniBrojTextBox.MaxLength = 5;
            postanskiBrojTextBox.MaxLength = 5;

        }

        #endregion

        #region UStanje()

        private void UStanje(string stanje)
        {
            //mora ici pre dugmica
            nazivTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            pozivniBrojTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            postanskiBrojTextBox.Enabled = (stanje == "Unos") || (stanje == "Izmena");


            unesiButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");
            izmeniButton.Enabled = (stanje == "Detaljno");
            potvrdiButton.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            odustaniButton.Enabled = (stanje == "Unos") || (stanje == "Izmena");
            obrisiButton.Enabled = (stanje == "Detaljno");
            krajButton.Enabled = (stanje == "Osnovno") || (stanje == "Detaljno");

            mestoDataGridView.Enabled = (stanje == "Detaljno");

            mestoBindingNavigator.Enabled = (stanje == "Detaljno");

            if ((stanje == "Unos") || (stanje == "Izmena"))
            {
                nazivTextBox.Select();
            }
        }

        #endregion

        #region PrikaziMestoDetaljno (indexReda)

        private void PrikaziMestoDetaljno(int indexReda)
        {
            mesto_IDTextBox.Text = mestoDataGridView.Rows[indexReda].Cells["Mesto_ID"].Value.ToString();
            nazivTextBox.Text = mestoDataGridView.Rows[indexReda].Cells["Naziv"].Value.ToString();
            pozivniBrojTextBox.Text = mestoDataGridView.Rows[indexReda].Cells["Pozivni broj"].Value.ToString();
            postanskiBrojTextBox.Text = mestoDataGridView.Rows[indexReda].Cells["Poštanski broj"].Value.ToString();

        }

        #endregion

        #region PrikaziMestoDetaljno()

        private void PrikaziMestoDetaljno()
        {
            if (!mestoDataGridView.RowCount.Equals(0))
            {
                mesto_IDTextBox.Text = mestoDataGridView.CurrentRow.Cells["Mesto_ID"].Value.ToString();
                nazivTextBox.Text = mestoDataGridView.CurrentRow.Cells["Naziv"].Value.ToString();
                pozivniBrojTextBox.Text = mestoDataGridView.CurrentRow.Cells["Pozivni broj"].Value.ToString();
                postanskiBrojTextBox.Text = mestoDataGridView.CurrentRow.Cells["Poštanski broj"].Value.ToString();

            }
        }

        #endregion

        #region IsprazniMestoDetaljno()

        private void IsprazniMestoDetaljno()
        {
            mesto_IDTextBox.Text = "";
            nazivTextBox.Text = "";
            pozivniBrojTextBox.Text = "";
            postanskiBrojTextBox.Text = "";
        }

        #endregion

        #region Dogadjaji

        private void mestoDataGridView_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            PrikaziMestoDetaljno(e.RowIndex);
        }

        private void mestoDataGridView_Sorted(object sender, EventArgs e)
        {
            if (!mestoDataGridView.RowCount.Equals(0))
            {
                this.mestoDataGridView.FirstDisplayedCell = this.mestoDataGridView.CurrentCell;
                mestoDataGridView.CurrentCell = mestoDataGridView[0, 0];
            }
        }

        #endregion


        #region Load

        private void Mesto_Load(object sender, EventArgs e)
        {
            try
            {
                DBMesto.DajSvaMesta(mestoDataSet.vwMesto);

            }
            catch (Exception ex)
            {
                MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (!mestoDataGridView.Rows.Count.Equals(0))
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
            IsprazniMestoDetaljno();
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
            mestoErrorProvider.Clear();

            #region Provera obaveznih polja

            if (nazivTextBox.Text == "")
            {
                mestoErrorProvider.SetError(nazivTextBox, "Obavezan podatak.");
                nazivTextBox.Select();
            }

            #endregion

            //ako su podaci na formi ispravno uneti
            else
            {

                try
                {
                    //ako je polje Sifra prazno onda je Insert 
                    if (mesto_IDTextBox.Text == "")
                    {
                        #region Unos

                        //unesi red i uzmi ID unetog reda
                        Byte _mesto_ID = DBMesto.UnesiMesto(nazivTextBox.Text, pozivniBrojTextBox.Text, postanskiBrojTextBox.Text);

                        DBMesto.DajSvaMesta(mestoDataSet.vwMesto);

                        //poziv metode koja vraca ID unetog reda ili -1
                        int _indexUnetogReda = PomocneKlase.Index.DajIndexReda(mestoDataGridView, "Mesto_ID", _mesto_ID.ToString());

                        //ako je nasao index upravo unetog reda  i ako je taj index validan.
                        if ((_indexUnetogReda != -1) && (_indexUnetogReda < mestoDataGridView.RowCount))
                        {
                            //podesava da CurrentCell bude na redu koji je unet
                            this.mestoDataGridView.CurrentCell = this.mestoDataGridView[0, _indexUnetogReda];
                        }
                        #endregion

                    }
                    //ako polje Sifra dokumenta nije prazno onda Update
                    else
                    {
                        #region Izmena

                        DBMesto.IzmeniMesto(Byte.Parse(mesto_IDTextBox.Text), nazivTextBox.Text, pozivniBrojTextBox.Text, postanskiBrojTextBox.Text);

                        Byte _sifraIzmenjenogMesta = Byte.Parse(mesto_IDTextBox.Text);


                        DBMesto.DajSvaMesta(mestoDataSet.vwMesto);

                        //poziv metode koja vraca index promenjenog  reda ili -1
                        int _indexIzmenjenogReda = PomocneKlase.Index.DajIndexReda(mestoDataGridView, "Mesto_ID", _sifraIzmenjenogMesta.ToString());

                        //ako je nasao index upravo izmenjenog reda i ako je taj index validan. Ako prvi uslov nije tacan drugi se ne proverava
                        if ((_indexIzmenjenogReda != -1) && (_indexIzmenjenogReda < mestoDataGridView.RowCount))
                        {
                            //podesava da CurrentCell bude na redu koji je izmenjen
                            this.mestoDataGridView.CurrentCell = this.mestoDataGridView[0, _indexIzmenjenogReda];
                        }

                        #endregion

                    }

                    if (!mestoDataGridView.Rows.Count.Equals(0))
                    {
                        UStanje("Detaljno");
                    }
                    else
                    {
                        IsprazniMestoDetaljno();
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
            mestoErrorProvider.Clear();

            if (!mestoDataGridView.Rows.Count.Equals(0))
            {
                PrikaziMestoDetaljno();
                UStanje("Detaljno");
            }
            else
            {
                IsprazniMestoDetaljno();
                UStanje("Osnovno");
            }
        }

        #endregion

        #region Obrisi

        private void obrisiButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Da li ste sigurni da želite da obrišete mesto?",
                    "Potvrdi brisanje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    DBMesto.ObrisiMesto(Byte.Parse(mesto_IDTextBox.Text));

                    DBMesto.DajSvaMesta(mestoDataSet.vwMesto);

                    if (!mestoDataGridView.Rows.Count.Equals(0))
                    {
                        UStanje("Detaljno");
                    }
                    else
                    {
                        IsprazniMestoDetaljno();
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
            mestoDataSet.Clear();
            mestoDataSet.Dispose();
            this.Close();
        }

        #endregion

        #endregion


    }
}