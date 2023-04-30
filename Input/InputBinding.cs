﻿namespace Space_Game
{
    /// <summary>
    /// Represents a single input binding. Is either on or off.
    /// </summary>
    abstract class InputBinding
    {
        bool mPreviousState;
        bool mCurrentState;


        public InputBinding()
        {
            mPreviousState = false;
            mCurrentState = false;
        }


        /// <summary>
        /// Is the input pressed since last update?
        /// </summary>
        /// <returns>True if input was pressed since last update</returns>
        public bool IsInputPressed()
        {
            // Inverse of previous state is returned. If input is pressed in current update but not previous, function returns true.
            return mCurrentState && !mPreviousState;
        }


        /// <summary>
        /// Is the input released since last update?
        /// </summary>
        /// <returns>True if input was released since last input</returns>
        public bool IsInputReleased()
        {
            return !mCurrentState && mPreviousState;
        }


        /// <summary>
        /// Is the input currently down?
        /// </summary>
        /// <returns>True if input currently held</returns>
        abstract public bool IsInputDown();


        /// <summary>
        /// Update key state.
        /// </summary>
        /// <param name="gameTime">Frame time</param>
        public void Update(GameTime gameTime)
        {
            mPreviousState = mCurrentState;
            mCurrentState = IsInputDown();
        }
    }




    /// <summary>
    /// Keyboard button binding.
    /// </summary>
    class KeyBinding : InputBinding
    {
        Keys mInputKey;

        public KeyBinding(Keys inputKey)
        {
            mInputKey = inputKey;
        }

        /// <summary>
        /// Is the key currently down?
        /// </summary>
        /// <returns>True if key is held</returns>
        public override bool IsInputDown()
        {
            return Keyboard.GetState().IsKeyDown(mInputKey);
        }
    }
}
