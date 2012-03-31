namespace LAV
{
    partial class DodatniPodaci
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cenaUgradnjeTextBox = new System.Windows.Forms.TextBox();
            this.normaSatiTextBox = new System.Windows.Forms.TextBox();
            this.napomenaRichTextBox = new System.Windows.Forms.RichTextBox();
            this.potvrdiButton = new System.Windows.Forms.Button();
            this.dodatniPodaciErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dodatniPodaciErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25.94595F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 2.432432F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 71.89189F));
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.cenaUgradnjeTextBox, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.normaSatiTextBox, 2, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.63291F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 49.36709F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(370, 69);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 52);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(62, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Napomena:";
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Cena ugradnje";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Norma sati";
            // 
            // cenaUgradnjeTextBox
            // 
            this.cenaUgradnjeTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cenaUgradnjeTextBox.Location = new System.Drawing.Point(106, 3);
            this.cenaUgradnjeTextBox.Name = "cenaUgradnjeTextBox";
            this.cenaUgradnjeTextBox.Size = new System.Drawing.Size(224, 20);
            this.cenaUgradnjeTextBox.TabIndex = 2;
            // 
            // normaSatiTextBox
            // 
            this.normaSatiTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.normaSatiTextBox.Location = new System.Drawing.Point(106, 27);
            this.normaSatiTextBox.Name = "normaSatiTextBox";
            this.normaSatiTextBox.Size = new System.Drawing.Size(224, 20);
            this.normaSatiTextBox.TabIndex = 3;
            // 
            // napomenaRichTextBox
            // 
            this.napomenaRichTextBox.Location = new System.Drawing.Point(12, 87);
            this.napomenaRichTextBox.Name = "napomenaRichTextBox";
            this.napomenaRichTextBox.Size = new System.Drawing.Size(370, 134);
            this.napomenaRichTextBox.TabIndex = 1;
            this.napomenaRichTextBox.Text = "";
            // 
            // potvrdiButton
            // 
            this.potvrdiButton.Location = new System.Drawing.Point(307, 233);
            this.potvrdiButton.Name = "potvrdiButton";
            this.potvrdiButton.Size = new System.Drawing.Size(75, 23);
            this.potvrdiButton.TabIndex = 2;
            this.potvrdiButton.Text = "&Potvrdi";
            this.potvrdiButton.UseVisualStyleBackColor = true;
            this.potvrdiButton.Click += new System.EventHandler(this.potvrdiButton_Click);
            // 
            // dodatniPodaciErrorProvider
            // 
            this.dodatniPodaciErrorProvider.ContainerControl = this;
            // 
            // DodatniPodaci
            // 
            this.AcceptButton = this.potvrdiButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 268);
            this.Controls.Add(this.potvrdiButton);
            this.Controls.Add(this.napomenaRichTextBox);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DodatniPodaci";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dodatni podaci";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dodatniPodaciErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RichTextBox napomenaRichTextBox;
        private System.Windows.Forms.Button potvrdiButton;
        private System.Windows.Forms.TextBox cenaUgradnjeTextBox;
        private System.Windows.Forms.TextBox normaSatiTextBox;
        private System.Windows.Forms.ErrorProvider dodatniPodaciErrorProvider;
    }
}