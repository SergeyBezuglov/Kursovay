using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Reflection;

namespace OstapenkoAE.Controls
{
    /// <summary>
    /// Элемент управления, предназначенный для отображения графиков.
    /// </summary>
    public partial class Plot : UserControl
    {
        Point centerPoint; // Положение начала координат в пикселях
        double scaleX;     // Масштаб по оси X
        double scaleY;     // Масштаб по оси Y
        int minPix;        // размер "мёртвой зоны"

        /// <summary>
        /// Возвращает либо задает центр мировых координат
        /// на плоскости контрола.
        /// </summary>
        [Category("Plot")]
        public Point CenterPoint
        {
            get
            {
                return this.centerPoint;
            }
            set
            {
                this.centerPoint = value;
            }
        }
        /// <summary>
        /// Возвращает либо задает длину стороны квадрата
        /// в пикселях, перемещение курсора внутри которого
        /// будет считаться точечным действием.
        /// </summary>
        [Category("Plot")]
        public int MinPix
        {
            get
            {
                return this.minPix;
            }
            set
            {
                if (value <= 0)
                {
                    throw new Exception("Значение MinPix должно быть > 0");
                }
                this.minPix = value;
            }
        }
        /// <summary>
        /// Возвращает либо задает коэффициент масштабирования
        /// по оси X (задает число пикселей, соответствующее
        /// единице измерения вдоль оси).
        /// </summary>
        [Category("Plot")]
        public double ScaleX
        {
            get
            {
                return this.scaleX;
            }
            set
            {
                this.scaleX = value;
            }
        }
        /// <summary>
        /// Возвращает либо задает название величины,
        /// откладываемой по оси X.
        /// </summary>
        [Category("Plot")]
        public string XName
        {
            get
            {
                return this.labelXname.Text.TrimEnd(' ', '=');
            }
            set
            {
                this.labelXname.Text = value + " =";
            }
        }
        /// <summary>
        /// Возвращает либо задает название единицы измерения
        /// по оси X.
        /// </summary>
        [Category("Plot")]
        public string XUnits
        {
            get
            {
                return this.labelXunits.Text.Trim('[', ']');
            }
            set
            {
                if (value != "")
                {
                    this.labelXunits.Text = "[" + value + "]";
                }
                else
                {
                    this.labelXunits.Text = "";
                }
            }
        }

        /// <summary>
        /// Возвращает либо задает коэффициент масштабирования
        /// по оси Y (задает число пикселей, соответствующее
        /// единице измерения вдоль оси).
        /// </summary>
        [Category("Plot")]
        public double ScaleY
        {
            get
            {
                return this.scaleY;
            }
            set
            {
                this.scaleY = value;
            }
        }
        /// <summary>
        /// Возвращает либо задает название величины,
        /// откладываемой по оси Y.
        /// </summary>
        [Category("Plot")]
        public string YName
        {
            get
            {
                return this.labelYname.Text.TrimEnd(' ', '=');
            }
            set
            {
                this.labelYname.Text = value + " =";
            }
        }
        /// <summary>
        /// Возвращает либо задает название единицы измерения
        /// по оси Y.
        /// </summary>
        [Category("Plot")]
        public string YUnits
        {
            get
            {
                return this.labelYunits.Text.Trim('[', ']');
            }
            set
            {
                if (value != "")
                {
                    this.labelYunits.Text = "[" + value + "]";
                }
                else
                {
                    this.labelYunits.Text = "";
                }
            }
        }

