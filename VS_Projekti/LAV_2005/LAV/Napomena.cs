using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAV
{
    public partial class Napomena : Form
    {
        public Napomena()
        {
            InitializeComponent();

            napomenaRichTextBox.MaxLength = 200;
        }

        public Napomena(RichTextBox napomena)
            : this()
        {
            NapomenaRichTextBox = napomena;
            napomenaRichTextBox.Text = napomena.Text;
        }

        RichTextBox NapomenaRichTextBox;


        private void sacuvajButton_Click(object sender, EventArgs e)
        {
            this.Close();

            NapomenaRichTextBox.Text = napomenaRichTextBox.Text;
        }
    }
}