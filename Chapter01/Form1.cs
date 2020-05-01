using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Chapter1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            int nx = 900;
            int ny = 450;
            Bitmap bmp = new Bitmap(nx, ny);
            for (int j = 0; j < ny; j++)
            {
                for (int i = 0; i < nx; i++)
                {
                    int r = i * 255 / nx;
                    int g = 255-j * 255 / ny;
                    int b = (int)(0.2 * 255);
                    Color color = Color.FromArgb(r, g, b);
                    bmp.SetPixel(i, j, color);
                }
            }
            pictureBox1.BackgroundImage = bmp;
        }
    }
}
