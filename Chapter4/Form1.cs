using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chapter4
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private Vector3D GetColor(Ray r, HittableList world)
        {
            HitRecord rec;
            if (world.Hit(r, 0, double.MaxValue, out rec))
            {
                return 0.5 * new Vector3D(rec.normal.X + 1, rec.normal.Y + 1, rec.normal.Z + 1);
            }
            else
            {
                Vector3D unitDirection = r.Direction.UnitVector();
                double t = 0.5 * (unitDirection.Y + 1);
                return (1 - t) * new Vector3D(1, 1, 1) + t * new Vector3D(0.5, 0.7, 1);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            int nx = 900;
            int ny = 500;
            Bitmap bmp = new Bitmap(nx, ny);
            Vector3D lowerLeft = new Vector3D(-2, 1, -1);
            Vector3D horizontal = new Vector3D(4, 0, 0);
            Vector3D vertical = new Vector3D(0, -2, 0);
            Vector3D origin = new Vector3D(0, 0, 0);

            List<Hittable> list = new List<Hittable>();
            list.Add(new Sphere(new Vector3D(0, 0, -1), 0.5));
            list.Add(new Sphere(new Vector3D(0, -100.5, -1), 100));
            HittableList world = new HittableList(list, 2)
;
            for (int j = 0; j < ny; j++)
            {
                for (int i = 0; i < nx; i++)
                {
                    double u = (double)i / (double)nx;
                    double v = (double)j / (double)ny;
                    Ray ray = new Ray(origin, lowerLeft + u * horizontal + v * vertical);
                    Vector3D p = ray.GetPoint(2);
                    Vector3D color = GetColor(ray, world);    
                    int r = (int)(255 * color.X);
                    int g = (int)(255 * color.Y);
                    int b = (int)(255 * color.Z);
                    bmp.SetPixel(i, j, Color.FromArgb(r, g, b));   //如果之前没有将判别式除以4的话这里会报错，则需要限定一下边界，
                                                                   //将rgb的值限制在0到255之间，会得到一个和书上不一样的球
                }

            }
            pictureBox1.BackgroundImage = bmp;
        }
    }
}
