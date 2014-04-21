using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Server.Factories.FactoryEnums;
using Server.Model.Object.Task;
//using Server.Model.Object.Task.Tasks;

namespace Server.Model.Object
{
    class LivingObject : AnimatedObject
    {
        private float healthPoints;

        public float HealthPoints
        {
            get { return healthPoints; }
            set { healthPoints = value; }
        }

        private bool isDead;

        public bool IsDead
        {
            get { return isDead; }
            set { isDead = value; }
        }

        protected GenderEnum gender;

        public GenderEnum Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        //protected InterAction interAction; //???

        private List<LivingObjectTask> tasks;

        public List<LivingObjectTask> Tasks
        {
            get { return tasks; }
            set { tasks = value; }
        }

        private LivingObjectTask currentTask;

        private Path.Path path;

        protected Path.Path Path
        {
            get { return path; }
            set { path = value; }
        }

        public LivingObject()
            : base()
        {
            this.healthPoints = 20;
            this.isDead = false;
            tasks = new List<LivingObjectTask>();
            MovementSpeed = 1f;
            path = null; // ???
            currentTask = null;
        }

        public override void update()
        {
            base.update();
            this.doTasks();
        }

        private void doTasks()
        {
            if(currentTask!=null)
            {
                if(currentTask.wantToDoTask())
                {
                }
                else
                {
                    currentTask = null;
                }
            }
            LivingObjectTask var_TaskWantToDo = null;
            foreach (LivingObjectTask var_Task in this.tasks)
            {
                if (var_Task.wantToDoTask())
                {
                    if (currentTask == null)
                    {
                        if (var_TaskWantToDo == null)
                        {
                            var_TaskWantToDo = var_Task;
                        }
                        else if (var_TaskWantToDo.Priority < var_Task.Priority)
                        {
                            var_TaskWantToDo = var_Task;
                        }
                    }
                    else
                    {
                        if (var_TaskWantToDo == null)
                        {
                            if (this.currentTask.Priority < var_Task.Priority)
                            {
                                var_TaskWantToDo = var_Task;
                            }
                        }
                        else if (var_TaskWantToDo.Priority < var_Task.Priority)
                        {
                            var_TaskWantToDo = var_Task;
                        }
                    }
                }
            }
            if (var_TaskWantToDo != null)
            {
                this.currentTask = var_TaskWantToDo;
            }
            if (this.currentTask != null)
            {
                this.currentTask.update();
            }
        }

        public void attackLivingObject(LivingObject _Target)
        {
            ChangeDirection(_Target.Position);
            _Target.onAttacked(this, 2);
        }

        public void MoveWithoutDirectionChange(Vector3 _TargetPosition)
        {
            this.Position = _TargetPosition;
        }

        public void Move(Vector3 _TargetPosition)
        {
            ChangeDirection(_TargetPosition);
            this.Position = _TargetPosition;
        }

        public virtual void onAttacked(LivingObject _Attacker, int _DamageAmount)
        {
            this.damage(_DamageAmount);
            if (_Attacker.directionEnum == ObjectEnums.DirectionEnum.Down)
            {
                this.knockBack(new Vector3(0,20,0));
            }
            if (_Attacker.directionEnum == ObjectEnums.DirectionEnum.Left)
            {
                this.knockBack(new Vector3(-20, 0, 0));
            }
            if (_Attacker.directionEnum == ObjectEnums.DirectionEnum.Right)
            {
                this.knockBack(new Vector3(20, 0, 0));
            }
            if (_Attacker.directionEnum == ObjectEnums.DirectionEnum.Top)
            {
                this.knockBack(new Vector3(0, -20, 0));
            }
        }

        public void damage(int _DamageAmount)
        {
            this.healthPoints -= _DamageAmount;
            if (this.healthPoints <= 0)
            {
                this.isDead = true;
            }
            this.Animation = new Server.Model.Object.Animation.Animations.TakeDamageAnimation();
        }

        public void knockBack(Vector3 _KnockBackAmount)
        {
            this.Position += _KnockBackAmount;
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch, Vector3 _DrawPositionExtra, Color _Color)
        {
            base.draw(_GraphicsDevice, _SpriteBatch, _DrawPositionExtra, _Color);
        }
    }
}
