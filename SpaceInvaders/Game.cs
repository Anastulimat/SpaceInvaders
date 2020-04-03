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
            Play, Pause
        }

        public GameState state;

        /// <summary>
        /// Player ship
        /// </summary>
        private SpaceShip playerShip;

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
            this.gameSize = gameSize;
            this.state = GameState.Play;

            for (int i = 1; i <= 3; i++)
            {
                AddNewGameObject(new Bunker(i * 135, gameSize.Height - 130));
            }

            this.playerShip = new PlayerSpaceship(this.gameSize.Width / 2, this.gameSize.Height, 3);
            this.AddNewGameObject(this.playerShip);
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
        /// Draw the whole game
        /// </summary>
        /// <param name="g">Graphics to draw in</param>
        public void Draw(Graphics g)
        {
            if(this.state == GameState.Pause)
            {
                string drawString = "Le jeu est en pause !\n";
                string subTitle = "Appuiez sur la touche 'p' pour reprendre !";

                Font drawFont = new Font("Arial", 16);
                Font subFont = new Font("Arial", 8);
                SolidBrush drawBrush = new SolidBrush(Color.Red);
                SolidBrush subBrush = new SolidBrush(Color.DarkOrange);
                StringFormat drawFormat = new StringFormat
                {
                    LineAlignment = StringAlignment.Center,
                    Alignment = StringAlignment.Center
                };
                g.DrawString(drawString, drawFont, drawBrush, this.gameSize.Width / 2, this.gameSize.Height / 2, drawFormat);
                g.DrawString(subTitle, subFont, subBrush, this.gameSize.Width / 2, (this.gameSize.Height / 2) + 20, drawFormat);
            }

            foreach (GameObject gameObject in gameObjects)
                gameObject.Draw(this, g);
        }

        /// <summary>
        /// Update game
        /// </summary>
        public void Update(double deltaT)
        {
            // add new game objects
            gameObjects.UnionWith(pendingNewGameObjects);
            pendingNewGameObjects.Clear();

            /*
            // if space is pressed
            if (keyPressed.Contains(Keys.Space))
            {
                // create new BalleQuiTombe
                GameObject newObject = new BalleQuiTombe(gameSize.Width / 2, 0);
                // add it to the game
                AddNewGameObject(newObject);
                // release key space (no autofire)
                ReleaseKey(Keys.Space);
            }
            */
            if (keyPressed.Contains(Keys.P) && this.state == GameState.Play)
            {
                this.state = GameState.Pause;
                ReleaseKey(Keys.P);
                System.Threading.Thread.Sleep(1);

            }

            if (keyPressed.Contains(Keys.P) && this.state == GameState.Pause)
            {
                this.state = GameState.Play;
                ReleaseKey(Keys.P);
            }

            if (this.state == GameState.Play)
            {
                // update each game object
                foreach (GameObject gameObject in gameObjects)
                {
                    gameObject.Update(this, deltaT);
                }
            }
            

            // remove dead objects
            gameObjects.RemoveWhere(gameObject => !gameObject.IsAlive());
        }
        #endregion
    }
}
