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
        #region quadTreeObject

        public Object.Object getObject(int _Id)
        {
            if (this.quadTreeObject != null)
            {
                foreach (QuadTree<Object.Object>.QuadNode var_QuadNode in this.quadTreeObject.GetAllNodes())
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
                    if(this.getObject(_Object.Id)==null)
                    {
                        Block.Block block = chunk.getBlockAtCoordinate(_Object.Position.X, _Object.Position.Y);
                        block.addObject(_Object);
                        if (insertInQuadTree)
                        {
                            this.quadTreeObject.Insert(_Object);
                        }
                        if (Configuration.Configuration.isHost)
                        {
                            Event.EventList.Add(new Event(new GameLibrary.Connection.Message.UpdateObjectMessage(_Object), GameMessageImportance.VeryImportant));
                        }
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
            //TODO: Gucke ob element auch vorhanden ;)
            this.quadTreeObject.Remove(_Object);
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

        #endregion

        #region quadTreePreEnviornment

        public Object.Object getPreEnvironmentObject(int _Id)
        {
            if (this.quadTreeObject != null)
            {
                foreach (QuadTree<Object.Object>.QuadNode var_QuadNode in this.quadTreeEnvironmentObject.GetAllNodes())
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

        public Object.Object addPreEnvironmentObject(Object.Object _Object)
        {
            return addPreEnvironmentObject(_Object, true);
        }

        public Object.Object addPreEnvironmentObject(Object.Object _Object, Boolean insertInQuadTree)
        {
            Region.Region region = getRegionObjectIsIn(_Object);
            return addPreEnvironmentObject(_Object, insertInQuadTree, region);
        }

        public Object.Object addPreEnvironmentObject(Object.Object _Object, Boolean insertInQuadTree, Region.Region _Region)
        {
            if (_Region != null)
            {
                //Chunk.Chunk chunk = _Region.getChunkObjectIsIn(_Object);
                //if (chunk != null)
                //{
                if (this.getPreEnvironmentObject(_Object.Id) == null)
                {
                    //Block.Block block = chunk.getBlockAtCoordinate(_Object.Position.X, _Object.Position.Y);
                    //block.addObject(_Object);
                    if (insertInQuadTree)
                    {
                        this.quadTreeEnvironmentObject.Insert(_Object);
                    }
                    if (Configuration.Configuration.isHost)
                    {
                        Event.EventList.Add(new Event(new GameLibrary.Connection.Message.UpdatePreEnvironmentObjectMessage(_Object), GameMessageImportance.VeryImportant));
                    }
                }
                //}
            }
            else
            {
                Logger.Logger.LogInfo("World.addObject: Object konnte der Region nicht hinzugefügt werden, da diese null war");
            }
            return _Object;
        }

        public void removePreEnvironmentObjectFromWorld(Object.Object _Object)
        {
            this.quadTreeEnvironmentObject.Remove(_Object);
            /*if (_Object.CurrentBlock != null)
            {
                _Object.CurrentBlock.removeObject(_Object);
                _Object.CurrentBlock = null;
            }*/

            if (Configuration.Configuration.isHost)
            {
                Event.EventList.Add(new Event(new GameLibrary.Connection.Message.RemoveObjectMessage(_Object), GameMessageImportance.VeryImportant));
            }
        }

        #endregion

        #region getObjectInRange
        public List<Object.Object> getObjectsInRange(Vector3 _Position, float _Range)
        {
            return getObjectsInRange(_Position, this.quadTreeObject.Root, _Range, new List<SearchFlags.Searchflag>());
        }

        public List<Object.Object> getObjectsInRange(Vector3 _Position, float _Range, List<SearchFlags.Searchflag> _SearchFlags)
        {
            return getObjectsInRange(_Position, this.quadTreeObject.Root, _Range, _SearchFlags);
        }

        public List<Object.Object> getObjectsInRange(Vector3 _Position, QuadTree<Object.Object>.QuadNode currentNode, float _Range)
        {
            return getObjectsInRange(_Position, currentNode, _Range, new List<SearchFlags.Searchflag>());
        }

        public List<Object.Object> getObjectsInRange(Vector3 _Position, QuadTree<Object.Object>.QuadNode currentNode, float _Range, List<SearchFlags.Searchflag> _SearchFlags)
        {
            Util.Circle circle = new Util.Circle(_Position, _Range);
            List<Object.Object> result = new List<Object.Object>();
            if (currentNode != null)
            {
                Rectangle surroundingRectangle = new Rectangle((int)(circle.Position.X - circle.Radius), (int)(circle.Position.Y - circle.Radius), (int)(circle.Radius * 2), (int)(circle.Radius * 2));

                getObjectsInRange(surroundingRectangle, currentNode/*this.quadTreeObject.Root*/, result, _SearchFlags);
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
            }
            else
            {
                Logger.Logger.LogErr("getObjectsInRage(currentNode ist null, wahrscheinlich Root eines Quadtrees");
            }
            return result;
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
            if (currentNode.Equals(this.quadTreeObject.Root))
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
                        if (Util.Intersection.RectangleIntersectsRectangle(bounds, ((AnimatedObject)var_Object).DrawBounds))
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
        #endregion
    }
}
