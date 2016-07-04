using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SSS___.Core
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class ImageComponent : Microsoft.Xna.Framework.DrawableGameComponent
    {
        public enum DrawMode
        {
            Center = 1,
            Stretch,
        };

        protected readonly Texture2D texture; //Texture to draw
        protected readonly DrawMode drawMode; //Draw mode
        protected SpriteBatch spriteBatch = null; //SpriteBatch
        protected Rectangle imageRectangle; //Image rectangle


        public ImageComponent(Game game, Texture2D texture, DrawMode drawMode)
            : base(game)
        {
            // TODO: Construct any child components here
            this.texture = texture;
            this.drawMode = drawMode;
            //get the current spriteBatch
            spriteBatch = (SpriteBatch)
                Game.Services.GetService(typeof (SpriteBatch));

            //Create a rectangle with the size and position of the image
            switch (drawMode)
            {
                case DrawMode.Center: //(Calculation for centered image)
                    imageRectangle = new Rectangle((Game.Window.ClientBounds.Width - texture.Width)/2,
                        (Game.Window.ClientBounds.Height - texture.Height)/2, texture.Width, texture.Height);
                    break;
                case DrawMode.Stretch: //(Calculation for stretched image)
                    imageRectangle = new Rectangle(0, 0, Game.Window.ClientBounds.Width, Game.Window.ClientBounds.Height);
                    break;
            }
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

            base.Update(gameTime);
        }

        //Allows the GameComponent to draw itself
        public override void Draw(GameTime gameTime) 
        {
            spriteBatch.Draw(texture, imageRectangle, Color.White); 
            base.Draw(gameTime);
        }
    }
}