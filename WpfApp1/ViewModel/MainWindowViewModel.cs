using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Commands;
using WpfApp1.Exception1;

namespace WpfApp1.ViewModel
{
    public class MainWindowViewModel : BaseViewModel
    {
        public string urlAddress { get; set; }

        public RelayCommand SearchBtnClick { get; set; }
        public MainWindowViewModel(MainWindow mainWindow)
        {
            SearchBtnClick = new RelayCommand((sender) =>
            {
               
                try
                {
                    urlAddress = mainWindow.AdressName.Text;
                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlAddress);
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    StreamReader readStream = null;

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Stream receiveStream = response.GetResponseStream();

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
                        if (arr != null)
                        {
                            mainWindow.Adress.Header = mainWindow.AdressName.Text;
                            mainWindow.one.Header = arr[0];
                            mainWindow.two.Header = arr[1];
                            mainWindow.three.Header = arr[2];
                            mainWindow.four.Header = arr[3];
                            mainWindow.five.Header = arr[4];
                            mainWindow.six.Header = arr[5];
                            mainWindow.Seven.Header = arr[6];
                        }
                        else
                        {
                            throw new InsertedWrongAdressException("Adress Not Found! Try Again");
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                



            });

        }
    }
}
