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

using GameLibrary.Connection.Message;

using GameLibrary.Factory;
using GameLibrary.Factory.FactoryEnums;
using GameLibrary.Model.Behaviour.Member;


namespace Client.Connection
{
    class ClientIGameMessageManager
    {
        public static void OnClientSendIGameMessage(EIGameMessageType _EIGameMessageType, NetIncomingMessage _NetIncomingMessage)
        {
            var var_gameMessageType = _EIGameMessageType;
            switch (var_gameMessageType)
            {
                case EIGameMessageType.UpdateChunkMessage:
                    handleUpdateChunkMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.UpdateRegionMessage:
                    handleUpdateRegionMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.UpdatePlayerMessage:
                    handleUpdatePlayerMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.UpdateLivingObjectMessage:
                    handleUpdateLivingObjectMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.UpdateObjectPositionMessage:
                    handleUpdateObjectPositionMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.UpdateObjectHealthMessage:
                    handleUpdateObjectHealthMessage(_NetIncomingMessage);
                    break;

            }
        }

        private static void handleUpdateChunkMessage(NetIncomingMessage _Im)
        {
            var message = new UpdateChunkMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));
            GameLibrary.Model.Map.World.World.world.getRegion(message.RegionId).setChunkAtPosition((int)message.Chunk.Position.X, (int)message.Chunk.Position.Y, message.Chunk);
            foreach (GameLibrary.Model.Object.LivingObject var_LivingObject in message.Chunk.getAllObjectsInChunk())
            {
                if (GameLibrary.Model.Map.World.World.world.getLivingObject(var_LivingObject.Id) == null)
                    GameLibrary.Model.Map.World.World.world.addLivingObject(var_LivingObject);
            }
            if (GameLibrary.Model.Map.World.World.world.getChunkAtPosition(GameLibrary.Model.Object.PlayerObject.playerObject.Position.X, GameLibrary.Model.Object.PlayerObject.playerObject.Position.Y) != null)
            {
                GameLibrary.Model.Map.World.World.world.addPlayerObject(GameLibrary.Model.Object.PlayerObject.playerObject);
            }
        }

        private static void handleUpdateRegionMessage(NetIncomingMessage _Im)
        {
            var message = new UpdateRegionMessage(_Im);
            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            if (!GameLibrary.Model.Map.World.World.world.containsRegion(message.Region.Id))
            {
                message.Region.setAllNeighboursOfChunks();
                GameLibrary.Model.Map.World.World.world.addRegion(message.Region);
                /*foreach (GameLibrary.Model.Map.Chunk.Chunk var_Chunk in message.Region.Chunks)
                {
                    foreach (GameLibrary.Model.Object.LivingObject var_LivingObject in var_Chunk.getAllObjectsInChunk())
                    {
                        if (GameLibrary.Model.Map.World.World.world.getLivingObject(var_LivingObject.Id) == null)
                            GameLibrary.Model.Map.World.World.world.addLivingObject(var_LivingObject);
                    }
                }*/
            }
            else
            {
                GameLibrary.Logger.Logger.LogErr("Region sollte hinzugefügt werden, ist allerdings schon vorhanden -> Benutze UpdateChunkMessage");
            }
        }

        private static void handleUpdatePlayerMessage(NetIncomingMessage _Im)
        {
            var message = new UpdatePlayerMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            if (GameLibrary.Connection.Client.client.ClientStatus == GameLibrary.Connection.EClientStatus.RequestedPlayerPosition)
            {
                if (GameLibrary.Connection.Client.client.PlayerObject == null)
                {
                    GameLibrary.Connection.Client.client.PlayerObject = message.PlayerObject;
                    GameLibrary.Connection.Client.client.ClientStatus = GameLibrary.Connection.EClientStatus.RequestWorld;
                }
            }

            /*GameLibrary.Model.Object.PlayerObject.playerObject = message.PlayerObject;
            if (GameLibrary.Model.Map.World.World.world.getChunkAtPosition(GameLibrary.Model.Object.PlayerObject.playerObject.Position.X, GameLibrary.Model.Object.PlayerObject.playerObject.Position.Y) != null)
            {
                GameLibrary.Model.Map.World.World.world.addPlayerObject(GameLibrary.Model.Object.PlayerObject.playerObject);
            }
            else
            {
                GameLibrary.Connection.Event.EventList.Add(new GameLibrary.Connection.Event(new RequestChunkMessage(new Vector2(GameLibrary.Model.Object.PlayerObject.playerObject.Position.X, GameLibrary.Model.Object.PlayerObject.playerObject.Position.Y)), GameLibrary.Connection.GameMessageImportance.VeryImportant));
            }*/

            GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.W }, new GameLibrary.Commands.CommandTypes.WalkUpCommand(GameLibrary.Model.Object.PlayerObject.playerObject)));
            GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.S }, new GameLibrary.Commands.CommandTypes.WalkDownCommand(GameLibrary.Model.Object.PlayerObject.playerObject)));
            GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.A }, new GameLibrary.Commands.CommandTypes.WalkLeftCommand(GameLibrary.Model.Object.PlayerObject.playerObject)));
            GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.D }, new GameLibrary.Commands.CommandTypes.WalkRightCommand(GameLibrary.Model.Object.PlayerObject.playerObject)));
            GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.Space }, new GameLibrary.Commands.CommandTypes.AttackCommand(GameLibrary.Model.Object.PlayerObject.playerObject)));

            //GameLibrary.Camera.Camera.camera.setTarget(GameLibrary.Model.Object.PlayerObject.playerObject);
        }

        private static void handleUpdateLivingObjectMessage(NetIncomingMessage _Im)
        {
            var message = new UpdateLivingObjectMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            GameLibrary.Model.Object.LivingObject var_LivingObject = GameLibrary.Model.Map.World.World.world.getLivingObject(message.Id) ?? GameLibrary.Model.Map.World.World.world.addLivingObject(GameLibrary.Util.Serializer.DeserializeObjectFromString<GameLibrary.Model.Object.LivingObject>(message.Content));//CreatureFactory.creatureFactory.createNpcObject(message.Id, RaceEnum.Human, FactionEnum.Castle_Test, CreatureEnum.Chieftain, GenderEnum.Male));
            var_LivingObject.Position = var_LivingObject.Position;
            var_LivingObject.MoveUp = message.MoveUp;
            var_LivingObject.MoveDown = message.MoveDown;
            var_LivingObject.MoveLeft = message.MoveLeft;
            var_LivingObject.MoveRight = message.MoveRight;
            var_LivingObject.markAsDirty();
        }

        private static void handleUpdateObjectPositionMessage(NetIncomingMessage _Im)
        {
            var message = new UpdateObjectPositionMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            GameLibrary.Model.Object.LivingObject var_LivingObject = GameLibrary.Model.Map.World.World.world.getLivingObject(message.Id);
            if (var_LivingObject != null)
            {
                var_LivingObject.Position = message.Position;
                var_LivingObject.markAsDirty();
            }   
        }

        private static void handleUpdateObjectHealthMessage(NetIncomingMessage _Im)
        {
            var message = new UpdateObjectHealthMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            GameLibrary.Model.Object.LivingObject var_LivingObject = GameLibrary.Model.Map.World.World.world.getLivingObject(message.Id);
            if (var_LivingObject != null)
            {
                var_LivingObject.HealthPoints = message.Health;
                var_LivingObject.MaxHealthPoints = message.MaxHealth;
                var_LivingObject.damage(0);
                var_LivingObject.markAsDirty();
            }
            else
            {
                GameLibrary.Logger.Logger.LogErr("LivingObject mit Id: " + message.Id + " konnte nicht im Quadtree gefunden werden -> Health wird nicht geupdatet");
            }
        }
    }
}
