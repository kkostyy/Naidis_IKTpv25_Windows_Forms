using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Naidis_IKTpv25_Windows_Forms
{
    public partial class AvaVorm : Form
    {
        TreeView tree;
        Button nupp;
        Label silt;
        PictureBox pilt;
        CheckBox mruut1, mruut2;
        RadioButton rnupp1, rnupp2;
        TextBox tbox;
        TabControl tabs;
        TabPage tab1, tab2, tab3;

        public AvaVorm()
        {
            Height = 600;
            Width = 1000;
            Text = "Naidis IKTpv25 Windows Forms";
            tree = new TreeView();
            tree.Dock = DockStyle.Left;
            tree.AfterSelect += Tree_AfterSelect;

            TreeNode tn = new TreeNode("Elemendid");
            tn.Nodes.Add(new TreeNode("Nupp"));
            tn.Nodes.Add(new TreeNode("Silt"));
            tn.Nodes.Add(new TreeNode("Pilt"));
            tn.Nodes.Add(new TreeNode("Märkeruut"));
            tn.Nodes.Add(new TreeNode("RadioNupp"));
            tn.Nodes.Add(new TreeNode("Tekstiväli"));
            tn.Nodes.Add(new TreeNode("Vahekaardid"));

            tree.Nodes.Add(tn);
            nupp = new Button();
            silt = new Label();
            pilt = new PictureBox();
            nupp.Text = "Vajutamind";
            nupp.Location = new Point(300, 100);
            nupp.Height = 50;
            nupp.Width = 100;
            nupp.Click += (sender, e) =>
            {
                MessageBox.Show("Nuppu vajutati");
            };

            silt.Text = "See on silt";
            silt.Location = new Point(300, 200);
            silt.Size = new Size(200, 30);
            silt.Font = new Font("Arial", 16, FontStyle.Bold);
            silt.AutoSize = true;
            silt.MouseLeave += Silt_MouseLeave;
            silt.MouseHover += Mouse_Hover;

            pilt.Location = new Point(400, 300);
            pilt.Image = Image.FromFile(@"C:\Users\opilane\source\repos\Naidis_IKTpv25_Windows_Forms\Naidis_IKTpv25_Windows_Forms\Pildid\литвин на кондиции.jpg");
            pilt.Size = new Size(100, 100);

            Controls.Add(tree);
        }

        private void Tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Text == "Nupp")
            {
                Controls.Add(nupp);
                tree.SelectedNode = null;
            }
            else if (e.Node.Text == "Silt")
            {
                Controls.Add(silt);
                tree.SelectedNode = null;
            }
            else if (e.Node.Text == "Pilt")
            {
                Controls.Add(pilt);
                tree.SelectedNode = null;
            }
            else if (e.Node.Text == "Märkeruut")
            {
                mruut1 = new CheckBox();
                mruut1.Text = "Tee väiksemaks";
                mruut1.Location = new Point(150, 150);
                mruut1.CheckedChanged += Mruut_CheckedChanged;
                mruut2 = new CheckBox();
                mruut2.Text = "Näita pilt";
                mruut2.Location = new Point(150, 200);
                mruut2.CheckedChanged += Mruut_CheckedChanged2;
                Controls.Add(mruut1);
                Controls.Add(mruut2);
            }
            else if (e.Node.Text == "RadioNupp")
            {
                rnupp1 = new RadioButton();
                rnupp1.Text = "Must";
                rnupp1.Location = new Point(200, 400);
                rnupp1.CheckedChanged += Rnupp_CheckedChanged;
                rnupp2 = new RadioButton();
                rnupp2.Text = "Sinine";
                rnupp2.Location = new Point(200, 450);
                rnupp2.CheckedChanged += Rnupp_CheckedChanged;
                Controls.Add(rnupp1);
                Controls.Add(rnupp2);
                tree.SelectedNode = null;
            }
            else if (e.Node.Text == "Tekstiväli")
            {
                tbox = new TextBox();
                tbox.Location = new Point(200, 500);
                tbox.Width = 200;
                tbox.TextChanged += (s, arg) =>
                {
                    Controls.Add(silt);
                    if (tbox.Text.Length > 0)
                    {
                        silt.Text = tbox.Text;
                    }
                    if (tbox.Text.Length == 0)
                    {
                        silt.Text = "See on pilt";
                    }
                };
                Controls.Add(tbox);
                tree.SelectedNode = null;
            }
            else if (e.Node.Text == "Vahekaardid")
            {
                tabs = new TabControl();
                tabs.Location = new Point(500, 100);
                tabs.Size = new Size(500, 500);
                tab1 = new TabPage("Tehno+TLN");
                WebBrowser brauser = new WebBrowser();
                brauser.Dock = DockStyle.Fill;
                brauser.ScriptErrorsSuppressed = true;
                brauser.Url = new Uri("https://techno.ee/");
                tab1.Controls.Add(brauser);
                //tab1.DoubleClick += AvaBrauser;

                tab2 = new TabPage("Пасьянс косынка");
                WebBrowser brauser2 = new WebBrowser();
                brauser2.Dock = DockStyle.Fill;
                brauser2.ScriptErrorsSuppressed = true;
                brauser2.Url = new Uri("https://razlozhi.ru/patience-sol");
                tab2.Controls.Add(brauser2);
                //tab2.DoubleClick += AvaBrauser;

                tab3 = new TabPage("+");
                tabs.SelectedIndexChanged += (s, arg) =>
                {
                    if (tabs.SelectedTab == tab3)
                    {
                        string uuskardinimi = Interaction.InputBox("Sisesta uue vahekaardi nimi: ", "Uus vahekaart", "");
                        if (string.IsNullOrWhiteSpace(uuskardinimi))
                        {
                            MessageBox.Show("Vahekaardi nimi ei tohi olla tühi");
                            tabs.SelectedTab = tab1;
                            return;
                        }
                        else
                        {
                            TabPage uusVahekaart = new TabPage(uuskardinimi);
                            string url = Interaction.InputBox("Sisestage url: ", "Uus vahekaart", "https://www.example.com");
                            if (string.IsNullOrWhiteSpace(url))
                            {
                                MessageBox.Show("url ei tohi olla tühi");
                                tabs.SelectedTab = tab1;
                                return;
                            }
                            else
                            {
                                WebBrowser browser = new WebBrowser();

                                browser.Dock = DockStyle.Fill;
                                browser.ScriptErrorsSuppressed = true;
                                browser.Url = new Uri(url);

                                uusVahekaart.Controls.Add(browser);
                                //uusVahekaart.DoubleClick += AvaBrauser;

                                tabs.TabPages.Insert(tabs.TabCount - 1, uusVahekaart);

                                tabs.SelectedTab = uusVahekaart;
                            }
                        }
                    }
                };

                tabs.TabPages.Add(tab1);
                tabs.TabPages.Add(tab2);
                tabs.TabPages.Add(tab3);
                tabs.MouseDoubleClick += tabs_MouseDoubleClick;

                Controls.Add(tabs);
            }
        }

        private void Rnupp_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton nupp = sender as RadioButton;
            if (nupp == rnupp1 && nupp.Checked)
            {
                BackColor = Color.Black;
                ForeColor = Color.White;
            }
            else
            {
                BackColor = Color.Blue;
                ForeColor = Color.Orange;
            }
        }

        private void Silt_MouseLeave(object sender, EventArgs e)
        {
            silt.BackColor = Color.Green;
        }

        private void Mouse_Hover(object sender, EventArgs e)
        {
            silt.BackColor = Color.GreenYellow;
        }
        private void Mruut_CheckedChanged(object sender, EventArgs e)
        {
            if (mruut1.Checked)
            {
                this.Size = new Size(300, 500);
                mruut1.Text = "Tee suuremaks";
            }
            else
            {
                Size = new Size(1000, 600);
                mruut1.Text = "Tee väiksemaks";
            }
        }
        private void Mruut_CheckedChanged2(object sender, EventArgs e)
        {
            if (mruut2.Checked)
            {
                pilt.Visible = true;
                mruut2.Text = "Näita pilt";
            }
            else
            {
                pilt.Visible = false;
                mruut2.Text = "Peida pilt";
            }
        }

        private void tabs_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Browser browser = new Browser();

            browser.Show();

            TabControl tabs = sender as TabControl;

            for (int i = 0; i < tabs.TabPages.Count; i++)
            {
                Rectangle rect = tabs.GetTabRect(i);

                if (rect.Contains(e.Location))
                {
                    TabPage vahekaart = tabs.TabPages[i];

                    browser.tabs.TabPages.Insert(browser.tabs.TabCount - 1, vahekaart);

                    break;
                }
            }
        }
    }
}