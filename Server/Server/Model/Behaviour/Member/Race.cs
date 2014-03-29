using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Behaviour;
using Server.Factories.FactoryEnums;

namespace Server.Model.Behaviour.Member
{
    class Race : Behaviour<Race, RaceEnum>
    {
        public static List<Race> races = new List<Race>();
        public Race(RaceEnum _type) : base(_type)
        {
            
        }
    }
}
