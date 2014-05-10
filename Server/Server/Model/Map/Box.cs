using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
namespace Server.Model.Map
{
    class Box
    {
        private int id;
        private Vector2 size;

        private Box topNeighbour;
        private Box leftNeighbour;
        private Box rightNeighbour;
        private Box bottomNeighbour;

        private Vector2 position;

        private String name;

        private Box parent;

        public virtual void update()
        {

        }
    }
}
