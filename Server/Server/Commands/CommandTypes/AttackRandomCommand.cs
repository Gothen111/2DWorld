﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Object;

namespace Server.Commands.CommandTypes
{
    class AttackRandomCommand : Command
    {
        private LivingObject attackActor;

        internal LivingObject AttackActor
        {
            get { return attackActor; }
            set { attackActor = value; }
        }


        public AttackRandomCommand(LivingObject _attackActor)
        {
            this.Actor = CommandManager.commandManager;
            this.attackActor = _attackActor;
        }

        public override void doCommand()
        {
            ((CommandManager)Actor).handleAttackCommand(attackActor);
        }
    }
}