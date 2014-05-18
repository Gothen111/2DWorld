using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Server.Model.Map.Region;

using Microsoft.Xna.Framework.Graphics;
using Server.Model.Object;
using Server.Model.Collison;

namespace Server.Model.Map.World
{
    class World
    {
        private List<Region.Region> regions;

        private String name;

        public String Name
        {
            get { return name; }
            set { name = value; }
        }

        private QuadTree<LivingObject> quadTree;

        internal QuadTree<LivingObject> QuadTree
        {
            get { return quadTree; }
            set { quadTree = value; }
        }

        private Vector2 size;

        public Vector2 Size
        {
            get { return size; }
            set { size = value; }
        }

        public World(String _Name)
        {
            this.name = _Name;
            //this.size = new Vector2(_SizeX, _SizeY);

            regions = new List<Region.Region>();
            quadTree = new QuadTree<LivingObject>(new Vector3(32, 32, 0), 20);
        }

        public bool addRegion(Region.Region _Region)
        {
            if (!containsRegion(_Region.Id))
            {
                this.regions.Add(_Region);
                return false;
            }
            else
            {
                Logger.Logger.LogErr("World->addRegion(...) : Chunk mit Id: " + _Region.Id + " schon vorhanden!");
                return false;
            }
        }

        public bool containsRegion(int _Id)
        {
            return false;
        }

        public bool containsRegion(Region.Region _Region)
        {
            return false;
        }

        public void draw(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch, LivingObject _Target)
        {
            float var_LayerDepth = 0.79f;
            float var_AmountToRemove = 0.001f;

            if (_Target.CurrentBlock != null)
            {
                Chunk.Chunk var_ChunkMid = _Target.CurrentBlock.ParentChunk;
                var_ChunkMid.draw(_GraphicsDevice, _SpriteBatch, var_LayerDepth - var_AmountToRemove * Chunk.Chunk.chunkSizeX, var_AmountToRemove);
                
                Chunk.Chunk var_ChunkTop = (Chunk.Chunk)var_ChunkMid.TopNeighbour;
                if (var_ChunkTop != null)
                {
                    Chunk.Chunk var_ChunkTopLeft = (Chunk.Chunk)var_ChunkTop.LeftNeighbour;
                    if (var_ChunkTopLeft != null)
                    {
                        var_ChunkTopLeft.draw(_GraphicsDevice, _SpriteBatch, var_LayerDepth, var_AmountToRemove);
                    }
                    Chunk.Chunk var_ChunkTopRight = (Chunk.Chunk)var_ChunkTop.RightNeighbour;
                    if (var_ChunkTopRight != null)
                    {
                        var_ChunkTopRight.draw(_GraphicsDevice, _SpriteBatch, var_LayerDepth, var_AmountToRemove);
                    }
                    var_ChunkTop.draw(_GraphicsDevice, _SpriteBatch, var_LayerDepth, var_AmountToRemove);
                }
                Chunk.Chunk var_ChunkLeft = (Chunk.Chunk)var_ChunkMid.LeftNeighbour;
                if (var_ChunkLeft != null)
                {
                    var_ChunkLeft.draw(_GraphicsDevice, _SpriteBatch, var_LayerDepth - var_AmountToRemove * Chunk.Chunk.chunkSizeX, var_AmountToRemove);
                }
                Chunk.Chunk var_ChunkRight = (Chunk.Chunk)var_ChunkMid.RightNeighbour;
                if (var_ChunkRight != null)
                {
                    var_ChunkRight.draw(_GraphicsDevice, _SpriteBatch, var_LayerDepth - var_AmountToRemove * Chunk.Chunk.chunkSizeX, var_AmountToRemove);
                }

                Chunk.Chunk var_ChunkBottom = (Chunk.Chunk)var_ChunkMid.BottomNeighbour;
                if (var_ChunkBottom != null)
                {
                    Chunk.Chunk var_ChunkBottomLeft = (Chunk.Chunk)var_ChunkBottom.LeftNeighbour;
                    if (var_ChunkBottomLeft != null)
                    {
                        var_ChunkBottomLeft.draw(_GraphicsDevice, _SpriteBatch, var_LayerDepth - var_AmountToRemove * 2 * Chunk.Chunk.chunkSizeX, var_AmountToRemove);
                    }
                    Chunk.Chunk var_ChunkBottomRight = (Chunk.Chunk)var_ChunkBottom.RightNeighbour;
                    if (var_ChunkBottomRight != null)
                    {
                        var_ChunkBottomRight.draw(_GraphicsDevice, _SpriteBatch, var_LayerDepth - var_AmountToRemove * 2 * Chunk.Chunk.chunkSizeX, var_AmountToRemove);
                    }
                    var_ChunkBottom.draw(_GraphicsDevice, _SpriteBatch, var_LayerDepth - var_AmountToRemove * 2 * Chunk.Chunk.chunkSizeX, var_AmountToRemove);
                }
            }
        }