        /// <summary>
        /// Рисует графики.
        /// </summary>
        public void Draw()
        {
            try
            {
                // Получаем пустое изображение с размерами
                // элемента pictureBoxPlot,
                // выполняющее роль буфера
                this.pictureBoxPlot.Image =
                new Bitmap(this.pictureBoxPlot.Width,
                this.pictureBoxPlot.Height);
                // Из изображения получаем объект Graphics,
                // предоставляющий методы рисования
                using (Graphics graphics =
                Graphics.FromImage(this.pictureBoxPlot.Image))
                {
                    // Заливаем изображение цветом фона
                    graphics.Clear(this.pictureBoxPlot.BackColor);
                    // Создаем объект Pen, содержащий
                    // параметры рисования линий
                    using (Pen pen = new Pen(Color.Black))
                    {
                        // Устанавливаем толщину линий
                        pen.Width = 1f;
                        // Рисование осей
                        this.DrawAxes(graphics);

                        // Рисование графиков
                        if (this.items != null)
                        {
                            foreach (KeyValuePair<string, PlotItem> pair
                            in this.items)
                            {
                                PlotItem item = pair.Value;
                                pen.Color = item.LineColor;
                                if (item.Function != null)
                                {
                                    this.DrawFunctionFromX(graphics, pen,
                                    item.Function);

                                }
                                if (item.Points != null)
                                {
                                    this.DrawPoints(graphics, pen,
                                    item.Points);

                                }

                            }
                        }
                    }
                }
                this.pictureBoxPlot.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Ошибка графика",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
            }



        }
        /// <summary>
        /// Рисует оси координат.
        /// </summary>
        /// <param name="Graphics">Холст.</param>
        /// <param name="Pen">Кисть.</param>
        private void DrawAxes(Graphics Graphics)
        {
            //Graphics.DrawLine(Pen, 0, this.centerPoint.Y, this.pictureBoxPlot.Width, this.centerPoint.Y);
            //Graphics.DrawLine(Pen, this.centerPoint.X, this.pictureBoxPlot.Height, this.centerPoint.X, 0);
            // Задаем ориентировочное расстояние между линиями сетки
            // в пикселях
            int delta = 80;
            // Задаем размер отступов,
            // необходимых для корректного для размещения подписей
            int xIndent = 30; // горизонтальный отступ
            int yIndent = 18; // вертикальный отступ
                              // Сетку рисовать будем серым цветом
            using (Pen pen = new Pen(Color.Gray))
            {
                // Задаем толщину линий стеки
                pen.Width = 0.7f;
                // Задаем стиль линий сетки - точками
                pen.DashStyle = DashStyle.Dot;
                // Задаем шрифт подписей
                Font font = new Font("Arial", 8);
                // Задаем цвет подписей
                Brush brush = Brushes.Black;
                #region Подписи к горизонтальной оси
                // Получаем ориентировочное расстояние между вертикальными линиями сетки в единицах оси X
                double Delta = delta / this.scaleX;
                // Приводим Delta к виду m * 10^k,
                // где 0.75 <= m && m <= 7.5
                int k = 0; // показатель степени
                while (Delta > 7.5)
                {
                    Delta /= 10;
                    k++;
                }
                while (Delta < 0.75)
                {
                    Delta *= 10;
                    k--;
                }
                // Получаем фактическое расстояние между вертикальными
                // линиями сетки в единицах оси X,
                // выбирая ближайшее значение из {1, 2, 5}
                double dX;
                if (3.5 < Delta) // && Delta <= 7.5
                {
                    dX = 5 * Math.Pow(10, k);
                }
                else
                {
                    if (1.5 < Delta) // && Delta <= 3.5
                    {
                        dX = 2 * Math.Pow(10, k);
                    }
                    else // 0.75 <= Delta && Delta <= 1.5
                    {
                        dX = 1 * Math.Pow(10, k);
                    }
                }
                // Вычисляем номер i самой левой вертикальной линии
                // i < 0 для X < 0, i > 0 для X > 0, i = 0 для X =0
                int i = (int)((xIndent - this.centerPoint.X) / (dX * this.scaleX));
                // Получаем координату левой линии в мировой системе
                double X = dX * i;
                // Получаем координату левой линии в системе монитора
                int x = this.centerPoint.X + (int)(X * this.scaleX);
                // Если линия оказалась в зоне левого отступа из-за
                // погрешности округления,
                // то переходим к следующей
                if (x < xIndent)
                {
                    X = dX * ++i;
                    x = this.centerPoint.X + (int)(X * this.scaleX);
                }
                // Рисуем линии до зоны правого отступа
                while (x <= this.pictureBoxPlot.Width - xIndent)
                {
                    // Рисуем вертикальную линию, оставляя снизу зону
                    // для подписи
                    Graphics.DrawLine(pen, x, 0, x,
                    this.pictureBoxPlot.Height - yIndent);
                    // Формируем и выводим подпись
                    string mark = "";
                    if (k < 0)
                    {
                        // X - число дробное: выделяем место под
                        // дробную часть
                        mark = X.ToString("F" + (-k).ToString());
                    }
                    else
                    {
                        // X - число целое
                        mark = X.ToString();
                    }
                    // Получаем координаты левой верхней точки
                    // прямоугольника подписи и выводим ее
                    int xm = x - 7 - Math.Abs(k) * 3;
                    int ym = this.pictureBoxPlot.Height - 16;
                    Graphics.DrawString(mark, font, brush, xm, ym);
                    // Получаем параметры следующей линии
                    X = dX * ++i;
                    x = this.centerPoint.X + (int)(X * this.scaleX);
                }
                #endregion
                #region Подписи к вертикальной оси
                // Получаем ориентировочное расстояние между горизонтальными линиями сетки в единицах оси Y
                double Delta1 = delta / this.scaleY;
                // Приводим Delta к виду m * 10^k,
                // где 0.75 <= m && m <= 7.5
                int k1 = 0; // показатель степени
                while (Delta1 > 7.5)
                {
                    Delta1 /= 10;
                    k1++;
                }
                while (Delta1 < 0.75)
                {
                    Delta1 *= 10;
                    k1--;
                }
                // Получаем фактическое расстояние между горизонталньыми
                // линиями сетки в единицах оси Y,
                // выбирая ближайшее значение из {1, 2, 5}
                double dY;
                if (3.5 < Delta1) // && Delta <= 7.5
                {
                    dY = 5 * Math.Pow(10, k1);
                }
                else
                {
                    if (1.5 < Delta1) // && Delta <= 3.5
                    {
                        dY = 2 * Math.Pow(10, k1);
                    }
                    else // 0.75 <= Delta && Delta <= 1.5
                    {
                        dY = 1 * Math.Pow(10, k1);
                    }
                }

                int i1 = (int)((yIndent - this.centerPoint.Y) / (dY * this.scaleY));
                double Y = dY * i1;
                int y = this.centerPoint.Y + (int)(Y * this.scaleY);
                if (y < yIndent)
                {
                    Y = dY * ++i1;
                    y = this.centerPoint.Y + (int)(Y * this.scaleY);
                }
                while (y <= this.pictureBoxPlot.Height - yIndent)
                {
                    // Рисуем горизонтальную линию, оставляя слева зону
                    // для подписи
                    Graphics.DrawLine(pen, xIndent, y, this.pictureBoxPlot.Width, y);
                    // Формируем и выводим подпись
                    string mark = "";
                    if (k1 < 0)
                    {
                        // Y - число дробное: выделяем место под
                        // дробную часть
                        mark = (-Y).ToString("F" + (-k1).ToString());
                    }
                    else
                    {
                        // Y - число целое
                        mark = (-Y).ToString();
                    }
                    int ym = y - 7 - Math.Abs(k1) * 3;
                    int xm = 0;
                    Graphics.DrawString(mark, font, brush, xm, ym);
                    // Получаем параметры следующей линии
                    Y = dY * ++i1;
                    y = this.centerPoint.Y + (int)(Y * this.scaleY);
                }
                #endregion
            }
        }
        Dictionary<string, PlotItem> items;

