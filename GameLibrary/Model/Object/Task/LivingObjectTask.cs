using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Map.World;
using GameLibrary.Commands.CommandTypes;

namespace GameLibrary.Model.Object.Task
{
    public class LivingObjectTask
    {
        private LivingObject taskOwner;

        public LivingObject TaskOwner
        {
            get { return taskOwner; }
            set { taskOwner = value; }
        }

        private Tasks.TaskPriority priority;

        public Tasks.TaskPriority Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public LivingObjectTask()
        {

        }

        public LivingObjectTask(LivingObject _TaskOwner, Tasks.TaskPriority _Priority)
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
