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

        private QuadTree<Object.Object> quadTree;

        public QuadTree<Object.Object> QuadTree
        {
            get { return quadTree; }
            set { quadTree = value; }
        }

        private List<PlayerObject> playerObjects;

        private List<Object.Object> objectsToUpdate;

        #endregion
        #region Constructors
        public World()
            :base()
        {
            this.quadTree = new QuadTree<Object.Object>(new Vector3(32, 32, 0), 20);
        }

        public World(SerializationInfo info, StreamingContext ctxt) : this()
        {
            this.playerObjects = new List<PlayerObject>();
            this.regions = new List<Region.Region>();
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
        }

        public World(String _Name)
        {
            this.Name = _Name;

            regions = new List<Region.Region>();
            if (Configuration.Configuration.isHost)
            {
                quadTree = new QuadTree<Object.Object>(new Vector3(32, 32, 0), 20);
            }

            this.playerObjects = new List<PlayerObject>();

            Logger.Logger.LogInfo("Welt " + _Name + " wurde erstellt!");
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
                Logger.Logger.LogErr("World->addRegion(...) : Region mit Id: " + _Region.Id + " schon vorhanden!");
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
            return containsRegion(_Region.Id);
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

        public Block.Block getBlockAtCoordinate(float _PosX, float _PosY)
        {
            Chunk.Chunk var_Chunk = World.world.getChunkAtPosition(_PosX, _PosY);
            if (var_Chunk != null)
            {
                return var_Chunk.getBlockAtCoordinate(_PosX, _PosY);
            }
            return null;
        }

        public Region.Region createRegionAt(int _PosX, int _PosY)
        {
            int var_RegionType = Util.Random.GenerateGoodRandomNumber(0, Enum.GetValues(typeof(RegionEnum)).Length);
            return GameLibrary.Factory.RegionFactory.regionFactory.generateRegion("Region" + Region.Region._id, _PosX, _PosY, (RegionEnum)var_RegionType, this);
        }

        #region drawing
        public void drawBlocks(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch, LivingObject _Target) // LIVINGOBJEKT
        {
            if (_Target != null)
            {
                if (_Target.CurrentBlock != null)
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
                }
            }
        }

        public void drawObjects(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch, LivingObject _Target) // LIVINGOBJECT
        {
            if (_Target != null)
            {
                List<Object.Object> var_Objects = this.objectsToUpdate; // = this.getObjectsInRange(_Target.Position, 400);

                var_Objects.Sort(new Ressourcen.ObjectPositionComparer());

                foreach (AnimatedObject var_AnimatedObject in var_Objects)
                {
                    //TODO: Objekte müssen abhänig von position gemalt werden! also ne layerdepth bekommen!
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

        #region Methoden für Range-Berechnung

        public Region.Region getRegionObjectIsIn(GameLibrary.Model.Object.Object _Object)
        {
            foreach (Region.Region var_Region in this.regions)
            {
                if (_Object.Position.X >= var_Region.Position.X)
                {
                    if (_Object.Position.X <= var_Region.Position.X + var_Region.Bounds.Width)
                    {
                        if (_Object.Position.Y >= var_Region.Position.Y)
                        {
                            if (_Object.Position.Y <= var_Region.Position.Y + var_Region.Bounds.Height)
                            {
                                return var_Region;
                            }
                        }
                    }
                }
            }
            return null;
        }

        public Object.Object addObject(Object.Object _Object)
        {
            return addObject(_Object, true);
        }

        public Object.Object addObject(Object.Object _Object, Boolean insertInQuadTree)
        {
            Region.Region region = getRegionObjectIsIn(_Object);
            return addObject(_Object, insertInQuadTree, region);
        }

        public Object.Object addObject(Object.Object _Object, Boolean insertInQuadTree, Region.Region _Region)
        {
            if (_Region != null)
            {
                Chunk.Chunk chunk = _Region.getChunkObjectIsIn(_Object);
                if (chunk != null)
                {
                    Block.Block block = chunk.getBlockAtCoordinate(_Object.Position.X, _Object.Position.Y);
                    block.addObject(_Object);
                    if (insertInQuadTree)
                    {
                        if (quadTree == null)
                            quadTree = new QuadTree<Object.Object>(new Vector3(32, 32, 0), 20);
                        quadTree.Insert(_Object);
                    }
                }
            }
            else
            {
                Logger.Logger.LogInfo("World.addObject: Object konnte der Region nicht hinzugefügt werden, da diese null war");
            }
            return _Object;
        }

        public void removeObjectFromWorld(Object.Object _Object)
        {
            //TODO: Client informieren!
            quadTree.Remove(_Object);
            if (_Object.CurrentBlock != null)
            {
                _Object.CurrentBlock.removeObject(_Object);
                _Object.CurrentBlock = null;
            }

            if (Configuration.Configuration.isHost)
            {
                Event.EventList.Add(new Event(new GameLibrary.Connection.Message.RemoveObjectMessage(_Object), GameMessageImportance.VeryImportant));
            }
        }

        public List<Object.Object> getObjectsInRange(Vector3 _Position, float _Range)
        {
            return getObjectsInRange(_Position, _Range, new List<SearchFlags.Searchflag>());
        }

        public List<Object.Object> getObjectsInRange(Vector3 _Position, float _Range, List<SearchFlags.Searchflag> _SearchFlags)
        {
            Util.Circle circle = new Util.Circle(_Position, _Range);
            List<Object.Object> result = new List<Object.Object>();
            Rectangle surroundingRectangle = new Rectangle((int)(circle.Position.X - circle.Radius), (int)(circle.Position.Y - circle.Radius), (int)(circle.Radius * 2), (int)(circle.Radius * 2));

            getObjectsInRange(surroundingRectangle, this.quadTree.Root, result, _SearchFlags);
            List<Object.Object> toRemove = new List<Object.Object>();
            foreach (Object.Object var_Object in result)
            {
                if (Vector3.Distance(var_Object.Position, _Position) > _Range) //TODO: Mit CollisionBounds berechnen, ob Object im Kreis liegt
                {
                    toRemove.Add(var_Object);
                }
            }
            foreach (Object.Object var_Object in toRemove)
            {
                result.Remove(var_Object);
            }
            return result;
        }

        public List<Object.Object> getObjectsColliding(Rectangle bounds)
        {
            return getObjectsColliding(bounds, new List<SearchFlags.Searchflag>());
        }

        public List<Object.Object> getObjectsColliding(Rectangle bounds, List<SearchFlags.Searchflag> _SearchFlags)
        {
            List<Object.Object> result = new List<Object.Object>();
            getObjectsColliding(bounds, this.QuadTree.Root, result, _SearchFlags);
            return result;
        }

        private void getObjectsColliding(Rectangle bounds, QuadTree<Object.Object>.QuadNode currentNode, List<Object.Object> result, List<SearchFlags.Searchflag> _SearchFlags)
        {
            if (Util.Intersection.RectangleIsInRectangle(bounds, currentNode.Bounds))
            {
                //Circle fits in node, so search in subnodes
                Boolean circleFitsInSubnode = false;
                foreach (QuadTree<Object.Object>.QuadNode node in currentNode.Nodes)
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

        private void getObjectsInRange(Rectangle bounds, QuadTree<Object.Object>.QuadNode currentNode, List<Object.Object> result, List<SearchFlags.Searchflag> _SearchFlags)
        {
            if (Util.Intersection.RectangleIsInRectangle(bounds, currentNode.Bounds))
            {
                foreach (QuadTree<Object.Object>.QuadNode node in currentNode.Nodes)
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

        public void addAllObjectsInRange(QuadTree<Object.Object>.QuadNode currentNode, Rectangle bounds, List<Object.Object> result, List<SearchFlags.Searchflag> _SearchFlags)
        {
            foreach (Object.Object var_Object in currentNode.Objects)
            {
                if (!result.Contains(var_Object))// && !var_Object.IsDead)
                {
                    Boolean containsAllFlags = true;
                    foreach (SearchFlags.Searchflag searchFlag in _SearchFlags)
                    {
                        if (!searchFlag.hasFlag(var_Object))
                            containsAllFlags = false;

                    }
                    if (!containsAllFlags)
                        continue;
                    if (var_Object is AnimatedObject)
                    {
                        if (Util.Intersection.RectangleIntersectsRectangle(bounds, ((AnimatedObject) var_Object).DrawBounds))
                        {
                            if (var_Object.CollisionBounds != null && var_Object.CollisionBounds.Count > 0)
                            {
                                foreach (Rectangle collisionBound in var_Object.CollisionBounds)
                                {
                                    if (Util.Intersection.RectangleIntersectsRectangle(bounds, collisionBound))
                                    {
                                        result.Add(var_Object);
                                        break;
                                    }
                                }
                            }
                            else
                            {
                                result.Add(var_Object);
                            }
                        }
                    }
                }
            }
            foreach (QuadTree<Object.Object>.QuadNode node in currentNode.Nodes)
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
                    this.addObject(_PlayerObject);
                }
            }
        }

        #endregion

        #region update-Methoden

        public override void update()
        {
            base.update();

            this.objectsToUpdate = new List<Object.Object>();

            this.updatePlayerObjectsNeighborhood();

            foreach (Object.Object var_Object in this.objectsToUpdate)
            {
                var_Object.update();
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

                Chunk.Chunk var_ChunkMid = (Chunk.Chunk)_PlayerObject.CurrentBlock.Parent;

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

            List<Object.Object> var_Objects = this.getObjectsInRange(_PlayerObject.Position, 400);
            foreach (Object.Object var_Object in var_Objects) 
            {
                if (!this.objectsToUpdate.Contains(var_Object))
                {
                    this.objectsToUpdate.Add(var_Object);
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

        public Object.Object getObject(int _Id)
        {
            if (this.quadTree != null)
            {
                foreach (QuadTree<Object.Object>.QuadNode var_QuadNode in this.quadTree.GetAllNodes())
                {
                    foreach (Object.Object var_Object in var_QuadNode.Objects)
                    {
                        if (var_Object.Id == _Id)
                        {
                            return var_Object;
                        }
                    }
                }
            }
            return null;
        }

        #endregion
    }
}
