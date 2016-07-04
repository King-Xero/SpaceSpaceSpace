using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SSS___.Core
{
    /// <summary>
    /// Sprite Class
    /// </summary>
    public class Sprite : Microsoft.Xna.Framework.DrawableGameComponent
    {

        private int activeFrame; //Active frame from the sprite sheet animation
        private readonly Texture2D texture; //Will hold the sprite sheet texture
        private List<Rectangle> frames; //List of sprite frames from the texture

        protected Vector2 position;
        protected TimeSpan elapsedTime = TimeSpan.Zero;
        protected Rectangle currentFrame; //Rectangle of the current sprite frame
        protected long frameDelay;
        protected SpriteBatch spBatch;

        public Sprite(Game game, ref Texture2D spriteSheet)
            : base(game)
        {
            // TODO: Construct any child components here

            texture = spriteSheet;
            activeFrame = 0;
        }

        public List<Rectangle> Frames //List with frames of the animated sprite
        {
            get { return frames; }
            set { frames = value; }
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            spBatch = (SpriteBatch) Game.Services.GetService(typeof (SpriteBatch));

            base.Initialize();
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            elapsedTime += gameTime.ElapsedGameTime;
            //Check if next frame should be displayed, and if so, change it each n milliseconds
            if (elapsedTime > TimeSpan.FromMilliseconds(frameDelay))
            {
                elapsedTime -= TimeSpan.FromMilliseconds(frameDelay);
                activeFrame++;
                if (activeFrame == frames.Count)
                {
                    activeFrame = 0;
                }
                currentFrame = frames[activeFrame]; //Get the current frame
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            spBatch.Draw(texture, position, currentFrame, Color.White); //Draw the current frame in the current position on the screen

            base.Draw(gameTime);
        }
    }
}