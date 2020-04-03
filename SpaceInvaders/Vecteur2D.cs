using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Vecteur2D
    {
        private double x, y;

        public Vecteur2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public double X { get; set; }

        public double Y { get; set; }

        public double Norme
        {
            get
            {
                return Math.Sqrt((this.x * this.x) + (this.y * this.y));
            }
        }

        public static Vecteur2D operator+(Vecteur2D v1, Vecteur2D v2)
        {
            return new Vecteur2D(v1.x + v2.x, v1.y + v2.y);
        }

        public static Vecteur2D operator-(Vecteur2D v1, Vecteur2D v2)
        {
            return new Vecteur2D(v1.x - v2.x, v1.y - v2.y);
        }
        
        public static Vecteur2D operator-(Vecteur2D v)
        {
            return new Vecteur2D(-v.x, -v.y);
        }

        public static Vecteur2D operator*(Vecteur2D v, double d)
        {
            return new Vecteur2D(v.x * d, v.y * d);
        }

        public static Vecteur2D operator*(double d, Vecteur2D v)
        {
            return new Vecteur2D(v.x * d, v.y * d);
        }

        public static Vecteur2D operator/(Vecteur2D v, double d)
        {
            return new Vecteur2D(v.x / d, v.y / d);
        }
    }
}               
