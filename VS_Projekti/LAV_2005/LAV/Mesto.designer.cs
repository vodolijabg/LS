namespace LAV
{
    partial class Mesto
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Mesto));
            this.mestoToolStrip = new System.Windows.Forms.ToolStrip();
            this.mestoStatusStrip = new System.Windows.Forms.StatusStrip();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.unesiButton = new System.Windows.Forms.Button();
            this.izmeniButton = new System.Windows.Forms.Button();
            this.potvrdiButton = new System.Windows.Forms.Button();
            this.odustaniButton = new System.Windows.Forms.Button();
            this.obrisiButton = new System.Windows.Forms.Button();
            this.krajButton = new System.Windows.Forms.Button();
            this.mestoDetaljnoTabControl = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.mesto_IDTextBox = new System.Windows.Forms.TextBox();
            this.nazivTextBox = new System.Windows.Forms.TextBox();
            this.postanskiBrojTextBox = new System.Windows.Forms.TextBox();
            this.pozivniBrojTextBox = new System.Windows.Forms.TextBox();
            this.mestoBindingNavigator = new System.Windows.Forms.BindingNavigator(this.components);
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.mestoDataGridView = new System.Windows.Forms.DataGridView();
            this.mestoErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.mestoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mestoDataSet = new LAV.DS.MestoDataSet();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.mestoDetaljnoTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mestoBindingNavigator)).BeginInit();
            this.mestoBindingNavigator.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mestoDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mestoErrorProvider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mestoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mestoDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // mestoToolStrip
            // 
            this.mestoToolStrip.Location = new System.Drawing.Point(0, 0);
            this.mestoToolStrip.Name = "mestoToolStrip";
            this.mestoToolStrip.Size = new System.Drawing.Size(792, 25);
            this.mestoToolStrip.TabIndex = 2;
            this.mestoToolStrip.Text = "toolStrip1";
            // 
            // mestoStatusStrip
            // 
            this.mestoStatusStrip.Location = new System.Drawing.Point(0, 544);
            this.mestoStatusStrip.Name = "mestoStatusStrip";
            this.mestoStatusStrip.Size = new System.Drawing.Size(792, 22);
            this.mestoStatusStrip.TabIndex = 3;
            this.mestoStatusStrip.Text = "statusStrip1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tableLayoutPanel2);
            this.groupBox1.Controls.Add(this.mestoDetaljnoTabControl);
            this.groupBox1.Controls.Add(this.mestoBindingNavigator);
            this.groupBox1.Controls.Add(this.mestoDataGridView);
            this.groupBox1.Location = new System.Drawing.Point(12, 40);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(768, 489);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mesto";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tableLayoutPanel2.ColumnCount = 7;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 99F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 97F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 95F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 106F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 146F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 158F));
            this.tableLayoutPanel2.Controls.Add(this.unesiButton, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.izmeniButton, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.potvrdiButton, 3, 0);
            this.tableLayoutPanel2.Controls.Add(this.odustaniButton, 4, 0);
            this.tableLayoutPanel2.Controls.Add(this.obrisiButton, 5, 0);
            this.tableLayoutPanel2.Controls.Add(this.krajButton, 6, 0);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(6, 447);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(756, 34);
            this.tableLayoutPanel2.TabIndex = 3;
            // 
            // unesiButton
            // 
            this.unesiButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.unesiButton.Location = new System.Drawing.Point(58, 3);
            this.unesiButton.Name = "unesiButton";
            this.unesiButton.Size = new System.Drawing.Size(75, 28);
            this.unesiButton.TabIndex = 0;
            this.unesiButton.Text = "&Unesi";
            this.unesiButton.UseVisualStyleBackColor = true;
            this.unesiButton.Click += new System.EventHandler(this.unesiButton_Click);
            // 
            // izmeniButton
            // 
            this.izmeniButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.izmeniButton.Location = new System.Drawing.Point(157, 3);
            this.izmeniButton.Name = "izmeniButton";
            this.izmeniButton.Size = new System.Drawing.Size(75, 28);
            this.izmeniButton.TabIndex = 1;
            this.izmeniButton.Text = "&Izmeni";
            this.izmeniButton.UseVisualStyleBackColor = true;
            this.izmeniButton.Click += new System.EventHandler(this.izmeniButton_Click);
            // 
            // potvrdiButton
            // 
            this.potvrdiButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.potvrdiButton.Location = new System.Drawing.Point(254, 3);
            this.potvrdiButton.Name = "potvrdiButton";
            this.potvrdiButton.Size = new System.Drawing.Size(75, 28);
            this.potvrdiButton.TabIndex = 2;
            this.potvrdiButton.Text = "&Potvrdi";
            this.potvrdiButton.UseVisualStyleBackColor = true;
            this.potvrdiButton.Click += new System.EventHandler(this.potvrdiButton_Click);
            // 
            // odustaniButton
            // 
            this.odustaniButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.odustaniButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.odustaniButton.Location = new System.Drawing.Point(349, 3);
            this.odustaniButton.Name = "odustaniButton";
            this.odustaniButton.Size = new System.Drawing.Size(75, 28);
            this.odustaniButton.TabIndex = 3;
            this.odustaniButton.Text = "&Odustani";
            this.odustaniButton.UseVisualStyleBackColor = true;
            this.odustaniButton.Click += new System.EventHandler(this.odustaniButton_Click);
            // 
            // obrisiButton
            // 
            this.obrisiButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.obrisiButton.Location = new System.Drawing.Point(455, 3);
            this.obrisiButton.Name = "obrisiButton";
            this.obrisiButton.Size = new System.Drawing.Size(75, 28);
            this.obrisiButton.TabIndex = 4;
            this.obrisiButton.Text = "O&briši";
            this.obrisiButton.UseVisualStyleBackColor = true;
            this.obrisiButton.Click += new System.EventHandler(this.obrisiButton_Click);
            // 
            // krajButton
            // 
            this.krajButton.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.krajButton.Location = new System.Drawing.Point(601, 3);
            this.krajButton.Name = "krajButton";
            this.krajButton.Size = new System.Drawing.Size(75, 28);
            this.krajButton.TabIndex = 5;
            this.krajButton.Text = "&Kraj";
            this.krajButton.UseVisualStyleBackColor = true;
            this.krajButton.Click += new System.EventHandler(this.krajButton_Click);
            // 
            // mestoDetaljnoTabControl
            // 
            this.mestoDetaljnoTabControl.Controls.Add(this.tabPage1);
            this.mestoDetaljnoTabControl.Location = new System.Drawing.Point(6, 224);
            this.mestoDetaljnoTabControl.Name = "mestoDetaljnoTabControl";
            this.mestoDetaljnoTabControl.SelectedIndex = 0;
            this.mestoDetaljnoTabControl.Size = new System.Drawing.Size(756, 219);
            this.mestoDetaljnoTabControl.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.tabPage1.Controls.Add(this.tableLayoutPanel1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(748, 193);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Detaljno";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 48F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 15F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 579F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.label6, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.mesto_IDTextBox, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.nazivTextBox, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.postanskiBrojTextBox, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.pozivniBrojTextBox, 3, 2);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(742, 187);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mesto_ID";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Naziv";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Pozivni broj";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 102);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Poštanski broj";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(151, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(9, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "*";
            // 
            // mesto_IDTextBox
            // 
            this.mesto_IDTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.mesto_IDTextBox.Enabled = false;
            this.mesto_IDTextBox.Location = new System.Drawing.Point(166, 5);
            this.mesto_IDTextBox.Name = "mesto_IDTextBox";
            this.mesto_IDTextBox.Size = new System.Drawing.Size(121, 20);
            this.mesto_IDTextBox.TabIndex = 9;
            // 
            // nazivTextBox
            // 
            this.nazivTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.nazivTextBox.Location = new System.Drawing.Point(166, 36);
            this.nazivTextBox.Name = "nazivTextBox";
            this.nazivTextBox.Size = new System.Drawing.Size(255, 20);
            this.nazivTextBox.TabIndex = 10;
            // 
            // postanskiBrojTextBox
            // 
            this.postanskiBrojTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.postanskiBrojTextBox.Location = new System.Drawing.Point(166, 98);
            this.postanskiBrojTextBox.Name = "postanskiBrojTextBox";
            this.postanskiBrojTextBox.Size = new System.Drawing.Size(100, 20);
            this.postanskiBrojTextBox.TabIndex = 12;
            // 
            // pozivniBrojTextBox
            // 
            this.pozivniBrojTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pozivniBrojTextBox.Location = new System.Drawing.Point(166, 67);
            this.pozivniBrojTextBox.Name = "pozivniBrojTextBox";
            this.pozivniBrojTextBox.Size = new System.Drawing.Size(100, 20);
            this.pozivniBrojTextBox.TabIndex = 11;
            // 
            // mestoBindingNavigator
            // 
            this.mestoBindingNavigator.AddNewItem = null;
            this.mestoBindingNavigator.BackColor = System.Drawing.SystemColors.Control;
            this.mestoBindingNavigator.CountItem = this.bindingNavigatorCountItem;
            this.mestoBindingNavigator.DeleteItem = null;
            this.mestoBindingNavigator.Dock = System.Windows.Forms.DockStyle.None;
            this.mestoBindingNavigator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2});
            this.mestoBindingNavigator.Location = new System.Drawing.Point(6, 196);
            this.mestoBindingNavigator.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.mestoBindingNavigator.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.mestoBindingNavigator.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.mestoBindingNavigator.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.mestoBindingNavigator.Name = "mestoBindingNavigator";
            this.mestoBindingNavigator.PositionItem = this.bindingNavigatorPositionItem;
            this.mestoBindingNavigator.Size = new System.Drawing.Size(210, 25);
            this.mestoBindingNavigator.TabIndex = 1;
            this.mestoBindingNavigator.Text = "bindingNavigator1";
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(36, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 21);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // mestoDataGridView
            // 
            this.mestoDataGridView.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.mestoDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.mestoDataGridView.Location = new System.Drawing.Point(6, 19);
            this.mestoDataGridView.Name = "mestoDataGridView";
            this.mestoDataGridView.Size = new System.Drawing.Size(756, 174);
            this.mestoDataGridView.TabIndex = 0;
            this.mestoDataGridView.RowEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.mestoDataGridView_RowEnter);
            this.mestoDataGridView.Sorted += new System.EventHandler(this.mestoDataGridView_Sorted);
            // 
            // mestoErrorProvider
            // 
            this.mestoErrorProvider.ContainerControl = this;
            // 
            // mestoDataSet
            // 
            this.mestoDataSet.DataSetName = "MestoDataSet";
            this.mestoDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Mesto
            // 
            this.AcceptButton = this.potvrdiButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.odustaniButton;
            this.ClientSize = new System.Drawing.Size(792, 566);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.mestoStatusStrip);
            this.Controls.Add(this.mestoToolStrip);
            this.Name = "Mesto";
            this.Text = "Mesto";
            this.Load += new System.EventHandler(this.Mesto_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.mestoDetaljnoTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mestoBindingNavigator)).EndInit();
            this.mestoBindingNavigator.ResumeLayout(false);
            this.mestoBindingNavigator.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mestoDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mestoErrorProvider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mestoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mestoDataSet)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip mestoToolStrip;
        private System.Windows.Forms.StatusStrip mestoStatusStrip;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Button unesiButton;
        private System.Windows.Forms.Button izmeniButton;
        private System.Windows.Forms.Button potvrdiButton;
        private System.Windows.Forms.Button odustaniButton;
        private System.Windows.Forms.Button obrisiButton;
        private System.Windows.Forms.Button krajButton;
        private System.Windows.Forms.TabControl mestoDetaljnoTabControl;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox mesto_IDTextBox;
        private System.Windows.Forms.TextBox nazivTextBox;
        private System.Windows.Forms.BindingNavigator mestoBindingNavigator;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.DataGridView mestoDataGridView;
        private System.Windows.Forms.ErrorProvider mestoErrorProvider;
        private System.Windows.Forms.BindingSource mestoBindingSource;
        private System.Windows.Forms.TextBox postanskiBrojTextBox;
        private System.Windows.Forms.TextBox pozivniBrojTextBox;
        private LAV.DS.MestoDataSet mestoDataSet;
    }
}