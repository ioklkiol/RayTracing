using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Ray
{
    private Vector3D _origin;
    private Vector3D _direction;

    public Vector3D Origin { get => _origin; set => _origin = value; }
    public Vector3D Direction { get => _direction; set => _direction = value; }

    public Ray(Vector3D origin, Vector3D direction)
    {
        Origin = origin;
        Direction = direction;
    }
    //根据距离得到射线上的点
    public Vector3D GetPoint(double distance)
    {
        return Origin + distance * Direction;
    }
}
