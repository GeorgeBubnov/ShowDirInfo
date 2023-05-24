using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShowDirInfo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            folderBrowserDialog1.SelectedPath = Directory.GetCurrentDirectory();
            ExeptionDelegate exeptionViewer = ShowExeption;
            info = new Info(exeptionViewer);
        }
        IInfo info;
        protected void ShowExeption(string exeption)
        {
            MessageBox.Show(exeption, "Вызвана исключительная ситуация",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                info.DirectoriesInfo(folderBrowserDialog1.SelectedPath);
                try
                {
                    StreamReader sr = new StreamReader("log.txt");
                    string line = sr.ReadLine();
                    richTextBox1.Text = "";
                    while (line != null)
                    {
                        richTextBox1.Text += line + "\n";
                        Console.WriteLine(line);
                        line = sr.ReadLine();
                    }
                    sr.Close();
                }
                catch (Exception exception)
                {
                    ShowExeption(exception.Message);
                }
            }
        }
    }
}
