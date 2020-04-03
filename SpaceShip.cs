using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class SpaceShip : SimpleObject
    {
        public double SpaceShipSpeedPixelPerSecond = ConstantsDeJeu.SpaceShipSpeedPixelPerSecond;
        public bool rightDirection = false;

        protected Missile missile;

        /// <summary>
        /// Constructeur SpaceShip
        /// </summary>
        /// <param name="x">Position en X</param>
        /// <param name="y">Position en Y</param>
        /// <param name="lives">La vie du vaisseau</param>
        /// <param name="side">Le camps de vaisseau</param>
        public SpaceShip(float x, float y, int lives, Side side) : base(side)
        {
            Position = new Vecteur2D(x, y);
            Lives = lives;
            Side = side;
        }


        /// <summary>
        /// Fonction de tire du vaisseau
        /// </summary>
        /// <param name="gamInstance">Instance du jeu</param>
        public void Shoot(Game gamInstance)
        {
            if(this.missile == null || !(this.missile.IsAlive()))
            {
                Missile newMissile;
                if(Side == Side.Ally)
                {
                    newMissile = new Missile((float)(Position.X + (Image.Width / 2)), (float)(Position.Y + (Image.Height / 2)), 10, SpaceInvaders.Properties.Resources.shoot1, Side)
                    {
                        upDirection = true
                    };
                }
                else
                {
                    newMissile = new Missile((float)(Position.X + (Image.Width / 2)), (float)(Position.Y + (Image.Height / 2)), 5, SpaceInvaders.Properties.Resources.shoot2, Side);
                }
                missile = newMissile;
                gamInstance.AddNewGameObject(this.missile);
            }
        }

        /// <summary>
        /// Update vaisseau
        /// </summary>
        /// <param name="gameInstance">>Instance du jeu</param>
        /// <param name="deltaT">Nombre de mili sconde écoulées depuis le dernier update</param>
        public override void Update(Game gameInstance, double deltaT)
        {
            if (!rightDirection)
            {
                Position.X += SpaceShipSpeedPixelPerSecond * deltaT;
            }

            if (rightDirection)
            {
                Position.X -= SpaceShipSpeedPixelPerSecond * deltaT;
            }
        }

        /// <summary>
        /// Lors d'un collision retirer le minimum de vie entre le missile et le vaisseau
        /// </summary>
        /// <param name="m">Objet misile</param>
        /// <param name="numberOfPixelsInCollision">Nombre de pixel en collision</param>
        protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
        {
            int numbreMinOfLifes = Math.Min(this.Lives, m.Lives);
            Lives -= numbreMinOfLifes;
            m.Lives -= numbreMinOfLifes;
        }
    }
}
