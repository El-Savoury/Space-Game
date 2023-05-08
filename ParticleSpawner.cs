namespace Space_Game
{
    internal class ParticleSpawner
    {
        #region rConstants

        const int LOADED_GRID_SIZE = 300;
        const int CHUNK_SIZE = 100;

        #endregion rConstants





        #region rMembers

        Rectangle mLoadedChunks = new Rectangle(0, 0, LOADED_GRID_SIZE, LOADED_GRID_SIZE);
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
                    Point chunkLocation = new Point(x, y);
                    if (!Utility.IsPointinRect(chunkLocation, mLoadedChunks))
                    {
                        LoadChunk(chunkLocation);
                    }
                }
            }

            // Check loaded chunks to see if they fall outside of adjacent grid and can be unloaded.
            for (int x = mLoadedChunks.X; x < mLoadedChunks.X + mLoadedChunks.Width; x++)
            {
                for (int y = mLoadedChunks.Y; y < mLoadedChunks.Y + mLoadedChunks.Height; y++)
                {
                    // If loaded chunk is not within new grid unload it.
                    Point chunkLocation = new Point(x, y);  
                    if(!Utility.IsPointinRect(chunkLocation, newChunks))
                    {
                        UnloadChunk();
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
        /// <param name="camera">Camera following player</param>
        /// <returns>Rectangle with player chunk in middle and its adjacent chunks</returns>
        private Rectangle GetChunks(Vector2 cameraPos)
        {
            // Convert camera position from world space to coordinate within chunk.
            Vector2 chunkCoord = cameraPos / CHUNK_SIZE;

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
        private void LoadChunk(Point chunkLocation)
        {
            //SpawnParticles(new Rectangle(chunkLocation.X, chunkLocation.Y, CHUNK_SIZE, CHUNK_SIZE));
        }


        /// <summary>
        /// Unload all assets within chunk.
        /// </summary>
        private void UnloadChunk()
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
        private void SpawnParticles(Rectangle spawnArea)
        {
            for (int x = 0; x < spawnArea.Width; x++)
            {
                for (int y = 0; y < spawnArea.Height; y++)
                {
                    bool isParticle = mRand.Next(0, 256) < 2;

                    if (isParticle)
                    {
                        mParticles.Add(new Particle(new Vector2(x, y)));
                    }
                }
            }
        }

        #endregion rUtility
    }
}