        /// <summary>
        /// Обновляет информацию о графике с указанным названием.
        /// Если графика с таким названием нет, то он будет создан.
        /// </summary>
        /// <param name="Legend">Название графика.</param>
        /// <param name="LineColor">Цвет графика.</param>
        /// <param name="Func">Функция y(x).</param>
        public void UpdateItem(string Legend, Color LineColor, Func<double, double> Func)
        {
            PlotItem item = new PlotItem(Legend, LineColor, Func);

            if (this.items.ContainsKey(Legend))
            {
                // Обновляем
                this.items[Legend] = item;
            }
            else
            {
                // Создаем новый
                this.items.Add(Legend, item);
            }
            SetLegendPanel();
        }

        public void UpdateItem(string Legend, Color LineColor, List<PointF> Points)
        {
            PlotItem item = new PlotItem(Legend, LineColor, Points);
            if (this.items.ContainsKey(Legend))
            {
                // Обновляем
                this.items[Legend] = item;
            }
            else
            {
                // Создаем новый
                this.items.Add(Legend, item);
            }
            SetLegendPanel();
        }

        public void DrawPoints(Graphics Graphics, Pen Pen, List<PointF> Points)
        {
            double X = Points[0].X;
            double Y = Points[0].Y;
            int x = this.centerPoint.X + (int)(this.scaleX * X);
            int y = this.centerPoint.Y - (int)(this.scaleY * Y);
            int xpr = x;
            int ypr = y;
            for (int i = 1; i < Points.Count; i++)
            {
                X = Points[i].X;
                Y = Points[i].Y;
                x = this.centerPoint.X + (int)(this.scaleX * X);
                y = this.centerPoint.Y - (int)(this.scaleY * Y);
                Graphics.DrawLine(Pen, xpr, ypr, x, y);
                xpr = x;
                ypr = y;
            }
        }


