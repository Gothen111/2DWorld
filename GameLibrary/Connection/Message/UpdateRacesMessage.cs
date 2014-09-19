using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Map.Region;
using GameLibrary.Model.Behaviour;
using Lidgren.Network;
using Lidgren.Network.Xna;

namespace GameLibrary.Connection.Message
{
    public class UpdateRacesMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdateRacesMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdateRacesMessage(List<GameLibrary.Model.Behaviour.Member.Race> items)
        {
            this.MessageTime = NetTime.Now;
        }

        #endregion

        #region Properties

        public double MessageTime { get; set; }

        public Array behaviourItems { get; set; }


        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdateBehaviourMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.MessageTime = im.ReadDouble();
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.MessageTime);
        }

        #endregion
    }
}
