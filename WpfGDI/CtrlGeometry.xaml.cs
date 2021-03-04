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
    /// CtrlGeometry.xaml 的交互逻辑
    /// </summary>
    public partial class CtrlGeometry : UserControl
    {
        public CtrlGeometry()
        {
            InitializeComponent();
        }
        /** 
        <Path Stroke="Black" Data="M 100,240 C 510,300 80,100 300,160 H40 v80" />的形式是StreamGeometry的XAML代码表示形式，也是最简洁的表示形式。
绘制指令格式语法:

(1) 直线:Line(L)
格式：L 结束点坐标 或: l 结束点坐标。
比如：L 100,100 或 l 100 100。坐标值可以使用x,y（中间用英文逗号隔开）或x y（中间用半角空格隔开）的形式。

(2) 水平直线  Horizontal line(H)：绘制从当前点到指定x坐标的直线。
格式：H x值 或 h x值(x为System.Double类型的值)
比如：H 100或h 100，也可以是：H 100.00或h 100.00等形式。

(3) 垂直直线 Vertical line(V)：绘制从当前点到指定y坐标的直线。
格式：V y值 或 v y值(y为System.Double类型的值)
比如：V 100或y 100，也可以是：V 100.00或v 100.00等形式。

(4) 三次方程式贝塞尔曲线 Cubic Bezier curve(C)：通过指定两个控制点来绘制由当前点到指定结束点间的三次方程贝塞尔曲线。
格式：C 第一控制点 第二控制点 结束点 或 c 第一控制点 第二控制点 结束点
比如：C 100,200 200,400 300,200 或 c 100,200 200,400 300,200
其中，点(100,200)为第一控制点，点(200,400)为第二控制点，点(300,200)为结束点。

(5) 二次方程式贝塞尔曲线 Quadratic Bezier curve(Q)：通过指定的一个控制点来绘制由当前点到指定结束点间的二次方程贝塞尔曲线。
格式：Q 控制点 结束点 或 q 控制点 结束点
比如：q 100,200 300,200。其中，点(100,200)为控制点，点(300,200)为结束点。

(6) 平滑三次方程式贝塞尔曲线: Smooth cubic Bezier curve(S)：通过一个指定点来“平滑地”控制当前点到指定点的贝塞尔曲线。
格式：S 控制点 结束点 或 s 控制点 结束点
比如：S 100,200 200,300

(7) 平滑二次方程式贝塞尔曲线 smooth quadratic Bezier curve(T)：与平滑三次方程贝塞尔曲线类似。
格式：T 控制点 结束点 或 t 控制点 结束点
比如：T 100,200 200,300

(8) 椭圆圆弧: elliptical Arc(A) : 在当前点与指定结束点间绘制圆弧。
A 尺寸 圆弧旋转角度值 优势弧的标记 正负角度标记 结束点
或：
a 尺寸 圆弧旋转角度值 优势弧的标记 正负角度标记 结束点
尺寸(Size): System.Windows.Size类型，指定椭圆圆弧X,Y方向上的半径值。
旋转角度（rotationAngle）：System.Double类型。
圆弧旋转角度值（rotationAngle）：椭圆弧的旋转角度值。
优势弧的标记（isLargeArcFlag）：是否为优势弧，如果弧的角度大于等于180度，则设为1，否则为0。
正负角度标记（sweepDirectionFlag）：当正角方向绘制时设为1，否则为0。
结束点（endPoint）：System.Windows.Point类型。

3. 关闭指令(close Command):用以将图形的首、尾点用直线连接，以形成一个封闭的区域。
用Z或z表示
————————————————
版权声明：本文为CSDN博主「jisuanjixu」的原创文章，遵循CC 4.0 BY-SA版权协议，转载请附上原文出处链接及本声明。
原文链接：https://blog.csdn.net/jisuanjixu/article/details/5457216
        **/
    }
}
