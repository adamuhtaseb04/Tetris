/*
 * Adam Almuhtaseb
 * Tetris #
 * Version 1.0
 * this file contains the array for BlockT's layout values, it's ID, and starting position
 */
namespace Tetris.Blocks
{
    class BlockT : Block
    {
        // array for blockT's layout
        private readonly Position[][] tiles = new Position[][] {
            new Position[] { new(0, 1), new(1, 0), new(1, 1), new(1, 2)},
            new Position[] { new(0, 1), new(1, 1), new(1, 2), new(2, 1)},
            new Position[] { new(1, 0), new(1, 1), new(1, 2), new(2, 1)},
            new Position[] { new(0, 1), new(1, 0), new(1, 1), new(2, 1)}
        };

        // ID for block T
        public override int ID => 6;
        // staring position for block T
        protected override Position StartOffSet => new Position(0, 3);
        protected override Position[][] Tiles => tiles;
    }
}
