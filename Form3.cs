using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Globalization;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using System.Diagnostics;

namespace Expiring_Users
{
    public partial class Form3 : Form
    {

        private User userObject = new User();

        public ArrayList Users = new ArrayList();

        private ArrayList UsersList = new ArrayList();

        public DateTime today = DateTime.Now;
        public DateTime twoweeks = DateTime.Now.AddDays(14);

        public Form3()
        {
            InitializeComponent();
        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            this.Hide();
            Form1 f1 = new Form1();
            f1.ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button1.Visible = false;
            label1.Text = "Loading...";
            addusers();
            performcommand();
            

            //textBox1.Text = twoweeks.ToString();

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void addusers()
        {

            string cmd;
            cmd = "net user /domain";

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

            string userlist;

            using var reader = new StringReader(output);


                while ((userlist = reader.ReadLine()) != null)
                {
                    userlist = userlist.Replace("\n", "").Replace("-", "");
                    String[] u2 = userlist.Split("        ");

                    foreach(string a in u2)
                    {

                        string b = a;
                        b = b.Replace(" ", "").Replace("\r\n", "").Replace("\r", "");

                        if (!b.Equals("") && b != "\r\n" && b != " ")
                        {
                            try
                            {
                                Users.Add(b);
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("error");
                            }
                        }
                    }
                }
            }

        private void performcommand()
        {
            textBox1.Text = "";
            foreach(String user in Users)
            {

                int active = 1;

                try
                {
                    string cmd;
                    cmd = "net user /domain "+user;

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

                    using var reader = new StringReader(output);

                    String userlist2;


                    while ((userlist2 = reader.ReadLine()) != null)
                    {
                        userlist2 = userlist2.Replace("?", "");


                        if (userlist2.Contains("User name"))
                        {
                            userlist2 = (userlist2.Replace("Password expires             ", "").Replace("User name                    ", ""));
                            userObject.setUsername(userlist2);

                        }
                        else if (userlist2.Contains("Account active               No"))
                        {
                            active = 0;


                        }
                        else if ((userlist2.Contains("Password expires")) && (active == 1))
                        {
                            userlist2 = (userlist2.Replace("Password expires             ", "").Replace("User name                    ", ""));
                            userObject.setExpiration(userlist2);

                            DateTime userDate = DateTime.Parse(userObject.getExpiration());

                            if (userDate.CompareTo(twoweeks) <= 0)
                            {
                                UsersList.Add(userObject);
                                string uname = userObject.getUsername();
                                string exp = userObject.getExpiration();
                                textBox1.AppendText(uname + "\t" + exp + "\r\n\r\n");

                            }

                        }


                    }
                    
                }
                catch (Exception e)
                {

                }

            }
            label1.Text = "Complete";
            button1.Enabled = true;
            button1.Visible = true;
        }
            

    }
}
