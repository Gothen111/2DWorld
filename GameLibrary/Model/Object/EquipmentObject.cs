using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using GameLibrary.Factory.FactoryEnums;

namespace GameLibrary.Model.Object
{
    [Serializable()]
    public class EquipmentObject : AnimatedObject
    {
        public EquipmentObject()
        {

        }

        public EquipmentObject(SerializationInfo info, StreamingContext ctxt)
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
    }
}
