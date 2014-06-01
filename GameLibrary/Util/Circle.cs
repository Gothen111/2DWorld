using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace GameLibrary.Util
{
    public class Circle
    {
        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        private float radius;

        public float Radius
        {
            get { return radius; }
            set { radius = value; }
        }

        public Circle()
        {

        }

        public Circle(Vector3 _Position, float _Radius)
        {
            this.position = _Position;
            this.radius = _Radius;
        }
    }
}
