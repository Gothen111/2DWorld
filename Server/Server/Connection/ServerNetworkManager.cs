using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using Lidgren.Network;

using GameLibrary.Connection;
using GameLibrary.Connection.Message;

namespace Server.Connection
{
    public class ServerNetworkManager : NetworkManager
    {
        private NetServer netServer;

        public override void Start(String _Ip, String _Port)
        {
 	        base.Start(_Ip, _Port);
            var config = new NetPeerConfiguration("2DWorld")
            {
                Port = Int32.Parse(_Port),//"14242"
                //SimulatedMinimumLatency = 0.2f,
                //SimulatedLoss = 0.1f
            };
            /*config.EnableMessageType(NetIncomingMessageType.WarningMessage);
            config.EnableMessageType(NetIncomingMessageType.VerboseDebugMessage);
            config.EnableMessageType(NetIncomingMessageType.ErrorMessage);
            config.EnableMessageType(NetIncomingMessageType.Error);
            config.EnableMessageType(NetIncomingMessageType.DebugMessage);
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);*/
            netServer = new NetServer(config);
            netServer.Start();

            NetworkManager.serverClients = new List<Client>();
        }

        public override NetIncomingMessage ReadMessage()
        {
            return netServer.ReadMessage();
        }

        public override void SendMessage(IGameMessage _IGameMessage, GameMessageImportance _Importance)
        {
            NetOutgoingMessage om = netServer.CreateMessage();
            om.Write((byte)_IGameMessage.MessageType);
            _IGameMessage.Encode(om);

            netServer.SendToAll(om, _Importance == GameMessageImportance.VeryImportant ? NetDeliveryMethod.ReliableOrdered : NetDeliveryMethod.Unreliable); // ReliableUnordered
        }

        public override void SendMessageToClient(IGameMessage _IGameMessage, Client _Client)
        {
            NetOutgoingMessage om = netServer.CreateMessage();
            om.Write((byte)_IGameMessage.MessageType);
            _IGameMessage.Encode(om);
            foreach (NetConnection connection in this.netServer.Connections)
            {
                if (connection.RemoteEndPoint == _Client.IPEndPoint)
                {
                    this.netServer.SendMessage(om, connection, NetDeliveryMethod.ReliableOrdered); // ReliableUnordered // Unreliable
                    break;
                }
            }
        }

        public override void update()
        {
            base.update();
            ServerMessageManager.ProcessNetworkMessages();
        }
    }
}
