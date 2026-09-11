using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Naidis_IKTpv25_Windows_Forms
{
    /// <summary>
    /// Üks mängukaart - seob kokku peidetud sümboli ja seda kuvava nupu.
    /// Eraldi klass, mis hoiab nupu ja tema oleku koos (OOP: encapsulation).
    /// </summary>
    public class Mangukaart
    {
        public char Sumbol { get; }
        public Button Nupp { get; }
        public bool OnPaaritud { get; set; }

        public Mangukaart(char sumbol, Button nupp)
        {
            Sumbol = sumbol;
            Nupp = nupp;
            OnPaaritud = false;
        }

        public void NaitaSumbolit() => Nupp.Text = Sumbol.ToString();

        public void Peida()
        {
            if (!OnPaaritud)
            {
                Nupp.Text = string.Empty;
            }
        }
    }

    /// <summary>
    /// Sarnaste piltide (sümbolite) leidmise mäng ("Matching Game").
    /// 4x4 ruudustik, kokku 8 sümbolipaari. Kõik juhtelemendid luuakse koodis.
    /// </summary>
    public class MatchingGameForm : Form
    {
        private const int Read = 4;
        private const int Veerud = 4;
        private const int RuuduSuurus = 75;

        // Wingdings fondi tähemärgid kuvatakse väikeste ikoonidena (lind, jalgratas, silm jne)
        private static readonly char[] Sumbolid = { 'J', ')', '&', '2', '~', '¡', 'Q', 'S' };
        private static readonly Random rnd = new Random();

        private readonly List<Mangukaart> kaardid = new List<Mangukaart>();
        private readonly Timer peitmiseTimer;

        private Mangukaart esimeneValik;
        private Mangukaart teineValik;
        private bool ootabPeitmist;
        private int leitudPaare;

        public MatchingGameForm()
        {
            Text = "Matching Game";
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterParent;
            ClientSize = new Size(Veerud * RuuduSuurus, Read * RuuduSuurus);

            peitmiseTimer = new Timer { Interval = 700 };
            peitmiseTimer.Tick += PeitmiseTimer_Tick;

            LooMangulaud();
        }

        private void LooMangulaud()
        {
            // Iga sümbol lisatakse kaks korda, siis kaardipakk segatakse läbi
            List<char> paketiSumbolid = new List<char>();
            foreach (char sumbol in Sumbolid)
            {
                paketiSumbolid.Add(sumbol);
                paketiSumbolid.Add(sumbol);
            }
            Segamine(paketiSumbolid);

            int i = 0;
            for (int rida = 0; rida < Read; rida++)
            {
                for (int veerg = 0; veerg < Veerud; veerg++)
                {
                    Button nupp = new Button
                    {
                        Size = new Size(RuuduSuurus, RuuduSuurus),
                        Location = new Point(veerg * RuuduSuurus, rida * RuuduSuurus),
                        BackColor = Color.CornflowerBlue,
                        FlatStyle = FlatStyle.Flat,
                        Font = new Font("Wingdings", 20),
                        Text = string.Empty,
                        TabStop = false
                    };
                    nupp.FlatAppearance.BorderColor = Color.White;

                    Mangukaart kaart = new Mangukaart(paketiSumbolid[i], nupp);
                    nupp.Click += (s, e) => Kaart_Click(kaart);

                    kaardid.Add(kaart);
                    Controls.Add(nupp);
                    i++;
                }
            }
        }

        private static void Segamine(List<char> nimekiri)
        {
            // Fisher-Yates segamisalgoritm
            for (int n = nimekiri.Count - 1; n > 0; n--)
            {
                int k = rnd.Next(n + 1);
                (nimekiri[n], nimekiri[k]) = (nimekiri[k], nimekiri[n]);
            }
        }

        private void Kaart_Click(Mangukaart kaart)
        {
            if (ootabPeitmist || kaart.OnPaaritud || kaart == esimeneValik)
            {
                return;
            }

            kaart.NaitaSumbolit();

            if (esimeneValik == null)
            {
                esimeneValik = kaart;
                return;
            }

            if (esimeneValik.Sumbol == kaart.Sumbol)
            {
                // Paar leitud
                esimeneValik.OnPaaritud = true;
                kaart.OnPaaritud = true;
                esimeneValik.Nupp.Enabled = false;
                kaart.Nupp.Enabled = false;
                esimeneValik.Nupp.BackColor = Color.LightGreen;
                kaart.Nupp.BackColor = Color.LightGreen;

                esimeneValik = null;
                leitudPaare++;

                if (leitudPaare == Sumbolid.Length)
                {
                    MessageBox.Show("Palju õnne, leidsid kõik paarid!", "Mäng läbi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                // Vale paar - peidame mõlemad kaardid väikese viivitusega, et kasutaja jõuaks näha
                teineValik = kaart;
                ootabPeitmist = true;
                peitmiseTimer.Start();
            }
        }

        private void PeitmiseTimer_Tick(object sender, EventArgs e)
        {
            peitmiseTimer.Stop();

            esimeneValik?.Peida();
            teineValik?.Peida();

            esimeneValik = null;
            teineValik = null;
            ootabPeitmist = false;
        }
    }
}
