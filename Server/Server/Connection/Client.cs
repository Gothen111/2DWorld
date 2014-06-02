using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Net;
using Lidgren.Network;

using GameLibrary.Model.Object;

namespace Server.Connection
{
    class Client
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

        public Client(IPEndPoint _IPEndPoint)
        {
            this.iPEndPoint = _IPEndPoint;
        }
    }
}
