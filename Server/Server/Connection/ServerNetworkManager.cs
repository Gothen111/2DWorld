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
    class ServerNetworkManager
    {
        public static ServerNetworkManager serverNetworkManager = new ServerNetworkManager();
        private NetServer netServer;

        private List<Client> clients;

        public void Connect(int _Port)
        {
            var config = new NetPeerConfiguration("2DWorld")
            {
                Port = _Port,//"14242"
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

            this.clients = new List<Client>();
        }

        public void Disconnect()
        {
        }

        public NetIncomingMessage ReadMessage()
        {
            return netServer.ReadMessage();
        }

        public void Recycle(NetIncomingMessage im)
        {
        }

        public NetOutgoingMessage CreateMessage()
        {
            return null;
        }

        public void SendMessage(IGameMessage _IGameMessage, GameMessageImportance _Importance)
        {
            NetOutgoingMessage om = netServer.CreateMessage();
            om.Write((byte)_IGameMessage.MessageType);
            _IGameMessage.Encode(om);

            netServer.SendToAll(om, _Importance == GameMessageImportance.VeryImportant ? NetDeliveryMethod.ReliableOrdered : NetDeliveryMethod.Unreliable); // ReliableUnordered
        }

        public void SendMessageToClient(IGameMessage _IGameMessage, Client _Client)
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
        /*
        public void SendMessageToAllPlayerWithoutOne(IGameMessage _IGameMessage, System.Net.IPAddress _IpAdress)
        {
            NetOutgoingMessage om = netServer.CreateMessage();
            om.Write((byte)_IGameMessage.MessageType);
            _IGameMessage.Encode(om);
            foreach (NetConnection connection in this.netServer.Connections)
            {
                if (connection.RemoteEndPoint.Address == _IpAdress)
                {
                    break;
                }
                this.netServer.SendMessage(om, connection, NetDeliveryMethod.ReliableOrdered); // ReliableUnordered // Unreliable
            }
        }

        public Lidgren.Network.NetConnection GetLastConnection()
        {
            if (this.netServer.ConnectionsCount > 0)
                return this.netServer.Connections[this.netServer.ConnectionsCount - 1];
            return null;
        }*/

        public void Dispose()
        {
        }

        public void UpdateSendingEvents()
        {
            for (int i = 0; i < Event.EventList.Count; i++)
            {
                IGameMessage var_IGameMessage = Event.EventList[i].getIGameMessage();
                GameMessageImportance var_Importance= Event.EventList[i].getImportance();

                SendMessage(var_IGameMessage, var_Importance);

                Event.EventList.Remove(Event.EventList[i]);
                i -= 1;
            }
        }

        public void update()
        {
            UpdateSendingEvents();
            ServerMessageManager.ProcessNetworkMessages();
        }

        public void addClient(Client _Client)
        {
            this.clients.Add(_Client);
        }

        public void removeClient(Client _Client)
        {
            this.clients.Remove(_Client);
        }

        public void setClientPlayerObject()
        {

        }

        public Client getClient(IPEndPoint _IPEndPoint)
        {
            foreach (Client var_Client in clients)
            {
                if(var_Client.IPEndPoint.Equals(_IPEndPoint))
                {
                    return var_Client;
                }
            }
            return null;
        }
    }
}
