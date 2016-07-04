using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SSS___
{
    /// <summary>
    /// This is a game component that implements the player
    /// </summary>
    public class Player : Microsoft.Xna.Framework.DrawableGameComponent
    {

        protected Texture2D texture; //Sprite texture
        protected Rectangle spriteRectangle; //Represents where the sprite picture is
        protected Vector2 position; //Position of player
        protected TimeSpan elapsedTime = TimeSpan.Zero;
        protected PlayerIndex playerIndex; //Player ID
        protected Rectangle screenBounds; //Screen Area
        protected int score; //Score points
        protected int health; //Health points
        private const int INITIALHEALTH = 100; //Amount of health the player starts with

        public Player(Game game, ref Texture2D spriteSheet, PlayerIndex playerID, Rectangle rectangle)
            : base(game)
        {
            // TODO: Construct any child components here
            texture = spriteSheet;
            position = new Vector2();
            playerIndex = playerID;
            spriteRectangle = rectangle;

#if XBOX360 //"Safe area" of TV on Xbox 360
            screenBounds = new Rectangle((int)(Game.Window.ClientBounds.Width * 0.03f),
            (int)(Game.Window.ClientBounds.Height * 0.03f),
            Game.Window.ClientBounds.Width - (int)(Game.Window.ClientBounds.Width * 0.03f),
            Game.Window.ClientBounds.Height - (int)(Game.Window.ClientBounds.Width * 0.03f));
#else //PC "Safe area"
            screenBounds = new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
#endif
        }

        public void Reset() //Put the player in start position on screen
        {
            if (playerIndex == PlayerIndex.One)
            {
                position.X = screenBounds.Width / 3; //Player ones's position along the bottom of the screen
            }
            else
            {
                position.X = (int)(screenBounds.Width / 1.5); //Player two's position along the bottom of the screen
            }

            position.Y = screenBounds.Height - spriteRectangle.Height - 10; //Places the player(s) at the very bottom of the screen
            score = 0; //Resets the score to zero
            health = INITIALHEALTH; //Resets the health to intial value
        }

        public int Score //Total points for the player
        {
            get { return score; }
            set
            {
                if (value < 0)
                {
                    score = 0; //Doesn't allow a negative score
                }
                else
                {
                    score = value;
                }
            }
        }

        public int Health //Health Remaining
        {
            get { return health; }
            set { health = value; }
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime) //Update the player position, health, and score
        {
            // TODO: Add your update code here
            //Move the player with an Xbox controller
            GamePadState gamepadStatus = GamePad.GetState(playerIndex);
            position.X += (int)((gamepadStatus.ThumbSticks.Left.X * 3) * 4);
            //Move the player with the keyboard
            if (playerIndex == PlayerIndex.One)
            {
                HandlePlayer1Keyboard();
            }
            else
            {
                HandlePlayer2Keyboard();
            }

            KeepInBounds(); //Keep the player on screen

            //Update the score
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromMilliseconds(1))
            {
                score++; //Increases score
            }

            base.Update(gameTime);
        }

        private void KeepInBounds()
        {
            if (position.X < screenBounds.Left)
            {
                position.X = screenBounds.Left;
            }
            if (position.X > screenBounds.Width - spriteRectangle.Width)
            {
                position.X = screenBounds.Width - spriteRectangle.Width;
            }
        }

        private void HandlePlayer1Keyboard() //Keyboard input for player one
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.A)) //Move the player left
            {
                position.X -= 6;
            }
            if (keyboard.IsKeyDown(Keys.D)) //Move the player right
            {
                position.X += 6;
            }
        }

        private void HandlePlayer2Keyboard() //Keyboard input for player one
        {
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Left)) //Move the player left
            {
                position.X -= 6;
            }
            if (keyboard.IsKeyDown(Keys.Right)) //Move the player right
            {
                position.X += 6;
            }
        }

        public override void Draw(GameTime gameTime) //Draw the player sprite
        {

            SpriteBatch sBatch = (SpriteBatch)Game.Services.GetService(typeof (SpriteBatch)); //Get the current sprite batch
            
            sBatch.Draw(texture, position, spriteRectangle, Color.White); //Draw the sprite

            base.Draw(gameTime);
        }

        public Rectangle GetBounds() //Rectangle used to check for collisions
        {
            return new Rectangle((int)position.X, (int)position.Y, spriteRectangle.Width, spriteRectangle.Height);
        }
    }
}