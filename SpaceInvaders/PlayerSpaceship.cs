using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class PlayerSpaceship : SpaceShip
    {
        public PlayerSpaceship(float x, float y, int lives) : base(x, y, lives) {}

        public override void Update(Game gameInstance, double deltaT)
        {
            if (gameInstance.keyPressed.Contains(Keys.Left))
            {
                positionX -= (float)(speedPixelPerSecond * deltaT);
                if (positionX <= 0)
                {
                    positionX = 0;
                }
            }

            if (gameInstance.keyPressed.Contains(Keys.Right))
            {
                positionX += (float)(speedPixelPerSecond * deltaT);
                if ((positionX + image.Width) >= gameInstance.gameSize.Width)
                {
                    positionX = gameInstance.gameSize.Width - image.Width;
                }
            }

            if (gameInstance.keyPressed.Contains(Keys.Space))
            {
                this.Shoot(gameInstance);
            }
        }
    }
}
