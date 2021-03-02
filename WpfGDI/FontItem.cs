
using System.Windows.Media;

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

    }
}
