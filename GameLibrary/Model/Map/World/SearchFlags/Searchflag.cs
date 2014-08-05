using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Model.Map.World.SearchFlags
{
    [Serializable()]
    abstract public class Searchflag
    {
        public abstract Boolean hasFlag(GameLibrary.Model.Object.Object _Object);
    }
}
