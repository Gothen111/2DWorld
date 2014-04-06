using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lidgren.Network;

using Server.Connection;

namespace Server.Connection.Message
{
    public interface IGameMessage
    {
        EIGameMessageType MessageType { get; }

        void Encode(NetOutgoingMessage om);

        void Decode(NetIncomingMessage im);
    }
}
