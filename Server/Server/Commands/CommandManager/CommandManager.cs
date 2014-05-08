using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Server.Model.Object;
using Server.Model.Object.Task.Tasks;
using Server.Commands.CommandTypes;

namespace Server.Commands
{
    class CommandManager
    {
        public static CommandManager commandManager = new CommandManager();

        private CommandManager() { }

        public void handleAttackCommand(LivingObject actor)
        {
            actor.Tasks.Add(new AttackRandomTask(actor, TaskPriority.Attack_Random));
        }

        public void handleStandCommand(LivingObject actor)
        {
            actor.Tasks.Add(new StandTask(actor, TaskPriority.Stand));
        }

        public void handleWalkRandomCommand(LivingObject actor)
        {
            actor.Tasks.Add(new WalkRandomTask(actor, TaskPriority.Walk_Random));
        }

        public void handleWalkUpCommand(LivingObject actor)
        {
            actor.MoveUp = true;
        }
        public void stopWalkUpCommand(LivingObject actor)
        {
            actor.MoveUp = false;
        }

        public void handleWalkDownCommand(LivingObject actor)
        {
            actor.MoveDown = true;
        }
        public void stopWalkDownCommand(LivingObject actor)
        {
            actor.MoveDown = false;
        }

        public void handleWalkLeftCommand(LivingObject actor)
        {
            actor.MoveLeft = true;
        }
        public void stopWalkLeftCommand(LivingObject actor)
        {
            actor.MoveLeft = false;
        }

        public void handleWalkRightCommand(LivingObject actor)
        {
            actor.MoveRight = true;
        }
        public void stopWalkRightCommand(LivingObject actor)
        {
            actor.MoveRight = false;
        }

        public void handleAttackWithWeaponCommand(LivingObject actor)
        {
            actor.attack();
        }
        public void handleInteractCommand(LivingObject actor)
        {
            actor.interact();
        }
    }
}