        public void Clear()
        {
            this.items.Clear();
        }

        /// <summary>
        /// Рисует график указанной функции y(x).
        /// </summary> 
        /// <param name="Graphics">Холст.</param>
        /// <param name="Pen">Кисть.</param>
        /// <param name="Function">Функция, график которой нужно
        /// построить.</param>
        private void DrawFunctionFromX(Graphics Graphics, Pen Pen,
         Func<double, double> Function)
        {
            double X;
            double Y;
            int x = 0;
            X = (x - this.centerPoint.X) / this.scaleX;
            Y = Function(X);
            int y = this.centerPoint.Y - (int)(this.scaleY * Y);
            int xpr = x;
            int ypr = y;
            for (int i = 1; i < this.pictureBoxPlot.Width; i++)
            {
                x = i;
                X = (x - this.centerPoint.X) / this.scaleX;
                Y = Function(X);
                y = this.centerPoint.Y - (int)(this.scaleY * Y);
                Graphics.DrawLine(Pen, xpr, ypr, x, y);
                xpr = x;
                ypr = y;
            }
        }

        Size oldSize; // Старый размер области рисования графика

        /// <summary>
        /// Задает новые параметры начала координат и масштаба,
        /// перерисовывая график.
        /// </summary>
        /// <param name="Center">Новый центр взгляда в координатах
        /// монитора.
        /// </param>
        /// <param name="ScaleX">Новый масштаб по оси X.</param>
        /// <param name="ScaleY">Новый масштаб по оси Y.</param>
        private void SetCamera(Point Center, double ScaleX,
         double ScaleY)
        {
            // Пересчитываем положение начала координат
            this.centerPoint.X =
            (int)Math.Round(Center.X + ScaleX / this.scaleX *
            (this.centerPoint.X - Center.X),
            MidpointRounding.AwayFromZero);
            this.centerPoint.Y =
            (int)Math.Round(Center.Y + ScaleY / this.scaleY *
            (this.centerPoint.Y - Center.Y),
            MidpointRounding.AwayFromZero);
            // Сохраняем новые значения масштаба
            this.scaleX = ScaleX;
            this.scaleY = ScaleY;
            // Обновляем содержимое буфера
            this.Draw();
        }

