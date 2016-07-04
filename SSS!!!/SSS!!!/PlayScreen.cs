using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SSS___.Core;

namespace SSS___
{
    /// <summary>
    /// This is a game component that implements the play screen
    /// </summary>
    public class PlayScreen : GameScreen
    {
        //Basics
        protected Texture2D playTexture; //Texture for all the elements of the game
        protected SpriteBatch spriteBatch = null;

        //Game elements
        protected Player player1; //Player 1
        protected Player player2; //Player 2
        protected MeteorManager meteors; //Meteor Manager
        protected EnemyManager enemies; //Enemy Manager
        protected HealthPack healthPack; //Health pack
        protected RumblePad rumblePad; //Rumble for Xbox controller
        protected ImageComponent background; //Game background
        protected Score scorePlayer1; //Score for player 1
        protected Score scorePlayer2; //Score for player 2

        //GUI
        protected Vector2 pausePosition; //Position of pause prompt
        protected Vector2 gameoverPosition; //Position of game over prompt
        protected Rectangle pauseRect = new Rectangle(170, 225, 144, 41); //Rectangle for pause prompt
        protected Rectangle gameoverRect = new Rectangle(173, 272, 266, 41); //Rectangle for game over prompt

        //GameState elements
        protected bool paused; //Is the game paused?
        protected bool gameOver; //Is it Game over?
        protected TimeSpan elapsedTime = TimeSpan.Zero;
        protected bool twoPlayers; //Is there 2 players?
        

        public PlayScreen(Game game, Texture2D spriteSheet, Texture2D backgroundTexture, SpriteFont font)
            : base(game)
        {
            // TODO: Construct any child components here
            
            //Add the game background image
            background = new ImageComponent(game, backgroundTexture, ImageComponent.DrawMode.Stretch);
            Components.Add(background);

            playTexture = spriteSheet;

            //Add the meteors
            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch));
            meteors = new MeteorManager(Game, ref playTexture);
            Components.Add(meteors);

            //Add the enemies
            enemies = new EnemyManager(Game, ref playTexture);
            Components.Add(enemies);

            //Add player 1
            player1 = new Player(Game, ref playTexture, PlayerIndex.One, new Rectangle(105, 0, 50, 40));
            player1.Initialize();
            Components.Add(player1);

            //Add player 2
            player2 = new Player(Game, ref playTexture, PlayerIndex.Two, new Rectangle(105, 100, 50, 50));
            player2.Initialize();
            Components.Add(player2);

            //Add the score for player 1
            scorePlayer1 = new Score(game, font, Color.Blue);
            scorePlayer1.Position = new Vector2(10, 10);
            Components.Add(scorePlayer1);
            //Add the score for player 2
            scorePlayer2 = new Score(game, font, Color.Green);
            scorePlayer2.Position = new Vector2(Game.Window.ClientBounds.Width - 200, 10);
            Components.Add(scorePlayer2);

            //Add the rumble for Xbox controller
            rumblePad = new RumblePad(game);
            Components.Add(rumblePad);

