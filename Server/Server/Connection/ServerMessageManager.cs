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
                                Console.WriteLine("{0} Connected", im.SenderEndPoint);

                                OnClientConnectToServer(im.SenderEndPoint);

                                break;
                            case NetConnectionStatus.Disconnected:
                                Console.WriteLine("{0} Disconnected", im.SenderEndPoint);

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
            GameLibrary.Connection.Event.EventList.Add(new GameLibrary.Connection.Event(new UpdateChunkMessage(GameLibrary.Model.Map.World.World.world.getRegion(0).getChunk(0)), GameLibrary.Connection.GameMessageImportance.VeryImportant));
            foreach (GameLibrary.Model.Collison.QuadTree<GameLibrary.Model.Object.LivingObject>.QuadNode var_QuadNode in GameLibrary.Model.Map.World.World.world.QuadTree.GetAllNodes())
            {
                foreach (GameLibrary.Model.Object.LivingObject var_LivingObject in var_QuadNode.Objects)
                {
                    GameLibrary.Connection.Event.EventList.Add(new GameLibrary.Connection.Event(new GameLibrary.Connection.Message.UpdateLivingObjectMessage(var_LivingObject), GameLibrary.Connection.GameMessageImportance.VeryImportant));
                }
            }
        }

        /// <summary>
        /// Bearbeitet falls ein Client aus welchem Grund auch immer Disconnected
        /// </summary>
        public static void OnClientDisconnectFromServer(IPEndPoint _IPEndPoint)
        {
        }
    }
}
