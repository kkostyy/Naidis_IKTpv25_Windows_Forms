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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Naidis_IKTpv25_WinForms_Berezevski
{
    public partial class Browser : Form
    {
        public TabControl tabs = new TabControl();
        TabPage newTab;
        public Browser()
        {
            newTab = new TabPage("+");
            //Height = 1000;
            //Width = 1000;
            //tabs.Size = new Size(1000, 1000);
            tabs.Dock = DockStyle.Fill;

            tabs.SelectedIndexChanged += (s, arg) =>
            {
                if (tabs.SelectedTab == newTab)
                {
                    string uuskardinimi = Interaction.InputBox("Sisesta uue vahekaardi nimi: ", "Uus vahekaart", "");
                    if (string.IsNullOrWhiteSpace(uuskardinimi))
                    {
                        MessageBox.Show("Vahekaardi nimi ei tohi olla tühi");
                        this.Close();
                        return;
                    }
                    else
                    {
                        TabPage uusVahekaart = new TabPage(uuskardinimi);
                        string url = Interaction.InputBox("Sisestage url: ", "Uus vahekaart", "https://www.example.com");
                        if (string.IsNullOrWhiteSpace(url))
                        {
                            MessageBox.Show("url ei tohi olla tühi");
                            Close();
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

            tabs.TabPages.Add(newTab);
            Controls.Add(tabs);


            InitializeComponent();
        }
    }
}
