using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SSS___.Core
{
    /// <summary>
    /// Gamepad rumble
    /// </summary>
    public class RumblePad : Microsoft.Xna.Framework.GameComponent
    {

        private int time; //Vibration time
        private int lastTickCount;

        public RumblePad(Game game)
            : base(game)
        {
            // TODO: Construct any child components here
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
            if (time > 0)
            {
                int elapsed = System.Environment.TickCount - lastTickCount;
                if (elapsed >= time)
                {
                    time = 0;
                    GamePad.SetVibration(PlayerIndex.One, 0, 0);
                    GamePad.SetVibration(PlayerIndex.Two, 0, 0);
                }
            }

            base.Update(gameTime);
        }

        //Turn the vibration off
        protected override void Dispose(bool disposing)
        {
            GamePad.SetVibration(PlayerIndex.One, 0, 0);

            base.Dispose(disposing);
        }

        //Set the vibration
        public void GamepadRumble(PlayerIndex playerIndex, int Time, float LeftMotor, float RightMotor)
        {
            lastTickCount = System.Environment.TickCount;
            time = Time;
            GamePad.SetVibration(playerIndex, LeftMotor, RightMotor);
        }

        public void Stop(PlayerIndex playerIndex) //Stop the vibration
        {
            GamePad.SetVibration(playerIndex, 0, 0);
        }
    }
}