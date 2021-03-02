
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfGDI
{
    /// <summary>
    /// 功能描述    ：FontItem  
    /// 创 建 者    ：panwangji
    /// 创建日期    ：2021/3/2 17:07:01 
    /// 最后修改者  ：panwangji
    /// 最后修改日期：2021/3/2 17:07:01 
    /// </summary>

    public class FontItem
    {
        private string text;                // 字体内容
        private string fontName;            // 字体名称
        private int fontSize;               // 字体大小
        private Color fontColor;            // 字体颜色

        public string Text
        {
            get
            {
                return text;
            }

            set
            {
                text = value;
            }
        }

        public string FontName
        {
            get
            {
                return fontName;
            }

            set
            {
                fontName = value;
            }
        }

        public int FontSize
        {
            get
            {
                return fontSize;
            }

            set
            {
                fontSize = value;
            }
        }

        public Color FontColor
        {
            get
            {
                return fontColor;
            }

            set
            {
                fontColor = value;
            }
        }
        private Color gradientColor1;       // 渐变颜色1
        private Color gradientColor2;       // 渐变颜色2
        private BitmapImage overlayImage;   // 叠加图片
        private Color strokeColor;          // 描边颜色
        private int strokeColorLength;      // 描边深度

        public Color GradientColor1
        {
            get
            {
                return gradientColor1;
            }

            set
            {
                gradientColor1 = value;
            }
        }

        public Color GradientColor2
        {
            get
            {
                return gradientColor2;
            }

            set
            {
                gradientColor2 = value;
            }
        }

        public BitmapImage OverlayImage
        {
            get
            {
                return overlayImage;
            }

            set
            {
                overlayImage = value;
            }
        }

        public Color StrokeColor
        {
            get
            {
                return strokeColor;
            }

            set
            {
                strokeColor = value;
            }
        }

        public int StrokeColorLength
        {
            get
            {
                return strokeColorLength;
            }

            set
            {
                strokeColorLength = value;
            }
        }

    }
}
