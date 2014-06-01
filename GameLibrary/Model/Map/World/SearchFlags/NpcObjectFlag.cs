﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Model.Map.World.SearchFlags
{
    public class NpcObjectFlag : Searchflag
    {
        public override Boolean hasFlag(GameLibrary.Model.Object.LivingObject livingObject)
        {
            return livingObject is GameLibrary.Model.Object.NpcObject;
        }
    }
}