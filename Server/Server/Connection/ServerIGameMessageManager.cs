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

            //Event.EventList.Add(new Event(new UpdatePlayerMessage(var_PlayerObject), GameMessageImportance.VeryImportant));
            Client var_Client = ServerNetworkManager.serverNetworkManager.getClient(_Im.SenderEndPoint);
            ServerNetworkManager.serverNetworkManager.addClient(var_Client);
            ServerNetworkManager.serverNetworkManager.SendMessageToClient(new UpdatePlayerMessage(var_PlayerObject), var_Client);

            GameLibrary.Camera.Camera.camera.setTarget(var_PlayerObject);
        }

        private static void handlePlayerCommandMessage(NetIncomingMessage _Im)
        {
            var message = new PlayerCommandMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            GameLibrary.Model.Object.PlayerObject var_PlayerObject = GameLibrary.Model.Map.World.World.world.getPlayerObject(message.Id);

            switch(message.ECommandType)
            {
                case GameLibrary.Commands.ECommandType.WalkDownCommand:
                    GameLibrary.Configuration.Configuration.commandManager.handleWalkDownCommand(var_PlayerObject);
                    break;
                case GameLibrary.Commands.ECommandType.WalkTopCommand:
                    GameLibrary.Configuration.Configuration.commandManager.handleWalkUpCommand(var_PlayerObject);
                    break;
                case GameLibrary.Commands.ECommandType.WalkLeftCommand:
                    GameLibrary.Configuration.Configuration.commandManager.handleWalkLeftCommand(var_PlayerObject);
                    break;
                case GameLibrary.Commands.ECommandType.WalkRightCommand:
                    GameLibrary.Configuration.Configuration.commandManager.handleWalkRightCommand(var_PlayerObject);
                    break;
                case GameLibrary.Commands.ECommandType.StopWalkDownCommand:
                    GameLibrary.Configuration.Configuration.commandManager.stopWalkDownCommand(var_PlayerObject);
                    break;
                case GameLibrary.Commands.ECommandType.StopWalkTopCommand:
                    GameLibrary.Configuration.Configuration.commandManager.stopWalkUpCommand(var_PlayerObject);
                    break;
                case GameLibrary.Commands.ECommandType.StopWalkLeftCommand:
                    GameLibrary.Configuration.Configuration.commandManager.stopWalkLeftCommand(var_PlayerObject);
                    break;
                case GameLibrary.Commands.ECommandType.StopWalkRightCommand:
                    GameLibrary.Configuration.Configuration.commandManager.stopWalkRightCommand(var_PlayerObject);
                    break;
            }
        }
    }
}
