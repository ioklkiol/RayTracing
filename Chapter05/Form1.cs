using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chapter5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //得到一个0到1的随机数
        private double RandomDouble()
        {
            var seed = Guid.NewGuid().GetHashCode();    //使用Guid类得到一个接近唯一的随机数种子
            Random r = new Random(seed);
            int i = r.Next(0, 100000);
            return (double)i / 100000;
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
            int nx = 200;
            int ny = 100;
            int ns = 100;                         //设置采样率抗锯齿，但渲染所需要的时间也变长了，获得随机数使用了大量时间,
                                                  //有时间的话可以优化一下函数RandomDouble
            Bitmap bmp = new Bitmap(nx, ny);
            Vector3D lowerLeft = new Vector3D(-2, 1, -1);
            Vector3D horizontal = new Vector3D(4, 0, 0);
            Vector3D vertical = new Vector3D(0, -2, 0);
            Vector3D origin = new Vector3D(0, 0, 0);

            List<Hittable> list = new List<Hittable>();
            list.Add(new Sphere(new Vector3D(0, 0, -1), 0.5));
            list.Add(new Sphere(new Vector3D(0, -100.5, -1), 100));
            HittableList world = new HittableList(list, 2);
            Camera cam = new Camera();
            for (int j = 0; j < ny; j++)
            {
                for (int i = 0; i < nx; i++)
                {
                    Vector3D color = new Vector3D(0, 0, 0);
                    for (int s = 0; s<ns; s++)
                    {
                        double u = (double)(i+RandomDouble()) / (double)nx;
                        double v = (double)(j+RandomDouble()) / (double)ny;
                        Ray ray = cam.GetRay(u,v);
                        color += GetColor(ray, world);      //将所有采样点的颜色相加
                    }
                    color /= ns;                            //除以采样点的数量得到平均值
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
