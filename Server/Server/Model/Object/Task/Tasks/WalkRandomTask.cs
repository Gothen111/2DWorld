﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Map.World;

namespace Server.Model.Object.Task.Tasks
{
    class WalkRandomTask : LivingObjectTask
    {
        private bool finishedWalking;

        public WalkRandomTask(LivingObject _TaskOwner, int _Priority)
            : base(_TaskOwner, _Priority)
        {
            this.finishedWalking = true;
        }

        public override bool wantToDoTask(World _World)
        {
            bool var_wantToDoTask = true;

            return var_wantToDoTask || base.wantToDoTask(_World);
        }

        public override void update(World _World)
        {
            base.update(_World);
        }

        private void walkRandom()
        {
            if (this.finishedWalking)
            {
                
            }
            else
            {

            }
        }
    }
}
