using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Server.Model.Object.Animation.Animations
{
    class TakeDamageAnimation : AnimatedObjectAnimation
    {
        public TakeDamageAnimation(AnimatedObject _AnimationOwner)
            : base(_AnimationOwner, 0, 20)
        {
        }

        public override Vector3 drawPositionExtra()
        {
            return base.drawPositionExtra() - new Vector3(0, ((float)this.Animation / (float)(this.AnimationMax)) * 6, 0);
        }

        public override Color drawColor()
        {
            if (this.Animation > this.AnimationMax - 5)
            {
                return new Color(255, 160, 160);
            }
            else
            {
                return base.drawColor();
            }
        }
    }
}
