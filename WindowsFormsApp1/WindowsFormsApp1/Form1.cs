using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private bool dragging = false; // Флаг, указывающий, перетаскивается ли форма
        private Point dragCursorPoint; // Точка курсора
        private Point dragFormPoint;
        private PointF[] points;
        public Form1()
        {
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.ClientSize = new Size(800, 800); // Установка размеров формы
            this.Text = "Фиолетовая звезда"; // Установка заголовка формы
                                           // Установка формы круглой
            SetFormToStar();
            // Подписка на событие Paint
            this.Paint += new PaintEventHandler(MainForm_Paint);

            this.MouseDown += new MouseEventHandler(Form1_MouseDown);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            this.MouseUp += new MouseEventHandler(Form1_MouseUp);

        }

        private void SetFormToStar()
        {
            int n = 5;               
            double R = 115, r = 300;   
            double alpha = -18;
            double x0 = this.ClientSize.Width / 2; 
            double y0 = this.ClientSize.Height / 2;
            points = new PointF[2 * n + 1];

            double a = alpha * Math.PI / 180.0, da = Math.PI / n, l;
            for (int k = 0; k < 2 * n + 1; k++)
            {
                l = k % 2 == 0 ? r : R;
                points[k] = new PointF((float)(x0 + l * Math.Cos(a)), (float)(y0 + l * Math.Sin(a)));
                Console.WriteLine($"Точка {k + 1}: ({points[k].X}+, {points[k].Y})");
                a += da;
            }
            GraphicsPath path = new GraphicsPath();
            path.AddPolygon(points);
            this.Region = new Region(path);
            

        }
        private void MainForm_Paint(object sender, PaintEventArgs e)
        {
            Brush solidBrush = Brushes.DarkViolet;
            int n = 5;
            double R = 115.115, r = 300.3;
            double alpha = -18;
            double x0 = this.ClientSize.Width / 2;
            double y0 = this.ClientSize.Height / 2;
            points = new PointF[2 * n + 1];

            double a = alpha * Math.PI / 180.0, da = Math.PI / n, l;
            for (int k = 0; k < 2 * n + 1; k++)
            {
                l = k % 2 == 0 ? r : R;
                points[k] = new PointF((float)(x0 + l * Math.Cos(a)), (float)(y0 + l * Math.Sin(a)));
                Console.WriteLine($"Точка {k + 1}: ({points[k].X}, {points[k].Y})");
                a += da;
            }
            e.Graphics.FillPolygon(solidBrush, points);

        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // Проверяем, нажата ли левая кнопка мыши
            {
                dragging = true; // Устанавливаем флаг перетаскивания
                dragCursorPoint = Cursor.Position; // Получаем текущую позицию курсора
                dragFormPoint = this.Location; // Получаем текущую позицию формы
            }
        }
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (dragging) // Если форма перетаскивается
            {
                Point dif = Point.Subtract(Cursor.Position, new Size(dragCursorPoint)); // 
                this.Location = Point.Add(dragFormPoint, new Size(dif)); // Устанавливаем
            }
        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) // Проверяем, отпущена ли левая кнопка мыши
            {
                dragging = false; // Сбрасываем флаг перетаскивания
            }
        }
    }
}
