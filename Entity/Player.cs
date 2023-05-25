namespace Space_Game
{
    /// <summary>
    /// Playable entity. 
    /// </summary>
    class Player : Entity
    {
        #region rConstants

        const float SPEED = 1.0f;
        const int WIDTH = 8;
        const int HEIGHT = 8;

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
            mMass = 5.0f;
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
        public void Update(GameTime gameTime, List<Particle> particles)
        {
            mPosition += CalcMovement();
            Repel(particles);
            Attract(particles);
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

        /// <summary>
        /// Repel nearby particles.
        /// </summary>
        /// <param name="particles">Particle to repel</param>
        public void Repel(List<Particle> particles)
        {
            foreach (Particle particle in particles)
            {
                if (Utility.GetDist(mPosition, particle.GetPos()) < 50 &&
                    !InputManager.KeyHeld(Controls.Confirm))
                {
                    Vector2 force = Utility.CalcGravity(this, particle);
                    particle.IncrementVelociy(-force);
                }
            }
        }


        /// <summary>
        /// Attract nearby particles.
        /// </summary>
        /// <param name="particles">Particle to repel</param>
        public void Attract(List<Particle> particles)
        {
            foreach (Particle particle in particles)
            {
                if (Utility.GetDist(mPosition, particle.GetPos()) < 300 &&
                    InputManager.KeyHeld(Controls.Confirm))
                {
                    Vector2 force = Utility.CalcGravity(this, particle);
                    particle.IncrementVelociy(force);
                }
            }
        }

        #endregion mUtility


    }
}
