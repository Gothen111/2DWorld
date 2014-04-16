using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Map.World;

namespace Server.Model.Object.Task.Tasks
{
    class AttackTask : LivingObjectTask
    {
        public AttackTask(LivingObject _TaskOwner, int _Priority)
            : base(_TaskOwner, _Priority)
        {

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
    }
}
