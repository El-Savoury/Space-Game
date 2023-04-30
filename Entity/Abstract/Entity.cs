﻿namespace Space_Game
{
    /// <summary>
    /// Represents a moving entity in the game world.
    /// </summary>
    abstract class Entity
    {
        #region rMembers

        protected Vector2 mPosition;
        protected Vector2 mCentreOfMass;
        protected Texture2D mTexture;
        private bool mEnabled;

        #endregion rMembers





        #region mInitialisation

        /// <summary>
        /// Entity constructor.
        /// </summary>
        /// <param name="pos">Starting position</param>
        public Entity(Vector2 pos)
        {
            mPosition = pos;
            mCentreOfMass = pos;
            mEnabled = true;
        }


        /// <summary>
        /// Load content for entity such as textures.
        /// </summary>
        /// <param name="content">Monogame content manager</param>
        public abstract void LoadContent(ContentManager content);

        #endregion mInitialsation





        #region rUpdate

        /// <summary>
        /// Update entity.
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public virtual void Update(GameTime gameTime)
        {

        }


        /// <summary>
        /// React to a collision witht this entity.
        /// </summary>
        /// <param name="entity"></param>
        public virtual void CollideWithEntity(Entity entity)
        {
            // Default: Do nothing.
        }

        #endregion rUpdate





        #region rDraw

        /// <summary>
        /// Draw entity.
        /// </summary>
        public abstract void Draw(DrawInfo info);

        #endregion rDraw





        #region rUtility

        /// <summary>
        /// Get position of entity.
        /// </summary>
        public Vector2 GetPosition()
        {
            return mPosition;
        }


        /// <summary>
        /// Set position of entity.
        /// </summary>
        public void SetPosition(Vector2 pos)
        {
            mPosition = pos;
        }


        /// <summary>
        /// Enable/Disable this entity. Disabled entities will not be drawn or updated.
        /// </summary>
        public virtual void SetEnabled(bool enabled)
        {
            mEnabled = enabled;
        }


        /// <summary>
        /// Is this entity enabled?
        /// </summary>
        /// <returns></returns>
        public bool IsEnabled()
        {
            return mEnabled;
        }

        #endregion rUtility
    }
}
