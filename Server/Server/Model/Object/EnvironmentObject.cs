﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Model.Object
{
    class EnvironmentObject : LivingObject
    {
        public EnvironmentObject()
        {
            this.LayerDepth = 0.0f;
            this.CanBeEffected = false;
        }
        public override void update()
        {
            base.update();
        }
    }
}
