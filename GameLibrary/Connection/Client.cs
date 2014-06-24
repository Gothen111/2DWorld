using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using Lidgren.Network;

using GameLibrary.Model.Object;

namespace GameLibrary.Connection
{
    public class Client
    {
        public static Client client;

        IPEndPoint iPEndPoint;

        public IPEndPoint IPEndPoint
        {
            get { return iPEndPoint; }
            set { iPEndPoint = value; }
        }
        PlayerObject playerObject;

        public PlayerObject PlayerObject
        {
            get { return playerObject; }
            set { playerObject = value; }
        }

        private EClientStatus clientStatus;

        public EClientStatus ClientStatus
        {
            get { return clientStatus; }
            set { clientStatus = value; }
        }

        public Client(IPEndPoint _IPEndPoint)
        {
            this.iPEndPoint = _IPEndPoint;
            this.clientStatus = EClientStatus.Connected;
        }
    }
}
