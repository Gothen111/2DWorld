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
