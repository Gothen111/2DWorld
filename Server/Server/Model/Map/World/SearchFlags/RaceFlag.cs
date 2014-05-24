using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Model.Map.World.SearchFlags
{
    class RaceFlag : Searchflag
    {
        private Behaviour.Member.Race race;

        public RaceFlag(Behaviour.Member.Race _Race) : base()
        {
            this.race = _Race;
        }

        public RaceFlag(Factories.FactoryEnums.RaceEnum _RaceEnum)
        {
            this.race = Factories.BehaviourFactory.behaviourFactory.getRace(_RaceEnum);
        }

        public override Boolean hasFlag(Model.Object.LivingObject livingObject)
        {
            if (livingObject is Model.Object.RaceObject)
            {
                return (livingObject as Model.Object.RaceObject).Race == this.race;
            }
            else
            {
                return false;
            }
        }
    }
}
