using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GameLibrary.Model.Object
{
    [Serializable()]
    public class NpcObject : FactionObject
    {
        public NpcObject() : base()
        {
            
        }

        public NpcObject(SerializationInfo info, StreamingContext ctxt)
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
