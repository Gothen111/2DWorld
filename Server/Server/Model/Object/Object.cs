﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Server.Model.Object
{
    class Object
    {
        private Vector3 position;

        protected Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        private List<Object> objects;

        protected List<Object> Objects
        {
            get { return objects; }
            set { objects = value; }
        }
        private Vector3 velocity;

        protected Vector3 Velocity
        {
            get { return velocity; }
            set { velocity = value; }
        }
    }
}
