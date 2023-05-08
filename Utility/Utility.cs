namespace Space_Game
{
    /// <summary>
    ///  Game specific utility functions.
    /// </summary>
    static class Utility
    {
        /// <summary>
        /// Checks if point is within bounds of rectangle.
        /// </summary>
        /// <returns>True if point is within rectangle</returns>
        public static bool IsPointinRect(Point point, Rectangle rect)
        {
            return point.X > rect.X && point.X < rect.X + rect.Width &&
                   point.Y > rect.Y && point.Y < rect.Y + rect.Height;
        }


        /// <summary>
        /// Get the difference between two intersecting rectangles.
        /// </summary>
        /// <param name="a">First rectangle</param>
        /// <param name="b">Second rectangle</param>
        /// <returns>List of rectangles representing the horizontal and vertical
        /// regions of each rectangle which are outside the area that's overlapping</returns>
        public static List<Rectangle> GetRectanglesDifference(Rectangle a, Rectangle b)
        {
            List<Rectangle> result = new List<Rectangle>();

            // Check if the rectangles intersect.
            if (a.X + a.Width < b.X ||
                b.X + b.Width < a.X ||
                a.Y + a.Height < b.Y ||
                b.Y + b.Height < a.Y)
            {
                // No intersection, so the difference is the two rectangles themselves.
                result.Add(a);
                result.Add(b);
            }
            else
            {
                // Calculate the difference.
                if (a.X < b.X)
                {
                    // Left rectangle.
                    result.Add(new Rectangle(x: a.X, y: a.Y, width: b.X - a.X, height: a.Height));
                }
                if (a.X + a.Width > b.X + b.Width)
                {
                    // Right rectangle.
                    result.Add(new Rectangle(x: b.X + b.Width, y: a.Y, width: a.X + a.Width - (b.X + b.Width), height: a.Height));
                }
                if (a.Y < b.Y)
                {
                    // Top rectangle.
                    result.Add(new Rectangle(x: a.X, y: a.Y, width: a.Width, height: b.Y - a.Y));
                }
                if (a.Y + a.Height > b.Y + b.Height)
                {
                    // Bottom rectangle.
                    result.Add(new Rectangle(x: a.X, y: b.X + b.Height, width: a.Width, height: a.Y + a.Height - (b.Y + b.Height)));
                }
            }

            return result;
        }
    }
}
