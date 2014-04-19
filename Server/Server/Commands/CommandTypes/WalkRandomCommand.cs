using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Object;

namespace Server.Commands.CommandTypes
{
    class WalkRandomCommand : Command
    {
        private LivingObject walkActor;

        public LivingObject WalkActor
        {
            get { return walkActor; }
            set { walkActor = value; }
        }

        public WalkRandomCommand(LivingObject _walkActor)
            : base(CommandPriority.Stand)
        {
            this.Actor = CommandManager.commandManager;
            this.walkActor = _walkActor;
        }

        public override void doCommand()
        {
            ((CommandManager)Actor).handleWalkRandomCommand(walkActor, this.Priority);
        }
    }
}
