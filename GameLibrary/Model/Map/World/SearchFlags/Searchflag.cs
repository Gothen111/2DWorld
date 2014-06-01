using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Model.Map.World.SearchFlags
{
    abstract public class Searchflag
    {
        public abstract Boolean hasFlag(GameLibrary.Model.Object.LivingObject livingObject);
    }
}
