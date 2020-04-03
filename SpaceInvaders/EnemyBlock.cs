using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class EnemyBlock : GameObject
    {
        private HashSet<SpaceShip> enemyShips;
        private int baseWidth;
        private Size size;
        private Vecteur2D position;

        public EnemyBlock(int baseWidth, Vecteur2D position)
        {
            this.baseWidth = baseWidth;
            this.position = position;
        }

        public Size Size { get; set; }

        public Vecteur2D Position { get; set; }

        void AddLine(int nbShips, int nbLives, Bitmap shipImage)
        {

        }

        void UpdateSize()
        {

        }


        public override void Collision(Missile m)
        {
            //throw new NotImplementedException();
        }

        public override void Draw(Game gameInstance, Graphics graphics)
        {
            //throw new NotImplementedException();
        }

        public override bool IsAlive()
        {
            return true;
        }

        public override void Update(Game gameInstance, double deltaT)
        {
            //throw new NotImplementedException();
        }
    }
}
