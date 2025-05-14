/*
 * Adam Almuhtaseb
 * Tetris #
 * Version 1.0
 * this file is for the game grid of Tetris
 */
namespace Tetris
{
    class TetrisGameGrid
    {
        // grid is represented as a 2D array of integers, first int is row, second int is column.
        private readonly int[,] grid;
        public int Rows { get; }
        public int Columns { get; }

        public int this[int r, int c]
        {
            get => grid[r, c];
            set => grid[r, c] = value;
        }

        // game grid constructor. 
        public TetrisGameGrid(int rows, int columns)
        {
            Rows = rows;
            Columns = columns;
            grid = new int[rows, columns];
        }

        // checks to see if block is inside grid.
        public bool isInside(int r, int c)
        {
            return r >= 0 && r < Rows && c >= 0 && c < Columns;
        }

        // checks to see if cell is empty or not.
        public bool isEmpty(int r, int c)
        {
            return isInside(r, c) && grid[r, c] == 0;
        }

        // checks to see if a row is full.
        public bool isRowFull(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] == 0)
                {
                    return false;
                }
            }
            return true;
        }

        // checks to see if a row is empty.
        public bool isRowEmpty(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                if (grid[r, c] != 0)
                {
                    return false;
                }
            }
            return true;
        }

        // method to clear a row.
        private void clearRow(int r)
        {
            for (int c = 0; c < Columns; c++)
            {
                grid[r, c] = 0;
            }
        }

        // method to move a row down.
        private void moveRowDown(int r, int numRows)
        {
            for (int c = 0; c < Columns; c++)
            {
                grid[r + numRows, c] = grid[r, c];
                grid[r, c] = 0;
            }
        }

        // method to clear full rows.
        public int clearFullRow()
        {
            int clearedRows = 0;

            for (int r = Rows - 1; r >= 0; r--)
            {
                if (isRowFull(r))
                {
                    clearRow(r);
                    clearedRows++;
                }
                else if (clearedRows > 0)
                {
                    moveRowDown(r, clearedRows);
                }
            }
            return clearedRows;
        }
    }
}
