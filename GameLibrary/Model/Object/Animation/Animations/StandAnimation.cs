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

        //TODO: Problem, man braucht wahrscheinlich 2 standanimationenne, einmal fpü enviomnet und einmal für creature...
        /*
        public override Rectangle sourceRectangle()
        {
            if (this.BodyPart.StandartTextureShift.X != 0)
            {
                return base.sourceRectangle();
            }
            else
            {
                return new Rectangle((int)this.BodyPart.Size.X, this.directionDrawY() * (int)this.BodyPart.Size.Y, (int)this.BodyPart.Size.X, (int)this.BodyPart.Size.Y);
            }      
        }*/
    }
}
