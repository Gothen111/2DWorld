using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Behaviour;
using GameLibrary.Factory.FactoryEnums;

namespace GameLibrary.Model.Behaviour.Member
{
    public class Race : Behaviour<Race, RaceEnum>
    {
        public Race(RaceEnum _type) : base(_type)
        {
            
        }
    }
}
