using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;

namespace GameLibrary.Model
{
    [Serializable()]
    public class WorldElement : ISerializable
    {
        private Vector3 size;

        public Vector3 Size
        {
            get { return size; }
            set { size = value; this.boundsChanged(); }
        }

        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { position = value; this.boundsChanged(); }
        }

        private Rectangle bounds;

        public Rectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }

        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        private double lastUpdateTime;

        public double LastUpdateTime
        {
            get { return lastUpdateTime; }
            set { lastUpdateTime = value; }
        }

        public WorldElement()
        {
        }

        public WorldElement(SerializationInfo info, StreamingContext ctxt) 
            :this()
        {           
            this.size = (Vector3)info.GetValue("size", typeof(Vector3));
            this.position = (Vector3)info.GetValue("position", typeof(Vector3));
            this.name = (String)info.GetValue("name", typeof(String));
            this.Bounds = ((Utility.Corpus.Square)info.GetValue("bounds", typeof(Utility.Corpus.Square))).Rectangle;
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("size", this.size, typeof(Vector3));
            info.AddValue("position", this.position, typeof(Vector3));
            info.AddValue("name", this.name);
            info.AddValue("bounds", new Utility.Corpus.Square(this.Bounds), typeof(Utility.Corpus.Square));
        }

        protected virtual void boundsChanged()
        {
        }
    }
}
