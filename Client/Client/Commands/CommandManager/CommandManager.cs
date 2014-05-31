using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Client.Model.Object;
using Client.Commands.CommandTypes;
using Client.Connection;

namespace Client.Commands
{
    class CommandManager
    {
        public static CommandManager commandManager = new CommandManager();

        private CommandManager() { }

        public void handleWalkUpCommand(LivingObject actor)
        {
            actor.MoveUp = true;
            Event.EventList.Add(new Event(new Connection.Message.PlayerCommandMessage(actor as PlayerObject,ECommandType.WalkTopCommand), GameMessageImportance.VeryImportant));
        }
        public void stopWalkUpCommand(LivingObject actor)
        {
            actor.MoveUp = false;
        }

        public void handleWalkDownCommand(LivingObject actor)
        {
            actor.MoveDown = true;
            Event.EventList.Add(new Event(new Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.WalkDownCommand), GameMessageImportance.VeryImportant));
        }
        public void stopWalkDownCommand(LivingObject actor)
        {
            actor.MoveDown = false;
        }

        public void handleWalkLeftCommand(LivingObject actor)
        {
            actor.MoveLeft = true;
            Event.EventList.Add(new Event(new Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.WalkLeftCommand), GameMessageImportance.VeryImportant));
        }
        public void stopWalkLeftCommand(LivingObject actor)
        {
            actor.MoveLeft = false;
        }

        public void handleWalkRightCommand(LivingObject actor)
        {
            actor.MoveRight = true;
            Event.EventList.Add(new Event(new Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.WalkRightCommand), GameMessageImportance.VeryImportant));
        }
        public void stopWalkRightCommand(LivingObject actor)
        {
            actor.MoveRight = false;
        }
    }
}
