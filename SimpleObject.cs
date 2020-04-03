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
        public int pixelNumber = 0;

        public Vecteur2D Position { get; set; }

        public double Vitesse { get; set; }

        public int Lives { get; set; }

        public Bitmap Image { get; set; }

        /// <summary>
        /// Constructor SimpleObject
        /// </summary>
        /// <param name="side">Le camps de l'objet</param>
        protected SimpleObject(Side side) : base(side)
        {
            Side = side;
        }

        /// <summary>
        /// Dessiner l'objet dnas le frame
        /// </summary>
        /// <param name="gameInstance">Instance du jeu</param>
        /// <param name="graphics">Objet graphics qui permet de dessiner</param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            if(IsAlive())
            {
                graphics.DrawImage(Image, (float)Position.X, (float)Position.Y, Image.Width, Image.Height);
            }
        }

        /// <summary>
        /// Return true ou false en fonction de la vie d'objet
        /// </summary>
        /// <returns></returns>
        public override bool IsAlive()
        {
            if (Lives <= 0)
                return false;
            return true;
        }


        protected abstract void OnCollision(Missile m, int numberOfPixelsInCollision);
        
        /// <summary>
        /// Détecter s'il y a une collision et appliquer la méthode OnColission selon les différents cas
        /// </summary>
        /// <param name="m">Objet Missile</param>
        public override void Collision(Missile m)
        {

            int x, y, xMissileInPlane, yMissileInPlane, xMissleInObject, yMissleInObject;

            /**
             * Vérifier s'il y a une intersction entre les deux objets
             * 
             *  m.Position.X > Position.X + Image.Width : le deuxième rectangle est à droite du premier OU
             *  m.Position.Y > Position.Y + Image.Height : le deuxième rectangle est haut dessus du premier OU
             *  Position.X > m.Position.X + mImage.Width : le premier rectangle est à droite du deuxième OU
             *  Position.Y > m.Position.Y + m.Image.Height : le premier rectangle est haut dessus du deuxième.
             * */
            if (!(m.Position.X > Position.X + Image.Width) && !(m.Position.Y > Position.Y + Image.Height) && !(Position.X > m.Position.X + m.Image.Width) && !(Position.Y > m.Position.Y + m.Image.Height))
            {

                // Loop through the images pixels to reset color.
                for (x = 0; x < m.Image.Width; x++)
                {
                    for (y = 0; y < m.Image.Height; y++)
                    {
                        xMissileInPlane = x + (int)m.Position.X;
                        yMissileInPlane = y + (int)m.Position.Y;

                        xMissleInObject = xMissileInPlane - (int)Position.X;
                        yMissleInObject = yMissileInPlane - (int)Position.Y;

                        if (xMissleInObject >= 0 && xMissleInObject < Image.Width && yMissleInObject >= 0 && yMissleInObject < Image.Height)
                        {

                            if ((Side == Side.Ally && m.Side == Side.Enemy) || (Side == Side.Enemy && m.Side == Side.Ally))
                            {
                                OnCollision(m, pixelNumber);
                            }

                            if (Image.GetPixel(xMissleInObject, yMissleInObject).GetBrightness() < 0.01 && Side == Side.Neutral)
                            {
                                pixelNumber++;
                                OnCollision(m, pixelNumber);
                                Image.SetPixel(xMissleInObject, yMissleInObject, Color.White);
                                pixelNumber = 0;
                            }
                        }
                    }
                }
            }
        }





    }
}
