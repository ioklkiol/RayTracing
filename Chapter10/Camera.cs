using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Camera
{
    private Vector3D origin = new Vector3D(0, 0, 0);
    private Vector3D lowerLeft = new Vector3D(-2, 1, -1);
    private Vector3D horizontal = new Vector3D(4, 0, 0);
    private Vector3D vertical = new Vector3D(0, -2, 0);

    public Vector3D Origin { get => origin; set => origin = value; }
    public Vector3D LowerLeft { get => lowerLeft; set => lowerLeft = value; }
    public Vector3D Horizontal { get => horizontal; set => horizontal = value; }
    public Vector3D Vertical { get => vertical; set => vertical = value; }

    public Camera(double vfov,double aspect)
    {
        double theta = vfov * Math.PI / 180;        //vfov即相机在垂直方向上从屏幕顶端扫描到底部所岔开的视角角度
        double halfHeight = Math.Tan(theta / 2);    //aspect：屏幕宽高比
        double halfWidth = aspect * halfHeight;
        lowerLeft = new Vector3D(-halfWidth, halfHeight, -1);
        horizontal = new Vector3D(2*halfWidth,0, 0);
        vertical = new Vector3D(0, 2 * -halfHeight, 0);
        origin = new Vector3D(0, 0, 0);
    }
    public Camera()
    {

    }
    public Ray GetRay(double u, double v)
    {
        return new Ray(origin,lowerLeft + u * horizontal + v * vertical - origin);
    }


}