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
    public class ClientNetworkManager : NetworkManager
    {
        private int connectionTry;
        private int timeOut;
        private int timeOutMax;

        private bool clientStarted;

        private NetClient netClient;
        private String ip;
        private String port;

        public ClientNetworkManager()
        {
            this.connectionTry = 1;
            this.timeOut = 0;
            this.timeOutMax = 1000;
            this.clientStarted = false;
        }

        public override void Start(String _Ip, String _Port)
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

        public override void Connect(String _Ip, String _Port)
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

        public override void Disconnect()
        {
            if (this.netClient != null)
            {
                this.clientStarted = false;
                this.netClient.Disconnect("");
            }
        }

        public override NetIncomingMessage ReadMessage()
        {
            return netClient.ReadMessage();
        }

        public override void SendMessage(IGameMessage gameMessage, GameMessageImportance _Importance)
        {
            NetOutgoingMessage om = netClient.CreateMessage();
            om.Write((byte)gameMessage.MessageType);
            gameMessage.Encode(om);

            netClient.SendMessage(om, _Importance == GameMessageImportance.VeryImportant ? NetDeliveryMethod.ReliableOrdered : NetDeliveryMethod.Unreliable); // ReliableUnordered
        }

        public override void update()
        {
            if (this.clientStarted)
            {
                //System.Console.WriteLine(this.netClient.ConnectionStatus.ToString());
                if (this.netClient.ConnectionStatus == NetConnectionStatus.Connected || this.netClient.ConnectionStatus == NetConnectionStatus.None)
                {
                    this.connectionTry = 1;
                    base.update();
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
            if (GameLibrary.Connection.NetworkManager.client != null)
            {
                switch (GameLibrary.Connection.NetworkManager.client.ClientStatus)
                {
                    case EClientStatus.Connected:
                        GameLibrary.Connection.NetworkManager.client.ClientStatus = EClientStatus.RequestPlayerPosition;
                        break;
                    case EClientStatus.RequestPlayerPosition:
                        GameLibrary.Connection.NetworkManager.client.ClientStatus = EClientStatus.RequestedPlayerPosition;
                        Event.EventList.Add(new Event(new RequestPlayerMessage("Fred"), GameMessageImportance.VeryImportant));
                        break;
                    case EClientStatus.RequestWorld:
                        GameLibrary.Connection.NetworkManager.client.ClientStatus = EClientStatus.RequestedWorld;
                        Event.EventList.Add(new Event(new RequestWorldMessage(), GameMessageImportance.VeryImportant));
                        break;
                    case EClientStatus.RequestRegion:
                        GameLibrary.Connection.NetworkManager.client.ClientStatus = EClientStatus.RequestedRegion;
                        Microsoft.Xna.Framework.Vector2 var_Position = new Microsoft.Xna.Framework.Vector2(GameLibrary.Connection.NetworkManager.client.PlayerObject.Position.X, GameLibrary.Connection.NetworkManager.client.PlayerObject.Position.Y);
                        Event.EventList.Add(new Event(new RequestRegionMessage(var_Position), GameMessageImportance.VeryImportant));
                        break;
                    case EClientStatus.RequestChunk:
                        GameLibrary.Connection.NetworkManager.client.ClientStatus = EClientStatus.RequestedChunk;
                        var_Position = new Microsoft.Xna.Framework.Vector2(GameLibrary.Connection.NetworkManager.client.PlayerObject.Position.X, GameLibrary.Connection.NetworkManager.client.PlayerObject.Position.Y);
                        Event.EventList.Add(new Event(new RequestChunkMessage(var_Position), GameMessageImportance.VeryImportant));
                        break;
                    case EClientStatus.JoinedWorld:
                        GameLibrary.Camera.Camera.camera.setTarget(GameLibrary.Connection.NetworkManager.client.PlayerObject);
                        //GameLibrary.Camera.Camera.camera.setTarget(GameLibrary.Model.Map.World.World.world.getLivingObject(0));
                        //GameLibrary.Camera.Camera.camera.setPosition(new Microsoft.Xna.Framework.Vector3(0, 0, 0));
                        break;
                }
            }
        }
    }
}
