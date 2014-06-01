using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Map.Region;
using GameLibrary.Model.Map.Chunk;
using GameLibrary.Model.Map.Block;

using GameLibrary.Model.Object;

namespace GameLibrary.Factory
{
    public class FarmFactory
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
                var_EnvironmentObject.Position = new Microsoft.Xna.Framework.Vector3(500, 500, 0);
                var_EnvironmentObject.CollisionBounds.Add(new Microsoft.Xna.Framework.Rectangle(var_EnvironmentObject.DrawBounds.Left + 40, var_EnvironmentObject.DrawBounds.Bottom - 105, 280, 65)); 
                _Region.ParentWorld.addLivingObject(var_EnvironmentObject,true, _Region); // Region wird erst world zugewiesen. dannach könne erst objetek hin :(
            }
        }
    }
}
