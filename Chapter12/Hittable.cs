using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public abstract class Hittable
{
   public abstract bool Hit(Ray r, double tMin, double tMax,out HitRecord rec);
} 

