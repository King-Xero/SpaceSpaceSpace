using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SSS___.Core;

namespace SSS___
{
    /// <summary>
    /// This is a game component that implements the help screen
    /// </summary>
    public class HelpScreen : GameScreen
    {
        public HelpScreen(Game game, Texture2D textureBack, Texture2D textureFront)
            : base(game)
        {
            // TODO: Construct any child components here
            Components.Add(new ImageComponent(game, textureBack, ImageComponent.DrawMode.Stretch)); //Add the help screen background
            Components.Add(new ImageComponent(game, textureFront, ImageComponent.DrawMode.Center)); //Add the help screen info
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
    }
}