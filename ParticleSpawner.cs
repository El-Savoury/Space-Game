namespace Space_Game
{
    /// <summary>
    /// Manages particle spawning chunk system.
    /// </summary>
    internal class ParticleSpawner
    {
        #region rConstants

        const int LOADED_GRID_SIZE = 3; // Grid width and height in chunks
        const int CHUNK_SIZE = 2000; // Chunk size in pixels
        const int PARTICLES_PER_CHUNK = 500;

        #endregion rConstants





        #region rMembers

        Entity mTarget;
        Rectangle mLoadedChunks;
        Random mRand = new Random();
        List<Particle> mParticles = new List<Particle>();

        #endregion rMembers






        #region rInitialisation

        /// <summary>
        /// Constructor.
        /// </summary>
        public ParticleSpawner(Entity target)
        {
            mTarget = target;
        }


        /// <summary>
        /// Load particles types to be spawned.
        /// </summary>
        public void LoadContent()
        {
        }

        #endregion rInitialisation






        #region rUpdate
        /// <summary>
        /// Update chunk grid system and spawn/despawn particles.
        /// </summary>
        /// <param name="playerPos">Position of player in world space</param>
        public void Update(GameTime gameTime, Vector2 playerPos)
        {
            GenerateChunks(playerPos);

            foreach (Particle particle in mParticles)
            {
                particle.Update(gameTime);
            }
        }

        #endregion rUpdate






        #region rDraw

        /// <summary>
        /// Draw all particles.
        /// </summary>
        /// <param name="info">Info needed to draw</param>
        public void Draw(DrawInfo info)
        {
            foreach (Particle particle in mParticles)
            {
                if (particle != null)
                {
                    particle.Draw(info);
                }
            }
        }

        #endregion rDraw







        #region rUtility

        /// <summary>
        /// Create and destroy chunks around player.
        /// </summary>
        /// <param name="playerPos">Position of player in world space</param>s
        private void GenerateChunks(Vector2 playerPos)
        {
            Rectangle newChunks = GetChunks(playerPos);

            // Check each chunk to see if it falls outside of the currently loaded chunks. 
            for (int x = newChunks.X; x < newChunks.X + newChunks.Width; x++)
            {
                for (int y = newChunks.Y; y < newChunks.Y + newChunks.Height; y++)
                {
                    // If chunk isnt part of loaded chunks spawn particles in new chunk.
                    Point chunkOrigin = new Point(x, y);
                    if (!Utility.IsPointinRect(chunkOrigin, mLoadedChunks))
                    {
                        LoadChunk(chunkOrigin);
                    }
                }
            }

            // Check loaded chunks to see if they fall outside of adjacent grid and can be unloaded.
            for (int x = mLoadedChunks.X; x < mLoadedChunks.X + mLoadedChunks.Width; x++)
            {
                for (int y = mLoadedChunks.Y; y < mLoadedChunks.Y + mLoadedChunks.Height; y++)
                {
                    // If loaded chunk is not within new grid unload it.
                    Point chunkPos = new Point(x, y);
                    if (!Utility.IsPointinRect(chunkPos, newChunks))
                    {
                        UnloadChunk(chunkPos);
                    }
                }
            }

            mLoadedChunks = newChunks;
        }


        /// <summary>
        /// Gets the area containing adjacent chunks to the one the player is currently in.
        /// </summary>
        /// <param name="playerPos">Position of player in world space</param>
        /// <returns>Rectangle with player chunk in middle and its adjacent chunks</returns>
        private Rectangle GetChunks(Vector2 playerPos)
        {
            // Convert camera position from world space to coordinate within chunk.
            double chunkX = playerPos.X / CHUNK_SIZE;
            double chunkY = playerPos.Y / CHUNK_SIZE;

            // Calculate rectangle origin. This is the top left corner of inhabited chunk.
            Point rectOrigin = new Point((int)Math.Floor(chunkX), (int)Math.Floor(chunkY)); // Round down to nearest int.

            // Offset the rectangle to position inhabited chunk as the central chunk.
            rectOrigin.X -= LOADED_GRID_SIZE / 2;
            rectOrigin.Y -= LOADED_GRID_SIZE / 2;

            return new Rectangle(rectOrigin.X, rectOrigin.Y, LOADED_GRID_SIZE, LOADED_GRID_SIZE);
        }


        /// <summary>
        /// Load all assets within chunk.
        /// </summary>
        private void LoadChunk(Point chunkPos)
        {
            SpawnParticles(chunkPos);
        }


        /// <summary>
        /// Unload all assets within chunk.
        /// </summary>
        private void UnloadChunk(Point chunkPos)
        {
            DespawnParticles(chunkPos);
        }


        /// <summary>
        /// Despawn all particles in chunk.
        /// </summary>
        /// <param name="chunkPos">Top left corner of chunk</param>
        private void DespawnParticles(Point chunkPos)
        {
            Rectangle chunk = new Rectangle(chunkPos.X * CHUNK_SIZE, chunkPos.Y * CHUNK_SIZE, CHUNK_SIZE, CHUNK_SIZE);

            for (int i = 0; i < mParticles.Count; i++)
            {
                if (mParticles[i] != null)
                {
                    if (mParticles[i].GetPosition().X >= chunk.X &&
                        mParticles[i].GetPosition().X <= chunk.X + chunk.Width &&
                        mParticles[i].GetPosition().Y >= chunk.Y &&
                        mParticles[i].GetPosition().Y <= chunk.Y + chunk.Height)
                    {
                        mParticles[i] = null;
                    }
                }
            }

            mParticles.RemoveAll(particle => particle == null);
        }


        /// <summary>
        /// Spawn particles with random x and y coordinates.
        /// </summary>
        private void SpawnParticles(Point chunkPos)
        {
            Vector2 chunkOrigin = new Vector2(chunkPos.X * CHUNK_SIZE, chunkPos.Y * CHUNK_SIZE);

            for (int i = 0; i < PARTICLES_PER_CHUNK; i++)
            {
                Vector2 particlePos = new Vector2(mRand.Next(0, CHUNK_SIZE), mRand.Next(0, CHUNK_SIZE));
                particlePos += chunkOrigin;

                if (particlePos.X % 2 == 0)
                {
                    mParticles.Add(new GreyParticle(particlePos, mTarget));
                }
                else
                {
                    mParticles.Add(new BlueParticle(particlePos));
                }
            }
        }

        #endregion rUtility
    }
}

