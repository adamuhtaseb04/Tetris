/*
 * Adam Almuhtaseb
 * Tetris #
 * Version 1.0
 * this file contains the necessary functions and resources for running the program's main window
 */
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace Tetris;

public partial class MainWindow : Window
{
    private readonly Image[,] imageControls;                    // 2D array of image controls for game grid
    private readonly int maxDelay = 1000;                       // max delay for drop drop speed (ms)
    private readonly int minDelay = 75;                         // min delay for drop drop speed (ms)
    private readonly int delayDecrease = 25;                    // delay decrease for drop drop speed (ms)
    private GameState gameState = new GameState();              // the current game state
    private MediaPlayer backgroundPlayer = new MediaPlayer();   // background music player

    // array of tile graphics
    private readonly ImageSource[] tileImages = new ImageSource[] {
        new BitmapImage(new Uri("Assets/EmptyTile.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/CyanTile.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/BlueTile.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/OrangeTile.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/YellowTile.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/GreenTile.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/PurpleTile.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/RedTile.png", UriKind.Relative))
    };

    // array of block graphics
    private readonly ImageSource[] blockImages = new ImageSource[] {
        new BitmapImage(new Uri("Assets/BlockEmpty.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/BlockI.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/BlockJ.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/BlockL.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/BlockO.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/BlockS.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/BlockT.png", UriKind.Relative)),
        new BitmapImage(new Uri("Assets/BlockZ.png", UriKind.Relative))
    };

    // constructor to initialize window and setup game grid
    public MainWindow()
    {
        InitializeComponent();
        PlayBackgroundMusic();
        imageControls = SetupGameCanvas(gameState.gameGrid);
    }

    // game loop to repeat game and logic
    private async Task GameLoop()
    {
        Draw(gameState);
        while (!gameState.gameOver)
        {
            int delay = Math.Max(minDelay, maxDelay - (gameState.score * delayDecrease));
            await Task.Delay(delay);
            gameState.MoveBlockDown();
            Draw(gameState);
        }
        GameOverMenu.Visibility = Visibility.Visible;
        FinalScoreText.Text = $"Score: {gameState.score}";
    }

    // setup the visual grid of image controls on canvas
    private Image[,] SetupGameCanvas(TetrisGameGrid grid)
    {
        Image[,] imageControls = new Image[grid.Rows, grid.Columns];
        int cellSize = 30;

        for (int r = 0; r < grid.Rows; r++)
        {
            for (int c = 0; c < grid.Columns; c++)
            {
                Image imageControl = new Image
                {
                    Width = cellSize,
                    Height = cellSize
                };
                Canvas.SetTop(imageControl, (r - 2) * cellSize);
                Canvas.SetLeft(imageControl, c * cellSize);
                GameCanvas.Children.Add(imageControl);
                imageControls[r, c] = imageControl;
            }
        }
        return imageControls;
    }

    // draw the current state of the game grid
    private void DrawGrid(TetrisGameGrid grid)
    {
        for (int r = 0; r < grid.Rows; r++)
        {
            for (int c = 0; c < grid.Columns; c++)
            {
                int id = grid[r, c];
                imageControls[r, c].Opacity = 1;
                imageControls[r, c].Source = tileImages[id];
            }
        }
    }

    // draw the current falling block
    private void DrawBlock(Block block)
    {
        foreach (Position p in block.TilePositions())
        {
            imageControls[p.Row, p.Column].Opacity = 1;
            imageControls[p.Row, p.Column].Source = tileImages[block.ID];
        }
    }

    // draw the next block's preview
    private void DrawNextBlock(BlockQueue blockQueue)
    {
        Block next = blockQueue.nextBlock;
        NextImage.Source = blockImages[next.ID];
    }

    // draw the currently held block's preview
    private void DrawHeldBlock(Block heldBlock)
    {
        if (heldBlock == null)
        {
            HoldImage.Source = blockImages[0];
        }
        else
        {
            HoldImage.Source = blockImages[heldBlock.ID];
        }
    }

    // draw the ghost of the current block (shadow of where block is to land at bottom)
    private void DrawGhostBlock(Block block)
    {
        int dropDistance = gameState.BlockDropDistance();
        foreach (Position p in block.TilePositions())
        {
            imageControls[p.Row + dropDistance, p.Column].Opacity = 0.50;
            imageControls[p.Row + dropDistance, p.Column].Source = tileImages[block.ID];
        }
    }

    // draw the entire game
    private void Draw(GameState gameState)
    {
        DrawGrid(gameState.gameGrid);
        DrawGhostBlock(gameState.CurrentBlock);
        DrawBlock(gameState.CurrentBlock);
        DrawNextBlock(gameState.blockQueue);
        DrawHeldBlock(gameState.HeldBlock);
        ScoreText.Text = $"Score: {gameState.score}";
    }

    // event handler to start game loop when canvas has been loaded
    private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
    {
        await GameLoop();
    }

    // event handler to process key presses and assign their game controls
    private void Window_KeyDown(object sender, KeyEventArgs e)
    {
        if (gameState.gameOver)
        {
            return;
        }

        switch (e.Key)
        {
            case Key.Left:
                gameState.MoveBlockLeft();
                break;
            case Key.Right:
                gameState.MoveBlockRight();
                break;
            case Key.Down:
                gameState.MoveBlockDown();
                break;
            case Key.Up:
                gameState.RotateBlockCW();
                break;
            case Key.Z:
                gameState.RotateBlockCCW();
                break;
            case Key.LeftShift:
                gameState.HoldBlock();
                break;
            case Key.RightShift:
                gameState.HoldBlock();
                break;
            case Key.Space:
                gameState.DropBlock();
                break;
            default:
                return;
        }
        Draw(gameState);
    }

    // event handler to reset game when "Play Again" button is clicked
    private async void PlayAgain_Click(object sender, RoutedEventArgs e)
    {
        gameState = new GameState();
        GameOverMenu.Visibility = Visibility.Hidden;
        await GameLoop();
    }

    // play background music
    private void PlayBackgroundMusic()
    {
        string path = System.IO.Path.GetFullPath("Assets/sayitright.wav");
        if (!System.IO.File.Exists(path))
        {
            MessageBox.Show($"Music file not found: {path}");
            return;
        }
        backgroundPlayer.Open(new Uri(path));
        backgroundPlayer.MediaEnded += (s, e) => {
            backgroundPlayer.Position = TimeSpan.Zero;
            backgroundPlayer.Play();
        };
        backgroundPlayer.Volume = 0.8;
        backgroundPlayer.Play();
    }
}
