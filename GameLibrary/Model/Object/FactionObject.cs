using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using GameLibrary.Model.Behaviour.Member;
using GameLibrary.Factory.FactoryEnums;
using Microsoft.Xna.Framework;

namespace GameLibrary.Model.Object
{
    [Serializable()]
    public class FactionObject : RaceObject
    {
        private FactionEnum factionEnum;

        public FactionEnum FactionEnum
        {
            get { return factionEnum; }
            set { factionEnum = value; }
        }

        public Faction Faction
        {
            get { return GameLibrary.Factory.BehaviourFactory.behaviourFactory.getFaction(this.FactionEnum); }
        }

        public FactionObject()
        {

        }


        public FactionObject(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            this.factionEnum = (FactionEnum)info.GetValue("factionEnum", typeof(FactionEnum));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
            info.AddValue("factionEnum", this.factionEnum, typeof(FactionEnum));
        }

        public override void update(GameTime _GameTime)
        {
            base.update(_GameTime);
        }
    }
}
