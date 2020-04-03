using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class PlayerSpaceship : SpaceShip
    {
        /// <summary>
        /// Constructeur du vaisseau de joueur
        /// </summary>
        /// <param name="x">Position en X</param>
        /// <param name="y">Position en Y</param>
        /// <param name="lives">Point de vie</param>
        /// <param name="side">Le camps du vaisseau du joueur</param>
        public PlayerSpaceship(float x, float y, int lives, Side side) : base(x, y, lives, side)
        {
            Image = SpaceInvaders.Properties.Resources.ship3;
            Position = new Vecteur2D(x, y - Image.Height); // Soustraction l'hauteur de l'image de la position y pour que l'affichage soit correct
            this.SpaceShipSpeedPixelPerSecond = ConstantsDeJeu.PlayerSpaceShipSpeedPixelPerSecond;
            Side = side;
        }

        /// <summary>
        /// Dessiner l'objet dnas le frame
        /// </summary>
        /// <param name="gameInstance">Instance du jeu</param>
        /// <param name="graphics">Objet graphics qui permet de dessiner</param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            base.Draw(gameInstance, graphics);
            
            string pleyrLives = "Vie = " + Lives;
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Black);
            if (Lives <= 10)
                drawBrush = new SolidBrush(Color.Red);
            graphics.DrawString(pleyrLives, drawFont, drawBrush, 5, gameInstance.gameSize.Height - 25);
        }

        /// <summary>
        /// Update vaisseau joueur
        /// </summary>
        /// <param name="gameInstance">>Instance du jeu</param>
        /// <param name="deltaT">Nombre de mili sconde écoulées depuis le dernier update</param>
        public override void Update(Game gameInstance, double deltaT)
        {
            if (gameInstance.keyPressed.Contains(Keys.Left))
            {
                Position.X -= (float)(SpaceShipSpeedPixelPerSecond * deltaT);
                if (Position.X <= 0)
                {
                    Position.X = 0;
                }
            }

            if (gameInstance.keyPressed.Contains(Keys.Right))
            {
                Position.X += (float)(SpaceShipSpeedPixelPerSecond * deltaT);
                if ((Position.X + Image.Width) >= gameInstance.gameSize.Width)
                {
                    Position.X = gameInstance.gameSize.Width - Image.Width;
                }
            }

            if (gameInstance.keyPressed.Contains(Keys.Space))
            {
                this.Shoot(gameInstance);
            }
        }

    }

}
