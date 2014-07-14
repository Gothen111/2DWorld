using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Model.Map.World.SearchFlags
{
    public class NpcObjectFlag : Searchflag
    {
        public override Boolean hasFlag(GameLibrary.Model.Object.Object _Object)
        {
            return _Object is GameLibrary.Model.Object.NpcObject;
        }
    }
}
