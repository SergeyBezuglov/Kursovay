using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Coursework
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedItem = "Дирихле"; //по умолчанию выбрано условие Дирихле (первого рода)
            radioButton1.Checked = true; //по умолчанию выбрано отсутствие источника
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if((string)this.comboBox1.SelectedItem == "Робина") //если выбраны условия Робина, показывает в интерфейсе элементы для их задания
            {
                textBox1.Show();
                textBox2.Show();
                textBox3.Show();
                textBox4.Show();
                label1.Show();
                label2.Show();
                label3.Show();
                label4.Show();
            }
            else //в противном случае скрывает их
            {
                textBox1.Hide();
                textBox2.Hide();
                textBox3.Hide();
                textBox4.Hide();
                label1.Hide();
                label2.Hide();
                label3.Hide();
                label4.Hide();
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton1.Checked == true) //если нет источника, убирает элементы, связанные с ним
            {
                label6.Hide();
                textBox5.Hide();
                label7.Hide();
                textBox6.Hide();
            }
            else //в противном случае показывает
            {
                label6.Show();
                textBox5.Show();
                label7.Show();
                textBox6.Show();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int i, j; // вспомогательные величины для циклов
            int Nx = 11, Ny = 11; //число шагов по x и y плюс 1, т.е. 10 
            int M = 100; //число шагов по времени
            int Ni=1; //номер точки, в которой расположен источник, изначально 1
            double[][] u = new double[Nx][]; //двумерная матрица
            for (i = 0; i < Nx; i++)
            {
                u[i] = new double[Ny];
            }
            double[] alfaX = new double[Nx]; //прогоночный коэффициент альфа по x
            double[] betaX = new double[Nx]; //прогоночный коэффициент бета по x
            double[] alfaY = new double[Ny]; //прогоночный коэффициент альфа по y
            double[] betaY = new double[Ny]; //прогоночный коэффициент бета по y
            double a, //нижняя диагональ матрицы
                b, //центральная диагональ матрицы
                c, //верхняя диагональ матрицы
                f; //правая часть СЛАУ
            double hx, //длина шага по x
                hy, //длина шага по y
                tau, //количество шага по времени
                t_end = 3, //значение времени, до которого проводятся вычисления
                time; //вспомогательная величина для счёта времени
            double L = 1, H = 1; //общая длина отрезка по X и Y
            double k1 = 1, k2 = 1, //коэффициенты теплоотдачи, 1 - для x, 2 - для y
                ux = 1, uy = 1; //температуры окр. среды
            double q = 1; //коэффициент для источника

            if ((string)this.comboBox1.SelectedItem == "Робина") //считыванние значений для граничных условий третьего рода, если они есть
            {
                k1 = double.Parse(textBox1.Text);
                k2 = double.Parse(textBox2.Text);
                ux = double.Parse(textBox3.Text);
                uy = double.Parse(textBox4.Text);
            }

            if (radioButton1.Checked == false) //считывание значений для источника, если он есть
            {
                Ni = int.Parse(textBox5.Text);
                q = double.Parse(textBox6.Text);
            }

            // вычисление длины шагов по x и y
            hx = L / (Nx - 1);
            hy = H / (Ny - 1);

            // вычисление шага по времени
            tau = t_end / M;
            
            //объявление вспомогательных матриц, хранящих в себе длины отрезков x и y, разделённые на число шагов
            double[] x = new double[Nx]; 
            double[] y = new double[Ny];
            for (i = 0; i < Nx; i++)
            {
                x[i] = i * hx;
            }
            for (i = 0; i < Ny; i++)
            {
                y[i] = i * hy;
            }

            //ввод значения двумерной матрицы в начальный момент времени t=0
            for (i = 0; i < Nx; i++)
                for (j = 0; j < Ny; j++)
                    u[i][j] = Math.Sin(0.5*Math.PI * x[i]) + Math.Sin(0.5*Math.PI * y[j]);

            //объявление массивов точек, которые будут заполняться значениями матрицы в разные моменты времени
            List<PointF> points1 = new List<PointF>(new PointF[] { });
            List<PointF> points2 = new List<PointF>(new PointF[] { });
            List<PointF> points3 = new List<PointF>(new PointF[] { });
            List<PointF> points4 = new List<PointF>(new PointF[] { });
            List<PointF> points5 = new List<PointF>(new PointF[] { });
            List<PointF> points6 = new List<PointF>(new PointF[] { });
            List<PointF> points7 = new List<PointF>(new PointF[] { });
            List<PointF> points8 = new List<PointF>(new PointF[] { });

            //считывание введённого в интерфейсе программы номера шага по y
            int jY = (int)numericUpDown1.Value;
            //запоминание графика в начальный момент времени
            for (i = 0; i < Nx; i++)
            {
                points1.Add(new PointF((float)(x[i]), (float)u[i][jY]));
            }

            int counter = 0;
            time = 0;

            //начало прогонки, цикл проверяет, достигнут ли конечный момент времени
            while (time < t_end)
            {
                //учвеличение времени на шаг τ
                time += tau;
                counter++;
                //решение СЛАУ в направлении x для определения значения на промежуточном временном слое
                for (j = 0; j < Ny; j++)
                {
                    //определение начальных коэффициентов прогонки на основе левого граничного условия
                    if ((string)this.comboBox1.SelectedItem == "Робина") //если выбраны граничные условия Робина
                    {
                        alfaX[0] = 2.0 * tau / (2.0 * tau * (1 + k1 * hx) + hx * hx);
                        betaX[0] = (hx * hx * u[0][j] + 2.0 * tau * k1 * hx * ux) / (2.0 * tau * (1 + k1 * hx) + hx * hx);
                    }
                    else //если выбраны условия Дирихле
                    {
                        alfaX[0] = 0.0;
                        betaX[0] = 0.0;
                    }

                    //цикл для определения коэффициентов прогонки
                    for (i = 1; i < Nx - 1; i++)
                    {
                        a = 1f / (hx * hx);
                        b = 2.0 / (hx * hx) + 1f / tau;
                        c = 1f / (hx * hx);
                        f = -u[i][j] / tau;

                        //если есть источник, добавляет его к правой части f
                        if (radioButton2.Checked == true)
                        {
                            if (j == Ni)
                                f = -u[i][j] / tau - hx * (j) * q;
                        }
                            

                        alfaX[i] = a / (b - c * alfaX[i - 1]);
                        betaX[i] = (c * betaX[i - 1] - f) / (b - c * alfaX[i - 1]);
                    }

                    //определение значения на правой границе на основе правого граничного условия
                    if ((string)this.comboBox1.SelectedItem == "Робина") //если выбрано условие Робина
                    {
                        u[Nx - 1][j] = (hx * hx * u[Nx - 1][j] + 2.0 * tau * (betaX[Nx - 2] + k2 * hx * uy)) /
                           (hx * hx + 2.0 * tau * ((1 - alfaX[Nx - 2]) + k2 * hx));
                    }
                    else //если Дирихле
                    {
                        u[Nx - 1][j] = 0.0;
                    }

                    //обратный ход прогонки,определяются неизвестные значения матрицы на промежуточном временном слое
                    for (i = Nx - 2; i >= 0; i--)
                        u[i][j] = alfaX[i] * u[i + 1][j] + betaX[i];
                }

                //решение СЛАУ в направлении y для определения значения на полном временном слое
                for (i = 1; i < Nx - 1; i++)
                {
                    //определение начальных коэффициентов прогонки на основе левого граничного условия
                    if ((string)this.comboBox1.SelectedItem == "Робина") //если выбраны граничные условия Робина
                    {
                        alfaY[0] = 2.0 * tau / (2.0 * tau * (1 + k1 * hy) + hy * hy);
                        betaY[0] = (hy * hy * u[i][0] + 2.0 * tau * k1 * hy * ux) / (2.0 * tau * (1 + k1 * hy) + hy * hy);
                    }
                    else //если выбраны условия Дирихле
                    {
                        alfaY[0] = 0.0;
                        betaY[0] = 0.0;
                    }

                    //цикл для определения коэффициентов прогонки
                    for (j = 1; j < Ny - 1; j++)
                    {
                        a = 1f / (hy * hy);
                        b = 2.0 / (hy * hy) + 1f / tau;
                        c = 1f / (hy * hy);
                        f = -u[i][j] / tau;

                        //если есть источник, добавляет его к правой части f
                        if (radioButton2.Checked == true)
                        {
                            if (i == Ni)
                                f = -u[i][j] / tau - hy * (i) * q;
                        }
                        
                        alfaY[j] = a / (b - c * alfaY[j - 1]);
                        betaY[j] = (c * betaY[j - 1] - f) / (b - c * alfaY[j - 1]);
                    }

                    //определение значения на правой границе на основе правого граничного условия
                    if ((string)this.comboBox1.SelectedItem == "Робина") //если выбрано условие Робина
                    {
                        u[i][Ny - 1] = (hy * hy * u[i][Ny - 1] + 2.0 * tau * (betaY[Ny - 2] + k2 * hy * uy)) /
                            (hy * hy + 2.0 * tau * ((1 - alfaY[Ny - 2]) + k2 * hy));
                    }
                    else //если Дирихле
                    {
                        u[i][Ny - 1] = 0.0;
                    }

                    //обратный ход прогонки,определяются неизвестные значения матрицы на полном временном слое
                    for (j = Ny - 2; j >= 0; j--)
                        u[i][j] = alfaY[j] * u[i][j + 1] + betaY[j];
                }
                //запоминание значений матрицы для построения графиков в моменты времени, когда достигнут 1, 5, 10, 20, 50, 75, 100 шаги по времени
                switch (counter)
                {
                    case 1:
                        for (i = 0; i < Nx; i++) { points2.Add(new PointF((float)(x[i]), (float)u[i][jY]));}
                        break;
                    case 5:
                        for (i = 0; i < Nx; i++) { points3.Add(new PointF((float)(x[i]), (float)u[i][jY]));}
                        break;
                    case 10:
                        for (i = 0; i < Nx; i++) { points4.Add(new PointF((float)(x[i]), (float)u[i][jY]));}
                        break;
                    case 20:
                        for (i = 0; i < Nx; i++) { points5.Add(new PointF((float)(x[i]), (float)u[i][jY]));}
                        break;
                    case 50:
                        for (i = 0; i < Nx; i++) { points6.Add(new PointF((float)(x[i]), (float)u[i][jY]));}
                        break;
                    case 75:
                        for (i = 0; i < Nx; i++) { points7.Add(new PointF((float)(x[i]), (float)u[i][jY]));}
                        break;
                    case 100:
                        for (i = 0; i < Nx; i++) { points8.Add(new PointF((float)(x[i]), (float)u[i][jY]));}
                        break;
                }

            }

            //вывод запомненых значений в графики
            this.plot1.UpdateItem("T=0", Color.Blue, points1);
            this.plot1.UpdateItem("T=0.03", Color.Purple, points2);
            this.plot1.UpdateItem("T=0.15", Color.Green, points3);
            this.plot1.UpdateItem("T=0.3", Color.Red, points4);
            this.plot1.UpdateItem("T=0.6", Color.DarkGreen, points5);
            this.plot1.UpdateItem("T=1.5", Color.DarkRed, points6);
            this.plot1.UpdateItem("T=2.25", Color.DarkBlue, points7);
            this.plot1.UpdateItem("T=3", Color.DarkMagenta, points8);
            this.plot1.Draw(); //обновление графиков
        }
    }
}
