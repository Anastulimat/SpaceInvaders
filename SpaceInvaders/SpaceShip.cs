using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class SpaceShip : SimpleObject
    {
        protected double speedPixelPerSecond = 100;
        protected Missile missile;

        public SpaceShip(float x, float y, int lives) : base()
        {
            this.image = SpaceInvaders.Properties.Resources.ship3;
            this.positionX = x;
            this.positionY = y - image.Height; // Soustraction l'hauteur de l'image de la position y pour que l'affichage soit correct
            this.position = new Vecteur2D(positionX, positionY);
            this.lives = lives;
        }

        public Vecteur2D Position { get; set; }

        public int Lives { get; set; }

        public Bitmap Image { get; set; }


        public void Shoot(Game gamInstance)
        {
            if(this.missile == null || !(this.missile.IsAlive()))
            {
                Missile newMissile = new Missile(positionX + (image.Width/2), positionY + (image.Height/2), 3);
                this.missile = newMissile;
                gamInstance.AddNewGameObject(this.missile);
            }
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            //throw new NotImplementedException();
        }
    }
}
