using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using GameLibrary.Model.Object.Body;

namespace GameLibrary.Model.Object.Animation.Animations
{
    public class StandAnimation : AnimatedObjectAnimation
    {
        public StandAnimation()
        {

        }

        public StandAnimation(BodyPart _BodyPart)
            : base(_BodyPart, -1, -1)
        {

        }
    }
}
