using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Model.Map.World.SearchFlags
{
    class FactionFlag : Searchflag
    {
        private Behaviour.Member.Faction faction;

        public FactionFlag(Behaviour.Member.Faction _Faction) : base()
        {
            this.faction = _Faction;
        }

        public FactionFlag(Factories.FactoryEnums.FactionEnum _FactionEnum)
        {
            this.faction = Factories.BehaviourFactory.behaviourFactory.getFaction(_FactionEnum);
        }

        public override Boolean hasFlag(Model.Object.LivingObject livingObject)
        {
            if (livingObject is Model.Object.FactionObject)
            {
                return (livingObject as Model.Object.FactionObject).Faction == this.faction;
            }
            else
            {
                return false;
            }
        }
    }
}
