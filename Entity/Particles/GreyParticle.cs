﻿namespace Space_Game
{
    /// <summary>
    /// A grey particle.
    /// </summary>
    internal class GreyParticle : RepulseParticle
    {
        /// <summary>
        /// Construct grey particle at position.
        /// </summary>
        /// <param name="pos">Starting position</param>
        public GreyParticle(Vector2 pos, Entity target) : base(pos, target)
        {
            mColour = Color.Gray;
        }
    }
}