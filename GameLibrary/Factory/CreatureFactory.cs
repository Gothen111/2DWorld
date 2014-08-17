using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Object;
using GameLibrary.Factory.FactoryEnums;
using Microsoft.Xna.Framework;

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
            //playerObject.GraphicPath = "Character/Char1_Small";

            switch (objectRace)
            {
                case RaceEnum.Ogre:
                    {
                        playerObject.GraphicPath = "Character/Ogre1";
                        playerObject.Size = new Vector3(80, 96, 0);
                        //npcObject.CollisionBounds.Add(new Rectangle(17 + (int)npcObject.Size.X / 2, 69 + (int)npcObject.Size.Y / 2, 49, 25));
                        break;
                    }
                case RaceEnum.Human:
                    {
                        switch (objectGender)
                        {
                            case GenderEnum.Male:
                                playerObject.GraphicPath = "Character/BodyMale";
                                break;
                            case GenderEnum.Female:
                                playerObject.GraphicPath = "Character/BodyFemale";
                                break;
                        }
                        break;
                    }
            }

            return playerObject;
        }

        public PlayerObject createPlayerObject(PlayerObject _PlayerObject)
        {
            PlayerObject playerObject = new PlayerObject();
            playerObject.Scale = _PlayerObject.Scale;
            playerObject.Velocity = new Vector3(0, 0, 0);
            playerObject.Faction = _PlayerObject.Faction;
            playerObject.Race = _PlayerObject.Race;
            playerObject.Gender = _PlayerObject.Gender;
            playerObject.Name = _PlayerObject.Name;
            playerObject.GraphicPath = _PlayerObject.GraphicPath; //"Character/Char1_Small";
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
                        npcObject.GraphicPath = "Character/Ogre1";
                        npcObject.Size = new Vector3(80, 96, 0);
                        //npcObject.CollisionBounds.Add(new Rectangle(17 + (int)npcObject.Size.X / 2, 69 + (int)npcObject.Size.Y / 2, 49, 25));
                        break;
                    }
                case RaceEnum.Human:
                    {
                        switch (objectGender)
                        {
                            case GenderEnum.Male:
                                npcObject.GraphicPath = "Character/BodyMale";
                                break;
                            case GenderEnum.Female:
                                npcObject.GraphicPath = "Character/BodyFemale";
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
