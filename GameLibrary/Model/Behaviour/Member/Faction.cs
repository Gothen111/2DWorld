using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
    }
}
