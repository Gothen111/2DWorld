using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using GameLibrary.Model.Object.Body;

namespace GameLibrary.Model.Object.Animation.Animations
{
    public class OpenChestAnimation : AnimatedObjectAnimation
    {
        private bool chestOpen;
        private int currentFrame;

        public OpenChestAnimation()
        {

        }

        public OpenChestAnimation(BodyPart _BodyPart)
            : base(_BodyPart, 0, 20)
        {
            this.chestOpen = false;
            this.currentFrame = 0;
        }

        public override void update()
        {
            if (!this.chestOpen)
            {
                base.update();
            }
            if (this.Animation <= this.AnimationMax / 4)
            {
                this.currentFrame = 3;
                this.chestOpen = true;
            }
            else if (this.Animation <= this.AnimationMax / 2)
            {
                this.currentFrame = 2;
            }
            else if (this.Animation <= this.AnimationMax * 3 / 4)
            {
                this.currentFrame = 1;
            }
        }

        public override bool finishedAnimation()
        {
            return base.finishedAnimation() && !this.chestOpen;
        }
        public override Rectangle sourceRectangle()
        {
            return new Rectangle((int)this.BodyPart.StandartTextureShift.X, (int)(this.currentFrame * this.BodyPart.Size.Y), (int)this.BodyPart.Size.X, (int)this.BodyPart.Size.Y);
        }
    }
}
