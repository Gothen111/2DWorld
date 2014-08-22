using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Factory.FactoryEnums;
using GameLibrary.Model.Object;

using GameLibrary.Model.Map.Region;

namespace GameLibrary.Factory
{
    public class EnvironmentFactory
    {
        public static EnvironmentFactory environmentFactory = new EnvironmentFactory();

        private EnvironmentFactory()
        {
        }

        public EnvironmentObject createEnvironmentObject(RegionEnum _RegionEnum, EnvironmentEnum objectType)
        {
            EnvironmentObject environmentObject = new EnvironmentObject();
            environmentObject.Body.MainBody.StandartTextureShift = new Microsoft.Xna.Framework.Vector2(0, 0);

            switch(objectType)
            {
                case EnvironmentEnum.Tree_Normal_1:
                    {
                        environmentObject.Body.MainBody.TexturePath = "Region/" + _RegionEnum.ToString() + "/Block/Environment/Tree/Tree1";
                        environmentObject.Size = new Microsoft.Xna.Framework.Vector3(64, 64, 0);
                        environmentObject.Body.setSize(new Microsoft.Xna.Framework.Vector3(64, 64, 0));
                        break;
                    }
                case EnvironmentEnum.Flower_1:
                    {
                        environmentObject.Body.MainBody.TexturePath = "Region/" + _RegionEnum.ToString() + "/Block/Environment/Flower/Flower1";
                        environmentObject.Size = new Microsoft.Xna.Framework.Vector3(32, 32, 0);
                        environmentObject.Body.MainBody.StandartTextureShift = new Microsoft.Xna.Framework.Vector2(Util.Random.GenerateGoodRandomNumber(0, 9) * 32, 0);
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
                        environmentObject.Body.MainBody.TexturePath = "Region/" + _RegionEnum.ToString() + "/Block/Environment/Chest/Chest";
                        environmentObject.Size = new Microsoft.Xna.Framework.Vector3(32, 48, 0);
                        environmentObject.Body.MainBody.StandartTextureShift = new Microsoft.Xna.Framework.Vector2(1*32, 0);
                        environmentObject.Interactions.Add(new GameLibrary.Model.Object.Interaction.Interactions.ChestInteraction(environmentObject));
                        break;
                    }
                case EnvironmentEnum.FarmHouse1:
                    {
                        environmentObject.Body.MainBody.TexturePath = "Region/" + _RegionEnum.ToString() + "/Block/Environment/Farm/FarmHouse1";
                        environmentObject.Size = new Microsoft.Xna.Framework.Vector3(370, 355, 0);
                        break;
                    }
            }
            return environmentObject;
        }
    }
}
