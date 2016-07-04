using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SSS___
{
    /// <summary>
    /// This is a game component that implements score and health points
    /// </summary>
    public class Score : Microsoft.Xna.Framework.DrawableGameComponent
    {

        protected SpriteBatch spriteBatch = null; //SpriteBatch
        protected Vector2 position = new Vector2(); //Score position
        
        //Values
        protected int score; //Score points
        protected int health; //Health points

        protected readonly SpriteFont font;
        protected readonly Color fontColour;


        public Score(Game game, SpriteFont font, Color fontColour)
            : base(game)
        {
            // TODO: Construct any child components here
            this.font = font;
            this.fontColour = fontColour;
            spriteBatch = (SpriteBatch)Game.Services.GetService(typeof(SpriteBatch)); //Get current sprite batch
        }

        public int ScorePoints //Score points
        {
            get { return score; }
            set { this.score = value; }
        }

        public int HealthPoints //Health points
        {
            get { return health; }
            set { health = value; }
        }

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
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

        public override void Draw(GameTime gameTime)
        {
            string TextToDraw = string.Format("Score: {0}", score); //Set the text for score

            //Draw a shadow for the score text
            spriteBatch.DrawString(font, TextToDraw, new Vector2(position.X + 1, position.Y + 1), Color.Black);
            //Draw the score text
            spriteBatch.DrawString(font, TextToDraw, new Vector2(position.X, position.Y), fontColour);

            float height = font.MeasureString(TextToDraw).Y; //Set a gap so that score and health are drawn on different lines
            TextToDraw = string.Format("Health: {0}", health); //Set the text for health
            //Draw a shadow for the health text
            spriteBatch.DrawString(font, TextToDraw, new Vector2(position.X + 1, position.Y + 1 + height), Color.Black);
            //Draw the health text
            spriteBatch.DrawString(font, TextToDraw, new Vector2(position.X + 1, position.Y + 1 + height), fontColour);

            base.Draw(gameTime);
        }
    }
}