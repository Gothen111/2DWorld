using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Server.Util;

namespace Server.Util
{
    class Intersection
    {

        public static Boolean RectangleIntersectsRectangle(Rectangle r1, Rectangle r2)
        {
            Boolean result = r1.Intersects(r2);
            return result;
        }

        public static Boolean CircleIntersectsRectangle(Circle circle, Rectangle rectangle)
        {
            //Berechne, ob der Kreis das Rechteck schneidet, also entweder liegt das Rechteck im Kreis, oder eine Seite des Rechtecks schneidet den Kreis
            Boolean intersectTop = LineIntersectsCircle(new Vector2(rectangle.X, rectangle.Y), new Vector2(rectangle.X + rectangle.Width, rectangle.Y), circle);
            Boolean intersectLeft = LineIntersectsCircle(new Vector2(rectangle.X, rectangle.Y), new Vector2(rectangle.X, rectangle.Y + rectangle.Height), circle);
            Boolean intersectRight = LineIntersectsCircle(new Vector2(rectangle.X + rectangle.Width, rectangle.Y), new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height), circle);
            Boolean intersectBottom = LineIntersectsCircle(new Vector2(rectangle.X, rectangle.Y + rectangle.Height), new Vector2(rectangle.X + rectangle.Width, rectangle.Y + rectangle.Height), circle);
            return CircleIsInRectangle(circle, rectangle) || intersectTop || intersectLeft || intersectRight || intersectBottom;
        }

        public static Boolean CircleIsInRectangle(Circle circle, Rectangle rectangle)
        {
            Boolean circleInRectangle = circle.Position.X - circle.Radius >= rectangle.Left && circle.Position.X + circle.Radius <= rectangle.Right && circle.Position.Y - circle.Radius >= rectangle.Top && circle.Position.Y + circle.Radius <= rectangle.Bottom;
            return circleInRectangle;
        }

        public static Boolean RectangleIsInRectangle(Rectangle smallerOne, Rectangle biggerOne)
        {
            return biggerOne.Left <= smallerOne.Left && biggerOne.Right >= smallerOne.Right && biggerOne.Top <= smallerOne.Top && biggerOne.Bottom >= smallerOne.Bottom;
        }

        public static Boolean LineIntersectsCircle(Vector2 a, Vector2 b, Circle circle)
        {
            /*//Steigung der Linie berechnen
            float delta = (point2.Y - point1.Y) / (point2.X - point1.X);
            //Steigung der Normalen berechnen
            float deltaNorm = -1 / delta;

            //Distanz der Steigung berechnen( welche Distanz wird für 1 X-Wert nach rechts zurückgelegt)
            double deltaDistance = distanceDelta(deltaNorm);
            //Punkt auf dem Kreis berechnen, welcher am nähesten an der Linie liegt
            Vector2 radiusPoint = new Vector2((float)(circle.Position.X + deltaDistance / circle.Radius * 1), (float)(circle.Position.Y + deltaDistance / circle.Radius * deltaDistance));
            //Berechne Schnittpunkt zwischen Kreis und Linie
            try
            {
                Vector2 intersectionPoint = LineIntersectionPoint(point1, point2, new Vector2(circle.Position.X, circle.Position.Y), radiusPoint);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
            //Steigung der Linie berechnen

            float delta = (point2.Y - point1.Y) / (point2.X - point1.X);

            //Steigung der Normalen berechnen

            float deltaNorm = -1 / delta;



            //Distanz der Steigung berechnen( welche Distanz wird für 1 X-Wert nach rechts zurückgelegt)

            double deltaDistance = distanceDelta(deltaNorm);

            //Punkt auf dem Kreis berechnen, welcher am nähesten an der Linie liegt

            Vector2 radiusPoint = new Vector2((float)(circle.Position.X + deltaDistance / circle.Radius * 1), (float)(circle.Position.Y + deltaDistance / circle.Radius * deltaDistance));

            //Berechne Schnittpunkt zwischen Kreis und Linie

            Vector2 intersectionPoint = Vector2.Zero;
            if (deltaNorm == 0)
            {
                intersectionPoint = new Vector2(point1.X, circle.Position.Y);
            }
            else
            {
                intersectionPoint = LineIntersectionPoint(point1, point2, new Vector2(circle.Position.X, circle.Position.Y), radiusPoint);
            }

            //Falls Schnittpunktkoordinate >= 0, dann ist ein Schnittpunkt vorhanden

            return Vector2.Distance(intersectionPoint, new Vector2(circle.Position.X, circle.Position.Y)) <= circle.Radius;*/

            // First up, let's normalise our vectors so the circle is on the origin
            Vector2 c = new Vector2(circle.Position.X, circle.Position.Y);
            float rad = circle.Radius;
            Vector2 normA = a - c;
            Vector2 normB = b - c;

            Vector2 d = normB - normA;

            // Want to solve as a quadratic equation, need 'a','b','c' components
            float aa = Vector2.Dot(d, d);
            float bb = 2 * (Vector2.Dot(normA, d));
            float cc = Vector2.Dot(normA, normA) - (rad * rad);

            // Get determinant to see if LINE intersects
            double deter = Math.Pow(bb, 2.0) - 4 * aa * cc;
            if (deter > 0)
            {
                // Get t values (solve equation) to see if LINE SEGMENT intersects
                float q; // Holds the solution to the quadratic equation
                if (bb >= 0)
                {
                    q = (-bb - (float)Math.Sqrt(deter)) / 2;
                }
                else
                {
                    q = (-bb + (float)Math.Sqrt(deter)) / 2;
                }

                float t0 = q / aa;
                float t1 = cc / q; 
                Boolean match = false;

                if (0.0 <= t0 && t0 <= 1.0)
                {
                    // Interpolate to get collision point
                    Vector2 collisionPoint = c + Vector2.Lerp(normA, normB, (float)t0);
                    match = true;
                }
                if (0.0 <= t1 && t1 <= 1.0)
                {
                    Vector2 collisionPoint2 = c + Vector2.Lerp(normA, normB, (float)t1);
                    match = true;
                }
                return match;
            }
            else
                return false;
        }

        private static double distanceDelta(float delta)
        {
            return Math.Sqrt(1 + Math.Pow(delta, 2));
        }

        public static Vector2 LineIntersectionPoint(Vector2 ps1, Vector2 pe1, Vector2 ps2, Vector2 pe2)
        {
            // Get A,B,C of first line - points : ps1 to pe1
            float A1 = pe1.Y - ps1.Y;
            float B1 = ps1.X - pe1.X;
            float C1 = A1 * ps1.X + B1 * ps1.Y;

            // Get A,B,C of second line - points : ps2 to pe2
            float A2 = pe2.Y - ps2.Y;
            float B2 = ps2.X - pe2.X;
            float C2 = A2 * ps2.X + B2 * ps2.Y;

            // Get delta and check if the lines are parallel
            float delta = A1 * B2 - A2 * B1;
            if (delta == 0)
                throw new Exception();

            // now return the Vector2 intersection point
            return new Vector2(
                (B2 * C1 - B1 * C2) / delta,
                (A1 * C2 - A2 * C1) / delta
            );
        }
    }
}
