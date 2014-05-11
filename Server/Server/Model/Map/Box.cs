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

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private Vector2 size;

        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }

        private Box topNeighbour;

        public Box TopNeighbour
        {
            get { return topNeighbour; }
            set { topNeighbour = value; }
        }
        private Box leftNeighbour;

        public Box LeftNeighbour
        {
            get { return leftNeighbour; }
            set { leftNeighbour = value; }
        }
        private Box rightNeighbour;

        public Box RightNeighbour
        {
            get { return rightNeighbour; }
            set { rightNeighbour = value; }
        }
        private Box bottomNeighbour;

        public Box BottomNeighbour
        {
            get { return bottomNeighbour; }
            set { bottomNeighbour = value; }
        }

        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; }
        }

        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        private Box parent;

        public Box Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public virtual void update()
        {

        }
    }
}
