using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;
using GameLibrary.Model.Map.Chunk;
namespace GameLibrary.Model.Object
{
    [Serializable()]
    public class Object : ISerializable
    {
        public static int _id = 0;
        private int id = _id++;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        private Vector3 position;

        public Vector3 Position
        {
            get { return position; }
            set { 
                position = value;
                bounds = new Rectangle((int)value.X - (int)size.X / 2, (int)value.Y - (int)size.Y, (int)size.X, (int)size.Y); //???
            }
        }

        private Vector3 size;

        public Vector3 Size
        {
            get { return size; }
            set { size = value;
                  bounds = new Rectangle((int)position.X - (int)value.X/2, (int)position.Y - (int)value.Y, (int)value.X, (int)value.Y); //???
            }
        }

        private Rectangle bounds;

        public Rectangle Bounds
        {
            get { return bounds; }
            set { bounds = value; }
        }

        private List<Rectangle> collisionBounds;

        public List<Rectangle> CollisionBounds
        {
            get { return collisionBounds; }
            set { collisionBounds = value; }
        }

        private Map.Block.Block currentBlock;

        public Map.Block.Block CurrentBlock
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
            this.collisionBounds = new List<Rectangle>();
        }

        public Object(SerializationInfo info, StreamingContext ctxt)
            :this()
        {
            this.Id = (int)info.GetValue("Id", typeof(int));

            this.position = (Vector3)info.GetValue("position", typeof(Vector3));
            this.size = (Vector3)info.GetValue("size", typeof(Vector3));
            this.velocity = (Vector3)info.GetValue("velocity", typeof(Vector3));

            this.bounds = new Rectangle((int)this.position.X - (int)size.X / 2, (int)this.position.Y - (int)size.Y, (int)size.X, (int)size.Y); //???

            this.objects = (List<Object>)info.GetValue("objects", typeof(List<Object>));

            List<Utility.Corpus.Square> var_List = (List<Utility.Corpus.Square>)info.GetValue("collisionBounds", typeof(List<Utility.Corpus.Square>));
            this.collisionBounds = new List<Rectangle>();

            foreach (Utility.Corpus.Square var_Square in var_List)
            {
                this.collisionBounds.Add(var_Square.Rectangle);
            }
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("Id", this.Id, typeof(int));

            info.AddValue("position", this.position, typeof(Vector3));
            info.AddValue("size", this.size, typeof(Vector3));
            info.AddValue("velocity", this.velocity, typeof(Vector3));

            info.AddValue("objects", this.objects, typeof(List<Object>));

            List<Utility.Corpus.Square> var_List = new List<Utility.Corpus.Square>();
            foreach (Rectangle var_Rectangle in this.collisionBounds)
            {
                var_List.Add(new Utility.Corpus.Square(var_Rectangle));
            }

            info.AddValue("collisionBounds", var_List, typeof(List<Utility.Corpus.Square>)); //???
        }

        public virtual void update()
        {
        }
    }
}
