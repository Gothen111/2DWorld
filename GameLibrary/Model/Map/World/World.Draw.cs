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
        public void drawBlocks(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch, LivingObject _Target) // LIVINGOBJEKT
        {
            if (_Target != null)
            {
                /*if (_Target.CurrentBlock != null)
                {
                    Chunk.Chunk var_ChunkMid = (Chunk.Chunk)_Target.CurrentBlock.Parent;
                    var_ChunkMid.drawBlocks(_GraphicsDevice, _SpriteBatch);

                    Chunk.Chunk var_ChunkTop = (Chunk.Chunk)var_ChunkMid.TopNeighbour;
                    if (var_ChunkTop != null)
                    {
                        Chunk.Chunk var_ChunkTopLeft = (Chunk.Chunk)var_ChunkTop.LeftNeighbour;
                        if (var_ChunkTopLeft != null)
                        {
                            var_ChunkTopLeft.drawBlocks(_GraphicsDevice, _SpriteBatch);
                        }
                        Chunk.Chunk var_ChunkTopRight = (Chunk.Chunk)var_ChunkTop.RightNeighbour;
                        if (var_ChunkTopRight != null)
                        {
                            var_ChunkTopRight.drawBlocks(_GraphicsDevice, _SpriteBatch);
                        }
                        var_ChunkTop.drawBlocks(_GraphicsDevice, _SpriteBatch);
                    }
                    Chunk.Chunk var_ChunkLeft = (Chunk.Chunk)var_ChunkMid.LeftNeighbour;
                    if (var_ChunkLeft != null)
                    {
                        var_ChunkLeft.drawBlocks(_GraphicsDevice, _SpriteBatch);
                    }
                    Chunk.Chunk var_ChunkRight = (Chunk.Chunk)var_ChunkMid.RightNeighbour;
                    if (var_ChunkRight != null)
                    {
                        var_ChunkRight.drawBlocks(_GraphicsDevice, _SpriteBatch);
                    }

                    Chunk.Chunk var_ChunkBottom = (Chunk.Chunk)var_ChunkMid.BottomNeighbour;
                    if (var_ChunkBottom != null)
                    {
                        Chunk.Chunk var_ChunkBottomLeft = (Chunk.Chunk)var_ChunkBottom.LeftNeighbour;
                        if (var_ChunkBottomLeft != null)
                        {
                            var_ChunkBottomLeft.drawBlocks(_GraphicsDevice, _SpriteBatch);
                        }
                        Chunk.Chunk var_ChunkBottomRight = (Chunk.Chunk)var_ChunkBottom.RightNeighbour;
                        if (var_ChunkBottomRight != null)
                        {
                            var_ChunkBottomRight.drawBlocks(_GraphicsDevice, _SpriteBatch);
                        }
                        var_ChunkBottom.drawBlocks(_GraphicsDevice, _SpriteBatch);
                    }
                }*/


                int var_DrawSizeX = 30;
                int var_DrawSizeY = 30;

                for (int x = 0; x < var_DrawSizeX; x++)
                {
                    for (int y = 0; y < var_DrawSizeY; y++)
                    {
                        Block.Block var_Block = this.getBlockAtCoordinate(_Target.CurrentBlock.Position.X + (-var_DrawSizeX / 2 + x) * Block.Block.BlockSize, _Target.CurrentBlock.Position.Y + (-var_DrawSizeY / 2 + y) * Block.Block.BlockSize);
                        if (var_Block != null)
                        {
                            var_Block.drawBlock(_GraphicsDevice, _SpriteBatch);
                        }
                    }
                }

            }
        }

        public void drawObjects(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch, LivingObject _Target) // LIVINGOBJECT
        {
            if (_Target != null)
            {
                if (!Configuration.Configuration.isHost)
                {
                    List<Object.Object> var_EnviornmentObjects = this.environmentObjectToDraw;//((Chunk.Chunk)_Target.CurrentBlock.Parent).getAllEnvironmentObjectsInChunk();// this.environmentObjectToDraw; // = this.getObjectsInRange(_Target.Position, 400);

                    var_EnviornmentObjects.Sort(new Ressourcen.ObjectPositionComparer());

                    foreach (AnimatedObject var_AnimatedObject in var_EnviornmentObjects)
                    {
                        var_AnimatedObject.draw(_GraphicsDevice, _SpriteBatch, new Vector3(), Color.White);
                    }
                }



                List<Object.Object> var_Objects = this.objectsToUpdate; // = this.getObjectsInRange(_Target.Position, 400);

                if (!(_Target is PlayerObject))
                {
                    var_Objects = this.getObjectsInRange(_Target.Position, 400);
                }

                var_Objects.Sort(new Ressourcen.ObjectPositionComparer());

                foreach (AnimatedObject var_AnimatedObject in var_Objects)
                {
                    var_AnimatedObject.draw(_GraphicsDevice, _SpriteBatch, new Vector3(), Color.White);
                }



                //TODO: preenvionmentobjekte brauchen n exta quadtree zum malen.
                if (_Target.CurrentBlock != null)
                {
                    Chunk.Chunk var_ChunkMid = (Chunk.Chunk)_Target.CurrentBlock.Parent;
                    //var_ChunkMid.drawObjects(_GraphicsDevice, _SpriteBatch);
                }
            }
        }
        #endregion
    }
}
