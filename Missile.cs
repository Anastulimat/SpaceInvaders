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
        public bool upDirection = false;
        private double vitesseMissile = ConstantsDeJeu.VitesseMissile;

        /// <summary>
        /// Constucteur de missille
        /// </summary>
        /// <param name="x">Position en X</param>
        /// <param name="y">Position en Y</param>
        /// <param name="lives">Nombre des points de vie</param>
        /// <param name="missileImage">Imafge du missille</param>
        /// <param name="side">Le Camps de missille</param>
        public Missile(float x, float y, int lives, Bitmap missileImage, Side side) : base(side)
        {
            Position = new Vecteur2D(x, y);
            Lives = lives;
            Image = missileImage;
        }

        /// <summary>
        /// Update Missille
        /// </summary>
        /// <param name="gameInstance">>Instance du jeu</param>
        /// <param name="deltaT">Nombre de mili sconde écoulées depuis le dernier update</param>
        public override void Update(Game gameInstance, double deltaT)
        {
            foreach (GameObject gameObject in gameInstance.gameObjects)
            {
                gameObject.Collision(this);
            }

            if(upDirection)
            {
                Position.Y -= (vitesseMissile * deltaT);
                if (Position.Y <= 0)
                    Lives = 0;
            }
            else
            {
                Position.Y += (vitesseMissile * deltaT);
                if (Position.Y >= gameInstance.gameSize.Height)
                    Lives = 0;
            }   
        }


        /// <summary>
        /// Lors d'un collision entre deux missilles, les deux sont morts
        /// </summary>
        /// <param name="m">Objet misile</param>
        /// <param name="numberOfPixelsInCollision">Nombre de pixel en collision</param>
        protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
        {
            Lives = 0;
            m.Lives = 0;
        }
    }
}
