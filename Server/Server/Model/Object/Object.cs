using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Server.Model.Object
{
    class Object
    {
        protected Vector3 position;
        protected List<Object> objects;
        protected Vector3 velocity;
    }
}
