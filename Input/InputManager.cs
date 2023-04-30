namespace Space_Game
{
    enum Controls
    {
        // Menu
        Confirm,

        // Camera
        ZoomIn,
        ZoomOut,

        // Ship
        Left,
        Right,
        Up,
        Down
    }

    /// <summary>
    /// Manages user inputs
    /// </summary>
    static class InputManager
    {
        #region rMembers

        static Dictionary<Controls, InputBindSet> mInputBindings = new Dictionary<Controls, InputBindSet>();

        #endregion rMembers





        #region rInitialisation

        /// <summary>
        /// Set default input bindings
        /// </summary>
        public static void SetDefaultBindings()
        {
            // Menu
            mInputBindings.Add(Controls.Confirm, new InputBindSet(new KeyBinding(Keys.Space)));

            // Camera
            mInputBindings.Add(Controls.ZoomIn, new InputBindSet(new KeyBinding(Keys.W)));
            mInputBindings.Add(Controls.ZoomOut, new InputBindSet(new KeyBinding(Keys.S)));

            // Ship
            mInputBindings.Add(Controls.Left, new InputBindSet(new KeyBinding(Keys.Left)));
            mInputBindings.Add(Controls.Right, new InputBindSet(new KeyBinding(Keys.Right)));
            mInputBindings.Add(Controls.Up, new InputBindSet(new KeyBinding(Keys.Up)));
            mInputBindings.Add(Controls.Down, new InputBindSet(new KeyBinding(Keys.Down)));
        }

        #endregion rInititialisation






        #region rKeySense

        /// <summary>
        /// Update input
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public static void Update(GameTime gameTime)
        {
            foreach (KeyValuePair<Controls, InputBindSet> keyBindPair in mInputBindings)
            {
                keyBindPair.Value.Update(gameTime);
            }
        }


        /// <summary>
        /// Was a key pressed recently?
        /// Checks input binding dictionary for specified control input and returns if it's paired key is pressed
        /// </summary>
        /// <param name="key">Key to check keybind dictionary for</param>
        /// <returns>True if key pressed in last update</returns>
        public static bool KeyPressed(Controls key)
        {
            return mInputBindings[key].AnyKeyPressed();
        }


        /// <summary>
        /// Was a key held recently?
        /// </summary>
        /// <param name="key">Key to check keybind dictionary for</param>
        /// <returns>True if key was down in most recent update and previous update</returns>
        public static bool KeyHeld(Controls key)
        {
            return mInputBindings[key].AnyKeyHeld();
        }

        #endregion rKeySense
    }
}
