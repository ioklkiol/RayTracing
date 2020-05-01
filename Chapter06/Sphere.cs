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
        double b = oc * r.Direction;                
        double c = oc * oc - Radius * Radius;       
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

