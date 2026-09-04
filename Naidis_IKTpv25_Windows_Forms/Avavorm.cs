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
    public partial class Avavorm : Form
    {
        TreeView tree;
        Button nupp;
        Label silt;
        PictureBox pilt;
        CheckBox mruut1, mruut2;
        public Avavorm()
        {
            Height = 600;
            Width = 1000;
            Text= "Naidis IKTpv25 Windows Forms";
            tree = new TreeView();
            tree.Dock = DockStyle.Left;
            tree.AfterSelect += Tree_AfterSelect;

            TreeNode tn=new TreeNode("Elemendid");
            tn.Nodes.Add(new TreeNode("Nupp"));
            tn.Nodes.Add(new TreeNode("Silt"));
            tn.Nodes.Add(new TreeNode("Pilt"));
            tn.Nodes.Add(new TreeNode("MärKeruut"));
            tree.Nodes.Add(tn);
            //nupp, silt ja pilt
            nupp = new Button();
            nupp.Text ="Vajuta mind";
            nupp.Location = new Point(200, 100);
            nupp.Height = 50;
            nupp.Width = 100;
            nupp.Click += (sender, e) => { MessageBox.Show("Nuppu vajutati!"); };

            silt = new Label();
            silt.Text = "See on silt";
            silt.Location = new Point(300, 200);
            silt.Size= new Size(200, 30);
            silt.Font= new Font("Arial", 16, FontStyle.Bold);
            silt.AutoSize = true;
            silt.MouseLeave += Silt_MouseLeave;
            silt.MouseHover += Silt_MouseHover;

            pilt = new PictureBox();
            pilt.Image = Image.FromFile(@"..\..\Pildid\AI_bot.png");
            pilt.Location = new Point(300, 300);
            pilt.Size = new Size(200, 200);
            pilt.SizeMode = PictureBoxSizeMode.StretchImage;
            pilt.DoubleClick += Pilt_DoubleClick;

            Controls.Add(tree);
        }

        private void Pilt_DoubleClick(object sender, EventArgs e)
        {
            Size väike=new Size(200, 200);
            Size suur=new Size(400, 400);
            if (pilt.Size == suur)
                pilt.Size = väike;
            else
                pilt.Size = suur;
        }

        private void Silt_MouseHover(object sender, EventArgs e)
        {
            silt.BackColor = Color.LightGray;
            silt.BorderStyle = BorderStyle.Fixed3D;
        }

        private void Silt_MouseLeave(object sender, EventArgs e)
        {
            silt.BorderStyle = BorderStyle.None;
            silt.BackColor = Color.Gray;
        
        }

        private void Tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(e.Node.Text == "Nupp")
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
                    mruut1.Text = "Märkeruut 1";
                    mruut1.Location = new Point(300, 400);
                    mruut1.CheckedChanged += Mruut_CheckedChanged;
                    mruut2 = new CheckBox();
                    mruut2.Text = "Märkeruut 2";
                    mruut2.Location = new Point(300, 450);
                    mruut2.CheckedChanged += Mruut_CheckedChanged;
                    Controls.Add(mruut1);
                    Controls.Add(mruut2);
                    tree.SelectedNode = null;
            }
        }
        private void Mruut_CheckedChanged(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
