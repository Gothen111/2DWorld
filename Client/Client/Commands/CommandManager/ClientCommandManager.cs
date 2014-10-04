using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Object;
using GameLibrary.Commands.CommandTypes;
using GameLibrary.Connection;
using GameLibrary.Commands;
using GameLibrary.Configuration;

namespace Client.Commands
{
    public class ClientCommandManager : CommandManager
    {
        public override void handleWalkUpCommand(LivingObject actor)
        {
            if (!actor.MoveUp)
            {
                actor.MoveUp = true;
                Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.WalkTopCommand), GameMessageImportance.VeryImportant);
            }
        }
        public override void stopWalkUpCommand(LivingObject actor)
        {
            if (actor.MoveUp)
            {
                actor.MoveUp = false;
                Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.StopWalkTopCommand), GameMessageImportance.VeryImportant);
            }
        }

        public override void handleWalkDownCommand(LivingObject actor)
        {
            if (!actor.MoveDown)
            {
                actor.MoveDown = true;
                Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.WalkDownCommand), GameMessageImportance.VeryImportant);
            }
        }
        public override void stopWalkDownCommand(LivingObject actor)
        {
            if (actor.MoveDown)
            {
                actor.MoveDown = false;
                Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.StopWalkDownCommand), GameMessageImportance.VeryImportant);
            }
        }

        public override void handleWalkLeftCommand(LivingObject actor)
        {
            if (!actor.MoveLeft)
            {
                actor.MoveLeft = true;
                Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.WalkLeftCommand), GameMessageImportance.VeryImportant);
            }
        }
        public override void stopWalkLeftCommand(LivingObject actor)
        {
            if (actor.MoveLeft)
            {
                actor.MoveLeft = false;
                Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.StopWalkLeftCommand), GameMessageImportance.VeryImportant);
            }
        }

        public override void handleWalkRightCommand(LivingObject actor)
        {
            if (!actor.MoveRight)
            {
                actor.MoveRight = true;
                Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.WalkRightCommand), GameMessageImportance.VeryImportant);
            }
        }
        public override void stopWalkRightCommand(LivingObject actor)
        {
            if (actor.MoveRight)
            {
                actor.MoveRight = false;
                Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.StopWalkRightCommand), GameMessageImportance.VeryImportant);
            }
        }

        public override void handleAttackCommand(LivingObject actor)
        {
            actor.attack();//actor.attackLivingObject(null, 0); //TODO: Noch Response einbauen, dass Attackanimation nur dann gestartet wird, wenn ein Objekt getroffen wurde
            Configuration.networkManager.addEvent(new GameLibrary.Connection.Message.PlayerCommandMessage(actor as PlayerObject, ECommandType.AttackCommand), GameMessageImportance.VeryImportant);
        }
    }
}
