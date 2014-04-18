using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Commands.CommandTypes
{
    abstract class Command<E>
    {
        private E actor;

        protected E Actor
        {
            get { return actor; }
            set { actor = value; }
        }

        private CommandPriority priority;

        public CommandPriority Priority
        {
            get { return priority; }
            set { priority = value; }
        }

        public Command(E _actor, CommandPriority _priority)
        {
            this.actor = _actor;
            this.priority = _priority;
        }

        public abstract void doCommand();
    }
}
