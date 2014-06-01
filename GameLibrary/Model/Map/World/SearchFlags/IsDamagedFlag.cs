using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Model.Map.World.SearchFlags
{
    public class IsDamagedFlag : Searchflag
    {
        public override Boolean hasFlag(GameLibrary.Model.Object.LivingObject livingObject)
        {
            return livingObject.HealthPoints < livingObject.MaxHealthPoints;
        }
    }
}
