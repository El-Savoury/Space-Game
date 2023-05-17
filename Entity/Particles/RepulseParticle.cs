namespace Space_Game
{
    /// <summary>
    /// Particle that moves away from player.
    /// </summary>
    abstract class RepulseParticle : Particle
    {
        #region rMembers

        Entity mTarget;

        #endregion rMembers





        #region rInitialisation

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="pos">Starting position</param>
        public RepulseParticle(Vector2 pos, Entity target) : base(pos)
        {
            mTarget = target;
        }

        #endregion rInitialisation




        #region rUpdate

        /// <summary>
        /// Update.
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public override void Update(GameTime gameTime)
        {
            if (Utility.GetDistance(mPosition, mTarget.GetPosition()) < 10.0f)
            {
                mPosition.X = 1.0f;
            }
        }

        #endregion rUpdate
    }
}
