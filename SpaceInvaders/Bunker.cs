using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class Bunker : SimpleObject
    {
        public Bunker(float x, float y)
        {
            this.positionX = x;
            this.positionY = y;
            this.position = new Vecteur2D(positionX, positionY);
            this.image = SpaceInvaders.Properties.Resources.bunker;
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            //throw new NotImplementedException();
        }

        public override bool IsAlive()
        {
            return true;
        }

        public override void Collision(Missile m)
        {

        }
    }
}
