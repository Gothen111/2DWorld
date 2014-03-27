using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Object;
using Server.Factories.FactoryEnums;

namespace Server.Factories
{
    class CreatureFactory
    {
        public static CreatureFactory creatureFactory = new CreatureFactory();

        private CreatureFactory()
        {
        }

        public NpcObject createNpcObject(CreatureEnum objectType)
        {
            NpcObject npcObject = new NpcObject();

            switch (objectType)
            {
                case CreatureEnum.HUMAN_FEMALE:
                    {
                        npcObject.
                        break;
                    }
            }

            return npcObject;
        }
    }
}
