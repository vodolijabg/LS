using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAV
{
    public partial class DodatniPodaci : Form
    {
        DataGridViewRow Red = null;
        DB.Artikal DBArtikal = new DB.Artikal();

        public DodatniPodaci()
        {
            InitializeComponent();
            Inicijalizuj();
        }

        void Inicijalizuj()
        {
            cenaUgradnjeTextBox.MaxLength = 19;
            normaSatiTextBox.MaxLength = 6;
            napomenaRichTextBox.MaxLength = 200;

        }

        public DodatniPodaci(DataGridViewRow red): this()
        {
            cenaUgradnjeTextBox.Text = red.Cells["Cena ugradnje"].Value.ToString();
            normaSatiTextBox.Text = red.Cells["Norma sati"].Value.ToString();
            napomenaRichTextBox.Text = red.Cells["Napomena"].Value.ToString();

            Red = red;
        }

        private void potvrdiButton_Click(object sender, EventArgs e)
        {
            //koristi se kod provere da li je vrednost polja CenaBezPoreza decimal (TryParse)
            Decimal _d;

            dodatniPodaciErrorProvider.Clear();

            #region Provera obaveznih polja

            if ((normaSatiTextBox.Text == "")&&(cenaUgradnjeTextBox.Text != ""))
            {
                dodatniPodaciErrorProvider.SetError(normaSatiTextBox, "Ili unesi i Cenu ugradnje i Norma sati ili ni jedan.");
                normaSatiTextBox.Select();
            }
            else if ((normaSatiTextBox.Text != "")&&(cenaUgradnjeTextBox.Text == ""))
            {
                dodatniPodaciErrorProvider.SetError(cenaUgradnjeTextBox, "Ili unesi i Cenu ugradnje i Norma sati ili ni jedan.");
                cenaUgradnjeTextBox.Select();
            }
            #endregion

            #region ProveraTipaPodataka

            //provera da li poslata vrednost decimal
            else if ((cenaUgradnjeTextBox.Text!="")&&(!Decimal.TryParse(cenaUgradnjeTextBox.Text, out _d)))
            {
                dodatniPodaciErrorProvider.SetError(cenaUgradnjeTextBox, "Vrednost uneta u polje Cena ugradnje mora biti broj.");
                cenaUgradnjeTextBox.Select();
            }
            //provera da li poslata vrednost ima vise od 16 cifara i dve decimale 
            else if ((cenaUgradnjeTextBox.Text != "") && (decimal.Parse(cenaUgradnjeTextBox.Text) > decimal.Parse("9999999999999999,99")))
            {
                dodatniPodaciErrorProvider.SetError(cenaUgradnjeTextBox, "Vrednost uneta u polje Cena dela može imati najviše 16 cifara i dve decimale.");
                cenaUgradnjeTextBox.Select();
            }
            else if ((normaSatiTextBox.Text != "") && (!Decimal.TryParse(normaSatiTextBox.Text, out _d)))
            {
                dodatniPodaciErrorProvider.SetError(normaSatiTextBox, "Vrednost uneta u polje Norma sati mora biti broj.");
                normaSatiTextBox.Select();
            }
            //provera da li poslata vrednost ima vise od 16 cifara i dve decimale 
            else if ((normaSatiTextBox.Text != "") && (decimal.Parse(normaSatiTextBox.Text) > decimal.Parse("999,99")))
            {
                dodatniPodaciErrorProvider.SetError(normaSatiTextBox, "Vrednost uneta u polje Norma sati može imati najviše 3 cifre i dve decimale.");
                normaSatiTextBox.Select();
            }

            

            #endregion

            #region Provera ChkConnstraintia

            //provera da li je poslata vrednost pozitivan broj 
            else if ((cenaUgradnjeTextBox.Text != "") && (decimal.Parse(cenaUgradnjeTextBox.Text) < 0))
            {
                dodatniPodaciErrorProvider.SetError(cenaUgradnjeTextBox, "Vrednost uneta u polje Cena ugradnje ne može biti negativan broj.");
                cenaUgradnjeTextBox.Select();
            }
            //provera da li je poslata vrednost pozitivan broj 
            else if ((normaSatiTextBox.Text != "") && (decimal.Parse(normaSatiTextBox.Text) < 0))
            {
                dodatniPodaciErrorProvider.SetError(normaSatiTextBox, "Vrednost uneta u polje Norma sati ne može biti negativan broj.");
                cenaUgradnjeTextBox.Select();
            }
           

            #endregion

            //ako su podaci na formi ispravno uneti
            else
            {
                try
                {
                    //ako je sve puno
                    if ((cenaUgradnjeTextBox.Text != "")&&(normaSatiTextBox.Text != "")&&(napomenaRichTextBox.Text != ""))
                    {
                        DBArtikal.ObradiDodatnePodatke(Int32.Parse(Red.Cells["Artikal_ID"].Value.ToString()),decimal.Parse(cenaUgradnjeTextBox.Text), decimal.Parse(normaSatiTextBox.Text), napomenaRichTextBox.Text);
                    }
                    else if ((cenaUgradnjeTextBox.Text != "") && (normaSatiTextBox.Text != "") && (napomenaRichTextBox.Text == ""))
                    {
                        DBArtikal.ObradiDodatnePodatke(Int32.Parse(Red.Cells["Artikal_ID"].Value.ToString()), decimal.Parse(cenaUgradnjeTextBox.Text), decimal.Parse(normaSatiTextBox.Text));
                    }
                    else if ((cenaUgradnjeTextBox.Text == "") && (normaSatiTextBox.Text == "") && (napomenaRichTextBox.Text != ""))
                    {
                        DBArtikal.ObradiDodatnePodatke(Int32.Parse(Red.Cells["Artikal_ID"].Value.ToString()), napomenaRichTextBox.Text);
                    }
                    else if ((cenaUgradnjeTextBox.Text == "") && (normaSatiTextBox.Text == "") && (napomenaRichTextBox.Text == ""))
                    {
                        DBArtikal.ObradiDodatnePodatke(Int32.Parse(Red.Cells["Artikal_ID"].Value.ToString()));
                    }

                    Red.Cells["Cena ugradnje"].Value = cenaUgradnjeTextBox.Text;
                    Red.Cells["Norma sati"].Value = normaSatiTextBox.Text;
                    Red.Cells["Napomena"].Value = napomenaRichTextBox.Text;

                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Zapiši sledeću poruku i obavesti programera: \n\n" + ex.Message, "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}