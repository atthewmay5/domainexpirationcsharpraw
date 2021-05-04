using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Management.Automation;
using System.Text;
using System.Windows.Forms;

namespace Expiring_Users
{
    public partial class Lookup : Form
    {
        public Lookup()
        {
            InitializeComponent();
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
        private void button1_Click(object sender, EventArgs e)
        {
            String ipaddress;
            ipaddress = textBox1.Text;

            string cmd;
            cmd = "nslookup "+ipaddress;

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
            textBox2.Text = "";

            using var reader = new StringReader(output);
            string iplist;
            int counter = 0;
            while ((iplist = reader.ReadLine()) != null)
            {
                if (counter == 0 || counter == 1 || counter == 2 || counter == 3 || iplist.Contains("C:"))
                {
                    counter++;
                }
                else
                {
                    textBox2.AppendText(iplist+"\r\n");
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
