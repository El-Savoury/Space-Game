namespace Space_Game
{
    /// <summary>
    /// Controls the viewport.
    /// </summary>
    internal class Camera
    {
        #region rConstants

        private const float MIN_ZOOM = 1.00f;
        private const float MAX_ZOOM = 1080.0f;
        private const float ZOOM_AMOUNT = 0.25f;
        private const float FOLLOW_OFFSET = 0.02F;

        #endregion rConstants






        #region rMembers

        private Vector2 mPosition;
        private float mZoom;
        private float mRotation;
        private Entity mTargetEntity;

        // Height and width of viewport which needs to adjust when player resizes game window.
        public int mCameraWidth { get; set; }
        public int mCameraHeight { get; set; }

        // Create a matrix to offset everything being drawn.
        public Matrix mTranslationMatrix { get; private set; }

        private void CreateTranslationMatrix()
        {
            mTranslationMatrix = Matrix.CreateTranslation(
                                -(int)mPosition.X,
                                -(int)mPosition.Y,
                                0) *
                                Matrix.CreateRotationZ(mRotation) *
                                Matrix.CreateScale(new Vector3(mZoom, mZoom, 1)) *
                                Matrix.CreateTranslation(new Vector3(GetCentre(), 0));
        }

        #endregion rMembers






        #region rInitialisation

        public Camera()
        {
            mZoom = MIN_ZOOM;
        }

        #endregion rInitialisation






        #region rUpdate

        /// <summary>
        /// Update camera.
        /// </summary>
        public void Update()
        {
            mCameraWidth = Main.GetGraphicsDevice().Viewport.Width;
            mCameraHeight = Main.GetGraphicsDevice().Viewport.Height;

            if (mTargetEntity != null)
            {
                FollowTarget();
            }

            CreateTranslationMatrix();

            if (IsZoomIn()) { AdjustZoom(ZOOM_AMOUNT); }
            else if (IsZoomOut()) { AdjustZoom(-ZOOM_AMOUNT); }
        }

        #endregion rUpdate







        #region rUtility

        /// <summary>
        /// Get centre position of camera.
        /// </summary>
        public Vector2 GetCentre()
        {
            return new Vector2(mCameraWidth * 0.5f, mCameraHeight * 0.5f);
        }


        /// <summary>
        /// Centre the camera on specific position.
        /// </summary>
        /// <param name="pos">Position to center camera on</param>
        public void CentreOn(Vector2 pos)
        {
            mPosition = pos;
        }

        /// <summary>
        /// Set entity for camera to follow.
        /// </summary>
        public void TargetEntity(Entity entity)
        {
            mTargetEntity = entity;
        }

        /// <summary>
        /// Get camera to follow players position at an offset.
        /// </summary>
        /// <param name="targetPos">Target position for camera to follow</param>
        private void FollowTarget()
        {
            mPosition += (mTargetEntity.GetPosition() - mPosition) * FOLLOW_OFFSET;
        }


        /// <summary>
        /// Zoom in or out.
        /// </summary>
        /// <param name="zoomAmount">Amount to zoom in or out</param>
        private void AdjustZoom(float zoomAmount)
        {
            mZoom += zoomAmount;
            mZoom = Math.Clamp(mZoom, MIN_ZOOM, MAX_ZOOM);
        }


        /// <summary>
        /// Is zoom in key held?
        /// </summary>
        private bool IsZoomIn()
        {
            return InputManager.KeyHeld(Controls.ZoomIn);
        }


        /// <summary>
        /// Is zoom out key held?
        /// </summary>
        private bool IsZoomOut()
        {
            return InputManager.KeyHeld(Controls.ZoomOut);
        }

        #endregion rUtility

    }
}
