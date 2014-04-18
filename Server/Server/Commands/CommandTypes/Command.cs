using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Server.Commands.CommandTypes
{
    abstract class Command
    {
        private Object actor;

        protected Object Actor
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

        public Command(CommandPriority _priority)
        {
            this.priority = _priority;
        }

        public abstract void doCommand();
    }
}
