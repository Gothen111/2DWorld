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

        private float maxHealthPoints;

        public float MaxHealthPoints
        {
            get { return maxHealthPoints; }
            set { maxHealthPoints = value; }
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

        private float aggroRange;

        public float AggroRange
        {
            get { return aggroRange; }
            set { aggroRange = value; }
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

        private bool canBeEffected; // Wie vergiften oder knockback usw.....

        public bool CanBeEffected
        {
            get { return canBeEffected; }
            set { canBeEffected = value; }
        }

        public LivingObject()
            : base()
        {
            this.healthPoints = 20;
            this.maxHealthPoints = 20;
            this.aggroRange = 600;
            this.isDead = false;
            tasks = new List<LivingObjectTask>();
            MovementSpeed = 1f;
            path = null; // ???
            currentTask = null;
            this.canBeEffected = true;
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
            this.Animation = new Animation.Animations.AttackAnimation(this);
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
            if (_Attacker.DirectionEnum == ObjectEnums.DirectionEnum.Down)
            {
                this.knockBack(new Vector3(0,20,0));
            }
            if (_Attacker.DirectionEnum == ObjectEnums.DirectionEnum.Left)
            {
                this.knockBack(new Vector3(-20, 0, 0));
            }
            if (_Attacker.DirectionEnum == ObjectEnums.DirectionEnum.Right)
            {
                this.knockBack(new Vector3(20, 0, 0));
            }
            if (_Attacker.DirectionEnum == ObjectEnums.DirectionEnum.Top)
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
            this.Animation = new Server.Model.Object.Animation.Animations.TakeDamageAnimation(this);
        }

        public void knockBack(Vector3 _KnockBackAmount)
        {
            if (this.canBeEffected)
            {
                this.Position += _KnockBackAmount;
            }
        }

        public override void draw(Microsoft.Xna.Framework.Graphics.GraphicsDevice _GraphicsDevice, Microsoft.Xna.Framework.Graphics.SpriteBatch _SpriteBatch, Vector3 _DrawPositionExtra, Color _Color)
        {
            if (this.healthPoints < this.maxHealthPoints)
            {
                Texture2D lifebar = Ressourcen.RessourcenManager.ressourcenManager.Texture["Character/Lifebar"];
                float lifePercentage = this.healthPoints / this.maxHealthPoints;
                float lifebarWidth = lifebar.Bounds.Width * lifePercentage;

                Rectangle lifebarBounds = new Rectangle((int)(this.Position.X + Ressourcen.RessourcenManager.ressourcenManager.Texture[this.GraphicPath].Bounds.Width / 2 - lifebarWidth / 2), (int)(this.Position.Y - 5), (int)lifebarWidth / 2, lifebar.Bounds.Height / 2);

                _SpriteBatch.Draw(lifebar, lifebarBounds, Color.White);
            }

            base.draw(_GraphicsDevice, _SpriteBatch, _DrawPositionExtra, _Color);
        }
    }
}
