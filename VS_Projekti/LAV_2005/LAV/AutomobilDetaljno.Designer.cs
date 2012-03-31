namespace LAV
{
    partial class AutomobilDetaljno
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
            this.automobilDetaljnoDataGridView = new System.Windows.Forms.DataGridView();
            this.Kolona = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Red = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.automobilDetaljnoDataSet = new LAV.DS.AutomobilDetaljnoDataSet();
            ((System.ComponentModel.ISupportInitialize)(this.automobilDetaljnoDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.automobilDetaljnoDataSet)).BeginInit();
            this.SuspendLayout();
            // 
            // automobilDetaljnoDataGridView
            // 
            this.automobilDetaljnoDataGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.automobilDetaljnoDataGridView.BackgroundColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.automobilDetaljnoDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Kolona,
            this.Red});
            this.automobilDetaljnoDataGridView.Location = new System.Drawing.Point(12, 12);
            this.automobilDetaljnoDataGridView.Name = "automobilDetaljnoDataGridView";
            this.automobilDetaljnoDataGridView.Size = new System.Drawing.Size(418, 442);
            this.automobilDetaljnoDataGridView.TabIndex = 1;
            // 
            // Kolona
            // 
            this.Kolona.HeaderText = "Kolona";
            this.Kolona.Name = "Kolona";
            // 
            // Red
            // 
            this.Red.HeaderText = "Red";
            this.Red.Name = "Red";
            // 
            // automobilDetaljnoDataSet
            // 
            this.automobilDetaljnoDataSet.DataSetName = "AutomobilDetaljnoDataSet";
            this.automobilDetaljnoDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // AutomobilDetaljno
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(442, 466);
            this.Controls.Add(this.automobilDetaljnoDataGridView);
            this.MinimizeBox = false;
            this.Name = "AutomobilDetaljno";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Automobil detaljno";
            this.Load += new System.EventHandler(this.AutomobilDetaljno_Load);
            ((System.ComponentModel.ISupportInitialize)(this.automobilDetaljnoDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.automobilDetaljnoDataSet)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView automobilDetaljnoDataGridView;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kolona;
        private System.Windows.Forms.DataGridViewTextBoxColumn Red;
        private LAV.DS.AutomobilDetaljnoDataSet automobilDetaljnoDataSet;
    }
}