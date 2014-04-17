using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Factories.FactoryEnums;

namespace Server.Model.Object
{
    class EquipmentObject : AnimatedObject
    {
        private WeaponEnum weaponEnum;

        public WeaponEnum WeaponEnum
        {
            get { return weaponEnum; }
            set { weaponEnum = value; }
        }

        private int normalDamage;

        public int NormalDamage
        {
            get { return normalDamage; }
            set { normalDamage = value; }
        }

        public override void update()
        {
            base.update();
        }
    }
}
