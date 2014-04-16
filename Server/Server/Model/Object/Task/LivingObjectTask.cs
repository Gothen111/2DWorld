using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Map.World;

namespace Server.Model.Object.Task
{
    class LivingObjectTask
    {
        private LivingObject taskOwner;

        public LivingObject TaskOwner
        {
            get { return taskOwner; }
            set { taskOwner = value; }
        }

        private int priority;

        public int Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public LivingObjectTask(LivingObject _TaskOwner, int _Priority)
        {
            this.taskOwner = _TaskOwner;
            this.priority = _Priority;
        }

        public virtual bool wantToDoTask(World _World)
        {
            return false;
        }

        public virtual void update(World _World)
        {
        }
    }
}
