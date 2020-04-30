using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chapter3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //判断光线是否与球有交点
        private bool HitSphere(Vector3D center, Double radius, Ray r)
        {
            Vector3D oc = r.Origin - center;
            double a = r.Direction * r.Direction;
            double b = 2 * oc * r.Direction;
            double c = oc * oc - radius * radius;
            double discriminant = b * b - 4 * a * c;     //当判别式小于零则没有交点，等于零时则相切，
            return discriminant > 0;                     //大于零时说明有两个交点，有两个交点时才能看到
        }
        private Vector3D GetColor(Ray r)
        {
            if (HitSphere(new Vector3D(0, 0, -1), 0.5, r))
                return new Vector3D(1, 0, 0);
            Vector3D unitDirection = r.Direction.UnitVector();  //球心到击中点的法向量
            double t = 0.5 * (unitDirection.Y + 1);
            return (1 - t) * new Vector3D(1, 1, 1) + t * new Vector3D(0.5, 0.7, 1);

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            int nx = 900;
            int ny = 500;
            Bitmap bmp = new Bitmap(nx, ny);
            Vector3D lowerLeft = new Vector3D(-2, 1, -1);       //这里的Y值和书上是相反的，因为坐标系不同
            Vector3D horizontal = new Vector3D(4, 0, 0);
            Vector3D vertical = new Vector3D(0, -2, 0);         //这里的Y值和书上是相反的，因为坐标系不同
            Vector3D origin = new Vector3D(0, 0, 0);
            for (int j = 0; j < ny; j++)
            {
                for (int i = 0; i < nx; i++)
                {
                    double u = (double)i / (double)nx;
                    double v = (double)j / (double)ny;
                    Ray ray = new Ray(origin, lowerLeft + u * horizontal + v * vertical);
                    Vector3D color = GetColor(ray);
                    int r = (int)(255 * color.X);
                    int g = (int)(255 * color.Y);
                    int b = (int)(255 * color.Z);
                    bmp.SetPixel(i, j, Color.FromArgb(r, g, b));
                }

            }
            pictureBox1.BackgroundImage = bmp;
        }
    }
}
