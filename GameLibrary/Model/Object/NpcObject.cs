using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace GameLibrary.Model.Object
{
    [Serializable()]
    public class NpcObject : FactionObject
    {
        public NpcObject() : base()
        {
            this.addEquipmentObject(GameLibrary.Factory.EquipmentFactory.equipmentFactory.createEquipmentWeaponObject(GameLibrary.Factory.FactoryEnums.WeaponEnum.Spear));
        }

        public NpcObject(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {

        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);
        }

        public override void update()
        {
            base.update();
        }
    }
}
