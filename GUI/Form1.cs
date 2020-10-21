/**
 * File Disassociator GUI coded by Lumito
 * (C) 2020, Lumito. www.lumito.net
 * Project licensed under the MIT license
 * Hosted by GitHub (github.com/LumitoLuma)
 */

using Microsoft.Win32;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace FDisassoc
{
    public partial class Form1 : Form
    {
        bool ee;

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            RegistryKey rgKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\");
            foreach (var value in rgKey.GetSubKeyNames())
            {
                if (value.Contains("."))
                {
                    comboBox1.Items.Add(value.ToLowerInvariant());
                }
            }
            comboBox1.Items.RemoveAt(1); // Removes "." path from the list
            ee = false;
        }

        void DelExt()
        {
            string ext = comboBox1.Text;
            RegistryKey uKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\", true);
            RegistryKey u2Key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Explorer\FileExts\" + ext, true);
            foreach (var k in u2Key.GetSubKeyNames())
            {
                u2Key.DeleteSubKey(k);
            }
            uKey.DeleteSubKey(ext);
            RegistryKey cKey = Registry.ClassesRoot.OpenSubKey("", true);
            RegistryKey c2Key = Registry.ClassesRoot.OpenSubKey(ext, true);
            foreach (var k2 in c2Key.GetSubKeyNames())
            {
                c2Key.DeleteSubKey(k2);
            }

            foreach (var val in cKey.GetSubKeyNames())
            {
                if (val.Equals(ext))
                {
                    cKey.DeleteSubKey(val);
                    break;
                }
            }
        }

        private void RestartExplorer()
        {
            foreach (var process in System.Diagnostics.Process.GetProcessesByName("explorer")) // Thanks ConsultUtah!
            {
                process.Kill();
            }
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(asm.Location);
            var lnkTime = File.GetLastWriteTime(Assembly.GetExecutingAssembly().Location).ToUniversalTime();
            MessageBox.Show("File Disassociator " + fvi.FileVersion.Replace(".0.0", "") + " coded by Lumito\n" +
                "Compiled on " + lnkTime + "\n\n" +
                "Project's website: https://github.com/LumitoLuma/FileDisassociator\n" +
                "Project licensed under the MIT license.\n\n" +
                "Special thanks to this people, that helped me with this project:\n" +
                " - ConsultUtah\n" +
                " - nihique\n\n" +
                "Visit my website: https://www.lumito.net", "About File Disassociator");
            if (ee)
            {
                MessageBox.Show("And thanks a lot for using my software ;)", "About File Disassociator");
            }
        }

        private void PictureBox2_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void PictureBox3_Click(object sender, EventArgs e)
        {
            string ext = comboBox1.Text;
            bool error = false;
            if (ext.Equals("Select an extension..."))
            {
                MessageBox.Show("You have not selected an extension.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                error = true;
            }
            else
            {
                DialogResult result = MessageBox.Show("Are you sure that you want to disassociate " + ext + " extension?\nThis can be VERY HARMFUL to your computer and make it unusable.", "Please confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        DelExt();
                    }
                    catch (Exception)
                    {
                        // Do nothing
                    }

                    /* This second time ensures that the extension is successfully removed */
                    try
                    {
                        DelExt();
                    }
                    catch (Exception)
                    {
                        // Do nothing again
                    }
                    comboBox1.Items.Remove(comboBox1.SelectedItem);
                    comboBox1.SelectedIndex = 0;
                }
                else
                {
                    error = true;
                }
            }

            if (checkBox1.Checked && !error)
            {
                RestartExplorer();
                MessageBox.Show("Successfully unassociated " + ext + " extension!\nIf you still see the extension working, try to restart Windows.", "File disassociator", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (!error)
            {
                MessageBox.Show("Successfully unassociated " + ext + " extension!\n" +
                    "Please restart explorer to see changes.", "File disassociator", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            RestartExplorer();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Alt && e.Shift)
            {
                ee = true;
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            ee = false;
        }
    }
}