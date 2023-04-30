using System.Threading.Tasks.Dataflow;

namespace Space_Game
{
    /// <summary>
    /// Controls the viewport
    /// </summary>
    internal class Camera
    {
        #region rConstants

        private const float MIN_ZOOM = 0.25f;
        private const float MAX_ZOOM = 1080.0f;
        private const float ZOOM_AMOUNT = 0.05f;
        private const float FOLLOW_OFFSET = 0.02F;

        #endregion rConstants






        #region rMembers

        public Vector2 mCurrentPosition;
        public Vector2 mTargetPosition;
        public float mZoom;
        public float mRotation;

        // Height and width of viewport which needs to adjust when player resizes game window.
        public int mViewportWidth { get; set; }
        public int mViewportHeight { get; set; }

        public Vector2 mViewPortCentre
        {
            get
            {
                return new Vector2(mViewportWidth * 0.5f, mViewportHeight * 0.5f);
            }
        }


        // Create a matrix to offset everything being drawn.
        public Matrix mTranslationMatrix { get; private set; }

        private void CreateTranslationMatrix()
        {
            mTranslationMatrix = Matrix.CreateTranslation(
                                -(int)mCurrentPosition.X,
                                -(int)mCurrentPosition.Y,
                                0) *
                                Matrix.CreateRotationZ(mRotation) *
                                Matrix.CreateScale(new Vector3(mZoom, mZoom, 1)) *
                                Matrix.CreateTranslation(new Vector3(mViewPortCentre, 0));
        }

        #endregion rMembers






        #region rInitialisation

        public Camera(Vector2 targetPos)
        {
            mTargetPosition = targetPos;
            mCurrentPosition = mTargetPosition;
            mZoom = MIN_ZOOM;
        }

        #endregion rInitialisation


        #region rUpdate

        /// <summary>
        /// Update camera
        /// </summary>
        /// <param name="player">Entity controlled by player for camera to follow</param>
        public void Update(Player player)
        {
            mViewportWidth = Main.GetGraphicsDevice().Viewport.Width;
            mViewportHeight = Main.GetGraphicsDevice().Viewport.Height;

            Follow(player.GetPosition());
            CreateTranslationMatrix();

            if (IsZoomIn()) { AdjustZoom(ZOOM_AMOUNT); }
            else if (IsZoomOut()) { AdjustZoom(-ZOOM_AMOUNT); }
        }

        #endregion rUpdate






        #region rUtility

        /// <summary>
        /// Get camera to follow players position at an offset
        /// </summary>
        /// <param name="targetPos">Target position for camera to follow</param>
        public void Follow(Vector2 targetPos)
        {
            mCurrentPosition += (targetPos - mCurrentPosition) * FOLLOW_OFFSET;
        }


        /// <summary>
        /// Zoom in or out
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
