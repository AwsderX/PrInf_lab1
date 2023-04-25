using Microsoft.VisualBasic.Devices;
using PrInf_lab1.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace PrInf_lab1
{
    public partial class Form1 : Form
    {
        string data;
        int waySub = 0;
        int f = 1;
        int iterations;
        bool stop = false;

        List<string> listHash = new List<string>();

        public Form1()
        {
            InitializeComponent();
            DataFileGet();
            label1.Text = "False";
            label1.ForeColor = Color.Red;
        }

        private void DataFileGet()
        {
            data = File.ReadAllText(@"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\txt.txt");
        }
        private void DataFileGet(int i)
        {
            data = File.ReadAllText(@$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\txt{i}.txt");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            data = data.Replace(textBox2.Text, textBox3.Text);
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            string path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
            File.WriteAllBytes(path, bytes);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            getHashSha1();
        }

        private void getHashSha1()
        {

            string command = "openssl"; // имя приложения
            string arguments = $"dgst -sha1 C:\\VSProjects\\PrInf_lab1\\PrInf_lab1\\Resources\\txt{f}.txt"; // аргументы

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = command;
            startInfo.Arguments = arguments;
            startInfo.RedirectStandardOutput = true; // перенаправляем стандартный вывод в процесс


            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            textBox1.Text = "";
            textBox1.Text = process.StandardOutput.ReadToEnd();
            textBox1.Text += Environment.NewLine;
            string a = textBox1.Text.Replace($"SHA1(C:\\VSProjects\\PrInf_lab1\\PrInf_lab1\\Resources\\txt{f}.txt)= ", "").Replace("\r\n", "");
            if (listHash.Contains(a))
            {
                textBox2.Text = $"Найдено совпадение - {a}{Environment.NewLine}";
                stop = true;
                label1.Text = "True";
                label1.ForeColor = Color.Green;
            }
            listHash.Add(a);
            process.WaitForExit();

            
        }

        public void MainWork()
        {
            DataFileGet();

            string oldWord = "1";
            int index = data.IndexOf(oldWord);
            data = data.Substring(0, index) + "11" + data.Substring(index + oldWord.Length);

            byte[] bytes = Encoding.UTF8.GetBytes(data);
            string path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\txt{f}.txt";
            File.WriteAllBytes(path, bytes);
            f++; //2
            //Переходим на файл 2
            DataFileGet();

            oldWord = "1";
            index = data.IndexOf(oldWord);
            data = data.Substring(0, index) + "12" + data.Substring(index + oldWord.Length);

            bytes = Encoding.UTF8.GetBytes(data);
            path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\txt{f}.txt";
            File.WriteAllBytes(path, bytes);
            getHashSha1();
            f++;
           
            List<string> files = new List<string>{ @"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\txt1.txt", @"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\txt2.txt" };
            waySub = @"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\txt".Length;
            iterations = 160; //160!
            DoubleFiles(files, iterations);


            string allItems = String.Join(Environment.NewLine, listHash);
            textBox1.Text = allItems;
        }
        private void DoubleFiles(List<string> files, int iterationsLeft)
        {
            if (iterationsLeft == 0)
            {
                return;
            }

            List<string> newFiles = new List<string>();

            foreach (string file in files)
            {
                // Делаем изменение
                string newFile1 = MakeChangesToFile(file, iterationsLeft);
                string newFile2 = MakeChangesToFile(file, iterationsLeft);

                // Добавляем новый файлы в лист
                newFiles.Add(newFile1);
                newFiles.Add(newFile2);
            }
            if (stop==true) { return; }
            // вызываем рекурсию с новым списком файлов и уменьшенным количеством итераций
            DoubleFiles(newFiles, iterationsLeft - 1);
        }

        private string MakeChangesToFile(string filePath, int iterationsLeft)
        {
            string newFileName = filePath.Substring(0, waySub) + f + Path.GetExtension(filePath);      

            File.Copy(filePath, newFileName, true);
            string fileString = File.ReadAllText(newFileName);

            string oldWord = $" {iterations-iterationsLeft+2}";

            int index = fileString.IndexOf(oldWord)+ oldWord.Length;
            fileString = fileString.Substring(0, index) + f + fileString.Substring(index);

            byte[] bytes = Encoding.UTF8.GetBytes(fileString);
            File.WriteAllBytes(newFileName, bytes);

            getHashSha1(newFileName);

            f++;
            return newFileName;
        }
        private void getHashSha1(string filePath)
        {

            string command = "openssl"; // имя приложения
            string arguments = $"dgst -sha1 {filePath}"; // аргументы

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = command;
            startInfo.Arguments = arguments;
            startInfo.RedirectStandardOutput = true; // перенаправляем стандартный вывод в процесс


            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            textBox1.Text = "";
            textBox1.Text = process.StandardOutput.ReadToEnd();
            textBox1.Text += Environment.NewLine;
            string a = textBox1.Text.Replace($"SHA1({filePath})= ", "").Replace("\r\n", "");
            if (listHash.Contains(a))
            {
                textBox2.Text = $"Найдено совпадение - {a}{Environment.NewLine}";
                stop = true;
                label1.Text = "True";
                label1.ForeColor = Color.Green;
            }
            listHash.Add(a);
            process.WaitForExit();


        }
        

        private async void button3_Click(object sender, EventArgs e)
        {
            MainWork();
            await Task.Delay(1);           
        }

    }
}