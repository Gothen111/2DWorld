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

        public static NpcObject createNpcObject(RaceEnum raceEnum, FactionEnum factionEnum, CreatureEnum creatureEnum, GenderEnum genderEnum)
        {
            NpcObject npcObject = new NpcObject();
            npcObject.Scale = 1;
            npcObject.Velocity = new Vector3(0, 0, 0);
            npcObject.Faction = BehaviourFactory.behaviourFactory.getFaction(factionEnum);
            npcObject.Race = BehaviourFactory.behaviourFactory.getRace(raceEnum);
            npcObject.Gender = genderEnum;
            npcObject.Name = NameFactory.getNameOfCreature(creatureEnum, genderEnum);

            switch (creatureEnum)
            {
                case CreatureEnum.Chieftain:
                    {
                        break;
                    }
            }

            return npcObject;
        }
    }
}
