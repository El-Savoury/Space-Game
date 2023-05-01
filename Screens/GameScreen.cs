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
        Random mRand = new Random();
        List<Particle> mParticles = new List<Particle>();

        #endregion rMembers






        #region rInitialisation

        /// <summary>
        /// Game screen constructor
        /// </summary>
        /// <param name="graphics">Graphics device</param>
        public GameScreen(GraphicsDeviceManager graphics) : base(graphics)
        {
            SpawnParticles();
        }

        /// <summary>
        /// Load content required for gameplay
        /// </summary>
        public override void LoadContent()
        {
            mPlayer = new Player(new Vector2(0, 0));
            mCamera = new Camera();
            mCamera.mViewportWidth = mGraphics.GraphicsDevice.Viewport.Height;
            mCamera.mViewportHeight = mGraphics.GraphicsDevice.Viewport.Height;
            mCamera.TargetEntity(mPlayer);
            mCamera.CentreOn(mPlayer.GetPosition());
        }

        #endregion rInitialisation






        #region rUpdate

        /// <summary>
        /// Update game screen
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public override void Update(GameTime gameTime)
        {
            if (InputManager.KeyPressed(Controls.Confirm))
            {
                ScreenManager.ActivateScreen(ScreenType.Title);
            }
            
            mPlayer.Update(gameTime);
            mCamera.Update();
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


            // Draw a background shape as reference point for movement
            info.spriteBatch.Draw(Main.GetDummyTexture(),
                                new Rectangle(0, 0, 400, 300),
                                Color.DarkBlue);

            foreach (Particle particle in mParticles)
            {
                particle.Draw(info);
            }


            mPlayer.Draw(info);

            info.spriteBatch.End();

            return mScreenTarget;
        }

        #endregion rDraw







        #region rUtility

        private void SpawnParticles()
        {
            for (int x = 0; x < mGraphics.GraphicsDevice.Viewport.Width; x++)
            {
                for (int y = 0; y < mGraphics.GraphicsDevice.Viewport.Height; y++)
                {
                    bool isParticle = mRand.Next(0, 256) < 2;

                    if (isParticle)
                    {
                        mParticles.Add(new Particle(new Vector2(x, y)));
                    }
                }
            }
        }

        #endregion rUtility
    }
}