        /// <summary>
        /// Элемент управления, предназначенный для отображения графиков.
        /// </summary>
        public Plot()
        {
            
            InitializeComponent();
            this.centerPoint = new Point(this.pictureBoxPlot.Width / 2, this.pictureBoxPlot.Height / 2);
            scaleX = 1; scaleY = 1;
            minPix = 3;
            XName = "Путь";
            YName = "Высота";
            XUnits = "метры"; YUnits = "метры";
            this.labelXvalue.Text = "";
            this.labelYvalue.Text = "";
            this.items = new Dictionary<string, PlotItem>();
            this.oldSize = this.pictureBoxPlot.Size;
            this.pictureBoxPlot.MouseWheel += this.pictureBoxPlot_MouseWheel;
            oldscaleX = scaleX;
            oldscaleY = scaleY;
            SetLegendPanel();
        }

        Point oldMousePos;

        private void pictureBoxPlot_SizeChanged(object sender, EventArgs e)
        {
            // Получаем коэффициенты растяжения (сжатия) по осям
            float kx = (float)this.pictureBoxPlot.Size.Width /
            (float)this.oldSize.Width;
            float ky = (float)this.pictureBoxPlot.Size.Height /
            (float)this.oldSize.Height;
            // Пересчитываем положение начала координат и масштаб
            this.centerPoint.X =
            (int)Math.Round(kx * this.centerPoint.X,

            MidpointRounding.AwayFromZero);
            this.centerPoint.Y =
            (int)Math.Round(ky * this.centerPoint.Y,
            MidpointRounding.AwayFromZero);
            this.scaleX *= kx;
            this.scaleY *= ky;
            // Сохраняем размеры области рисования
            this.oldSize = this.pictureBoxPlot.Size;
            // Обновляем содержимое буфера
            this.Draw();
        }


        private void pictureBoxPlot_MouseDown(object sender, MouseEventArgs e)
        {
            oldMousePos = e.Location;
            #region Перетаскивание с помощью левой кнопки мыши
            if (e.Button == MouseButtons.Left)
            {
                oldMousePos = e.Location;
            }
            #endregion
        }

        private void pictureBoxPlot_MouseEnter(object sender, EventArgs e)
        {

        }

        private void pictureBoxPlot_MouseLeave(object sender, EventArgs e)
        {
            #region Очистка текста после выхода за пределы
            labelXvalue.Text = " ";
            labelYvalue.Text = " ";
            #endregion
        }

        double oldscaleX;
        double oldscaleY;

        private void pictureBoxPlot_MouseMove(object sender, MouseEventArgs e)
        {

            // Стираем старую рамку
            this.pictureBoxPlot.Refresh();
            #region Рисование рамки для функции приближения
            if (e.Button == MouseButtons.Middle)
            {
                // Получаем размеры прямоугольника рамки
                int width = Math.Abs(e.Location.X - this.oldMousePos.X);
                int height = Math.Abs(e.Location.Y - this.oldMousePos.Y);
                // Проверяем, была ли преодолена "мертвая" зона
                // хотя бы в одном направлении
                if (width >= this.minPix || height >= this.minPix)
                {
                    // Определяем левую верхнюю вершину
                    // прямоугольника рамки
                    int x = Math.Min(e.Location.X,
                    this.oldMousePos.X);
                    int y = Math.Min(e.Location.Y,
                    this.oldMousePos.Y);
                    // Рисуем рамку на форме, не затрагивая
                    // содержимое буфера
                    using (Graphics g =
                    this.pictureBoxPlot.CreateGraphics())
                    {
                        using (Pen pen = new Pen(Color.Black))
                        {
                            pen.DashStyle = DashStyle.Dot;
                            g.DrawRectangle(pen, x, y, width, height);
                        }
                    }
                }
            }
            #endregion
            #region Вывод координат курсора
            float x1 = (e.Location.X - this.centerPoint.X) * ((float)oldscaleX / (float)scaleX);
            float y1 = (-e.Location.Y + this.centerPoint.Y) * ((float)oldscaleY / (float)scaleY);
            //int x2 =((e.Location.X-this.centerPoint.X)*(int)oldscaleX) / ((int)scaleX);
            // int y2 = ((this.centerPoint.Y-e.Location.Y)*(int)oldscaleY) / ((int)scaleY);
            //int x3 = e.Location.X - this.centerPoint.X;
            //int y3 = this.centerPoint.Y - e.Location.Y;
            labelXvalue.Text = x1 + " ";
            labelYvalue.Text = y1 + " ";
            #endregion
        }

