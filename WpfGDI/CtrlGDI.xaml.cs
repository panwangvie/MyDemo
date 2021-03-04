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
using Color = System.Windows.Media.Color;
using Point = System.Windows.Point;
using Size = System.Windows.Size;
using UserControl = System.Windows.Controls.UserControl;

namespace WpfGDI
{
    /// <summary>
    /// CtrlGDI.xaml 的交互逻辑
    /// </summary>
    public partial class CtrlGDI : UserControl
    {
        public CtrlGDI()
        {
            InitializeComponent();
        }

        private FontItem fontItem;

        /// <summary>
        /// 用于记录原始图像源
        /// </summary>
        private BitmapImage imgSource;

        private Point pBefore = new Point();//鼠标点击前坐标
        private Point eBefore = new Point();//图片移动前坐标     
        private double wBefore;//图片改变大小前宽
        private double hBefore;//图片改变大小前高
        private int minImgValue = 10;//图片最小宽高
        private bool IsCanMove = false;//是否可以移动
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

#if region
            /// <summary>
            /// 设置需要绘制的字体
            /// </summary>
            private void GetFontItem1(string text)
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
        private void DrawText1()
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
#endif
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
            fontItem.FontColor = ((SolidColorBrush)this.rStrokeColor.Fill).Color;
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
        /// 
        /// </summary>
        /// <param name="ImageOriginal"></param>
        /// <param name="format"></param>
        /// <returns></returns>
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
        public static BitmapImage BitmapToBitmapImage(System.Drawing.Bitmap ImageOriginal)
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">路径</param>
        /// <returns></returns>
        public static BitmapImage LoadBitmapImageByPath(string path)
        {

            BitmapImage image = new BitmapImage();
            image.BeginInit();
            image.UriSource = new Uri(path);
            image.EndInit();

            return image;
        }

        public static System.Drawing.Image BitmapImageToImage(BitmapImage bitmapImage)
        {
            System.Drawing.Image image = System.Drawing.Image.FromFile(bitmapImage.UriSource.LocalPath);
            return image;
        }


