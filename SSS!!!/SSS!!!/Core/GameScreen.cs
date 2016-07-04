using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace SSS___.Core
{
    /// <summary>
    /// This is a game component that implements IUpdateable.
    /// </summary>
    public class GameScreen : Microsoft.Xna.Framework.DrawableGameComponent
    {
        //List of child GameComponents
        private readonly List<GameComponent> components;

        //Components of Game Screen
        public List<GameComponent> Components //Used to expose the Components list, to be be able to add to new actors to the scene from the derived classes
        {
            get { return components; }
        }
        
        public GameScreen(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
            components = new List<GameComponent>();
            Visible = false; //Will not be visible initially
            Enabled = false; //Will not have its status updated initially
        }

        public virtual void Show() //Show the scene
        {
            Visible = true;
            Enabled = true;
        }

        public virtual void Hide() //Hide the scene
        {
            Visible = false;
            Enabled = false;
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
            for (int i = 0; i < components.Count; i++) //Loops through all of the child GameComponents
            {
                //Updated the component if it is enabled
                if (components[i].Enabled)
                {
                    components[i].Update(gameTime);
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            //Draw the child GameComponents (if drawable)
            for (int i = 0; i < components.Count; i++)
            {
                GameComponent gc = components[i];
                if ((gc is DrawableGameComponent) && ((DrawableGameComponent)gc).Visible)
                {
                    ((DrawableGameComponent)gc).Draw(gameTime);
                }
            }

            base.Draw(gameTime);
        }
    }
}