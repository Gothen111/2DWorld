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

        /// <summary>Erzegt einen Clienten für den Server
        /// <para>IPEndPoint _IPEndPoint</para>
        /// </summary>
        public Client(IPEndPoint _IPEndPoint)
        {
            this.iPEndPoint = _IPEndPoint;
            this.clientStatus = EClientStatus.Connected;
        }

        /// <summary>Erzeugt den Clienten für den Clienten
        /// </summary>
        public Client()
        {
            this.clientStatus = EClientStatus.Connected;
        }
    }
}
