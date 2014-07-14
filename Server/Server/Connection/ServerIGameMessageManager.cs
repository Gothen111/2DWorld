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
using GameLibrary.Configuration;

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
                case EIGameMessageType.RequestWorldMessage:
                    handleRequestWorldMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.RequestRegionMessage:
                    handleRequestRegionMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.PlayerCommandMessage:
                    handlePlayerCommandMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.RequestChunkMessage:
                    handleRequestChunkMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.RequestLivingObjectMessage:
                    handleRequestLivingObjectMessage(_NetIncomingMessage);
                    break;
            }
        }

        private static void handleRequestPlayerMessage(NetIncomingMessage _Im)
        {
            var message = new RequestPlayerMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            PlayerObject var_PlayerObject = CreatureFactory.creatureFactory.createPlayerObject(RaceEnum.Human, FactionEnum.Castle_Test, CreatureEnum.Chieftain, GenderEnum.Male);
            var_PlayerObject.Position = new Vector3(0, GameLibrary.Util.Random.GenerateGoodRandomNumber(0,100), 0);

            GameLibrary.Model.Map.World.World.world.addPlayerObject(var_PlayerObject);

            Client var_Client = Configuration.networkManager.getClient(_Im.SenderEndPoint);
            var_Client.PlayerObject = var_PlayerObject;
            Configuration.networkManager.SendMessageToClient(new UpdatePlayerMessage(var_PlayerObject), var_Client);

            GameLibrary.Camera.Camera.camera.setTarget(var_PlayerObject);
        }

        private static void handleRequestWorldMessage(NetIncomingMessage _Im)
        {
            var message = new RequestWorldMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            Client var_Client = Configuration.networkManager.getClient(_Im.SenderEndPoint);
            Configuration.networkManager.SendMessageToClient(new UpdateWorldMessage(GameLibrary.Model.Map.World.World.world), var_Client);
        }

        private static void handleRequestRegionMessage(NetIncomingMessage _Im)
        {
            var message = new RequestRegionMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            Client var_Client = Configuration.networkManager.getClient(_Im.SenderEndPoint);

            GameLibrary.Model.Map.Chunk.Chunk var_Chunk = GameLibrary.Model.Map.World.World.world.getChunkAtPosition(message.Position.X, message.Position.Y);

            if (var_Chunk != null)
            {
                Configuration.networkManager.SendMessageToClient(new UpdateRegionMessage((GameLibrary.Model.Map.Region.Region)var_Chunk.Parent), var_Client);
            }
            else
            {
                GameLibrary.Logger.Logger.LogErr("handleRequestChunkMessage: Chunk an Position X: " + message.Position.X + " Y: " + message.Position.Y + " ist null");
            }
        }

        private static void handleRequestChunkMessage(NetIncomingMessage _Im)
        {
            var message = new RequestChunkMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            Client var_Client = Configuration.networkManager.getClient(_Im.SenderEndPoint);

            GameLibrary.Model.Map.Chunk.Chunk var_Chunk = GameLibrary.Model.Map.World.World.world.getChunkAtPosition(message.Position.X, message.Position.Y);

            GameLibrary.Logger.Logger.LogDeb("Client Requested Chunk at X: " + message.Position.X + " Y: " + message.Position.Y);

            if (var_Chunk != null)
            {
                Configuration.networkManager.SendMessageToClient(new UpdateRegionMessage((GameLibrary.Model.Map.Region.Region)var_Chunk.Parent), var_Client);
                Configuration.networkManager.SendMessageToClient(new UpdateChunkMessage(var_Chunk), var_Client);
            }
            else
            {
                GameLibrary.Logger.Logger.LogErr("handleRequestChunkMessage: Chunk an Position X: " + message.Position.X + " Y: " + message.Position.Y + " ist null");
            }
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
                case GameLibrary.Commands.ECommandType.AttackCommand:
                    GameLibrary.Configuration.Configuration.commandManager.handleAttackCommand(var_PlayerObject);
                    break;
            }
        }
        private static void handleRequestLivingObjectMessage(NetIncomingMessage _Im)
        {
            var message = new RequestLivingObjectMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            Client var_Client = Configuration.networkManager.getClient(_Im.SenderEndPoint);

            GameLibrary.Model.Object.LivingObject var_LivingObject = (GameLibrary.Model.Object.LivingObject) GameLibrary.Model.Map.World.World.world.getObject(message.Id);
            if (var_LivingObject != null)
            {
                Configuration.networkManager.SendMessageToClient(new UpdateLivingObjectMessage(var_LivingObject), var_Client);
            }
            else
            {
                GameLibrary.Logger.Logger.LogErr("ServerIGameMessageManager->handleRequestLivingObjectMessage(...) LivingObject mit Id " + message.Id + " existiert nicht!");
            }
        }
    }
}
