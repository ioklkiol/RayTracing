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

    public Camera(Vector3D lookFrom,Vector3D lookAt,Vector3D vup, double vfov,double aspect)
    {
        Vector3D u, v, w;
        double theta = vfov * Math.PI / 180;        //vfov即相机在垂直方向上从屏幕顶端扫描到底部所岔开的视角角度
        double halfHeight = Math.Tan(theta / 2);    //aspect：屏幕宽高比
        double halfWidth = aspect * halfHeight;

        origin = lookFrom;
        w = (lookFrom - lookAt).UnitVector();
        u = (Vector3D.Cross(vup, w)).UnitVector();
        v = Vector3D.Cross(w, u);

        lowerLeft = origin - halfWidth * u - halfHeight * v - w;
        horizontal = 2*halfWidth*u; 
        vertical = 2*halfHeight*v;
    }
    public Camera()
    {

    }
    public Ray GetRay(double u, double v)
    {
        return new Ray(origin,lowerLeft + u * horizontal + v * vertical - origin);
    }


}