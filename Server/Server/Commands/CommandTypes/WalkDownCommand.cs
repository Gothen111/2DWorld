using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Object;

namespace Server.Commands.CommandTypes
{
    class WalkDownCommand : Command
    {
        private LivingObject walkActor;

        public LivingObject WalkActor
        {
            get { return walkActor; }
            set { walkActor = value; }
        }

        public WalkDownCommand(LivingObject _walkActor)
        {
            this.Actor = CommandManager.commandManager;
            this.walkActor = _walkActor;
        }

        public override void doCommand()
        {
            ((CommandManager)Actor).handleWalkDownCommand(walkActor);
        }

        public override void stopCommand()
        {
            ((CommandManager)Actor).stopWalkDownCommand(walkActor);
        }
    }
}
