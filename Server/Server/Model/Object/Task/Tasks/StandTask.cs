using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Map.World;
using Server.Commands.CommandTypes;

namespace Server.Model.Object.Task.Tasks
{
    class StandTask : LivingObjectTask
    {

        public StandTask(LivingObject _TaskOwner, TaskPriority _Priority)
            : base(_TaskOwner, _Priority)
        {
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
    }
}
