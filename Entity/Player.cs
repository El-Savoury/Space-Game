﻿namespace Space_Game
{
    /// <summary>
    /// Playable entity. 
    /// </summary>
    class Player : Entity
    {
        #region rConstants

        const float SPEED = 3.0f;
        const int WIDTH = 16;
        const int HEIGHT = 16;

        #endregion rConstants






        #region rMembers

        // Add members here.

        #endregion rMembers






        #region rInitialisation

        /// <summary>
        /// Construct player at position.
        /// </summary>
        /// <param name="pos">Starting position</param>
        public Player(Vector2 pos) : base(pos)
        {
        }


        /// <summary>
        /// Load player textures and assets.
        /// </summary>
        /// <param name="content">Monogame content manager</param>
        public override void LoadContent(ContentManager content)
        {
        }

        #endregion rInitialisation






        #region rUpdate


        /// <summary>
        /// Update player.
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public override void Update(GameTime gameTime)
        {
            mPosition += CalcMovement();
        }


        /// <summary>
        /// Calculate player movement based on directional input 
        /// </summary>
        /// <returns>Vector representing distance and direction to move</returns>
        private Vector2 CalcMovement()
        {
            Vector2 inputDir = Vector2.Zero;

            if (InputManager.KeyHeld(Controls.Left))
            {
                inputDir.X -= 1;
            }

            if (InputManager.KeyHeld(Controls.Right))
            {
                inputDir.X += 1;
            }
            if (InputManager.KeyHeld(Controls.Up))
            {
                inputDir.Y -= 1;
            }

            if (InputManager.KeyHeld(Controls.Down))
            {
                inputDir.Y += 1;
            }

            if (inputDir != Vector2.Zero)
            {
                inputDir.Normalize();
            }
            return inputDir * SPEED;
        }

        #endregion rUpdate







        #region rDraw

        /// <summary>
        /// Draw player
        /// </summary>
        /// <param name="info">Info needed to draw</param>
        public override void Draw(DrawInfo info)
        {
            info.spriteBatch.Draw(Main.GetDummyTexture(),
                                  new Rectangle((int)mPosition.X,
                                                (int)mPosition.Y,
                                                WIDTH, HEIGHT),
                                  Color.Red);
        }

        #endregion rDraw






        #region mUtility

        #endregion mUtility


    }
}
