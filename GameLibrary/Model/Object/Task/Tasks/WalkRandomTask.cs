using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Map.World;
using GameLibrary.Model.Map.Chunk;
using GameLibrary.Model.Map.Region;
using GameLibrary.Model.Map.Block;
using GameLibrary.Commands.CommandTypes;
using Microsoft.Xna.Framework;

namespace GameLibrary.Model.Object.Task.Tasks
{
    public class WalkRandomTask : LivingObjectTask
    {
        private bool finishedWalking;

        private float range;

        public float Range
        {
            get { return range; }
            set { range = value; }
        }

        private Vector3 targetPosition;

        public Vector3 TargetPosition
        {
            get { return targetPosition; }
            set { targetPosition = value; }
        }

        public WalkRandomTask(LivingObject _TaskOwner, TaskPriority _Priority)
            : base(_TaskOwner, _Priority)
        {
            this.finishedWalking = false;
            Chunk var_Chunk = Model.Map.World.World.world.getRegionLivingObjectIsIn(this.TaskOwner).getChunkLivingObjectIsIn(TaskOwner);
            Block var_Block = var_Chunk.getBlockAtPosition((float)Util.Random.GenerateGoodRandomNumber(0, (int)var_Chunk.Size.X-1), (float)Util.Random.GenerateGoodRandomNumber(0, (int)var_Chunk.Size.Y-1));
            targetPosition = new Vector3(var_Block.Position.X, var_Block.Position.Y, 0);
        }

        public override bool wantToDoTask()
        {
            bool var_wantToDoTask = true;

            return var_wantToDoTask || base.wantToDoTask();
        }

        public override void update()
        {
            base.update();
            walkRandom();
        }

        private void walkRandom()
        {
            if (this.finishedWalking)
            {
                Region var_Region = Model.Map.World.World.world.getRegionLivingObjectIsIn(this.TaskOwner);
                if (var_Region != null)
                {
                    Chunk var_Chunk = var_Region.getChunkLivingObjectIsIn(TaskOwner);
                    if (var_Chunk != null)
                    {
                        Block var_Block = var_Chunk.getBlockAtPosition((float)Util.Random.GenerateGoodRandomNumber((int)var_Chunk.Position.X, (int)(var_Chunk.Position.X + var_Chunk.Size.X - 1)), (float)Util.Random.GenerateGoodRandomNumber((int)var_Chunk.Position.Y, (int)(var_Chunk.Position.Y + var_Chunk.Size.Y - 1)));
                        targetPosition = new Vector3(var_Block.Position.X, var_Block.Position.Y, 0);
                        this.finishedWalking = false;
                    }
                }
            }
            else
            {
                float movementSpeed = this.TaskOwner.MovementSpeed;
                Vector3 var_Pos = Vector3.Zero;
                if (Math.Abs(this.TaskOwner.Position.X - targetPosition.X) > 1)
                {
                    this.finishedWalking = false;
                    if (this.TaskOwner.Position.X < targetPosition.X)
                    {
                        if (Math.Abs(this.TaskOwner.Position.Y - targetPosition.Y) > 1)
                        {
                            var_Pos += new Vector3(movementSpeed / 2, 0, 0);
                        }
                        else
                        {
                            var_Pos += new Vector3(movementSpeed, 0, 0);
                        }
                    }
                    else
                    {
                        if (Math.Abs(this.TaskOwner.Position.Y - targetPosition.Y) > 1)
                        {
                            var_Pos -= new Vector3(movementSpeed / 2, 0, 0);
                        }
                        else
                        {
                            var_Pos -= new Vector3(movementSpeed, 0, 0);
                        }
                    }
                }
                else
                {
                    this.finishedWalking = true;
                }

                if (Math.Abs(this.TaskOwner.Position.Y - targetPosition.Y) > 1)
                {
                    this.finishedWalking = false;
                    if (this.TaskOwner.Position.Y < targetPosition.Y)
                    {
                        if (Math.Abs(this.TaskOwner.Position.X - targetPosition.X) > 1)
                        {
                            var_Pos += new Vector3(0, movementSpeed / 2, 0);
                        }
                        else
                        {
                            var_Pos += new Vector3(0, movementSpeed, 0);
                        }
                    }
                    else
                    {
                        if (Math.Abs(this.TaskOwner.Position.X - targetPosition.X) > 1)
                        {
                            var_Pos -= new Vector3(0, movementSpeed / 2, 0);
                        }
                        else
                        {
                            var_Pos -= new Vector3(0, movementSpeed, 0);
                        }
                    }
                }
                else
                {
                    this.finishedWalking = true;
                }
                //this.TaskOwner.Move(var_Pos);
                this.TaskOwner.Velocity = var_Pos;
            }
        }
    }
}
