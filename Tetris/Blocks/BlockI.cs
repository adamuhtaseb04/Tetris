/*
 * Adam Almuhtaseb
 * Tetris #
 * Version 1.0
 * this file contains the array for BlockI's layout values, it's ID, and starting position
 */
namespace Tetris.Blocks
{
    class BlockI : Block
    {
        // array for blockI's layout
        private readonly Position[][] tiles = new Position[][] {
            new Position[] { new(1, 0), new(1, 1), new(1, 2), new(1, 3)},
            new Position[] { new(0, 2), new(1, 2), new(2, 2), new(3, 2)},
            new Position[] { new(2, 0), new(2, 1), new(2, 2), new(2, 3)},
            new Position[] { new(0, 1), new(1, 1), new(2, 1), new(3, 1)}
        };

        // ID for block I
        public override int ID => 1;
        // staring position for block I
        protected override Position StartOffSet => new Position(-1, 3);
        protected override Position[][] Tiles => tiles;

    }
}
