using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using Lidgren.Network;

using GameLibrary.Connection.Message;
using GameLibrary.Connection;

namespace Client.Connection
{
    class ClientNetworkManager
    {
        public static ClientNetworkManager clientNetworkManager = new ClientNetworkManager();

        private int connectionTry;
        private int timeOut;
        private int timeOutMax;

        private bool clientStarted;

        private NetClient netClient;
        private String ip;
        private String port;

        private ClientNetworkManager()
        {
            this.connectionTry = 1;
            this.timeOut = 0;
            this.timeOutMax = 1000;
            this.clientStarted = false;
        }

        public void Start(String _Ip, String _Port)
        {
            this.ip = _Ip;
            this.port = _Port;
            this.clientStarted = true;

            var config = new NetPeerConfiguration("2DWorld")
            {
                //SimulatedMinimumLatency = 0.2f,
                //SimulatedLoss = 0.1f
            };

            config.EnableMessageType(NetIncomingMessageType.WarningMessage);
            config.EnableMessageType(NetIncomingMessageType.VerboseDebugMessage);
            config.EnableMessageType(NetIncomingMessageType.ErrorMessage);
            config.EnableMessageType(NetIncomingMessageType.Error);
            config.EnableMessageType(NetIncomingMessageType.DebugMessage);
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);

            this.netClient = new NetClient(config);
            this.netClient.Start();

            this.Connect(this.ip, this.port);
        }

        public void Connect(String _Ip, String _Port)
        {
            if (this.netClient != null)
            {
                if (this.connectionTry >= 5)
                {
                    this.Disconnect();
                    this.netClient.Shutdown("");
                    GameLibrary.Logger.Logger.LogInfo("Abort!");
                }
                else
                {
                    GameLibrary.Logger.Logger.LogInfo("Connection Try : " + connectionTry);
                    this.netClient.Connect(new IPEndPoint(NetUtility.Resolve(_Ip), Convert.ToInt32(_Port)));
                    this.timeOut = this.timeOutMax * this.connectionTry;
                    this.connectionTry += 1;
                }
            }
        }

        public void Disconnect()
        {
            if (this.netClient != null)
            {
                this.clientStarted = false;
                this.netClient.Disconnect("");
            }
        }

        public NetIncomingMessage ReadMessage()
        {
            return netClient.ReadMessage();
        }

        public void Recycle(NetIncomingMessage im)
        {
        }

        public NetOutgoingMessage CreateMessage()
        {
            return null;
        }

        public void SendMessage(IGameMessage gameMessage, GameMessageImportance _Importance)
        {
            NetOutgoingMessage om = netClient.CreateMessage();
            om.Write((byte)gameMessage.MessageType);
            gameMessage.Encode(om);

            netClient.SendMessage(om, _Importance == GameMessageImportance.VeryImportant ? NetDeliveryMethod.ReliableOrdered : NetDeliveryMethod.Unreliable); // ReliableUnordered
        }

        public void Dispose()
        {
        }

        private void UpdateSendingEvents()
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
            if (this.clientStarted)
            {
                //System.Console.WriteLine(this.netClient.ConnectionStatus.ToString());
                if (this.netClient.ConnectionStatus == NetConnectionStatus.Connected || this.netClient.ConnectionStatus == NetConnectionStatus.None)
                {
                    this.connectionTry = 1;
                    this.UpdateSendingEvents();
                    ClientMessageManager.ProcessNetworkMessages();
                    this.checkClientStatus();
                }
                else
                {
                    if (this.timeOut <= 0)
                    {
                        this.Connect(this.ip, this.port);
                    }
                    else
                    {
                        this.timeOut -= 1;
                    }
                }
            }
        }

        public void checkClientStatus()
        {
            if (GameLibrary.Connection.Client.client != null)
            {
                switch (GameLibrary.Connection.Client.client.ClientStatus)
                {
                    case EClientStatus.Connected:
                        GameLibrary.Connection.Client.client.ClientStatus = EClientStatus.RequestWorld;
                        break;
                    case EClientStatus.RequestPlayerPosition:
                        GameLibrary.Connection.Client.client.ClientStatus = EClientStatus.RequestedPlayerPosition;
                        Event.EventList.Add(new Event(new RequestPlayerMessage("Fred"), GameMessageImportance.VeryImportant));
                        break;
                }
            }
        }
    }
}
