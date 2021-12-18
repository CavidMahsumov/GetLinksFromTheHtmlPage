using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Commands;

namespace WpfApp1.ViewModel
{
   public class MainWindowViewModel:BaseViewModel
    {
        public string urlAddress { get; set; }

        public RelayCommand SearchBtnClick { get; set; }
        public MainWindowViewModel(MainWindow mainWindow)
        {
            SearchBtnClick = new RelayCommand((sender) => {

                urlAddress = mainWindow.AdressName.Text;
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
                    mainWindow.Adress.Header = mainWindow.AdressName.Text;
                    mainWindow.one.Header = arr[0];
                    mainWindow.two.Header = arr[1];
                    mainWindow.three.Header = arr[2];
                    mainWindow.four.Header = arr[3];
                    mainWindow.five.Header = arr[4];
                    mainWindow.six.Header = arr[5];
                    mainWindow.Seven.Header = arr[6];
                    response.Close();
                    readStream.Close();
                }

            });

        }
    }
}
