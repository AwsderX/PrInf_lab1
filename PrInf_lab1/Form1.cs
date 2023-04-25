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
        int fl = 3;
        int pos = 2;
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
            //data = data.Replace("1 ", "11 ");

            string oldWord = "1";
            int index = data.IndexOf(oldWord);
            data = data.Substring(0, index) + "11" + data.Substring(index + oldWord.Length);

            byte[] bytes = Encoding.UTF8.GetBytes(data);
            string path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\txt{f}.txt";
            File.WriteAllBytes(path, bytes);
           // getHashSha1();
            f++; //2
            //Переходим на файл 2
            DataFileGet();
           // data = data.Replace("\n1 ", "\n12 ");

            oldWord = "1";
            index = data.IndexOf(oldWord);
            data = data.Substring(0, index) + "12" + data.Substring(index + oldWord.Length);

            bytes = Encoding.UTF8.GetBytes(data);
            path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\txt{f}.txt";
            File.WriteAllBytes(path, bytes);
            getHashSha1();
            f++;
            int temp = 2;

            //string[] initialFiles = { @"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\txt1.txt", @"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\txt2.txt" };
            List<string> files = new List<string>{ @"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\txt1.txt", @"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\txt2.txt" };
            waySub = @"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\txt".Length;
            iterations = 14; //160!
            //fl = 3;
            //iterationsLeft=iterations;
            // DoubleFiles(initialFiles);
            DoubleFiles(files, iterations);


            //DoubleFiles(initialFiles, iterations);

            #region 1
            //while (stop == false)
            //{
            //    for (int i = temp / 2; i < temp; i++)
            //    {
            //        DataFileGet(i);

            //        string[] words = data.Split(' ');
            //        data = String.Join(", ", words[pos]);
            //        bytes = Encoding.UTF8.GetBytes(data);
            //        path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
            //        File.WriteAllBytes(path, bytes);
            //        getHashSha1();
            //        f++;
            //    }
            //    temp = f - 1;
            //    pos++;
            //}
            #endregion


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

                // add the new files to the list
                newFiles.Add(newFile1);
                newFiles.Add(newFile2);
            }
            //getHashSha1();
            if (stop==true) { return; }
            // call the function again with the new files and decremented iterationsLeft
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
        //private void DoubleFiles(string[] files, int iterationsLeft)
        //{
        //    if (iterationsLeft == 0)
        //    {
        //        return;
        //    }

        //    string[] newFiles = new string[files.Length * 2];

        //    for (int i = 0; i < files.Length; i++)
        //    {
        //        // Делаем изменение
        //        string newFile1 = MakeChangesToFile(files[i], i);
        //        string newFile2 = MakeChangesToFile(files[i], i);

        //        // add the new files to the array
        //        newFiles[i * 2] = newFile1;
        //        newFiles[i * 2 + 1] = newFile2;
        //    }

        //    // call the function again with the new files and decremented iterationsLeft
        //    DoubleFiles(newFiles, iterationsLeft - 1);
        //}

        //private string MakeChangesToFile(string filePath, int changeNumber)
        //{
        //    string newFileName = Path.GetFileNameWithoutExtension(filePath) + "_" + (changeNumber * 2 + 1) + Path.GetExtension(filePath);
        //    string newFilePath = Path.Combine(Path.GetDirectoryName(filePath), newFileName);

        //    File.Copy(filePath, newFilePath, true);
        //    string fileString = File.ReadAllText(newFilePath);

        //    // replace the word in the file
        //    fileString = fileString.Replace("1", (changeNumber * 2 + 2).ToString());

        //    byte[] bytes = Encoding.UTF8.GetBytes(fileString);
        //    File.WriteAllBytes(newFilePath, bytes);

        //    return newFilePath;
        //}





        //private void DoubleFiles(string[] files)
        //{
        //    if (iterationsLeft == 0)
        //    {
        //        return;
        //    }

        //    string[] newFiles = new string[files.Length * 2];

        //    for (int i = 0; i < files.Length; i++)
        //    {
        //        // Делаем изменение
        //        string newFile1 = MakeChangesToFile(files[i], i);
        //        string newFile2 = MakeChangesToFile(files[i], i);

        //        // add the new files to the array
        //        newFiles[i * 2] = newFile1;
        //        newFiles[i * 2 + 1] = newFile2;
        //    }

        //    // call the function again with the new files and decremented iterationsLeft
        //    iterationsLeft--;
        //    DoubleFiles(newFiles);
        //    //DoubleFiles(newFiles, iterationsLeft - 1);
        //}

        //private string MakeChangesToFile(string filePath, int changeNumber)
        //{
        //    changeNumber++;
        //    //string newFileName = Path.GetFileNameWithoutExtension(filePath).Substring(0, Path.GetFileNameWithoutExtension(filePath).Length-1) + "1" + Path.GetExtension(filePath);
        //    string newFileName = filePath.Substring(0, waySub) + fl + Path.GetExtension(filePath);
        //    fl++;

        //    File.Copy(filePath, newFileName, true);
        //    string fileString = File.ReadAllText(filePath);

        //    //string[] words = fileString.Split(' ');
        //    string oldWord = $"{changeNumber}";
        //    if (changeNumber != 1) 
        //    { 
        //        oldWord = $" {changeNumber}";
        //    }
        //    int index = fileString.IndexOf(oldWord)+oldWord.Length;
        //    fileString = fileString.Substring(0, index) + fl + fileString.Substring(index + oldWord.Length);
        //    //fileString = String.Join("1 ", words[changeNumber]);

        //    byte[] bytes = Encoding.UTF8.GetBytes(fileString);
        //    File.WriteAllBytes(newFileName, bytes);

        //    return newFileName;
        //}

        private async void button3_Click(object sender, EventArgs e)
        {
            MainWork();
            await Task.Delay(1);

            //DataFileGet();

            //byte[] bytes = Encoding.UTF8.GetBytes(data);
            //string path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
            //File.WriteAllBytes(path, bytes);
            //getHashSha1();
            //f++;

            //data = data.Replace("положения", "условия");
            //bytes = Encoding.UTF8.GetBytes(data);
            //path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
            //File.WriteAllBytes(path, bytes);
            //getHashSha1();
            //f++;
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
            //while (stop == false && f < 20000) 
            //{ 
            //    for (int i = temp / 2; i < temp; i++)
            //    {
            //        DataFileGet(i);

            //        string[] words = data.Split(' ');
            //        data = String.Join(", ", words[1]);
            //        bytes = Encoding.UTF8.GetBytes(data);
            //        path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
            //        File.WriteAllBytes(path, bytes);
            //        getHashSha1();
            //        f++;
            //    }
            //    temp = f - 1;
            //    for (int i = temp / 2; i < temp; i++)
            //    {
            //        DataFileGet(i);

            //        string[] words = data.Split(' ');
            //        data = String.Join(" однако ", words);
            //        bytes = Encoding.UTF8.GetBytes(data);
            //        path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
            //        File.WriteAllBytes(path, bytes);
            //        getHashSha1();
            //        f++;
            //    }
            //    temp = f - 1;
            //    //for (int i = temp / 2; i < temp; i++)
            //    //{
            //    //    DataFileGet(i);

            //    //    data = data.Replace(" ", " \b ");
            //    //    bytes = Encoding.UTF8.GetBytes(data);
            //    //    path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
            //    //    File.WriteAllBytes(path, bytes);
            //    //    getHashSha1();
            //    //    f++;
            //    //}
            //    //temp = f - 1;
            //    //for (int i = temp / 2; i < temp; i++)
            //    //{
            //    //    DataFileGet(i);

            //    //    data += "\r\n";
            //    //    bytes = Encoding.UTF8.GetBytes(data);
            //    //    path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
            //    //    File.WriteAllBytes(path, bytes);
            //    //    getHashSha1();
            //    //    f++;
            //    //}
            //    await Task.Delay(1);
            //}


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

            //    data = data.Replace("экономики", "экономического развития");
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

            //    data = data.Replace("Российской Федерации", "России");
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

            //    data = data.Replace("договора", "соглашения");
            //    bytes = Encoding.UTF8.GetBytes(data);
            //    path = @$"C:\VSProjects\PrInf_lab1\PrInf_lab1\Resources\leasing{f}.txt";
            //    File.WriteAllBytes(path, bytes);
            //    getHashSha1();
            //    f++;
            //}
            //temp = f - 1;


            //string allItems = String.Join(Environment.NewLine, listHash);
            //textBox1.Text = allItems;
            //for (int i = 0; i < listHash.Count; i++)
            //{
            //    textBox1.Text += listHash[i] + Environment.NewLine;
            //}
        }

    }
}