using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SSS___.Core;

namespace SSS___
{
    /// <summary>
    /// This is a game component that implements the boss enemy
    /// </summary>
    public class EnemyBoss : Sprite
    {
        
        protected int Yspeed; //Vertical velocity
        protected int Xspeed; //Horizontal velocity
        protected Random random; //Random number generator
        private int index; //Unique ID for this enemy


        public EnemyBoss(Game game, ref Texture2D spriteSheet)
            : base(game, ref spriteSheet)
        {
            // TODO: Construct any child components here
            Frames = new List<Rectangle>();
            Rectangle frame = new Rectangle();
            frame.X = 65; //X coordinate of first frame
            frame.Y = 397; //Y coordinate of first frame
            frame.Width = 96; //frame width
            frame.Height = 96; //frame height
            Frames.Add(frame); //Add frame to list of frames

            random = new Random(GetHashCode()); //Initialize the random number generator
            PutinStartPosition(); //Put enemy in start position
        }

        public void PutinStartPosition()
        {
            //Set the boss to a random position along the top of the screen, with random vertical and horizontal speed
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
            //Check if boss is still visible
            if ((position.Y >= Game.Window.ClientBounds.Height) ||
                (position.X >= Game.Window.ClientBounds.Width) ||
                (position.X <= 0))
            {
                PutinStartPosition();
            }
            //Move the boss
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
                //frameDelay = 200 - (Yspeed * 5);
            }
        }

        //Horizontal velocity
        public int XSpeed
        {
            get { return Xspeed; }
            set { Xspeed = value; }
        }

        //Meteor identifier
        public int Index
        {
            get { return index; }
            set { index = value; }
        }

        //Check if the boss intersects with the specified rectangle, returns true if a collision occurs
        public bool CheckCollision(Rectangle rect)
        {
            Rectangle spriteRect = new Rectangle((int)position.X, (int)position.Y, currentFrame.Width, currentFrame.Height);
            return spriteRect.Intersects(rect);
        }
    }
}