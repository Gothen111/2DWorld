using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        protected GenderEnum gender;

        public GenderEnum Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        //protected InterAction interAction; //???

        private List<LivingObjectTask> tasks;

        protected List<LivingObjectTask> Tasks
        {
            get { return tasks; }
            set { tasks = value; }
        }

        private Path.Path path;

        protected Path.Path Path
        {
            get { return path; }
            set { path = value; }
        }

        public LivingObject()
            : base()
        {
            tasks = new List<LivingObjectTask>();
            path = null; // ???
        }

        public override void update()
        {
            base.update();
            this.doTasks();
        }

        private void doTasks()
        {
        }
    }
}
