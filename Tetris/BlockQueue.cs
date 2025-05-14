/*
 * Adam Almuhtaseb
 * Tetris #
 * Version 1.0
 * this file is for the queue of the Tetris blocks to be placed
 */
using Tetris.Blocks;

namespace Tetris
{
    class BlockQueue
    {
        // random number generator for selecting blocks
        private readonly Random random = new Random();

        // the next block to be placed in game
        public Block nextBlock { get; private set; }

        // array for possible block types
        private readonly Block[] blocks = new Block[] {
            new BlockI(),
            new BlockJ(),
            new BlockL(),
            new BlockO(),
            new BlockS(),
            new BlockT(),
            new BlockZ()
        };

        // initializes the queue with a random next block
        public BlockQueue()
        {
            nextBlock = randomBlock();
        }

        // returns a randomly selected block from the array
        private Block randomBlock()
        {
            return blocks[random.Next(blocks.Length)];
        }

        // returns the current next block and updates queue with a new random block
        // and makes sure the same block isn't selected again consecutively
        public Block getAndUpdate()
        {
            Block block = nextBlock;
            do
            {
                nextBlock = randomBlock();
            }
            while (block.ID == nextBlock.ID);
            return block;
        }
    }
}
