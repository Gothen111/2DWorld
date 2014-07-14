using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;

namespace GameLibrary.Model.Object
{
    [Serializable()]
    public class ItemObject : AnimatedObject
    {
        private bool stackAble;

        public bool StackAble
        {
            get { return stackAble; }
            set { stackAble = value; }
        }

        private int onStack;

        public int OnStack
        {
            get { return onStack; }
            set { onStack = value; }
        }

        private int stackMax;

        public int StackMax
        {
            get { return stackMax; }
            set { stackMax = value; }
        }

        private bool onlyFromPlayerTakeAble;

        public bool OnlyFromPlayerTakeAble
        {
            get { return onlyFromPlayerTakeAble; }
            set { onlyFromPlayerTakeAble = value; }
        }

        public ItemObject()
            :base()
        {
            this.onlyFromPlayerTakeAble = false;
        }

        public ItemObject(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {

        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }

        public override void update()
        {
            base.update();
        }

        public override void onCollide(AnimatedObject _CollideWith)
        {
            base.onCollide(_CollideWith);
            if (this.onlyFromPlayerTakeAble)
            {
                if (_CollideWith is PlayerObject)
                {
                    ((PlayerObject)_CollideWith).Inventory.addItemObjectToInventory(this);
                }
            }
            else
            {
                if (_CollideWith is CreatureObject)
                {
                    ((CreatureObject)_CollideWith).Inventory.addItemObjectToInventory(this);
                }
            }
           
        }
    }
}
