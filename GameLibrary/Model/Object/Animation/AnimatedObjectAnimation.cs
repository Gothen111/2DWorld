using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using GameLibrary.Model.Object.Body;

namespace GameLibrary.Model.Object.Animation
{
    public class AnimatedObjectAnimation
    {
        private BodyPart bodyPart;

        public BodyPart BodyPart
        {
            get { return bodyPart; }
            set { bodyPart = value; }
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

        public AnimatedObjectAnimation()
        {

        }

        public AnimatedObjectAnimation(BodyPart _BodyPart, int _Animation, int _AnimationMax)
        {
            this.bodyPart = _BodyPart;
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
            return this.bodyPart.TexturePath;
        }

        public int directionDrawY()
        {
            if (this.bodyPart.Direction == ObjectEnums.DirectionEnum.Down)
            {
                return 0;
            }
            else if (this.bodyPart.Direction == ObjectEnums.DirectionEnum.Left)
            {
                return 1;
            }
            else if (this.bodyPart.Direction == ObjectEnums.DirectionEnum.Right)
            {
                return 2;
            }
            else if (this.bodyPart.Direction == ObjectEnums.DirectionEnum.Top)
            {
                return 3;
            }

            return -1;
        }

        public virtual Rectangle sourceRectangle()
        {
            return new Rectangle((int)this.bodyPart.StandartTextureShift.X, this.directionDrawY() * (int)this.bodyPart.Size.Y, (int)this.bodyPart.Size.X, (int)this.bodyPart.Size.Y);
        }
    }
}
