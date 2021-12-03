using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System;
namespace FindCharInString
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 


    public partial class MainWindow : Window
    {
        bool found = false;
        [DllImport(@"D:\Politechnika v2\5 semestr\JA_projekt\FindCharInString\x64\Debug\JAAsm.dll")]
        unsafe static extern bool FindCharAsm(char* str, char c, int n);

        [DllImport(@"D:\Politechnika v2\5 semestr\JA_projekt\FindCharInString\x64\Debug\JAcpp.dll")]
        unsafe static extern bool FindCharCpp(char* ptr, char c, int n);
        string filePath;
        private void Clicked(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDlg = new Microsoft.Win32.OpenFileDialog();
            fileDlg.DefaultExt = ".txt";
            fileDlg.Filter = "Text Files|*.txt";
            if (fileDlg.ShowDialog() == true)
            {
                filePath = fileName.Text = fileDlg.FileName;
            }
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
        }

        private void checkbox1_Checked(object sender, RoutedEventArgs e)
        {
            checkbox2.IsChecked = false;
        }
        private void checkbox2_Checked(object sender, RoutedEventArgs e)
        {
            checkbox1.IsChecked = false;
        }
        unsafe private void ThreadsCPP(List<string> list, char c, int threadNumber)
        {
            //threadList = new List<Thread>();
            for (int i = 0; i < threadNumber; i++)
            {
                var str = list[i].ToCharArray();
                fixed (char* ptr = &str[0])
                {
                    ThreadedMethod<bool> threadedMethod = new ThreadedMethod<bool>();
                    UnsafePtr ptrClass = new UnsafePtr(ptr);
                    var t = new Thread((unused) => threadedMethod.ExecuteMethod(() => FindCharCpp(ptrClass.ptr, c, list[i].Length)));
                    t.Start();
                    //threadList.Add(t);
                    t.Join();
                    //ptrClass = null;
                    if (threadedMethod.Result == true)
                    {
                        found = true;
                        break;
                    }
                }
            }
        }
        unsafe private void ThreadsASM(List<string> list, char c, int threadNumber)
        {
            //threadList = new List<Thread>();
            for (int i = 0; i < threadNumber; i++)
            {
                var str = list[i].ToCharArray();
                fixed (char* ptr = &str[0])
                {
                    ThreadedMethod<bool> threadedMethod = new ThreadedMethod<bool>();
                    UnsafePtr ptrClass = new UnsafePtr(ptr);
                    var t = new Thread((unused) => threadedMethod.ExecuteMethod(() => FindCharAsm(ptrClass.ptr, c, list[i].Length)));
                    t.Start();
                    //threadList.Add(t);
                    t.Join();
                    //ptrClass = null;
                    if (threadedMethod.Result == true)
                    {
                        found = true;
                        break;
                    }
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if(fileName.Text == "Your file" || fileName.Text == null)
                MessageBox.Show("Wybierz plik wejsciowy!", "Błąd");
            if (charName.Text.Length <= 0)
                MessageBox.Show("Podaj szukany znak!", "Błąd");
            else
            {
                DataManager dataManager = new DataManager(filePath);
                int threadNumber = int.Parse((ThreadBox.SelectedItem as ComboBoxItem).Content.ToString());
                int charPerThread = dataManager.getData().Length / threadNumber;
                dataManager.divideData(threadNumber, charPerThread);
                char[] c = charName.Text.ToCharArray();

                bool checkCPP = false;
                if (checkbox1.IsChecked == true)
                    checkCPP = true;
                DateTime start = DateTime.Now;
                if (checkCPP == true)
                    ThreadsCPP(dataManager.getList(), c[0], threadNumber);
                else if (checkCPP == false)
                    ThreadsASM(dataManager.getList(), c[0], threadNumber);
                DateTime end = DateTime.Now;
                string time = (end - start).TotalMilliseconds.ToString() + "ms";
                if (found == true)
                    MessageBox.Show("Znaleziono podany znak!" + "\nOperacja zajęła: " + time/*+ (ThreadBox.SelectedItem as ComboBoxItem).Content.ToString()*/, "Wynik");
                else
                    MessageBox.Show("Nie znaleziono podanego znaku!" + "\nOperacja zajęła: " + time, "Wynik");
                found = false;
            }
        }
    }
}
