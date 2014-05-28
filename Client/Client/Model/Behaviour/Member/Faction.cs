using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Client.Model.Behaviour;
using Client.Factories;
using Client.Factories.FactoryEnums;

namespace Client.Model.Behaviour.Member
{
    class Faction : Behaviour<Faction, FactionEnum>
    {
        public Faction(FactionEnum _type) : base(_type)
        {

        }
    }
}
