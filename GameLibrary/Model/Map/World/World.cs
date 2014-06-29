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
    [Serializable()]
    public class World : Box, ISerializable
    {
        #region Attribute
        public static World world;

        private List<Region.Region> regions;

        private QuadTree<LivingObject> quadTree;

        public QuadTree<LivingObject> QuadTree
        {
            get { return quadTree; }
            set { quadTree = value; }
        }

        private float updatePlayerIntervall = 0;
        private float updatePlayerIntervallmax = 60;

        private List<PlayerObject> playerObjects;

        private List<LivingObject> livingObjectsToUpdate;

        #endregion
        #region Constructors
        public World()
            :base()
        {
            this.quadTree = new QuadTree<LivingObject>(new Vector3(32, 32, 0), 20);
        }

        public World(SerializationInfo info, StreamingContext ctxt) : this()
        {
            //this.regions = (List<Region.Region>)info.GetValue("regions", typeof(List<Region.Region>));
            this.playerObjects = new List<PlayerObject>();
            this.regions = new List<Region.Region>();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            //info.AddValue("regions", this.regions, typeof(List<Region.Region>));
        }

        public World(String _Name)
        {
            this.Name = _Name;

            regions = new List<Region.Region>();
            if (Configuration.Configuration.isHost)
            {
                quadTree = new QuadTree<LivingObject>(new Vector3(32, 32, 0), 20);
            }

            this.playerObjects = new List<PlayerObject>();
        }
        #endregion
        public bool addRegion(Region.Region _Region)
        {
            if (!containsRegion(_Region.Id))
            {
                this.regions.Add(_Region);
                //this.setAllNeighboursOfRegion(_Region);
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
            if (this.getRegion(_Id) != null)
            {
                return true;
            }
            return false;
        }

        public bool containsRegion(Region.Region _Region)
        {
            return false;
        }


        public void setAllNeighboursOfRegion(Region.Region _Region)
        {
            if (_Region != null)
            {
                Region.Region var_RegionNeighbourLeft = this.getRegionAtPosition(_Region.Position.X - (Region.Region.regionSizeX * Chunk.Chunk.chunkSizeX) * Block.Block.BlockSize, _Region.Position.Y);

                if (var_RegionNeighbourLeft != null)
                {
                    _Region.LeftNeighbour = var_RegionNeighbourLeft;
                    var_RegionNeighbourLeft.RightNeighbour = _Region;

                    foreach (Chunk.Chunk var_Chunk_Right in _Region.Chunks)
                    {
                        foreach (Chunk.Chunk var_Chunk_Left in var_RegionNeighbourLeft.Chunks)
                        {
                            if (var_Chunk_Right.Position.Y == var_Chunk_Left.Position.Y)
                            {
                                if (var_Chunk_Right.Position.X == var_Chunk_Left.Position.X + Chunk.Chunk.chunkSizeX * Block.Block.BlockSize)
                                {
                                    var_Chunk_Right.LeftNeighbour = var_Chunk_Left;
                                    var_Chunk_Left.RightNeighbour = var_Chunk_Right;
                                }
                            }
                        }
                    }
                }

                Region.Region var_RegionNeighbourRight = this.getRegionAtPosition(_Region.Position.X + (Region.Region.regionSizeX * Chunk.Chunk.chunkSizeX) * Block.Block.BlockSize, _Region.Position.Y);

                if (var_RegionNeighbourRight != null)
                {
                    _Region.RightNeighbour = var_RegionNeighbourRight;
                    var_RegionNeighbourRight.LeftNeighbour = _Region;

                    foreach (Chunk.Chunk var_Chunk_Right in var_RegionNeighbourRight.Chunks)
                    {
                        foreach (Chunk.Chunk var_Chunk_Left in _Region.Chunks)
                        {
                            if (var_Chunk_Right.Position.Y == var_Chunk_Left.Position.Y)
                            {
                                if (var_Chunk_Right.Position.X == var_Chunk_Left.Position.X + Chunk.Chunk.chunkSizeX * Block.Block.BlockSize)
                                {
                                    var_Chunk_Right.LeftNeighbour = var_Chunk_Left;
                                    var_Chunk_Left.RightNeighbour = var_Chunk_Right;
                                }
                            }
                        }
                    }
                }

                Region.Region var_RegionNeighbourTop = this.getRegionAtPosition(_Region.Position.X, _Region.Position.Y - (Region.Region.regionSizeY * Chunk.Chunk.chunkSizeY) * Block.Block.BlockSize);

                if (var_RegionNeighbourTop != null)
                {
                    _Region.TopNeighbour = var_RegionNeighbourTop;
                    var_RegionNeighbourTop.BottomNeighbour = _Region;

                    foreach (Chunk.Chunk var_Chunk_Top in var_RegionNeighbourTop.Chunks)
                    {
                        foreach (Chunk.Chunk var_Chunk_Bottom in _Region.Chunks)
                        {
                            if (var_Chunk_Top.Position.X == var_Chunk_Bottom.Position.X)
                            {
                                if (var_Chunk_Top.Position.Y == var_Chunk_Bottom.Position.Y - Chunk.Chunk.chunkSizeX * Block.Block.BlockSize)
                                {
                                    var_Chunk_Top.BottomNeighbour = var_Chunk_Bottom;
                                    var_Chunk_Bottom.TopNeighbour = var_Chunk_Top;
                                }
                            }
                        }
                    }
                }

                Region.Region var_RegionNeighbourBottom = this.getRegionAtPosition(_Region.Position.X, _Region.Position.Y + (Region.Region.regionSizeY * Chunk.Chunk.chunkSizeY) * Block.Block.BlockSize);

                if (var_RegionNeighbourBottom != null)
                {
                    _Region.BottomNeighbour = var_RegionNeighbourBottom;
                    var_RegionNeighbourBottom.TopNeighbour = _Region;

                    foreach (Chunk.Chunk var_Chunk_Top in _Region.Chunks)
                    {
                        foreach (Chunk.Chunk var_Chunk_Bottom in var_RegionNeighbourBottom.Chunks)
                        {
                            if (var_Chunk_Top.Position.X == var_Chunk_Bottom.Position.X)
                            {
                                if (var_Chunk_Top.Position.Y == var_Chunk_Bottom.Position.Y - Chunk.Chunk.chunkSizeX * Block.Block.BlockSize)
                                {
                                    var_Chunk_Top.BottomNeighbour = var_Chunk_Bottom;
                                    var_Chunk_Bottom.TopNeighbour = var_Chunk_Top;
                                }
                            }
                        }
                    }
                }
            }
        }

        public Region.Region getRegionAtPosition(float _PosX, float _PosY)
        {
            foreach (Region.Region var_Region in this.regions)
            {
                if (var_Region.Bounds.Left <= _PosX && var_Region.Bounds.Right >= _PosX)
                {
                    if (var_Region.Bounds.Top <= _PosY && var_Region.Bounds.Bottom >= _PosY)
                    {
                        return var_Region;
                    }
                }
            }
            return null;
        }

        public Chunk.Chunk getChunkAtPosition(float _PosX, float _PosY)
        {
            Region.Region var_Region = World.world.getRegionAtPosition(_PosX, _PosY);
            if (var_Region != null)
            {
                return var_Region.getChunkAtPosition(_PosX, _PosY);
            }
            return null;
        }

        public Region.Region createRegionAt(int _PosX, int _PosY)
        {
            return GameLibrary.Factory.RegionFactory.regionFactory.generateRegion("Region" + Region.Region._id, _PosX, _PosY, RegionEnum.Grassland, this);
        }

        #region drawing
        public void drawBlocks(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch, LivingObject _Target)
        {
            //float var_LayerDepth = 0.79f;
            //float var_AmountToRemove = 0.001f;
            if (_Target != null)
            {
                if (_Target.CurrentBlock != null)
                {
                    Chunk.Chunk var_ChunkMid = (Chunk.Chunk)_Target.CurrentBlock.Parent;
                    var_ChunkMid.drawBlocks(_GraphicsDevice, _SpriteBatch);//, var_LayerDepth - var_AmountToRemove * Chunk.Chunk.chunkSizeX, var_AmountToRemove);

                    Chunk.Chunk var_ChunkTop = (Chunk.Chunk)var_ChunkMid.TopNeighbour;
                    if (var_ChunkTop != null)
                    {
                        Chunk.Chunk var_ChunkTopLeft = (Chunk.Chunk)var_ChunkTop.LeftNeighbour;
                        if (var_ChunkTopLeft != null)
                        {
                            var_ChunkTopLeft.drawBlocks(_GraphicsDevice, _SpriteBatch);//, var_LayerDepth, var_AmountToRemove);
                        }
                        Chunk.Chunk var_ChunkTopRight = (Chunk.Chunk)var_ChunkTop.RightNeighbour;
                        if (var_ChunkTopRight != null)
                        {
                            var_ChunkTopRight.drawBlocks(_GraphicsDevice, _SpriteBatch);//, var_LayerDepth, var_AmountToRemove);
                        }
                        var_ChunkTop.drawBlocks(_GraphicsDevice, _SpriteBatch);//, var_LayerDepth, var_AmountToRemove);
                    }
                    Chunk.Chunk var_ChunkLeft = (Chunk.Chunk)var_ChunkMid.LeftNeighbour;
                    if (var_ChunkLeft != null)
                    {
                        var_ChunkLeft.drawBlocks(_GraphicsDevice, _SpriteBatch);//, var_LayerDepth - var_AmountToRemove * Chunk.Chunk.chunkSizeX, var_AmountToRemove);
                    }
                    Chunk.Chunk var_ChunkRight = (Chunk.Chunk)var_ChunkMid.RightNeighbour;
                    if (var_ChunkRight != null)
                    {
                        var_ChunkRight.drawBlocks(_GraphicsDevice, _SpriteBatch);//, var_LayerDepth - var_AmountToRemove * Chunk.Chunk.chunkSizeX, var_AmountToRemove);
                    }

                    Chunk.Chunk var_ChunkBottom = (Chunk.Chunk)var_ChunkMid.BottomNeighbour;
                    if (var_ChunkBottom != null)
                    {
                        Chunk.Chunk var_ChunkBottomLeft = (Chunk.Chunk)var_ChunkBottom.LeftNeighbour;
                        if (var_ChunkBottomLeft != null)
                        {
                            var_ChunkBottomLeft.drawBlocks(_GraphicsDevice, _SpriteBatch);//, var_LayerDepth - var_AmountToRemove * 2 * Chunk.Chunk.chunkSizeX, var_AmountToRemove);
                        }
                        Chunk.Chunk var_ChunkBottomRight = (Chunk.Chunk)var_ChunkBottom.RightNeighbour;
                        if (var_ChunkBottomRight != null)
                        {
                            var_ChunkBottomRight.drawBlocks(_GraphicsDevice, _SpriteBatch);//, var_LayerDepth - var_AmountToRemove * 2 * Chunk.Chunk.chunkSizeX, var_AmountToRemove);
                        }
                        var_ChunkBottom.drawBlocks(_GraphicsDevice, _SpriteBatch);//, var_LayerDepth - var_AmountToRemove * 2 * Chunk.Chunk.chunkSizeX, var_AmountToRemove);
                    }
                }

                /*List<LivingObject> var_LivingObjects = this.getObjectsInRange(_Target.Position, 1000);
                foreach (LivingObject var_LivingObject in var_LivingObjects)
                {
                    var_LivingObject.draw(_GraphicsDevice, _SpriteBatch, new Vector3(0, 0, 0), Color.White);
                }*/
            }
        }

        public void drawObjects(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch, LivingObject _Target)
        {
            //float var_LayerDepth = 0.79f;
            //float var_AmountToRemove = 0.001f;
            if (_Target != null)
            {
                List<LivingObject> var_LivingObjects = this.getObjectsInRange(_Target.Position, 400);
                foreach (LivingObject var_LivingObject in var_LivingObjects)
                {
                    var_LivingObject.draw(_GraphicsDevice, _SpriteBatch, new Vector3(), Color.White);
                }

                if (_Target.CurrentBlock != null)
                {
                    Chunk.Chunk var_ChunkMid = (Chunk.Chunk)_Target.CurrentBlock.Parent;
                    var_ChunkMid.drawObjects(_GraphicsDevice, _SpriteBatch);//, var_LayerDepth - var_AmountToRemove * Chunk.Chunk.chunkSizeX, var_AmountToRemove);

                    /*Chunk.Chunk var_ChunkTop = (Chunk.Chunk)var_ChunkMid.TopNeighbour;
                    if (var_ChunkTop != null)
                    {
                        Chunk.Chunk var_ChunkTopLeft = (Chunk.Chunk)var_ChunkTop.LeftNeighbour;
                        if (var_ChunkTopLeft != null)
                        {
                            var_ChunkTopLeft.drawObjects(_GraphicsDevice, _SpriteBatch);//, var_LayerDepth, var_AmountToRemove);
                        }
                        Chunk.Chunk var_ChunkTopRight = (Chunk.Chunk)var_ChunkTop.RightNeighbour;
                        if (var_ChunkTopRight != null)
                        {
                            var_ChunkTopRight.drawObjects(_GraphicsDevice, _SpriteBatch);//, var_LayerDepth, var_AmountToRemove);
                        }
                        var_ChunkTop.drawObjects(_GraphicsDevice, _SpriteBatch);//, var_LayerDepth, var_AmountToRemove);
                    }
                    Chunk.Chunk var_ChunkLeft = (Chunk.Chunk)var_ChunkMid.LeftNeighbour;
                    if (var_ChunkLeft != null)
                    {
                        var_ChunkLeft.drawObjects(_GraphicsDevice, _SpriteBatch);//, var_LayerDepth - var_AmountToRemove * Chunk.Chunk.chunkSizeX, var_AmountToRemove);
                    }
                    Chunk.Chunk var_ChunkRight = (Chunk.Chunk)var_ChunkMid.RightNeighbour;
                    if (var_ChunkRight != null)
                    {
                        var_ChunkRight.drawObjects(_GraphicsDevice, _SpriteBatch);//, var_LayerDepth - var_AmountToRemove * Chunk.Chunk.chunkSizeX, var_AmountToRemove);
                    }

                    Chunk.Chunk var_ChunkBottom = (Chunk.Chunk)var_ChunkMid.BottomNeighbour;
                    if (var_ChunkBottom != null)
                    {
                        Chunk.Chunk var_ChunkBottomLeft = (Chunk.Chunk)var_ChunkBottom.LeftNeighbour;
                        if (var_ChunkBottomLeft != null)
                        {
                            var_ChunkBottomLeft.drawObjects(_GraphicsDevice, _SpriteBatch);//, var_LayerDepth - var_AmountToRemove * 2 * Chunk.Chunk.chunkSizeX, var_AmountToRemove);
                        }
                        Chunk.Chunk var_ChunkBottomRight = (Chunk.Chunk)var_ChunkBottom.RightNeighbour;
                        if (var_ChunkBottomRight != null)
                        {
                            var_ChunkBottomRight.drawObjects(_GraphicsDevice, _SpriteBatch);//, var_LayerDepth - var_AmountToRemove * 2 * Chunk.Chunk.chunkSizeX, var_AmountToRemove);
                        }
                        var_ChunkBottom.drawObjects(_GraphicsDevice, _SpriteBatch);//, var_LayerDepth - var_AmountToRemove * 2 * Chunk.Chunk.chunkSizeX, var_AmountToRemove);
                    }*/
                }
            }
        }
        #endregion

        #region Methoden für Range-Berechnung

        public Region.Region getRegionLivingObjectIsIn(GameLibrary.Model.Object.LivingObject _LivingObject)
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

        public Object.LivingObject addLivingObject(Object.LivingObject livingObject)
        {
            return addLivingObject(livingObject, true);
        }

        public Object.LivingObject addLivingObject(Object.LivingObject livingObject, Boolean insertInQuadTree)
        {
            Region.Region region = getRegionLivingObjectIsIn(livingObject);
            return addLivingObject(livingObject, insertInQuadTree, region);
        }

        public Object.LivingObject addLivingObject(Object.LivingObject livingObject, Boolean insertInQuadTree, Region.Region _Region)
        {
            if (_Region != null)
            {
                Chunk.Chunk chunk = _Region.getChunkLivingObjectIsIn(livingObject);
                if (chunk != null)
                {
                    Block.Block block = chunk.getBlockAtCoordinate(livingObject.Position.X, livingObject.Position.Y);
                    block.addLivingObject(livingObject);
                    if (insertInQuadTree)
                    {
                        if (quadTree == null)
                            quadTree = new QuadTree<LivingObject>(new Vector3(32, 32, 0), 20);
                        quadTree.Insert(livingObject);
                    }
                }
            }
            else
            {
                Logger.Logger.LogInfo("World.addLivingObject: LivingObject konnte der Region nicht hinzugefügt werden, da diese null war");
            }
            return livingObject;
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
            return getObjectsInRange(_Position, _Range, new List<SearchFlags.Searchflag>());
        }

        public List<LivingObject> getObjectsInRange(Vector3 _Position, float _Range, List<SearchFlags.Searchflag> _SearchFlags)
        {
            Util.Circle circle = new Util.Circle(_Position, _Range);
            List<LivingObject> result = new List<LivingObject>();

            getObjectsInRange(circle, this.quadTree.Root, result, _SearchFlags);

            return result;
        }

        public List<LivingObject> getObjectsColliding(Rectangle bounds)
        {
            return getObjectsColliding(bounds, new List<SearchFlags.Searchflag>());
        }

        public List<LivingObject> getObjectsColliding(Rectangle bounds, List<SearchFlags.Searchflag> _SearchFlags)
        {
            List<LivingObject> result = new List<LivingObject>();
            getObjectsColliding(bounds, this.QuadTree.Root, result, _SearchFlags);
            return result;
        }

        private void getObjectsColliding(Rectangle bounds, QuadTree<LivingObject>.QuadNode currentNode, List<LivingObject> result, List<SearchFlags.Searchflag> _SearchFlags)
        {
            if (Util.Intersection.RectangleIsInRectangle(bounds, currentNode.Bounds))
            {
                //Circle fits in node, so search in subnodes
                Boolean circleFitsInSubnode = false;
                foreach (QuadTree<LivingObject>.QuadNode node in currentNode.Nodes)
                {
                    if (node != null)
                    {
                        if (Util.Intersection.RectangleIsInRectangle(bounds, node.Bounds))
                        {
                            circleFitsInSubnode = true;
                            getObjectsInRange(bounds, node, result, _SearchFlags);
                        }
                    }
                }

                //Aggrocircle fit into a subnode? then
                if (!circleFitsInSubnode)
                {
                    addAllObjectsInRange(currentNode, bounds, result, _SearchFlags);
                }
                return;
            }
            if (currentNode.Equals(quadTree.Root))
            {
                addAllObjectsInRange(currentNode, bounds, result, _SearchFlags);
            }
        }

        private void getObjectsInRange(Util.Circle aggroCircle, QuadTree<LivingObject>.QuadNode currentNode, List<LivingObject> result, List<SearchFlags.Searchflag> _SearchFlags)
        {
            if (Util.Intersection.CircleIsInRectangle(aggroCircle, currentNode.Bounds))
            {
                //Circle fits in node, so search in subnodes
                Boolean circleFitsInSubnode = false;
                foreach (QuadTree<LivingObject>.QuadNode node in currentNode.Nodes)
                {
                    if (node != null)
                    {
                        if (Util.Intersection.CircleIsInRectangle(aggroCircle, node.Bounds))
                        {
                            circleFitsInSubnode = true;
                            getObjectsInRange(aggroCircle, node, result, _SearchFlags);
                        }
                    }
                }

                //Aggrocircle fit into a subnode? then
                if (!circleFitsInSubnode)
                {
                    addAllObjectsInRange(currentNode, aggroCircle, result, _SearchFlags);
                }
                return;
            }
            if (currentNode.Equals(quadTree.Root))
            {
                addAllObjectsInRange(currentNode, aggroCircle, result, _SearchFlags);
            }
        }

        private void getObjectsInRange(Rectangle bounds, QuadTree<LivingObject>.QuadNode currentNode, List<LivingObject> result, List<SearchFlags.Searchflag> _SearchFlags)
        {
            if (Util.Intersection.RectangleIsInRectangle(bounds, currentNode.Bounds))
            {
                foreach (QuadTree<LivingObject>.QuadNode node in currentNode.Nodes)
                {
                    if (node != null)
                    {
                        if (Util.Intersection.RectangleIsInRectangle(bounds, node.Bounds))
                        {
                            getObjectsInRange(bounds, node, result, _SearchFlags);
                        }
                    }
                }

                //Aggrocircle fit into a subnode? then
                addAllObjectsInRange(currentNode, bounds, result, _SearchFlags);
                return;
            }
            if (currentNode.Equals(quadTree.Root))
            {
                addAllObjectsInRange(currentNode, bounds, result, _SearchFlags);
            }
        }

        private void addAllObjectsInRange(QuadTree<LivingObject>.QuadNode currentNode, Util.Circle circle, List<LivingObject> result, List<SearchFlags.Searchflag> _SearchFlags)
        {
            foreach (LivingObject livingObject in currentNode.Objects)
            {
                if (!result.Contains(livingObject))
                {
                    Boolean containsAllFlags = true;
                    foreach (SearchFlags.Searchflag searchFlag in _SearchFlags)
                    {
                        if (!searchFlag.hasFlag(livingObject))
                            containsAllFlags = false;

                    }
                    if (!containsAllFlags)
                        continue;
                    if (Util.Intersection.CircleIntersectsRectangle(circle, livingObject.Bounds))
                    {
                        if (livingObject.CollisionBounds.Count > 0)
                        {
                            foreach (Rectangle collisionBound in livingObject.CollisionBounds)
                            {
                                if (Util.Intersection.CircleIntersectsRectangle(circle, collisionBound))
                                {
                                    result.Add(livingObject);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            result.Add(livingObject);
                        }
                    }
                }
            }
            foreach (QuadTree<LivingObject>.QuadNode node in currentNode.Nodes)
            {
                if (node != null)
                    addAllObjectsInRange(node, circle, result, _SearchFlags);
            }
        }

        private void addAllObjectsInRange(QuadTree<LivingObject>.QuadNode currentNode, Rectangle bounds, List<LivingObject> result, List<SearchFlags.Searchflag> _SearchFlags)
        {
            foreach (LivingObject livingObject in currentNode.Objects)
            {
                if (!result.Contains(livingObject) && !livingObject.IsDead)
                {
                    Boolean containsAllFlags = true;
                    foreach (SearchFlags.Searchflag searchFlag in _SearchFlags)
                    {
                        if (!searchFlag.hasFlag(livingObject))
                            containsAllFlags = false;

                    }
                    if (!containsAllFlags)
                        continue;
                    if (Util.Intersection.RectangleIntersectsRectangle(bounds, livingObject.DrawBounds))
                    {
                        if (livingObject.CollisionBounds != null && livingObject.CollisionBounds.Count > 0)
                        {
                            foreach (Rectangle collisionBound in livingObject.CollisionBounds)
                            {
                                if (Util.Intersection.RectangleIntersectsRectangle(bounds, collisionBound))
                                {
                                    result.Add(livingObject);
                                    break;
                                }
                            }
                        }
                        else
                        {
                            result.Add(livingObject);
                        }
                    }
                }
            }
            foreach (QuadTree<LivingObject>.QuadNode node in currentNode.Nodes)
            {
                if (node != null)
                    addAllObjectsInRange(node, bounds, result, _SearchFlags);
            }
        }

        public bool containsPlayerObject(Object.PlayerObject _PlayerObject)
        {
            return this.playerObjects.Contains(_PlayerObject);
        }
        public void addPlayerObject(Object.PlayerObject _PlayerObject)
        {
            this.addPlayerObject(_PlayerObject, false);
        }
        public void addPlayerObject(Object.PlayerObject _PlayerObject, bool _OnlyToPlayerList)
        {
            if (!containsPlayerObject(_PlayerObject))
            {
                this.playerObjects.Add(_PlayerObject);

                if (!_OnlyToPlayerList)
                {
                    if (Configuration.Configuration.isHost)
                    {
                        Vector2 var_Position_Region = new Vector2(_PlayerObject.Position.X - _PlayerObject.Position.X % (Region.Region.regionSizeX * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize), _PlayerObject.Position.Y - _PlayerObject.Position.Y % (Region.Region.regionSizeY * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize));

                        Region.Region var_Region = World.world.getRegionAtPosition(_PlayerObject.Position.X, _PlayerObject.Position.Y)
                                                    ?? World.world.createRegionAt((int)var_Position_Region.X, (int)var_Position_Region.Y);
                        if (var_Region != null)
                        {
                            this.addRegion(var_Region);

                            Vector2 var_Position_Chunk = new Vector2(_PlayerObject.Position.X - _PlayerObject.Position.X % (Chunk.Chunk.chunkSizeX * Block.Block.BlockSize), _PlayerObject.Position.Y - _PlayerObject.Position.Y % (Chunk.Chunk.chunkSizeY * Block.Block.BlockSize));

                            Chunk.Chunk var_Chunk = var_Region.getChunkAtPosition(_PlayerObject.Position.X, _PlayerObject.Position.Y)
                                                    ?? var_Region.createChunkAt((int)var_Position_Chunk.X, (int)var_Position_Chunk.Y);
                        }
                    }

                    this.addLivingObject(_PlayerObject);
                }
                _PlayerObject.CurrentBlock.markAsDirty();
            }
        }

        #endregion

        #region update-Methoden

        public override void update()
        {
            if (this.NeedUpdate)
            {
                base.update();

                this.livingObjectsToUpdate = new List<LivingObject>();

                this.updatePlayerObjectsNeighborhood();
                if (GameLibrary.Configuration.Configuration.isHost && this.updatePlayerIntervall <= this.updatePlayerIntervallmax)
                {
                    this.updatePlayerIntervall = 0;
                    foreach (PlayerObject playerObject in this.playerObjects)
                    {
                        Configuration.Configuration.commandManager.sendUpdateObjectPositionCommand(playerObject);
                    }
                }
                else
                {
                    this.updatePlayerIntervall++;
                }

                foreach (LivingObject var_LivingObject in this.livingObjectsToUpdate)
                {
                    var_LivingObject.update();
                }

                this.updateChilds();
            }
        }


        private void updatePlayerObjectsNeighborhood()
        {
            foreach (Object.PlayerObject var_PlayerObject in this.playerObjects)
            {
                this.updatePlayerObjectNeighborhood(var_PlayerObject);
            }
        }

        private void updatePlayerObjectNeighborhood(Object.PlayerObject _PlayerObject)
        {
            if (_PlayerObject.CurrentBlock != null)
            {
                Region.Region var_PlayerObjectRegion = (Region.Region)_PlayerObject.CurrentBlock.Parent.Parent;
                //this.addChildToUpdateList(var_PlayerObjectRegion);

                Chunk.Chunk var_ChunkMid = (Chunk.Chunk)_PlayerObject.CurrentBlock.Parent;
                //var_PlayerObjectRegion.addChildToUpdateList(var_ChunkMid);

                //var_ChunkMid.markAsDirty();

                Chunk.Chunk var_ChunkTop = (Chunk.Chunk)var_ChunkMid.TopNeighbour;
                if (var_ChunkTop != null)
                {
                    Chunk.Chunk var_ChunkTopLeft = (Chunk.Chunk)var_ChunkTop.LeftNeighbour;
                    if (var_ChunkTopLeft != null)
                    {
                    }
                    else
                    {
                    }
                    Chunk.Chunk var_ChunkTopRight = (Chunk.Chunk)var_ChunkTop.RightNeighbour;
                    if (var_ChunkTopRight != null)
                    {
                    }
                    else
                    {
                    }
                }
                else
                {
                    if (Configuration.Configuration.isHost)
                    {
                        Region.Region var_Region = World.world.getRegionAtPosition((int)var_ChunkMid.Position.X, (int)var_ChunkMid.Position.Y + -1 * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize)
                                                 ?? World.world.createRegionAt((int)var_PlayerObjectRegion.Position.X, (int)var_PlayerObjectRegion.Position.Y - Region.Region.regionSizeY * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize);
                        if (var_Region != null)
                        {
                            this.addRegion(var_Region);
                            Chunk.Chunk var_Chunk = var_Region.createChunkAt((int)var_ChunkMid.Position.X, (int)var_ChunkMid.Position.Y + -1 * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize);
                            //this.setAllNeighboursOfRegion(var_Region);
                        }
                    }
                    else
                    {
                        if (var_ChunkMid.TopNeighbourRequested)
                        {
                        }
                        else
                        {
                            Event.EventList.Add(new Event(new RequestChunkMessage(new Vector2(var_ChunkMid.Position.X, var_ChunkMid.Position.Y + -1 * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize)), GameMessageImportance.VeryImportant));
                            var_ChunkMid.TopNeighbourRequested = true;
                        }
                    }
                }
                Chunk.Chunk var_ChunkLeft = (Chunk.Chunk)var_ChunkMid.LeftNeighbour;
                if (var_ChunkLeft != null)
                {
                }
                else
                {
                    if (Configuration.Configuration.isHost)
                    {
                        Region.Region var_Region = World.world.getRegionAtPosition((int)var_ChunkMid.Position.X + -1 * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, (int)var_ChunkMid.Position.Y)
                                                 ?? World.world.createRegionAt((int)var_ChunkMid.Position.X + -1 * Region.Region.regionSizeX * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, (int)var_PlayerObjectRegion.Position.Y);
                        if (var_Region != null)
                        {
                            this.addRegion(var_Region);
                            Chunk.Chunk var_Chunk = var_Region.createChunkAt((int)var_ChunkMid.Position.X + -1 * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, (int)var_ChunkMid.Position.Y);
                        }
                    }
                    else
                    {
                        if (var_ChunkMid.LeftNeighbourRequested)
                        {
                        }
                        else
                        {
                            Event.EventList.Add(new Event(new RequestChunkMessage(new Vector2(var_ChunkMid.Position.X + -1 * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, var_ChunkMid.Position.Y)), GameMessageImportance.VeryImportant));
                            var_ChunkMid.LeftNeighbourRequested = true;
                        }
                    }
                }
                Chunk.Chunk var_ChunkRight = (Chunk.Chunk)var_ChunkMid.RightNeighbour;
                if (var_ChunkRight != null)
                {
                }
                else
                {
                    if (Configuration.Configuration.isHost)
                    {
                        Chunk.Chunk var_Chunk = var_PlayerObjectRegion.createChunkAt((int)var_ChunkMid.Position.X + 1 * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, (int)var_ChunkMid.Position.Y);
                    }
                    else
                    {
                        if (var_ChunkMid.RightNeighbourRequested)
                        {
                        }
                        else
                        {
                            Event.EventList.Add(new Event(new RequestChunkMessage(new Vector2(var_ChunkMid.Position.X + 1 * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, var_ChunkMid.Position.Y)), GameMessageImportance.VeryImportant));
                            var_ChunkMid.RightNeighbourRequested = true;
                        }
                    }
                }

                Chunk.Chunk var_ChunkBottom = (Chunk.Chunk)var_ChunkMid.BottomNeighbour;
                if (var_ChunkBottom != null)
                {
                    Chunk.Chunk var_ChunkBottomLeft = (Chunk.Chunk)var_ChunkBottom.LeftNeighbour;
                    if (var_ChunkBottomLeft != null)
                    {
                    }
                    else
                    {
                    }
                    Chunk.Chunk var_ChunkBottomRight = (Chunk.Chunk)var_ChunkBottom.RightNeighbour;
                    if (var_ChunkBottomRight != null)
                    {
                    }
                    else
                    {
                    }
                }
                else
                {
                    if (Configuration.Configuration.isHost)
                    {
                        Chunk.Chunk var_Chunk = var_PlayerObjectRegion.createChunkAt((int)var_ChunkMid.Position.X, (int)var_ChunkMid.Position.Y + 1 * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize);
                    }
                    else
                    {
                        if (var_ChunkMid.BottomNeighbourRequested)
                        {
                        }
                        else
                        {
                            Event.EventList.Add(new Event(new RequestChunkMessage(new Vector2(var_ChunkMid.Position.X, var_ChunkMid.Position.Y + 1 * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize)), GameMessageImportance.VeryImportant));
                            var_ChunkMid.BottomNeighbourRequested = true;
                        }
                    }
                }
            }

            if (Configuration.Configuration.isHost)
            {
                Client var_Client = GameLibrary.Configuration.Configuration.networkManager.getClient(_PlayerObject);
                List<LivingObject> var_LivingObjects = this.getObjectsInRange(_PlayerObject.Position, 400);
                foreach(LivingObject var_LivingObject in var_LivingObjects)
                {
                    if (!this.livingObjectsToUpdate.Contains(var_LivingObject))
                    {
                        this.livingObjectsToUpdate.Add(var_LivingObject);
                    }
                    Configuration.Configuration.networkManager.SendMessageToClient(new UpdateObjectPositionMessage(var_LivingObject), var_Client);
                }
            }
        }
        #endregion

        #region Objekte anhand der ID finden

        public Region.Region getRegion(int _Id)
        {
            foreach (Region.Region var_Region in regions)
            {
                if (var_Region.Id == _Id)
                {
                    return var_Region;
                }
            }
            return null;
        }

        public PlayerObject getPlayerObject(int _Id)
        {
            foreach (PlayerObject var_PlayerObject in playerObjects)
            {
                if (var_PlayerObject.Id == _Id)
                {
                    return var_PlayerObject;
                }
            }
            return null;
        }

        public LivingObject getLivingObject(int _Id)
        {
            if (this.quadTree != null)
            {
                foreach (QuadTree<LivingObject>.QuadNode var_QuadNode in this.quadTree.GetAllNodes())
                {
                    foreach (LivingObject var_LivingObject in var_QuadNode.Objects)
                    {
                        if (var_LivingObject.Id == _Id)
                        {
                            return var_LivingObject;
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }
}
