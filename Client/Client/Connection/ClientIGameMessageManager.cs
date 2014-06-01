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

using Client.Connection.Message;

using Client.Factories;
using Client.Factories.FactoryEnums;
using Client.Model.Behaviour.Member;


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
            Model.Map.World.World.world.getRegion(message.RegionId).setChunkAtPosition(0, 0, message.Chunk);
        }

        private static void handleUpdatePlayerMessage(NetIncomingMessage _Im)
        {
            var message = new UpdatePlayerMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            Model.Object.PlayerObject.playerObject = CreatureFactory.creatureFactory.createPlayerObject(RaceEnum.Human, FactionEnum.Castle_Test, CreatureEnum.Chieftain, GenderEnum.Male);
            Model.Object.PlayerObject.playerObject.Id = message.Id;
            Model.Object.PlayerObject.playerObject.Position = new Vector3(0, 0, 0);  
            Model.Object.PlayerObject.playerObject.GraphicPath = "Character/Char1_Small";
            Model.Map.World.World.world.addPlayerObject(Model.Object.PlayerObject.playerObject);

            Model.Player.PlayerContoller.playerContoller.addInputAction(new Model.Player.InputAction(new List<Keys>() { Keys.W }, new Commands.CommandTypes.WalkUpCommand(Model.Object.PlayerObject.playerObject)));
            Model.Player.PlayerContoller.playerContoller.addInputAction(new Model.Player.InputAction(new List<Keys>() { Keys.S }, new Commands.CommandTypes.WalkDownCommand(Model.Object.PlayerObject.playerObject)));
            Model.Player.PlayerContoller.playerContoller.addInputAction(new Model.Player.InputAction(new List<Keys>() { Keys.A }, new Commands.CommandTypes.WalkLeftCommand(Model.Object.PlayerObject.playerObject)));
            Model.Player.PlayerContoller.playerContoller.addInputAction(new Model.Player.InputAction(new List<Keys>() { Keys.D }, new Commands.CommandTypes.WalkRightCommand(Model.Object.PlayerObject.playerObject)));

            Camera.Camera.camera.setTarget(Model.Object.PlayerObject.playerObject);
        }

        private static void handleUpdateLivingObjectMessage(NetIncomingMessage _Im)
        {
            var message = new UpdateLivingObjectMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            Model.Object.LivingObject var_LivingObject = Model.Map.World.World.world.getLivingObject(message.Id) ?? Model.Map.World.World.world.addLivingObject(CreatureFactory.creatureFactory.createNpcObject(message.Id, RaceEnum.Human, FactionEnum.Castle_Test, CreatureEnum.Chieftain, GenderEnum.Male));
            var_LivingObject.MoveUp = message.MoveUp;
            var_LivingObject.MoveDown = message.MoveDown;
            var_LivingObject.MoveLeft = message.MoveLeft;
            var_LivingObject.MoveRight = message.MoveRight;
        }
    }
}
