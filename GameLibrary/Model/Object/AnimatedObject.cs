using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameLibrary.Model.Object.ObjectEnums;

namespace GameLibrary.Model.Object
{
    [Serializable()]
    public class AnimatedObject: Object
    {
        public event EventHandler ObjectMoves;

        private float scale;

        public float Scale
        {
            get { return scale; }
            set { this.Size /= scale; scale = value; this.Size *= scale; }
        }

        private DirectionEnum directionEnum = DirectionEnum.Down;

        public DirectionEnum DirectionEnum
        {
            get { return directionEnum; }
            set { directionEnum = value; }
        }

        private String graphicPath;

        public String GraphicPath
        {
            get { return graphicPath; }
            set { graphicPath = value; }
        }

        public Rectangle DrawBounds
        {
            get
            {
                if (this.animation != null)
                {
                    return new Rectangle((int)(this.Position.X + this.animation.drawPositionExtra().X - this.Size.X / 2), (int)(this.Position.Y + this.animation.drawPositionExtra().Y - this.Size.Y), (int)this.Size.X, (int)this.Size.Y);
                }
                else
                {
                    return this.Bounds;
                }
            }
        }

        private List<Rectangle> collisionBounds;

        public List<Rectangle> CollisionBounds
        {
            get { return collisionBounds; }
            set { collisionBounds = value; }
        }

        private float movementSpeed;

        public float MovementSpeed
        {
            get { return movementSpeed; }
            set { movementSpeed = value; }
        }

        private bool moveUp;

        public bool MoveUp
        {
            get { return moveUp; }
            set { moveUp = value; }
        }
        private bool moveLeft;

        public bool MoveLeft
        {
            get { return moveLeft; }
            set { moveLeft = value; }
        }
        private bool moveRight;

        public bool MoveRight
        {
            get { return moveRight; }
            set { moveRight = value; }
        }
        private bool moveDown;

        public bool MoveDown
        {
            get { return moveDown; }
            set { moveDown = value; }
        }

        private Animation.AnimatedObjectAnimation animation;

        public Animation.AnimatedObjectAnimation Animation
        {
            get { return animation; }
            set { animation = value; }
        }

        private int standartStandPositionX;

        public int StandartStandPositionX
        {
            get { return standartStandPositionX; }
            set { standartStandPositionX = value; }
        }

        private float layerDepth;

        public float LayerDepth
        {
            get { return layerDepth; }
            set { layerDepth = value; }
        }

        public AnimatedObject() : base()
        {
            this.scale = 1f;
            this.Size = new Vector3(32, 32, 0);
            if (this.animation == null)
            {
                this.animation = new Animation.Animations.StandAnimation(this);
            }

            this.standartStandPositionX = 0;
            this.movementSpeed = 1.0f;
            this.collisionBounds = new List<Rectangle>();
        }

        public AnimatedObject(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt)
        {
            try
            {
                this.scale = (float)info.GetValue("scale", typeof(float));
            }
            catch (Exception e)
            {
                this.scale = 1f;
            }
            this.layerDepth = (float)info.GetValue("layerDepth", typeof(float));
            this.movementSpeed = (float)info.GetValue("movementSpeed", typeof(float));

            this.directionEnum = (DirectionEnum)info.GetValue("directionEnum", typeof(DirectionEnum));

            this.graphicPath = (String)info.GetValue("graphicPath", typeof(String));

            this.standartStandPositionX = (int)info.GetValue("standartStandPositionX", typeof(int));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("scale", this.scale, typeof(float));
            info.AddValue("layerDepth", this.layerDepth, typeof(float));
            info.AddValue("movementSpeed", this.movementSpeed, typeof(float));

            info.AddValue("directionEnum", this.directionEnum, typeof(DirectionEnum));

            info.AddValue("graphicPath", this.graphicPath, typeof(String));

            info.AddValue("standartStandPositionX", this.standartStandPositionX, typeof(int));

            base.GetObjectData(info, ctxt);
        }

        public override void update()
        {
            base.update();

            this.move();

            if (this.animation != null)
            {
                this.animation.update();
                if (this.animation.finishedAnimation() && !(this.animation is Animation.Animations.MoveAnimation))
                {
                    this.animation = new Animation.Animations.StandAnimation(this);
                }
            }
            //this.Velocity = new Vector3(0, 0, 0);
        }

