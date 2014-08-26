using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GameLibrary.Model.Object.ObjectEnums;
using GameLibrary.Model.Object.Body;
using GameLibrary.Model.Map.Block;

namespace GameLibrary.Model.Object
{
    [Serializable()]
    public class AnimatedObject: Object
    {
        private Body.Body body;

        public Body.Body Body
        {
            get { return body; }
            set { body = value; }
        }

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
            set { 
                    
                    directionEnum = value; 
                }
        }

        public Rectangle DrawBounds
        {
            get
            {
                return new Rectangle((int)(this.Position.X - this.Size.X / 2), (int)(this.Position.Y - this.Size.Y), (int)this.Size.X, (int)this.Size.Y);
            }
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

        public AnimatedObject() : base()
        {
            this.body = new Body.Body();
            this.scale = 1f;
            this.Size = new Vector3(32, 32, 0);

            this.movementSpeed = 1.0f;
        }

        public AnimatedObject(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt)
        {
            this.scale = (float)info.GetValue("scale", typeof(float));
            this.movementSpeed = (float)info.GetValue("movementSpeed", typeof(float));

            this.directionEnum = (DirectionEnum)info.GetValue("directionEnum", typeof(DirectionEnum));

            this.body = (Body.Body)info.GetValue("body", typeof(Body.Body));
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            info.AddValue("scale", this.scale, typeof(float));
            info.AddValue("movementSpeed", this.movementSpeed, typeof(float));

            info.AddValue("directionEnum", this.directionEnum, typeof(DirectionEnum));

            info.AddValue("body", this.body, typeof(Body.Body));

            base.GetObjectData(info, ctxt);
        }

        public override void update()
        {
            base.update();
            this.body.update();
            /*if (this.animation != null)
            {
                this.animation.update();
                if (this.animation.finishedAnimation() || this.Velocity.Equals(Vector3.Zero))// && !(this.animation is Animation.Animations.MoveAnimation || this.Velocity != Vector3.Zero))
                {
                    this.animation = new Animation.Animations.StandAnimation(this);
                }
            }*/
            this.move();          
        }

        private void move()
        {
            float var_X = (-Convert.ToInt32(this.moveLeft) + Convert.ToInt32(this.moveRight)) * this.movementSpeed;
            float var_Y = (-Convert.ToInt32(this.moveUp) + Convert.ToInt32(this.moveDown)) * this.movementSpeed;

            Vector2 var_PositionBlockSizeOld = new Vector2((this.Position.X) / Map.Block.Block.BlockSize, (this.Position.Y) / Map.Block.Block.BlockSize);
            Vector2 var_PositionBlockSizeNew = new Vector2((this.Position.X + var_X) / Map.Block.Block.BlockSize, (this.Position.Y + var_Y) / Map.Block.Block.BlockSize);
            
            Map.Block.Block var_Block = this.CurrentBlock;

            if (Configuration.Configuration.isHost)
            {
                if ((int)var_PositionBlockSizeOld.X < (int)var_PositionBlockSizeNew.X)
                {
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
            }

            this.Velocity = new Vector3(var_X, var_Y, 0);
            if (var_X != 0 || var_Y != 0)
            {
                Rectangle nextBounds = new Rectangle((int)(this.DrawBounds.Left + this.Velocity.X), (int)(this.DrawBounds.Top + this.Velocity.Y), this.DrawBounds.Width, this.DrawBounds.Height);
                List<Object> objectsColliding = GameLibrary.Model.Map.World.World.world.getObjectsColliding(nextBounds); // World.getObjectsColliding(nextBounds);
                objectsColliding.Remove(this as LivingObject);
                if (objectsColliding.Count < 1)
                {
                    if (Configuration.Configuration.isHost)
                    {
                        this.Position += this.Velocity;
                        GameLibrary.Connection.Event.EventList.Add(new GameLibrary.Connection.Event(new GameLibrary.Connection.Message.UpdateObjectPositionMessage((LivingObject)this), GameLibrary.Connection.GameMessageImportance.VeryImportant));
                    }
                    checkChangedBlock();
                }
                else
                {
                    if (Configuration.Configuration.isHost)
                    {
                        foreach (AnimatedObject var_AnimatedObject in objectsColliding)
                        {
                            var_AnimatedObject.onCollide(this);
                            this.onCollide(var_AnimatedObject);
                        }
                    }
                }
            }


            if (this.Velocity.X != 0 || this.Velocity.Y != 0)
            {
                this.body.walk(this.Velocity);
                this.updateMovementDirection();
            }
            else
            {
                this.body.stopWalk();
            }
        }

        private void updateMovementDirection()
        {
            if (this.Velocity.X == 0 && this.Velocity.Y == 0)
            {

            }
            if (this.Velocity.X == 0)
            {
                if (this.Velocity.Y < 0)
                {
                    this.DirectionEnum = ObjectEnums.DirectionEnum.Top;
                }
                else if (this.Velocity.Y > 0)
                {
                    this.DirectionEnum = ObjectEnums.DirectionEnum.Down;
                }
            }
            else if (this.Velocity.X < 0)
            {
                this.DirectionEnum = ObjectEnums.DirectionEnum.Left;
                if (Math.Abs(this.Velocity.X) < Math.Abs(this.Velocity.Y))
                {
                    if (this.Velocity.Y < 0)
                    {
                        this.DirectionEnum = ObjectEnums.DirectionEnum.Top;
                    }
                    else if (this.Velocity.Y > 0)
                    {
                        this.DirectionEnum = ObjectEnums.DirectionEnum.Down;
                    }
                }
            }
            else if (this.Velocity.X > 0)
            {
                this.DirectionEnum = ObjectEnums.DirectionEnum.Right;
                if (Math.Abs(this.Velocity.X) < Math.Abs(this.Velocity.Y))
                {
                    if (this.Velocity.Y < 0)
                    {
                        this.DirectionEnum = ObjectEnums.DirectionEnum.Top;
                    }
                    else if (this.Velocity.Y > 0)
                    {
                        this.DirectionEnum = ObjectEnums.DirectionEnum.Down;
                    }
                }
            }
            this.body.setDirection(this.directionEnum);
        }

        public virtual void onCollide(AnimatedObject _CollideWith)
        {

        }

        public virtual void draw(GraphicsDevice _GraphicsDevice, SpriteBatch _SpriteBatch, Vector3 _DrawPositionExtra, Color _Color)
        {
            //TODO: An das Attribut Scale anpassen
            Vector2 var_Position = new Vector2(this.Position.X + _DrawPositionExtra.X - this.Size.X/2, this.Position.Y + _DrawPositionExtra.Y - this.Size.Y);

            /*if (this.animation.drawColor() != Color.White)
            {
                var_DrawColor = Color.Lerp(this.objectDrawColor, this.animation.drawColor(), 0.1f);
            }*/

            this.body.draw(_GraphicsDevice, _SpriteBatch, var_Position);
        }

        public virtual void onChangedBlock()
        {
        }

        public virtual void onChangedChunk()
        {
        }

        public void checkChangedBlock()
        {
            //TODO: Methode vebessern, über world get block usw ... vll ;) vll ist das ja auch nicht schneller :D
            if (this.CurrentBlock != null)
            {
                bool var_BlockChanged = false;
                Block var_OldBlock = this.CurrentBlock;

                int var_BlockPosX = (int)this.CurrentBlock.Position.X / Map.Block.Block.BlockSize;
                int var_BlockPosY = (int)this.CurrentBlock.Position.Y / Map.Block.Block.BlockSize;

                Vector3 var_Position = this.Position;

                if (var_Position.X < var_BlockPosX * Map.Block.Block.BlockSize)
                {
                    this.CurrentBlock.removeObject(this);
                    if (this.CurrentBlock.LeftNeighbour != null)
                    {
                        ((Map.Block.Block)this.CurrentBlock.LeftNeighbour).addObject(this);
                        var_BlockChanged = true;
                    }
                }
                else if (var_Position.X > (var_BlockPosX + 1) * Map.Block.Block.BlockSize)
                {
                    this.CurrentBlock.removeObject((LivingObject)this);
                    if (this.CurrentBlock.RightNeighbour != null)
                    {
                        ((Map.Block.Block)this.CurrentBlock.RightNeighbour).addObject(this);
                        var_BlockChanged = true;
                    }
                }
                else if (var_Position.Y < var_BlockPosY * Map.Block.Block.BlockSize)
                {
                    this.CurrentBlock.removeObject((LivingObject)this);
                    if (this.CurrentBlock.TopNeighbour != null)
                    {
                        ((Map.Block.Block)this.CurrentBlock.TopNeighbour).addObject(this);
                        var_BlockChanged = true;
                    }
                }
                else if (var_Position.Y > (var_BlockPosY + 1) * Map.Block.Block.BlockSize)
                {
                    this.CurrentBlock.removeObject((LivingObject)this);
                    if (this.CurrentBlock.BottomNeighbour != null)
                    {
                        ((Map.Block.Block)this.CurrentBlock.BottomNeighbour).addObject(this);
                        var_BlockChanged = true;
                    }
                }

                if (var_BlockChanged)
                {
                    this.onChangedBlock();
                    if (var_OldBlock.Parent != this.CurrentBlock.Parent)
                    {
                        this.onChangedChunk();
                    }
                }
            }
            else
            {
                Logger.Logger.LogErr("AnimatedObject->checkChangedBlock() : this.CurrentBlock = null");
            }
        }
    }
}
