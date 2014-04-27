using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Server.Model.Object.Animation.Animations
{
    class StandAnimation : AnimatedObjectAnimation
    {
        public StandAnimation(AnimatedObject _AnimationOwner)
            : base(_AnimationOwner, -1, -1)
        {
        }
    }
}
