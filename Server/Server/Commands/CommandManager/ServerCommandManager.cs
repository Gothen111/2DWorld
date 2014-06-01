using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Object;
using GameLibrary.Model.Object.Task.Tasks;
using GameLibrary.Commands.CommandTypes;

using GameLibrary.Commands;

namespace Server.Commands
{
    class ServerCommandManager : CommandManager
    {
        public override void handleWalkUpCommand(LivingObject actor)
        {
            actor.MoveUp = true;
        }
        public override void stopWalkUpCommand(LivingObject actor)
        {
            actor.MoveUp = false;
        }

        public override void handleWalkDownCommand(LivingObject actor)
        {
            actor.MoveDown = true;
        }
        public override void stopWalkDownCommand(LivingObject actor)
        {
            actor.MoveDown = false;
        }

        public override void handleWalkLeftCommand(LivingObject actor)
        {
            actor.MoveLeft = true;
        }
        public override void stopWalkLeftCommand(LivingObject actor)
        {
            actor.MoveLeft = false;
        }

        public override void handleWalkRightCommand(LivingObject actor)
        {
            actor.MoveRight = true;
        }
        public override void stopWalkRightCommand(LivingObject actor)
        {
            actor.MoveRight = false;
        }
    }
}
