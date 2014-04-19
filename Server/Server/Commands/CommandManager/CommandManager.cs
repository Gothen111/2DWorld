using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Object;
using Server.Model.Object.Task.Tasks;
using Server.Commands.CommandTypes;

namespace Server.Commands
{
    class CommandManager
    {
        public static CommandManager commandManager = new CommandManager();

        private CommandManager() { }

        public void handleAttackCommand(LivingObject actor, CommandPriority priority)
        {
            actor.Tasks.Add(new AttackRandomTask(actor, priority));
        }

        public void handleStandCommand(LivingObject actor, CommandPriority priority)
        {
            actor.Tasks.Add(new StandTask(actor, priority));
        }

        public void handleWalkRandomCommand(LivingObject actor, CommandPriority priority)
        {
            actor.Tasks.Add(new WalkRandomTask(actor, priority));
        }
    }
}
