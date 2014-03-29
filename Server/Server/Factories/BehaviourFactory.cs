using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Behaviour.Member.Faction;
using Server.Model.Behaviour.Member.Race;
using Server.Factories.FactoryEnums;
using Server.Model.Behaviour;

namespace Server.Factories
{
    class BehaviourFactory
    {
        public static BehaviourFactory behaviourFactory = new BehaviourFactory();

        protected List<Behaviour<Faction>> factions;
        public List<Behaviour<Faction>> Factions
        {
            get{ return factions;}
        }

        protected List<Race> races;
        public List<Race> Races{
            get{ return races;}
        }

        private BehaviourFactory() { }

        public Faction getFaction(FactionEnum item)
        {
            foreach(Behaviour<Faction> behave in factions)
            {
                foreach(Faction faction in behave.BehaviourMembers)
                {
                    if (faction.Type == item)
                        return faction;
                }
            }
            return createFaction(item);
        }

        private Faction createFaction(FactionEnum type)
        {
            Faction faction;
            switch (type)
            {
                case FactionEnum.Human:
                    {
                        faction = new Faction(type);
                        break;
                    }
            }
            factions.Add(faction);
            faction.Behaviour = factions;
            return faction;
        }

        private Race createRace(RaceEnum type)
        {

        }
    }
}
