﻿using System;
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

        public void handleAttackCommand(LivingObject actor)
        {
            actor.Tasks.Add(new AttackTask(actor, CommandPriority.Attack));
        }
    }
}