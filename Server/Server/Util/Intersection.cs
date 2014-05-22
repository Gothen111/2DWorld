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

        public static Boolean LineIntersectsCircle(Vector2 point1, Vector2 point2, Circle circle)
        {
            //Steigung der Linie berechnen
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
