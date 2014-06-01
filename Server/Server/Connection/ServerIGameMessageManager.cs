using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using Lidgren.Network;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

using GameLibrary.Connection;
using GameLibrary.Connection.Message;

using GameLibrary.Model.Map;
using GameLibrary.Model.Object;
using GameLibrary.Factory;
using GameLibrary.Factory.FactoryEnums;
using GameLibrary.Model.Behaviour.Member;

namespace Server.Connection
{
    class ServerIGameMessageManager
    {
        public static void OnClientSendIGameMessage(EIGameMessageType _EIGameMessageType, NetIncomingMessage _NetIncomingMessage)
        {
            var var_gameMessageType = _EIGameMessageType;
            switch (var_gameMessageType)
            {
                case EIGameMessageType.RequestPlayerMessage:
                    handleRequestPlayerMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.PlayerCommandMessage:
                    handlePlayerCommandMessage(_NetIncomingMessage);
                    break;
            }
        }

        private static void handleRequestPlayerMessage(NetIncomingMessage _Im)
        {
            var message = new RequestPlayerMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            PlayerObject var_PlayerObject = CreatureFactory.creatureFactory.createPlayerObject(RaceEnum.Human, FactionEnum.Castle_Test, CreatureEnum.Chieftain, GenderEnum.Male);
            var_PlayerObject.Position = new Vector3(0, 0, 0);
            var_PlayerObject.GraphicPath = "Character/Char1_Small";

            GameLibrary.Model.Map.World.World.world.addPlayerObject(var_PlayerObject);

            Event.EventList.Add(new Event(new UpdatePlayerMessage(var_PlayerObject), GameMessageImportance.VeryImportant));

            GameLibrary.Camera.Camera.camera.setTarget(var_PlayerObject);
        }

        private static void handlePlayerCommandMessage(NetIncomingMessage _Im)
        {
            var message = new PlayerCommandMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            GameLibrary.Model.Object.PlayerObject var_PlayerObject = GameLibrary.Model.Map.World.World.world.getPlayerObject(message.Id);

            switch(message.ECommandType)
            {
                /*case GameLibrary.Commands.ECommandType.WalkDownCommand:
                    GameLibrary.Commands.CommandManager.commandManager.handleWalkDownCommand(var_PlayerObject);
                    break;
                case Commands.ECommandType.WalkTopCommand:
                    GameLibrary.Commands.CommandManager.commandManager.handleWalkUpCommand(var_PlayerObject);
                    break;
                case Commands.ECommandType.WalkLeftCommand:
                    Commands.CommandManager.commandManager.handleWalkLeftCommand(var_PlayerObject);
                    break;
                case Commands.ECommandType.WalkRightCommand:
                    Commands.CommandManager.commandManager.handleWalkRightCommand(var_PlayerObject);
                    break;
                case Commands.ECommandType.StopWalkDownCommand:
                    Commands.CommandManager.commandManager.stopWalkDownCommand(var_PlayerObject);
                    break;
                case Commands.ECommandType.StopWalkTopCommand:
                    Commands.CommandManager.commandManager.stopWalkUpCommand(var_PlayerObject);
                    break;
                case Commands.ECommandType.StopWalkLeftCommand:
                    Commands.CommandManager.commandManager.stopWalkLeftCommand(var_PlayerObject);
                    break;
                case Commands.ECommandType.StopWalkRightCommand:
                    Commands.CommandManager.commandManager.stopWalkRightCommand(var_PlayerObject);
                    break;*/
            }
        }
    }
}
