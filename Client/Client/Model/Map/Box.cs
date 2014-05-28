using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;
namespace Client.Model.Map
{
    [Serializable()]
    class Box : ISerializable
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

        public Box()
        {

        }

        public Box(SerializationInfo info, StreamingContext ctxt)
        {
            this.id = (int)info.GetValue("id", typeof(int));
            this.size = (Vector2)info.GetValue("size", typeof(Vector2));
            this.position = (Vector2)info.GetValue("position", typeof(Vector2));
            this.name = (String)info.GetValue("name", typeof(String));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("id", this.id);
            info.AddValue("size", this.size, typeof(Vector2));
            info.AddValue("position", this.position, typeof(Vector2));
            info.AddValue("name", this.name);
        }

        public virtual void update()
        {

        }
    }
}
