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

        public NpcObject createNpcObject(CreatureEnum objectType)
        {
            NpcObject npcObject = new NpcObject();

            switch (objectType)
            {
                case CreatureEnum.Human_Female:
                    {
                        npcObject.Name = "Frau";
                        npcObject.Scale = 1;
                        npcObject.Velocity = new Vector3(0, 0, 0);
                        break;
                    }
            }

            return npcObject;
        }
    }
}
