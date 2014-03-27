using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Server.Model.Object
{
    class AnimatedObject: Object
    {
        private float scale;

        protected float Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        //protected int animation; //???
        private Vector3 size;

        protected Vector3 Size
        {
            get { return size; }
            set { size = value; }
        }
        private String graphicPath;

        protected String GraphicPath
        {
            get { return graphicPath; }
            set { graphicPath = value; }
        }
    }
}
