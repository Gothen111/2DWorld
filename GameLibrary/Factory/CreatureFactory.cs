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
            playerObject.GraphicPath = "Character/Char1_Small";

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
            npcObject.GraphicPath = "Character/Char1_Small";
            npcObject.Tasks.Add(new Model.Object.Task.Tasks.AttackRandomTask(npcObject, Model.Object.Task.Tasks.TaskPriority.Attack_Random));

            return npcObject;
        }
    }
}
