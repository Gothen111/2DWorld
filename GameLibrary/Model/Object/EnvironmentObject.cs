using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GameLibrary.Model.Object
{
    [Serializable()]
    public class EnvironmentObject : LivingObject
    {
        public EnvironmentObject() :base()
        {
            this.LayerDepth = 0.0f;
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

        public override void update()
        {
            if (this.NeedUpdate)
            {
                base.update();
            }
        }
    }
}
