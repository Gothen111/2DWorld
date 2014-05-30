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
            Model.Object.PlayerObject.playerObject.Position = new Vector3(0, 0, 0);
            Model.Object.PlayerObject.playerObject.GraphicPath = "Character/Char1_Small";
            Model.Map.World.World.world.addPlayerObject(Model.Object.PlayerObject.playerObject);
        }
    }
}
