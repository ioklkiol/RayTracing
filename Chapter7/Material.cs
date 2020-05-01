using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Material
{
    private Vector3D _albedo;       //衰减三元组，决定每次碰撞之后光线的衰减
    public Vector3D Albedo { get => _albedo; set => _albedo = value; }

    protected Material(Vector3D albedo)
    {
        Albedo = albedo;
    }
    public abstract bool Scatter(Ray rIn, HitRecord rec, out Vector3D attenuation, out Ray scattered);

    //得到反射光线
    public Vector3D Reflect(Vector3D v, Vector3D n)
    {
        return v - 2 * v * n * n;
    }

    private static double RandomDouble()
    {
        var seed = Guid.NewGuid().GetHashCode();
        Random r = new Random(seed);
        int i = r.Next(0, 100000);
        return (double)i / 100000;
    }
    public static Vector3D RandomInUnitShpere()
    {
        Vector3D p = new Vector3D();
        do
        {
            p = 2 * new Vector3D(RandomDouble(), RandomDouble(), RandomDouble()) - new Vector3D(1, 1, 1);
        } while (p.SquaredMagnitude() >= 1);
        return p;
    }
}
//Lambert材质
public class Lambertian : Material
{
    public Lambertian(Vector3D albedo) : base(albedo)
    {
    }

    public override bool Scatter(Ray rIn, HitRecord rec, out Vector3D attenuation, out Ray scattered)
    {
        Vector3D target = rec.p + rec.normal + RandomInUnitShpere();
        scattered = new Ray(rec.p, target - rec.p);
        attenuation = Albedo;
        return true;
    }
}
//金属材质
public class Metal : Material
{
    private double _fuzz;      //镜面模糊系数

    public double Fuzz { get => _fuzz; set => _fuzz = value; }

    public Metal(Vector3D albedo,double f) : base(albedo)
    {
        Fuzz = Math.Min(f, 1);
    }


    public override bool Scatter(Ray rIn, HitRecord rec, out Vector3D attenuation, out Ray scattered)
    {
        Vector3D reflected = Reflect(rIn.Direction.UnitVector(), rec.normal);
        scattered = new Ray(rec.p, reflected+Fuzz*RandomInUnitShpere());     //模糊镜面反射 = 镜面反射 + 模糊系数 * 单位球随机点漫反射
        attenuation = Albedo;
        return scattered.Direction * rec.normal > 0;


    }
}