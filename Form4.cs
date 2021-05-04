using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace Expiring_Users
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            sysinfo();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void sysinfo()
        {

            string cmd;
            cmd = "systeminfo";

            System.Diagnostics.Process iplookup = new System.Diagnostics.Process();
            iplookup.StartInfo.UseShellExecute = false;
            iplookup.StartInfo.RedirectStandardInput = true;
            iplookup.StartInfo.RedirectStandardOutput = true;
            iplookup.StartInfo.CreateNoWindow = true;
            iplookup.StartInfo.FileName = "cmd.exe";
            iplookup.Start();

            iplookup.StandardInput.WriteLine(cmd);
            iplookup.StandardInput.Flush();
            iplookup.StandardInput.Close();
            string output = iplookup.StandardOutput.ReadToEnd();
            iplookup.WaitForExit();
            textBox1.Text = "";

            using var reader = new StringReader(output);
            string sinfo;
            int counter = 0;
            while ((sinfo = reader.ReadLine()) != null)
            {
                if (sinfo.Contains("C:"))
                {
                    counter++;
                }
                else
                {
                    textBox1.AppendText(sinfo + "\r\n");
                }
            }
        }
    }
}
