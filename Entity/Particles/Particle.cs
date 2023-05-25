using System.Collections.Generic;

namespace Space_Game
{
    /// <summary>
    /// Represents a particle than can interact with player.
    /// </summary>
    abstract class Particle : Entity
    {
        #region rConstants

        //const int WIDTH = 2;
        //const int HEIGHT = 2;

        #endregion rConstants





        #region rMembers

        protected int mWidth;
        protected int mHeight;
        protected Vector2 mVelocity;
        protected Color mColour;
        private Random mRandom = new Random();

        #endregion rMembers





        #region rInitilaisation

        /// <summary>
        /// Construct particle at position.
        /// </summary>
        /// <param name="pos">Starting position</param>
        public Particle(Vector2 pos) : base(pos)
        {
            mMass = 1;
            mWidth = 2;
            mHeight = mRandom.Next(2, 4);
            mVelocity = new Vector2((float)mRandom.NextDouble(), (float)mRandom.NextDouble());
            mVelocity.X = Math.Clamp(mVelocity.X, 0.1f, 0.2f);
            mVelocity.Y = Math.Clamp(mVelocity.Y, 0.1f, 0.2f);
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
        public virtual void Update(GameTime gameTime, List<Particle> particles)
        {
            mPosition += mVelocity;
        }

        #endregion rUpdate






        #region rDraw

        /// <summary>
        /// Draw particle.
        /// </summary>
        /// <param name="info">Info needed to draw</param>
        public override void Draw(DrawInfo info)
        {
            info.spriteBatch.Draw(Main.GetDummyTexture(), new Rectangle((int)mPosition.X, (int)mPosition.Y, mWidth, mHeight), mColour);
        }

        #endregion rDraw






        #region rUtility

        public void IncrementVelociy(Vector2 amount)
        {
            mVelocity += amount;
        }

        #endregion rUtility

    }
}
