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

        private List<Object.PlayerObject> playerObjects;
        #endregion
        #region Constructors
        public World()
            :base()
        {
            this.quadTree = new QuadTree<LivingObject>(new Vector3(32, 32, 0), 20);
        }

        public World(SerializationInfo info, StreamingContext ctxt) : this()
        {
            this.regions = (List<Region.Region>)info.GetValue("regions", typeof(List<Region.Region>));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("regions", this.regions, typeof(List<Region.Region>));
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
            }
        }

        public void drawObjects(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch, LivingObject _Target)
        {
            //float var_LayerDepth = 0.79f;
            //float var_AmountToRemove = 0.001f;
            if (_Target != null)
            {
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


        public void addPlayerObject(Object.PlayerObject _PlayerObject)
        {
            this.playerObjects.Add(_PlayerObject);
            this.addLivingObject(_PlayerObject);

            _PlayerObject.CurrentBlock.markAsDirty();
        }

        #endregion

        #region update-Methoden

        public override void update()
        {
            if (this.NeedUpdate)
            {
                base.update();
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
                this.addChildToUpdateList(var_PlayerObjectRegion);

                Chunk.Chunk var_ChunkMid = (Chunk.Chunk)_PlayerObject.CurrentBlock.Parent;
                var_PlayerObjectRegion.addChildToUpdateList(var_ChunkMid);
                var_ChunkMid.markAsDirty();

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
                    //var_PlayerObjectRegion.createChunkAt((int)var_ChunkMid.Position.X, (int)var_ChunkMid.Position.Y + -1 * Chunk.Chunk.chunkSizeY * Block.Block.BlockSize);
                }
                Chunk.Chunk var_ChunkLeft = (Chunk.Chunk)var_ChunkMid.LeftNeighbour;
                if (var_ChunkLeft != null)
                {
                }
                else
                {
                    //var_PlayerObjectRegion.createChunkAt((int)var_ChunkMid.Position.X + -1 * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, (int)var_ChunkMid.Position.Y);
                }
                Chunk.Chunk var_ChunkRight = (Chunk.Chunk)var_ChunkMid.RightNeighbour;
                if (var_ChunkRight != null)
                {
                }
                else
                {
                    //var_PlayerObjectRegion.createChunkAt((int)var_ChunkMid.Position.X + 1 * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize, (int)var_ChunkMid.Position.Y);
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
                    //var_PlayerObjectRegion.createChunkAt((int)var_ChunkMid.Position.X, (int)var_ChunkMid.Position.Y + 1 * Chunk.Chunk.chunkSizeX * Block.Block.BlockSize);
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
