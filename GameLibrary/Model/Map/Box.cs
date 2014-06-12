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
        public static int _id = 0;
        private int id = _id++;

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

        [System.Xml.Serialization.XmlIgnoreAttribute]
        private Box parent;

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public Box Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        private bool needUpdate;

        [System.Xml.Serialization.XmlIgnoreAttribute]
        public bool NeedUpdate
        {
            get { return needUpdate; }
            set { needUpdate = value; }
        }

        [System.Xml.Serialization.XmlIgnoreAttribute]
        private List<Box> childsToUpdate;

        public Box()
        {
            this.needUpdate = true;
            this.childsToUpdate = new List<Box>();
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
            this.needUpdate = false;
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

        public void markAsDirty()
        {
            this.needUpdate = true;
            if (this.parent != null)
            {
                this.parent.markAsDirty();
            }
        }

        public void addChildToUpdateList(Box _Box)
        {
            if (!this.childsToUpdate.Contains(_Box))
            {
                this.childsToUpdate.Add(_Box);
            }
        }

        public void updateChilds()
        {
            foreach (Box var_Box in this.childsToUpdate)
            {
                var_Box.update();
            }
            this.childsToUpdate.Clear();
        }
    }
}
