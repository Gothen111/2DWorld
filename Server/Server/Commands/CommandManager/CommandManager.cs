using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Object;

namespace Server.Commands
{
    class CommandManager
    {
        public static CommandManager commandManager = new CommandManager();

        private CommandManager() { }

        public void handleAttackCommand(LivingObject actor, LivingObject target)
        {
            //TODO: Überarbeite LivingObject, damit es Attackspeed selber verwaltet
            //actor.attackLivingObject(target);
        }
    }
}
