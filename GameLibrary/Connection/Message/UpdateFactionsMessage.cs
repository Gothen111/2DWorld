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
    public class UpdateFactionsMessage : IGameMessage
    {
        #region Constructors and Destructors

        public UpdateFactionsMessage(NetIncomingMessage im)
        {
            this.Decode(im);
        }

        public UpdateFactionsMessage(List<GameLibrary.Model.Behaviour.Member.Faction> items)
        {
            this.MessageTime = NetTime.Now;
            this.factions = items;

            int var_CountRaces = this.factions.Count;
            this.value = new int[var_CountRaces, var_CountRaces];
            for (int y = 0; y < var_CountRaces; y++)
            {
                for (int x = 0; x < var_CountRaces; x++)
                {
                    this.value[y, x] = this.factions[y].BehaviourMember[x].Value;
                }
            }
        }

        #endregion

        #region Properties

        public double MessageTime { get; set; }

        public List<GameLibrary.Model.Behaviour.Member.Faction> factions { get; set; }

        public int[,] value { get; set; }

        #endregion

        #region Public Methods

        public EIGameMessageType MessageType
        {
            get { return EIGameMessageType.UpdateFactionsMessage; }
        }

        public void Decode(NetIncomingMessage im)
        {
            this.MessageTime = im.ReadDouble();
            int var_CountFactions = im.ReadInt32();

            this.factions = new List<Model.Behaviour.Member.Faction>();
            for (int i = 0; i < var_CountFactions; i++)
            {
                this.factions.Add(Utility.Serializer.DeserializeObjectFromString<Model.Behaviour.Member.Faction>(im.ReadString()));
            }
            this.value = new int[var_CountFactions, var_CountFactions];
            for (int y = 0; y < var_CountFactions; y++)
            {
                for (int x = 0; x < var_CountFactions; x++)
                {
                    this.value[y, x] = im.ReadInt32();
                    this.factions[y].BehaviourMember.Add(new BehaviourItem<Model.Behaviour.Member.Faction>(this.factions[x], this.value[y, x]));
                }
            }
        }

        public void Encode(NetOutgoingMessage om)
        {
            om.Write(this.MessageTime);
            int var_CountFactions = this.factions.Count;
            om.Write(var_CountFactions);
            for (int i = 0; i < var_CountFactions; i++)
            {
                om.Write(Utility.Serializer.SerializeObjectToString(this.factions[i]));
            }
            for (int y = 0; y < var_CountFactions; y++)
            {
                for (int x = 0; x < var_CountFactions; x++)
                {
                    om.Write(this.value[y,x]);
                }
            }
        }

        #endregion
    }
}
