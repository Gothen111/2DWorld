using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Map.Region;
using Server.Model.Map.Chunk;
using Server.Model.Map.Block;

using Server.Model.Object;

namespace Server.Factories
{
    class FarmFactory
    {
        public static FarmFactory farmFactory = new FarmFactory();

        public void generateFarms(Region _Region, int _MaxCount, int _MinDistance)
        {
            int var_StartPositionX = (int)_Region.Position.X;
            int var_StartPositionY = (int)_Region.Position.Y;

            int var_EndPositionX = var_StartPositionX + (int)_Region.Size.X * Chunk.chunkSizeX * Block.BlockSize;
            int var_EndPositionY = var_StartPositionY + (int)_Region.Size.Y * Chunk.chunkSizeY * Block.BlockSize;

            int var_Count = Util.Random.GenerateGoodRandomNumber(0, _MaxCount);
            var_Count = _MaxCount;
            for (int i = 0; i < var_Count; i++)
            {
                EnvironmentObject var_EnvironmentObject = EnvironmentFactory.environmentFactory.createEnvironmentObject(FactoryEnums.EnvironmentEnum.FarmHouse1);
                var_EnvironmentObject.Position = new Microsoft.Xna.Framework.Vector3(200, 200, 0);
                var_EnvironmentObject.World = _Region.ParentWorld;
                _Region.ParentWorld.addLivingObject(var_EnvironmentObject,true, _Region); // Region wird erst world zugewiesen. dannach könne erst objetek hin :(
            }
        }
    }
}
