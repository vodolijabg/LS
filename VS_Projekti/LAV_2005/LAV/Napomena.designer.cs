namespace LAV
{
    partial class Napomena
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
            this.napomenaRichTextBox = new System.Windows.Forms.RichTextBox();
            this.sacuvajButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // napomenaRichTextBox
            // 
            this.napomenaRichTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.napomenaRichTextBox.Location = new System.Drawing.Point(0, 0);
            this.napomenaRichTextBox.Name = "napomenaRichTextBox";
            this.napomenaRichTextBox.Size = new System.Drawing.Size(292, 178);
            this.napomenaRichTextBox.TabIndex = 0;
            this.napomenaRichTextBox.Text = "";
            // 
            // sacuvajButton
            // 
            this.sacuvajButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.sacuvajButton.BackgroundImage = global::LAV.Properties.Resources.Save;
            this.sacuvajButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.sacuvajButton.Location = new System.Drawing.Point(248, 184);
            this.sacuvajButton.Name = "sacuvajButton";
            this.sacuvajButton.Size = new System.Drawing.Size(32, 29);
            this.sacuvajButton.TabIndex = 1;
            this.sacuvajButton.UseVisualStyleBackColor = true;
            this.sacuvajButton.Click += new System.EventHandler(this.sacuvajButton_Click);
            // 
            // Napomena
            // 
            this.AcceptButton = this.sacuvajButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 225);
            this.Controls.Add(this.sacuvajButton);
            this.Controls.Add(this.napomenaRichTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Napomena";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Napomena";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox napomenaRichTextBox;
        private System.Windows.Forms.Button sacuvajButton;
    }
}