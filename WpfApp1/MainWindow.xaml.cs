using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string urlAddress { get; set; }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
             urlAddress = AdressName.Text;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;
                if (String.IsNullOrWhiteSpace(response.CharacterSet))
                    readStream = new StreamReader(receiveStream);
                else
                    readStream = new StreamReader(receiveStream,

                        Encoding.GetEncoding(response.CharacterSet));

                string data = readStream.ReadToEnd();
                var doc = new HtmlWeb().Load(urlAddress);
                var linkTags = doc.DocumentNode.Descendants("link");
                var linkedPages = doc.DocumentNode.Descendants("a")
                                                  .Select(a => a.GetAttributeValue("href", null))
                                                  .Where(u => !String.IsNullOrEmpty(u));
                string[] arr = linkedPages.ToArray();
                Adress.Header = AdressName.Text;
                one.Header = arr[0];
                two.Header = arr[1];
                three.Header = arr[2];
                four.Header = arr[3];
                five.Header = arr[4];
                six.Header = arr[5];
                Seven.Header = arr[6];
                response.Close();
                readStream.Close();
            }
        }
    }
}
