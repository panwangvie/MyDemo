using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing.Imaging;
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

namespace ImageConvert
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //ObservableCollection<ImageFormat> imageFormats=new ObservableCollection<ImageFormat>();
        public MainWindow()
        {
            InitializeComponent();

        }

        private static void InitImageFormats()
        {
            //imageFormats.Add(ImageFormat.Bmp);
            //imageFormats.Add(ImageFormat.Bmp);
            //imageFormats.Add(ImageFormat.Bmp);
            //imageFormats.Add(ImageFormat.Bmp);
        }
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void Selector_OnSelected(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}
