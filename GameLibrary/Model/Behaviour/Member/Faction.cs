using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using GameLibrary.Model.Behaviour;
using GameLibrary.Factory;
using GameLibrary.Factory.FactoryEnums;

namespace GameLibrary.Model.Behaviour.Member
{
    public class Faction : Behaviour<Faction, FactionEnum>
    {
        public Faction(FactionEnum _type) : base(_type)
        {

        }

        public Faction(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            Faction faction = GameLibrary.Factory.BehaviourFactory.behaviourFactory.getFaction(this.type);
            if (faction != null)
            {
                this.behaviour = faction.BehaviourMember;
            }
            else
            {
                Logger.Logger.LogErr("Faction " + this.type.ToString() + " nicht in der Factory gefunden. Clientbehaviourfactory wurde nicht vom Server geupdatet");
            }
        }
    }
}
