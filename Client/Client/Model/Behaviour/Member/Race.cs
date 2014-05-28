using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Client.Model.Behaviour;
using Client.Factories.FactoryEnums;

namespace Client.Model.Behaviour.Member
{
    class Race : Behaviour<Race, RaceEnum>
    {
        public Race(RaceEnum _type) : base(_type)
        {
            
        }
    }
}
