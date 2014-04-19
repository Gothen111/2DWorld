using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Map.World;
using Server.Model.Map.Chunk;
using Server.Model.Map.Block;
using Server.Commands.CommandTypes;
using Microsoft.Xna.Framework;

namespace Server.Model.Object.Task.Tasks
{
    class WalkRandomTask : LivingObjectTask
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

        public WalkRandomTask(LivingObject _TaskOwner, CommandPriority _Priority)
            : base(_TaskOwner, _Priority)
        {
            this.finishedWalking = false;
            Chunk var_Chunk = this.TaskOwner.World.getRegionLivingObjectIsIn(this.TaskOwner).getChunkLivingObjectIsIn(TaskOwner);
            Block var_Block = var_Chunk.getBlockAtPosition((float)Util.Random.GenerateGoodRandomNumber(0, (int)var_Chunk.Size.X-1), (float)Util.Random.GenerateGoodRandomNumber(0, (int)var_Chunk.Size.Y-1));
            targetPosition = new Vector3(var_Block.Position.X * Block.BlockSize, var_Block.Position.Y * Block.BlockSize, 0);
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
                Chunk var_Chunk = this.TaskOwner.World.getRegionLivingObjectIsIn(this.TaskOwner).getChunkLivingObjectIsIn(TaskOwner);
                Block var_Block = var_Chunk.getBlockAtPosition((float)Util.Random.GenerateGoodRandomNumber(0, (int)var_Chunk.Size.X-1), (float)Util.Random.GenerateGoodRandomNumber(0, (int)var_Chunk.Size.Y-1));
                targetPosition = new Vector3(var_Block.Position.X * Block.BlockSize, var_Block.Position.Y * Block.BlockSize, 0);
                this.finishedWalking = false;
            }
            else
            {
                float movementSpeed = this.TaskOwner.MovementSpeed;
                Vector3 var_Pos = this.TaskOwner.Position;
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
                this.TaskOwner.Move(var_Pos);
            }
        }
    }
}