        private void move()
        {
            float var_X = (-Convert.ToInt32(this.moveLeft) + Convert.ToInt32(this.moveRight)) * this.movementSpeed;
            float var_Y = (-Convert.ToInt32(this.moveUp) + Convert.ToInt32(this.moveDown)) * this.movementSpeed;

            Vector2 var_PositionBlockSizeOld = new Vector2((this.Position.X) / Map.Block.Block.BlockSize, (this.Position.Y) / Map.Block.Block.BlockSize);
            Vector2 var_PositionBlockSizeNew = new Vector2((this.Position.X + var_X) / Map.Block.Block.BlockSize, (this.Position.Y + var_Y) / Map.Block.Block.BlockSize);
            
            Map.Block.Block var_Block = this.CurrentBlock;

            if ((int)var_PositionBlockSizeOld.X < (int)var_PositionBlockSizeNew.X)
            {
                Console.WriteLine((int)var_PositionBlockSizeOld.X);
                var_Block = (Map.Block.Block)this.CurrentBlock.RightNeighbour;
                if (var_Block == null || !var_Block.IsWalkAble)
                {
                    var_X = 0;
                }
            }
            else if ((int)var_PositionBlockSizeOld.X > (int)var_PositionBlockSizeNew.X)
            {
                var_Block = (Map.Block.Block)this.CurrentBlock.LeftNeighbour;
                if (var_Block == null || !var_Block.IsWalkAble)
                {
                    var_X = 0;
                }
            }
            if ((int)var_PositionBlockSizeOld.Y < (int)var_PositionBlockSizeNew.Y)
            {
                var_Block = (Map.Block.Block)this.CurrentBlock.BottomNeighbour;
                if (var_Block == null || !var_Block.IsWalkAble)
                {
                    var_Y = 0;
                }
            }
            else if ((int)var_PositionBlockSizeOld.Y > (int)var_PositionBlockSizeNew.Y)
            {
                var_Block = (Map.Block.Block)this.CurrentBlock.TopNeighbour;
                if (var_Block == null || !var_Block.IsWalkAble)
                {
                    var_Y = 0;
                }
            }

            this.Velocity = new Vector3(var_X, var_Y, 0);
            if (var_X != 0 || var_Y != 0)
            {
                Rectangle nextBounds = new Rectangle((int)(this.DrawBounds.Left + this.Velocity.X), (int)(this.DrawBounds.Top + this.Velocity.Y), this.DrawBounds.Width, this.DrawBounds.Height);
                List<LivingObject> objectsColliding = GameLibrary.Model.Map.World.World.world.getObjectsColliding(nextBounds); // World.getObjectsColliding(nextBounds);
                objectsColliding.Remove(this as LivingObject);
                if (objectsColliding.Count < 1)
                {
                    this.Position += this.Velocity;
                    EventHandler handler = this.ObjectMoves;
                    if (handler != null)
                    {
                        handler(this, EventArgs.Empty);
                    }
                }
                if (this.Position.X < 0)
                    this.Position += new Vector3(0 - this.Position.X, 0, 0);
                if (this.Position.Y < 0)
                    this.Position += new Vector3(0, 0 - this.Position.Y, 0);
            }


            if (this.Velocity.X != 0 || this.Velocity.Y != 0)
            {
                if (this.animation is Animation.Animations.MoveAnimation)
                {
                }
                else
                {
                    if (this.animation.finishedAnimation())
                    {
                        this.animation = new Animation.Animations.MoveAnimation(this);
                    }
                }
            }
            else
            {
            }
        }

        public void ChangeDirection(Vector3 _TargetPosition)
        {
            if (_TargetPosition.X < this.Position.X)
            {
                this.directionEnum = ObjectEnums.DirectionEnum.Left;
            }
            else if (_TargetPosition.X > this.Position.X)
            {
                this.directionEnum = ObjectEnums.DirectionEnum.Right;
            }
            else if (_TargetPosition.Y < this.Position.Y)
            {
                this.directionEnum = ObjectEnums.DirectionEnum.Top;
            }
            else if (_TargetPosition.Y > this.Position.Y)
            {
                this.directionEnum = ObjectEnums.DirectionEnum.Down;
            }
        }

        public virtual void draw(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch, Vector3 _DrawPositionExtra, Color _Color)
        {
            Vector3 var_DrawPositionExtra = Vector3.Zero;
            if(this.animation != null)
                var_DrawPositionExtra = this.animation.drawPositionExtra();
            //TODO: An das Attribut Scale anpassen
            Vector2 var_Position = new Vector2(this.Position.X + _DrawPositionExtra.X - this.Size.X/2, this.Position.Y + _DrawPositionExtra.Y - this.Size.Y) + new Vector2(var_DrawPositionExtra.X, var_DrawPositionExtra.Y);

            if (this.animation != null && !this.animation.graphicPath().Equals(""))
            {
                _SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.animation.graphicPath()], var_Position, this.animation.sourceRectangle(), this.animation.drawColor(), 0f, Vector2.Zero, new Vector2(this.scale, this.scale), SpriteEffects.None, this.layerDepth);//Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.animation.graphicPath()], var_Position, this.animation.sourceRectangle(), this.animation.drawColor(), this.scale, Vector2.Zero, null, this.layerDepth);
            }
            //_SpriteBatch.Draw(Ressourcen.RessourcenManager.ressourcenManager.Texture[this.animation.graphicPath()], var_Position, this.animation.sourceRectangle(), this.animation.drawColor(), 0, new Vector2(0,0), 1,SpriteEffects.None,this.layerDepth);
        
        }
    }
}
