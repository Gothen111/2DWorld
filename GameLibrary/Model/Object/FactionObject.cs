using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using GameLibrary.Model.Behaviour.Member;

namespace GameLibrary.Model.Object
{
    [Serializable()]
    public class FactionObject : RaceObject
    {
        private Faction faction;

        public Faction Faction
        {
            get { return faction; }
            set { faction = value; }
        }

        public FactionObject()
        {

        }


        public FactionObject(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            this.faction = (Faction)info.GetValue("faction", typeof(Faction));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
            info.AddValue("faction", this.faction, typeof(Faction));
        }

        public override void update()
        {
            base.update();
        }
    }
}
