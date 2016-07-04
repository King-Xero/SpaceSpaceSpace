using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SSS___.Core;

namespace SSS___
{
    /// <summary>
    /// This is a game component that implements the health pack
    /// </summary>
    public class HealthPack : Sprite
    {
        protected Texture2D texture; //Texture for the health pack
        protected Random random; //Random number genterator

        public HealthPack(Game game, ref Texture2D spriteSheet)
            : base(game, ref spriteSheet)
        {
            // TODO: Construct any child components here
            texture = spriteSheet;

            Frames = new List<Rectangle>(); //List of frames in the animation
            Rectangle frame = new Rectangle(); //Rectangle for a frame of the animation
            frame.X = 169; //X coordinate of first frame
            frame.Y = 0; //Y coordinate of first frame
            frame.Width = 15; //frame width
            frame.Height = 15; //frame height
            Frames.Add(frame); //Add frame to list of frames

            //Y coordinates of remaining frames and adding them to the list
            frame.Y = 15;
            Frames.Add(frame);

            frame.Y = 30;
            Frames.Add(frame);

            frame.Y = 45;
            Frames.Add(frame);

            frame.Y = 60;
            Frames.Add(frame);

            frame.Y = 75;
            Frames.Add(frame);

            frame.Y = 90;
            Frames.Add(frame);

            frame.Y = 105;
            Frames.Add(frame);

            frame.Y = 120;
            Frames.Add(frame);

            frame.Y = 135;
            Frames.Add(frame);

            frame.Y = 150;
            Frames.Add(frame);

            frame.Y = 165;
            Frames.Add(frame);

            frame.Y = 180;
            Frames.Add(frame);

            frame.Y = 195;
            Frames.Add(frame);

            frame.Y = 210;
            Frames.Add(frame);

            frameDelay = 200;

            random = new Random(GetHashCode()); //Initialize the random number generator
            PutInStartPosition(); //Put the health pack in the start position

        }

        public void PutInStartPosition()
        {
            //Set the health pack to a random position along the top of the screen
            position.X = random.Next(Game.Window.ClientBounds.Width - currentFrame.Width);
            position.Y = -15;
            Enabled = false; //Disabled when put in start position
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
            if (position.Y >= Game.Window.ClientBounds.Height) //Check if the health pack is still visible
            {
                position.Y = -15;
                Enabled = false;
            }

            position.Y += 2; //Move

            base.Update(gameTime);
        }

        //Check if the meteor intersects with the specified rectangle, returns true if a collision occurs
        public bool CheckCollision(Rectangle rect)
        {
            Rectangle spriteRect = new Rectangle((int)position.X, (int)position.Y, currentFrame.Width, currentFrame.Height);
            return spriteRect.Intersects(rect);
        }
    }
}