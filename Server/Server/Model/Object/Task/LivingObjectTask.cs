using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Map.World;
using Server.Commands.CommandTypes;

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

        private CommandPriority priority;

        public CommandPriority Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public LivingObjectTask(LivingObject _TaskOwner, CommandPriority _Priority)
        {
            this.taskOwner = _TaskOwner;
            this.priority = _Priority;
        }

        public virtual bool wantToDoTask()
        {
            return false;
        }

        public virtual void update()
        {
        }
    }
}
