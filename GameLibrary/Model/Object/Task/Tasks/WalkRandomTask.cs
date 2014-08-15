using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GameLibrary.Model.Map.World;
using GameLibrary.Model.Map.Chunk;
using GameLibrary.Model.Map.Region;
using GameLibrary.Model.Map.Block;
using GameLibrary.Commands.CommandTypes;
using Microsoft.Xna.Framework;

namespace GameLibrary.Model.Object.Task.Tasks
{
    public class WalkRandomTask : LivingObjectTask
    {
        private bool finishedWalking;

        private float range;

        public float Range
        {
            get { return range; }
            set { range = value; }
        }

        private Vector2 targetPosition;

        public Vector2 TargetPosition
        {
            get { return targetPosition; }
            set { targetPosition = value; }
        }

        public WalkRandomTask()
        {

        }

        public WalkRandomTask(LivingObject _TaskOwner, TaskPriority _Priority)
            : base(_TaskOwner, _Priority)
        {
            this.finishedWalking = false;
            targetPosition = new Vector2(Util.Random.GenerateGoodRandomNumber((int)(this.TaskOwner.Position.X - Chunk.chunkSizeX * Block.BlockSize / 2), (int)(this.TaskOwner.Position.X + Chunk.chunkSizeX * Block.BlockSize / 2)), Util.Random.GenerateGoodRandomNumber((int)(this.TaskOwner.Position.X - Chunk.chunkSizeX * Block.BlockSize / 2), (int)(this.TaskOwner.Position.X + Chunk.chunkSizeX * Block.BlockSize / 2)));
            this.TaskOwner.Path = createPath(new Vector2(this.TaskOwner.Position.X, this.TaskOwner.Position.Y), new Vector2(this.targetPosition.X, this.targetPosition.Y));
            int counter = 1;
            while (!isPathPossible() && counter >= 0)
            {
                targetPosition = new Vector2(Util.Random.GenerateGoodRandomNumber((int)(this.TaskOwner.Position.X - Chunk.chunkSizeX * Block.BlockSize / 2), (int)(this.TaskOwner.Position.X + Chunk.chunkSizeX * Block.BlockSize / 2)), Util.Random.GenerateGoodRandomNumber((int)(this.TaskOwner.Position.X - Chunk.chunkSizeX * Block.BlockSize / 2), (int)(this.TaskOwner.Position.X + Chunk.chunkSizeX * Block.BlockSize / 2)));
                this.TaskOwner.Path = createPath(new Vector2(this.TaskOwner.Position.X, this.TaskOwner.Position.Y), new Vector2(this.targetPosition.X, this.targetPosition.Y));
                counter--;
            }
        }

        public override bool wantToDoTask()
        {
            bool var_wantToDoTask = true;

            return var_wantToDoTask || base.wantToDoTask();
        }

        public override void update()
        {
            base.update();
            walkRandom();
        }

        private void walkRandom()
        {
            if (this.finishedWalking)
            {
                targetPosition = new Vector2(Util.Random.GenerateGoodRandomNumber((int)(this.TaskOwner.Position.X - Chunk.chunkSizeX * Block.BlockSize / 2), (int)(this.TaskOwner.Position.X + Chunk.chunkSizeX * Block.BlockSize / 2)), Util.Random.GenerateGoodRandomNumber((int)(this.TaskOwner.Position.X - Chunk.chunkSizeX * Block.BlockSize / 2), (int)(this.TaskOwner.Position.X + Chunk.chunkSizeX * Block.BlockSize / 2)));
                this.TaskOwner.Path = GameLibrary.Model.Path.PathFinderAStar.generatePath(new Vector2(this.TaskOwner.Position.X, this.TaskOwner.Position.Y), new Vector2(this.targetPosition.X, this.targetPosition.Y));
            }
            else
            {
                if (Vector2.Distance(new Vector2(this.TaskOwner.Position.X, this.TaskOwner.Position.Y), this.targetPosition) <= 5)
                {
                    this.finishedWalking = true;
                }
                else
                {
                    /*this.TaskOwner.Path = createPath(new Vector2(this.TaskOwner.Position.X, this.TaskOwner.Position.Y), new Vector2(this.targetPosition.X, this.targetPosition.Y));
                    int counter = 1;
                    while (!isPathPossible() && counter >= 0)
                    {
                        targetPosition = new Vector2(Util.Random.GenerateGoodRandomNumber((int)(this.TaskOwner.Position.X - Chunk.chunkSizeX * Block.BlockSize / 2), (int)(this.TaskOwner.Position.X + Chunk.chunkSizeX * Block.BlockSize / 2)), Util.Random.GenerateGoodRandomNumber((int)(this.TaskOwner.Position.X - Chunk.chunkSizeX * Block.BlockSize / 2), (int)(this.TaskOwner.Position.X + Chunk.chunkSizeX * Block.BlockSize / 2)));
                        this.TaskOwner.Path = createPath(new Vector2(this.TaskOwner.Position.X, this.TaskOwner.Position.Y), new Vector2(this.targetPosition.X, this.targetPosition.Y));
                        counter--;
                    }*/
                }
            }
        }

        private bool isPathPossible()
        {
            return this.TaskOwner.Path != null;
        }

        private Path.Path createPath(Vector2 currentPosition, Vector2 targetPosition)
        {
            return GameLibrary.Model.Path.PathFinderAStar.generatePath(currentPosition, targetPosition);
        }
    }
}
