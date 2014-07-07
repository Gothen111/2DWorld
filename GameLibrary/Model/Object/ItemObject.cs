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

        public ItemObject()
            :base()
        {
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
            if (_CollideWith is CreatureObject)
            {
                ((CreatureObject)_CollideWith).Inventory.addItemObjectToInventory(this);
            }
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch, Microsoft.Xna.Framework.Vector3 _DrawPositionExtra, Microsoft.Xna.Framework.Color _Color)
        {
        }
    }
}
