using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Server.Model.Map.Chunk;
namespace Server.Model.Object
{
    class Object
    {
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

        private Map.World.World world;

        public Map.World.World World
        {
            get { return world; }
            set { world = value; }
        }

        public virtual void update()
        {

        }
    }
}
