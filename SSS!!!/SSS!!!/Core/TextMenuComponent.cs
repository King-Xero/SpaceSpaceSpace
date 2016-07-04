using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SSS___.Core
{
    /// <summary>
    /// Text component of the menu screen
    /// </summary>
    public class TextMenuComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected SpriteBatch spriteBatch = null; //SpriteBatch
        protected readonly SpriteFont regularFont, selectedFont; //Fonts
        protected Color regularColour = Color.Red, selectedColour = Color.White; //Colours
        protected Vector2 position = new Vector2(); //Menu position
        protected int selectedIndex = 0; //Currently selected item
        private readonly List<string> menuItems; //List of menu items
        protected int width, height; //Size of menu in pixels
        //User input
        protected KeyboardState oldKeyboardState;
        protected GamePadState oldGamePadState;

        public void SetMenuItems(string[] items) //Set the menu options
        {
            menuItems.Clear();
            menuItems.AddRange(items);
            CalculateBounds();
        }

        public int Width //Width of menu in pixels
        {
            get { return width; }
        }

        public int Height //Height of menu in pixels
        {
            get { return height; }
        }

        public int SelectedIndex //Selected menu item index
        {
            get { return selectedIndex; }
            set { selectedIndex = value; }
        }

        public Color RegularColour //Regular item colour
        {
            get { return regularColour; }
            set { regularColour = value; }
        }

        public Color SelectedColour //Selected item colour
        {
            get { return selectedColour; }
            set { selectedColour = value; }
        }

        public Vector2 Position //Position of component on screen
        {
            get { return position; }
            set { position = value; }
        }

        protected void CalculateBounds()
        {
            //Sets values to zero so that the values are reset each time this method is called
            height = 0;
            width = 0;
            foreach (string item in menuItems) //Loop through items in the menu
            {
                Vector2 size = selectedFont.MeasureString(item); //Calculates the width of the string
                if (size.X > width)
                {
                    width = (int) size.X;
                }
                height += selectedFont.LineSpacing + 5; //Calculates the height of the string and adds extra spacing
            }
        }

        public TextMenuComponent(Game game, SpriteFont normalFont, SpriteFont selectedFont) //"normalFont" for regular items, "selectedFont" for highlighted items
            : base(game)
        {
            // TODO: Construct any child components here
            regularFont = normalFont;
            this.selectedFont = selectedFont;
            menuItems = new List<string>();

            //Get the current sprite batch
            spriteBatch = (SpriteBatch)
                Game.Services.GetService(typeof(SpriteBatch));
            
            //User input
            oldKeyboardState = Keyboard.GetState();
            oldGamePadState = GamePad.GetState(PlayerIndex.One);
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
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            GamePadState gamepadState = GamePad.GetState(PlayerIndex.One);
            KeyboardState keyboardState = Keyboard.GetState();

            bool down, up;
            //Keyboard
            down = (oldKeyboardState.IsKeyDown(Keys.Down) && (keyboardState.IsKeyUp(Keys.Down)));
            up = (oldKeyboardState.IsKeyDown(Keys.Up) && (keyboardState.IsKeyUp(Keys.Up)));
            //GamePad
            down |= (oldGamePadState.DPad.Down == ButtonState.Pressed) && (gamepadState.DPad.Down == ButtonState.Released);
            up |= (oldGamePadState.DPad.Up == ButtonState.Pressed) && (gamepadState.DPad.Up == ButtonState.Released);

            if (down)
            {
                selectedIndex++; //Increases the selected index by 1
                //If the bottom of the menu is passed, loop up to the start of the menu
                if (selectedIndex == menuItems.Count)
                {
                    selectedIndex = 0;
                }
            }

            if (up)
            {
                selectedIndex--; //Decreases the selected index by 1
                //If the top of the menu is passed, loop down to the bottom of the menu
                if (selectedIndex < 0)
                {
                    selectedIndex = menuItems.Count - 1;
                }
            }
            oldKeyboardState = keyboardState; //In the next frame current state of the keyboard and last state of the keyboard will be set
            oldGamePadState = gamepadState; //In the next frame current state of the gamepad and last state of the gamepad will be set
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            float y = position.Y;
            for (int i = 0; i < menuItems.Count; i++) //Loop through the menu items
            {
                SpriteFont font; //Chosen font
                Color theColour; //Font colour
                //If the item is the selected item, draw it with the highlighted font
                if (i == SelectedIndex)
                {
                    font = selectedFont;
                    theColour = selectedColour;
                }
                //Else draw it with the normal font
                else
                {
                    font = regularFont;
                    theColour = regularColour;
                }

                //Draw a shadow for the text
                spriteBatch.DrawString(font, menuItems[i], new Vector2(position.X + 1, y + 1), Color.Black);
                //Draw the text item
                spriteBatch.DrawString(font, menuItems[i], new Vector2(position.X, y), theColour);
                y += font.LineSpacing;
            }

            base.Draw(gameTime);
        }
    }
}