        /// <summary>
        /// 绘制图像
        /// </summary>
        private void DrawImage(ImageProcessingEffect imgEffect)
        {
            if (this.imgSource == null) return;

            BitmapImage bitmap = new BitmapImage();
            switch (imgEffect)
            {
                case ImageProcessingEffect.BlackAndWhite://黑白
                    bitmap = BlackAndWhite();
                    this.imgPic.Source = bitmap;
                    break;
                case ImageProcessingEffect.Emboss://浮雕
                    bitmap = Emboss();
                    this.imgPic.Source = bitmap;
                    break;
                case ImageProcessingEffect.Sharpening://锐化
                    bitmap = Sharpening();
                    this.imgPic.Source = bitmap;
                    break;
                case ImageProcessingEffect.MirrorHorizontal://水平镜像
                    bitmap = MirrorHorizontal();
                    this.imgPic.Source = bitmap;
                    break;
                case ImageProcessingEffect.MirrorVertical://垂直镜像
                    bitmap = MirrorVertical();
                    this.imgPic.Source = bitmap;
                    break;
                case ImageProcessingEffect.CenterRotate://中心旋转
                    bitmap = CenterRotate((int)this.numAngle.Value);
                    this.imgPic.Source = bitmap;
                    break;
                case ImageProcessingEffect.Mask://蒙板
                    bitmap = Mask(((SolidColorBrush)this.rPicMaskColor.Fill).Color);
                    this.imgPic.Source = bitmap;
                    break;
                default:
                    break;
            }
            if (bitmap != null)
            {
                this.imgPic.Width = bitmap.PixelWidth;
                this.imgPic.Height = bitmap.PixelHeight;
                Canvas.SetLeft(this.imgPic, (this.mainCanvas.ActualWidth - bitmap.PixelWidth) / 2);
                Canvas.SetTop(this.imgPic, (this.mainCanvas.ActualHeight - bitmap.PixelHeight) / 2);
            }
        }
        /// <summary>
        /// 黑白
        /// </summary>
        public BitmapImage BlackAndWhite(int opacity = 255)
        {
            try
            {
                System.Drawing.Bitmap bmap = new System.Drawing.Bitmap(this.imgSource.UriSource.LocalPath);
                int height = bmap.Height;
                int width = bmap.Width;
                System.Drawing.Color pixel;
                for (int x = 0; x < width; x++)
                {
                    for (int y = 0; y < height; y++)
                    {
                        pixel = bmap.GetPixel(x, y);
                        int r, g, b, a = 0, Result = 0;
                        r = pixel.R;
                        g = pixel.G;
                        b = pixel.B;
                        int iType = 2;//这里选取加权平均值法产生黑白图像
                        switch (iType)
                        {
                            case 0://平均值法
                                Result = ((r + g + b) / 3);
                                break;
                            case 1://最大值法
                                Result = r > g ? r : g;
                                Result = Result > b ? Result : b;
                                break;
                            case 2://加权平均值法
                                Result = ((int)(0.7 * r) + (int)(0.2 * g) + (int)(0.1 * b));
                                break;
                        }
                        if (opacity < pixel.A)
                        {
                            a = opacity;
                        }
                        else
                        {
                            a = pixel.A;
                        }
                        bmap.SetPixel(x, y, System.Drawing.Color.FromArgb(a, Result, Result, Result));
                    }
                }
                return  BitmapToBitmapImage(bmap, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception)
            {
                return this.imgSource;
            }
        }
        /// <summary>
        /// 浮雕
        /// </summary>
        public BitmapImage Emboss(int opacity = 255)
        {
            try
            {
                int o = opacity;
                System.Drawing.Bitmap bmap = new System.Drawing.Bitmap(this.imgSource.UriSource.LocalPath);
                System.Drawing.Color pixel1, pixel2;
                int width = bmap.Width;
                int height = bmap.Height;
                for (int x = 0; x < width - 1; x++)
                {
                    for (int y = 0; y < height - 1; y++)
                    {
                        int r = 0, g = 0, b = 0, a = 0;
                        pixel1 = bmap.GetPixel(x, y);
                        pixel2 = bmap.GetPixel(x + 1, y + 1);

                        r = Math.Abs(pixel1.R - pixel2.R + 128);
                        g = Math.Abs(pixel1.G - pixel2.G + 128);
                        b = Math.Abs(pixel1.B - pixel2.B + 128);
                        a = pixel1.A;
                        if (a == 0)
                        {
                            opacity = a;
                        }
                        else
                        {
                            opacity = o;
                        }
                        if (r > 255)
                            r = 255;
                        if (r < 0)
                            r = 0;
                        if (g > 255)
                            g = 255;
                        if (g < 0)
                            g = 0;
                        if (b > 255)
                            b = 255;
                        if (b < 0)
                            b = 0;
                        bmap.SetPixel(x, y, System.Drawing.Color.FromArgb(opacity, r, g, b));
                    }
                }
                return BitmapToBitmapImage(bmap, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception)
            {
                return this.imgSource;
            }
        }
        /// <summary>
        /// 锐化
        /// </summary>
        public BitmapImage Sharpening(int opacity = 255)
        {
            try
            {
                System.Drawing.Bitmap bmap = new System.Drawing.Bitmap(this.imgSource.UriSource.LocalPath);
                int height = bmap.Height;
                int width = bmap.Width;
                System.Drawing.Color pixel;
                //拉普拉斯模板
                int[] Laplacian = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };
                for (int x = 1; x < width - 1; x++)
                {
                    for (int y = 1; y < height - 1; y++)
                    {
                        int r = 0, g = 0, b = 0, a = 0;
                        int Index = 0;
                        for (int col = -1; col <= 1; col++)
                            for (int row = -1; row <= 1; row++)
                            {
                                pixel = bmap.GetPixel(x + row, y + col);
                                r += pixel.R * Laplacian[Index];
                                g += pixel.G * Laplacian[Index];
                                b += pixel.B * Laplacian[Index];
                                Index++;
                                if (pixel.A < opacity)
                                {
                                    a = pixel.A;
                                }
                                else
                                {
                                    a = opacity;
                                }
                            }
                        //处理颜色值溢出
                        r = r > 255 ? 255 : r;
                        r = r < 0 ? 0 : r;
                        g = g > 255 ? 255 : g;
                        g = g < 0 ? 0 : g;
                        b = b > 255 ? 255 : b;
                        b = b < 0 ? 0 : b;

                        bmap.SetPixel(x - 1, y - 1, System.Drawing.Color.FromArgb(a, r, g, b));
                    }
                }
                return BitmapToBitmapImage(bmap, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception)
            {
                return this.imgSource;
            }
        }

        /// <summary>
        /// 水平镜像
        /// </summary>
        private BitmapImage MirrorHorizontal()
        {
            try
            {
                System.Drawing.Bitmap tempImage = new System.Drawing.Bitmap(this.imgSource.UriSource.LocalPath);
                BitmapImage bitmap = new BitmapImage();
                tempImage.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipX);
                using (System.IO.MemoryStream ms = new MemoryStream())
                {
                    tempImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    bitmap.BeginInit();
                    bitmap.StreamSource = ms;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    bitmap.Freeze();
                }
                tempImage.Dispose();
                return bitmap;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 垂直镜像
        /// </summary>
        private BitmapImage MirrorVertical()
        {
            try
            {
                System.Drawing.Bitmap tempImage = new System.Drawing.Bitmap(this.imgSource.UriSource.LocalPath);
                BitmapImage bitmap = new BitmapImage();
                tempImage.RotateFlip(System.Drawing.RotateFlipType.RotateNoneFlipY);
                using (System.IO.MemoryStream ms = new MemoryStream())
                {
                    tempImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    bitmap.BeginInit();
                    bitmap.StreamSource = ms;
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.EndInit();
                    bitmap.Freeze();
                }
                tempImage.Dispose();
                return bitmap;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 中心点旋转
        /// </summary>
        private BitmapImage CenterRotate(int angle)
        {
            try
            {
                System.Drawing.Bitmap tempImage = new System.Drawing.Bitmap(this.imgSource.UriSource.LocalPath);
                //原图的宽和高
                int srcWidth = tempImage.Width;
                int srcHeight = tempImage.Height;
                //图像旋转之后所占区域宽和高
                System.Drawing.Rectangle rotateRec = GetRotateRectangle(srcWidth, srcHeight, angle);
                int rotateWidth = rotateRec.Width;
                int rotateHeight = rotateRec.Height;
                //目标位图
                System.Drawing.Bitmap destImage = new System.Drawing.Bitmap(rotateWidth, rotateHeight);
                System.Drawing.Graphics graphics = System.Drawing.Graphics.FromImage(destImage);
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bilinear;
                if (angle != 0)
                {
                    //矩阵旋转
                    System.Drawing.Drawing2D.Matrix matrix = graphics.Transform;
                    matrix.RotateAt(angle, new System.Drawing.PointF((float)(rotateWidth / 2), (float)(rotateHeight / 2)));
                    graphics.Transform = matrix;
                }
                //如果要将源图像画到画布上且中心与画布中心重合，需要的偏移量
                System.Drawing.Point Offset = new System.Drawing.Point((rotateWidth - srcWidth) / 2, (rotateHeight - srcHeight) / 2);
                graphics.DrawImage(tempImage, new System.Drawing.Rectangle((int)Offset.X, (int)Offset.Y, srcWidth, srcHeight));

                BitmapImage bi =  BitmapToBitmapImage(destImage, System.Drawing.Imaging.ImageFormat.Png);
                graphics.Dispose();
                tempImage.Dispose();
                destImage.Dispose();

                return bi;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 计算矩形绕中心任意角度旋转后所占区域矩形宽高
        /// </summary>
        private System.Drawing.Rectangle GetRotateRectangle(int width, int height, float angle)
        {
            double radian = angle * Math.PI / 180; ;
            double cos = Math.Cos(radian);
            double sin = Math.Sin(radian);

            int newWidth = (int)(Math.Max(Math.Abs(width * cos - height * sin), Math.Abs(width * cos + height * sin)));
            int newHeight = (int)(Math.Max(Math.Abs(width * sin - height * cos), Math.Abs(width * sin + height * cos)));
            return new System.Drawing.Rectangle(0, 0, newWidth, newHeight);
        }

        /// <summary>
        /// 绘制蒙板
        /// </summary>
        private unsafe BitmapImage Mask(Color maskColor)
        {
            System.Drawing.Bitmap tempImage = new System.Drawing.Bitmap(this.imgSource.UriSource.LocalPath);
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            int w = tempImage.Width;
            int h = tempImage.Height;
            System.Drawing.Imaging.BitmapData bckdata = null;
            //目标位图
            System.Drawing.Region region = null;
            System.Drawing.Bitmap destImage = null;
            System.Drawing.Graphics graphics = null;
            try
            {
                // 根据图片得到一个图片非透明部分的区域
                bckdata = tempImage.LockBits(new System.Drawing.Rectangle(0, 0, w, h), System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                uint* bckInt = (uint*)bckdata.Scan0;
                for (int j = 0; j < h; j++)
                {
                    for (int i = 0; i < w; i++)
                    {
                        if ((*bckInt & 0xff000000) != 0)
                        {
                            path.AddRectangle(new System.Drawing.Rectangle(i, j, 1, 1));
                        }
                        bckInt++;
                    }
                }
                tempImage.UnlockBits(bckdata);
                bckdata = null;

                region = new System.Drawing.Region(path);

                //定义画布，宽高为图像原始的宽高
                destImage = new System.Drawing.Bitmap(w, h);
                graphics = System.Drawing.Graphics.FromImage(destImage);
                System.Drawing.RectangleF newRect = region.GetBounds(graphics);
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;

                //图像蒙板
                if (maskColor != null)
                    graphics.FillPath(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(maskColor.A, maskColor.R, maskColor.G, maskColor.B)), path);

                path.Dispose();
                path = null;

                BitmapImage bi = BitmapToBitmapImage(destImage, System.Drawing.Imaging.ImageFormat.Png);
                destImage.Dispose();
                return bi;
            }
            catch (Exception ex)
            {
                return null;
            }
            finally
            {
                if (bckdata != null)
                {
                    tempImage.UnlockBits(bckdata);
                    bckdata = null;
                }
                if (graphics != null)
                    graphics.Dispose();
            }
        }

        #region(事件)
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


        /// <summary>
        /// 选择图片
        /// </summary>
        private void btnChooseImg_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "选择图片";
            openFileDialog.Filter = "图片(*.jpg,*.png,*.bmp)|*.jpg;*.png;*.bmp"; ;
            openFileDialog.FileName = string.Empty;
            openFileDialog.Multiselect = false;
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                BitmapImage bi =  LoadBitmapImageByPath(openFileDialog.FileName);
                if (bi != null)
                {
                    this.imgSource = bi;
                    this.imgPic.Source = bi;
                    this.imgPic.Width = bi.PixelWidth;
                    this.imgPic.Height = bi.PixelHeight;
                    Canvas.SetLeft(this.imgPic, (this.mainCanvas.ActualWidth - bi.PixelWidth) / 2);
                    Canvas.SetTop(this.imgPic, (this.mainCanvas.ActualHeight - bi.PixelHeight) / 2);
                }
            }
           
        }
        /// <summary>
        /// 黑白
        /// </summary>
        private void btnBlackAndWhite_Click(object sender, RoutedEventArgs e)
        {
            DrawImage(ImageProcessingEffect.BlackAndWhite);
        }
        /// <summary>
        /// 浮雕
        /// </summary>
        private void btnEmboss_Click(object sender, RoutedEventArgs e)
        {
            DrawImage(ImageProcessingEffect.Emboss);
        }
        /// <summary>
        /// 锐化
        /// </summary>
        private void btnSharpening_Click(object sender, RoutedEventArgs e)
        {
            DrawImage(ImageProcessingEffect.Sharpening);
        }

        /// <summary>
        /// 水平镜像
        /// </summary>
        private void btnMirrorHorizontal_Click(object sender, RoutedEventArgs e)
        {
            DrawImage(ImageProcessingEffect.MirrorHorizontal);
        }
        /// <summary>
        /// 垂直镜像
        /// </summary>
        private void btnMirrorVertical_Click(object sender, RoutedEventArgs e)
        {
            DrawImage(ImageProcessingEffect.MirrorVertical);
        }
        /// <summary>
        /// 旋转
        /// </summary>
        private void numAngle_ValueChanged(object sender, EventArgs e)
        {
            DrawImage(ImageProcessingEffect.CenterRotate);
        }

        /// <summary>
        /// 蒙板
        /// </summary>
        private void rPicMaskColor_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ColorSelectorWindow csw = new ColorSelectorWindow();
            csw.ShowDialog();
            this.rPicMaskColor.Fill = new SolidColorBrush(csw.returnSelectColor);
            DrawImage(ImageProcessingEffect.Mask);
        }

      
    

        /// <summary>
        /// 鼠标按下
        /// </summary>
        private void mainCanvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource.GetType() == typeof(MyImage))
            {
                MyImage img = (MyImage)e.OriginalSource;
                //获取点击前鼠标坐标
                this.pBefore = e.GetPosition(this.mainCanvas);
                //获取点击图片的坐标和位置
                this.eBefore = new Point(Canvas.GetLeft(img), Canvas.GetTop(img));
                this.wBefore = img.Width;
                this.hBefore = img.Height;
                //设置为可移动
                IsCanMove = true;
                //鼠标捕获此图片
                img.CaptureMouse();
                img.IsSelect = true;
                img.InvalidateVisual();
                //展示XY坐标
                ShowXY(img);
                //展示宽高
                ShowWH(img);
                SetOtherUnSelect(img);

                //改变鼠标样式
                this.Cursor = System.Windows.Input.Cursors.SizeAll;
            }
            else
            {
                SetOtherUnSelect();
            }
        }
        /// <summary>
        /// 鼠标抬起
        /// </summary>
        private void mainCanvas_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource.GetType() == typeof(MyImage))
            {
                MyImage img = (MyImage)e.OriginalSource;
                //设置为不可移动
                IsCanMove = false;
                //鼠标释放此图片
                img.ReleaseMouseCapture();
                img.InvalidateVisual();
                this.Cursor = System.Windows.Input.Cursors.Arrow;
            }
        }
        /// <summary>
        /// 鼠标移动
        /// </summary>
        private void mainCanvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (!IsCanMove)
                return;

