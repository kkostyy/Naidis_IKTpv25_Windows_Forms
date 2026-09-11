using System;
using System.Drawing;
using System.Windows.Forms;

namespace Naidis_IKTpv25_Windows_Forms
{
    /// <summary>
    /// Üks matemaatiline ülesanne (nt "26 + 34 = ?").
    /// Eraldi klass vastutab ülesande genereerimise ja kontrollimise eest
    /// (OOP põhimõte: encapsulation - andmed ja loogika on koos).
    /// </summary>
    public class MatemaatikaUlesanne
    {
        private static readonly Random rnd = new Random();

        public int Arv1 { get; private set; }
        public int Arv2 { get; private set; }
        public char Tehe { get; }
        public int OigeVastus { get; private set; }

        public MatemaatikaUlesanne(char tehe)
        {
            Tehe = tehe;
            GenereeriUuesti();
        }

        public void GenereeriUuesti()
        {
            switch (Tehe)
            {
                case '+':
                    Arv1 = rnd.Next(10, 51);
                    Arv2 = rnd.Next(10, 51);
                    OigeVastus = Arv1 + Arv2;
                    break;

                case '-':
                    Arv1 = rnd.Next(20, 91);
                    Arv2 = rnd.Next(1, Arv1);       // tagab, et tulemus ei lähe negatiivseks
                    OigeVastus = Arv1 - Arv2;
                    break;

                case '×':
                    Arv1 = rnd.Next(2, 10);
                    Arv2 = rnd.Next(2, 10);
                    OigeVastus = Arv1 * Arv2;
                    break;

                case '÷':
                    Arv2 = rnd.Next(2, 10);          // jagaja
                    OigeVastus = rnd.Next(2, 10);     // jagatis
                    Arv1 = Arv2 * OigeVastus;         // jagatav valitakse nii, et jaguks täpselt
                    break;

                default:
                    throw new ArgumentException("Tundmatu tehe: " + Tehe);
            }
        }

        public bool KontrolliVastust(int vastus) => vastus == OigeVastus;
    }

    /// <summary>
    /// Matemaatiline äraarvamismäng ("Math Quiz").
    /// Kõik juhtelemendid luuakse koodis - Toolbox'i ei kasutata.
    /// </summary>
    public class MathQuizForm : Form
    {
        private const int AlgusAeg = 30; // sekundites

        private readonly Label timeLeftCaption;
        private readonly TextBox timeLeftBox;
        private readonly Button startButton;
        private readonly Timer countdownTimer;

        private readonly MatemaatikaUlesanne[] ulesanded;
        private readonly Label[] arv1Sildid;
        private readonly Label[] arv2Sildid;
        private readonly NumericUpDown[] vastusValjad;

        private int allesJaanudAeg;

        public MathQuizForm()
        {
            Text = "Math Quiz";
            Width = 340;
            Height = 340;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            StartPosition = FormStartPosition.CenterParent;

            timeLeftCaption = new Label
            {
                Text = "Time Left",
                Location = new Point(60, 20),
                AutoSize = true,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            };

            timeLeftBox = new TextBox
            {
                Location = new Point(140, 17),
                Width = 120,
                ReadOnly = true,
                TextAlign = HorizontalAlignment.Center
            };

            char[] tehted = { '+', '-', '×', '÷' };
            int n = tehted.Length;

            ulesanded = new MatemaatikaUlesanne[n];
            arv1Sildid = new Label[n];
            arv2Sildid = new Label[n];
            vastusValjad = new NumericUpDown[n];

            int y = 65;
            for (int i = 0; i < n; i++)
            {
                ulesanded[i] = new MatemaatikaUlesanne(tehted[i]);

                arv1Sildid[i] = new Label { Location = new Point(35, y), AutoSize = true, Font = new Font("Segoe UI", 11) };
                Label teheSilt = new Label { Text = tehted[i].ToString(), Location = new Point(90, y), AutoSize = true, Font = new Font("Segoe UI", 11) };
                arv2Sildid[i] = new Label { Location = new Point(120, y), AutoSize = true, Font = new Font("Segoe UI", 11) };
                Label vordusSilt = new Label { Text = "=", Location = new Point(170, y), AutoSize = true, Font = new Font("Segoe UI", 11) };

                vastusValjad[i] = new NumericUpDown
                {
                    Location = new Point(195, y - 3),
                    Width = 70,
                    Minimum = -1000,
                    Maximum = 1000,
                    Enabled = false
                };

                Controls.Add(arv1Sildid[i]);
                Controls.Add(teheSilt);
                Controls.Add(arv2Sildid[i]);
                Controls.Add(vordusSilt);
                Controls.Add(vastusValjad[i]);

                y += 40;
            }

            startButton = new Button
            {
                Text = "Start the quiz",
                Location = new Point(100, y + 15),
                Width = 130,
                Height = 30
            };
            startButton.Click += StartButton_Click;

            countdownTimer = new Timer { Interval = 1000 };
            countdownTimer.Tick += CountdownTimer_Tick;

            Controls.Add(timeLeftCaption);
            Controls.Add(timeLeftBox);
            Controls.Add(startButton);

            NaitaUlesandeid();
        }

        private void NaitaUlesandeid()
        {
            for (int i = 0; i < ulesanded.Length; i++)
            {
                arv1Sildid[i].Text = ulesanded[i].Arv1.ToString();
                arv2Sildid[i].Text = ulesanded[i].Arv2.ToString();
                vastusValjad[i].Value = 0;
            }
            timeLeftBox.Text = AlgusAeg + " seconds";
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            foreach (var ulesanne in ulesanded)
            {
                ulesanne.GenereeriUuesti();
            }
            NaitaUlesandeid();

            allesJaanudAeg = AlgusAeg;
            timeLeftBox.Text = allesJaanudAeg + " seconds";

            foreach (var valjund in vastusValjad)
            {
                valjund.Enabled = true;
            }
            startButton.Enabled = false;

            countdownTimer.Start();
        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            allesJaanudAeg--;
            timeLeftBox.Text = allesJaanudAeg + " seconds";

            if (allesJaanudAeg <= 0)
            {
                countdownTimer.Stop();
                LopetaViktoriin();
            }
        }

        private void LopetaViktoriin()
        {
            int oigeidVastuseid = 0;

            for (int i = 0; i < ulesanded.Length; i++)
            {
                vastusValjad[i].Enabled = false;
                if (ulesanded[i].KontrolliVastust((int)vastusValjad[i].Value))
                {
                    oigeidVastuseid++;
                }
            }

            startButton.Enabled = true;

            MessageBox.Show(
                $"Aeg sai otsa!\nÕigeid vastuseid: {oigeidVastuseid} / {ulesanded.Length}",
                "Tulemus",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
    }
}
