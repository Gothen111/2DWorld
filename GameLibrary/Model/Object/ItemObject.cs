using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Runtime.Serialization;
using GameLibrary.Factory.FactoryEnums;
using Microsoft.Xna.Framework;

namespace GameLibrary.Model.Object
{
    [Serializable()]
    public class ItemObject : AnimatedObject
    {
        private ItemEnum itemEnum;

        public ItemEnum ItemEnum
        {
            get { return itemEnum; }
            set { itemEnum = value; }
        }

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

        private int positionInInventory;

        public int PositionInInventory
        {
            get { return positionInInventory; }
            set { positionInInventory = value; }
        }

        private String itemIconGraphicPath;

        public String ItemIconGraphicPath
        {
            get { return itemIconGraphicPath; }
            set { itemIconGraphicPath = value; }
        }

        public ItemObject()
            :base()
        {
            this.onlyFromPlayerTakeAble = false;
            this.onStack = 1;
            this.positionInInventory = -1;
        }

        public ItemObject(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            this.itemEnum = (ItemEnum)info.GetValue("itemEnum", typeof(int));
            this.onStack = (int)info.GetValue("onStack", typeof(int));
            this.positionInInventory = (int)info.GetValue("positionInInventory", typeof(int));
            this.itemIconGraphicPath = (String)info.GetValue("itemIconGraphicPath", typeof(String));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("itemEnum", this.itemEnum, typeof(int));
            info.AddValue("onStack", this.onStack, typeof(int));
            info.AddValue("positionInInventory", this.positionInInventory, typeof(int));
            info.AddValue("itemIconGraphicPath", this.itemIconGraphicPath, typeof(String));
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
                    ((PlayerObject)_CollideWith).addItemObjectToInventory(this);
                }
            }
            else
            {
                if (_CollideWith is CreatureObject)
                {
                    ((CreatureObject)_CollideWith).addItemObjectToInventory(this);
                }
            }          
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch, Microsoft.Xna.Framework.Vector3 _DrawPositionExtra, Microsoft.Xna.Framework.Color _Color)
        {
            _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.ItemIconGraphicPath], new Microsoft.Xna.Framework.Vector2(this.Position.X, this.Position.Y), new Rectangle(0, 0, (int)this.Size.X, (int)this.Size.Y), _Color, 0f, Microsoft.Xna.Framework.Vector2.Zero, new Microsoft.Xna.Framework.Vector2(this.Scale, this.Scale), Microsoft.Xna.Framework.Graphics.SpriteEffects.None, 1.0f);
        }
    }
}
