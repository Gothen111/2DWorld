using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Object;

namespace Server.Commands.CommandTypes
{
    class AttackCommand : Command<CommandManager>
    {
        private LivingObject attackTarget, attackActor;

        internal LivingObject AttackActor
        {
            get { return attackActor; }
            set { attackActor = value; }
        }

        protected LivingObject AttackTarget
        {
            get { return attackTarget; }
            set { attackTarget = value; }
        }


        public AttackCommand(LivingObject _attackActor,LivingObject _attackTarget) : base(CommandManager.commandManager, CommandPriority.Attack)
        {
            this.attackActor = _attackActor;
            this.attackTarget = _attackTarget;
        }

        public override void doCommand()
        {
            Actor.handleAttackCommand(attackActor, attackTarget);
        }
    }
}
