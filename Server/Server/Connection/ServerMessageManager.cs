using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using Lidgren.Network;

using Server.Connection;
using GameLibrary.Connection.Message;

namespace Server.Connection
{
    class ServerMessageManager
    {
        /// <summary>
        /// Update für jeden Tick
        /// </summary>
        public void Update()
        {
            ProcessNetworkMessages();
        }

        /// <summary>
        /// Bearbeitet Netzwerk Messages
        /// </summary>
        public static void ProcessNetworkMessages()
        {
            NetIncomingMessage im;
            while ((im = ServerNetworkManager.serverNetworkManager.ReadMessage()) != null)
            {
                switch (im.MessageType)
                {
                    case NetIncomingMessageType.VerboseDebugMessage:
                    case NetIncomingMessageType.DebugMessage:
                    case NetIncomingMessageType.WarningMessage:
                    case NetIncomingMessageType.ErrorMessage:
                        Console.WriteLine(im.ReadString());
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        switch ((NetConnectionStatus)im.ReadByte())
                        {
                            case NetConnectionStatus.Connected:
                                GameLibrary.Logger.Logger.LogInfo(im.SenderEndPoint + " Connected");

                                OnClientConnectToServer(im.SenderEndPoint);

                                break;
                            case NetConnectionStatus.Disconnected:
                                GameLibrary.Logger.Logger.LogInfo(im.SenderEndPoint + " Disconnected");

                                OnClientDisconnectFromServer(im.SenderEndPoint);

                                break;
                            case NetConnectionStatus.RespondedAwaitingApproval:
                                im.SenderConnection.Approve();
                                break;
                        }
                        break;
                    case NetIncomingMessageType.Data:
                        var gameMessageType = (EIGameMessageType)im.ReadByte();

                        ServerIGameMessageManager.OnClientSendIGameMessage(gameMessageType, im);

                        break;
                }
                ServerNetworkManager.serverNetworkManager.Recycle(im);
            }
        }

        /// <summary>
        /// Bearbeitet falls ein Client Connected
        /// </summary>
        public static void OnClientConnectToServer(IPEndPoint _IPEndPoint)
        {
            //GameLibrary.Connection.Event.EventList.Add(new GameLibrary.Connection.Event(new UpdateChunkMessage(GameLibrary.Model.Map.World.World.world.getRegion(0).getChunk(0)), GameLibrary.Connection.GameMessageImportance.VeryImportant));
            Client var_Client = new Client(_IPEndPoint);
            ServerNetworkManager.serverNetworkManager.addClient(var_Client);
            ServerNetworkManager.serverNetworkManager.SendMessageToClient(new UpdateRegionMessage(GameLibrary.Model.Map.World.World.world.getRegion(0)), var_Client);
        }

        /// <summary>
        /// Bearbeitet falls ein Client aus welchem Grund auch immer Disconnected
        /// </summary>
        public static void OnClientDisconnectFromServer(IPEndPoint _IPEndPoint)
        {
            Client var_Client = ServerNetworkManager.serverNetworkManager.getClient(_IPEndPoint);
            ServerNetworkManager.serverNetworkManager.removeClient(var_Client);
        }
    }
}
