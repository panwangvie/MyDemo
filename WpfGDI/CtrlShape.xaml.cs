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

namespace WpfGDI
{
    /// <summary>
    /// CtrlShape.xaml 的交互逻辑
    /// </summary>
    public partial class CtrlShape : UserControl
    {
        public CtrlShape()
        {
            InitializeComponent();
            Polyline myPolyline = new Polyline();
            myPolyline.Stroke = System.Windows.Media.Brushes.SlateGray;
            myPolyline.StrokeThickness = 2;
            myPolyline.FillRule = FillRule.EvenOdd;
            System.Windows.Point Point4 = new System.Windows.Point(1, 50);
            System.Windows.Point Point5 = new System.Windows.Point(10, 80);
            System.Windows.Point Point6 = new System.Windows.Point(20, 40);
            PointCollection myPointCollection2 = new PointCollection();
            myPointCollection2.Add(Point4);
            myPointCollection2.Add(Point5);
            myPointCollection2.Add(Point6);
            myPolyline.Points = myPointCollection2;
            this.myGrid.Children.Add(myPolyline);

            //Add the Polygon Element
            Polygon myPolygon = new Polygon();
            myPolygon.Stroke = System.Windows.Media.Brushes.Black;
            myPolygon.Fill = System.Windows.Media.Brushes.LightSeaGreen;
            myPolygon.StrokeThickness = 2;
            myPolygon.HorizontalAlignment = HorizontalAlignment.Left;
            myPolygon.VerticalAlignment = VerticalAlignment.Center;
            System.Windows.Point Point1 = new System.Windows.Point(1, 50);
            System.Windows.Point Point2 = new System.Windows.Point(10, 80);
            System.Windows.Point Point3 = new System.Windows.Point(50, 50);
            PointCollection myPointCollection = new PointCollection();
            myPointCollection.Add(Point1);
            myPointCollection.Add(Point2);
            myPointCollection.Add(Point3);
            myPolygon.Points = myPointCollection;
            this.myGrid.Children.Add(myPolygon);

            //Add the Path Element
            Path myPath = new Path();
            myPath.Stroke = System.Windows.Media.Brushes.Black;
            myPath.Fill = System.Windows.Media.Brushes.MediumSlateBlue;
            myPath.StrokeThickness = 4;
            myPath.HorizontalAlignment = HorizontalAlignment.Left;
            myPath.VerticalAlignment = VerticalAlignment.Center;
            EllipseGeometry myEllipseGeometry = new EllipseGeometry();
            myEllipseGeometry.Center = new System.Windows.Point(50, 50);
            myEllipseGeometry.RadiusX = 25;
            myEllipseGeometry.RadiusY = 25;
            myPath.Data = myEllipseGeometry;
            myPath.Margin = new Thickness(20, 100,0,0);
            myGrid.Children.Add(myPath);

            // Add a Rectangle Element
            Rectangle myRect = new System.Windows.Shapes.Rectangle();
            myRect.Stroke = System.Windows.Media.Brushes.Black;
            myRect.Fill = System.Windows.Media.Brushes.SkyBlue;
            myRect.HorizontalAlignment = HorizontalAlignment.Left;
            myRect.VerticalAlignment = VerticalAlignment.Center;
            myRect.Height = 50;
            myRect.Width = 50;
            myRect.Margin = new Thickness(20, 200, 0, 0);
            myGrid.Children.Add(myRect);
        }

    }
}
