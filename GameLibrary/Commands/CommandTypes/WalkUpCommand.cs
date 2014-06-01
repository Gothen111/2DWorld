using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Object;

namespace GameLibrary.Commands.CommandTypes
{
    public class WalkUpCommand : Command
    {
        private LivingObject walkActor;

        public LivingObject WalkActor
        {
            get { return walkActor; }
            set { walkActor = value; }
        }

        public WalkUpCommand(LivingObject _walkActor)
        {
            this.walkActor = _walkActor;
        }

        public override void doCommand()
        {
            GameLibrary.Configuration.Configuration.commandManager.handleWalkUpCommand(walkActor);
        }

        public override void stopCommand()
        {
            GameLibrary.Configuration.Configuration.commandManager.stopWalkUpCommand(walkActor);
        }
    }
}
