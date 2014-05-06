using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Object;

namespace Server.Commands.CommandTypes
{
    class AttackWithWeaponCommand : Command
    {
        private LivingObject attackActor;

        public LivingObject AttackActor
        {
            get { return attackActor; }
            set { attackActor = value; }
        }

        public AttackWithWeaponCommand(LivingObject _AttackActor)
        {
            this.Actor = CommandManager.commandManager;
            this.attackActor = _AttackActor;
        }

        public override void doCommand()
        {
            ((CommandManager)Actor).handleAttackWithWeaponCommand(attackActor);
        }

        public override void stopCommand()
        {
        }
    }
}
