using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Client.Factories.FactoryEnums;
using Client.Model.Object.Interaction;
//using Server.Model.Object.Task.Tasks;

namespace Client.Model.Object
{
    [Serializable()]
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

        private List<LivingObjectInteraction> interactions;

        internal List<LivingObjectInteraction> Interactions
        {
            get { return interactions; }
            set { interactions = value; }
        }

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
            this.aggroRange = 200;
            this.isDead = false;
            MovementSpeed = 1f;
            path = null; // ???
            this.interactions = new List<LivingObjectInteraction>();
        }

        public LivingObject(SerializationInfo info, StreamingContext ctxt) : base(info, ctxt)
        {
            this.healthPoints = (float)info.GetValue("healthPoints", typeof(float));
            this.maxHealthPoints = (float)info.GetValue("maxHealthPoints", typeof(float));
            this.aggroRange = (float)info.GetValue("aggroRange", typeof(float));

            this.isDead = (bool)info.GetValue("isDead", typeof(bool));
            this.canBeEffected = (bool)info.GetValue("canBeEffected", typeof(bool));

            this.gender = (GenderEnum)info.GetValue("gender", typeof(GenderEnum));

            //TODO: Verändere Methoden, sodass Interaction und Tasks nicht mehr den Owner speichern, sonst gibt es eine Kettenspeicherung bei Serialisierung
            //this.interactions = (List<LivingObjectInteraction>)info.GetValue("interactions", typeof(List<LivingObjectInteraction>));

            //this.tasks = (List<LivingObjectTask>)info.GetValue("tasks", typeof(List<LivingObjectTask>));
        }

        public virtual void GetObjectData(SerializationInfo info, StreamingContext ctxt)
        {
            base.GetObjectData(info, ctxt);

            info.AddValue("healthPoints", this.healthPoints, typeof(float));
            info.AddValue("maxHealthPoints", this.maxHealthPoints, typeof(float));
            info.AddValue("aggroRange", this.aggroRange, typeof(float));

            info.AddValue("isDead", this.isDead, typeof(bool));
            info.AddValue("canBeEffected", this.canBeEffected, typeof(bool));

            info.AddValue("gender", this.gender, typeof(GenderEnum));

            //info.AddValue("interactions", this.interactions, typeof(List<LivingObjectInteraction>));

            //info.AddValue("tasks", this.tasks, typeof(List<LivingObjectTask>));
        }

        public override void update()
        {
            base.update();
        }


        
        public void getInteracted(LivingObject _Interactor)
        {
            foreach (LivingObjectInteraction var_Interaction in this.interactions)
            {
                var_Interaction.doInteraction(_Interactor);
            }
        }

        public void interact()
        {
            List<LivingObject> var_LivingObjects = Model.Map.World.World.world.getObjectsInRange(this.Position, this.Size.X + 5);
            var_LivingObjects.Remove(this);
            foreach (LivingObject var_LivingObject in var_LivingObjects)
            {
                var_LivingObject.getInteracted(this);
            }
        }

        public virtual void attack()
        {
        }

        public void attackLivingObject(LivingObject _Target, int _Damage)
        {
            //ChangeDirection(_Target.Position);
            this.Animation = new Animation.Animations.AttackAnimation(this);
            _Target.onAttacked(this, _Damage);
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
            Vector3 knockBackVector = this.Position - _Attacker.Position;
            knockBackVector.X = knockBackVector.X / knockBackVector.Length() * 20;
            knockBackVector.Y = knockBackVector.Y / knockBackVector.Length() * 20;
            knockBackVector.Z = knockBackVector.Z / knockBackVector.Length() * 20;
            this.knockBack(knockBackVector);
        }

        public void damage(int _DamageAmount)
        {
            this.healthPoints -= _DamageAmount;
            if (this.healthPoints <= 0 && !this.isDead)
            {
                this.isDead = true;
                try
                {
                    Texture2D texture = Ressourcen.RessourcenManager.ressourcenManager.Texture[this.GraphicPath + "_Dead"];
                    this.GraphicPath = this.GraphicPath + "_Dead";
                    this.CurrentBlock.objectsPreEnviorment.Add(this);
                    this.CurrentBlock.removeLivingObject(this);
                }
                catch (Exception e)
                {
                    Model.Map.World.World.world.removeObjectFromWorld(this);
                }
            }
            else if (this.healthPoints <= 0)
            {
                Model.Map.World.World.world.removeObjectFromWorld(this);
            }
            else
            {
                if (this.canBeEffected)
                {
                    this.Animation = new Model.Object.Animation.Animations.TakeDamageAnimation(this);
                }
            }
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

                Rectangle lifebarBounds = new Rectangle((int)(this.Position.X + Ressourcen.RessourcenManager.ressourcenManager.Texture[this.GraphicPath].Bounds.Width / 2 - lifebarWidth / 2 - this.Size.X / 2), (int)(this.Position.Y - 5 - this.Size.Y), (int)lifebarWidth / 2, lifebar.Bounds.Height / 2);

                _SpriteBatch.Draw(lifebar, lifebarBounds, Color.White);
            }

            base.draw(_GraphicsDevice, _SpriteBatch, _DrawPositionExtra, _Color);
        }
    }
}
