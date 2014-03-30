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
        public Faction(FactionEnum _type) : base(_type)
        {

        }
    }
}
