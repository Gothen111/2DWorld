using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Model.Map.World.SearchFlags
{
    public class RaceFlag : Searchflag
    {
        private Behaviour.Member.Race race;

        public RaceFlag(Behaviour.Member.Race _Race) : base()
        {
            this.race = _Race;
        }

        public RaceFlag(GameLibrary.Factory.FactoryEnums.RaceEnum _RaceEnum)
        {
            this.race = GameLibrary.Factory.BehaviourFactory.behaviourFactory.getRace(_RaceEnum);
        }

        public override Boolean hasFlag(GameLibrary.Model.Object.LivingObject livingObject)
        {
            if (livingObject is GameLibrary.Model.Object.RaceObject)
            {
                return (livingObject as GameLibrary.Model.Object.RaceObject).Race == this.race;
            }
            else
            {
                return false;
            }
        }
    }
}
