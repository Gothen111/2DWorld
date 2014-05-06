using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
namespace Server.Model.Object.Special
{
    class ChestObject : EnvironmentObject
    {
        public ChestObject()
            :base()
        {
            this.Animation = new Model.Object.Animation.Animations.OpenChestAnimation(this);
        }
    }
}
