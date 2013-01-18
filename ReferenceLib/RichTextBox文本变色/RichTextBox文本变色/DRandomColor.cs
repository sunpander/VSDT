using System;
using System.Text;
using System.Collections;
using System.Drawing;

namespace RichTextBox文本变色
{
    /// <summary>
    /// 生成不重复且不相似的随机色彩的生成器。
    /// </summary>
    public class DRandomColor
    {
        private Random random;

        /// <summary>
        /// 构造函数。
        /// </summary>
        public DRandomColor()
        {
            random = new Random();
        }

        /// <summary>
        /// 重置对象。
        /// </summary>
        public void Reset()
        {
            colors.Clear();
        }

        /// <summary>
        /// 检查RGB码是否在范围中。
        /// </summary>
        /// <param name="value">待检查的RGB码。</param>
        /// <returns></returns>
        private int CheckRGB(int value)
        {
            if (value < 0)
                return 0;
            else if (value > 255)
                return 255;
            else
                return value;
        }

        /// <summary>
        /// 获取或设置红色值下限。
        /// </summary>
        public int Red_Min
        {
            get
            {
                return this.R_min;
            }
            set
            {
                this.R_min = CheckRGB(value);
            }
        }

        /// <summary>
        /// 获取或设置红色值上限。
        /// </summary>
        public int Red_Max
        {
            get
            {
                return this.R_max;
            }
            set
            {
                this.R_max = CheckRGB(value);
            }
        }

        /// <summary>
        /// 获取或设置绿色值下限。
        /// </summary>
        public int Green_Min
        {
            get
            {
                return this.G_min;
            }
            set
            {
                this.G_min = CheckRGB(value);
            }
        }

        /// <summary>
        /// 获取或设置绿色值上限。
        /// </summary>
        public int Green_Max
        {
            get
            {
                return this.G_max;
            }
            set
            {
                this.G_max = CheckRGB(value);
            }
        }

        /// <summary>
        /// 获取或设置蓝色值下限。
        /// </summary>
        public int Blue_Min
        {
            get
            {
                return this.B_min;
            }
            set
            {
                this.B_min = CheckRGB(value);
            }
        }
        /// <summary>
        /// 获取或设置蓝色值上限。
        /// </summary>
        public int Blue_Max
        {
            get
            {
                return this.B_max;
            }
            set
            {
                this.B_max = CheckRGB(value);
            }
        }

        /// <summary>
        /// 获取或设置用于区分两个颜色的差别程度值。不希望生成的颜色看上去太相似，
        /// 毕竟人眼的识别能力有限。该值越大，两个随机颜色间的区别越大。该值最大100,
        /// 负数表示允许生成重复色彩。
        /// </summary>
        public int DifferenceGrade
        {
            get
            {
                return this.differenceGrade;
            }
            set
            {
                if (value > 100)
                    this.differenceGrade = 100;
                else if (value < 0)
                    this.differenceGrade = -1;
                else
                    this.differenceGrade = value;
            }
        }

        /// <summary>
        /// 记录随机生成的色彩，用于防止出现重复色。
        /// </summary>
        private ArrayList colors = new ArrayList();

        /// <summary>
        /// 三元色范围值，因为颜色值越小，颜色越深（RGB都是0可就漆黑一片，即使不都是0，碰巧两三个50以下，也会看不见背景上的字），
        /// 除此以外，用户多半不会喜欢R值过低，使心情沉闷，或有其他视觉喜好，所以在这里用变量以方便修改调控。
        /// </summary>
        private int R_min = 75, R_max = 200, G_min = 75, G_max = 200, B_min = 75, B_max = 200;

        /// <summary>
        /// 用于区分两个颜色的差别程度。不希望生成的颜色看上去太相似，
        /// 毕竟人眼的识别能力有限。该值越大，两个随机颜色间的区别越大。
        /// </summary>
        private int differenceGrade = 100;

        /// <summary>
        /// 获取随机色，与colors集合一起实现无重复随机色生成。
        /// </summary>
        /// <returns></returns>
        public Color GetRandomColor()
        {
            Color result = Color.White;
            bool isRepeat = false;
            do
            {
                isRepeat = false;
                int r = random.Next(R_min, R_max);
                int g = random.Next(G_min, G_max);
                int b = random.Next(B_min, B_max);
                result = Color.FromArgb(r, g, b);
                foreach (object i in colors)
                {
                    Color c = (Color)i;
                    if ((System.Math.Abs(c.R - result.R) + System.Math.Abs(c.G - result.G) + System.Math.Abs(c.B - result.B)) < differenceGrade)
                    {
                        isRepeat = true;
                        break;
                    }
                }
            } while (isRepeat);
            colors.Add(result);
            return result;
        }

		public Color CustomColor(int r,int g,int b)
		{
			Color result = Color.White;
			result = Color.FromArgb(r, g, b);
			return result;
		}

    }
}
