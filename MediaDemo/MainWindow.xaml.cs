using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
using Microsoft.Win32;

namespace MediaDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Open_OnClick(object sender, RoutedEventArgs e)
        {
            double screenHeight = SystemParameters.FullPrimaryScreenHeight;
            double screenWidth = SystemParameters.FullPrimaryScreenWidth;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择多媒体";
            //openFileDialog.Filter =
            //    "图片(*.*,*.png,*.bmp,*jpeg,*jpe,*pns,*tif,*tiff)|*.jpg;*.png;*.bmp;*jpeg;*jpe;*pns;*tif;*tiff";
            openFileDialog.FileName = string.Empty;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() is bool openDialog)
            {
                if (openDialog)
                {
                    var path = openFileDialog.FileName;
                    if (File.Exists(path))
                    {
                        //if (media.LoadedBehavior == MediaState.Manual)

                        media.Source = new Uri(path);
                        media.Play();
                    }
                }
            }

        }

        private void Start_OnClick(object sender, RoutedEventArgs e)
        {
            if ( start.Content.ToString() == "暂停")
            {
                media.Pause();
                start.Content = "开始";
            }
            else if(start.Content.ToString() == "开始")
            {
                media.Play();
                start.Content = "暂停";
            }
        }
    }
}
