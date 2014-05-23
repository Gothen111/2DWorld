using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Factories.FactoryEnums;

namespace Server.Model.Object.Equipment
{
    class EquipmentWeapon : EquipmentObject
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

        private int range;

        public int Range
        {
            get { return range; }
            set { range = value; }
        }

        private float attackSpeed;

        public float AttackSpeed
        {
            get { return attackSpeed; }
            set { attackSpeed = value; }
        }

        private float attackSpeedMax;

        public float AttackSpeedMax
        {
            get { return attackSpeedMax; }
            set { attackSpeedMax = value; }
        }

        public void update()
        {
            if (attackSpeed < attackSpeedMax)
            {
                attackSpeed++;
            }
        }

        public Boolean isAttackReady()
        {
            return attackSpeed >= attackSpeedMax;
        }

        public void attack()
        {
            attackSpeed = 0;
        }
    }
}
