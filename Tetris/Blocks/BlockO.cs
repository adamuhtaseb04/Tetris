/*
 * Adam Almuhtaseb
 * Tetris #
 * Version 1.0
 * this file contains the array for BlockO's layout values, it's ID, and starting position
 */
namespace Tetris.Blocks
{
    class BlockO : Block
    {
        // array for blockO's layout
        private readonly Position[][] tiles = new Position[][] {
            new Position[] { new(0, 0), new(0, 1), new(1, 0), new(1, 1)}
        };

        // ID for block O
        public override int ID => 4;
        // staring position for block O
        protected override Position StartOffSet => new Position(0, 4);
        protected override Position[][] Tiles => tiles;
    }
}
