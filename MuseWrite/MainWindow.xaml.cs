using System;
using System.Collections.Generic;
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

namespace MuseWrite
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 起始位置
        /// </summary>
        Point startPoint;
        /// <summary>
        /// 点集合
        /// </summary>
        List<Point> pointList = new List<Point>();
        /// <summary>
        /// 鼠标左键按下获取开始Point
        /// </summary>
        private void Canvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(myCanvas);
        }

        /// <summary>
        /// 按下鼠标左键移动
        /// </summary>
        private void Canvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                // 返回指针相对于Canvas的位置
                Point point = e.GetPosition(myCanvas);

                if (pointList.Count == 0)
                {
                    // 加入起始点
                    pointList.Add(new Point(this.startPoint.X, this.startPoint.Y));
                }
                else
                {
                    // 加入移动过程中的point
                    pointList.Add(point);
                }

                // 去重复点
                var disList = pointList.Distinct().ToList();
                var count = disList.Count(); // 总点数
                if (point != this.startPoint && this.startPoint != null)
                {
                    var l = new Line();
                    string color = (cboColor.SelectedItem as ComboBoxItem).Content as string;

                    if (color == "默认")
                    {
                        l.Stroke = Brushes.Black;
                    }
                    if (color == "红色")
                    {
                        l.Stroke = Brushes.Red;
                    }
                    if (color == "绿色")
                    {
                        l.Stroke = Brushes.Green;
                    }
                    l.StrokeThickness = 1;
                    if (count < 2)
                        return;
                    l.X1 = disList[count - 2].X;  // count-2  保证 line的起始点为点集合中的倒数第二个点。
                    l.Y1 = disList[count - 2].Y;
                    // 终点X,Y 为当前point的X,Y
                    l.X2 = point.X;
                    l.Y2 = point.Y;
                    myCanvas.Children.Add(l);
                }
            }
        }
        /// <summary>
        /// 选择style
        /// </summary>
        private void cboStyle_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string style = (cboStyle.SelectedItem as ComboBoxItem).Content as string;
            if (this.myCanvas == null)
            {
                return;
            }
            List<Line> list = GetChildObjects<Line>(this.myCanvas);
            if (list.Count > 0)
            {
                list.ForEach(l =>
                {
                    if (style == "默认")
                    {
                        l.StrokeDashArray = new DoubleCollection(new List<double>() { });
                    }
                    if (style == "虚线")
                    {
                        l.StrokeDashArray = new DoubleCollection(new List<double>() {
                     1,1,1,1
                    });
                    }
                });
                list.Clear();
            }
        }

        /// <summary>
        /// 选择颜色
        /// </summary>
        private void cboColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string color = (cboColor.SelectedItem as ComboBoxItem).Content as string;
            if (this.myCanvas != null)
            {

                List<Line> list = GetChildObjects<Line>(this.myCanvas);
                if (list.Count > 0)
                {
                    list.ForEach(l =>
                    {
                        if (color == "默认")
                        {
                            l.Stroke = Brushes.Black;
                        }
                        if (color == "红色")
                        {
                            l.Stroke = Brushes.Red;
                        }
                        if (color == "绿色")
                        {
                            l.Stroke = Brushes.Green;
                        }
                    });
                    list.Clear();
                }
            }
        }

        private List<T> GetChildObjects<T>(Canvas myCanvas)
        {
            List<T> childs = new List<T>();
            foreach(var x in myCanvas.Children)
            {
                if(x is T)
                {
                    childs.Add((T)x);
                }
            }
            return childs;

        }
    }
}
