using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;
using GameLibrary.Model.Map.Region;

using Microsoft.Xna.Framework.Graphics;
using GameLibrary.Model.Object;
using GameLibrary.Model.Collison;
using GameLibrary.Connection;
using GameLibrary.Connection.Message;

namespace GameLibrary.Model.Map.World
{
    public partial class World
    {
        #region drawing
        public void draw(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch, LivingObject _Target) // LIVINGOBJEKT
        {
            if (Setting.Setting.drawWorld)
            {
                if (_Target != null)
                {
                    if (_Target.CurrentBlock != null)
                    {
                        int var_DrawSizeX = Setting.Setting.blockDrawRange;
                        int var_DrawSizeY = Setting.Setting.blockDrawRange;

                        List<Object.Object> var_PreEnviornmentObjectsToDraw = new List<Object.Object>();
                        List<Object.Object> var_ObjectsToDraw = new List<Object.Object>();

                        for (int x = 0; x < var_DrawSizeX; x++)
                        {
                            for (int y = 0; y < var_DrawSizeY; y++)
                            {
                                Block.Block var_Block = this.getBlockAtCoordinate(_Target.CurrentBlock.Position.X + (-var_DrawSizeX / 2 + x) * Block.Block.BlockSize, _Target.CurrentBlock.Position.Y + (-var_DrawSizeY / 2 + y) * Block.Block.BlockSize);
                                if (var_Block != null)
                                {
                                    if (Setting.Setting.drawBlocks)
                                    {
                                        var_Block.drawBlock(_GraphicsDevice, _SpriteBatch);
                                    }
                                    var_PreEnviornmentObjectsToDraw.AddRange(var_Block.ObjectsPreEnviorment);
                                    var_ObjectsToDraw.AddRange(var_Block.Objects);
                                }
                            }
                        }
                        if (Setting.Setting.drawPreEnvironmentObjects)
                        {
                            var_PreEnviornmentObjectsToDraw.Sort(new Ressourcen.ObjectPositionComparer());
                            foreach (AnimatedObject var_AnimatedObject in var_PreEnviornmentObjectsToDraw)
                            {
                                var_AnimatedObject.draw(_GraphicsDevice, _SpriteBatch, Vector3.Zero, Color.White);
                            }
                        }
                        if (Setting.Setting.drawObjects)
                        {
                            var_ObjectsToDraw.Sort(new Ressourcen.ObjectPositionComparer());
                            foreach (AnimatedObject var_AnimatedObject in var_ObjectsToDraw)
                            {
                                var_AnimatedObject.draw(_GraphicsDevice, _SpriteBatch, Vector3.Zero, Color.White);
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
