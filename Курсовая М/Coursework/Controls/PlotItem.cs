using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace OstapenkoAE.Controls
{
    /// <summary>
    /// Содержит информацию о графике функции.
    /// </summary>
    internal class PlotItem
    {/// <summary>
     /// Содержит информацию о графике функции.
     /// </summary>
     /// <param name="Legend">Название графика.</param>
     /// <param name="LineColor">Цвет графика.</param>
     /// <param name="Func">Функция y(x).</param>
     /// <param name="points">Функция y(x) по точкам.</param>
        internal PlotItem(string Legend, Color LineColor,
        Func<double, double> Func)
        {
            this.legend = Legend;
            this.lineColor = LineColor;
            this.func = Func;
        }

        internal PlotItem(string Legend, Color LineColor,
        List<PointF> Points)
        {
            this.legend = Legend;
            this.lineColor = LineColor;
            this.points = Points;
        }
        /// <summary>
        /// Возвращает название графика.
        /// </summary>
        internal string Legend
        {
            get
            {
                return this.legend;
            }
        }
        /// <summary>
        /// Возвращает либо задает цвет графика.
        /// </summary>
        internal Color LineColor
        {
            get
            {
                return this.lineColor;
            }
            set
            {
                this.lineColor = value;
            }
        }
        /// <summary>
        /// Возвращает либо задает функцию y(x).
        /// </summary>
        internal Func<double, double> Function
        {
            get
            {
                return this.func;
            }
            set
            {
                this.func = value;
            }
        }

        internal List<PointF> Points
        {
            get
            {
                return this.points;
            }
            set
            {
                this.points = value;
            }
        }

        string legend;
        Color lineColor;
        Func<double, double> func;
        List<PointF> points;
    }
} 
