using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Object;
using Server.Factories.FactoryEnums;
using Microsoft.Xna.Framework;

namespace Server.Factories
{
    class CreatureFactory
    {
        public static CreatureFactory creatureFactory = new CreatureFactory();

        private CreatureFactory()
        {
        }

        public NpcObject createNpcObject(RaceEnum objectRace, FactionEnum objectFaction, CreatureEnum objectType, GenderEnum objectGender)
        {
            NpcObject npcObject = new NpcObject();
            npcObject.Scale = 1;
            npcObject.Velocity = new Vector3(0, 0, 0);
            npcObject.Faction = BehaviourFactory.behaviourFactory.getFaction(objectFaction);
            npcObject.Race = BehaviourFactory.behaviourFactory.getRace(objectRace);
            npcObject.Gender = objectGender;

            switch (objectType)
            {
                case CreatureEnum.Chieftain:
                    {
                        npcObject.Name = "Anführer"; //TODO NameFactory entwerfen
                        break;
                    }
            }

            return npcObject;
        }
    }
}
