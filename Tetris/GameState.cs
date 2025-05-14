/*
 * Adam Almuhtaseb
 * Tetris #
 * Version 1.0
 * this file is for the current state of the Tetris game when being played
 */
namespace Tetris
{
    class GameState
    {
        private Block currentBlock;                     // current block being controlled by player
        public TetrisGameGrid gameGrid { get; }         // grid representing playfield
        public BlockQueue blockQueue { get; }           // queue of upcoming blocks
        public bool gameOver { get; private set; }      // check if game is over
        public int score { get; private set; }          // player's current score
        public Block HeldBlock { get; private set; }    // currently held block
        public bool CanHold { get; private set; }       // check if player can currently hold a block

        // current block logic to reset and position it when set
        public Block CurrentBlock
        {
            get => currentBlock;
            private set
            {
                currentBlock = value;
                currentBlock.Reset();

                for (int i = 0; i < 2; i++)
                {
                    currentBlock.Move(1, 0);
                    if (!BlockFits())
                    {
                        currentBlock.Move(-1, 0);
                    }
                }
            }
        }

        // initializes game state, grid, and block queue
        public GameState()
        {
            gameGrid = new TetrisGameGrid(22, 10);
            blockQueue = new BlockQueue();
            CurrentBlock = blockQueue.getAndUpdate();
            CurrentBlock = currentBlock;
            CanHold = true;
        }

        // checks to see if current block fits in current position
        private bool BlockFits()
        {
            foreach (Position p in CurrentBlock.TilePositions())
            {
                if (!gameGrid.isEmpty(p.Row, p.Column))
                {
                    return false;
                }
            }
            return true;
        }

        // handles holding and swapping current block
        public void HoldBlock()
        {
            if (!CanHold)
            {
                return;
            }
            if (HeldBlock == null)
            {
                HeldBlock = CurrentBlock;
                CurrentBlock = blockQueue.getAndUpdate();
            }
            else
            {
                Block tmp = CurrentBlock;
                CurrentBlock = HeldBlock;
                HeldBlock = tmp;
            }
            CanHold = false;
        }

        // rotate block clockwise
        public void RotateBlockCW()
        {
            CurrentBlock.rotateCW();
            if (!BlockFits())
            {
                CurrentBlock.rotateCCW();
            }
        }

        // rotate block counterclockwise
        public void RotateBlockCCW()
        {
            CurrentBlock.rotateCCW();
            if (!BlockFits())
            {
                CurrentBlock.rotateCW();
            }
        }

        // move block left
        public void MoveBlockLeft()
        {
            CurrentBlock.Move(0, -1);
            if (!BlockFits())
            {
                CurrentBlock.Move(0, 1);
            }
        }

        // move block right
        public void MoveBlockRight()
        {
            CurrentBlock.Move(0, 1);
            if (!BlockFits())
            {
                CurrentBlock.Move(0, -1);
            }
        }

        // check if game is over
        private bool IsGameOver()
        {
            return !(gameGrid.isRowEmpty(0) && gameGrid.isRowEmpty(1));
        }

        // place current block, clear row, update score, and spawn new block
        private void PlaceBlock()
        {
            foreach (Position p in CurrentBlock.TilePositions())
            {
                gameGrid[p.Row, p.Column] = currentBlock.ID;
            }

            score += gameGrid.clearFullRow();

            if (IsGameOver())
            {
                gameOver = true;
            }
            else
            {
                CurrentBlock = blockQueue.getAndUpdate();
                CanHold = true;
            }
        }

        // move block down
        public void MoveBlockDown()
        {
            currentBlock.Move(1, 0);

            if (!BlockFits())
            {
                CurrentBlock.Move(-1, 0);
                PlaceBlock();
            }
        }

        // calculate distance for dropping tile
        private int TileDropDistance(Position p)
        {
            int drop = 0;
            while (gameGrid.isEmpty(p.Row + drop + 1, p.Column))
            {
                drop++;
            }
            return drop;
        }

        // calculate minimum drop distance for current block
        public int BlockDropDistance()
        {
            int drop = gameGrid.Rows;
            foreach (Position p in CurrentBlock.TilePositions())
            {
                drop = System.Math.Min(drop, TileDropDistance(p));
            }
            return drop;
        }

        // instantly drop block
        public void DropBlock()
        {
            CurrentBlock.Move(BlockDropDistance(), 0);
            PlaceBlock();
        }
    }
}
