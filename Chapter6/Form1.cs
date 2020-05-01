using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chapter6
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
        //在视点单位球中找一个随机点，返回这个点减去球心位置的单位向量，由于我们的视点就是在原点，所以省略了减去球心位置的过程
        //如果视点不在原点的话得到的随机向量会不正确,物体会变形
        public Vector3D RandomInUnitShpere()
        {
            Vector3D p = new Vector3D();
            do
            {
                //RandomDouble生成的是0~1的随机数，然后乘以2再减去1，得到的p的每一个分量均位于-1到1，其实它的范围是一个正方体，
                //但我们要求的是球内随机点，所以判断一下x*x+y*y+z*z是否大于1
                p = 2 * new Vector3D(RandomDouble(), RandomDouble(), RandomDouble()) - new Vector3D(1, 1, 1);
            } while (p.SquaredMagnitude() >= 1);       //如果x*x+y*y+z*z大于1，则说明这个点在正方体内但不在球内，需要重新找
            return p;
        }

        private Vector3D GetColor(Ray r, HittableList world)
        {
            HitRecord rec;
            if (world.Hit(r, 0, double.MaxValue, out rec))
            {
                Vector3D target = rec.p + rec.normal + RandomInUnitShpere( );   //击中点加法线向量得到击中点单位球的球心，球心加上
                                                                               //随机向量得到反射的方向。
                return 0.5 * GetColor(new Ray(rec.p, target - rec.p), world);
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
                    color = new Vector3D(Math.Sqrt(color.X), Math.Sqrt(color.Y), Math.Sqrt(color.Z));//进行伽马校正
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
