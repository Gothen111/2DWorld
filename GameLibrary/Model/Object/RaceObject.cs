using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using GameLibrary.Model.Behaviour.Member;

namespace GameLibrary.Model.Object
{
    public class RaceObject : CreatureObject
    {
        private Race race;

        public Race Race
        {
            get { return race; }
            set { race = value; }
        }

        public RaceObject() :base()
        {
            
        }

        public RaceObject(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            this.race = (Race)info.GetValue("race", typeof(Race));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
            info.AddValue("race", this.race, typeof(Race));
        }

        public override void update()
        {
            base.update();
        }
    }
}
