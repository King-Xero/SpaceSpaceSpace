using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SSS___.Core;

namespace SSS___
{
    /// <summary>
    /// This is a game component that implements the menu screen
    /// </summary>
    public class MenuScreen : GameScreen 
    {

        protected TextMenuComponent menu; //Text items for the menu screen
        protected readonly Texture2D elements;

        protected SpriteBatch spriteBatch = null;

        protected Rectangle line1Rectangle = new Rectangle(0, 0, 536, 131); //The rectangle that contains the image for Line 1 in the texture
        protected Vector2 line1Position; //Holds the position of line 1 on the screen
        protected Rectangle line2Rectangle = new Rectangle(120, 165, 517, 130); //The rectangle that contains the image for Line 2 in the texture
        protected Vector2 line2Position; //Holds the position of line 2 on the screen
        protected Rectangle line3Rectangle = new Rectangle(8, 304, 375, 144); //The rectangle that contains the image for Line 3 in the texture
        protected Vector2 line3Position; //Holds the position of line 3 on the screen
        protected bool showLine3; //Shows or hides line 3
        protected TimeSpan elapsedTime = TimeSpan.Zero;

        public MenuScreen(Game game, SpriteFont smallFont, SpriteFont largeFont, Texture2D background, Texture2D elements)
            : base(game)
        {
            // TODO: Construct any child components here

            this.elements = elements;
            Components.Add(new ImageComponent(game, background, ImageComponent.DrawMode.Stretch)); //Add image component

            //Create Menu
            string[] items = { "One Player", "Two Players", "Help", "Quit" }; //Creates and array with entries for the menu
            menu = new TextMenuComponent(game, smallFont, largeFont);
            menu.SetMenuItems(items);
            Components.Add(menu);

            //Get the current sprite batch
            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));

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

            if (!menu.Visible) //If the menu isn't visible
            {
                if (line2Position.X >= (Game.Window.ClientBounds.Width - 595) / 2)
                {
                    line2Position.X -= 15; //Moves line 2 left if it is not in its final position
                }

                if (line1Position.X <= (Game.Window.ClientBounds.Width - 715) / 2)
                {
                    line1Position.X += 15; //Moves line 1 right if it is not in its final position
                }
                else
                {
                    menu.Visible = true; //Makes the menu visible
                    menu.Enabled = true; //Enables the Menu

#if XBOX360 //Calculation for position of line 3 on Xbox 360
                    line3Position = new Vector2((line2Position.X + line2Rectangle.Width - line3Rectangle.Width / 2), line2Position.Y);
#else //Calculation for position of line 3 on PC
                    line3Position = new Vector2((line2Position.X + line2Rectangle.Width - line3Rectangle.Width / 2) - 80, line2Position.Y);
#endif
                    showLine3 = true; //Shows line 3
                }
            }
            else //If line 1 and line 2 are in their final position, makes line 3 "flash" 
            {
                elapsedTime += gameTime.ElapsedGameTime; //Increases "elapsedTime" using elapsed game time
                if (elapsedTime > TimeSpan.FromSeconds(1))// "Flash threshold"
                {
                    elapsedTime -= TimeSpan.FromSeconds(1); //Decreases "elapsedTime" below "flash threshold"
                    showLine3 = !showLine3; //"Flashes" line 3 off and on
                }
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            spriteBatch.Draw(elements, line1Position, line1Rectangle, Color.White); //Draws line 1
            spriteBatch.Draw(elements, line2Position, line2Rectangle, Color.White); //Draws line 2
            if (showLine3) //If line 3 is showing, draw line 3
            {
                spriteBatch.Draw(elements, line3Position, line3Rectangle, Color.White);
            }
        }

        public override void Show() //Show the menu screen
        {
            line1Position.X = -1 * line1Rectangle.Width;
            line1Position.Y = 40;
            line2Position.X = Game.Window.ClientBounds.Width;
            line2Position.Y = 180;
            menu.Position = new Vector2((Game.Window.ClientBounds.Width - menu.Width) / 2, 330); //Center menu on screen
            //These are visible when the game title is done
            menu.Visible = false;
            menu.Enabled = false;
            showLine3 = false;

            base.Show();
        }

        public override void Hide() //Hide the menu screen
        {
            base.Hide();
        }

        public int SelectedMenuIndex //Used to select an option from the menu screen
        {
            get { return menu.SelectedIndex; }
        }
    }
}