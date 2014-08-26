using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;

namespace GameLibrary.Model.Object
{
    [Serializable()]
    public class PlayerObject : FactionObject
    {
        public PlayerObject() :base()
        {
            /*this.addEquipmentObject(GameLibrary.Factory.EquipmentFactory.equipmentFactory.createEquipmentArmorObject(GameLibrary.Factory.FactoryEnums.ArmorEnum.GoldenArmor));
            this.addEquipmentObject(GameLibrary.Factory.EquipmentFactory.equipmentFactory.createEquipmentWeaponObject(GameLibrary.Factory.FactoryEnums.WeaponEnum.Sword));

            this.getWeaponInHand().PositionInInventory = 0;
            this.getWearingArmor().PositionInInventory = 1;*/
        }

        public PlayerObject(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }

        public override void onChangedBlock()
        {
            base.onChangedBlock();
            if (Configuration.Configuration.isHost)
            {
            }
            else
            {
                //Request Blocks around!
            }
        }

        public override void onChangedChunk()
        {
            base.onChangedChunk();
            if (Configuration.Configuration.isHost)
            {
            }
            else
            {
                //Request Chunks around!
                //GameLibrary.Model.Map.World.World.world.checkPlayerObjectNeighbourChunks(this);       
            }
            GameLibrary.Model.Map.World.World.world.checkPlayerObjectNeighbourChunks(this);       
        }

        public override void update()
        {
            base.update();
            if (Configuration.Configuration.isHost)
            {
                //GameLibrary.Connection.Event.EventList.Add(new Connection.Event(new Connection.Message.UpdateLivingObjectMessage(this), Connection.GameMessageImportance.VeryImportant));
            }          
        }
    }
}
