using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace DriwingVisualFwDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DrawingVisual visualx = new DrawingVisual();
            System.Diagnostics.Stopwatch stopwatch = new System.Diagnostics.Stopwatch();

            stopwatch.Start();
            using (DrawingContext dc = visualx.RenderOpen())
            {
                dc.PushOpacity(0.8);
                dc.PushOpacityMask(Brushes.Red);
                Pen pen = new Pen(Brushes.Green, 2);
                //如果对Pen对象调用Freeze（）方法，冻结对对象的修改，则可以大幅度提升绘图性能
               pen.Freeze();
                for (int i = 0; i < 5000; i++)
                {
                    dc.DrawLine(pen, new Point(i * 2, Math.Sin(i * 0.1) * 100 + 200), new Point((i + 1) * 2, Math.Sin((i + 1) * 0.1) * 100 + 200));
                    dc.DrawText(new FormattedText("Hello, World!", CultureInfo.CurrentCulture, FlowDirection.LeftToRight, new Typeface("Arial"), 10, Brushes.CornflowerBlue), new Point(i * 2, Math.Sin(i * 0.1) * 100 + 200));
                }
            }
            panel.AddVisual(visualx);
            stopwatch.Stop();
            Console.WriteLine(stopwatch.ElapsedTicks);
            //WriteableBitmap
        }
    }
}
