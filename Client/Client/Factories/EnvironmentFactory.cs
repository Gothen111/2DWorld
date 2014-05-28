﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Client.Factories.FactoryEnums;
using Client.Model.Object;

namespace Client.Factories
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
            environmentObject.StandartStandPositionX = 0;

            switch(objectType)
            {
                case EnvironmentEnum.Tree_Normal_1:
                    {
                        environmentObject.GraphicPath = "Environment/Tree/Tree1";
                        environmentObject.Size = new Microsoft.Xna.Framework.Vector3(64, 64, 0);
                        break;
                    }
                case EnvironmentEnum.Flower_1:
                    {
                        environmentObject.GraphicPath = "Environment/Flower/Flower1";
                        environmentObject.Size = new Microsoft.Xna.Framework.Vector3(32, 32, 0);
                        environmentObject.StandartStandPositionX = Util.Random.GenerateGoodRandomNumber(0,9) * 32;
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
                case EnvironmentEnum.Chest:
                    {
                        environmentObject.GraphicPath = "Environment/Chest/Chest";
                        environmentObject.Size = new Microsoft.Xna.Framework.Vector3(32, 48, 0);
                        environmentObject.StandartStandPositionX = 1*32;
                        environmentObject.Interactions.Add(new Model.Object.Interaction.Interactions.ChestInteraction(environmentObject));
                        break;
                    }
                case EnvironmentEnum.FarmHouse1:
                    {
                        environmentObject.GraphicPath = "Environment/Farm/FarmHouse1";
                        environmentObject.Size = new Microsoft.Xna.Framework.Vector3(370, 355, 0);
                        break;
                    }
            }
            return environmentObject;
        }
    }
}