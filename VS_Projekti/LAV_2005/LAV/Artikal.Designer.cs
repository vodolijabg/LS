namespace LAV
{
    partial class Artikal
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Artikal));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.nadjiButton = new System.Windows.Forms.Button();
            this.slicnoTrazenjeCheckBox = new System.Windows.Forms.CheckBox();
            this.biloKojiBrojCheckBox = new System.Windows.Forms.CheckBox();
            this.brojProizvodjacaCheckBox = new System.Windows.Forms.CheckBox();
            this.koriscenBrojCheckBox = new System.Windows.Forms.CheckBox();
            this.oEBrojCheckBox = new System.Windows.Forms.CheckBox();
            this.eANBrojCheckBoxe = new System.Windows.Forms.CheckBox();
            this.uporedniBrojCheckBox = new System.Windows.Forms.CheckBox();
            this.brojZaPretraguComboBox = new System.Windows.Forms.ComboBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.artikalDataGridView = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.napomenaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vezaArtikalDobavljacDataGridView = new System.Windows.Forms.DataGridView();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.brojacToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.importToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.exportToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.artikalDataSet = new LAV.DS.ArtikalDataSet();
            this.tableLayoutPanel1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.artikalDataGridView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vezaArtikalDobavljacDataGridView)).BeginInit();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.artikalDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 5;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 113F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 226F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 29F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 230F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.nadjiButton, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.slicnoTrazenjeCheckBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.biloKojiBrojCheckBox, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.brojProizvodjacaCheckBox, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.koriscenBrojCheckBox, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.oEBrojCheckBox, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.eANBrojCheckBoxe, 4, 2);
            this.tableLayoutPanel1.Controls.Add(this.uporedniBrojCheckBox, 4, 1);
            this.tableLayoutPanel1.Controls.Add(this.brojZaPretraguComboBox, 1, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 28);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 25F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(768, 110);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(36, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Broj proizvoda";
            // 
            // nadjiButton
            // 
            this.nadjiButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nadjiButton.BackgroundImage = global::LAV.Properties.Resources.search;
            this.nadjiButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.nadjiButton.Location = new System.Drawing.Point(342, 3);
            this.nadjiButton.Name = "nadjiButton";
            this.nadjiButton.Size = new System.Drawing.Size(21, 21);
            this.nadjiButton.TabIndex = 2;
            this.nadjiButton.UseVisualStyleBackColor = true;
            this.nadjiButton.Click += new System.EventHandler(this.nadjiButton_Click);
            // 
            // slicnoTrazenjeCheckBox
            // 
            this.slicnoTrazenjeCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.slicnoTrazenjeCheckBox.AutoSize = true;
            this.slicnoTrazenjeCheckBox.Location = new System.Drawing.Point(116, 32);
            this.slicnoTrazenjeCheckBox.Name = "slicnoTrazenjeCheckBox";
            this.slicnoTrazenjeCheckBox.Size = new System.Drawing.Size(95, 17);
            this.slicnoTrazenjeCheckBox.TabIndex = 3;
            this.slicnoTrazenjeCheckBox.Text = "Slično traženje";
            this.slicnoTrazenjeCheckBox.UseVisualStyleBackColor = true;
            // 
            // biloKojiBrojCheckBox
            // 
            this.biloKojiBrojCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.biloKojiBrojCheckBox.AutoSize = true;
            this.biloKojiBrojCheckBox.Location = new System.Drawing.Point(371, 5);
            this.biloKojiBrojCheckBox.Name = "biloKojiBrojCheckBox";
            this.biloKojiBrojCheckBox.Size = new System.Drawing.Size(82, 17);
            this.biloKojiBrojCheckBox.TabIndex = 4;
            this.biloKojiBrojCheckBox.Text = "Bilo koji broj";
            this.biloKojiBrojCheckBox.UseVisualStyleBackColor = true;
            this.biloKojiBrojCheckBox.CheckedChanged += new System.EventHandler(this.biloKojiBrojCheckBox_CheckedChanged);
            // 
            // brojProizvodjacaCheckBox
            // 
            this.brojProizvodjacaCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.brojProizvodjacaCheckBox.AutoSize = true;
            this.brojProizvodjacaCheckBox.Location = new System.Drawing.Point(371, 32);
            this.brojProizvodjacaCheckBox.Name = "brojProizvodjacaCheckBox";
            this.brojProizvodjacaCheckBox.Size = new System.Drawing.Size(106, 17);
            this.brojProizvodjacaCheckBox.TabIndex = 5;
            this.brojProizvodjacaCheckBox.Text = "Broj proizvođača";
            this.brojProizvodjacaCheckBox.UseVisualStyleBackColor = true;
            this.brojProizvodjacaCheckBox.CheckedChanged += new System.EventHandler(this.brojeviCheckBox_CheckedChanged);
            // 
            // koriscenBrojCheckBox
            // 
            this.koriscenBrojCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.koriscenBrojCheckBox.AutoSize = true;
            this.koriscenBrojCheckBox.Location = new System.Drawing.Point(371, 59);
            this.koriscenBrojCheckBox.Name = "koriscenBrojCheckBox";
            this.koriscenBrojCheckBox.Size = new System.Drawing.Size(87, 17);
            this.koriscenBrojCheckBox.TabIndex = 6;
            this.koriscenBrojCheckBox.Text = "Korišćen broj";
            this.koriscenBrojCheckBox.UseVisualStyleBackColor = true;
            this.koriscenBrojCheckBox.CheckedChanged += new System.EventHandler(this.brojeviCheckBox_CheckedChanged);
            // 
            // oEBrojCheckBox
            // 
            this.oEBrojCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.oEBrojCheckBox.AutoSize = true;
            this.oEBrojCheckBox.Location = new System.Drawing.Point(371, 87);
            this.oEBrojCheckBox.Name = "oEBrojCheckBox";
            this.oEBrojCheckBox.Size = new System.Drawing.Size(61, 17);
            this.oEBrojCheckBox.TabIndex = 7;
            this.oEBrojCheckBox.Text = "OE broj";
            this.oEBrojCheckBox.UseVisualStyleBackColor = true;
            this.oEBrojCheckBox.CheckedChanged += new System.EventHandler(this.brojeviCheckBox_CheckedChanged);
            // 
            // eANBrojCheckBoxe
            // 
            this.eANBrojCheckBoxe.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.eANBrojCheckBoxe.AutoSize = true;
            this.eANBrojCheckBoxe.Location = new System.Drawing.Point(541, 59);
            this.eANBrojCheckBoxe.Name = "eANBrojCheckBoxe";
            this.eANBrojCheckBoxe.Size = new System.Drawing.Size(68, 17);
            this.eANBrojCheckBoxe.TabIndex = 9;
            this.eANBrojCheckBoxe.Text = "EAN broj";
            this.eANBrojCheckBoxe.UseVisualStyleBackColor = true;
            this.eANBrojCheckBoxe.CheckedChanged += new System.EventHandler(this.brojeviCheckBox_CheckedChanged);
            // 
            // uporedniBrojCheckBox
            // 
            this.uporedniBrojCheckBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.uporedniBrojCheckBox.AutoSize = true;
            this.uporedniBrojCheckBox.Location = new System.Drawing.Point(541, 32);
            this.uporedniBrojCheckBox.Name = "uporedniBrojCheckBox";
            this.uporedniBrojCheckBox.Size = new System.Drawing.Size(89, 17);
            this.uporedniBrojCheckBox.TabIndex = 8;
            this.uporedniBrojCheckBox.Text = "Uporedni broj";
            this.uporedniBrojCheckBox.UseVisualStyleBackColor = true;
            this.uporedniBrojCheckBox.CheckedChanged += new System.EventHandler(this.brojeviCheckBox_CheckedChanged);
            // 
            // brojZaPretraguComboBox
            // 
            this.brojZaPretraguComboBox.FormattingEnabled = true;
            this.brojZaPretraguComboBox.Location = new System.Drawing.Point(116, 3);
            this.brojZaPretraguComboBox.Name = "brojZaPretraguComboBox";
            this.brojZaPretraguComboBox.Size = new System.Drawing.Size(220, 21);
            this.brojZaPretraguComboBox.TabIndex = 10;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 144);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.artikalDataGridView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.vezaArtikalDobavljacDataGridView);
            this.splitContainer1.Size = new System.Drawing.Size(768, 410);
            this.splitContainer1.SplitterDistance = 246;
            this.splitContainer1.TabIndex = 1;
            // 
            // artikalDataGridView
            // 
            this.artikalDataGridView.AllowUserToAddRows = false;
            this.artikalDataGridView.AllowUserToDeleteRows = false;
            this.artikalDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.artikalDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.artikalDataGridView.ContextMenuStrip = this.contextMenuStrip1;
            this.artikalDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.artikalDataGridView.Location = new System.Drawing.Point(0, 0);
            this.artikalDataGridView.Name = "artikalDataGridView";
            this.artikalDataGridView.ReadOnly = true;
            this.artikalDataGridView.Size = new System.Drawing.Size(768, 246);
            this.artikalDataGridView.TabIndex = 0;
            this.artikalDataGridView.Paint += new System.Windows.Forms.PaintEventHandler(this.artikalDataGridView_Paint);
            this.artikalDataGridView.KeyUp += new System.Windows.Forms.KeyEventHandler(this.artikalDataGridView_KeyUp);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.napomenaToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(146, 26);
            // 
            // napomenaToolStripMenuItem
            // 
            this.napomenaToolStripMenuItem.Name = "napomenaToolStripMenuItem";
            this.napomenaToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.napomenaToolStripMenuItem.Text = "Dodatni podaci";
            this.napomenaToolStripMenuItem.Click += new System.EventHandler(this.dodatniPodaciToolStripMenuItem_Click);
            // 
            // vezaArtikalDobavljacDataGridView
            // 
            this.vezaArtikalDobavljacDataGridView.AllowUserToAddRows = false;
            this.vezaArtikalDobavljacDataGridView.AllowUserToDeleteRows = false;
            this.vezaArtikalDobavljacDataGridView.BackgroundColor = System.Drawing.SystemColors.Window;
            this.vezaArtikalDobavljacDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.vezaArtikalDobavljacDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vezaArtikalDobavljacDataGridView.Location = new System.Drawing.Point(0, 0);
            this.vezaArtikalDobavljacDataGridView.Name = "vezaArtikalDobavljacDataGridView";
            this.vezaArtikalDobavljacDataGridView.ReadOnly = true;
            this.vezaArtikalDobavljacDataGridView.Size = new System.Drawing.Size(768, 160);
            this.vezaArtikalDobavljacDataGridView.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripSeparator1,
            this.brojacToolStripButton,
            this.toolStripSeparator2,
            this.importToolStripButton,
            this.toolStripSeparator3,
            this.exportToolStripButton,
            this.toolStripSeparator4});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(792, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // brojacToolStripButton
            // 
            this.brojacToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.brojacToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("brojacToolStripButton.Image")));
            this.brojacToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.brojacToolStripButton.Name = "brojacToolStripButton";
            this.brojacToolStripButton.Size = new System.Drawing.Size(41, 22);
            this.brojacToolStripButton.Text = "Brojač";
            this.brojacToolStripButton.Click += new System.EventHandler(this.brojacToolStripButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // importToolStripButton
            // 
            this.importToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.importToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("importToolStripButton.Image")));
            this.importToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.importToolStripButton.Name = "importToolStripButton";
            this.importToolStripButton.Size = new System.Drawing.Size(43, 22);
            this.importToolStripButton.Text = "Import";
            this.importToolStripButton.Click += new System.EventHandler(this.uveziCenovnikToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // exportToolStripButton
            // 
            this.exportToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.exportToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("exportToolStripButton.Image")));
            this.exportToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.exportToolStripButton.Name = "exportToolStripButton";
            this.exportToolStripButton.Size = new System.Drawing.Size(43, 22);
            this.exportToolStripButton.Text = "Export";
            this.exportToolStripButton.Click += new System.EventHandler(this.exportToolStripButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // artikalDataSet
            // 
            this.artikalDataSet.DataSetName = "ArtikalDataSet";
            this.artikalDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Artikal
            // 
            this.AcceptButton = this.nadjiButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.ControlBox = false;
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Artikal";
            this.Text = "Artikal";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Artikal_FormClosed);
            this.Load += new System.EventHandler(this.Artikal_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.artikalDataGridView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vezaArtikalDobavljacDataGridView)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.artikalDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button nadjiButton;
        private System.Windows.Forms.CheckBox slicnoTrazenjeCheckBox;
        private System.Windows.Forms.CheckBox biloKojiBrojCheckBox;
        private System.Windows.Forms.CheckBox brojProizvodjacaCheckBox;
        private System.Windows.Forms.CheckBox koriscenBrojCheckBox;
        private System.Windows.Forms.CheckBox oEBrojCheckBox;
        private System.Windows.Forms.CheckBox uporedniBrojCheckBox;
        private System.Windows.Forms.CheckBox eANBrojCheckBoxe;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.DataGridView artikalDataGridView;
        private System.Windows.Forms.DataGridView vezaArtikalDobavljacDataGridView;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem napomenaToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton brojacToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton importToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton exportToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private LAV.DS.ArtikalDataSet artikalDataSet;
        private System.Windows.Forms.ComboBox brojZaPretraguComboBox;
    }
}