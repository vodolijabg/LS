namespace LAV
{
    partial class Automobil
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tipAutomobilaDataGridView = new System.Windows.Forms.DataGridView();
            this.Detaljno = new System.Windows.Forms.DataGridViewImageColumn();
            this.modelAutomobilaComboBox = new System.Windows.Forms.ComboBox();
            this.dataGridViewImageColumn1 = new System.Windows.Forms.DataGridViewImageColumn();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.proizvodjacAutomobilaComboBox = new System.Windows.Forms.ComboBox();
            this.automobilDataSet = new LAV.DS.AutomobilDataSet();
            this.dataGridViewImageColumn2 = new System.Windows.Forms.DataGridViewImageColumn();
            ((System.ComponentModel.ISupportInitialize)(this.tipAutomobilaDataGridView)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.automobilDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // tipAutomobilaDataGridView
            // 
            this.tipAutomobilaDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tipAutomobilaDataGridView.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tipAutomobilaDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.tipAutomobilaDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Detaljno});
            this.tipAutomobilaDataGridView.Location = new System.Drawing.Point(12, 78);
            this.tipAutomobilaDataGridView.Name = "tipAutomobilaDataGridView";
            this.tipAutomobilaDataGridView.RowHeadersVisible = false;
            this.tipAutomobilaDataGridView.Size = new System.Drawing.Size(768, 476);
            this.tipAutomobilaDataGridView.TabIndex = 4;
            this.tipAutomobilaDataGridView.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.tipAutomobilaDataGridView_CellMouseClick);
            // 
            // Detaljno
            // 
            this.Detaljno.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.Detaljno.HeaderText = "     ";
            this.Detaljno.Image = global::LAV.Properties.Resources.Collapsed;
            this.Detaljno.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.Detaljno.Name = "Detaljno";
            this.Detaljno.Width = 25;
            // 
            // modelAutomobilaComboBox
            // 
            this.modelAutomobilaComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.modelAutomobilaComboBox.FormattingEnabled = true;
            this.modelAutomobilaComboBox.Location = new System.Drawing.Point(124, 34);
            this.modelAutomobilaComboBox.Name = "modelAutomobilaComboBox";
            this.modelAutomobilaComboBox.Size = new System.Drawing.Size(437, 21);
            this.modelAutomobilaComboBox.TabIndex = 3;
            this.modelAutomobilaComboBox.SelectedIndexChanged += new System.EventHandler(this.modelAutomobilaComboBox_SelectedIndexChanged);
            // 
            // dataGridViewImageColumn1
            // 
            this.dataGridViewImageColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewImageColumn1.HeaderText = "";
            this.dataGridViewImageColumn1.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn1.Name = "dataGridViewImageColumn1";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Proizvođač automobila";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Model automobila";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 17.36012F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 82.63988F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.proizvodjacAutomobilaComboBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.modelAutomobilaComboBox, 1, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(697, 60);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // proizvodjacAutomobilaComboBox
            // 
            this.proizvodjacAutomobilaComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.proizvodjacAutomobilaComboBox.FormattingEnabled = true;
            this.proizvodjacAutomobilaComboBox.Location = new System.Drawing.Point(124, 4);
            this.proizvodjacAutomobilaComboBox.Name = "proizvodjacAutomobilaComboBox";
            this.proizvodjacAutomobilaComboBox.Size = new System.Drawing.Size(236, 21);
            this.proizvodjacAutomobilaComboBox.TabIndex = 2;
            this.proizvodjacAutomobilaComboBox.SelectedIndexChanged += new System.EventHandler(this.proizvodjacAutomobilaComboBox_SelectedIndexChanged);
            // 
            // automobilDataSet
            // 
            this.automobilDataSet.DataSetName = "AutomobilDataSet";
            this.automobilDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataGridViewImageColumn2
            // 
            this.dataGridViewImageColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.ColumnHeader;
            this.dataGridViewImageColumn2.HeaderText = "     ";
            this.dataGridViewImageColumn2.Image = global::LAV.Properties.Resources.Collapsed;
            this.dataGridViewImageColumn2.ImageLayout = System.Windows.Forms.DataGridViewImageCellLayout.Zoom;
            this.dataGridViewImageColumn2.Name = "dataGridViewImageColumn2";
            // 
            // Automobil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.ControlBox = false;
            this.Controls.Add(this.tipAutomobilaDataGridView);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Automobil";
            this.Text = "Automobil";
            this.Load += new System.EventHandler(this.Automobil_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tipAutomobilaDataGridView)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.automobilDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView tipAutomobilaDataGridView;
        private System.Windows.Forms.ComboBox modelAutomobilaComboBox;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ComboBox proizvodjacAutomobilaComboBox;
        private LAV.DS.AutomobilDataSet automobilDataSet;
        private System.Windows.Forms.DataGridViewImageColumn Detaljno;
        private System.Windows.Forms.DataGridViewImageColumn dataGridViewImageColumn2;
    }
}