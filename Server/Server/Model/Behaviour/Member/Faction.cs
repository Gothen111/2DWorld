using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Behaviour;
using Server.Factories;
using Server.Factories.FactoryEnums;

namespace Server.Model.Behaviour.Member.Faction
{
    class Faction
    {
        protected Behaviour<Faction> behaviour;
        public Behaviour<Faction> Behaviour
        {
            get { return behaviour; }
            set { behaviour = value; }
        }

        protected FactionEnum type;
        public FactionEnum Type
        {
            get { return type; }
        }

        public Faction(FactionEnum _type)
        {
            this.type = _type;
            this.behaviour = BehaviourFactory.behaviourFactory.Factions;
        }
    }
}
