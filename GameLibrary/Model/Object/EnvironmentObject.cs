using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using GameLibrary.Factory.FactoryEnums;

namespace GameLibrary.Model.Object
{
    [Serializable()]
    public class EnvironmentObject : LivingObject
    {
        private EnvironmentEnum environmentEnum;

        public EnvironmentEnum EnvironmentEnum
        {
            get { return environmentEnum; }
            set { environmentEnum = value; }
        }

        public EnvironmentObject() :base()
        {
            this.CanBeEffected = false;
        }

        public EnvironmentObject(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }

        public override void update(GameTime _GameTime)
        {
            base.update(_GameTime);
        }
    }
}
