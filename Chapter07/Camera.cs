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

    public Camera(Vector3D origin, Vector3D lowerLeft, Vector3D horizontal, Vector3D vertical)
    {
        Origin = origin;
        LowerLeft = lowerLeft;
        Horizontal = horizontal;
        Vertical = vertical;
    }
    public Camera()
    {

    }
    public Ray GetRay(double u, double v)
    {
        return new Ray(origin,lowerLeft + u * horizontal + v * vertical - origin);
    }


}