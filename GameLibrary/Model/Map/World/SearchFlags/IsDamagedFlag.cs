using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Model.Map.World.SearchFlags
{
    public class IsDamagedFlag : Searchflag
    {
        public override Boolean hasFlag(GameLibrary.Model.Object.Object _Object)
        {
            if (_Object is Object.LivingObject)
            {
                return ((Object.LivingObject)_Object).HealthPoints < ((Object.LivingObject)_Object).MaxHealthPoints;
            }
            else
            {
                return false;
            }
        }
    }
}
