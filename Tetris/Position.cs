/*
 * Adam Almuhtaseb
 * Tetris #
 * Version 1.0
 * this file is for position of the blocks' rows and columns
 */
namespace Tetris
{
    public class Position
    {
        public int Row { get; set; }    // current row
        public int Column { get; set; } // current column

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }
    }
}
