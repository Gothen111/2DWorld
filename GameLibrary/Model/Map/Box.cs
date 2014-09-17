using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;
namespace GameLibrary.Model.Map
{
    [Serializable()]
    public class Box : ISerializable
    {
        private Vector2 size;

        public Vector2 Size
        {
            get { return size; }
            set { size = value; boundsChanged(); }
        }

        private Vector2 position;

        public Vector2 Position
        {
            get { return position; }
            set { position = value; boundsChanged(); }
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

        #region neighbours
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

        private bool topNeighbourRequested;

        public bool TopNeighbourRequested
        {
            get { return topNeighbourRequested; }
            set { topNeighbourRequested = value; }
        }
        private bool leftNeighbourRequested;

        public bool LeftNeighbourRequested
        {
            get { return leftNeighbourRequested; }
            set { leftNeighbourRequested = value; }
        }
        private bool rightNeighbourRequested;

        public bool RightNeighbourRequested
        {
            get { return rightNeighbourRequested; }
            set { rightNeighbourRequested = value; }
        }
        private bool bottomNeighbourRequested;

        public bool BottomNeighbourRequested
        {
            get { return bottomNeighbourRequested; }
            set { bottomNeighbourRequested = value; }
        }

        private int neighbourRequestedTimer;
        private int neighbourRequestedTimerMax;

        #endregion

        public Box()
        {
            this.topNeighbourRequested = false;
            this.leftNeighbourRequested = false;
            this.rightNeighbourRequested = false;
            this.bottomNeighbourRequested = false;
            this.neighbourRequestedTimerMax = 200;
            this.neighbourRequestedTimer = this.neighbourRequestedTimerMax;
            
        }

        public Box(SerializationInfo info, StreamingContext ctxt) 
            :this()
        {
            
            this.size = (Vector2)info.GetValue("size", typeof(Vector2));
            this.position = (Vector2)info.GetValue("position", typeof(Vector2));
            this.name = (String)info.GetValue("name", typeof(String));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("size", this.size, typeof(Vector2));
            info.AddValue("position", this.position, typeof(Vector2));
            info.AddValue("name", this.name);
        }

        public virtual void update()
        {
            if (this.neighbourRequestedTimer <= 0)
            {
                this.topNeighbourRequested = false;
                this.leftNeighbourRequested = false;
                this.rightNeighbourRequested = false;
                this.bottomNeighbourRequested = false;
                this.neighbourRequestedTimer = this.neighbourRequestedTimerMax;
            }
            else
            {
                this.neighbourRequestedTimer -= 1;
            }
        }

        public virtual void boundsChanged()
        {

        }
    }
}
