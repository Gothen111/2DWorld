using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Behaviour;
using Server.Factories;
using Server.Factories.FactoryEnums;

namespace Server.Model.Behaviour.Member
{
    class Faction : Behaviour<Faction, FactionEnum>
    {
        public static List<Faction> factions = new List<Faction>();
        public Faction(FactionEnum _type) : base(_type)
        {

        }
    }
}
