using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Object;

namespace Server.Commands.CommandTypes
{
    class StandCommand : Command
    {
        private LivingObject stopActor;

        public LivingObject StopActor
        {
          get { return stopActor; }
          set { stopActor = value; }
        }

        public StandCommand(LivingObject _attackActor) : base(CommandPriority.Stand)
        {
            this.Actor = CommandManager.commandManager;
            this.stopActor = _attackActor;
        }

        public override void doCommand()
        {
            ((CommandManager)Actor).handleStandCommand(stopActor, this.Priority);
        }
    }
}
