﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Model.Map.World.SearchFlags
{
    class DeadFlag : Searchflag
    {
        public override Boolean hasFlag(Model.Object.LivingObject livingObject)
        {
            return livingObject.IsDead;
        }
    }
}