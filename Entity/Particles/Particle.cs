namespace Space_Game
{
    /// <summary>
    /// Represents a particle than can interact with player.
    /// </summary>
    abstract class Particle : Entity
    {
        #region rConstants

        const int WIDTH = 2;
        const int HEIGHT = 2;

        #endregion rConstants





        #region rMembers

        protected Vector2 mVelocity;
        protected Color mColour;


        #endregion rMembers





        #region rInitilaisation

        /// <summary>
        /// Contruct particle at position.
        /// </summary>
        /// <param name="pos">Starting position</param>
        public Particle(Vector2 pos) : base(pos)
        {
        }


        /// <summary>
        /// Load particle textures and assets.
        /// </summary>
        /// <param name="content">Monogame content manager</param>
        public override void LoadContent(ContentManager content)
        {
        }

        #endregion rInitialisation






        #region rUpdate

        /// <summary>
        /// Update particle.
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public override void Update(GameTime gameTime)
        {

        }

        #endregion rUpdate






        #region rDraw

        /// <summary>
        /// Draw particle.
        /// </summary>
        /// <param name="info">Info needed to draw</param>
        public override void Draw(DrawInfo info)
        {
            info.spriteBatch.Draw(Main.GetDummyTexture(), new Rectangle((int)mPosition.X, (int)mPosition.Y, WIDTH, HEIGHT), mColour);
        }

        #endregion rDraw






        #region rUtility

        #endregion rUtility

    }
}
