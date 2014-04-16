using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Server.Model.Map.World;
using Server.Model.Map.Chunk;

namespace Server.Model.Object.Task.Tasks
{
    class AttackTask : LivingObjectTask
    {
        private LivingObject target;

        private int attackSpeed = 0;
        private int attackSpeedMax = 50;

        public AttackTask(LivingObject _TaskOwner, int _Priority)
            : base(_TaskOwner, _Priority)
        {
            target = null;
        }

        public override bool wantToDoTask()
        {
            bool var_wantToDoTask = true;

            return var_wantToDoTask || base.wantToDoTask();
        }

        public override void update()
        {
            base.update();

            this.attackSpeed -= 1;

            if (target == null)
            {
                Chunk var_Chunk = this.TaskOwner.World.getRegionLivingObjectIsIn(this.TaskOwner).getChunkLivingObjectIsIn(TaskOwner);
                List<LivingObject> var_LivingObjects = var_Chunk.getAllLivingObjectsinChunk();
                var_LivingObjects.Remove(this.TaskOwner);
                if (var_LivingObjects.Count > 0)
                {
                    target = var_LivingObjects.ElementAt(Util.Random.GenerateGoodRandomNumber(0, var_LivingObjects.Count));
                }
            }
            else
            {
                if (target.IsDead)
                {
                    Chunk var_Chunk = this.TaskOwner.World.getRegionLivingObjectIsIn(this.TaskOwner).getChunkLivingObjectIsIn(TaskOwner);
                    List<LivingObject> var_LivingObjects = var_Chunk.getAllLivingObjectsinChunk();
                    var_LivingObjects.Remove(this.TaskOwner);
                    if (var_LivingObjects.Count > 0)
                    {
                        target = var_LivingObjects.ElementAt(Util.Random.GenerateGoodRandomNumber(0, var_LivingObjects.Count));
                    }
                }
            }
            if(target!=null)
            {
                
                float var_X = 0;
                float var_Y = 0;
                if(target.Position.X > this.TaskOwner.Position.X + Map.Block.Block.BlockSize)
                {
                    var_X += 0.9f;
                }
                else if (target.Position.X < this.TaskOwner.Position.X - Map.Block.Block.BlockSize)
                {
                    var_X -= 0.9f;
                }
                if (target.Position.Y > this.TaskOwner.Position.Y + Map.Block.Block.BlockSize)
                {
                    var_Y += 0.9f;
                }
                else if (target.Position.Y < this.TaskOwner.Position.Y - Map.Block.Block.BlockSize)
                {
                    var_Y -= 0.9f;
                }

                if (var_X == 0 && var_Y == 0)
                {
                    if (attackSpeed <= 0)
                    {
                        this.target.damage(2);
                        this.attackSpeed = this.attackSpeedMax;
                    }
                }
                this.TaskOwner.Velocity = new Vector3(var_X, var_Y, 0);
            }
        }
    }
}
