using Microsoft.Xna.Framework.Graphics;

namespace Space_Game
{
    /// <summary>
    /// Playable entity 
    /// </summary>
    class Player : Entity
    {
        #region rConstants

        const float SPEED = 3.0f;

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
        /// Load textures and assets.
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
            Move();
        }



        public void Move()
        {
            if (InputManager.KeyHeld(Controls.Left))
            {
                mPosition.X -= SPEED;
            }

            if (InputManager.KeyHeld(Controls.Right))
            {
                mPosition.X += SPEED;
            }
            if (InputManager.KeyHeld(Controls.Up))
            {
                mPosition.Y -= SPEED;
            }

            if (InputManager.KeyHeld(Controls.Down))
            {
                mPosition.Y += SPEED;
            }
        }
        #endregion rUpdate






        #region rDraw

        /// <summary>
        /// Draw player
        /// </summary>
        /// <param name="info"></param>
        public override void Draw(DrawInfo info)
        {
        }


        public void DrawSquare(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Main.GetDummyTexture(), new Rectangle((int)mPosition.X, (int)mPosition.Y, 32, 32), Color.Red);
        }

        #endregion rDraw





        #region mUtility

     
        #endregion mUtility


    }
}
