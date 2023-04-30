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
            mPlayer = new Player(new Vector2(100, 100));
            mCamera = new Camera(mPlayer.GetPosition());
            mCamera.mViewportWidth = mGraphics.GraphicsDevice.Viewport.Height;
            mCamera.mViewportHeight = mGraphics.GraphicsDevice.Viewport.Height;
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
            mCamera.Update(mPlayer);
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

            mPlayer.DrawSquare(info.spriteBatch);

            info.spriteBatch.End();

            return mScreenTarget;
        }

        #endregion rDraw







        #region rUtitlity

        #endregion rUtility
    }
}
