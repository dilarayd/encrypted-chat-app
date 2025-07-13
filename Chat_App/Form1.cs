using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chat_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static class Encryption
        {
            private static readonly string key = "12345678901234567890123456789012"; 
            private static readonly string iv = "1234567890123456"; 

            public static string Encrypt(string plainText)
            {
                using (Aes aes = Aes.Create())
                {
                    aes.Key = Encoding.UTF8.GetBytes(key);
                    aes.IV = Encoding.UTF8.GetBytes(iv);

                    ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                        {
                            using (StreamWriter sw = new StreamWriter(cs))
                            {
                                sw.Write(plainText);
                            }
                            return Convert.ToBase64String(ms.ToArray());
                        }
                    }
                }
            }
        }

            private void button1_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            color = listView1.Items.Count;
            message_id = listView1.Items.Count;

            string encryptedMessage = Encryption.Encrypt(textBox1.Text);

            string[] addRow = { message_id.ToString(), now.ToString(), textBox1.Text };
            ListViewItem row = new ListViewItem(addRow);
            listView1.Items.Add(row);
            listView1.Items[color].BackColor = Color.IndianRed;
      

            string[] addRow2 = { message_id.ToString(), now.ToString(), textBox1.Text };
            ListViewItem row2 = new ListViewItem(addRow2);
            form2x.listView1.Items.Add(row2);
            form2x.listView1.Items[color].BackColor = Color.LightGreen;

            string[] addRow3 = { message_id.ToString(), now.ToString(), encryptedMessage };
            ListViewItem row3 = new ListViewItem(addRow3);
            form3x.listView1.Items.Add(row3);
            form3x.listView1.Items[color].BackColor = Color.LightBlue;


            textBox1.Text = "";
            button1.Enabled = false;
            timer1.Start();
        }

        public Form2 form2x = new Form2();
        public Form3 form3x = new Form3();
        public int message_id;
        public int color;

        private void Form1_Load(object sender, EventArgs e)
        {
            message_id = 0;
            color = 0;
            form2x.Show();
            form2x.Location = new Point(700, 200);
            this.Location = new Point(300, 200);
            form3x.Show();
            form3x.Location = new Point(500, 600);

            listView1.View = View.Details;
            listView1.GridLines = true;
            listView1.FullRowSelect = true;
            listView1.Columns.Add("ID", 30);
            listView1.Columns.Add("Tarih", 60);
            listView1.Columns.Add("Mesaj", 450);
        }

        int counter = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            counter++;
            progressBar1.Value = counter;
            if(counter==3)
            {
                button1.Enabled = true;
                counter = 0;
                progressBar1.Value = 0;
                timer1.Stop();
            }
        }
    }
}
