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
    /// CtrlDrawding.xaml 的交互逻辑
    /// </summary>
    public partial class CtrlDrawding : UserControl
    {
        public CtrlDrawding()
        {
            InitializeComponent();
        }
       
       
        
    }

    public class MyVisualHost : FrameworkElement
    {
        private VisualCollection children;

        public MyVisualHost()
        {
            children = new VisualCollection(this);

            var visual = new DrawingVisual();
            children.Add(visual);

            using (var dc = visual.RenderOpen())
            {
                dc.DrawLine(new Pen(Brushes.Black, 1), new Point(0, 0), new Point(400, 400));
                dc.DrawLine(new Pen(Brushes.Black, 1), new Point(0, 400), new Point(400, 0));
            }
        }

        protected override int VisualChildrenCount
        {
            get { return children.Count; }
        }

        protected override Visual GetVisualChild(int index)
        {
            if (index < 0 || index >= children.Count)
            {
                throw new ArgumentOutOfRangeException();
            }

            return children[index];
        }
    }

    
    public class MyVisualHost2 : FrameworkElement
    {
        private VisualCollection visualCollection;

    public MyVisualHost2()
        {
            visualCollection = new VisualCollection(this);
            CreateDrawingVisualRectangle();
        }
        private DrawingVisual CreateDrawingVisualRectangle()
        {
            DrawingVisual drawingVisual = new DrawingVisual();
            visualCollection.Add(drawingVisual);

            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                Rect rect = new Rect(new Point(100, 10), new Size(320, 80));
                drawingContext.DrawRectangle(Brushes.Red, (Pen)null, rect);
            }
            return drawingVisual;
        }
        //必须重载这两个方法，不然是画不出来的
        // 重载自己的VisualTree的孩子的个数，由于只有一个DrawingVisual，返回1
        protected override int VisualChildrenCount
        {
            get { return 1; }
        }
 
        protected override Visual GetVisualChild(int index)
        {
            if (index == 0)
                return visualCollection[index];

            throw new IndexOutOfRangeException();
        }
 

    }
}
