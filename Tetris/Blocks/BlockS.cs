/*
 * Adam Almuhtaseb
 * Tetris #
 * Version 1.0
 * this file contains the array for BlockS's layout values, it's ID, and starting position
 */
namespace Tetris.Blocks
{
    class BlockS : Block
    {
        // array for blockS's layout
        private readonly Position[][] tiles = new Position[][] {
            new Position[] { new(0, 1), new(0, 2), new(1, 0), new(1, 1)},
            new Position[] { new(0, 1), new(1, 1), new(1, 2), new(2, 2)},
            new Position[] { new(1, 1), new(1, 2), new(2, 0), new(2, 1)},
            new Position[] { new(0, 0), new(1, 0), new(1, 1), new(2, 1)}
        };

        // ID for block S
        public override int ID => 5;
        // staring position for block S
        protected override Position StartOffSet => new Position(0, 3);
        protected override Position[][] Tiles => tiles;
    }
}