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

using Server.Connection.Message;

using Server.Factories;
using Server.Factories.FactoryEnums;
using Server.Model.Behaviour.Member;

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
            }
        }

        private static void handleRequestPlayerMessage(NetIncomingMessage _Im)
        {
            var message = new RequestPlayerMessage(_Im);

            var timeDelay = (float)(NetTime.Now - _Im.SenderConnection.GetLocalTime(message.MessageTime));

            Model.Object.PlayerObject var_PlayerObject = CreatureFactory.creatureFactory.createPlayerObject(RaceEnum.Human, FactionEnum.Castle_Test, CreatureEnum.Chieftain, GenderEnum.Male);
            var_PlayerObject.Position = new Vector3(0, 0, 0);
            var_PlayerObject.GraphicPath = "Character/Char1_Small";

            Model.Map.World.World.world.addPlayerObject(var_PlayerObject);

            Event.EventList.Add(new Event(new UpdatePlayerMessage(var_PlayerObject), GameMessageImportance.VeryImportant));
        }
    }
}
