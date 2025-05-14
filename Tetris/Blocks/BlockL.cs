/*
 * Adam Almuhtaseb
 * Tetris #
 * Version 1.0
 * this file contains the array for BlockL's layout values, it's ID, and starting position
 */
namespace Tetris.Blocks
{
    class BlockL : Block
    {
        // array for blockL's layout
        private readonly Position[][] tiles = new Position[][] {
            new Position[] { new(0, 2), new(1, 0), new(1, 1), new(1, 2)},
            new Position[] { new(0, 1), new(1, 1), new(2, 1), new(2, 2)},
            new Position[] { new(1, 0), new(1, 1), new(1, 2), new(2, 0)},
            new Position[] { new(0, 0), new(0, 1), new(1, 1), new(2, 1)}
        };

        // ID for block L
        public override int ID => 3;
        // staring position for block L
        protected override Position StartOffSet => new Position(0, 3);
        protected override Position[][] Tiles => tiles;
    }
}
