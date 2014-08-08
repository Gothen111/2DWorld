using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using GameLibrary.Model.Map.World;
using GameLibrary.Model.Map.Chunk;
using GameLibrary.Commands.CommandTypes;

namespace GameLibrary.Model.Object.Task.Tasks
{
    public class AttackRandomTask : LivingObjectTask
    {
        private LivingObject target;

        private int attackSpeed = 0;
        private int attackSpeedMax = 50;

        private float updateTarget = 40;
        private float updateTargetMax = 40;

        private float updatePathToTarget = 0;
        private float updatePathToTargetMax = 50;

        private bool wantToDoTaskCheck = true;
        private float updateWantToDo = 20;

        public AttackRandomTask()
        {

        }

        public AttackRandomTask(LivingObject _TaskOwner, TaskPriority _Priority) : base(_TaskOwner, _Priority)
        {
            target = null;
        }

        public override bool wantToDoTask()
        {
            if (updateWantToDo <= 0)
            {
                wantToDoTaskCheck = true;
                List<Object> var_Objects = Model.Map.World.World.world.getObjectsInRange(this.TaskOwner.Position, this.TaskOwner.AggroRange);
                if (var_Objects.Count <= 1)
                    wantToDoTaskCheck = false;
                updateWantToDo = 20;
            }
            else
            {
                updateWantToDo--;
            }

            return wantToDoTaskCheck || base.wantToDoTask();
        }

        public override void update()
        {
            base.update();

            this.attackSpeed -= 1;

            if (this.target == null)
            {
                if (this.updateTarget > 0)
                {
                    this.updateTarget--;
                }
                else
                {
                    List<Object> var_Objects = Model.Map.World.World.world.getObjectsInRange(this.TaskOwner.Position, this.TaskOwner.AggroRange);
                    var_Objects.Remove(this.TaskOwner);
                    if (var_Objects.Count > 0)
                    {
                        foreach (Object var_Object in var_Objects)
                        {
                            if (var_Object is LivingObject)
                            {
                                this.TaskOwner.AggroSystem.addAggro((LivingObject)var_Object, this.TaskOwner.AggroRange - Vector3.Distance(this.TaskOwner.Position, var_Object.Position));
                            }
                        }
                        target = this.TaskOwner.AggroSystem.getTarget();
                        if (target == this.TaskOwner)
                        {
                            Logger.Logger.LogDeb("AttackTask->update(): Target is TaskOwner: Should not be possible!");
                            target = null;
                        }
                    }
                    var_Objects.Clear();
                    this.target = this.TaskOwner.AggroSystem.getTarget();
                    this.updateTarget = this.updateTargetMax;
                }
            }
            else
            {
                if (target.IsDead)
                {
                    List<Object> var_Objects = Model.Map.World.World.world.getObjectsInRange(this.TaskOwner.Position, this.TaskOwner.AggroRange);
                    var_Objects.Remove(this.TaskOwner);
                    if (var_Objects.Count > 0)
                    {
                        foreach (Object var_Object in var_Objects)
                        {
                            if (var_Object is LivingObject)
                            {
                                this.TaskOwner.AggroSystem.addAggro((LivingObject)var_Object, this.TaskOwner.AggroRange - Vector3.Distance(this.TaskOwner.Position, var_Object.Position));
                            }
                        }
                        target = this.TaskOwner.AggroSystem.getTarget();
                        if (target == this.TaskOwner)
                        {
                            Logger.Logger.LogDeb("AttackTask->update(): Target is TaskOwner: Should not be possible!");
                            target = null;
                        }
                    }
                    var_Objects.Clear();
                }
            }
            if(target!=null)
            {
                target = this.TaskOwner.AggroSystem.getTarget();
                if (target == null || target.IsDead)
                {
                    target = null;
                    return;
                }
                if (this.updatePathToTarget > 0)
                {
                    this.updatePathToTarget--;
                }
                else
                {                   
                    this.TaskOwner.Path = GameLibrary.Model.Path.PathFinder.generatePath(new Vector2(this.TaskOwner.Position.X, this.TaskOwner.Position.Y), new Vector2(this.target.Position.X, this.target.Position.Y));
                    if (this.TaskOwner.Path != null)
                    {

                    }
                    this.updatePathToTarget = this.updatePathToTargetMax;
                }

                /*
                this.TaskOwner.MoveRight = false;
                this.TaskOwner.MoveLeft = false;
                this.TaskOwner.MoveDown = false;
                this.TaskOwner.MoveUp = false;
                if (this.target.Position.X > this.TaskOwner.Position.X + Map.Block.Block.BlockSize)
                {
                    this.TaskOwner.MoveRight = true;
                }
                else if (this.target.Position.X < this.TaskOwner.Position.X - Map.Block.Block.BlockSize)
                {
                    this.TaskOwner.MoveLeft = true;
                }
                if (this.target.Position.Y > this.TaskOwner.Position.Y + Map.Block.Block.BlockSize)
                {
                    this.TaskOwner.MoveDown = true;
                }
                else if (this.target.Position.Y < this.TaskOwner.Position.Y - Map.Block.Block.BlockSize)
                {
                    this.TaskOwner.MoveUp = true;
                }

                if (!this.TaskOwner.MoveRight && !this.TaskOwner.MoveLeft && !this.TaskOwner.MoveDown && !this.TaskOwner.MoveUp)
                {
                    if (attackSpeed <= 0)
                    {
                        this.TaskOwner.attack();
                        this.attackSpeed = this.attackSpeedMax;
                    }
                }
                */
                
                if (Vector3.Distance(this.TaskOwner.Position, this.target.Position) <= 35)
                {
                    if (attackSpeed <= 0)
                    {
                        this.TaskOwner.attack();
                        this.attackSpeed = this.attackSpeedMax;
                    }
                }
            }
        }
    }
}
