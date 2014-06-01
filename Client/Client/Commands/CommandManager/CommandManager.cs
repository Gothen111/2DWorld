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

        private CommandManager() 
        { 
        }

        public void handleWalkUpCommand(LivingObject actor)
        {
            if (!actor.MoveUp)
            {
                Event.EventList.Add(new Event(new Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.WalkTopCommand), GameMessageImportance.VeryImportant));
            }
        }
        public void stopWalkUpCommand(LivingObject actor)
        {
            if (actor.MoveUp)
            {
                Event.EventList.Add(new Event(new Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.StopWalkTopCommand), GameMessageImportance.VeryImportant));
            }
        }

        public void handleWalkDownCommand(LivingObject actor)
        {
            if (!actor.MoveDown)
            {
                Event.EventList.Add(new Event(new Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.WalkDownCommand), GameMessageImportance.VeryImportant));
            }
        }
        public void stopWalkDownCommand(LivingObject actor)
        {
            if (actor.MoveDown)
            {
                Event.EventList.Add(new Event(new Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.StopWalkDownCommand), GameMessageImportance.VeryImportant));
            }
        }

        public void handleWalkLeftCommand(LivingObject actor)
        {
            if (!actor.MoveLeft)
            {
                Event.EventList.Add(new Event(new Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.WalkLeftCommand), GameMessageImportance.VeryImportant));
            }
        }
        public void stopWalkLeftCommand(LivingObject actor)
        {
            if (actor.MoveLeft)
            {
                Event.EventList.Add(new Event(new Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.StopWalkLeftCommand), GameMessageImportance.VeryImportant));
            }
        }

        public void handleWalkRightCommand(LivingObject actor)
        {
            if (!actor.MoveRight)
            {
                Event.EventList.Add(new Event(new Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.WalkRightCommand), GameMessageImportance.VeryImportant));
            }
        }
        public void stopWalkRightCommand(LivingObject actor)
        {
            if (actor.MoveRight)
            {
                Event.EventList.Add(new Event(new Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.StopWalkRightCommand), GameMessageImportance.VeryImportant));
            }
        }
    }
}