        private void pictureBoxPlot_MouseUp(object sender, MouseEventArgs e)
        {
            #region Изменение масштаба: приближение по рамке или клику
            if (e.Button == MouseButtons.Middle)
            {
                // Получаем размеры выделенной области
                int width = Math.Abs(e.Location.X - this.oldMousePos.X);
                int height = Math.Abs(e.Location.Y - this.oldMousePos.Y);
                if (width < this.minPix || height < this.minPix)
                {
                    if (width < this.minPix && height < this.minPix)
                    {
                        // Точечное действие - приближение по клику
                        this.SetCamera(e.Location, 2f * this.scaleX, 2f * this.scaleY);
                    }
                    else
                    {
                        // Ничего не делаем и стираем рамку
                        this.pictureBoxPlot.Refresh();
                    }
                }
                else
                {
                    // Приближение по рамке:
                    // Получаем коэффициенты изменения масштаба
                    // по осям
                    double kx = (double)this.pictureBoxPlot.Width /
                    (double)width;
                    double ky = (double)this.pictureBoxPlot.Height /
                    (double)height;

                    // Вычисляем координаты центра выделенной области
                    int x = (int)Math.Round(0.5d * (e.Location.X +
                    this.oldMousePos.X),
                    MidpointRounding.AwayFromZero);
                    int y = (int)Math.Round(0.5d * (e.Location.Y +
                    this.oldMousePos.Y),
                    MidpointRounding.AwayFromZero);
                    Point center = new Point(x, y);
                    // Устанавливаем камеру в центр выделенной области
                    this.SetCamera(center, kx * this.scaleX,
                    ky * this.scaleY);
                }
            }
            #endregion
            #region Изменение масштаба: отдаление по клику
            if (e.Button == MouseButtons.Right)
            {
                // Точечное действие - отдаление по клику
                this.SetCamera(e.Location, 0.5f * this.scaleX, 0.5f * this.scaleY);
            }
            #endregion
            #region Перетаскивание по левому клику: отпуск мыши
            if (e.Button == MouseButtons.Left)
            {
                //x, y - разность между положением отжатия и нажатия
                int x = e.Location.X - this.oldMousePos.X;
                int y = e.Location.Y - this.oldMousePos.Y;
                //Point center = new Point(x + this.centerPoint.X, y+ this.centerPoint.Y);
                //this.SetCamera(center, this.scaleX, this.scaleY);
                this.centerPoint.X += x;
                this.centerPoint.Y += y;
                // Сохраняем размеры области рисования
                this.oldSize = this.pictureBoxPlot.Size;
                // Обновляем содержимое буфера
                this.Draw();
            }
            #endregion
        }

        private void pictureBoxPlot_MouseWheel(object sender, MouseEventArgs e)
        {
            // Получаем коэффициент изменения масштаба
            double k = Math.Pow(2d,
            e.Delta / SystemInformation.MouseWheelScrollDelta);
            // Устанавливаем камеру в точку расположения курсора
            this.SetCamera(e.Location, k * this.scaleX,
            k * this.scaleY);
            // Передача фокуса для корректной работы
            // события MouseWheel
            this.pictureBoxPlot.Focus();

        }

