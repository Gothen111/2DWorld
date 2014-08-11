using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Object;
using GameLibrary.Factory.FactoryEnums;
using Microsoft.Xna.Framework;

namespace GameLibrary.Factory
{
    public class EquipmentFactory
    {
        public static EquipmentFactory equipmentFactory = new EquipmentFactory();

        private EquipmentFactory()
        {
        }

        public EquipmentObject createEquipmentObject()
        {
            EquipmentObject equipmentObject = new EquipmentObject();
            equipmentObject.Scale = 1;
            equipmentObject.Velocity = new Vector3(0, 0, 0);

            return equipmentObject;
        }

        public GameLibrary.Model.Object.Equipment.EquipmentWeapon createEquipmentWeaponObject(WeaponEnum _WeaponEnum)
        {
            GameLibrary.Model.Object.Equipment.EquipmentWeapon equipmentWeaponObject = new GameLibrary.Model.Object.Equipment.EquipmentWeapon();
            equipmentWeaponObject.Scale = 1;
            equipmentWeaponObject.Velocity = new Vector3(0, 0, 0);
            equipmentWeaponObject.StackMax = 1;
            equipmentWeaponObject.Size = new Microsoft.Xna.Framework.Vector3(32, 32, 0);

            switch (_WeaponEnum)
            {
                case WeaponEnum.Sword:
                    {
                        equipmentWeaponObject.ItemEnum = ItemEnum.Weapon;
                        equipmentWeaponObject.NormalDamage = 2;
                        equipmentWeaponObject.WeaponEnum = _WeaponEnum;
                        equipmentWeaponObject.Range = 50;
                        equipmentWeaponObject.AttackSpeed = 0;
                        equipmentWeaponObject.AttackSpeedMax = 60;
                        equipmentWeaponObject.GraphicPath = "Object/Item/Small/Sword1";
                        equipmentWeaponObject.ItemIconGraphicPath = "Object/Item/Small/Sword1";
                        //equipmentWeaponObject.SearchFlags.Add(new GameLibrary.Model.Map.World.SearchFlags.());
                        break;
                    }
                case WeaponEnum.Spear:
                    {
                        equipmentWeaponObject.NormalDamage = 1;
                        equipmentWeaponObject.WeaponEnum = _WeaponEnum;
                        equipmentWeaponObject.Range = 80;
                        equipmentWeaponObject.AttackSpeed = 0;
                        equipmentWeaponObject.AttackSpeedMax = 30;
                        break;
                    }
                case WeaponEnum.Paper:
                    {
                        equipmentWeaponObject.NormalDamage = 5;
                        equipmentWeaponObject.WeaponEnum = _WeaponEnum;
                        equipmentWeaponObject.Range = 80;
                        equipmentWeaponObject.AttackSpeed = 0;
                        equipmentWeaponObject.AttackSpeedMax = 20;
                        break;
                    }
            }

            return equipmentWeaponObject;
        }

        public GameLibrary.Model.Object.Equipment.EquipmentArmor createEquipmentArmorObject(ArmorEnum _ArmorEnum)
        {
            GameLibrary.Model.Object.Equipment.EquipmentArmor equipmentArmorObject = new GameLibrary.Model.Object.Equipment.EquipmentArmor();
            equipmentArmorObject.Scale = 1;
            equipmentArmorObject.Velocity = new Vector3(0, 0, 0);
            equipmentArmorObject.StackMax = 1;
            equipmentArmorObject.Size = new Microsoft.Xna.Framework.Vector3(32, 32, 0);

            switch (_ArmorEnum)
            {
                case ArmorEnum.Chest:
                    {
                        equipmentArmorObject.NormalArmor = 5;
                        equipmentArmorObject.ArmorEnum = _ArmorEnum;
                        equipmentArmorObject.ItemIconGraphicPath = "Object/Item/Small/Cloth1";
                        equipmentArmorObject.GraphicPath = "Character/Cloth1";
                        break;
                    }
            }

            return equipmentArmorObject;
        }
    }
}