            if (e.OriginalSource != null && e.LeftButton == MouseButtonState.Pressed)
            {
                if (e.OriginalSource.GetType() == typeof(MyImage))
                {
                    MyImage img = (MyImage)e.OriginalSource;

                    if (IsCanMove && img != null)
                    {
                        MoveChange(e, img.changeType);
                    }
                }
                else
                {
                    base.OnMouseMove(e);
                }
            }
            else
                this.Cursor = System.Windows.Input.Cursors.Arrow;
        }
        /// <summary>
        /// 移动改变坐标或大小
        /// </summary>
        private void MoveChange(System.Windows.Input.MouseEventArgs e, ChangeType changeType)
        {
            try
            {
                switch (changeType)
                {
                    case ChangeType.ChangeLeftTop:
                        setLTvp(e);
                        break;
                    case ChangeType.ChangeRightTop:
                        setRTvp(e);
                        break;
                    case ChangeType.ChangeRightBottom:
                        setRBvp(e);
                        break;
                    case ChangeType.ChangeLeftBottom:
                        setLBvp(e);
                        break;
                    case ChangeType.Move:
                        MyImage img = (MyImage)e.OriginalSource;
                        if (img != null)
                            MoveImage(e);
                        break;
                }
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// 移动对象
        /// </summary>
        public void MoveImage(System.Windows.Input.MouseEventArgs e)
        {
            MyImage img = (MyImage)e.OriginalSource;
            //获取鼠标移动中的坐标
            Point p = e.GetPosition(this.mainCanvas);
            //计算拖拽距离
            double dragx = p.X - pBefore.X;
            double dragy = p.Y - pBefore.Y;
            //当拖拽距离大于一定范围时改变控件位置
            if (Math.Abs(dragx) > 5 || Math.Abs(dragy) > 5)
            {
                Canvas.SetLeft(img, eBefore.X + dragx);
                Canvas.SetTop(img, eBefore.Y + dragy);
                img.InvalidateVisual();
                ShowXY(img);
            }
        }
        /// <summary>
        /// 左上角拖动
        /// </summary>
        public void setLTvp(System.Windows.Input.MouseEventArgs e)
        {
            //获取鼠标移动中的坐标
            Point p = e.GetPosition(this);
            if (e.OriginalSource.GetType() == typeof(MyImage))
            {
                MyImage img = (MyImage)e.OriginalSource;
                double changeX = p.X - pBefore.X;
                double changeY = p.Y - pBefore.Y;

                if ((wBefore - changeX) > minImgValue)
                {
                    Canvas.SetLeft(img, eBefore.X + changeX);
                    img.Width = wBefore - changeX;
                }
                else
                {
                    img.Width = minImgValue;
                }

                if ((hBefore - changeY) > minImgValue)
                {
                    Canvas.SetTop(img, eBefore.Y + changeY);
                    img.Height = hBefore - changeY;
                }
                else
                {
                    img.Height = minImgValue;
                }
                img.InvalidateVisual();

                ShowXY(img);
                ShowWH(img);
            }

        }
        /// <summary>
        /// 右上角拖动
        /// </summary>
        public void setRTvp(System.Windows.Input.MouseEventArgs e)
        {
            //获取鼠标移动中的坐标
            Point p = e.GetPosition(this);
            if (e.OriginalSource.GetType() == typeof(MyImage))
            {
                MyImage img = (MyImage)e.OriginalSource;
                double changeX = p.X - pBefore.X;
                double changeY = p.Y - pBefore.Y;

                img.Width = (wBefore + changeX) > minImgValue ? wBefore + changeX : minImgValue;

                if ((hBefore - changeY) > minImgValue)
                {
                    Canvas.SetTop(img, eBefore.Y + changeY);
                    img.Height = hBefore - changeY;
                }
                else
                {
                    img.Height = minImgValue;
                }
                img.InvalidateVisual();

                ShowXY(img);
                ShowWH(img);
            }
        }
        /// <summary>
        /// 左下角拖动
        /// </summary>
        public void setLBvp(System.Windows.Input.MouseEventArgs e)
        {
            //获取鼠标移动中的坐标
            Point p = e.GetPosition(this);
            if (e.OriginalSource.GetType() == typeof(MyImage))
            {
                MyImage img = (MyImage)e.OriginalSource;
                double changeX = p.X - pBefore.X;
                double changeY = p.Y - pBefore.Y;

                if ((wBefore - changeX) > minImgValue)
                {
                    Canvas.SetLeft(img, eBefore.X + changeX);
                    img.Width = wBefore - changeX;
                }
                else
                {
                    img.Width = minImgValue;
                }

                img.Height = (hBefore + changeY) > minImgValue ? hBefore + changeY : minImgValue;
                img.InvalidateVisual();

                ShowXY(img);
                ShowWH(img);
            }
        }
        /// <summary>
        /// 右下角拖动
        /// </summary>
        public void setRBvp(System.Windows.Input.MouseEventArgs e)
        {
            //获取鼠标移动中的坐标
            Point p = e.GetPosition(this);
            if (e.OriginalSource.GetType() == typeof(MyImage))
            {
                MyImage img = (MyImage)e.OriginalSource;
                double changeX = p.X - pBefore.X;
                double changeY = p.Y - pBefore.Y;
                img.Width = (wBefore + changeX) > minImgValue ? wBefore + changeX : minImgValue;
                img.Height = (hBefore + changeY) > minImgValue ? hBefore + changeY : minImgValue;
                ShowWH(img);
            }
        }
        /// <summary>
        /// 设置其他为未选择状态
        /// </summary>
        public void SetOtherUnSelect(object myImage = null)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(this.mainCanvas); i++)
            {
                if (VisualTreeHelper.GetChild(this.mainCanvas, i).GetType() == typeof(MyImage))
                {
                    MyImage child = (MyImage)VisualTreeHelper.GetChild(this.mainCanvas, i);
                    if (myImage == null)
                    {
                        child.IsSelect = false;
                        child.InvalidateVisual();
                    }
                    else if (myImage != child)
                    {
                        if (child.IsSelect)
                        {
                            child.IsSelect = false;
                            child.InvalidateVisual();
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 将Canvas内容输出为位图
        /// </summary>
        public RenderTargetBitmap ExportToBitmap()
        {
            try
            {
                Canvas surface = this.mainCanvas;

                Size size = new Size(surface.Width, surface.Height);
                surface.Measure(size);
                surface.Arrange(new Rect(size));

                RenderTargetBitmap renderBitmap =
                new RenderTargetBitmap(
                (int)size.Width,
                (int)size.Height,
                96d,
                96d,
                PixelFormats.Pbgra32);
                renderBitmap.Render(surface);

                return renderBitmap;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 保存为png图片
        /// </summary>
        private void btnSaveImg_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RenderTargetBitmap getSource = ExportToBitmap();
                //BitmapImage getImage = ConventToBitmapImage(getSource);
                string savePath = @"E:\切图\example1.png";
                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(getSource));
                using (FileStream fs = new FileStream(savePath, FileMode.Create))
                {
                    encoder.Save(fs);
                    fs.Close();
                }
            }catch(Exception ex)
            {

            }
 
        }

        /// <summary>
        /// 把内存里的BitmapImage数据保存到硬盘中
        /// </summary>
        /// <param name="bitmapImage">BitmapImage数据</param>
        /// <param name="filePath">输出的文件路径</param>
        public static void SaveBitmapImageIntoFile(BitmapImage bitmapImage, string filePath)
        {
            BitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmapImage));

            using (var fileStream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
            {
                encoder.Save(fileStream);
            }
        }

        private BitmapImage ConventToBitmapImage(RenderTargetBitmap getSource)
        {

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(getSource));
            BitmapImage bitmapImage = new BitmapImage();
            using (var memoryStream = new MemoryStream())
            {
                encoder.Save(memoryStream);
                memoryStream.Seek(0, SeekOrigin.Begin);
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memoryStream;
                bitmapImage.EndInit();
            }
            return bitmapImage;
        }

        /// <summary>
        /// 展示XY坐标
        /// </summary>
        private void ShowXY(MyImage img)
        {
            this.locationX.Value = Convert.ToDecimal(Canvas.GetLeft(img));
            this.locationY.Value = Convert.ToDecimal(Canvas.GetTop(img));
        }
        /// <summary>
        /// 展示img宽高
        /// </summary>
        private void ShowWH(MyImage img)
        {
            this.imgWidth.Value = Convert.ToDecimal(img.Width);
            this.imgHeight.Value = Convert.ToDecimal(img.Height);
        }

        /// <summary>
        /// X坐标改变事件
        /// </summary>
        private void LocationX_ValueChanged(object sender, EventArgs e)
        {
            if (IsCanMove)
                return;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(this.mainCanvas); i++)
            {
                if (VisualTreeHelper.GetChild(this.mainCanvas, i).GetType() == typeof(MyImage))
                {
                    MyImage child = (MyImage)VisualTreeHelper.GetChild(this.mainCanvas, i);
                    if (child.IsSelect)
                    {
                        Canvas.SetLeft(child, Convert.ToDouble(this.locationX.Value));
                        child.InvalidateVisual();
                    }
                }
            }
        }
        /// <summary>
        /// Y坐标改变事件
        /// </summary>
        private void LocationY_ValueChanged(object sender, EventArgs e)
        {
            if (IsCanMove)
                return;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(this.mainCanvas); i++)
            {
                if (VisualTreeHelper.GetChild(this.mainCanvas, i).GetType() == typeof(MyImage))
                {
                    MyImage child = (MyImage)VisualTreeHelper.GetChild(this.mainCanvas, i);
                    if (child.IsSelect)
                    {
                        Canvas.SetTop(child, Convert.ToDouble(this.locationY.Value));
                        child.InvalidateVisual();
                    }
                }
            }
        }
        /// <summary>
        /// 宽改变事件
        /// </summary>
        private void ImgWidth_ValueChanged(object sender, EventArgs e)
        {
            if (IsCanMove)
                return;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(this.mainCanvas); i++)
            {
                if (VisualTreeHelper.GetChild(this.mainCanvas, i).GetType() == typeof(MyImage))
                {
                    MyImage child = (MyImage)VisualTreeHelper.GetChild(this.mainCanvas, i);
                    if (child.IsSelect)
                    {
                        child.Width = Convert.ToDouble(this.imgWidth.Value);
                        child.InvalidateVisual();
                    }
                }
            }
        }
        /// <summary>
        /// 高改变事件
        /// </summary>
        private void ImgHeight_ValueChanged(object sender, EventArgs e)
        {
            if (IsCanMove)
                return;

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(this.mainCanvas); i++)
            {
                if (VisualTreeHelper.GetChild(this.mainCanvas, i).GetType() == typeof(MyImage))
                {
                    MyImage child = (MyImage)VisualTreeHelper.GetChild(this.mainCanvas, i);
                    if (child.IsSelect)
                    {
                        child.Height = Convert.ToDouble(this.imgHeight.Value);
                        child.InvalidateVisual();
                    }
                }
            }
        }

 
#endregion
        /// <summary>
        /// 释放资源的
        /// </summary>
        /// <param name="hObject"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        private void DeleteObject()
        {
            using (System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(1000, 1000))
            {
                IntPtr hBitmap = bmp.GetHbitmap();

                try
                {
                    var source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(hBitmap, IntPtr.Zero, Int32Rect.Empty, System.Windows.Media.Imaging.BitmapSizeOptions.FromEmptyOptions());
                }
                finally
                {
                    DeleteObject(hBitmap);
                }
            }
        }

        private void btnBlackAndWhite_Click()
        {

        }
    }
}
