using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Object;
using GameLibrary.Commands.CommandTypes;
using GameLibrary.Connection;

namespace GameLibrary.Commands
{
    public abstract class CommandManager
    {
        public abstract void handleWalkUpCommand(LivingObject actor);
        public abstract void stopWalkUpCommand(LivingObject actor);

        public abstract void handleWalkDownCommand(LivingObject actor);
        public abstract void stopWalkDownCommand(LivingObject actor);

        public abstract void handleWalkLeftCommand(LivingObject actor);
        public abstract void stopWalkLeftCommand(LivingObject actor);

        public abstract void handleWalkRightCommand(LivingObject actor);
        public abstract void stopWalkRightCommand(LivingObject actor);

        public abstract void sendUpdateObjectPositionCommand(LivingObject actor);
    }
}
