using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Missile : SimpleObject
    {

        private double vitesse = 300;
         
        public Missile(float x, float y, int lives)
        {
            this.positionX = x;
            this.positionY = y;
            this.position = new Vecteur2D(positionX, positionY);
            this.lives = 3;
            this.image = SpaceInvaders.Properties.Resources.shoot1;
            this.Image = image;
        }

        public Vecteur2D Position { get; set; }

        public float PositionX { get; set; }

        public float PositionY { get; set; }

        public double Vitesse { get; set; }

        public int Lives { get; set; }

        public Bitmap Image { get; set; }

        public override void Update(Game gameInstance, double deltaT)
        {
            
            positionY -= (float) (vitesse * deltaT);
            if (positionY <= 0)
                lives = 0;


            foreach (GameObject gameObject in gameInstance.gameObjects)
            {
                 gameObject.Collision(this);
            }
        }
    }
}
