using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;
namespace GameLibrary.Model.Map
{
    [Serializable()]
    public class Box : WorldElement
    {
        private Box parent;

        public Box Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        private bool isRequested;

        public bool IsRequested
        {
            get { return isRequested; }
            set { isRequested = value; }
        }

        private bool hasReceived;

        public bool HasReceived
        {
            get { return hasReceived; }
            set { hasReceived = value; }
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

        private int requestedTimer;
        private int requestedTimerMax;

        #endregion

        public Box()
            :base()
        {
            this.isRequested = false;
            this.hasReceived = false;

            this.requestedTimerMax = 10;
            this.requestedTimer = this.requestedTimerMax;
        }

        public Box(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }

        public virtual void update(GameTime _GameTime)
        {
            if (!Configuration.Configuration.isHost)
            {
                if (this.requestedTimer <= 0 && this.isRequested)
                {
                    this.requestFromServer();
                    this.requestedTimer = this.requestedTimerMax;
                }
                else
                {
                    this.requestedTimer -= 1;
                }
            }
        }

        protected override void boundsChanged()
        {
            this.Bounds = new Rectangle((int)this.Position.X, (int)this.Position.Y, this.Bounds.Width, this.Bounds.Height);
        }

        public virtual void requestFromServer()
        {
            this.isRequested = true;
        }
    }
}
