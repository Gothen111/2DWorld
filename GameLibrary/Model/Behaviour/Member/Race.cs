using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Behaviour;
using GameLibrary.Factory.FactoryEnums;
using System.Runtime.Serialization;

namespace GameLibrary.Model.Behaviour.Member
{
    [Serializable()]
    public class Race : Behaviour<Race, RaceEnum>
    {
        public Race(RaceEnum _type) : base(_type)
        {
            
        }

        public Race(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt)
        {
            
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }
    }
}
