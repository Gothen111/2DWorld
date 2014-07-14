using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Model.Map.World.SearchFlags
{
    public class FactionFlag : Searchflag
    {
        private Behaviour.Member.Faction faction;

        public FactionFlag(Behaviour.Member.Faction _Faction) : base()
        {
            this.faction = _Faction;
        }

        public FactionFlag(GameLibrary.Factory.FactoryEnums.FactionEnum _FactionEnum)
        {
            this.faction = GameLibrary.Factory.BehaviourFactory.behaviourFactory.getFaction(_FactionEnum);
        }

        public override Boolean hasFlag(GameLibrary.Model.Object.Object _Object)
        {
            if (_Object is GameLibrary.Model.Object.FactionObject)
            {
                return (_Object as GameLibrary.Model.Object.FactionObject).Faction == this.faction;
            }
            else
            {
                return false;
            }
        }
    }
}
