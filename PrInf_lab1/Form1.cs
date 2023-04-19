using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace PrInf_lab1
{
    public partial class Form1 : Form
    {
        string data;
        public Form1()
        {
            InitializeComponent();
            DataFileGet();
        }

        private void DataFileGet()
        {
            data = File.ReadAllText(@"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing1.txt");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            data = data.Replace(textBox2.Text, textBox3.Text);
            byte[] bytes = Encoding.UTF8.GetBytes(data); 
            string path = @"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing1.txt";
            File.WriteAllBytes(path, bytes); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string command = "openssl"; // ��� ����������
            string arguments = "dgst -sha1 C:\\VSProjects\\PrInf_lab1\\PrInf_lab1\\Resources\\leasing1.txt"; // ���������

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = command;
            startInfo.Arguments = arguments;
            startInfo.RedirectStandardOutput = true; // �������������� ����������� ����� � �������


            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            textBox1.Text = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
        }
    }
}