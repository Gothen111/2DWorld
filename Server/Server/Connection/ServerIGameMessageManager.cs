using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using Lidgren.Network;

using Server.Connection.Message;

namespace Server.Connection
{
    class ServerIGameMessageManager
    {
        public static ServerIGameMessageManager serverIGameMessageManager = new ServerIGameMessageManager();

        public void OnClientSendIGameMessage(EIGameMessageType _EIGameMessageType, NetIncomingMessage _NetIncomingMessage)
        {
            var var_gameMessageType = _EIGameMessageType;
            switch (var_gameMessageType)
            {
               
            }
        }
    }
}
