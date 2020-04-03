using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;

namespace SpaceInvaders
{
    class Game
    {
        public enum GameState
        {
            Play, Pause, Win, Lost
        }
        public static GameState state;

        public EnemyBlock enemies;

        /// <summary>
        /// Player ship
        /// </summary>
        public SpaceShip PlayerShip;

        #region GameObjects management
        /// <summary>
        /// Set of all game objects currently in the game
        /// </summary>
        public HashSet<GameObject> gameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Set of new game objects scheduled for addition to the game
        /// </summary>
        private HashSet<GameObject> pendingNewGameObjects = new HashSet<GameObject>();

        /// <summary>
        /// Schedule a new object for addition in the game.
        /// The new object will be added at the beginning of the next update loop
        /// </summary>
        /// <param name="gameObject">object to add</param>
        public void AddNewGameObject(GameObject gameObject)
        {
            pendingNewGameObjects.Add(gameObject);
        }
        #endregion


        /******************************************************************************/

        #region game technical elements
        /// <summary>
        /// Size of the game area
        /// </summary>
        public Size gameSize;

        /// <summary>
        /// State of the keyboard
        /// </summary>
        public HashSet<Keys> keyPressed = new HashSet<Keys>();
        #endregion

        /******************************************************************************/

        #region static fields (helpers)
        /// <summary>
        /// Singleton for easy access
        /// </summary>
        public static Game game { get; private set; }

        /// <summary>
        /// A shared black brush
        /// </summary>
        private static Brush blackBrush = new SolidBrush(Color.Black);

        /// <summary>
        /// A shared simple font
        /// </summary>
        private static Font defaultFont = new Font("Times New Roman", 24, FontStyle.Bold, GraphicsUnit.Pixel);

        /// <summary>
        /// A shared simple font
        /// </summary>
        private static Font ArialFont16 = new Font("Arial", 16);
        private static Font ArialFont8 = new Font("Arial", 8);

        private static Brush redBrush = new SolidBrush(Color.Red);
        private static Brush darkOrangeBrush = new SolidBrush(Color.DarkOrange);

        /// <summary>
        /// A shared String Format for alignment
        /// </summary>
        private static StringFormat drawFormat = new StringFormat
        {
            LineAlignment = StringAlignment.Center,
            Alignment = StringAlignment.Center
        };


        private static string drawString;
        private static string drawSubString;
        #endregion

        /******************************************************************************/

        #region constructors
        /// <summary>
        /// Singleton constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        /// 
        /// <returns></returns>
        public static Game CreateGame(Size gameSize)
        {
            if (game == null)
                game = new Game(gameSize);
            return game;
        }

        /// <summary>
        /// Private constructor
        /// </summary>
        /// <param name="gameSize">Size of the game area</param>
        private Game(Size gameSize)
        {
            this.InitGame(gameSize);
        }

        #endregion


        /******************************************************************************/

        #region methods

        /// <summary>
        /// Force a given key to be ignored in following updates until the user
        /// explicitily retype it or the system autofires it again.
        /// </summary>
        /// <param name="key">key to ignore</param>
        public void ReleaseKey(Keys key)
        {
            keyPressed.Remove(key);
        }

        /// <summary>
        /// Initialiser les composants du jeu
        /// </summary>
        /// <param name="gameSize">La taille du jeu</param>
        public void InitGame(Size gameSize)
        {
            this.gameSize = gameSize;

            for (int i = 1; i <= 3; i++)
            {
                AddNewGameObject(new Bunker(i * 135, gameSize.Height - 130, Side.Neutral));
            }

            PlayerShip = new PlayerSpaceship(this.gameSize.Width / 2, this.gameSize.Height, 50, Side.Ally);
            this.AddNewGameObject(this.PlayerShip);

            this.enemies = new EnemyBlock(50, 50, 400, Side.Enemy);
            enemies.AddLine(6, 5, SpaceInvaders.Properties.Resources.ship7);
            enemies.AddLine(6, 10, SpaceInvaders.Properties.Resources.ship2);
            enemies.AddLine(6, 10, SpaceInvaders.Properties.Resources.ship5);
            this.AddNewGameObject(this.enemies);

            state = GameState.Play;
        }

        /// <summary>
        /// Supprimer tous les objets
        /// </summary>
        public void ClearGame()
        {
            gameObjects.Clear();
            pendingNewGameObjects.Clear();
            enemies = null;
        }


        /// <summary>
        /// Draw the whole game
        /// </summary>
        /// <param name="g">Graphics to draw in</param>
        public void Draw(Graphics g)
        {
            if (state == GameState.Pause)
            {
                DrawSimpleString("Le jeu est en pause !\n", "Appuiez sur la touche 'p' pour reprendre !", g);
            }

            else if (state == GameState.Win)
            {
                DrawSimpleString("Vous avez gangé !!!\n", "Appuiez sur la touche 'space' pour reprendre !", g);
            }

            else if (state == GameState.Lost)
            {
                DrawSimpleString("Lost !!!\n", "Appuiez sur la touche 'space' pour reprendre !", g);
            }

            foreach (GameObject gameObject in gameObjects)
                gameObject.Draw(this, g);
        }

        /// <summary>
        /// Afficher un string dans le frame
        /// </summary>
        /// <param name="mainstring">le text principale à afficher</param>
        /// <param name="subString">le text secondaire à afficher</param>
        /// <param name="g">Objet graphics pour dessiner</param>
        private void DrawSimpleString(string mainstring, string subString, Graphics g)
        {
            drawString = mainstring;
            drawSubString = subString;
            g.DrawString(drawString, ArialFont16, redBrush, this.gameSize.Width / 2, this.gameSize.Height / 2, drawFormat);
            g.DrawString(drawSubString, ArialFont8, darkOrangeBrush, this.gameSize.Width / 2, (this.gameSize.Height / 2) + 20, drawFormat);
        }

        /// <summary>
        /// Update game
        /// </summary>
        public void Update(double deltaT)
        {
            // add new game objects
            gameObjects.UnionWith(pendingNewGameObjects);
            pendingNewGameObjects.Clear();

            if (keyPressed.Contains(Keys.P) && state == GameState.Play)
            {
                state = GameState.Pause;
                ReleaseKey(Keys.P);
                System.Threading.Thread.Sleep(1);

            }

            if (keyPressed.Contains(Keys.P) && state == GameState.Pause)
            {
                state = GameState.Play;
                ReleaseKey(Keys.P);
            }

            if (state == GameState.Play)
            {
                // Update each game object
                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.Update(this, deltaT);
                }

                if (!PlayerShip.IsAlive())
                {
                    state = GameState.Lost;
                }

                if(!this.enemies.IsAlive())
                {
                    state = GameState.Win;
                }
            }
            
            if(state == GameState.Lost || state == GameState.Win)
            {
                if(keyPressed.Contains(Keys.Space))
                {
                    ClearGame();
                    InitGame(gameSize);
                    ReleaseKey(Keys.Space);
                }
            }

            // remove dead objects
            gameObjects.RemoveWhere(gameObject => !gameObject.IsAlive());
        }
        #endregion
    }
}
