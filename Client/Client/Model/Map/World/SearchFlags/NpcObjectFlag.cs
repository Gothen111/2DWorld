using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Model.Map.World.SearchFlags
{
    class NpcObjectFlag : Searchflag
    {
        public override Boolean hasFlag(Model.Object.LivingObject livingObject)
        {
            return livingObject is Model.Object.NpcObject;
        }
    }
}
