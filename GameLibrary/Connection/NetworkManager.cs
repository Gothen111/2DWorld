using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using Lidgren.Network;

using GameLibrary.Connection;
using GameLibrary.Connection.Message;
using GameLibrary.Model.Object;

namespace GameLibrary.Connection
{
    public class NetworkManager
    {
        public static Client client;
        public static List<Client> serverClients;

        public NetworkManager()
        {
        }

        public virtual void Start(String _Ip, String _Port)
        {
        }

        public virtual void Connect(String _Ip, String _Port)
        {
        }

        public virtual void Disconnect()
        {
        }

        public virtual NetIncomingMessage ReadMessage()
        {
            return null;
        }

        public virtual void Recycle(NetIncomingMessage im)
        {
        }

        public virtual NetOutgoingMessage CreateMessage()
        {
            return null;
        }

        public virtual void SendMessage(IGameMessage _IGameMessage, GameMessageImportance _Importance)
        {
        }

        public virtual void SendMessageToClient(IGameMessage _IGameMessage, Client _Client)
        {
        }

        public virtual void Dispose()
        {
        }

        public virtual void UpdateSendingEvents()
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

        public virtual void update()
        {
            this.UpdateSendingEvents();
        }

        public void addClient(Client _Client)
        {
            serverClients.Add(_Client);
        }

        public void removeClient(Client _Client)
        {
            serverClients.Remove(_Client);
        }

        public Client getClient(IPEndPoint _IPEndPoint)
        {
            foreach (Client var_Client in serverClients)
            {
                if (var_Client.IPEndPoint.Equals(_IPEndPoint))
                {
                    return var_Client;
                }
            }
            return null;
        }

        public Client getClient(PlayerObject _PlayerObject)
        {
            foreach (Client var_Client in serverClients)
            {
                if (var_Client.PlayerObject != null)
                {
                    if (var_Client.PlayerObject.Equals(_PlayerObject))
                    {
                        return var_Client;
                    }
                }
            }
            return null;
        }
    }
}
