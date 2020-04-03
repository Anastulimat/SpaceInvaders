using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Vecteur2D
    {
        /// <summary>
        /// Création d'un vecteur 2D qui représente la position en X et Y
        /// </summary>
        /// <param name="x">Position sur l'axe des abscisses</param>
        /// <param name="y">Position sur l'axe des ordonnées</param>
        public Vecteur2D(double x, double y)
        {
            X = x;
            Y = y;
        }

        public double X { get; set; }

        public double Y { get; set; }

        
        public double Norme
        {
            get
            {
                return Math.Sqrt((X * X) + (Y * Y));
            }
        }

        public static Vecteur2D operator+(Vecteur2D v1, Vecteur2D v2)
        {
            return new Vecteur2D(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vecteur2D operator-(Vecteur2D v1, Vecteur2D v2)
        {
            return new Vecteur2D(v1.X - v2.X, v1.Y - v2.Y);
        }
        
        public static Vecteur2D operator-(Vecteur2D v)
        {
            return new Vecteur2D(-v.X, -v.Y);
        }

        public static Vecteur2D operator*(Vecteur2D v, double d)
        {
            return new Vecteur2D(v.X * d, v.Y * d);
        }

        public static Vecteur2D operator*(double d, Vecteur2D v)
        {
            return new Vecteur2D(v.X * d, v.Y * d);
        }

        public static Vecteur2D operator/(Vecteur2D v, double d)
        {
            return new Vecteur2D(v.X / d, v.Y / d);
        }
    }
}               
