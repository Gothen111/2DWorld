using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Object;
using GameLibrary.Factory.FactoryEnums;
using Microsoft.Xna.Framework;
using GameLibrary.Model.Object.Body;

namespace GameLibrary.Factory
{
    public class CreatureFactory
    {
        public static CreatureFactory creatureFactory = new CreatureFactory();

        private CreatureFactory()
        {
        }

        public PlayerObject createPlayerObject(RaceEnum objectRace, FactionEnum objectFaction, CreatureEnum objectType, GenderEnum objectGender)
        {
            PlayerObject playerObject = new PlayerObject();
            playerObject.Scale = 1;
            playerObject.Velocity = new Vector3(0, 0, 0);
            playerObject.Faction = BehaviourFactory.behaviourFactory.getFaction(objectFaction);
            playerObject.Race = BehaviourFactory.behaviourFactory.getRace(objectRace);
            playerObject.Gender = objectGender;
            playerObject.Name = NameFactory.getName(objectType, objectGender);

            switch (objectRace)
            {
                case RaceEnum.Ogre:
                    {
                        playerObject.Body = new BodyHuman();
                        ((BodyHuman)playerObject.Body).MainBody.TexturePath = "Character/Ogre1";
                        playerObject.Size = new Vector3(80, 96, 0);
                        //npcObject.CollisionBounds.Add(new Rectangle(17 + (int)npcObject.Size.X / 2, 69 + (int)npcObject.Size.Y / 2, 49, 25));
                        break;
                    }
                case RaceEnum.Human:
                    {
                        playerObject.Body = new BodyHuman();
                        switch (objectGender)
                        {
                            case GenderEnum.Male:
                                ((BodyHuman)playerObject.Body).MainBody.TexturePath = "Character/BodyMale";
                                break;
                            case GenderEnum.Female:
                                ((BodyHuman)playerObject.Body).MainBody.TexturePath = "Character/BodyFemale";
                                break;
                        }
                        break;
                    }
            }

            EquipmentObject var_EquipmentObject_Armor = GameLibrary.Factory.EquipmentFactory.equipmentFactory.createEquipmentArmorObject(GameLibrary.Factory.FactoryEnums.ArmorEnum.GoldenArmor);
            var_EquipmentObject_Armor.PositionInInventory = 0;

            playerObject.Body.setEquipmentObject(var_EquipmentObject_Armor);

            EquipmentObject var_EquipmentObject_Sword = GameLibrary.Factory.EquipmentFactory.equipmentFactory.createEquipmentWeaponObject(GameLibrary.Factory.FactoryEnums.WeaponEnum.Sword);
            var_EquipmentObject_Sword.PositionInInventory = 1;

            playerObject.Body.setEquipmentObject(var_EquipmentObject_Sword);
           
            return playerObject;
        }

        public PlayerObject createPlayerObject(PlayerObject _PlayerObject)
        {
            PlayerObject playerObject = this.createPlayerObject(RaceEnum.Human, FactionEnum.Beerdrinker, CreatureEnum.Archer, GenderEnum.Male);
            playerObject.Scale = _PlayerObject.Scale;
            playerObject.Velocity = new Vector3(0, 0, 0);
            playerObject.Faction = _PlayerObject.Faction;
            playerObject.Race = _PlayerObject.Race;
            playerObject.Gender = _PlayerObject.Gender;
            playerObject.Name = _PlayerObject.Name;
            playerObject.ObjectDrawColor = _PlayerObject.ObjectDrawColor;

            return playerObject;
        }

        public NpcObject createNpcObject(RaceEnum objectRace, FactionEnum objectFaction, CreatureEnum objectType, GenderEnum objectGender)
        {
            NpcObject npcObject = new NpcObject();
            npcObject.Scale = 1;
            npcObject.Velocity = new Vector3(0, 0, 0);
            npcObject.Faction = BehaviourFactory.behaviourFactory.getFaction(objectFaction);
            npcObject.Race = BehaviourFactory.behaviourFactory.getRace(objectRace);
            npcObject.Gender = objectGender;
            npcObject.Name = NameFactory.getName(objectType, objectGender);
            switch (objectRace)
            {
                case RaceEnum.Ogre:
                    {
                        npcObject.Body = new BodyHuman();
                        ((BodyHuman)npcObject.Body).MainBody.TexturePath = "Character/Ogre1";
                        npcObject.Size = new Vector3(80, 96, 0);
                        //npcObject.CollisionBounds.Add(new Rectangle(17 + (int)npcObject.Size.X / 2, 69 + (int)npcObject.Size.Y / 2, 49, 25));
                        break;
                    }
                case RaceEnum.Human:
                    {
                        npcObject.Body = new BodyHuman();
                        switch (objectGender)
                        {
                            case GenderEnum.Male:
                                ((BodyHuman)npcObject.Body).MainBody.TexturePath = "Character/BodyMale";
                                break;
                            case GenderEnum.Female:
                                ((BodyHuman)npcObject.Body).MainBody.TexturePath = "Character/BodyFemale";
                                break;
                        }
                        break;
                    }
            }
            npcObject.Tasks.Add(new Model.Object.Task.Tasks.AttackRandomTask(npcObject, Model.Object.Task.Tasks.TaskPriority.Attack_Random));
            //npcObject.Tasks.Add(new Model.Object.Task.Tasks.WalkRandomTask(npcObject, Model.Object.Task.Tasks.TaskPriority.Walk_Random));

            return npcObject;
        }
    }
}
