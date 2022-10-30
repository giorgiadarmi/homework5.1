using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace homeW5._1
{
    public partial class Form1 : Form
    {

        private Rectangle rectangle;
        private Pen rectangleBorder;
        private Point firstPointDrag;
        public List<int> distribution_list = new List<int> { 15, 7, 11, 5, 11, 22, 13, 3 };
        public Random random;

        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            this.UpdateStyles();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            rectangle = new Rectangle(100, 100, 300, 300);
            rectangleBorder = Pens.Black;
            this.timer1.Start();
            this.timer1.Interval = 1;
            this.pictureBoxRectangle.Image = new Bitmap(pictureBoxRectangle.Width, pictureBoxRectangle.Height);
        }

        private int FromXRealToXVirtual(int X, int minX, int maxX, int W)
        {
            return W * (X - minX) / (maxX - minX);
        }

        private int find_max(List<int> list)
        {
            int max = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] > max)
                {
                    max = list[i];
                }
            }
            return max + 1;
        }

        private void drawHistogramVertical(Graphics g, int x, int interval, int start)
        {
            SolidBrush new_brush = new SolidBrush(Color.FromArgb(128, Color.Green));
            SolidBrush new_brush_border = new SolidBrush(Color.FromArgb(150, Color.DarkSeaGreen));
            for (int i = 0; i < distribution_list.Count; i++)
            {
                int new_height = FromXRealToXVirtual(distribution_list[i], 0, find_max(distribution_list), rectangle.Width);
                Rectangle rect_insto = new Rectangle(x, start, new_height, interval - 1);
                start += interval;
                g.DrawRectangle(new Pen(new_brush_border, 2), rect_insto);
                g.FillRectangle(new_brush, rect_insto);
            }

        }

        private void drawHistogramHorizontal(Graphics g, int y, int interval, int start)
        {
            SolidBrush new_brush = new SolidBrush(Color.FromArgb(128, Color.Blue));
            SolidBrush new_brush_border = new SolidBrush(Color.FromArgb(150, Color.BlueViolet));
            for (int i = 0; i < distribution_list.Count; i++)
            {
                int new_height = FromXRealToXVirtual(distribution_list[i], 0, find_max(distribution_list), rectangle.Height);
                Rectangle rect_insto = new Rectangle(start, y + (rectangle.Height - new_height), interval - 1, new_height);
                start += interval;
                g.DrawRectangle(new Pen(new_brush_border, 2), rect_insto);
                g.FillRectangle(new_brush, rect_insto);
            }

        }


        // Every tick seconds redraw the rectangle
        private void DrawRectangle(object sender, EventArgs e)
        {
            var g = Graphics.FromImage(pictureBoxRectangle.Image);
            g.Clear(this.BackColor);
            g.DrawRectangle(rectangleBorder, rectangle);
            g.FillRectangle(Brushes.White, rectangle);

            int number_of_interval_vertical = (rectangle.Height / distribution_list.Count);
            int number_of_interval_horizontal = (rectangle.Width / distribution_list.Count);
            drawHistogramVertical(g, rectangle.X, number_of_interval_vertical, rectangle.Top);
            drawHistogramHorizontal(g, rectangle.Y, number_of_interval_horizontal, rectangle.Left);
            this.pictureBoxRectangle.Refresh();
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            DrawRectangle(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            random = new Random();
            for (int i = 0; i < distribution_list.Count; i++)
            {
                distribution_list[i] = random.Next(1, 50);
                Console.WriteLine(distribution_list[i]);
            }

        }
    }

}