        public void update()
        {
            foreach (Region.Region var_Region in this.regions)
            {
                var_Region.update();
            }
        }

        public Region.Region getRegionLivingObjectIsIn(Server.Model.Object.LivingObject _LivingObject)
        {
            foreach (Region.Region var_Region in this.regions)
            {
                if (_LivingObject.Position.X >= var_Region.Position.X)
                {
                    if (_LivingObject.Position.X <= var_Region.Position.X + var_Region.Bounds.Width)
                    {
                        if (_LivingObject.Position.Y >= var_Region.Position.Y)
                        {
                            if (_LivingObject.Position.Y <= var_Region.Position.Y + var_Region.Bounds.Height)
                            {
                                return var_Region;
                            }
                        }
                    }
                }
            }
            return null;
        }

        public void addLivingObject(Object.LivingObject livingObject)
        {
            addLivingObject(livingObject, true);
        }

        public void addLivingObject(Object.LivingObject livingObject, Boolean insertInQuadTree)
        {
            Region.Region region = getRegionLivingObjectIsIn(livingObject);
            addLivingObject(livingObject, insertInQuadTree, region);
        }

        public void addLivingObject(Object.LivingObject livingObject, Boolean insertInQuadTree, Region.Region _Region)
        {
            Chunk.Chunk chunk = _Region.getChunkLivingObjectIsIn(livingObject);
            Block.Block block = chunk.getBlockAtCoordinate(livingObject.Position.X, livingObject.Position.Y);
            block.addLivingObject(livingObject);
            if (insertInQuadTree)
                quadTree.Insert(livingObject);
        }

        public void removeObjectFromWorld(LivingObject livingObject)
        {
            quadTree.Remove(livingObject);
            if (livingObject.CurrentBlock != null)
            {
                livingObject.CurrentBlock.removeLivingObject(livingObject);
                livingObject.CurrentBlock = null;
            }
        }

        public List<LivingObject> getObjectsInRange(Vector3 _Position, float _Range)
        {
            Util.Circle circle = new Util.Circle(_Position, _Range);
            List<LivingObject> result = new List<LivingObject>();

            getObjectsInRange(circle, this.quadTree.Root, result);

            return result;
        }

        private void getObjectsInRange(Util.Circle aggroCircle , QuadTree<LivingObject>.QuadNode currentNode, List<LivingObject> result)
        {
            if (Util.Intersection.CircleIsInRectangle(aggroCircle, currentNode.Bounds))
            {
                //Circle fits in node, so search in subnodes
                Boolean circleFitsInSubnode = false;
                foreach(QuadTree<LivingObject>.QuadNode node in currentNode.Nodes)
                {
                    if (node != null)
                    {
                        if (Util.Intersection.CircleIsInRectangle(aggroCircle, node.Bounds))
                        {
                            circleFitsInSubnode = true;
                            getObjectsInRange(aggroCircle, node, result);
                        }
                    }
                }

                //Aggrocircle fit into a subnode? then
                if (!circleFitsInSubnode)
                {
                    addAllObjectsInRange(currentNode, aggroCircle, result);
                }
                return;
            }
            if (currentNode.Equals(quadTree.Root))
            {
                addAllObjectsInRange(currentNode, aggroCircle, result);
            }
        }

        private void addAllObjectsInRange(QuadTree<LivingObject>.QuadNode currentNode, Util.Circle circle, List<LivingObject> result)
        {
            foreach(LivingObject livingObject in currentNode.Objects)
            {
                if (!result.Contains(livingObject))
                {
                    if (Vector3.Distance(livingObject.Position, circle.Position) < circle.Radius)
                    {
                        result.Add(livingObject);
                    }
                }
            }
            foreach (QuadTree<LivingObject>.QuadNode node in currentNode.Nodes)
            {
                if (node != null)
                    addAllObjectsInRange(node, circle, result);
            }
        }
    }
}
