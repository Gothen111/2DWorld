using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Lidgren.Network;
using Lidgren.Network.Xna;

using GameLibrary.Model.Object.Inventory;
using GameLibrary.Model.Object;

namespace GameLibrary.Connection.Message
{
    public class UpdateCreatureEquipmentMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdateCreatureEquipmentMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        //TODO: Lieber durch die equipment liste durch iterieren anstatt createobject
        public UpdateCreatureEquipmentMessage(int _Id, CreatureObject _CreatureObject)
        {
            this.MessageTime = NetTime.Now;
            this.Id = _Id;
            this.CreatureObject = _CreatureObject;
        }

        #endregion

        #region Properties

        public double MessageTime { get; set; }

        public int Id { get; set; }

        public CreatureObject CreatureObject { get; set; }


        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdateCreatureEquipmentMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.MessageTime = im.ReadDouble();
            this.Id = im.ReadInt32();
            this.CreatureObject = Util.Serializer.DeserializeObjectFromString<CreatureObject>(im.ReadString());
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.MessageTime);
            om.Write(this.Id);
            om.Write(Util.Serializer.SerializeObjectToString(this.CreatureObject));
        }

        #endregion
    }
}
