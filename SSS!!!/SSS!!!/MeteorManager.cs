using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SSS___
{
    /// <summary>
    /// This is a game component that manages all of the meteors in the game
    /// </summary>
    public class MeteorManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected List<SmallMeteor> sMeteors; //List of active small meteors
        protected List<MediumMeteor> mMeteors; //List of active medium meteors
        private const int STARTMETEORCOUNT = 2; //Constant for initial meteor count
        private const int ADDSMALLMETEORTIME = 150; //Time for a new small meteor
        private const int ADDMEDIUMMETEORTIME = 300; //Time for a new medium meteor

        protected Texture2D sMeteorTexture; //Texture for small meteor
        protected Texture2D mMeteorTexture; //Texture for medium meteor
        protected TimeSpan elapsedTime = TimeSpan.Zero;

        private Random random = new Random();


        public MeteorManager(Game game, ref Texture2D spriteSheet)
            : base(game)
        {
            // TODO: Construct any child components here
            sMeteorTexture = spriteSheet;
            sMeteors = new List<SmallMeteor>(); //Create list for small meteors
            mMeteorTexture = spriteSheet;
            mMeteors = new List<MediumMeteor>(); //Create list for medium meteors
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            sMeteors.Clear(); //Clears the list of small meteors
            mMeteors.Clear(); //Clears the list of medium meteors

            Start(); //Initialize all meteors in the lists
            for (int i = 0; i < sMeteors.Count; i++)
            {
                sMeteors[i].Initialize();
            }

            for (int i = 0; i < mMeteors.Count; i++)
            {
                mMeteors[i].Initialize();
            }

            base.Initialize();
        }

        public void Start() //Start the meteors
        {
            elapsedTime = TimeSpan.Zero; //Initialize a counter
            for (int i = 0; i < STARTMETEORCOUNT; i++)
            {
                    AddNewSmallMeteor();
                    AddNewMediumMeteor();
            }
        }

        public List<SmallMeteor> AllSmallMeteors //List of all small meteors in the game
        {
            get { return sMeteors; }
        }

        public List<MediumMeteor> AllMediumMeteors //List of all medium meteors in the game
        {
            get { return mMeteors; }
        }

        private void CheckForNewSmallMeteor(GameTime gameTime) //Check if its time for a new small meteor
        {
            //Add a new small meteor each ADDSMALLMETEORTIME
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(ADDSMALLMETEORTIME))
            {
                elapsedTime -= TimeSpan.FromSeconds(ADDSMALLMETEORTIME);
                    AddNewSmallMeteor();
            }
        }

        private void CheckForNewMediumMeteor(GameTime gameTime) //Check if its time for a new medium meteor
        {
            //Add a new medium meteor each ADDMEDIUMMETEORTIME
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(ADDMEDIUMMETEORTIME))
            {
                elapsedTime -= TimeSpan.FromSeconds(ADDMEDIUMMETEORTIME);
                AddNewMediumMeteor();
            }
        }

        private void AddNewSmallMeteor() //Add a new small meteor to the game
        {
            SmallMeteor newSmallMeteor = new SmallMeteor(Game, ref sMeteorTexture); //Create new small meteor
            newSmallMeteor.Initialize(); //Initialize the new small meteor
            sMeteors.Add(newSmallMeteor); //Add the new small meteor to the list of small meteors in the game
            newSmallMeteor.Index = sMeteors.Count - 1; //Sets the identifier
        }

        private void AddNewMediumMeteor() //Add a new medium meteor to the game
        {
            MediumMeteor newMediumMeteor = new MediumMeteor(Game, ref mMeteorTexture); //Create new medium meteor
            newMediumMeteor.Initialize(); //Initialize the new medium meteor
            mMeteors.Add(newMediumMeteor); //Add the new medium meteor to the list of medium meteors in the game
            newMediumMeteor.Index = mMeteors.Count - 1; //Sets the identifier
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            CheckForNewSmallMeteor(gameTime);
            CheckForNewMediumMeteor(gameTime);

            //Update small meteors
            for (int i = 0; i < sMeteors.Count; i++)
            {
                sMeteors[i].Update(gameTime);
            }

            //Update medium meteors
            for (int i = 0; i < mMeteors.Count; i++)
            {
                mMeteors[i].Update(gameTime);
            }

            base.Update(gameTime);
        }

        //Check if the ship collided with a meteor, returns true if a collision occurs
        public bool SmallMeteorCollision(Rectangle rect)
        {
            for (int i = 0; i < sMeteors.Count; i++)
            {
                if (sMeteors[i].CheckCollision(rect))
                {
                    sMeteors[i].PutinStartPosition(); //Reset the meteor
                    return true;
                }
            }
            return false;
        }

        public bool MediumMeteorCollision(Rectangle rect)
        {
            for (int i = 0; i < mMeteors.Count; i++)
            {
                if (mMeteors[i].CheckCollision(rect))
                {
                    mMeteors[i].PutinStartPosition(); //Reset the meteor
                    return true;
                }
            }
            return false;
        }

        public override void Draw(GameTime gameTime)
        {
            //Draw the small meteors
            for (int i = 0; i < sMeteors.Count; i++)
            {
                sMeteors[i].Draw(gameTime);
            }

            //Draw the medium meteors
            for (int i = 0; i < mMeteors.Count; i++)
            {
                mMeteors[i].Draw(gameTime);
            }

            base.Draw(gameTime);
        }
    }
}