using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAV
{
    public partial class Automobil : Form
    {
        DB.ProizvodjacAutomobila DBProizvodjacAutomobila = new DB.ProizvodjacAutomobila();
        DB.ModelAutomobila DBModelAutomobila = new DB.ModelAutomobila();
        DB.TipAutomobila DBTipAutomobila = new DB.TipAutomobila();

        TextBox TipAutomobilaTextBox = null;
        bool NapunjenePadajuceListe = false;

        public Automobil()
        {
            InitializeComponent();

            Inicijalizuj();
        }

        public Automobil(TextBox tipAutomobilaTextBox):this()
        {
            this.ControlBox = true;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            TipAutomobilaTextBox = tipAutomobilaTextBox;

            this.tipAutomobilaDataGridView.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(tipAutomobilaDataGridView_CellMouseDoubleClick);
        }

        public Automobil(TextBox tipAutomobilaTextBox, Int32 tipAutomobila_ID): this()
        {
            this.ControlBox = true;
            this.MinimizeBox = false;
            this.StartPosition = FormStartPosition.CenterParent;
            TipAutomobilaTextBox = tipAutomobilaTextBox;

            this.tipAutomobilaDataGridView.CellMouseDoubleClick += new DataGridViewCellMouseEventHandler(tipAutomobilaDataGridView_CellMouseDoubleClick);

            #region NapuniPadajucuListuProizvodjacAutomobila
            try
            {
                DBProizvodjacAutomobila.DajPadajucuListuProizvodjacAutomobila(automobilDataSet.proizvodjacAutomobila);

                automobilDataSet.proizvodjacAutomobila.Rows[0].Delete();
                proizvodjacAutomobilaComboBox.SelectedIndex = 0;


            }
            catch (Exception)
            {
                automobilDataSet.proizvodjacAutomobila.Clear();

                //dodaj prazan red na indexu 0
                DataRow _dr;
                _dr = automobilDataSet.proizvodjacAutomobila.NewRow();

                _dr[0] = "-1";
                _dr[1] = "";
                automobilDataSet.proizvodjacAutomobila.Rows.InsertAt(_dr, 0);

                MessageBox.Show("Greška pri čitanju liste proizvođača automobila.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

            #region NapuniPadajucuListuModelAutomobila
            if ((!proizvodjacAutomobilaComboBox.Items.Count.Equals(0)) && (!proizvodjacAutomobilaComboBox.Items.Count.Equals(proizvodjacAutomobilaComboBox.SelectedIndex)) && (proizvodjacAutomobilaComboBox.SelectedIndex >= 0))
            {
                try
                {
                    DBModelAutomobila.DajPadajucuListuModelAutomobila(automobilDataSet.modelAutomobila, Int16.Parse(proizvodjacAutomobilaComboBox.SelectedValue.ToString()));

                    automobilDataSet.modelAutomobila.Rows[0].Delete();
                    modelAutomobilaComboBox.SelectedIndex = 0;
                }
                catch (Exception)
                {
                    automobilDataSet.modelAutomobila.Clear();

                    //dodaj prazan red na indexu 0
                    DataRow _dr;
                    _dr = automobilDataSet.modelAutomobila.NewRow();

                    _dr[0] = "-1";
                    _dr[1] = "";
                    automobilDataSet.modelAutomobila.Rows.InsertAt(_dr, 0);


                    MessageBox.Show("Greška pri čitanju modela automobila.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion

            NapunjenePadajuceListe = true;

            try
            {
                DBTipAutomobila.DajTipAutomobilaOsnovniPodaci(automobilDataSet.vwTipAutomobilaOsnovniPodaci, tipAutomobila_ID);
                
                if (!automobilDataSet.vwTipAutomobilaOsnovniPodaci.Rows.Count.Equals(0))
                { 
                    proizvodjacAutomobilaComboBox.SelectedIndex = (int)proizvodjacAutomobilaComboBox.FindString(automobilDataSet.vwTipAutomobilaOsnovniPodaci[0]["Proizvodjac"].ToString());
                    modelAutomobilaComboBox.SelectedIndex = (int)modelAutomobilaComboBox.FindString(automobilDataSet.vwTipAutomobilaOsnovniPodaci[0]["Model"].ToString());
                }
            }
            catch (Exception)
            {

               // throw;
            }

            //poziv metode koja vraca index promenjenog  reda ili -1
            int _indexReda = PomocneKlase.Index.DajIndexReda(tipAutomobilaDataGridView, "TipAutomobila_ID", tipAutomobila_ID.ToString());

            //ako je nasao index upravo izmenjenog reda i ako je taj index validan. Ako prvi uslov nije tacan drugi se ne proverava
            if ((_indexReda != -1) && (_indexReda < tipAutomobilaDataGridView.RowCount))
            {
                this.tipAutomobilaDataGridView.CurrentCell = this.tipAutomobilaDataGridView["Tip", _indexReda];
            }


        }


        void tipAutomobilaDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            TipAutomobilaTextBox.Text = automobilDataSet.proizvodjacAutomobila.Rows[proizvodjacAutomobilaComboBox.SelectedIndex]["Naziv"].ToString() + " - " + automobilDataSet.modelAutomobila.Rows[modelAutomobilaComboBox.SelectedIndex]["Naziv"].ToString() + " - " + tipAutomobilaDataGridView.Rows[e.RowIndex].Cells["Tip"].Value.ToString();
            
            TipAutomobilaTextBox.Tag = tipAutomobilaDataGridView.Rows[e.RowIndex].Cells["TipAutomobila_ID"].Value.ToString();

            this.Close();

            automobilDataSet.Clear();
            automobilDataSet.Dispose();

        }

        #region Inicijalizuj

        private void Inicijalizuj()
        {
            proizvodjacAutomobilaComboBox.DataSource = automobilDataSet.proizvodjacAutomobila;
            proizvodjacAutomobilaComboBox.DisplayMember = "Naziv";
            proizvodjacAutomobilaComboBox.ValueMember = "ProizvodjacAutomobila_ID";
            //
            proizvodjacAutomobilaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            proizvodjacAutomobilaComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            proizvodjacAutomobilaComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;


            modelAutomobilaComboBox.DataSource = automobilDataSet.modelAutomobila;
            modelAutomobilaComboBox.DisplayMember = "Naziv";
            modelAutomobilaComboBox.ValueMember = "ModelAutomobila_ID";
            //
            modelAutomobilaComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            modelAutomobilaComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            modelAutomobilaComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            tipAutomobilaDataGridView.DataSource = automobilDataSet.tipAutomobila.DefaultView;

            //DataGridView podesavanja
            tipAutomobilaDataGridView.MultiSelect = false;
            tipAutomobilaDataGridView.AllowUserToAddRows = false;
            tipAutomobilaDataGridView.AllowUserToResizeRows = false;
            tipAutomobilaDataGridView.AllowUserToDeleteRows = false;
            tipAutomobilaDataGridView.ReadOnly = true;
            //tipAutomobilaDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            tipAutomobilaDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //tipAutomobilaDataGridView.Columns["TipAutomobila_ID"].Visible = false;
            tipAutomobilaDataGridView.TabStop = false;
        }

        #endregion

        #region StringUDatum()
        /// <summary>
        /// Vrednosti kolona  [Proizvodnja od] i [Proizvodnja do] koje sadrze string u formatu YYYYMM zamenjuje sa formatom MM.YYYY
        /// </summary>
        /// <param name="tipAutomobila"></param>
        void StringUDatum(DataTable tipAutomobila)
        {
            foreach (DataRow _red in tipAutomobila.Rows)
            {
                _red["Proizvodnja od"] = PomocneKlase.Stringovi.StringUDatum(_red["Proizvodnja od"].ToString());
                _red["Proizvodnja do"] = PomocneKlase.Stringovi.StringUDatum(_red["Proizvodnja do"].ToString());
            }
        }

        #endregion

        #region Load
        private void Automobil_Load(object sender, EventArgs e)
        {
            if (!NapunjenePadajuceListe)
            {
                #region NapuniPadajucuListuProizvodjacAutomobila
                try
                {
                    DBProizvodjacAutomobila.DajPadajucuListuProizvodjacAutomobila(automobilDataSet.proizvodjacAutomobila);

                    automobilDataSet.proizvodjacAutomobila.Rows[0].Delete();
                    proizvodjacAutomobilaComboBox.SelectedIndex = 0;


                }
                catch (Exception)
                {
                    automobilDataSet.proizvodjacAutomobila.Clear();

                    //dodaj prazan red na indexu 0
                    DataRow _dr;
                    _dr = automobilDataSet.proizvodjacAutomobila.NewRow();

                    _dr[0] = "-1";
                    _dr[1] = "";
                    automobilDataSet.proizvodjacAutomobila.Rows.InsertAt(_dr, 0);

                    MessageBox.Show("Greška pri čitanju liste proizvođača automobila.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                #endregion

                #region NapuniPadajucuListuModelAutomobila
                if ((!proizvodjacAutomobilaComboBox.Items.Count.Equals(0)) && (!proizvodjacAutomobilaComboBox.Items.Count.Equals(proizvodjacAutomobilaComboBox.SelectedIndex)) && (proizvodjacAutomobilaComboBox.SelectedIndex >= 0))
                {
                    try
                    {
                        DBModelAutomobila.DajPadajucuListuModelAutomobila(automobilDataSet.modelAutomobila, Int16.Parse(proizvodjacAutomobilaComboBox.SelectedValue.ToString()));

                        automobilDataSet.modelAutomobila.Rows[0].Delete();
                        modelAutomobilaComboBox.SelectedIndex = 0;
                    }
                    catch (Exception)
                    {
                        automobilDataSet.modelAutomobila.Clear();

                        //dodaj prazan red na indexu 0
                        DataRow _dr;
                        _dr = automobilDataSet.modelAutomobila.NewRow();

                        _dr[0] = "-1";
                        _dr[1] = "";
                        automobilDataSet.modelAutomobila.Rows.InsertAt(_dr, 0);


                        MessageBox.Show("Greška pri čitanju modela automobila.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                #endregion

            }
        }
        #endregion


        private void proizvodjacAutomobilaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region NapuniPadajucuListuModelAutomobila
            if ((!proizvodjacAutomobilaComboBox.Items.Count.Equals(0)) && (!proizvodjacAutomobilaComboBox.Items.Count.Equals(proizvodjacAutomobilaComboBox.SelectedIndex)) && (proizvodjacAutomobilaComboBox.SelectedIndex >= 0))
            {
                try
                {
                    DBModelAutomobila.DajPadajucuListuModelAutomobila(automobilDataSet.modelAutomobila, Int16.Parse(proizvodjacAutomobilaComboBox.SelectedValue.ToString()));

                    automobilDataSet.modelAutomobila.Rows[0].Delete();
                    modelAutomobilaComboBox.SelectedIndex = 0;
                }
                catch (Exception)
                {
                    automobilDataSet.modelAutomobila.Clear();

                    //dodaj prazan red na indexu 0
                    DataRow _dr;
                    _dr = automobilDataSet.modelAutomobila.NewRow();

                    _dr[0] = "-1";
                    _dr[1] = "";
                    automobilDataSet.modelAutomobila.Rows.InsertAt(_dr, 0);


                    MessageBox.Show("Greška pri čitanju modela automobila.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            #endregion
        }

        private void modelAutomobilaComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((!modelAutomobilaComboBox.Items.Count.Equals(0)) && (!modelAutomobilaComboBox.Items.Count.Equals(modelAutomobilaComboBox.SelectedIndex)) && (modelAutomobilaComboBox.SelectedIndex >= 0))
            {
                try
                {
                    DBTipAutomobila.DajTipoveAutomobilaZaModel(automobilDataSet.tipAutomobila, Int32.Parse(modelAutomobilaComboBox.SelectedValue.ToString()));

                    StringUDatum(automobilDataSet.tipAutomobila);
                }
                catch (Exception)
                {
                    MessageBox.Show("Greška pri čitanju tipova automobila.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                automobilDataSet.tipAutomobila.Clear();
            }
        }

        private void tipAutomobilaDataGridView_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex.Equals(0))
            {
                AutomobilDetaljno automobilaDetaljno = new AutomobilDetaljno(Int32.Parse(tipAutomobilaDataGridView.Rows[e.RowIndex].Cells["TipAutomobila_ID"].Value.ToString()), proizvodjacAutomobilaComboBox.Text + " " + modelAutomobilaComboBox.Text + " " + tipAutomobilaDataGridView.Rows[e.RowIndex].Cells["Tip"].Value.ToString());
                automobilaDetaljno.ShowDialog();
            }
        }
     
    }
}