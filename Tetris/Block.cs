/*
 * Adam Almuhtaseb
 * Tetris #
 * Version 1.0
 * this file is for the basic functions of a Tetris block
 */
namespace Tetris
{
    public abstract class Block
    {
        protected abstract Position[][] Tiles { get; }      // defines block's shape in each rotation state
        protected abstract Position StartOffSet { get; }    // starting position offset for block
        public abstract int ID { get; }                     // unique ID for each block
        private int rotationState;                          // current rotation state of block (index into Tiles)
        private Position offset;                            // current position offset of block on grid

        // initializes block at its starting offset
        public Block()
        {
            offset = new Position(StartOffSet.Row, StartOffSet.Column);
        }

        // returns absolute positions of all tiles for block in current rotation
        public IEnumerable<Position> TilePositions()
        {
            foreach (Position p in Tiles[rotationState])
            {
                yield return new Position(p.Row + offset.Row, p.Column + offset.Column);
            }
        }

        // rotate block clockwise
        public void rotateCW()
        {
            rotationState = (rotationState + 1) % Tiles.Length;
        }

        // rotate block counter clockwise
        public void rotateCCW()
        {
            if (rotationState == 0)
            {
                rotationState = Tiles.Length - 1;
            }
            else
            {
                rotationState--;
            }
        }

        // move block by specified number of rows and columns
        public void Move(int rows, int columns)
        {
            offset.Row += rows;
            offset.Column += columns;
        }

        // reset block to initial rotation and starting offset
        public void Reset()
        {
            rotationState = 0;
            offset.Row = StartOffSet.Row;
            offset.Column = StartOffSet.Column;
        }
    }
}
