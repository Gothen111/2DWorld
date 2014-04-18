using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Map.World;
using Server.Commands.CommandTypes;

namespace Server.Model.Object.Task.Tasks
{
    class WalkRandomTask : LivingObjectTask
    {
        private bool finishedWalking;

        public WalkRandomTask(LivingObject _TaskOwner, CommandPriority _Priority)
            : base(_TaskOwner, _Priority)
        {
            this.finishedWalking = true;
        }

        public override bool wantToDoTask()
        {
            bool var_wantToDoTask = true;

            return var_wantToDoTask || base.wantToDoTask();
        }

        public override void update()
        {
            base.update();
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
