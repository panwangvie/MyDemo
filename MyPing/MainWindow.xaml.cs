using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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

namespace MyPing
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow()
        {
            InitializeComponent();
            
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            if (PropertyChanged != null)
            {
                try
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
                }
                catch (Exception ex)
                {

                }
            }
        }
        private List<string> _ipList = new List<string>();
        public List<string> ipList {
            get => _ipList;
            set 
            { 
                _ipList = value; OnPropertyChanged();
            } }

        private ObservableCollection<string> _vs = new ObservableCollection<string>();
        public ObservableCollection<string> vs { get => _vs; set { _vs = value; OnPropertyChanged(); } }
        static object obj = new object();
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var start = tbSIp1.Text.Trim();
            var sta = tbSIp4.Text.Trim();
            var end = tbEndIp4.Text.Trim();
            vs.Clear();
            ipList.Clear();

            for (int i = int.Parse(sta); i <= int.Parse(end); i++)
            {
                string ip = start + "." + i;
                 IPAddress iP = IPAddress.Parse(ip);
                ipList.Add(ip+"不在线");
                //System.Threading.Thread thread =new Thread(()=> {
                lock (obj)
                    {
                      Ping ping = new Ping();

                     Task<PingReply> pingReply1 = ping.SendPingAsync(iP);
                  
                    pingReply1.ContinueWith((task) => {

                        PingReply pingReply = task.Result;
                        if (pingReply.Status == IPStatus.Success)
                        {


                            var index = ipList.FindIndex(x => x.Contains(pingReply.Address.ToString()));
                            if (index != -1)
                            {
                                this.Dispatcher.Invoke(() =>
                                                            {

                                                                vs[index] = pingReply.Address.ToString()+"在线 " +  (pingReply.RoundtripTime+1)+"ms";
                                                            });
                            }
                        }
                        else if (pingReply.Status == IPStatus.TimedOut)
                        {
                            // ping.SendPing(pingReply.Address);
                            //this.Dispatcher.Invoke(() =>
                            //{
                            //});

                        }
                        else
                        {
                            //this.Dispatcher.Invoke(() =>
                            //{
                            //});

                        }

                    });

                        //PingReply pingReply = ping.Send(ip);
                        //if (pingReply.Status == IPStatus.Success)
                        //{
                        //    this.Dispatcher.Invoke(() =>
                        //    {
                        //        listBox.Items.Add(new TextBox() { Text = i + " 在线  " });
                        //    });

                    //}
                    //else if (pingReply.Status == IPStatus.TimedOut)
                    //{
                    //    this.Dispatcher.Invoke(() =>
                    //    {
                    //        listBox.Items.Add(new TextBox() { Text = i + " 超时  " });
                    //    });

                    //}
                    //else
                    //{
                    //    this.Dispatcher.Invoke(() =>
                    //    {
                    //        listBox.Items.Add(new TextBox() { Text = i + " 失败  " });
                    //    });

                    //}
                }
                //});
                //thread.Start();

            }
            vs = new ObservableCollection<string>(ipList);
             this.IpICtl.ItemsSource = vs;
         }
    }
}
