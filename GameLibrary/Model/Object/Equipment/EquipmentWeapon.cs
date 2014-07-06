using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using GameLibrary.Factory.FactoryEnums;

namespace GameLibrary.Model.Object.Equipment
{
    [Serializable()]
    public class EquipmentWeapon : EquipmentObject
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

        private List<Map.World.SearchFlags.Searchflag> searchFlags;

        public List<Map.World.SearchFlags.Searchflag> SearchFlags
        {
            get { return searchFlags; }
            set { searchFlags = value; }
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

        public EquipmentWeapon()
        {
            searchFlags = new List<Map.World.SearchFlags.Searchflag>();
        }

        public EquipmentWeapon(SerializationInfo info, StreamingContext ctxt)
            : base(info, ctxt)
        {
            this.weaponEnum = (WeaponEnum)info.GetValue("weaponEnum", typeof(WeaponEnum));

            this.normalDamage = (int)info.GetValue("normalDamage", typeof(int));
            this.range = (int)info.GetValue("range", typeof(int));
            this.attackSpeed = (float)info.GetValue("attackSpeed", typeof(float));
            this.attackSpeedMax = (float)info.GetValue("attackSpeedMax", typeof(float));

            this.searchFlags = (List<Map.World.SearchFlags.Searchflag>)info.GetValue("searchFlags", typeof(List<Map.World.SearchFlags.Searchflag>));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("weaponEnum", weaponEnum, typeof(WeaponEnum));

            info.AddValue("normalDamage", normalDamage, typeof(int));
            info.AddValue("range", range, typeof(int));
            info.AddValue("attackSpeed", range, typeof(float));
            info.AddValue("attackSpeedMax", range, typeof(float));

            info.AddValue("searchFlags", this.searchFlags, typeof(List<Map.World.SearchFlags.Searchflag>));

            base.GetObjectData(info, ctxt);
        }

        public override void update()
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
