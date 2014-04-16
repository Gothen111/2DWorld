using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Model.Object.Task.Tasks
{
    class AttackTask : LivingObjectTask
    {
        public AttackTask(LivingObject _TaskOwner, int _Priority)
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
