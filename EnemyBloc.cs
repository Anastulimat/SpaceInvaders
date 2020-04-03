using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace SpaceInvaders
{
    class EnemyBlock : GameObject
    {
        // Représente la probabilité qu’un vaisseau ennemi produise un tir en 1 seconde
        public double randomShootProbability = 0.5;

        //Ensemble des vaisseaux du bloc
        public HashSet<SpaceShip> enemyShips = new HashSet<SpaceShip>();

        //Largeur du bloc au moment de sa création
        private int baseWidth;

        public int lineNumber = 1;
        public int blockHeight = 0;
        public bool leftDirection = false;
        public int shipHorizonalSpace;
        public int shipVerticlaSapce = ConstantsDeJeu.EnemyShipVerticlaSapce;

        private double enemyBlockSpeed = ConstantsDeJeu.EnemyBlockSpeed;

        Random rand = new Random();

        //Taille du bloc (largeur, hauteur), adaptée au fur et à mesure du jeu.
        public Size Size { get; set; }

        //Position du bloc(coin supérieur gauche)
        public Vecteur2D Position { get; set; }


        /// <summary>
        /// Permet d’initialiser la position et la largeur de base du bloc d'ennemie
        /// </summary>
        /// <param name="x">Position en X</param>
        /// <param name="y">Position en Y</param>
        /// <param name="baseWidth">La largeur de base du bloc</param>
        /// <param name="side">Le camps du bloc</param>
        public EnemyBlock(float x, float y, int baseWidth, Side side) : base(side)
        {
            Position = new Vecteur2D(x, y);
            this.baseWidth = baseWidth;
            Size = new Size(0, 0);
        }


        /// <summary>
        /// Ajoute une nouvelle ligne d’ennemis au bloc d'ennemi
        /// </summary>
        /// <param name="nbShips">Nombre de vaisseau ennemi</param>
        /// <param name="nbLives">Nombre de points de vie de vie de chaque vaisseau</param>
        /// <param name="shipImage">L'image des vaisseaux</param>
        public void AddLine(int nbShips, int nbLives, Bitmap shipImage)
        {
            float posY;
            shipHorizonalSpace = baseWidth / nbShips;

            posY = (float)Position.Y + blockHeight + shipVerticlaSapce;
            if (lineNumber == 1)
            {
                posY = (float)Position.Y + blockHeight;
            }
            
            for (int i = 0; i < nbShips; i++)
            {
                SpaceShip spaceShip = new SpaceShip((float)(Position.X + (i * shipHorizonalSpace)), posY, nbLives, Side.Enemy)
                {
                    Image = shipImage,
                };
                enemyShips.Add(spaceShip);
            }
            blockHeight += shipImage.Height + shipVerticlaSapce;
            lineNumber++;
            UpdateSize();
        }




        /// <summary>
        /// Recalcule la taille et la position du bloc en fonction des vaisseaux qu’il contient.
        /// </summary>
        public void UpdateSize()
        {
            int minX = Int32.MaxValue, maxX = 0;
            int minY = Int32.MaxValue, maxY = 0;
            foreach(SpaceShip enemySpaceShip in enemyShips)
            {
                if (enemySpaceShip.Position.X < minX) minX = (int) enemySpaceShip.Position.X;
                if (enemySpaceShip.Position.X + enemySpaceShip.Image.Width > maxX) maxX = (int)enemySpaceShip.Position.X + enemySpaceShip.Image.Width;
                if (enemySpaceShip.Position.Y < minY) minY = (int)enemySpaceShip.Position.Y;
                if (enemySpaceShip.Position.Y + enemySpaceShip.Image.Height > maxY) maxY = (int)enemySpaceShip.Position.Y + enemySpaceShip.Image.Height;
            }
            if(minX > Position.X)
            {
                Position.X = minX;
            }
            Size = new Size(maxX - minX, maxY - minY);
        }

        /// <summary>
        /// Dessiner chaque vaisseau du bloc
        /// </summary>
        /// <param name="gameInstance">Instance du jeu</param>
        /// <param name="graphics">Objet graphics qui permet de dessiner</param>
        public override void Draw(Game gameInstance, Graphics graphics)
        {
            foreach (SpaceShip spaceShip in enemyShips)
            {
                spaceShip.Draw(gameInstance, graphics);
            }
        }

        /// <summary>
        /// Return false si tous les vaisseau du bloc sont morts
        /// </summary>
        /// <returns></returns>
        public override bool IsAlive()
        {
            if (enemyShips.Count <= 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Fonction de changement de direction
        /// </summary>
        /// <param name="paramLeftdirection">Indecateur si le bloc se déplace à gauche</param>
        /// <param name="paramRandomShootProbability">Probabilité de tire d'un viasseau</param>
        /// <param name="paramY">La nouvelle poition en Y</param>
        /// <param name="paramBlocSpeed">Nombre à ajouter à la vitesse du bloc</param>
        private void ChangeMovement(bool paramLeftdirection, double paramRandomShootProbability, double paramY, double paramBlocSpeed)
        {
            if(!paramLeftdirection)
            {
                leftDirection = true;
            }
            else
            {
                leftDirection = false;
            }
            randomShootProbability += paramRandomShootProbability;
            Position.Y += paramY;
            enemyBlockSpeed += paramBlocSpeed;
            foreach (SpaceShip spaceShip in enemyShips)
            {
                spaceShip.rightDirection = leftDirection;
                spaceShip.Position.Y += paramY;
                spaceShip.SpaceShipSpeedPixelPerSecond = enemyBlockSpeed;
            }
        }


        /// <summary>
        /// Update bloc d'ennemi
        /// </summary>
        /// <param name="gameInstance">>Instance du jeu</param>
        /// <param name="deltaT">Nombre de mili sconde écoulées depuis le dernier update</param>
        public override void Update(Game gameInstance, double deltaT)
        {
            double r = rand.NextDouble();
            int randomShip = rand.Next(0, enemyShips.Count);

            if (!leftDirection)
            {
                Position.X += enemyBlockSpeed * deltaT;
            }

            if(leftDirection)
            {
                Position.X -= enemyBlockSpeed * deltaT;
            }

            if (Size.Width + Position.X >= gameInstance.gameSize.Width)
            {
                ChangeMovement(false, ConstantsDeJeu.NewRandomShootProbabilityForEnemyBloc, ConstantsDeJeu.YNewEnemyBlocPosition, ConstantsDeJeu.NewEnemyBlocSpeed);
            }

            if (Position.X <= 0)
            {
                ChangeMovement(true, ConstantsDeJeu.NewRandomShootProbabilityForEnemyBloc, ConstantsDeJeu.YNewEnemyBlocPosition, ConstantsDeJeu.NewEnemyBlocSpeed);
            }

            SpaceShip randomSpaceShip = enemyShips.ElementAt(randomShip);
            foreach (SpaceShip spaceShip in enemyShips)
            {
                spaceShip.Update(gameInstance, deltaT);

                // Test de fin de jeu
                if(spaceShip.Position.Y + spaceShip.Image.Height >= gameInstance.PlayerShip.Position.Y)
                {
                    gameInstance.PlayerShip.Lives = 0;
                }
            }

            //Tirs aléatoire
            if (r <= randomShootProbability * deltaT)
            {
                randomSpaceShip.Shoot(gameInstance);
            }
            UpdateSize();
        }

        /// <summary>
        /// Cas du collision entre bloc et missile, si le missile touche un vaisseau et qu'il est mort, on l'enlève de la liste d'ennemies
        /// </summary>
        /// <param name="m"></param>
        public override void Collision(Missile m)
        {
            if (!(m.Position.X > Position.X + Size.Width) && !(m.Position.Y > Position.Y + Size.Height) && !(Position.X > m.Position.X + m.Image.Width) && !(Position.Y > m.Position.Y + m.Image.Height))
            {
                foreach (SpaceShip spaceShip in enemyShips)
                {
                    spaceShip.Collision(m);
                }
                enemyShips.RemoveWhere(spaceShip => !spaceShip.IsAlive());
            }
        }
    }
}
