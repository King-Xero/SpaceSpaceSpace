using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SSS___
{
    /// <summary>
    /// This is a game component that manages all of the enemies in the game
    /// </summary>
    public class EnemyManager : Microsoft.Xna.Framework.DrawableGameComponent
    {
        protected List<Enemy1> enemy1s; //List of active ememy1s
        protected List<Enemy2> enemy2s; //List of active ememy2s
        protected List<Enemy3> enemy3s; //List of active ememy3s
        protected List<EnemyBoss> enemyBs; //List of active Bosses
        private const int STARTENEMYCOUNT = 4; //Constant for initial enemy count
        private const int ADDENEMY1TIME = 120; //Time for a new enemy1
        private const int ADDENEMY2TIME = 180; //Time for a new enemy2
        private const int ADDENEMY3TIME = 240; //Time for a new enemy3
        private const int ADDENEMYBTIME = 5; //Time for a new boss

        protected Texture2D enemy1Texture; //Texture for enemy 1
        protected Texture2D enemy2Texture; //Texture for enemy 2
        protected Texture2D enemy3Texture; //Texture for enemy 3
        protected Texture2D enemyBTexture; //Texture for boss
        protected TimeSpan elapsedTime = TimeSpan.Zero;

        private Random random = new Random();


        public EnemyManager(Game game, ref Texture2D spriteSheet)
            : base(game)
        {
            // TODO: Construct any child components here
            enemy1Texture = spriteSheet;
            enemy1s = new List<Enemy1>();
            enemy2Texture = spriteSheet;
            enemy2s = new List<Enemy2>();
            enemy3Texture = spriteSheet;
            enemy3s = new List<Enemy3>();
            enemyBTexture = spriteSheet;
            enemyBs = new List<EnemyBoss>();
        }

        /// <summary>
        /// Allows the game component to perform any initialization it needs to before starting
        /// to run.  This is where it can query for any required services and load content.
        /// </summary>
        public override void Initialize()
        {
            // TODO: Add your initialization code here
            enemy1s.Clear(); //Clears the list of enemy1s
            enemy2s.Clear(); //Clears the list of enemy2s
            enemy3s.Clear(); //Clears the list of enemy3s
            enemyBs.Clear(); //Clears the list of bosses            

            Start(); //Initialize all enemies in the list

            for (int i = 0; i < enemy1s.Count; i++)
            {
                enemy1s[i].Initialize();
            }

            for (int i = 0; i < enemy2s.Count; i++)
            {
                enemy2s[i].Initialize();
            }

            for (int i = 0; i < enemy3s.Count; i++)
            {
                enemy3s[i].Initialize();
            }

            for (int i = 0; i < enemyBs.Count; i++)
            {
                enemyBs[i].Initialize();
            }

            base.Initialize();
        }

        public void Start() //Start the enemies
        {
            elapsedTime = TimeSpan.Zero; //Initialize a counter
            for (int i = 0; i < STARTENEMYCOUNT; i++)
            {
                    AddNewEnemy1();
                    //Other enemy types won't spawn in game unless they are started here. 
                    //However as a result of this they appear at the beginning of the game which was not intended.
                    //AddNewEnemy2();
                    //AddNewEnemy3();
                    //AddNewEnemyB();
            }
        }

        public List<Enemy1> AllEnemy1s
        {
            get { return enemy1s; }
        }

        public List<Enemy2> AllEnemy2s
        {
            get { return enemy2s; }
        }

        public List<Enemy3> AllEnemy3s
        {
            get { return enemy3s; }
        }

        public List<EnemyBoss> AllEnemyBs
        {
            get { return enemyBs; }
        }

        private void CheckForNewEnemy1(GameTime gameTime) //Check if its time for a new enemy1
        {
            //Add a new enemy1 each ADDENEMY1TIME
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(ADDENEMY1TIME))
            {
                elapsedTime -= TimeSpan.FromSeconds(ADDENEMY1TIME);
                    AddNewEnemy1();
            }
        }

        private void CheckForNewEnemy2(GameTime gameTime) //Check if its time for a new enemy2
        {
            //Add a new enemy2 each ADDENEMY2TIME
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(ADDENEMY2TIME))
            {
                elapsedTime -= TimeSpan.FromSeconds(ADDENEMY2TIME);
                AddNewEnemy2();
            }
        }

        private void CheckForNewEnemy3(GameTime gameTime) //Check if its time for a new enemy3
        {
            //Add a new enemy3 each ADDENEMY1TIME
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromSeconds(ADDENEMY3TIME))
            {
                elapsedTime -= TimeSpan.FromSeconds(ADDENEMY3TIME);
                AddNewEnemy3();
            }
        }

        private void CheckForNewEnemyB(GameTime gameTime) //Check if its time for a new boss
        {
            //Add a new boss each ADDENEMYBTIME
            elapsedTime += gameTime.ElapsedGameTime;

            if (elapsedTime > TimeSpan.FromMinutes(ADDENEMYBTIME))
            {
                elapsedTime -= TimeSpan.FromMinutes(ADDENEMYBTIME);
                AddNewEnemyB();
            }
        }

        private void AddNewEnemy1() //Add a new enemy1 to the game
        {
            Enemy1 newEnemy1 = new Enemy1(Game, ref enemy1Texture); //Create new enemy1
            newEnemy1.Initialize(); //Initialize the new enemy1
            enemy1s.Add(newEnemy1); //Add the new enemy1 to the list of enemy1s in the game
            newEnemy1.Index = enemy1s.Count - 1; //Sets the identifier
        }

        private void AddNewEnemy2() //Add a new enemy2 to the game
        {
            Enemy2 newEnemy2 = new Enemy2(Game, ref enemy2Texture); //Create new enemy2
            newEnemy2.Initialize(); //Initialize the new enemy2
            enemy2s.Add(newEnemy2); //Add the new enemy2 to the list of enemy2s in the game
            newEnemy2.Index = enemy2s.Count - 1; //Sets the identifier
        }

        private void AddNewEnemy3() //Add a new enemy3 to the game
        {
            Enemy3 newEnemy3 = new Enemy3(Game, ref enemy3Texture); //Create new enemy3
            newEnemy3.Initialize(); //Initialize the new enemy3
            enemy3s.Add(newEnemy3); //Add the new enemy3 to the list of enemy3s in the game
            newEnemy3.Index = enemy3s.Count - 1; //Sets the identifier
        }

        private void AddNewEnemyB() //Add a new boss to the game
        {
            EnemyBoss newEnemyB = new EnemyBoss(Game, ref enemyBTexture); //Create new boss
            newEnemyB.Initialize(); //Initialize the new boss
            enemyBs.Add(newEnemyB); //Add the new boss to the list of bosses in the game
            newEnemyB.Index = enemyBs.Count - 1; //Sets the identifier
        }

        /// <summary>
        /// Allows the game component to update itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        public override void Update(GameTime gameTime)
        {
            // TODO: Add your update code here
            CheckForNewEnemy1(gameTime);
            CheckForNewEnemy2(gameTime);
            CheckForNewEnemy3(gameTime);
            CheckForNewEnemyB(gameTime);

            //Update enemy1s
            for (int i = 0; i < enemy1s.Count; i++)
            {
                enemy1s[i].Update(gameTime);
            }

            //Update enemy2s
            for (int i = 0; i < enemy2s.Count; i++)
            {
                enemy2s[i].Update(gameTime);
            }

            //Update enemy3s
            for (int i = 0; i < enemy3s.Count; i++)
            {
                enemy3s[i].Update(gameTime);
            }

            //Update bosses
            for (int i = 0; i < enemyBs.Count; i++)
            {
                enemyBs[i].Update(gameTime);
            }

            base.Update(gameTime);
        }

        //Check if the ship collided with an enemy, returns true if a collision occurs
        public bool Enemy1Collision(Rectangle rect)
        {
            for (int i = 0; i < enemy1s.Count; i++)
            {
                if (enemy1s[i].CheckCollision(rect))
                {
                    enemy1s[i].PutinStartPosition(); //Reset the enemy
                    return true;
                }
            }
            return false;
        }

        public bool Enemy2Collision(Rectangle rect)
        {
            for (int i = 0; i < enemy2s.Count; i++)
            {
                if (enemy2s[i].CheckCollision(rect))
                {
                    enemy2s[i].PutinStartPosition(); //Reset the enemy
                    return true;
                }
            }
            return false;
        }


        public bool Enemy3Collision(Rectangle rect)
        {
            for (int i = 0; i < enemy3s.Count; i++)
            {
                if (enemy3s[i].CheckCollision(rect))
                {
                    enemy3s[i].PutinStartPosition(); //Reset the enemy
                    return true;
                }
            }
            return false;
        }

        public bool BossCollision(Rectangle rect)
        {
            for (int i = 0; i < enemyBs.Count; i++)
            {
                if (enemyBs[i].CheckCollision(rect))
                {
                    enemyBs[i].PutinStartPosition(); //Reset the enemy
                    return true;
                }
            }
            return false;
        }

        public override void Draw(GameTime gameTime)
        {
            //Draw the enemy1s
            for (int i = 0; i < enemy1s.Count; i++)
            {
                enemy1s[i].Draw(gameTime);
            }

            //Draw the enemy2s
            for (int i = 0; i < enemy2s.Count; i++)
            {
                enemy2s[i].Draw(gameTime);
            }

            //Draw the enemy3s
            for (int i = 0; i < enemy3s.Count; i++)
            {
                enemy3s[i].Draw(gameTime);
            }

            //Draw the bosses
            for (int i = 0; i < enemyBs.Count; i++)
            {
                enemyBs[i].Draw(gameTime);
            }

            base.Draw(gameTime);
        }
    }
}