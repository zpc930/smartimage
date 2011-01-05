using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Orc.SmartImage
{
    /// <summary>
    /// 极坐标点
    /// </summary>
    public struct PolarPointD
    {
        /// <summary>
        /// 半径
        /// </summary>
        public double Radius;

        /// <summary>
        /// 角度
        /// </summary>
        public double Angle;

        public PolarPointD(double radius, double angle)
        {
            Radius = radius;
            Angle = angle;
        }
    }

    /// <summary>
    /// 极坐标点
    /// </summary>
    public struct PolarPoint
    {
        /// <summary>
        /// 半径
        /// </summary>
        public int Radius;

        /// <summary>
        /// 角度
        /// </summary>
        public int Angle;

        public PolarPoint(int radius, int angle)
        {
            Radius = radius;
            Angle = angle;
        }
    }
}
