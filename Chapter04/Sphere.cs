using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Sphere : Hittable
{
    private Vector3D _center;
    private double _radius;

    public Vector3D Center { get => _center; set => _center = value; }
    public double Radius { get => _radius; set => _radius = value; }

    public Sphere(Vector3D cen, double r)
    {
        Center = cen;
        Radius = r;
    }

    public override bool Hit(Ray r, double tMin, double tMax,out HitRecord rec)
    {
        rec = new HitRecord();
        Vector3D oc = r.Origin - Center;
        double a = r.Direction * r.Direction;
        double b = oc * r.Direction;                //在这里我们只关注判别式discriminant=b*b-4ac是否大于0，将b*b和4ac同时除以了4
        double c = oc * oc - Radius * Radius;       //如果不同时除以4的话最后得到的color的值会不在0到255之间，则需要限定一下边界
        double discriminant = b * b -  a * c;
        if (discriminant > 0)
        {
            double temp = (-b - Math.Sqrt(discriminant)) / a;
            if (temp < tMax && temp > tMin)
            {
                rec.t = temp;
                rec.p = r.GetPoint(rec.t);
                rec.normal = (rec.p - Center) / Radius;
                return true;
            }
            temp = (-b + Math.Sqrt(discriminant)) / a;
            if (temp < tMax && temp > tMin)
            {
                rec.t = temp;
                rec.p = r.GetPoint(rec.t);
                rec.normal = (rec.p - Center) / Radius;
                return true;
            }

        }
        return false;
    }
}

