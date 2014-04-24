using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Server.Model.Map.World;
using Server.Model.Map.Chunk;
using Server.Commands.CommandTypes;

namespace Server.Model.Object.Task.Tasks
{
    class AttackRandomTask : LivingObjectTask
    {
        private LivingObject target;

        private int attackSpeed = 0;
        private int attackSpeedMax = 50;

        public AttackRandomTask(LivingObject _TaskOwner, TaskPriority _Priority)
            : base(_TaskOwner, _Priority)
        {
            target = null;
        }

        public override bool wantToDoTask()
        {
            Chunk var_Chunk = this.TaskOwner.World.getRegionLivingObjectIsIn(this.TaskOwner).getChunkLivingObjectIsIn(TaskOwner);
            bool var_wantToDoTask = false;
            if (var_Chunk != null)
            {
                var_wantToDoTask = true;
                List<LivingObject> var_LivingObjects = var_Chunk.getAllLivingObjectsinChunk();
                for (int x = var_LivingObjects.Count - 1; x >= 0; x--)
                {
                    LivingObject var_LivingObject = var_LivingObjects.ElementAt(x);
                    if (getDistanceToObject(var_LivingObject) > this.TaskOwner.AggroRange)
                        var_LivingObjects.Remove(var_LivingObject);
                }


                if (var_LivingObjects.Count <= 1)
                    var_wantToDoTask = false;
            }

            return var_wantToDoTask || base.wantToDoTask();
        }

        public double getDistanceToObject(LivingObject _Object)
        {
            double distance = Math.Sqrt(Math.Pow(this.TaskOwner.Position.X - _Object.Position.X, 2) + Math.Pow(this.TaskOwner.Position.Y - _Object.Position.Y, 2));
            return distance;
        }

        public override void update()
        {
            base.update();

            this.attackSpeed -= 1;

            if (target == null)
            {

                List<LivingObject> var_LivingObjects = this.TaskOwner.World.getRegionLivingObjectIsIn(this.TaskOwner).getChunkLivingObjectIsIn(TaskOwner).getAllLivingObjectsinChunk();
                var_LivingObjects.Remove(this.TaskOwner);
                if (var_LivingObjects.Count > 0)
                {
                    target = var_LivingObjects.ElementAt(Util.Random.GenerateGoodRandomNumber(0, var_LivingObjects.Count));
                    if (target == this.TaskOwner)
                    {
                        Logger.Logger.LogDeb("AttackTask->update(): Target is TaskOwner: Should not be possible!");
                        target = null;
                    }
                }
            }
            else
            {
                if (target.IsDead)
                {
                    List<LivingObject> var_LivingObjects = this.TaskOwner.World.getRegionLivingObjectIsIn(this.TaskOwner).getChunkLivingObjectIsIn(TaskOwner).getAllLivingObjectsinChunk();
                    var_LivingObjects.Remove(this.TaskOwner);
                    for (int x = var_LivingObjects.Count - 1; x >= 0; x--)
                    {
                        LivingObject var_LivingObject = var_LivingObjects.ElementAt(x);
                        if (getDistanceToObject(var_LivingObject) > this.TaskOwner.AggroRange)
                            var_LivingObjects.Remove(var_LivingObject);
                    }
                    if (var_LivingObjects.Count > 0)
                    {
                        target = var_LivingObjects.ElementAt(Util.Random.GenerateGoodRandomNumber(0, var_LivingObjects.Count));
                    }
                }
            }
            if(target!=null)
            {
                if (target.IsDead)
                {
                    target = null;
                    return;
                }
                float var_X = 0;
                float var_Y = 0;
                if (this.target.Position.X > this.TaskOwner.Position.X + Map.Block.Block.BlockSize)
                {
                    var_X += 0.9f;
                }
                else if (this.target.Position.X < this.TaskOwner.Position.X - Map.Block.Block.BlockSize)
                {
                    var_X -= 0.9f;
                }
                if (this.target.Position.Y > this.TaskOwner.Position.Y + Map.Block.Block.BlockSize)
                {
                    var_Y += 0.9f;
                }
                else if (this.target.Position.Y < this.TaskOwner.Position.Y - Map.Block.Block.BlockSize)
                {
                    var_Y -= 0.9f;
                }

                if (var_X == 0 && var_Y == 0)
                {
                    if (attackSpeed <= 0)
                    {
                        this.TaskOwner.attackLivingObject(this.target);
                        this.attackSpeed = this.attackSpeedMax;
                    }
                }
                this.TaskOwner.Velocity = new Vector3(var_X, var_Y, 0);
            }
        }
    }
}
