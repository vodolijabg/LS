namespace LAV
{
    partial class Konekcija
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
            this.testKonekcijaButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SQLServerTextBox = new System.Windows.Forms.TextBox();
            this.SQLBazaTextBox = new System.Windows.Forms.TextBox();
            this.autentifikacijaComboBox = new System.Windows.Forms.ComboBox();
            this.korisnikTextBox = new System.Windows.Forms.TextBox();
            this.lozinkaTextBox = new System.Windows.Forms.TextBox();
            this.sacuvajButton = new System.Windows.Forms.Button();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // testKonekcijaButton
            // 
            this.testKonekcijaButton.Location = new System.Drawing.Point(159, 189);
            this.testKonekcijaButton.Name = "testKonekcijaButton";
            this.testKonekcijaButton.Size = new System.Drawing.Size(108, 23);
            this.testKonekcijaButton.TabIndex = 5;
            this.testKonekcijaButton.Text = "Test konekcije";
            this.testKonekcijaButton.UseVisualStyleBackColor = true;
            this.testKonekcijaButton.Click += new System.EventHandler(this.testKonekcijaButton_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 39.13044F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 60.86956F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.SQLServerTextBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.SQLBazaTextBox, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.autentifikacijaComboBox, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.korisnikTextBox, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.lozinkaTextBox, 1, 4);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(12, 12);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 16.66667F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(368, 167);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "SQL Server";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "SQL Baza";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Autentifikacija";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 109);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "     Korisnik";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 143);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "     Lozinka";
            // 
            // SQLServerTextBox
            // 
            this.SQLServerTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.SQLServerTextBox.Location = new System.Drawing.Point(147, 6);
            this.SQLServerTextBox.Name = "SQLServerTextBox";
            this.SQLServerTextBox.Size = new System.Drawing.Size(218, 20);
            this.SQLServerTextBox.TabIndex = 6;
            // 
            // SQLBazaTextBox
            // 
            this.SQLBazaTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.SQLBazaTextBox.Location = new System.Drawing.Point(147, 39);
            this.SQLBazaTextBox.Name = "SQLBazaTextBox";
            this.SQLBazaTextBox.Size = new System.Drawing.Size(218, 20);
            this.SQLBazaTextBox.TabIndex = 7;
            // 
            // autentifikacijaComboBox
            // 
            this.autentifikacijaComboBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.autentifikacijaComboBox.FormattingEnabled = true;
            this.autentifikacijaComboBox.Items.AddRange(new object[] {
            "Windows",
            "SQL"});
            this.autentifikacijaComboBox.Location = new System.Drawing.Point(147, 72);
            this.autentifikacijaComboBox.Name = "autentifikacijaComboBox";
            this.autentifikacijaComboBox.Size = new System.Drawing.Size(218, 21);
            this.autentifikacijaComboBox.TabIndex = 8;
            this.autentifikacijaComboBox.SelectedIndexChanged += new System.EventHandler(this.autentifikacijaComboBox_SelectedIndexChanged);
            // 
            // korisnikTextBox
            // 
            this.korisnikTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.korisnikTextBox.Location = new System.Drawing.Point(147, 105);
            this.korisnikTextBox.Name = "korisnikTextBox";
            this.korisnikTextBox.Size = new System.Drawing.Size(218, 20);
            this.korisnikTextBox.TabIndex = 9;
            // 
            // lozinkaTextBox
            // 
            this.lozinkaTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lozinkaTextBox.Location = new System.Drawing.Point(147, 139);
            this.lozinkaTextBox.Name = "lozinkaTextBox";
            this.lozinkaTextBox.Size = new System.Drawing.Size(218, 20);
            this.lozinkaTextBox.TabIndex = 10;
            this.lozinkaTextBox.UseSystemPasswordChar = true;
            // 
            // sacuvajButton
            // 
            this.sacuvajButton.Location = new System.Drawing.Point(269, 189);
            this.sacuvajButton.Name = "sacuvajButton";
            this.sacuvajButton.Size = new System.Drawing.Size(108, 23);
            this.sacuvajButton.TabIndex = 11;
            this.sacuvajButton.Text = "Sačuvaj";
            this.sacuvajButton.UseVisualStyleBackColor = true;
            this.sacuvajButton.Click += new System.EventHandler(this.sacuvajButton_Click);
            // 
            // Konekcija
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(392, 220);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.testKonekcijaButton);
            this.Controls.Add(this.sacuvajButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Konekcija";
            this.Text = "Konekcija";
            this.Load += new System.EventHandler(this.Konekcija_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button testKonekcijaButton;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox SQLServerTextBox;
        private System.Windows.Forms.TextBox SQLBazaTextBox;
        private System.Windows.Forms.ComboBox autentifikacijaComboBox;
        private System.Windows.Forms.TextBox korisnikTextBox;
        private System.Windows.Forms.TextBox lozinkaTextBox;
        private System.Windows.Forms.Button sacuvajButton;
    }
}