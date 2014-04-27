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

        public void DrawTest(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch)
        {
            foreach (Region.Region var_Region in this.regions)
            {
                var_Region.DrawTest(_GraphicsDevice, _SpriteBatch);
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
                    Logger.Logger.LogDeb("Position des Objekts ist größer als Anfangsbreite der Region " + var_Region.Id);
                    if (_LivingObject.Position.X <= var_Region.Position.X + var_Region.Bounds.Width)
                    {
                        Logger.Logger.LogDeb("Position des Objekts ist kleiner als Breite der Region " + var_Region.Id);
                        if (_LivingObject.Position.Y >= var_Region.Position.Y)
                        {
                            Logger.Logger.LogDeb("Position des Objekts ist größer als Anfangshöhe der Region " + var_Region.Id);
                            if (_LivingObject.Position.Y <= var_Region.Position.Y + var_Region.Bounds.Height)
                            {
                                Logger.Logger.LogDeb("Position des Objekts ist kleiner als Höhe der Region " + var_Region.Id);
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
            Region.Region region = getRegionLivingObjectIsIn(livingObject);
            Chunk.Chunk chunk = region.getChunkLivingObjectIsIn(livingObject);
            Block.Block block = chunk.getBlockAtCoordinate(livingObject.Position.X, livingObject.Position.Y);
            block.addLivingObject(livingObject);
            quadTree.Insert(livingObject);
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
            }
        }

        private void addAllObjectsInRange(QuadTree<LivingObject>.QuadNode currentNode, Util.Circle circle, List<LivingObject> result)
        {
            foreach(LivingObject livingObject in currentNode.Objects)
            {
                if(Vector3.Distance(livingObject.Position, circle.Position) < circle.Radius)
                {
                    result.Add(livingObject);
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
