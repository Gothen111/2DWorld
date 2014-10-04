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
    public class RaceObject : CreatureObject
    {
        private RaceEnum raceEnum;

        public RaceEnum RaceEnum
        {
            get { return raceEnum; }
            set { raceEnum = value; }
        }

        public Race Race
        {
            get { return GameLibrary.Factory.BehaviourFactory.behaviourFactory.getRace(this.RaceEnum); }
        }

        public RaceObject() :base()
        {
        }

        public RaceObject(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            this.raceEnum = (RaceEnum)info.GetValue("raceEnum", typeof(RaceEnum));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
            info.AddValue("raceEnum", this.raceEnum, typeof(RaceEnum));
        }

        public override void update(GameTime _GameTime)
        {
            base.update(_GameTime);
        }
    }
}
