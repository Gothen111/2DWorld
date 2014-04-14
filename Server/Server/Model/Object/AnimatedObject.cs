using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Server.Model.Object.ObjectEnums;

namespace Server.Model.Object
{
    class AnimatedObject: Object
    {
        private float scale;

        public float Scale
        {
            get { return scale; }
            set { scale = value; }
        }
        protected int animationTime = 0;
        protected int animationTimeMax = 20;
        protected int animation = 0;
        private DirectionEnum directionEnum = DirectionEnum.Down;
        private Vector3 size;

        public Vector3 Size
        {
            get { return size; }
            set { size = value; }
        }
        private String graphicPath;

        public String GraphicPath
        {
            get { return graphicPath; }
            set { graphicPath = value; }
        }

        public override void update()
        {
            base.update();

            this.move();

            if (this.animationTime <= 0)
            {
                if (this.animation == 0)
                {
                    this.animation = 2; // Right
                }
                else
                {
                    this.animation = 0; // Left
                }
                this.animationTime = (int) (this.animationTimeMax/((Math.Abs(this.Velocity.X)+Math.Abs(this.Velocity.Y)+Math.Abs(this.Velocity.Z)))/2.7f);
                this.updateMovementDirection();
            }
            else
            {
                this.animationTime -= 1;
            }
        }

        private void move()
        {
            this.Position += this.Velocity;
        }

        private void updateMovementDirection()
        {
            if (this.Velocity.X == 0 && this.Velocity.Y == 0)
            {

            }
            if (this.Velocity.X < 0)
            {
                this.directionEnum = DirectionEnum.Left;
                if (Math.Abs(this.Velocity.X) < Math.Abs(this.Velocity.Y))
                {
                    if(this.Velocity.Y < 0)
                    {
                        this.directionEnum = DirectionEnum.Top;
                    }
                    else
                    {
                        this.directionEnum = DirectionEnum.Down;
                    }
                }
            }
            else if (this.Velocity.X > 0)
            {
                this.directionEnum = DirectionEnum.Right;
                if (Math.Abs(this.Velocity.X) < Math.Abs(this.Velocity.Y))
                {
                    if (this.Velocity.Y < 0)
                    {
                        this.directionEnum = DirectionEnum.Top;
                    }
                    else
                    {
                        this.directionEnum = DirectionEnum.Down;
                    }
                }
            }
        }

        public void draw(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch)
        {
            int var_DrawX = 0;
            int var_DrawY = 0;

            if (this.Velocity.X == 0 && this.Velocity.Y == 0)
            {
                var_DrawX = 1;
            }
            else
            {
                var_DrawX = this.animation;
            }

            if (this.directionEnum == DirectionEnum.Down)
            {
                var_DrawY = 0;
            }
            else if (this.directionEnum == DirectionEnum.Left)
            {
                var_DrawY = 1;
            }
            else if (this.directionEnum == DirectionEnum.Right)
            {
                var_DrawY = 2;
            }
            else if (this.directionEnum == DirectionEnum.Top)
            {
                var_DrawY = 3;
            }

            _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.GraphicPath], new Vector2(this.Position.X, this.Position.Y), new Rectangle(var_DrawX * 32, var_DrawY * 32, 32, 32), Color.White);
        }
    }
}
