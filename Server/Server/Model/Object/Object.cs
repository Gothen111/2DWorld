using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Server.Model.Object
{
    class Object
    {
        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        private List<Object> objects;

        public List<Object> Objects
        {
            get { return objects; }
            set { objects = value; }
        }
        private Vector3 velocity;

        public Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
    }
}
