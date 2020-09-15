/**
 * File Disassociator GUI coded by Lumito
 * (C) 2020, Lumito. www.lumito.net
 * Project licensed under the MIT license
 * Hosted by GitHub (github.com/LumitoLuma)
 */

using System.IO;
using System.Windows.Forms;

namespace FDisassoc
{
    public partial class Warning : Form
    {
        int seconds = 60;

        public Warning()
        {
            InitializeComponent();
            richTextBox1.SelectedRtf = License.Agreement;
        }

        private void RichTextBox1_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(e.LinkText);
        }

        private void Warning_Load(object sender, System.EventArgs e)
        {
            timer1.Start();
        }

        private void Timer1_Tick(object sender, System.EventArgs e)
        {
            if (seconds > 1)
            {
                seconds--;
                label2.Text = "Please wait " + seconds + " seconds...";
            }
            else
            {
                seconds = 0;
                label2.Visible = false;
                button1.Enabled = true;
                timer1.Dispose();
            }
        }

        private void Button1_Click(object sender, System.EventArgs e)
        {
            File.WriteAllText(System.Environment.GetFolderPath(System.Environment.SpecialFolder.LocalApplicationData) + "/Temp/AceptedFDGA.dat", "1");

            /* Thanks nihique! */
            Hide();
            Form1 form1 = new Form1();
            form1.Closed += (s, args) => this.Close();
            form1.Show();
        }

        private void Button2_Click(object sender, System.EventArgs e)
        {
            System.Environment.Exit(1);
        }
    }
}