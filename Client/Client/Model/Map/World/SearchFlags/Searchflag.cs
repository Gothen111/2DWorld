using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client.Model.Map.World.SearchFlags
{
    abstract class Searchflag
    {
        public abstract Boolean hasFlag(Model.Object.LivingObject livingObject);
    }
}
