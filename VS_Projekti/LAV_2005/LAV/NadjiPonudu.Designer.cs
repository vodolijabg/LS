namespace LAV
{
    partial class NadjiPonudu
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
            this.components = new System.ComponentModel.Container();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.nadjiPonuduDataGridView = new System.Windows.Forms.DataGridView();
            this.nadjiButton = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.brojServisneKnjiziceTextBox = new System.Windows.Forms.TextBox();
            this.poslovniPartnerComboBox = new System.Windows.Forms.ComboBox();
            this.zakljucenaCheckBox = new System.Windows.Forms.CheckBox();
            this.nadjiPonuduDataSet = new LAV.DS.NadjiPonuduDataSet();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.resetujToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.nadjiPonuduDataGridView)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nadjiPonuduDataSet)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(105, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Broj servisne knjižice";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 46);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(83, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Poslovni partner";
            // 
            // nadjiPonuduDataGridView
            // 
            this.nadjiPonuduDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.nadjiPonuduDataGridView.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.nadjiPonuduDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.nadjiPonuduDataGridView.Location = new System.Drawing.Point(12, 158);
            this.nadjiPonuduDataGridView.Name = "nadjiPonuduDataGridView";
            this.nadjiPonuduDataGridView.Size = new System.Drawing.Size(768, 396);
            this.nadjiPonuduDataGridView.TabIndex = 3;
            this.nadjiPonuduDataGridView.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.nadjiPonuduDataGridView_CellMouseDoubleClick);
            // 
            // nadjiButton
            // 
            this.nadjiButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nadjiButton.BackgroundImage = global::LAV.Properties.Resources.search;
            this.nadjiButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.nadjiButton.Location = new System.Drawing.Point(376, 77);
            this.nadjiButton.Name = "nadjiButton";
            this.nadjiButton.Size = new System.Drawing.Size(23, 23);
            this.nadjiButton.TabIndex = 8;
            this.nadjiButton.UseVisualStyleBackColor = true;
            this.nadjiButton.Click += new System.EventHandler(this.nadjiButton_Click_1);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.ContextMenuStrip = this.contextMenuStrip1;
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(768, 140);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Uslov za pretragu";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 6;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 124F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 249F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 256F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.brojServisneKnjiziceTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.poslovniPartnerComboBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.nadjiButton, 2, 2);
            this.tableLayoutPanel1.Controls.Add(this.zakljucenaCheckBox, 1, 2);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(6, 19);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(756, 107);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // brojServisneKnjiziceTextBox
            // 
            this.brojServisneKnjiziceTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.brojServisneKnjiziceTextBox.Location = new System.Drawing.Point(127, 7);
            this.brojServisneKnjiziceTextBox.Name = "brojServisneKnjiziceTextBox";
            this.brojServisneKnjiziceTextBox.Size = new System.Drawing.Size(225, 20);
            this.brojServisneKnjiziceTextBox.TabIndex = 10;
            // 
            // poslovniPartnerComboBox
            // 
            this.poslovniPartnerComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.poslovniPartnerComboBox.FormattingEnabled = true;
            this.poslovniPartnerComboBox.Location = new System.Drawing.Point(127, 42);
            this.poslovniPartnerComboBox.Name = "poslovniPartnerComboBox";
            this.poslovniPartnerComboBox.Size = new System.Drawing.Size(225, 21);
            this.poslovniPartnerComboBox.TabIndex = 6;
            // 
            // zakljucenaCheckBox
            // 
            this.zakljucenaCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.zakljucenaCheckBox.AutoSize = true;
            this.zakljucenaCheckBox.Location = new System.Drawing.Point(127, 80);
            this.zakljucenaCheckBox.Name = "zakljucenaCheckBox";
            this.zakljucenaCheckBox.Size = new System.Drawing.Size(79, 17);
            this.zakljucenaCheckBox.TabIndex = 11;
            this.zakljucenaCheckBox.Text = "Zaključena";
            this.zakljucenaCheckBox.UseVisualStyleBackColor = true;
            // 
            // nadjiPonuduDataSet
            // 
            this.nadjiPonuduDataSet.DataSetName = "NadjiPonuduDataSet";
            this.nadjiPonuduDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetujToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(112, 26);
            // 
            // resetujToolStripMenuItem
            // 
            this.resetujToolStripMenuItem.Name = "resetujToolStripMenuItem";
            this.resetujToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.resetujToolStripMenuItem.Text = "Resetuj";
            this.resetujToolStripMenuItem.Click += new System.EventHandler(this.resetujToolStripMenuItem_Click);
            // 
            // NadjiPonudu
            // 
            this.AcceptButton = this.nadjiButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.Controls.Add(this.nadjiPonuduDataGridView);
            this.Controls.Add(this.groupBox1);
            this.MinimizeBox = false;
            this.Name = "NadjiPonudu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nađi ponudu";
            this.Load += new System.EventHandler(this.NadjiPonudu_Load);
            ((System.ComponentModel.ISupportInitialize)(this.nadjiPonuduDataGridView)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nadjiPonuduDataSet)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView nadjiPonuduDataGridView;
        private System.Windows.Forms.Button nadjiButton;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TextBox brojServisneKnjiziceTextBox;
        private System.Windows.Forms.ComboBox poslovniPartnerComboBox;
        private System.Windows.Forms.CheckBox zakljucenaCheckBox;
        private LAV.DS.NadjiPonuduDataSet nadjiPonuduDataSet;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem resetujToolStripMenuItem;
    }
}