using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using Lidgren.Network;

using GameLibrary.Connection.Message;
using GameLibrary.Connection;
using Microsoft.Xna.Framework.Input;

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
            if (GameLibrary.Connection.NetworkManager.client != null)
            {
                switch (GameLibrary.Connection.NetworkManager.client.ClientStatus)
                {
                    case EClientStatus.Connected:
                        GameLibrary.Connection.NetworkManager.client.ClientStatus = EClientStatus.RequestPlayerPosition;
                        break;
                    case EClientStatus.RequestPlayerPosition:
                        GameLibrary.Connection.NetworkManager.client.ClientStatus = EClientStatus.RequestedPlayerPosition;
                        Event.EventList.Add(new Event(new RequestPlayerMessage(GameLibrary.Connection.NetworkManager.client.PlayerObject), GameMessageImportance.VeryImportant));
                        break;
                    case EClientStatus.RequestWorld:
                        GameLibrary.Connection.NetworkManager.client.ClientStatus = EClientStatus.RequestedWorld;   
                        Event.EventList.Add(new Event(new RequestWorldMessage(), GameMessageImportance.VeryImportant));
                        break;
                    case EClientStatus.JoinWorld:
                        GameLibrary.Model.Map.World.World.world.addPlayerObject(GameLibrary.Connection.NetworkManager.client.PlayerObject);
                        GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.W }, new GameLibrary.Commands.CommandTypes.WalkUpCommand(GameLibrary.Connection.NetworkManager.client.PlayerObject)));
                        GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.S }, new GameLibrary.Commands.CommandTypes.WalkDownCommand(GameLibrary.Connection.NetworkManager.client.PlayerObject)));
                        GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.A }, new GameLibrary.Commands.CommandTypes.WalkLeftCommand(GameLibrary.Connection.NetworkManager.client.PlayerObject)));
                        GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.D }, new GameLibrary.Commands.CommandTypes.WalkRightCommand(GameLibrary.Connection.NetworkManager.client.PlayerObject)));
                        GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.Space }, new GameLibrary.Commands.CommandTypes.AttackCommand(GameLibrary.Connection.NetworkManager.client.PlayerObject)));
                        GameLibrary.Connection.NetworkManager.client.ClientStatus = EClientStatus.JoinedWorld;
                        break;
                    /*case EClientStatus.RequestRegion:
                        GameLibrary.Connection.NetworkManager.client.ClientStatus = EClientStatus.RequestedRegion;
                        Microsoft.Xna.Framework.Vector3 var_Position = new Microsoft.Xna.Framework.Vector3(GameLibrary.Connection.NetworkManager.client.PlayerObject.Position.X, GameLibrary.Connection.NetworkManager.client.PlayerObject.Position.Y, 0);
                        
                        /*Event.EventList.Add(new Event(new RequestRegionMessage(var_Position), GameMessageImportance.VeryImportant));
                        
                        Microsoft.Xna.Framework.Vector3 var_PositionRegion = GameLibrary.Model.Map.Region.Region.parsePosition(var_Position);

                        GameLibrary.Model.Map.Region.Region var_Region = new GameLibrary.Model.Map.Region.Region("", (int)var_PositionRegion.X, (int)var_PositionRegion.Y, GameLibrary.Model.Map.Region.RegionEnum.Grassland, GameLibrary.Model.Map.World.World.world);
                        var_Region.IsRequested = true;
                        GameLibrary.Model.Map.World.World.world.addRegion(var_Region);*/

                        /*GameLibrary.Model.Map.World.World.world.createRegionAt(var_Position);
                        break;
                    case EClientStatus.RequestChunk:
                        GameLibrary.Connection.NetworkManager.client.ClientStatus = EClientStatus.RequestedChunk;
                        var_Position = new Microsoft.Xna.Framework.Vector3(GameLibrary.Connection.NetworkManager.client.PlayerObject.Position.X, GameLibrary.Connection.NetworkManager.client.PlayerObject.Position.Y, 0);
                        //Event.EventList.Add(new Event(new RequestChunkMessage(var_Position), GameMessageImportance.VeryImportant));

                        GameLibrary.Model.Map.Region.Region var_Region = GameLibrary.Model.Map.World.World.world.getRegionAtPosition(var_Position);
                        if (var_Region != null)
                        {
                            /*Microsoft.Xna.Framework.Vector3 var_PositionChunk = GameLibrary.Model.Map.Chunk.Chunk.parsePosition(var_Position);
                            GameLibrary.Model.Map.Chunk.Chunk var_Chunk = new GameLibrary.Model.Map.Chunk.Chunk("", (int)var_PositionChunk.X, (int)var_PositionChunk.Y, var_Region);
                            var_Chunk.IsRequested = true;
                            var_Region.setChunkAtPosition((int)var_PositionChunk.X, (int)var_PositionChunk.Y, var_Chunk);*/

                            /*GameLibrary.Model.Map.World.World.world.createChunkAt(var_Position);
                            
                            GameLibrary.Model.Map.World.World.world.addPlayerObject(GameLibrary.Connection.NetworkManager.client.PlayerObject);

                            GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.W }, new GameLibrary.Commands.CommandTypes.WalkUpCommand(GameLibrary.Connection.NetworkManager.client.PlayerObject)));
                            GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.S }, new GameLibrary.Commands.CommandTypes.WalkDownCommand(GameLibrary.Connection.NetworkManager.client.PlayerObject)));
                            GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.A }, new GameLibrary.Commands.CommandTypes.WalkLeftCommand(GameLibrary.Connection.NetworkManager.client.PlayerObject)));
                            GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.D }, new GameLibrary.Commands.CommandTypes.WalkRightCommand(GameLibrary.Connection.NetworkManager.client.PlayerObject)));
                            GameLibrary.Model.Player.PlayerContoller.playerContoller.addInputAction(new GameLibrary.Model.Player.InputAction(new List<Keys>() { Keys.Space }, new GameLibrary.Commands.CommandTypes.AttackCommand(GameLibrary.Connection.NetworkManager.client.PlayerObject)));
                            GameLibrary.Connection.NetworkManager.client.ClientStatus = EClientStatus.JoinedWorld;
                        }
                        break;
                    /*case EClientStatus.RequestBlock:
                        GameLibrary.Connection.NetworkManager.client.ClientStatus = EClientStatus.RequestedBlock;
                        var_Position = new Microsoft.Xna.Framework.Vector2(GameLibrary.Connection.NetworkManager.client.PlayerObject.Position.X, GameLibrary.Connection.NetworkManager.client.PlayerObject.Position.Y);
                        Event.EventList.Add(new Event(new RequestBlockMessage(var_Position), GameMessageImportance.VeryImportant));
                        Console.WriteLine("4");
                        break;*/
                    case EClientStatus.JoinedWorld:
                        GameLibrary.Gui.MenuManager.menuManager.setMenu(new GameLibrary.Gui.Menu.GameSurface());
                        GameLibrary.Camera.Camera.camera.setTarget(GameLibrary.Connection.NetworkManager.client.PlayerObject);
                        GameLibrary.Connection.NetworkManager.client.ClientStatus = EClientStatus.InWorld;
                        break;
                    case EClientStatus.InWorld:
                        break;
                }
            }
        }
    }
}
