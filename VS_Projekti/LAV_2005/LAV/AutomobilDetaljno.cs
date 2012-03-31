using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAV
{
    public partial class AutomobilDetaljno : Form
    {
        DB.TipAutomobila DBTipAutomobila = new DB.TipAutomobila();

        public AutomobilDetaljno()
        {
            InitializeComponent();

            Inicijalizuj();
        }

        public AutomobilDetaljno(Int32 tipAutomobila_ID, string kompletanNazivTipa): this()
        {
            this.Text = kompletanNazivTipa;
            
            try
            {
                DBTipAutomobila.DajTipAutomobilaDetaljno(automobilDetaljnoDataSet.tipAutomobilaDetaljnoDataTable, tipAutomobila_ID);
                //DBTipAutomobila.DajMotorZaTipAutomobila(automobilDetaljnoDataSet.motorDataTable, tipAutomobila_ID);
            }
            catch (Exception)
            {
                MessageBox.Show("Greška pri čitanju podataka o tipu automobila.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Inicijulizuj()
        void Inicijalizuj()
        {
            //DataGridView podesavanja
            automobilDetaljnoDataGridView.MultiSelect = false;
            automobilDetaljnoDataGridView.AllowUserToAddRows = false;
            automobilDetaljnoDataGridView.AllowUserToResizeRows = false;
            automobilDetaljnoDataGridView.AllowUserToDeleteRows = false;
            automobilDetaljnoDataGridView.ReadOnly = true;
            automobilDetaljnoDataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            automobilDetaljnoDataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            automobilDetaljnoDataGridView.RowHeadersVisible = false;
            automobilDetaljnoDataGridView.ColumnHeadersVisible = false;

            
        } 
        #endregion

        #region Load

        private void AutomobilDetaljno_Load(object sender, EventArgs e)
        {
            if (!automobilDetaljnoDataSet.tipAutomobilaDetaljnoDataTable.Rows.Count.Equals(0))
            {
                foreach (DataColumn _kolona in automobilDetaljnoDataSet.tipAutomobilaDetaljnoDataTable.Columns)
                {
                    foreach (DataRow _red in automobilDetaljnoDataSet.tipAutomobilaDetaljnoDataTable.Rows)
                    {
                        if ((_red[_kolona].ToString() != "") & (_kolona.ToString() != "Tip automobila"))
                        {
                            if ((_kolona.ToString() == "Proizvodnja od") || (_kolona.ToString() == "Proizvodnja do"))
                            {
                                string[] _s = { _kolona.ToString(), PomocneKlase.Stringovi.StringUDatum(_red[_kolona].ToString())};
                                automobilDetaljnoDataGridView.Rows.Add(_s);
                            }
                            else
                            {
                                string[] _s = { _kolona.ToString(), _red[_kolona].ToString() };
                                automobilDetaljnoDataGridView.Rows.Add(_s);
                            }
                        }
                    }
                }

                if (!automobilDetaljnoDataSet.motorDataTable.Rows.Count.Equals(0))
                {
                    string[] _prazanRed = { "", ""};
                    automobilDetaljnoDataGridView.Rows.Add(_prazanRed);

                    bool _prviMotor = true;

                    foreach (DataRow _r in automobilDetaljnoDataSet.motorDataTable.Rows)
                    {
                        if (_prviMotor)
                        {
                            _prviMotor = false;

                            string[] _s = { "Motor", _r["Oznaka"].ToString() };
                            automobilDetaljnoDataGridView.Rows.Add(_s);
                        }
                        else
                        {
                            string[] _s = { "", _r["Oznaka"].ToString() };
                            automobilDetaljnoDataGridView.Rows.Add(_s);
                        }
                    }
                }
            }
        } 

        #endregion

        //prikazi podatke o motoru
        private void tipAutomobilaDetaljnoDataGridView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if ((tipAutomobilaDetaljnoDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString() == "Motor") || (tipAutomobilaDetaljnoDataGridView.Rows[e.RowIndex].Cells[0].Value.ToString() == "") && (tipAutomobilaDetaljnoDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString() != ""))
            //{
            //    DataView _dv = new DataView(tipAutomobilaDetaljnoDataSet.motorDataTable);
            //    _dv.Sort = "Oznaka";

            //    int _i = _dv.Find(tipAutomobilaDetaljnoDataGridView.Rows[e.RowIndex].Cells[1].Value.ToString());

            //    //_i je -1 ako nije nasao
            //    if (_i != -1)
            //    {
            //        MotorDetaljno motorDetaljno = new MotorDetaljno(Int16.Parse(_dv[_i]["Motor_ID"].ToString()), _dv[_i]["Oznaka"].ToString());
            //        motorDetaljno.ShowDialog();
            //    }
            //}
        }
 
    }
}