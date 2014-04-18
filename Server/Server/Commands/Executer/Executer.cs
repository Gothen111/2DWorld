using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Commands.CommandTypes;

namespace Server.Commands.Executer
{
    class Executer
    {
        public static Executer executer = new Executer();

        private List<Command<Object>> scheduledCommands;

        protected List<Command<Object>> ScheduledCommands
        {
            get { return scheduledCommands; }
            set { scheduledCommands = value; }
        }

        private Executer()
        {
            scheduledCommands = new List<Command<Object>>();
        }

        public void update(float delta)
        {
            while (scheduledCommands.Count > 0)
            {
                Command<Object> command = scheduledCommands.First();
                command.doCommand();
                scheduledCommands.RemoveAt(0);
            }
        }

        public void addCommand(Command<Object> command)
        {
            scheduledCommands.Add(command);
        }

        public void removeCommand(Command<Object> command)
        {
            removeCommand(getCommandIndex(command));
        }

        public void removeCommand(int index)
        {
            if (index != null && index >= 0)
                scheduledCommands.RemoveAt(index);
        }

        public int getCommandIndex(Command<Object> command)
        {
            return scheduledCommands.IndexOf(command);
        }
    }
}
