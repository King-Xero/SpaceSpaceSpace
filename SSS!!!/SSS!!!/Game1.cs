using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SSS___.Core;

namespace SSS___
{
    /// <summary>
    /// This is the main type for your game
    /// 
    /// Code references:
    /// http://xnagpa.net/xnatutorials.php
    /// http://msdn.microsoft.com/en-us/library/bb195024.aspx 
    /// Learning XNA 3.0 by Aaron Reed
    /// Beginning XNA 3.0 Game Programming: From Novice to Professional by Alexandre Santos Lobão, Bruno Evangelista, José Antonio Leal de Farias, and Riemer Grootjans
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        //Game Screens
        protected HelpScreen helpScreen;
        protected MenuScreen menuScreen;
        protected PlayScreen playScreen;

        //Active game screen
        protected GameScreen activeScreen;

        //Textures
        protected Texture2D helpBackgroundTexture, helpForegroundTexture;
        protected Texture2D menuBackgroundTexture, menuElementsTexture;
        protected Texture2D playBackgroundTexture, playElementsTexture;

        //Fonts
        private SpriteFont smallFont, largeFont, scoreFont;

        //User input
        protected KeyboardState oldKeyboardState;
        protected GamePadState oldGamepadState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            //this.graphics.IsFullScreen = true; //FullScreen
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch);

            //Help Screen
            helpBackgroundTexture = Content.Load<Texture2D>(@"HelpImages/Help Background"); //Help Screen Background
            helpForegroundTexture = Content.Load<Texture2D>(@"HelpImages/Help Info"); //Help Screen Image
            helpScreen = new HelpScreen(this, helpBackgroundTexture, helpForegroundTexture); //Creates the help screen
            Components.Add(helpScreen); //Adds the help screen to the list of game components

            //Menu Screen
            smallFont = Content.Load<SpriteFont>(@"MenuFonts/smallFont"); //Font for menu items
            largeFont = Content.Load<SpriteFont>(@"MenuFonts/largeFont"); //Font for selected menu item
            menuBackgroundTexture = Content.Load<Texture2D>(@"MenuImages/Menu Background"); //Menu Screen Background
            menuElementsTexture = Content.Load<Texture2D>(@"MenuImages/Menu Elements"); //Menu Screen Title
            menuScreen = new MenuScreen(this, smallFont, largeFont, menuBackgroundTexture, menuElementsTexture); //Creates the menu screen
            Components.Add(menuScreen); //Adds the menu screen to the list of game components

            //Play Screen
            scoreFont = Content.Load<SpriteFont>(@"PlayFonts/scoreFont"); //Font for the player's score and health
            playElementsTexture = Content.Load<Texture2D>(@"PlayImages/Sprite Sheet"); //Sprite Sheet for the game
            playBackgroundTexture = Content.Load<Texture2D>(@"PlayImages/Game BackGround"); //Play Screen Background
            playScreen = new PlayScreen(this, playElementsTexture, playBackgroundTexture, scoreFont); //Creates the play screen
            Components.Add(playScreen); //Adds the play screen to the list of game components

            //Screen to show on start
            menuScreen.Show();
            activeScreen = menuScreen;

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();

            // TODO: Add your update logic here
            HandleInput(); //Handle user input
            base.Update(gameTime);
        }

        private void HandleInput() //Handle user input
        {
            if (activeScreen == menuScreen) //Handle menu input
            {
                HandleMenuInput();
            }
            else if (activeScreen == helpScreen) //Handle help screen input
            {
                if (CheckEnterStart())
                {
                    ShowScreen(menuScreen);
                }
            }
            else if (activeScreen == playScreen)
            {
                HandlePlayInput();
            }
        }

        private bool CheckEnterStart() //Check if the enter key or the start button was pressed, and if so, return true
        {
            //Get the gamepad and keyboard state
            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboardState = Keyboard.GetState();

            bool result = (oldKeyboardState.IsKeyDown(Keys.Enter) && (keyboardState.IsKeyUp(Keys.Enter)));
            result |= (oldGamepadState.Buttons.Start == ButtonState.Pressed) && (gamepadState.Buttons.Start == ButtonState.Released);

            oldKeyboardState = keyboardState;
            oldGamepadState = gamepadState;

            return result;
        }

        private void HandleMenuInput() //Handle buttons and keyboard in the menu screen
        {
            if (CheckEnterStart())
            {
                switch (menuScreen.SelectedMenuIndex)
                {
                    case 0: //Single player game
                        playScreen.TwoPlayers = false;
                        ShowScreen(playScreen);
                        break;
                    case 1: //Two player game
                        playScreen.TwoPlayers = true;
                        ShowScreen(playScreen);
                        break;
                    case 2: //Help screen
                        ShowScreen(helpScreen);
                        break;
                    case 3: //Exit game
                        Exit();
                        break;
                }
            }
        }

        private void HandlePlayInput()
        {
            //Get the gamepad and keyboard state
            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboardState = Keyboard.GetState();

            bool backKey = (oldKeyboardState.IsKeyDown(Keys.Escape) && (keyboardState.IsKeyUp(Keys.Escape)));
            backKey |= (oldGamepadState.Buttons.Back == ButtonState.Pressed) && (gamepadState.Buttons.Back == ButtonState.Released);

            bool enterkey = (oldKeyboardState.IsKeyDown(Keys.Enter) && (keyboardState.IsKeyUp(Keys.Enter)));
            enterkey |= (oldGamepadState.Buttons.Start == ButtonState.Pressed) && (gamepadState.Buttons.Back == ButtonState.Released);

            oldKeyboardState = keyboardState; //Get the keyboard state
            oldGamepadState = gamepadState; //Get the gamepad state

            if (enterkey)
            {
                if (playScreen.GameOver) //If its game over, the game exits to the menu screen
                {
                    ShowScreen(menuScreen);
                }
                else //If the game is paused, the game starts again
                {
                    playScreen.Paused = !playScreen.Paused;
                }
            }
            if (backKey) //The game exits to the menu screen
            {
                ShowScreen(menuScreen);
            }
        }

        protected void ShowScreen(GameScreen screen)
        {
            activeScreen.Hide();
            activeScreen = screen;
            screen.Show();
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();//BlendState.AlphaBlend);
            // TODO: Add your drawing code here
            base.Draw(gameTime); //Draws all the child components
            spriteBatch.End();
        }
    }
}
