﻿using System;
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

        public override void UpdateSendingEvents()
        {
            for (int i = 0; i < Event.EventList.Count; i++)
            {
                IGameMessage var_IGameMessage = Event.EventList[i].getIGameMessage();
                GameMessageImportance var_Importance = Event.EventList[i].getImportance();

                SendMessage(var_IGameMessage, var_Importance);

                Event.EventList.Remove(Event.EventList[i]);
                i -= 1;
            }
        }

        private void sendMessageToClientsInRange(IGameMessage _IGameMessage, GameMessageImportance _GameMessageImportance)
        {
            int var_Range = 1000;

            foreach (Client var_Client in NetworkManager.serverClients)
            {
                if (var_Client.PlayerObject != null)
                {
                    int var_Distance = (int) Math.Sqrt(Math.Pow(var_Client.PlayerObject.Position.X, 2) + Math.Pow(var_Client.PlayerObject.Position.Y, 2));
                    if (var_Distance <= var_Range)
                    {
                        this.SendMessageToClient(_IGameMessage, var_Client);
                    }
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
