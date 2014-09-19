using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using GameLibrary.Model.Behaviour;
using GameLibrary.Factory.FactoryEnums;

namespace GameLibrary.Model.Behaviour.Member
{
    public class Race : Behaviour<Race, RaceEnum>
    {
        public Race(RaceEnum _type) : base(_type)
        {
            
        }

        public Race(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt)
        {
            Race race = GameLibrary.Factory.BehaviourFactory.behaviourFactory.getRace(this.type);
            if (race != null)
            {
                this.behaviour = race.BehaviourMember;
            }
            else
            {
                Logger.Logger.LogErr("Race " + this.type.ToString() + " nicht in der Factory gefunden. Clientbehaviourfactory wurde nicht vom Server geupdatet");
            }
        }
    }
}
