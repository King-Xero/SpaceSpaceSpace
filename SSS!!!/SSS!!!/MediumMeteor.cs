using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SSS___.Core;

namespace SSS___
{
    /// <summary>
    /// This is a game component that implements a medium animated meteor
    /// </summary>
    public class MediumMeteor : Sprite
    {
        
        protected int Yspeed; //Vertical velocity
        protected int Xspeed; //Horizontal velocity
        protected Random random; //Random number generator
        private int index; //ID for this meteor


        public MediumMeteor(Game game, ref Texture2D spriteSheet)
            : base(game, ref spriteSheet)
        {
            // TODO: Construct any child components here
            Frames = new List<Rectangle>();
            Rectangle frame = new Rectangle();
            frame.X = 0; //X coordinate of first frame
            frame.Y = 0; //Y coordinate of first frame
            frame.Width = 65; //frame width
            frame.Height = 65; //frame height
            Frames.Add(frame); //Add frame to list of frames

            //Y coordinates of remaining frames and adding them to the list
            frame.Y = 65;
            Frames.Add(frame);

            frame.Y = 130;
            Frames.Add(frame);

            frame.Y = 195;
            Frames.Add(frame);

            frame.Y = 260;
            Frames.Add(frame);

            frame.Y = 325;
            Frames.Add(frame);

            frame.Y = 390;
            Frames.Add(frame);

            frame.Y = 455;
            Frames.Add(frame);

            random = new Random(GetHashCode()); //Initialize the random number generator
            PutinStartPosition(); //Put meteor in start position
        }

        public void PutinStartPosition()
        {
            //Set the meteor to a random position along the top of the screen, with random vertical and horizontal speed
            position.X = random.Next(Game.Window.ClientBounds.Width - currentFrame.Width);
            position.Y = 0;
            YSpeed = 1 + random.Next(4);
            XSpeed = random.Next(3) - 1;
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
            //Check if meteor is still visible
            if ((position.Y >= Game.Window.ClientBounds.Height) ||
                (position.X >= Game.Window.ClientBounds.Width) ||
                (position.X <= 0))
            {
                PutinStartPosition();
            }
            //Move Meteor
            position.Y += Yspeed;
            position.X += Xspeed;

            base.Update(gameTime);
        }
        
        //Vertical velocity
        public int YSpeed
        {
            get { return Yspeed; }
            set
            {
                Yspeed = value;
                frameDelay = 200 - (Yspeed * 5); //Animation speed depends how fast the meteor is "falling"
            }
        }

        //Horizontal velocity
        public int XSpeed
        {
            get { return Xspeed; }
            set { Xspeed = value; }
        }

        public int Index //ID for this meteor
        {
            get { return index; }
            set { index = value; }
        }


        //Check if the meteor intersects with the specified rectangle, returns true if a collision occurs
        public bool CheckCollision(Rectangle rect)
        {
            Rectangle spriteRect = new Rectangle((int)position.X, (int)position.Y, currentFrame.Width, currentFrame.Height);
            return spriteRect.Intersects(rect);
        }
    }
}