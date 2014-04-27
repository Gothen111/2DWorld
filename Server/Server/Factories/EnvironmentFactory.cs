using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Factories.FactoryEnums;
using Server.Model.Object;

namespace Server.Factories
{
    class EnvironmentFactory
    {
        public static EnvironmentFactory environmentFactory = new EnvironmentFactory();

        private EnvironmentFactory()
        {
        }

        public EnvironmentObject createEnvironmentObject(EnvironmentEnum objectType)
        {
            EnvironmentObject environmentObject = new EnvironmentObject();

            switch(objectType)
            {
                case EnvironmentEnum.Tree_Normal_1:
                    {
                        environmentObject.GraphicPath = "Environment/Tree/Tree1";
                        environmentObject.Size = new Microsoft.Xna.Framework.Vector3(64, 64, 0);
                        environmentObject.StandartStandPositionY = 0;
                        break;
                    }
                case EnvironmentEnum.Plant:
                    {
                        break;
                    }
                case EnvironmentEnum.Tree_Brown:
                    {
                        break;
                    }
                case EnvironmentEnum.Tree_Grey:
                    {
                        break;
                    }
            }
            return environmentObject;
        }
    }
}
