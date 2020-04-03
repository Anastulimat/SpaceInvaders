using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class Bunker : SimpleObject
    {
        /// <summary>
        /// Constructeur du bunker
        /// </summary>
        /// <param name="x">Position en X</param>
        /// <param name="y">Position en Y</param>
        /// <param name="side">Le camps du bunker</param>
        public Bunker(float x, float y, Side side) : base(side)
        {
            Position = new Vecteur2D(x, y);
            Image = SpaceInvaders.Properties.Resources.bunker;
        }

        public override void Update(Game gameInstance, double deltaT){}

        public override bool IsAlive() {return true;}

        /// <summary>
        /// Lors d'un collision entre un missile et le buncker, on enleve le nombre de pixels en collision
        /// </summary>
        /// <param name="m">Objet misile</param>
        /// <param name="numberOfPixelsInCollision">Nombre de pixel en collision</param>
        protected override void OnCollision(Missile m, int numberOfPixelsInCollision)
        {
            m.Lives -= numberOfPixelsInCollision;
        }

        public void Method()
        {
            throw new System.NotImplementedException();
        }
    }
}
