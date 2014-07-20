using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Object;

namespace GameLibrary.Model.Map.World.SearchFlags
{
    public class DeadFlag : Searchflag
    {
        public override Boolean hasFlag(GameLibrary.Model.Object.Object _Object)
        {
            return _Object is LivingObject ? ((LivingObject)_Object).IsDead : false;
        }
    }
}
