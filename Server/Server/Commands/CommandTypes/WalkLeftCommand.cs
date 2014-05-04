using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Object;

namespace Server.Commands.CommandTypes
{
    class WalkLeftCommand : Command
    {
        private LivingObject walkActor;

        public LivingObject WalkActor
        {
            get { return walkActor; }
            set { walkActor = value; }
        }

        public WalkLeftCommand(LivingObject _walkActor)
        {
            this.Actor = CommandManager.commandManager;
            this.walkActor = _walkActor;
        }

        public override void doCommand()
        {
            ((CommandManager)Actor).handleWalkLeftCommand(walkActor);
        }

        public override void stopCommand()
        {
            ((CommandManager)Actor).stopWalkLeftCommand(walkActor);
        }
    }
}
