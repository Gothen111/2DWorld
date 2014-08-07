using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameLibrary.Model.Path
{
    public class Path
    {
        private LinkedList<PathNode> pathNodes;

        public LinkedList<PathNode> PathNodes
        {
            get { return pathNodes; }
            set { pathNodes = value; }
        }

        public Path()
        {
        }

        public Path(LinkedList<PathNode> _PathNodes)
        {
            this.pathNodes = _PathNodes;
        }
    }
}
