using System.Reflection.Metadata.Ecma335;

namespace Space_Game
{
    internal class ParticleSpawner
    {
        #region rConstants

        const int LOADED_GRID_SIZE = 3; // Grid width and height in chunks
        const int CHUNK_SIZE = 100; // Chunk size in pixels
        const int PARTICLES_PER_CHUNK = 25;

        #endregion rConstants





        #region rMembers

        Rectangle mLoadedChunks;
        Random mRand = new Random();
        List<Particle> mParticles = new List<Particle>();

        #endregion rMembers






        #region rInitialisation

        /// <summary>
        /// Constructor.
        /// </summary>
        public ParticleSpawner()
        {
        }

        #endregion rInitialisation






        #region rUpdate
        /// <summary>
        /// Update chunk grid system and spawn/despawn particles.
        /// </summary>
        /// <param name="cameraPos">Position of camera in game world</param>
        public void Update(Vector2 cameraPos)
        {
            Rectangle newChunks = GetChunks(cameraPos);

            // Check each coordinate of adjacent grid to see if falls outside of the currently loaded chunks. 
            for (int x = newChunks.X; x < newChunks.X + newChunks.Width; x++)
            {
                for (int y = newChunks.Y; y < newChunks.Y + newChunks.Height; y++)
                {
                    // If chunk isnt part of loaded chunks spawn particles in new chunk.
                    Point chunkPos = new Point(x, y);
                    if (!Utility.IsPointinRect(chunkPos, mLoadedChunks))
                    {
                        LoadChunk(chunkPos);
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
                particle.Draw(info);
            }
        }

        #endregion rDraw







        #region rUtility


        /// <summary>
        /// Gets the area containing adjacent chunks to the one the player is currently in.
        /// </summary>
        /// <param name="cameraCentre">Camera following player</param>
        /// <returns>Rectangle with player chunk in middle and its adjacent chunks</returns>
        private Rectangle GetChunks(Vector2 cameraCentre)
        {
            // Convert camera position from world space to coordinate within chunk.
            Vector2 chunkCoord = cameraCentre / CHUNK_SIZE;

            // Calculate rectangle origin. This is the top left corner of inhabited chunk.
            Point rectOrigin = new Point((int)chunkCoord.X, (int)chunkCoord.Y); // Round down to nearest int.

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
        }



        /// <summary>
        /// Spawn new particles in rectangle.
        /// </summary>
        /// <param name="rect"> Empty rectangle created when spawnRect exceeds prevSpawnRect</param>
        private void SpawnInRect(Rectangle rect)
        {
            //SpawnParticles(rect);
        }


        /// <summary>
        /// Despawn all particles in rectangle.
        /// </summary>
        /// <param name="rect">Rectangle containing existing particles created when prevSpawnRect exceeds spawnRect</param>        
        private void DespawnInRect(Rectangle rect)
        {
            for (int i = 0; i < mParticles.Count; i++)
            {
                if (mParticles[i].GetPosition().X >= rect.X &&
                    mParticles[i].GetPosition().X <= rect.X + rect.Width &&
                    mParticles[i].GetPosition().Y >= rect.Y &&
                    mParticles[i].GetPosition().Y <= rect.Y + rect.Height)
                {
                    mParticles[i] = null;
                    mParticles.Remove(mParticles[i]);
                }
            }
        }


        /// <summary>
        /// Create chance for particle to spawn for each pixel within defined area.
        /// </summary>
        private void SpawnParticles(Point chunkPos)
        {
            Vector2 chunkOrigin = new Vector2(chunkPos.X * CHUNK_SIZE, chunkPos.Y * CHUNK_SIZE);

            for (int i = 0; i < PARTICLES_PER_CHUNK; i++)
            {
                Vector2 particlePos = new Vector2(mRand.Next(0, CHUNK_SIZE), mRand.Next(0, CHUNK_SIZE));
                particlePos += chunkOrigin;
                mParticles.Add(new Particle(particlePos));
            }
        }

        #endregion rUtility
    }
}

