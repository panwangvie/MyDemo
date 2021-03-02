using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfGDI
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private FontItem fontItem;
        public MainWindow()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 加载本地字体
        /// </summary>
        private void InitFontName()
        {
            try
            {
                InstalledFontCollection insFont = new InstalledFontCollection();
                System.Drawing.FontFamily[] families = insFont.Families;
                foreach (System.Drawing.FontFamily family in families)
                {
                    this.cbFontName.Items.Add(family.Name);
                }
                this.cbFontName.SelectedItem = "宋体";
            }
            catch (Exception)
            {

            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InitFontName();
        }

     
        /// <summary>
        /// 文本改变
        /// </summary>
        private void tbFontText_TextChanged(object sender, TextChangedEventArgs e)
        {
            GetFontItem(this.tbFontText.Text.Trim());
            DrawText();
        }
        /// <summary>
        /// 设置需要绘制的字体
        /// </summary>
        private void GetFontItem(string text)
        {
            if (text.Length <= 0)
            {
                fontItem = null;
                return;
            }
            fontItem = new FontItem();
            fontItem.FontColor = ((SolidColorBrush)this.rFontColor.Fill).Color;
            fontItem.FontName = this.cbFontName.SelectedValue.ToString();
            fontItem.FontSize = (int)this.numFontSize.Value;
            fontItem.Text = text;
        }
        /// <summary>
        /// 绘制字体
        /// </summary>
        private void DrawText()
        {
            try
            {
                if (fontItem == null)
                {
                    this.imgFont.Source = null;
                    return;
                }

                System.Drawing.Font fontText = new System.Drawing.Font(fontItem.FontName, fontItem.FontSize);
                System.Drawing.Size sizeText = System.Windows.Forms.TextRenderer.MeasureText(fontItem.Text, fontText, new System.Drawing.Size(0, 0), System.Windows.Forms.TextFormatFlags.NoPadding);
                Rect viewport = new Rect(0, 0, sizeText.Width, sizeText.Height);

                if ((int)viewport.Width == 0 || (int)viewport.Height == 0)
                    return;

                System.Drawing.Bitmap tempMap = new System.Drawing.Bitmap((int)viewport.Width, (int)viewport.Height);
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(tempMap);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                System.Drawing.RectangleF rect = new System.Drawing.RectangleF(0, 0, sizeText.Width, sizeText.Height);
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddString(fontItem.Text, fontText.FontFamily, (int)fontText.Style, fontText.Size, rect, System.Drawing.StringFormat.GenericDefault);
                g.FillPath(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(fontItem.FontColor.A, fontItem.FontColor.R, fontItem.FontColor.G, fontItem.FontColor.B)), path);
                path.Dispose();

                BitmapImage tempImage = BitmapToBitmapImage(tempMap, System.Drawing.Imaging.ImageFormat.Png);
                
                g.Dispose();
                tempMap.Dispose();

                if (tempImage != null)
                {
                    this.imgFont.Source = tempImage;
                    this.imgFont.Width = tempImage.Width;
                    this.imgFont.Height = tempImage.Height;
                    Canvas.SetLeft(this.imgFont, (this.mainCanvas.ActualWidth - tempImage.Width) / 2);
                    Canvas.SetTop(this.imgFont, (this.mainCanvas.ActualHeight - tempImage.Height) / 2);
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }
        /// <summary>
        /// 设置需要绘制的字体
        /// </summary>
        private void GetFontItem2(string text)
        {
            if (text.Length <= 0)
            {
                fontItem = null;
                return;
            }
            fontItem = new FontItem();
            fontItem.FontColor = ((SolidColorBrush)this.rStrokeColor.Fill).Color;
            fontItem.FontName = this.cbFontName.SelectedValue.ToString();
            fontItem.FontSize = (int)this.numFontSize.Value;
            fontItem.Text = text;
        }
        /// <summary>
        /// 绘制字体
        /// </summary>
        private void DrawText2()
        {
            try
            {
                if (fontItem == null)
                {
                    this.imgFont.Source = null;
                    return;
                }

                System.Drawing.Font fontText = new System.Drawing.Font(fontItem.FontName, fontItem.FontSize);
                System.Drawing.Size sizeText = System.Windows.Forms.TextRenderer.MeasureText(fontItem.Text, fontText, new System.Drawing.Size(0, 0), System.Windows.Forms.TextFormatFlags.NoPadding);
                Rect viewport = new Rect(0, 0, sizeText.Width, sizeText.Height);

                if ((int)viewport.Width == 0 || (int)viewport.Height == 0)
                    return;

                System.Drawing.Bitmap tempMap = new System.Drawing.Bitmap((int)viewport.Width, (int)viewport.Height);
                System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(tempMap);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                System.Drawing.RectangleF rect = new System.Drawing.RectangleF(0, 0, sizeText.Width, sizeText.Height);
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                path.AddString(fontItem.Text, fontText.FontFamily, (int)fontText.Style, fontText.Size, rect, System.Drawing.StringFormat.GenericDefault);

                //描边
                g.DrawPath(new System.Drawing.Pen(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(fontItem.StrokeColor.A, fontItem.StrokeColor.R, fontItem.StrokeColor.G, fontItem.StrokeColor.B)), fontItem.StrokeColorLength), path);
                //颜色
                g.FillPath(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(fontItem.FontColor.A, fontItem.FontColor.R, fontItem.FontColor.G, fontItem.FontColor.B)), path);
                //渐变
                g.FillPath(new System.Drawing.Drawing2D.LinearGradientBrush(rect, System.Drawing.Color.FromArgb(fontItem.GradientColor1.A, fontItem.GradientColor1.R, fontItem.GradientColor1.G, fontItem.GradientColor1.B), System.Drawing.Color.FromArgb(fontItem.GradientColor2.A, fontItem.GradientColor2.R, fontItem.GradientColor2.G, fontItem.GradientColor2.B), System.Drawing.Drawing2D.LinearGradientMode.Vertical), path);
                //图片叠加
                if (fontItem.OverlayImage != null)
                {
                    System.Drawing.TextureBrush brush = new System.Drawing.TextureBrush(BitmapImageToImage(fontItem.OverlayImage), System.Drawing.Drawing2D.WrapMode.TileFlipXY);//可改变渐变方式
                    g.FillPath(brush, path);
                }

                path.Dispose();

                BitmapImage tempImage =  BitmapToBitmapImage(tempMap, System.Drawing.Imaging.ImageFormat.Png);
                g.Dispose();
                tempMap.Dispose();

                if (tempImage != null)
                {
                    this.imgFont.Source = tempImage;
                    this.imgFont.Width = tempImage.Width;
                    this.imgFont.Height = tempImage.Height;
                    Canvas.SetLeft(this.imgFont, (this.mainCanvas.ActualWidth - tempImage.Width) / 2);
                    Canvas.SetTop(this.imgFont, (this.mainCanvas.ActualHeight - tempImage.Height) / 2);
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }


        public static BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap ImageOriginal, System.Drawing.Imaging.ImageFormat format)
        {

            System.Drawing.Bitmap ImageOriginalBase = new Bitmap(ImageOriginal);
            BitmapImage bitmapImage = new BitmapImage();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                //ImageOriginalBase.Save(ms, ImageOriginalBase.RawFormat);
                ImageOriginalBase.Save(ms, format);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
           
            return bitmapImage;
        }
        public static BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap ImageOriginal )
        {

            System.Drawing.Bitmap ImageOriginalBase = new Bitmap(ImageOriginal);
            BitmapImage bitmapImage = new BitmapImage();
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                //ImageOriginalBase.Save(ms, ImageOriginalBase.RawFormat);
                ImageOriginalBase.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                bitmapImage.BeginInit();
                bitmapImage.StreamSource = ms;
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.EndInit();
                bitmapImage.Freeze();
            }
            return bitmapImage;
        }

        public static BitmapImage LoadBitmapImageByPath(string path)
        {
            BitmapImage bitmapImage = new BitmapImage();
            return bitmapImage;
        }

        public static System.Drawing.Image BitmapImageToImage(BitmapImage bitmapImage)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile("");
            return image;
        } 

        /// <summary>
        /// 字体选择
        /// </summary>
        private void cbFontName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (fontItem != null)
            {
                fontItem.FontName = this.cbFontName.SelectedValue.ToString();
                DrawText();
            }
        }
        /// <summary>
        /// 字体大小
        /// </summary>
        private void numFontSize_ValueChanged(object sender, EventArgs e)
        {
            if (fontItem != null)
            {
                fontItem.FontSize = (int)this.numFontSize.Value;
                DrawText();
            }
        }
        /// <summary>
        /// 字体颜色
        /// </summary>
        private void rFontColor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ColorSelectorWindow csw = new ColorSelectorWindow();
            csw.ShowDialog();
            if (fontItem != null)
            {
                fontItem.FontColor = csw.returnSelectColor;
                this.rFontColor.Fill = new SolidColorBrush(csw.returnSelectColor);
                DrawText();
            }
        }
        /// <summary>
        /// 描边颜色
        /// </summary>
        private void rStrokeColor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ColorSelectorWindow csw = new ColorSelectorWindow();
            csw.ShowDialog();
            if (fontItem != null)
            {
                fontItem.StrokeColor = csw.returnSelectColor;
                this.rStrokeColor.Fill = new SolidColorBrush(csw.returnSelectColor);
                DrawText();
            }
        }
        /// <summary>
        /// 描边深度
        /// </summary>
        private void numStrokeLength_ValueChanged(object sender, EventArgs e)
        {
            if (fontItem != null)
            {
                fontItem.StrokeColorLength = (int)this.numStrokeLength.Value;
                DrawText();
            }
        }
        /// <summary>
        /// 渐变起始颜色
        /// </summary>
        private void rGradientColor1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ColorSelectorWindow csw = new ColorSelectorWindow();
            csw.ShowDialog();
            if (fontItem != null)
            {
                fontItem.GradientColor1 = csw.returnSelectColor;
                this.rGradientColor1.Fill = new SolidColorBrush(csw.returnSelectColor);
                DrawText();
            }
        }
        /// <summary>
        /// 渐变结束颜色
        /// </summary>
        private void rGradientColor2_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ColorSelectorWindow csw = new ColorSelectorWindow();
            csw.ShowDialog();
            if (fontItem != null)
            {
                fontItem.GradientColor2 = csw.returnSelectColor;
                this.rGradientColor2.Fill = new SolidColorBrush(csw.returnSelectColor);
                DrawText();
            }
        }
        /// <summary>
        /// 图片叠加
        /// </summary>
        private void btnOverlayImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择图片";
            openFileDialog.Filter = "图片(*.jpg,*.png,*.bmp)|*.jpg;*.png;*.bmp";
            openFileDialog.FileName = string.Empty;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BitmapImage bi = LoadBitmapImageByPath(openFileDialog.FileName);
                if (bi != null)
                {
                    if (fontItem != null)
                    {
                        fontItem.OverlayImage = bi;
                        DrawText();
                    }
                }
            }
        }
    }
}
