using System;
using System.Drawing;
using System.Windows.Forms;

namespace Naidis_IKTpv25_Windows_Forms
{
    /// <summary>
    /// Pildi vaatamise programm ("Picture Viewer").
    /// Kõik juhtelemendid luuakse koodis konstruktoris - Toolbox'i ei kasutata.
    /// </summary>
    public class PildiVaatajaForm : Form
    {
        private readonly PictureBox pictureBox;
        private readonly CheckBox sketchCheckBox;
        private readonly Button showPictureButton;
        private readonly Button setBackColorButton;
        private readonly Button clearPictureButton;
        private readonly Button closeButton;
        private readonly Panel bottomPanel;

        // Hoiame originaalpilti alles, et "Sketch" märkeruudu lahti tehes saaks värvipildi tagasi
        private Image originaalPilt;

        public PildiVaatajaForm()
        {
            Text = "Picture Viewer";
            Width = 420;
            Height = 380;
            MinimumSize = new Size(320, 260);
            StartPosition = FormStartPosition.CenterParent;

            // Pildi kuvamise ala täidab enamuse aknast
            pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                SizeMode = PictureBoxSizeMode.Zoom
            };

            // Alumine riba märkeruudu ja nuppude jaoks
            bottomPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 42
            };

            sketchCheckBox = new CheckBox
            {
                Text = "Sketch",
                Location = new Point(8, 12),
                AutoSize = true
            };
            sketchCheckBox.CheckedChanged += SketchCheckBox_CheckedChanged;

            showPictureButton = new Button { Text = "Show a picture", AutoSize = true, Height = 26 };
            setBackColorButton = new Button { Text = "Set the background color", AutoSize = true, Height = 26 };
            clearPictureButton = new Button { Text = "Clear the picture", AutoSize = true, Height = 26 };
            closeButton = new Button { Text = "Close", AutoSize = true, Height = 26 };

            showPictureButton.Click += ShowPictureButton_Click;
            setBackColorButton.Click += SetBackColorButton_Click;
            clearPictureButton.Click += ClearPictureButton_Click;
            closeButton.Click += (s, e) => Close();

            bottomPanel.Controls.Add(sketchCheckBox);
            bottomPanel.Controls.Add(showPictureButton);
            bottomPanel.Controls.Add(setBackColorButton);
            bottomPanel.Controls.Add(clearPictureButton);
            bottomPanel.Controls.Add(closeButton);

            // PictureBox lisatakse Fill-ina esimesena, siis Dock=Bottom paneel jääb alumisse serva
            Controls.Add(pictureBox);
            Controls.Add(bottomPanel);

            Resize += (s, e) => PaigutaNupud();
            PaigutaNupud();
        }

        /// <summary>
        /// Paigutab nupud alumise paneeli paremasse serva nii, et aken oleks vabalt suurust muudetav.
        /// </summary>
        private void PaigutaNupud()
        {
            int right = bottomPanel.ClientSize.Width - 8;
            const int y = 8;

            closeButton.Location = new Point(right - closeButton.Width, y);
            right -= closeButton.Width + 6;

            clearPictureButton.Location = new Point(right - clearPictureButton.Width, y);
            right -= clearPictureButton.Width + 6;

            setBackColorButton.Location = new Point(right - setBackColorButton.Width, y);
            right -= setBackColorButton.Width + 6;

            showPictureButton.Location = new Point(right - showPictureButton.Width, y);
        }

        private void ShowPictureButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog())
            {
                dialog.Title = "Vali pilt";
                dialog.Filter = "Pildifailid|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Kõik failid|*.*";

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    originaalPilt?.Dispose();
                    originaalPilt = Image.FromFile(dialog.FileName);

                    sketchCheckBox.Checked = false;
                    pictureBox.Image = originaalPilt;
                }
            }
        }

        private void SetBackColorButton_Click(object sender, EventArgs e)
        {
            using (var dialog = new ColorDialog())
            {
                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    pictureBox.BackColor = dialog.Color;
                }
            }
        }

        private void ClearPictureButton_Click(object sender, EventArgs e)
        {
            pictureBox.Image = null;
            originaalPilt?.Dispose();
            originaalPilt = null;
            sketchCheckBox.Checked = false;
        }

        /// <summary>
        /// "Sketch" märkeruut näitab pilti mustvalgena (lihtne visandi-efekt).
        /// </summary>
        private void SketchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (originaalPilt == null)
            {
                sketchCheckBox.Checked = false;
                return;
            }

            pictureBox.Image = sketchCheckBox.Checked
                ? PildiTootlus.MuudaHalltoonideks((Bitmap)originaalPilt)
                : originaalPilt;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                originaalPilt?.Dispose();
            }
            base.Dispose(disposing);
        }
    }

    /// <summary>
    /// Abiklass pildi töötlemiseks. Eraldi klass hoiab vormi koodi puhtana
    /// (OOP põhimõte: single responsibility - iga klass teeb üht asja).
    /// </summary>
    public static class PildiTootlus
    {
        public static Bitmap MuudaHalltoonideks(Bitmap lahtepilt)
        {
            Bitmap tulemus = new Bitmap(lahtepilt.Width, lahtepilt.Height);

            for (int x = 0; x < lahtepilt.Width; x++)
            {
                for (int y = 0; y < lahtepilt.Height; y++)
                {
                    Color piksel = lahtepilt.GetPixel(x, y);
                    int hall = (int)(piksel.R * 0.3 + piksel.G * 0.59 + piksel.B * 0.11);
                    tulemus.SetPixel(x, y, Color.FromArgb(piksel.A, hall, hall, hall));
                }
            }

            return tulemus;
        }
    }
}
