using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfGDI
{
    /// <summary>
    /// 功能描述    ：MyImage  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/3/4 15:20:10 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/3/4 15:20:10 
    /// </summary>
    public partial class MyImage : System.Windows.Controls.Image
    {
      

        public ChangeType changeType;//变换类型
        private bool isEnter = false;//是否鼠标进入
        private bool isCanDragSize = false;//是否可以改变大小
        private Rect LTrect, LBrect, RTrect, RBrect;//左上、左下、右上、右下四个方向的矩形
        public bool IsSelect;//是否选中
        /// <summary>
        /// 重绘
        /// </summary>
        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);
            if (IsSelect)
            {
                try
                {
                    //绘制边框
                    Rect rect = new Rect(0, 0, this.Width, this.Height);
                    Pen p = new Pen(new SolidColorBrush(Colors.Red), 2);
                    p.DashStyle = DashStyles.Solid;
                    p.StartLineCap = PenLineCap.Triangle;
                    dc.DrawRectangle(Brushes.Transparent, p, rect);

                    //在4个角画出对应的表示方向的小方块
                    dc.DrawRectangle(new SolidColorBrush(Colors.Red), p, new Rect(0, 0, 5, 5));
                    dc.DrawRectangle(new SolidColorBrush(Colors.Red), p, new Rect(this.Width - 5, 0, 5, 5));
                    dc.DrawRectangle(new SolidColorBrush(Colors.Red), p, new Rect(0, this.Height - 5, 5, 5));
                    dc.DrawRectangle(new SolidColorBrush(Colors.Red), p, new Rect(this.Width - 5, this.Height - 5, 5, 5));
                }
                catch { }
            }
        }

        public MyImage()
        {
            this.Stretch = Stretch.Fill;//需要设置Stretch属性，避免拖拽改变宽高时出现异常
            this.MouseLeftButtonDown += MyImage_MouseLeftButtonDown;
            this.MouseLeftButtonUp += MyImage_MouseLeftButtonUp;
            this.MouseEnter += MyImage_MouseEnter;
            this.MouseLeave += MyImage_MouseLeave;
            this.MouseMove += MyImage_MouseMove;
        }

        /// <summary>
        /// 鼠标按下
        /// </summary>
        private void MyImage_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //获取当前坐标
            Point p = e.GetPosition(this);
            //获取四个方向的矩形
            GetESWNRect();
            //获取位置
            int x = (int)Canvas.GetLeft(this);
            int y = (int)Canvas.GetTop(this);

            isCanDragSize = true;
            //左上
            if (IsInThis(p.X, p.Y, LTrect))
            {
                this.Cursor = Cursors.SizeNWSE;
                changeType = ChangeType.ChangeLeftTop;
            }
            //右上
            else if (IsInThis(p.X, p.Y, RTrect))
            {
                this.Cursor = Cursors.SizeNESW;
                changeType = ChangeType.ChangeRightTop;
            }
            //左下
            else if (IsInThis(p.X, p.Y, LBrect))
            {
                this.Cursor = Cursors.SizeNESW;
                changeType = ChangeType.ChangeLeftBottom;
            }
            //右下
            else if (IsInThis(p.X, p.Y, RBrect))
            {
                this.Cursor = Cursors.SizeNWSE;
                changeType = ChangeType.ChangeRightBottom;
            }
            //中心
            else if (p.X > 5 && p.X < this.Width - 5 && p.Y > 5 && p.Y < this.Height - 5)
            {
                this.Cursor = Cursors.SizeAll;
                changeType = ChangeType.Move;
            }
            else
            {
                this.Cursor = Cursors.Arrow;
            }
        }
        /// <summary>
        /// 鼠标抬起
        /// </summary>
        private void MyImage_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            isCanDragSize = false;
        }
        /// <summary>
        /// 鼠标进入
        /// </summary>
        private void MyImage_MouseEnter(object sender, MouseEventArgs e)
        {
            isEnter = true;
            this.Cursor = Cursors.SizeAll;
            this.InvalidateVisual();
        }
        /// <summary>
        /// 鼠标移出
        /// </summary>
        private void MyImage_MouseLeave(object sender, MouseEventArgs e)
        {
            isEnter = false;
            this.Cursor = Cursors.No;
            this.InvalidateVisual();
        }
        /// <summary>
        /// 鼠标移动
        /// </summary>
        private void MyImage_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isEnter)
                return;

            //获取鼠标移动中的坐标
            Point p = e.GetPosition(this);
            //获取四个方向的矩形
            GetESWNRect();
            //获取当前位置
            int x = (int)Canvas.GetLeft(this);
            int y = (int)Canvas.GetTop(this);

            if (!isCanDragSize)
            {
                //左上
                if (IsInThis(p.X, p.Y, LTrect))
                {
                    this.Cursor = Cursors.SizeNWSE;
                    changeType = ChangeType.ChangeLeftTop;
                }
                //右上
                else if (IsInThis(p.X, p.Y, RTrect))
                {
                    this.Cursor = Cursors.SizeNESW;
                    changeType = ChangeType.ChangeRightTop;
                }
                //左下
                else if (IsInThis(p.X, p.Y, LBrect))
                {
                    this.Cursor = Cursors.SizeNESW;
                    changeType = ChangeType.ChangeLeftBottom;
                }
                //右下
                else if (IsInThis(p.X, p.Y, RBrect))
                {
                    this.Cursor = Cursors.SizeNWSE;
                    changeType = ChangeType.ChangeRightBottom;
                }
                //中心
                else if (p.X > 5 && p.X < this.Width - 5 && p.Y > 5 && p.Y < this.Height - 5)
                {
                    this.Cursor = Cursors.SizeAll;
                    changeType = ChangeType.Move;
                }
                else
                {
                    this.Cursor = Cursors.Arrow;
                }
                return;
            }
        }
        /// <summary>
	    /// 指定的坐标是否在矩形里
	    /// </summary>
	    public bool IsInThis(double x, double y, Rect rect)
        {
            return x >= rect.X && x <= rect.X + rect.Width && y >= rect.Y && y <= rect.Y + rect.Height;
        }
        /// <summary>
        /// 获得4个方向的矩形
        /// </summary>
        public void GetESWNRect()
        {
            double x = Canvas.GetLeft(this);
            double y = Canvas.GetTop(this);
            int localArea = 5;//单个角落方块区域的大小

            LTrect = new Rect(0, 0, localArea, localArea);
            LBrect = new Rect(0, (this.Height - localArea) <= 0 ? 0 : (this.Height - localArea), localArea, localArea);
            RTrect = new Rect((this.Width - localArea) <= 0 ? 0 : (this.Width - localArea), 0, localArea, localArea);
            RBrect = new Rect((this.Width - localArea) <= 0 ? 0 : (this.Width - localArea), (this.Height - localArea) <= 0 ? 0 : (this.Height - localArea), localArea, localArea);
        }
    }
}
