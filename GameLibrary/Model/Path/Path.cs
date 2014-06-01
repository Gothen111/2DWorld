using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace GameLibrary.Model.Path
{
    public class Path
    {
        Vector2 startPoint;
        Vector2 currentPoint;
        Vector2 endPoint;

        List<Vector2> wayToWalk;

        public Path(Vector2 _StartPoint, Vector2 _EndPoint)
        {
            this.startPoint = _StartPoint;
            this.endPoint = _EndPoint;

            this.generatePath();
        }

        private void generatePath()
        {
            this.wayToWalk = PathFinder.generatePath(this.startPoint, this.endPoint);
        }
    }
}
