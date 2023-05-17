namespace Space_Game
{
    /// <summary>
    /// A blue particle.
    /// </summary>
    internal class BlueParticle : Particle
    {
        /// <summary>
        /// Construct grey particle at position.
        /// </summary>
        /// <param name="pos">Starting position</param>
        public BlueParticle(Vector2 pos) : base(pos)
        {
            mColour = Color.Cyan;
        }

    }
}