        private void buttonLegend_Click(object sender, EventArgs e)
        {
            if (this.panelLegend.Visible)
            {
                this.panelLegend.Visible = false;
                this.buttonLegend.Text = @"/\";
            }
            else
            {
                this.panelLegend.Visible = true;
                this.buttonLegend.Text = @"\/";
            }
        }

        /// <summary>
        /// Обновляет состояние панели "Легенда".
        /// </summary>
        private void SetLegendPanel()
        {
            // Очищаем таблицу
            this.dataGridViewLegend.Rows.Clear();
            // Применяем настройки видимости
            if (this.items.Count == 0)
            {
                // Если графиков нет, то скрываем
                this.panelLegend.Visible = false;
                this.buttonLegend.Visible = false;
                this.buttonLegend.Text = @"/\";
            }
            else
            {
                // Если графики есть, то отображаем
                this.panelLegend.Visible = true;
                this.buttonLegend.Visible = true;
                this.buttonLegend.Text = @"\/";
            }
            // Запрещаем пользователю менять размеры ячеек
            this.dataGridViewLegend.AllowUserToResizeColumns =
            false;
            this.dataGridViewLegend.AllowUserToResizeRows = false;
            // Устанавливаем размеры панели и таблицы
            // в зависимости от количества графиков
            int rowHeight = 20; // высота ячейки
            this.dataGridViewLegend.Height =
            (rowHeight + 2) * this.items.Count + 3;
            this.panelLegend.Height =
            this.dataGridViewLegend.Height + 22;
            this.panelLegend.Location =
            new Point(this.pictureBoxPlot.Width - this.panelLegend.Width, this.pictureBoxPlot.Height - this.panelLegend.Height - 25);
            // Заполняем таблицу новыми данными
            this.dataGridViewLegend.RowCount = this.items.Count;
            int i = 0;
            foreach (KeyValuePair<string, PlotItem> pair in this.items)
            {
                // Выводим название графика в ячейку "Легенда"
                this.dataGridViewLegend["ChartLegend", i].Value = pair.Value.Legend;
                // Закрашиваем цветом графика ячейку "Цвет"
                Bitmap bitmap = new Bitmap(
                this.dataGridViewLegend["ChartColor", i].Size.Width,
                this.dataGridViewLegend["ChartColor", i].Size.Height);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.Clear(pair.Value.LineColor);
                }
                this.dataGridViewLegend["ChartColor", i].Value =
                bitmap;
                i++;
            }
        }

        private void dataGridViewLegend_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Проверяем, был ли клик по ячейке столбца "Цвет"
            if (e.ColumnIndex ==
             this.dataGridViewLegend.Columns.IndexOf(
             this.dataGridViewLegend.Columns["ChartColor"]))
            {
                // Получаем название графика
                // из активной строки таблицы
                string legend = this.dataGridViewLegend["ChartLegend",
                e.RowIndex].Value.ToString();
                // Получаем ссылку на элемент коллекции графиков
                PlotItem item = this.items[legend];
                // Показываем диалог "Цвет" с выбранным цветом графика
                ColorDialog dialog = new ColorDialog();
                dialog.Color = item.LineColor;
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Обновляем цвет графика из диалога "Цвет"
                    item.LineColor = dialog.Color;
                    // Закрашиваем цветом графика ячейку "Цвет"
                    Bitmap bitmap = new Bitmap(
                    this.dataGridViewLegend["ChartColor", e.RowIndex].Size.Width, this.dataGridViewLegend["ChartColor", e.RowIndex].Size.Height);
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.Clear(item.LineColor);
                    }
                    this.dataGridViewLegend["ChartColor", e.RowIndex].Value = bitmap;
                }
                // Обновляем содержимое буфера
                this.Draw();
            }
        }
    }
}
