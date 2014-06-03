using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Object;

namespace GameLibrary.Commands.CommandTypes
{
    public class UpdateChunkCommand : Command
    {
        private Model.Map.Chunk.Chunk walkActor;

        public Model.Map.Chunk.Chunk WalkActor
        {
            get { return walkActor; }
            set { walkActor = value; }
        }

        public UpdateChunkCommand(Model.Map.Chunk.Chunk _walkActor)
        {
            this.walkActor = _walkActor;
        }

        public override void doCommand()
        {
            GameLibrary.Configuration.Configuration.commandManager.sendUpdateChunkCommand(walkActor);
        }

        public override void stopCommand()
        {
            
        }
    }
}
