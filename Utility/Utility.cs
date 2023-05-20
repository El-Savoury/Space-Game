namespace Space_Game
{
    /// <summary>
    ///  Game specific utility functions.
    /// </summary>
    static class Utility
    {
        /// <summary>
        /// Calculate gravitational attraction force between entities. 
        /// </summary>
        public static float CalculateGravity(Entity e1, Entity e2)
        {
            float distance = Utility.GetDistance(e1.GetPosition(), e2.GetPosition());
            float g = 1f; // Universal gravitational constant.

            // Attraction force is equal between both objects so only need to calculate this once.
            float gravity = g * (e1.GetMass() * e2.GetMass()) / (distance * distance); 

            return gravity;
        }

        /// <summary>
        /// Gets the difference vector between two positions.
        /// </summary>
        /// <returns>Vector2 which is the difference vector between positions</returns>
        public static Vector2 GetDifference(Vector2 v1, Vector2 v2)
        {
            return v1 - v2;
        }


        /// <summary>
        /// Get distance between two postitons.
        /// </summary>
        /// <returns>Float equalling distance between positions</returns>
        public static float GetDistance(Vector2 a, Vector2 b)
        {
            float dx = a.X - b.X;
            float dy = a.Y - b.Y;
            return (float)Math.Sqrt(dx * dx + dy * dy);
        }


        /// <summary>
        /// Checks if point is within bounds of rectangle.
        /// </summary>
        /// <returns>True if point is within rectangle</returns>
        public static bool IsPointinRect(Point point, Rectangle rect)
        {
            return point.X >= rect.X && point.X < rect.X + rect.Width &&
                   point.Y >= rect.Y && point.Y < rect.Y + rect.Height;
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
