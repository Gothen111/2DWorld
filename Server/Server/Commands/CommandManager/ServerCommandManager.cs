using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Object;
using GameLibrary.Commands.CommandTypes;
using GameLibrary.Model.Object.Task.Tasks;
using GameLibrary.Connection;
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

        public override void sendUpdateObjectPositionCommand(LivingObject actor)
        {
            Event.EventList.Add(new Event(new GameLibrary.Connection.Message.UpdateObjectPositionMessage(actor), GameMessageImportance.VeryImportant));
        }

        public override void sendUpdateObjectHealthCommand(LivingObject actor)
        {
            Event.EventList.Add(new Event(new GameLibrary.Connection.Message.UpdateObjectHealthMessage(actor), GameMessageImportance.VeryImportant));
        }

        public override void handleAttackCommand(LivingObject actor)
        {
            if (actor is CreatureObject)
                (actor as CreatureObject).attack();
        }

        public override void sendUpdateChunkCommand(GameLibrary.Model.Map.Chunk.Chunk chunk)
        {
            Event.EventList.Add(new Event(new GameLibrary.Connection.Message.UpdateChunkMessage(chunk), GameMessageImportance.VeryImportant));
        }
    }
}
