using System;
using System.IO;
using System.Windows.Forms;

namespace FDisassoc
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            string AgreePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "/Temp/AceptedFDGA.dat";
            if (File.Exists(AgreePath))
            {
                using (StreamReader sW = new StreamReader(AgreePath))
                {
                    string txt = sW.ReadLine();
                    if (txt == "1")
                    {
                        Application.Run(new Form1());
                    }
                    else
                    {
                        Application.Run(new Warning());
                    }
                }
            }
            else
            {
                Application.Run(new Warning());
            }
        }
    }
}