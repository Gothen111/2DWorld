using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using Lidgren.Network;

using Server.Connection.Message;

namespace Server.Connection
{
    class ServerMessageManager
    {
        public static ServerMessageManager serverMessageManager;

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
        private void ProcessNetworkMessages()
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

                                this.OnClientConnectToServer(im.SenderEndPoint);

                                break;
                            case NetConnectionStatus.Disconnected:
                                Console.WriteLine("{0} Disconnected", im.SenderEndPoint);

                                this.OnClientDisconnectFromServer(im.SenderEndPoint);

                                break;
                            case NetConnectionStatus.RespondedAwaitingApproval:
                                im.SenderConnection.Approve();
                                break;
                        }
                        break;
                    case NetIncomingMessageType.Data:
                        var gameMessageType = (EIGameMessageType)im.ReadByte();

                        ServerIGameMessageManager.serverIGameMessageManager.OnClientSendIGameMessage(gameMessageType, im);

                        break;
                }
                ServerNetworkManager.serverNetworkManager.Recycle(im);
            }
        }

        /// <summary>
        /// Bearbeitet falls ein Client Connected
        /// </summary>
        public void OnClientConnectToServer(IPEndPoint _IPEndPoint)
        {
        }

        /// <summary>
        /// Bearbeitet falls ein Client aus welchem Grund auch immer Disconnected
        /// </summary>
        public void OnClientDisconnectFromServer(IPEndPoint _IPEndPoint)
        {
        }
    }
}
