using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Object;

namespace Server.Commands.CommandTypes
{
    class InteractCommand : Command
    {
        private LivingObject interactionActor;

        public LivingObject InteractionActor
        {
            get { return interactionActor; }
            set { interactionActor = value; }
        }

        public InteractCommand(LivingObject _InteractionActor)
        {
            this.Actor = CommandManager.commandManager;
            this.interactionActor = _InteractionActor;
        }

        public override void doCommand()
        {
            ((CommandManager)Actor).handleInteractCommand(interactionActor);
        }

        public override void stopCommand()
        {
        }
    }
}
