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
                        equipmentWeaponObject.NormalDamage = 4;
                        equipmentWeaponObject.WeaponEnum = _WeaponEnum;
                        Model.Object.Equipment.Attack.Attack var_Attack = new Model.Object.Equipment.Attack.Attack(50, 1.0f, 60.0f, Model.Object.Equipment.Attack.AttackType.Front);
                        equipmentWeaponObject.Attacks.Add(var_Attack);
                        equipmentWeaponObject.Body.MainBody.TexturePath = "Character/Sword";
                        equipmentWeaponObject.ItemIconGraphicPath = "Object/Item/Small/Sword1";
                        //equipmentWeaponObject.SearchFlags.Add(new GameLibrary.Model.Map.World.SearchFlags.());
                        break;
                    }
                case WeaponEnum.Spear:
                    {
                        equipmentWeaponObject.NormalDamage = 2;
                        equipmentWeaponObject.WeaponEnum = _WeaponEnum;
                        Model.Object.Equipment.Attack.Attack var_Attack = new Model.Object.Equipment.Attack.Attack(80, 1.0f, 30.0f, Model.Object.Equipment.Attack.AttackType.Front);
                        equipmentWeaponObject.Attacks.Add(var_Attack);
                        
                        break;
                    }
                case WeaponEnum.Paper:
                    {
                        equipmentWeaponObject.NormalDamage = 5;
                        equipmentWeaponObject.WeaponEnum = _WeaponEnum;
                        Model.Object.Equipment.Attack.Attack var_Attack = new Model.Object.Equipment.Attack.Attack(80, 1.0f, 20.0f, Model.Object.Equipment.Attack.AttackType.Front);
                        equipmentWeaponObject.Attacks.Add(var_Attack);
                        
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
            equipmentArmorObject.ArmorEnum = _ArmorEnum;

            switch (_ArmorEnum)
            {
                case ArmorEnum.Chest:
                    {
                        equipmentArmorObject.NormalArmor = 1;
                        equipmentArmorObject.ItemIconGraphicPath = "Object/Item/Small/Cloth1";
                        equipmentArmorObject.Body.MainBody.TexturePath = "Character/Cloth1";
                        break;
                    }
                case ArmorEnum.GoldenArmor:
                    equipmentArmorObject.NormalArmor = 10;
                        equipmentArmorObject.ItemIconGraphicPath = "Object/Item/Small/Cloth1";
                        equipmentArmorObject.Body.MainBody.TexturePath = "Character/GoldenArmor";
                    break;
            }

            return equipmentArmorObject;
        }
    }
}
