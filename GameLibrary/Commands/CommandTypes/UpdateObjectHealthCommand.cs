using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Object;

namespace GameLibrary.Commands.CommandTypes
{
    public class UpdateObjectHealthCommand : Command
    {
        private LivingObject walkActor;

        public LivingObject WalkActor
        {
            get { return walkActor; }
            set { walkActor = value; }
        }

        public UpdateObjectHealthCommand(LivingObject _walkActor)
        {
            this.walkActor = _walkActor;
        }

        public override void doCommand()
        {
            GameLibrary.Configuration.Configuration.commandManager.sendUpdateObjectHealthCommand(walkActor);
        }

        public override void stopCommand()
        {
            
        }
    }
}
