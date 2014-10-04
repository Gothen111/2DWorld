using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using Lidgren.Network;

using GameLibrary.Connection.Message;
using GameLibrary.Connection;
using Microsoft.Xna.Framework.Input;
using GameLibrary.Configuration;

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
            base.Start(_Ip, _Port);

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
            if (this.clientStarted && this.netClient != null)
            {
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
            if (this.client != null)
            {
                switch (this.client.ClientStatus)
                {
                    case EClientStatus.Connected:
                        this.client.ClientStatus = EClientStatus.RequestPlayerPosition;
                        break;
                    case EClientStatus.RequestPlayerPosition:
                        this.client.ClientStatus = EClientStatus.RequestedPlayerPosition;
                        Configuration.networkManager.addEvent(new RequestPlayerMessage(this.client.PlayerObject), GameMessageImportance.VeryImportant);
                        break;
                    case EClientStatus.RequestWorld:
                        this.client.ClientStatus = EClientStatus.RequestedWorld;   
                        Configuration.networkManager.addEvent(new RequestWorldMessage(), GameMessageImportance.VeryImportant);
                        break;
                    case EClientStatus.JoinWorld:
                        GameLibrary.Model.Map.World.World.world.addPlayerObject(this.client.PlayerObject);
                        GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.W }, new GameLibrary.Commands.CommandTypes.WalkUpCommand(this.client.PlayerObject)));
                        GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.S }, new GameLibrary.Commands.CommandTypes.WalkDownCommand(this.client.PlayerObject)));
                        GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.A }, new GameLibrary.Commands.CommandTypes.WalkLeftCommand(this.client.PlayerObject)));
                        GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.D }, new GameLibrary.Commands.CommandTypes.WalkRightCommand(this.client.PlayerObject)));
                        GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.Space }, new GameLibrary.Commands.CommandTypes.AttackCommand(this.client.PlayerObject)));
                        this.client.ClientStatus = EClientStatus.JoinedWorld;
                        break;
                    case EClientStatus.JoinedWorld:
                        GameLibrary.Gui.MenuManager.menuManager.setMenu(new GameLibrary.Gui.Menu.GameSurface());
                        GameLibrary.Camera.Camera.camera.setTarget(this.client.PlayerObject);
                        this.client.ClientStatus = EClientStatus.InWorld;
                        break;
                    case EClientStatus.InWorld:
                        break;
                }
            }
        }
    }
}
