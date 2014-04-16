using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public override bool wantToDoTask()
        {
            base.wantToDoTask();

            return true;
        }

        public override void update()
        {
            base.update();
        }

        private void walkRandom()
        {
            if (this.finishedWalking)
            {
                if (this.TaskOwner.CurrentChunk != null)
                {

                }
            }
            else
            {

            }
        }
    }
}
