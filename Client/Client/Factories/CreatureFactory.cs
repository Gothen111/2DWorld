using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Client.Model.Object;
using Client.Factories.FactoryEnums;
using Microsoft.Xna.Framework;

namespace Client.Factories
{
    class CreatureFactory
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

            return npcObject;
        }

        public NpcObject createNpcObject(int _Id, RaceEnum objectRace, FactionEnum objectFaction, CreatureEnum objectType, GenderEnum objectGender)
        {
            NpcObject npcObject = new NpcObject();
            npcObject.Id = _Id;
            npcObject.Scale = 1;
            npcObject.Velocity = new Vector3(0, 0, 0);
            npcObject.Faction = BehaviourFactory.behaviourFactory.getFaction(objectFaction);
            npcObject.Race = BehaviourFactory.behaviourFactory.getRace(objectRace);
            npcObject.Gender = objectGender;
            npcObject.Name = NameFactory.getName(objectType, objectGender);

            return npcObject;
        }
    }
}
