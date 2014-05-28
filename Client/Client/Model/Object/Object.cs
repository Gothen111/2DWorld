﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;
using Client.Model.Map.Chunk;
namespace Client.Model.Object
{
    [Serializable()]
    class Object
    {
        private static int _id = 0;
        public readonly int Id = _id++;

        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { 
                position = value; 
                bounds = new Rectangle((int)value.X, (int)value.Y, (int)size.X, (int)size.Y); 
                if(positionChanged != null)
                    positionChanged(this, new EventArgs()); 
            }
        }

        public event EventHandler positionChanged;

        private Vector3 size;

        public Vector3 Size
        {
            get { return size; }
            set { size = value; bounds = new Rectangle((int)position.X, (int)position.Y, (int)size.X, (int)size.Y); }
        }

        private Rectangle bounds;

        public Rectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }

        private Map.Block.Block currentBlock;

        internal Map.Block.Block CurrentBlock
        {
            get { return currentBlock; }
            set { currentBlock = value; }
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

        public Object()
        {
            
        }

        public Object(SerializationInfo info, StreamingContext ctxt)
        {
            this.Id = (int)info.GetValue("Id", typeof(int));

            this.position = (Vector3)info.GetValue("position", typeof(Vector3));
            this.size = (Vector3)info.GetValue("size", typeof(Vector3));
            this.velocity = (Vector3)info.GetValue("velocity", typeof(Vector3));

            this.bounds = (Rectangle)info.GetValue("bounds", typeof(Rectangle));

            this.objects = (List<Object>)info.GetValue("objects", typeof(List<Object>));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Id", this.Id, typeof(int));

            info.AddValue("position", this.position, typeof(Vector3));
            info.AddValue("size", this.size, typeof(Vector3));
            info.AddValue("velocity", this.velocity, typeof(Vector3));

            info.AddValue("bounds", this.bounds, typeof(Rectangle));

            info.AddValue("objects", this.objects, typeof(List<Object>));
        }

        public virtual void update()
        {

        }
    }
}