using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace GameLibrary.Model.Object.Animation.Animations
{
    public class StandAnimation : AnimatedObjectAnimation
    {
        public StandAnimation()
        {

        }

        public StandAnimation(AnimatedObject _AnimationOwner) : base(_AnimationOwner, -1, -1)
        {

        }
    }
}
