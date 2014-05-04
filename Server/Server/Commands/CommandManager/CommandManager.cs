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
            actor.moveUp(true);
            //actor.setMoveVelocity(new Microsoft.Xna.Framework.Vector3(0, -1, 0)); // ???
        }
        public void stopWalkUpCommand(LivingObject actor)
        {
            actor.moveUp(false);
            //actor.Velocity = new Microsoft.Xna.Framework.Vector3(actor.Velocity.X, 0, actor.Velocity.Z); // ???
        }

        public void handleWalkDownCommand(LivingObject actor)
        {
            actor.moveDown(true);
            //actor.setMoveVelocity(new Microsoft.Xna.Framework.Vector3(0, 1, 0)); // ???
        }
        public void stopWalkDownCommand(LivingObject actor)
        {
            actor.moveDown(false);
            //actor.Velocity = new Microsoft.Xna.Framework.Vector3(actor.Velocity.X, 0, actor.Velocity.Z); // ???
        }

        public void handleWalkLeftCommand(LivingObject actor)
        {
            actor.moveLeft(true);
            //actor.setMoveVelocity(new Microsoft.Xna.Framework.Vector3(-1, 0, 0)); // ???
        }
        public void stopWalkLeftCommand(LivingObject actor)
        {
            actor.moveLeft(false);
            //actor.Velocity = new Microsoft.Xna.Framework.Vector3(0, actor.Velocity.Y, actor.Velocity.Z); // ???
        }

        public void handleWalkRightCommand(LivingObject actor)
        {
            actor.moveRight(true);
            //actor.setMoveVelocity(new Microsoft.Xna.Framework.Vector3(1, 0, 0)); // ???
        }
        public void stopWalkRightCommand(LivingObject actor)
        {
            actor.moveRight(false);
            //actor.Velocity = new Microsoft.Xna.Framework.Vector3(0, actor.Velocity.Y, actor.Velocity.Z); // ???
        }
    }
}
