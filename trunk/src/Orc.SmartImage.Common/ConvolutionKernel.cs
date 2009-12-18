using System;
using System.Collections.Generic;
using System.Text;

namespace Orc.SmartImage
{
    /// <summary>
    /// 卷积核
    /// </summary>
    public class ConvolutionKernel
    {
        /// <summary>
        /// 图像平滑卷积核01：
        /// 1,1,1
        /// 1,1,1   × (1/9)
        /// 1,1,1
        /// </summary>
        public static readonly ConvolutionKernel Average01 = new ConvolutionKernel( 
            new int[,]
            {
                {1,1,1},
                {1,1,1},
                {1,1,1}
            },
            3,3,9);

        /// <summary>
        /// 图像平滑卷积核02：
        /// 1,1,1
        /// 1,2,1   × (1/10)
        /// 1,1,1
        /// </summary>
        public static readonly ConvolutionKernel Average02 = new ConvolutionKernel(
            new int[,]
            {
                {1,1,1},
                {1,2,1},
                {1,1,1}
            },
            3, 3, 10);

        /// <summary>
        /// 图像平滑卷积核03：
        /// 1,2,1
        /// 2,4,2   × (1/16)
        /// 1,2,1
        /// </summary>
        public static readonly ConvolutionKernel Average03 = new ConvolutionKernel(
    new int[,]
            {
                {1,2,1},
                {2,4,2},
                {1,2,1}
            },
    3, 3, 16);

        /// <summary>
        /// 根据 4-邻域 计算的 Laplace 算子
        /// 0,1,0
        /// 1,-4,1
        /// 0,1,0
        /// </summary>
        public static readonly ConvolutionKernel Laplace01 = new ConvolutionKernel(
    new int[,]
            {
                {0,1,0},
                {1,-4,1},
                {0,1,0}
            },
    3, 3, 1);

        /// <summary>
        /// 根据 8-邻域 计算的 Laplace 算子
        /// 1,1, 1
        /// 1,-8,1 * 1/3
        /// 1,1, 1
        /// </summary>
        public static readonly ConvolutionKernel Laplace02 = new ConvolutionKernel(
    new int[,]
            {
                {1,1, 1},
                {1,-8,1},
                {1,1, 1}
            },
    3, 3, 3);

        public static readonly ConvolutionKernel SobelX = new ConvolutionKernel(
    new int[,]
            {
                {-1, 0, 1},
                {-2, 0, 2},
                {-1, 0, 1}
            },
    3, 3, 1);

        public static readonly ConvolutionKernel SobelY = new ConvolutionKernel(
            new int[,]
            {
                {1, 2, 1},
                {0, 0, 0},
                {-1,-2,-1}
            },
            3, 3, 1);

        /// <summary>
        /// int[height, width]
        /// </summary>
        public int[,] Kernel { get; private set; }
        public int Divisor { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        /// <summary>
        /// 用二维数组表示的ConvolutionKernel。
        /// </summary>
        /// <param name="kernel">kernel</param>
        /// <param name="divisor">divisor</param>
        public ConvolutionKernel(int[,] kernel, int width, int height, int divisor)
        {
            if (kernel == null) throw new ArgumentNullException("kernel");
            if (width < 1) throw new ArgumentException("Width must > 0.");
            if (height < 1) throw new ArgumentException("Height must > 0.");
            if (IsEvenNumber(height) == true) throw new ArgumentException("Height must be odd number.");
            if (IsEvenNumber(width) == true) throw new ArgumentException("Width must be odd number.");
            if (divisor < 1) throw new ArgumentException("Divisor must > 0");
            if (kernel.Length != width * height) throw new ArithmeticException("The elements' count must equal multiply of width and height.");

            Height = height;
            Width = width;
            Kernel = kernel;
            Divisor = divisor;
        }

        // 判断是否是偶数
        private Boolean IsEvenNumber(int number)
        {
            return ((number >> 1) << 1) == number;
        }
    }
}
