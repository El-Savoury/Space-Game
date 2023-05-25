using System.Collections.Generic;

namespace Space_Game.Screens
{
    /// <summary>
    /// Gameplay screen
    /// </summary>
    internal class GameScreen : Screen
    {
        #region rMembers

        Player mPlayer;
        Camera mCamera;
        List<Particle> mParticles;
        ParticleSpawner mParticleSpawner;

        #endregion rMembers






        #region rInitialisation

        /// <summary>
        /// Game screen constructor
        /// </summary>
        /// <param name="graphics">Graphics device</param>
        public GameScreen(GraphicsDeviceManager graphics) : base(graphics)
        {
        }

        /// <summary>
        /// Load content required for gameplay
        /// </summary>
        public override void LoadContent()
        {
            mPlayer = new Player(new Vector2(0, 0));
            mParticles = new List<Particle>();
            mParticleSpawner = new ParticleSpawner(mParticles, mPlayer);

            // Init camera to current viewport size and centre on player.
            mCamera = new Camera();
            mCamera.mWidth = mGraphics.GraphicsDevice.Viewport.Height;
            mCamera.mHeight = mGraphics.GraphicsDevice.Viewport.Height;
            mCamera.TargetEntity(mPlayer);
            mCamera.CentreOn(mPlayer.GetPos());
        }

        #endregion rInitialisation






        #region rUpdate

        /// <summary>
        /// Update game screen
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public override void Update(GameTime gameTime)
        {
            //if (InputManager.KeyPressed(Controls.Confirm))
            //{
            //    ScreenManager.ActivateScreen(ScreenType.Title);
            //}

            mPlayer.Update(gameTime, mParticles);
            mCamera.Update();
            mParticleSpawner.Update(gameTime, mPlayer.GetPos());
        }

        #endregion rUpdate






        #region rDraw

        /// <summary>
        /// Draw game screen to render target
        /// </summary>
        /// <param name="info">Info needed to draw</param>
        /// <returns>Render target with game screen drawn on it</returns>
        public override RenderTarget2D DrawToRenderTarget(DrawInfo info)
        {
            info.device.SetRenderTarget(mScreenTarget);
            info.device.Clear(Color.Black);

            info.spriteBatch.Begin(SpriteSortMode.FrontToBack,
                                   BlendState.AlphaBlend,
                                   null, null, null, null,
                                   mCamera.mTranslationMatrix);

            //// Draw a background shape as reference point for movement
            //info.spriteBatch.Draw(Main.GetDummyTexture(),
            //                    new Rectangle(0, 0, 400, 300),
            //                    Color.DarkBlue);

            mParticleSpawner.Draw(info);
            mPlayer.Draw(info);

            info.spriteBatch.End();

            return mScreenTarget;
        }

        #endregion rDraw







        #region rUtility

        #endregion rUtility
    }
}
