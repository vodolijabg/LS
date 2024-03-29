using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAV
{
    public partial class NadjiPonudu : Form
    {
        DB.PoslovniPartner DBPoslovniPartner = new DB.PoslovniPartner();
        DB.Ponuda DBPonuda = new DB.Ponuda();

        Ponuda PonudaKlasa = null;


        public NadjiPonudu()
        {
            InitializeComponent();
            Inicijalizuj();
        }

        public NadjiPonudu(Ponuda ponudaKlasa):this()
        {
            PonudaKlasa = ponudaKlasa;
        }

        #region Inicijalizuj

        void Inicijalizuj()
        {
            //DataGridView
            nadjiPonuduDataGridView.MultiSelect = false;
            nadjiPonuduDataGridView.AllowUserToAddRows = false;
            nadjiPonuduDataGridView.AllowUserToResizeRows = false;
            nadjiPonuduDataGridView.AllowUserToDeleteRows = false;
            nadjiPonuduDataGridView.ReadOnly = true;
            nadjiPonuduDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            nadjiPonuduDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            nadjiPonuduDataGridView.DataSource = nadjiPonuduDataSet.vwPonudaZaglavlje;

            nadjiPonuduDataGridView.TabStop = false;

            poslovniPartnerComboBox.DataSource = nadjiPonuduDataSet.poslovniPartnerDataTable;
            poslovniPartnerComboBox.DisplayMember = "Naziv";
            poslovniPartnerComboBox.ValueMember = "PoslovniPartner_ID";
            //
            poslovniPartnerComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            poslovniPartnerComboBox.AutoCompleteSource = AutoCompleteSource.ListItems;
            poslovniPartnerComboBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            brojServisneKnjiziceTextBox.TabIndex = 1;
            poslovniPartnerComboBox.TabIndex = 2;
            zakljucenaCheckBox.TabIndex = 3;
            nadjiButton.TabIndex = 4;

        }
        #endregion

        #region Load

        private void NadjiPonudu_Load(object sender, EventArgs e)
        {
            try
            {
                DBPoslovniPartner.DajPadajucuListuPoslovniPartner(nadjiPonuduDataSet.poslovniPartnerDataTable);

                DataRow _dr = nadjiPonuduDataSet.poslovniPartnerDataTable.NewRow();
                _dr["PoslovniPartner_ID"] = "-1";
                _dr["Naziv"] = "";

                nadjiPonuduDataSet.poslovniPartnerDataTable.Rows.InsertAt(_dr, 0);

                poslovniPartnerComboBox.SelectedIndex = 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #endregion

        #region DajSQLUpit

        private string DajSQLUpit()
        {
            string _SQLUpit = " SELECT Ponuda_ID, Datum, [Poslovni partner], [Broj servisne knjižice], Zaključana FROM vwPonudaZaglavlje ";

            string _uslovBrojServisneKnjizice = "";
            string _uslovPoslovniPartner = "";
            string _uslovZakljucena = "";

            bool _prviUslov = true;

            if (brojServisneKnjiziceTextBox.Text != "")
            {
                _uslovBrojServisneKnjizice = " [Broj servisne knjižice] = '" + brojServisneKnjiziceTextBox.Text + "'";
            }
            if ((!poslovniPartnerComboBox.Items.Count.Equals(0)) && (poslovniPartnerComboBox.SelectedIndex>0))
            {
                _uslovPoslovniPartner = " PoslovniPartner_ID = '" + poslovniPartnerComboBox.SelectedValue.ToString() + "'";
            }
            if (zakljucenaCheckBox.Checked)
            {
                _uslovZakljucena = " Zaključana = 1";
            }
            else 
            {
                _uslovZakljucena = " Zaključana = 0";
            }
            
            if (_uslovBrojServisneKnjizice != "")
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

                _SQLUpit = _SQLUpit + _uslovBrojServisneKnjizice;
            }

            if (_uslovPoslovniPartner != "")
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

                _SQLUpit = _SQLUpit + _uslovPoslovniPartner;
            }

            if (_uslovZakljucena != "")
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

                _SQLUpit = _SQLUpit + _uslovZakljucena;
            }


            return _SQLUpit;
        }

        #endregion

        private void nadjiPonuduDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (!nadjiPonuduDataGridView.RowCount.Equals(0))
            {
                if (PonudaKlasa != null)
                {
                    PonudaKlasa.PrikaziPonudu(Int32.Parse(nadjiPonuduDataGridView.CurrentRow.Cells["Ponuda_ID"].Value.ToString()));
                    this.Close();
                }
            }
        }

        private void nadjiButton_Click_1(object sender, EventArgs e)
        {
            try
            {
                DBPonuda.NadjiPonudu(nadjiPonuduDataSet.vwPonudaZaglavlje, DajSQLUpit());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void resetujToolStripMenuItem_Click(object sender, EventArgs e)
        {
            brojServisneKnjiziceTextBox.Text = "";
            if (!poslovniPartnerComboBox.Items.Count.Equals(0))
            {
                poslovniPartnerComboBox.SelectedIndex = 0; 
            }
            zakljucenaCheckBox.Checked = false;
        }
    }
}