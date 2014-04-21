using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Server.Model.Object.Animation
{
    class AnimatedObjectAnimation
    {
        private int animation;

        public int Animation
        {
            get { return animation; }
            set { animation = value; }
        }
        private int animationMax;

        public int AnimationMax
        {
            get { return animationMax; }
            set { animationMax = value; }
        }

        public AnimatedObjectAnimation(int _Animation, int _AnimationMax)
        {
            this.animation = _Animation;
            this.animationMax = _AnimationMax;
            this.onStartAnimation();
        }

        public virtual void onStartAnimation()
        {
            this.animation = this.animationMax;
        }

        public virtual void update()
        {
            if (this.animation <= 0)
            {

            }
            else
            {
                animation -= 1;
            }
        }

        public virtual Vector3 drawPositionExtra()
        {
            return new Vector3(0, 0, 0);
        }

        public virtual Color drawColor()
        {
            return Color.White;
        }
    }
}
