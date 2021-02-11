using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<int, string> words = new Dictionary<int, string>();
        int wordNum = 4;
        string password = "";
        public MainWindow()
        {
            InitializeComponent();
            LoadDictionary();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            password = "";
            wordCount();
            int value = RandomIntGenerator();
            password = value.ToString();
            presentResult();
        }

        /// <summary>
        /// Sets the number of words to the user specified value from the combo box
        /// </summary>
        private void wordCount() 
        {
            int userNum = Int32.Parse(NumWords.SelectedIndex.ToString());
            switch (userNum)
            {
                case 0:
                    wordNum = 4;
                    break;
                case 1:
                    wordNum = 5;
                    break;
                case 2:
                    wordNum = 6;
                    break;
                case 3:
                    wordNum = 7;
                    break;
                case 4:
                    wordNum = 8;
                    break;
                default:
                    wordNum = 4;
                    break;
            }
        }

        private void LoadDictionary() 
        {
            using (var readtextfile = new StreamReader("wordList.txt"))
            {
                string line = null;
                int number = 0;
                string listWord = "";

                while ((line = readtextfile.ReadLine()) != null)
                {
                    string[] lineSplit = line.Split("\t");
                    number = Int32.Parse(lineSplit[0]);
                    listWord = lineSplit[1];
                    words.Add(number, listWord);
                }
            }
        }

        static int RandomIntGenerator()
        {
            int lowerBound = 11111;
            int upperBound = 66666;
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            var byteArray = new byte[4];
            provider.GetBytes(byteArray);
            var scaler = BitConverter.ToUInt32(byteArray, 0);
            var result = (int)(lowerBound + (upperBound - lowerBound) * (scaler / (double)uint.MaxValue));
            return result;
        }

        private void presentResult() 
        {
            //password = wordNum.ToString();
            OutputResult.Text = password;
        }
    }
}
