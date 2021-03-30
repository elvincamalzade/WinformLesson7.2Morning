using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinformLesson7._2
{
    public partial class Form1 : Form
    {
        IFactory FigureFactory { get; set; }
        public Form1()
        {
            InitializeComponent();

            List<string> figures = new List<string> { "TriAngle", "Circle", "Rectangle" };
            comboBox1.Items.AddRange(figures.ToArray());
            comboBox1.SelectedIndex = 2;

        }
        List<IFigure> Figures = new List<IFigure>();
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var item = comboBox1.SelectedItem.ToString();
            if (item == "TriAngle")
            {
                FigureFactory = new TriAngleFactory();
            }
            else if (item == "Rectangle")
            {
                FigureFactory = new RectangleFactory();
            }
            else if (item == "Circle")
            {
                FigureFactory = new CircleFactory();
            }
        }
        public Color FigureColor { get; set; }
        private void button1_Click(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            var result = colorDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                FigureColor = colorDialog.Color;
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
           if(FigureFactory.GetFigure() is Rectangle rect)
            {
                rect.Color = FigureColor;
                rect.Point = e.Location;
                rect.Size = new Size(int.Parse(widthTxtb.Text), int.Parse(heightTxtb.Text));


                rect.isFilled = false;

                Figures.Add(rect);
            }

            this.Refresh();

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Pen pen = new Pen(FigureColor, 3);
            SolidBrush brush = new SolidBrush(FigureColor);

            using (var a=e.Graphics)
            {
                foreach (var item in Figures)
                {
                    if(item is Rectangle rect)
                    {
                        if (rect.isFilled)
                        {
                            a.FillRectangle(brush, rect.Point.X, rect.Point.Y, rect.Size.Width, rect.Size.Height);
                        }
                        else
                        {
                            a.DrawRectangle(pen, rect.Point.X, rect.Point.Y, rect.Size.Width, rect.Size.Height);
                        }
                    }
                }
               
            }

        }
    }



    interface IFigure
    {
        Point Point { get; set; }
        Size Size { get; set; }
        Color Color { get; set; }
        bool isFilled { get; set; }
    }


    class Rectangle : IFigure
    {
        public Point Point { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; }
        public bool isFilled { get; set; }
    }

    class TriAngle : IFigure
    {
        public Point Point { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; }
        public bool isFilled { get; set; }
    }

    class Circle : IFigure
    {
        public Point Point { get; set; }
        public Size Size { get; set; }
        public Color Color { get; set; }
        public bool isFilled { get; set; }
    }


    interface IFactory
    {
        IFigure GetFigure();
    }


    class CircleFactory : IFactory
    {
        public IFigure GetFigure()
        {
            return new Circle();
        }
    }

    class RectangleFactory : IFactory
    {
        public IFigure GetFigure()
        {
            return new Rectangle();
        }
    }


    class TriAngleFactory : IFactory
    {
        public IFigure GetFigure()
        {
            return new TriAngle();
        }
    }


}