            //Add the health packs
            healthPack = new HealthPack(game, ref playTexture);
            healthPack.Initialize();
            Components.Add(healthPack);
        }

        public bool TwoPlayers //Indicate whether or not 2 player mode
        {
            get { return twoPlayers; }
            set { twoPlayers = value; }
        }

        public bool GameOver //True, if the game is in gameOver state
        {
            get { return gameOver; }
        }

        public bool Paused //Paused mode
        {
            get { return paused; }
            set { paused = value; }
        }

        public override void Show() //Show the play screen
        {
            meteors.Initialize();
            enemies.Initialize();
            healthPack.PutInStartPosition();
            player1.Reset();
            player2.Reset();

            paused = false; //Game is NOT paused when started
            //Paused prompt position
            pausePosition.X = (Game.Window.ClientBounds.Width - pauseRect.Width) / 2;
            pausePosition.Y = (Game.Window.ClientBounds.Height - pauseRect.Height) / 2;

            gameOver = false; //Game over is NOT true when game is started
            //Game over prompt position
            gameoverPosition.X = (Game.Window.ClientBounds.Width - gameoverRect.Width) / 2;
            gameoverPosition.Y = (Game.Window.ClientBounds.Height - gameoverRect.Height) / 2;

            //Player 2 elements are visible and enabled depending on the status of "twoPlayers"
            player2.Visible = twoPlayers;
            player2.Enabled = twoPlayers;
            scorePlayer2.Visible = twoPlayers;
            scorePlayer2.Enabled = twoPlayers;

            base.Show();
        }

        public override void Hide() //Hide the play screen
        {
            //Stop the controller rumble
            rumblePad.Stop(PlayerIndex.One);
            rumblePad.Stop(PlayerIndex.Two);

            base.Hide();
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
            if ((!paused) && (!gameOver))
            {
                HandleDamages(); //Check meteor collisions
                HandleHealthPack(gameTime); //Check if player has collected health pack
                //Update player's score and health
                scorePlayer1.ScorePoints = player1.Score;
                scorePlayer1.HealthPoints = player1.Health;
                //if its a two player game, update player 2's score and health
                if (twoPlayers)
                {
                    scorePlayer2.ScorePoints = player2.Score;
                    scorePlayer2.HealthPoints = player2.Health;
                }
                //Check if the player is dead
                gameOver = ((player1.Health <= 0) || (player2.Health <= 0));
                if (gameOver)
                {
                    player1.Visible = (player1.Health > 0);
                    player2.Visible = (player2.Health > 0) && twoPlayers;
                    //Stop the rumble
                    rumblePad.Stop(PlayerIndex.One);
                    rumblePad.Stop(PlayerIndex.Two);
                }
                base.Update(gameTime);
            }
            if (gameOver) //Meteors, enemies and health packs keep their animation when its game over
            {
                meteors.Update(gameTime);
                enemies.Update(gameTime);
                healthPack.Update(gameTime);
            }
        }

        private void HandleDamages() //Check collisions
        {
           //Check small meteor collisions for player 1
            if (meteors.SmallMeteorCollision(player1.GetBounds()))
            {
                rumblePad.GamepadRumble(PlayerIndex.One, 500, 1.0f, 1.0f); //Activate rumble
                player1.Health -= 5; //Player damage
            }

            //Check medium meteor collisions for player 1
            if (meteors.MediumMeteorCollision(player1.GetBounds()))
            {
                rumblePad.GamepadRumble(PlayerIndex.One, 500, 1.0f, 1.0f); //Activate rumble
                player1.Health -= 10; //Player damage
            }

           //Check small meteor collisions for player 2
            if (twoPlayers)
            {
                if (meteors.SmallMeteorCollision(player2.GetBounds()))
                {
                    rumblePad.GamepadRumble(PlayerIndex.Two, 500, 1.0f, 1.0f); //Activate rumble
                    player2.Health -= 5; //Player damage
                }
            }

            //Check medium meteor collisions for player 2
            if (twoPlayers)
            {
                if (meteors.MediumMeteorCollision(player2.GetBounds()))
                {
                    rumblePad.GamepadRumble(PlayerIndex.Two, 500, 1.0f, 1.0f); //Activate rumble
                    player2.Health -= 10; //Player damage
                }
            }

            //Check enemy1 collisions for player 1
            if (enemies.Enemy1Collision(player1.GetBounds()))
            {
                rumblePad.GamepadRumble(PlayerIndex.One, 500, 1.0f, 1.0f); //Activate rumble
                player1.Health -= 20; //Player damage
            }

            //Check enemy2 collisions for player 1
            if (enemies.Enemy2Collision(player1.GetBounds()))
            {
                rumblePad.GamepadRumble(PlayerIndex.One, 500, 1.0f, 1.0f); //Activate rumble
                player1.Health -= 30; //Player damage
            }

            //Check enemy3 collisions for player 1
            if (enemies.Enemy3Collision(player1.GetBounds()))
            {
                rumblePad.GamepadRumble(PlayerIndex.One, 500, 1.0f, 1.0f); //Activate rumble
                player1.Health -= 40; //Player damage
            }

            //Check boss collisions for player 1
            if (enemies.BossCollision(player1.GetBounds()))
            {
                rumblePad.GamepadRumble(PlayerIndex.One, 500, 1.0f, 1.0f); //Activate rumble
                player1.Health -= 50; //Player damage
            }

            //Check enemy1 collisions for player 2
            if (twoPlayers)
            {
                if (enemies.Enemy1Collision(player2.GetBounds()))
                {
                    rumblePad.GamepadRumble(PlayerIndex.Two, 500, 1.0f, 1.0f); //Activate rumble
                    player2.Health -= 20; //Player damage
                }
            }

            //Check enemy2 collisions for player 2
            if (twoPlayers)
            {
                if (enemies.Enemy2Collision(player2.GetBounds()))
                {
                    rumblePad.GamepadRumble(PlayerIndex.Two, 500, 1.0f, 1.0f); //Activate rumble
                    player2.Health -= 30; //Player damage
                }
            }

            //Check enemy3 collisions for player 2
            if (twoPlayers)
            {
                if (enemies.Enemy3Collision(player2.GetBounds()))
                {
                    rumblePad.GamepadRumble(PlayerIndex.Two, 500, 1.0f, 1.0f); //Activate rumble
                    player2.Health -= 40; //Player damage
                }
            }

            //Check enemy collisions for player 2
            if (twoPlayers)
            {
                if (enemies.BossCollision(player2.GetBounds()))
                {
                    rumblePad.GamepadRumble(PlayerIndex.Two, 500, 1.0f, 1.0f); //Activate rumble
                    player2.Health -= 50; //Player damage
                }
            }

            //Check if the players collide with each other
            if (twoPlayers)
            {
                if (player1.GetBounds().Intersects(player2.GetBounds()))
                {
                    //Player 1
                    rumblePad.GamepadRumble(PlayerIndex.One, 500, 1.0f, 1.0f); //Activate rumble
                    player1.Health -= 10; //Player damage
                    player1.Score -= 10; //Score penalty
                    //Player 2
                    rumblePad.GamepadRumble(PlayerIndex.Two, 500, 1.0f, 1.0f); //Activate rumble
                    player2.Health -= 10; //Player damage
                    player2.Score -= 10; //Score penalty
                }
            }
        }

        private void HandleHealthPack(GameTime gameTime)
        {
            //Check if player 1 collects a health pack
            if (healthPack.CheckCollision(player1.GetBounds()))
            {
                elapsedTime = TimeSpan.Zero;
                healthPack.PutInStartPosition(); //Reset health pack
                player1.Health += 50; //Health boost
            }

            //Check if player 2 collects a health pack
            if (twoPlayers)
            {
                if (healthPack.CheckCollision(player2.GetBounds()))
                {
                    elapsedTime = TimeSpan.Zero;
                    healthPack.PutInStartPosition(); //Reset health pack
                    player2.Health += 50; //Health boost
                }
            }

            //Check if its time to send a health pack
            elapsedTime += gameTime.ElapsedGameTime;
            if (elapsedTime > TimeSpan.FromSeconds(20))
            {
                elapsedTime -= TimeSpan.FromSeconds(20);
                healthPack.Enabled = true;
            }
        }

        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime); //Draw all Game Components

            if (paused)
            {
                spriteBatch.Draw(playTexture, pausePosition, pauseRect, Color.White); //Draw the paused prompt
            }

            if (gameOver)
            {
                spriteBatch.Draw(playTexture, gameoverPosition, gameoverRect, Color.White); //Draw the paused prompt
            }
        }
    }
}