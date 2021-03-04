using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfGDI
{
    /// <summary>
    /// 功能描述    ：ImageProcessingEffect  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/3/4 14:48:49 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/3/4 14:48:49 
    /// </summary>
    //图片处理类型枚举
    public enum ImageProcessingEffect
    {
        Normal = 0,//原始图片
        Emboss = 1,//浮雕
        Sharpening = 2,//锐化
        BlackAndWhite = 3,//黑白
        MirrorHorizontal = 4,//水平镜像
        MirrorVertical = 5,//垂直镜像
        CenterRotate = 6,//中心旋转
        Mask = 7//蒙板
    }

    /// <summary>
    /// 拖动改变类型
    /// </summary>
    public enum ChangeType
    {
        Move = 0,// 移动
        ChangeLeftTop = 1,//左上
        ChangeRightTop = 2,//右上
        ChangeRightBottom = 3,//右下
        ChangeLeftBottom = 4,//左下
        Other = 5//其他
    }
}
