using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;

namespace Client.Model.Object.Animation
{
    class AnimatedObjectAnimation
    {
        private AnimatedObject animationOwner;

        internal AnimatedObject AnimationOwner
        {
            get { return animationOwner; }
            set { animationOwner = value; }
        }

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

        public AnimatedObjectAnimation(AnimatedObject _AnimationOwner, int _Animation, int _AnimationMax)
        {
            this.animationOwner = _AnimationOwner;
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
            this.animation -= 1;
        }

        public virtual Vector3 drawPositionExtra()
        {
            return new Vector3(0, 0, 0);
        }

        public virtual Color drawColor()
        {
            return Color.White;
        }

        public virtual bool finishedAnimation()
        {
            return this.animation <= -1;
        }

        public virtual String graphicPath()
        {
            return this.animationOwner.GraphicPath;
        }

        public int directionDrawY()
        {
            if (this.animationOwner.DirectionEnum == ObjectEnums.DirectionEnum.Down)
            {
                return 0;
            }
            else if (this.animationOwner.DirectionEnum == ObjectEnums.DirectionEnum.Left)
            {
                return 1;
            }
            else if (this.animationOwner.DirectionEnum == ObjectEnums.DirectionEnum.Right)
            {
                return 2;
            }
            else if (this.animationOwner.DirectionEnum == ObjectEnums.DirectionEnum.Top)
            {
                return 3;
            }

            return -1;
        }

        public virtual Rectangle sourceRectangle()
        {
            return new Rectangle(this.animationOwner.StandartStandPositionX, this.directionDrawY() * (int)this.animationOwner.Size.Y, (int)this.animationOwner.Size.X, (int)this.animationOwner.Size.Y);
        }
    }
}
