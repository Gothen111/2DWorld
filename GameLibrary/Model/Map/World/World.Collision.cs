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
        #region Collision

        public List<Object.Object> getObjectsColliding(Rectangle bounds)
        {
            return getObjectsColliding(bounds, new List<SearchFlags.Searchflag>());
        }

        public List<Object.Object> getObjectsColliding(Rectangle bounds, List<SearchFlags.Searchflag> _SearchFlags)
        {
            List<Object.Object> result = new List<Object.Object>();
            getObjectsColliding(bounds, this.quadTreeObject.Root, result, _SearchFlags);
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
            if (currentNode.Equals(this.quadTreeObject.Root))
            {
                addAllObjectsInRange(currentNode, bounds, result, _SearchFlags);
            }
        }

        #endregion
    }
}
