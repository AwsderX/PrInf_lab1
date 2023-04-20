using System;
using System.Diagnostics;
using System.Reflection;
using System.Text;

namespace PrInf_lab1
{
    public partial class Form1 : Form
    {
        string data;
        int f = 1;
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
            data = File.ReadAllText(@"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing.txt");
        }
        private void DataFileGet(int i)
        {
            data = File.ReadAllText(@$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{i}.txt");
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

            string command = "openssl"; // им€ приложени€
            string arguments = $"dgst -sha1 C:\\VSProjects\\PrInf_lab1\\PrInf_lab1\\Resources\\leasing{f}.txt"; // аргументы

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = command;
            startInfo.Arguments = arguments;
            startInfo.RedirectStandardOutput = true; // перенаправл€ем стандартный вывод в процесс


            Process process = new Process();
            process.StartInfo = startInfo;
            process.Start();
            textBox1.Text = "";
            textBox1.Text = process.StandardOutput.ReadToEnd();
            textBox1.Text += Environment.NewLine;
            string a = textBox1.Text.Replace($"SHA1(C:\\VSProjects\\PrInf_lab1\\PrInf_lab1\\Resources\\leasing{f}.txt)= ", "").Replace("\r\n", "");
            if (listHash.Contains(a))
            {
                textBox2.Text = $"Ќайдено совпадение - {a}{Environment.NewLine}";
                stop = true;
                label1.Text = "True";
                label1.ForeColor = Color.Green;
            }
            listHash.Add(a);
            process.WaitForExit();

            
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            DataFileGet();

            byte[] bytes = Encoding.UTF8.GetBytes(data);
            string path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
            File.WriteAllBytes(path, bytes);
            getHashSha1();
            f++;

            data = data.Replace("положени€", "услови€");
            bytes = Encoding.UTF8.GetBytes(data);
            path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
            File.WriteAllBytes(path, bytes);
            getHashSha1();
            f++;
            int temp = 0;
            for (int i = 1; i < 2; i++)
            {
                DataFileGet(i); 

                data = data.Replace("платежей", "выплат");
                bytes = Encoding.UTF8.GetBytes(data);
                path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
                File.WriteAllBytes(path, bytes);
                getHashSha1();
                f++;
            }
            temp = f - 1;
            while (stop == false && f < 20000) 
            { 
                for (int i = temp / 2; i < temp; i++)
                {
                    DataFileGet(i);

                    string[] words = data.Split(' ');
                    data = String.Join(", ", words);
                    bytes = Encoding.UTF8.GetBytes(data);
                    path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
                    File.WriteAllBytes(path, bytes);
                    getHashSha1();
                    f++;
                }
                temp = f - 1;
                for (int i = temp / 2; i < temp; i++)
                {
                    DataFileGet(i);

                    string[] words = data.Split(' ');
                    data = String.Join(" однако ", words);
                    bytes = Encoding.UTF8.GetBytes(data);
                    path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
                    File.WriteAllBytes(path, bytes);
                    getHashSha1();
                    f++;
                }
                temp = f - 1;
                //for (int i = temp / 2; i < temp; i++)
                //{
                //    DataFileGet(i);

                //    data = data.Replace(" ", " \b ");
                //    bytes = Encoding.UTF8.GetBytes(data);
                //    path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
                //    File.WriteAllBytes(path, bytes);
                //    getHashSha1();
                //    f++;
                //}
                //temp = f - 1;
                //for (int i = temp / 2; i < temp; i++)
                //{
                //    DataFileGet(i);

                //    data += "\r\n";
                //    bytes = Encoding.UTF8.GetBytes(data);
                //    path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
                //    File.WriteAllBytes(path, bytes);
                //    getHashSha1();
                //    f++;
                //}
                await Task.Delay(1);
            }


            //int temp = 0;
            //for (int i = 1; i < 2; i++)
            //{
            //    DataFileGet(i);

            //    data = data.Replace("платежей", "выплат");
            //    bytes = Encoding.UTF8.GetBytes(data);
            //    path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
            //    File.WriteAllBytes(path, bytes);
            //    getHashSha1();
            //    f++;
            //}
            //temp = f - 1;

            //for (int i = temp / 2; i < temp; i++)
            //{
            //    DataFileGet(i);

            //    data = data.Replace("экономики", "экономического развити€");
            //    bytes = Encoding.UTF8.GetBytes(data);
            //    path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
            //    File.WriteAllBytes(path, bytes);
            //    getHashSha1();
            //    f++;
            //}
            //temp = f - 1;

            //for (int i = temp / 2; i < temp; i++)
            //{
            //    DataFileGet(i);

            //    data = data.Replace("–оссийской ‘едерации", "–оссии");
            //    bytes = Encoding.UTF8.GetBytes(data);
            //    path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
            //    File.WriteAllBytes(path, bytes);
            //    getHashSha1();

            //    f++;
            //}
            //temp = f - 1;
            //for (int i = temp / 2; i < temp; i++)
            //{
            //    DataFileGet(i);

            //    data = data.Replace("млн", "миллионов");
            //    bytes = Encoding.UTF8.GetBytes(data);
            //    path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
            //    File.WriteAllBytes(path, bytes);
            //    getHashSha1();

            //    f++;
            //}
            //temp = f - 1;

            //for (int i = temp / 2; i < temp; i++)
            //{
            //    DataFileGet(i);

            //    data = data.Replace("руб", "рублей");
            //    bytes = Encoding.UTF8.GetBytes(data);
            //    path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
            //    File.WriteAllBytes(path, bytes);
            //    getHashSha1();

            //    f++;
            //}
            //temp = f - 1;

            //for (int i = temp / 2; i < temp; i++)
            //{
            //    DataFileGet(i);

            //    data = data.Replace("2", "6");
            //    bytes = Encoding.UTF8.GetBytes(data);
            //    path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
            //    File.WriteAllBytes(path, bytes);
            //    getHashSha1();
            //    f++;
            //}
            //temp = f - 1;
            //for (int i = temp / 2; i < temp; i++)
            //{
            //    DataFileGet(i);

            //    data = data.Replace("лизинговых", "арендных");
            //    bytes = Encoding.UTF8.GetBytes(data);
            //    path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
            //    File.WriteAllBytes(path, bytes);
            //    getHashSha1();
            //    f++;
            //}
            //temp = f - 1;
            //for (int i = temp / 2; i < temp; i++)
            //{
            //    DataFileGet(i);

            //    data = data.Replace("8", "4");
            //    bytes = Encoding.UTF8.GetBytes(data);
            //    path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
            //    File.WriteAllBytes(path, bytes);
            //    getHashSha1();
            //    f++;
            //}
            //temp = f - 1;

            //for (int i = temp / 2; i < temp; i++)
            //{
            //    DataFileGet(i);

            //    data = data.Replace("96", "40 ");
            //    bytes = Encoding.UTF8.GetBytes(data);
            //    path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
            //    File.WriteAllBytes(path, bytes);
            //    getHashSha1();
            //    f++;
            //}
            //temp = f - 1;
            //for (int i = temp / 2; i < temp; i++)
            //{
            //    DataFileGet(i);

            //    data += "\r\n";
            //    bytes = Encoding.UTF8.GetBytes(data);
            //    path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
            //    File.WriteAllBytes(path, bytes);
            //    getHashSha1();
            //    f++;
            //}
            //temp = f - 1;
            //for (int i = temp / 2; i < temp; i++)
            //{
            //    DataFileGet(i);

            //    data = data.Replace("договора", "соглашени€");
            //    bytes = Encoding.UTF8.GetBytes(data);
            //    path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
            //    File.WriteAllBytes(path, bytes);
            //    getHashSha1();
            //    f++;
            //}
            //temp = f - 1;


            string allItems = String.Join(Environment.NewLine, listHash);
            textBox1.Text = allItems;
            //for (int i = 0; i < listHash.Count; i++)
            //{
            //    textBox1.Text += listHash[i] + Environment.NewLine;
            //}
        }

    }
}