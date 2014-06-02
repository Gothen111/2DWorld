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
                case EIGameMessageType.UpdatePlayerMessage:
                    handleUpdatePlayerMessage(_NetIncomingMessage);
                    break;
                case EIGameMessageType.UpdateLivingObjectMessage:
                    handleUpdateLivingObjectMessage(_NetIncomingMessage);
                    break;

            }
        }

        private static void handleUpdateChunkMessage(NetIncomingMessage _Im)
        {
            var message = new UpdateChunkMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));
            GameLibrary.Model.Map.World.World.world.getRegion(message.RegionId).setChunkAtPosition(0, 0, message.Chunk);
        }

        private static void handleUpdatePlayerMessage(NetIncomingMessage _Im)
        {
            var message = new UpdatePlayerMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            GameLibrary.Model.Object.PlayerObject.playerObject = CreatureFactory.creatureFactory.createPlayerObject(RaceEnum.Human, FactionEnum.Castle_Test, CreatureEnum.Chieftain, GenderEnum.Male);
            GameLibrary.Model.Object.PlayerObject.playerObject.Id = message.Id;
            GameLibrary.Model.Object.PlayerObject.playerObject.Position = new Vector3(0, 0, 0);  
            GameLibrary.Model.Object.PlayerObject.playerObject.GraphicPath = "Character/Char1_Small";
            GameLibrary.Model.Map.World.World.world.addPlayerObject(GameLibrary.Model.Object.PlayerObject.playerObject);

            GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.W }, new GameLibrary.Commands.CommandTypes.WalkUpCommand(GameLibrary.Model.Object.PlayerObject.playerObject)));
            GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.S }, new GameLibrary.Commands.CommandTypes.WalkDownCommand(GameLibrary.Model.Object.PlayerObject.playerObject)));
            GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.A }, new GameLibrary.Commands.CommandTypes.WalkLeftCommand(GameLibrary.Model.Object.PlayerObject.playerObject)));
            GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.D }, new GameLibrary.Commands.CommandTypes.WalkRightCommand(GameLibrary.Model.Object.PlayerObject.playerObject)));

            GameLibrary.Camera.Camera.camera.setTarget(GameLibrary.Model.Object.PlayerObject.playerObject);
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
        }
    }
}
