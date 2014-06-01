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
    class ClientMessageManager
    {
        /// <summary>
        /// Bearbeitet Netzwerk Messages
        /// </summary>
        public static void ProcessNetworkMessages()
        {
            NetIncomingMessage im;
            while ((im = ClientNetworkManager.clientNetworkManager.ReadMessage()) != null)
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

                        ClientIGameMessageManager.OnClientSendIGameMessage(gameMessageType, im);

                        break;
                }
                ClientNetworkManager.clientNetworkManager.Recycle(im);
            }
        }

        /// <summary>
        /// Bearbeitet falls ein Client Connected
        /// </summary>
        public static void OnClientConnectToServer(IPEndPoint _IPEndPoint)
        {
            Event.EventList.Add(new Event(new RequestPlayerMessage("Fred"), GameMessageImportance.VeryImportant));
        }

        /// <summary>
        /// Bearbeitet falls ein Client aus welchem Grund auch immer Disconnected
        /// </summary>
        public static void OnClientDisconnectFromServer(IPEndPoint _IPEndPoint)
        {
        }
    }
}
