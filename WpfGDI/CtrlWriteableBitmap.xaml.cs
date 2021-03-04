using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Drawing;
using System.Drawing.Drawing2D;
using Point = System.Drawing.Point;
using Image = System.Windows.Controls.Image;

namespace WpfGDI
{
    /// <summary>
    /// CtrlWriteableBitmap.xaml 的交互逻辑
    /// </summary>
    public partial class CtrlWriteableBitmap : UserControl
    {

        WriteableBitmap wBitmap = null;
        public bool Continued = true;

        public CtrlWriteableBitmap()
        {
            InitializeComponent();
            wBitmap = new WriteableBitmap((int)img.Width, (int)img.Height, 96, 96, PixelFormats.Pbgra32, null);
            img.Source = wBitmap;
        }

        protected void StartDrawGraph(System.Windows.Controls.Image GraphPanel)
        {
            Thread drawThread = new Thread(new ParameterizedThreadStart(DoDraw));

            //wBitmap.Lock();
            drawThread.Start(new object[] { wBitmap.BackBuffer, wBitmap.BackBufferStride, wBitmap.PixelWidth, wBitmap.PixelHeight });
        }

        protected void DoDraw(object Parameter)
        {

            object[] domain = (object[])Parameter;
            while (Continued)
            {
                //可以在线程中获取数据、生成图像内容，但是 显示图像内容的WriteableBitmap必须和主窗体属于同一线程，否则图像内容不能正常显示
                //采用的方法是：将WriteableBitmap的内存指针传递给线程
                this.Dispatcher.BeginInvoke(new Action(() => {
                    wBitmap.Lock();
                }));

                FillGraphToBitmap((IntPtr)domain[0], (int)domain[1], (int)domain[2], (int)domain[3]);

                this.Dispatcher.BeginInvoke(new Action(() => {
                    wBitmap.AddDirtyRect(new System.Windows.Int32Rect(0, 0, (int)img.Width, (int)img.Height));
                    wBitmap.Unlock();
                }));
                Thread.Sleep(100);
            }
        }

        protected void FillGraphToBitmap(IntPtr WBmpBackBuffer, int WBmpBackBufferStride, int x, int y)
        {
            Bitmap backBitmap = new Bitmap(x, y, WBmpBackBufferStride, System.Drawing.Imaging.PixelFormat.Format32bppPArgb, WBmpBackBuffer);
            Graphics graphics = Graphics.FromImage(backBitmap);
            //graphics.Clear(System.Drawing.Color.White);
            GraphicsPath path = new GraphicsPath();
            path.FillMode = FillMode.Winding;
            AddPolyLines(path, x, y);
            graphics.DrawPath(new System.Drawing.Pen(System.Drawing.Color.Green, 1f), path);
            graphics.Flush();
            path.Dispose();
            path = null;
            graphics.Dispose();
            graphics = null;
            backBitmap.Dispose();
            backBitmap = null;
        }

        protected void AddPolyLines(GraphicsPath gPath, int X, int Y)
        {
            Random rx = new Random();
            for (int i = 0; i < 30; i++)
            {
                Point p1 = new Point(rx.Next(X), rx.Next(Y));
                Point p2 = new Point(rx.Next(X), rx.Next(Y));
                gPath.AddLine(p1, p2);
            }
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;
            if (!Continued)
            {
                Continued = true;
                btn.Content = "开始";
            }
            else
            {
                Continued = false;
                btn.Content = "停止";
            }
        }

        private void BtnBengin_Click(object sender, RoutedEventArgs e)
        {
            StartDrawGraph(img);
        }

        private void Grid_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }
    }
}
