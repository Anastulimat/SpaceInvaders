using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;

namespace SpaceInvaders
{
    abstract class SimpleObject : GameObject
    {
        protected int lives;
        protected float positionX;
        protected float positionY;
        protected Vecteur2D position;
        protected Bitmap image;

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            graphics.DrawImage(image, positionX, positionY, image.Width, image.Height);
        }

        public Vecteur2D Position { get; set; }

        public double Vitesse { get; set; }

        public int Lives { get; set; }

        public Bitmap Image { get; set; }

        public override bool IsAlive()
        {
            if (lives <= 0)
                return false;
            return true;
        }

        public override void Collision(Missile m)
        {
            //throw new NotImplementedException();
        }


    }
}
