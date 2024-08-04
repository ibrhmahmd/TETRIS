using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ImageSource[] tileImages = new ImageSource[]{
            new BitmapImage(new Uri("Assets/TileEmpty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileCyan.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileBlue.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileOrange.png", UriKind. Relative)),
            new BitmapImage(new Uri("Assets/TileYellow.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileGreen.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TilePurple.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/TileRed.png", UriKind.Relative))
        };
        private readonly ImageSource[] blockImages = new ImageSource[]{
            new BitmapImage(new Uri("Assets/Block-Empty.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-I.png", UriKind. Relative)), 
            new BitmapImage(new Uri("Assets/Block-J.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-L.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-0.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-S.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-T.png", UriKind.Relative)),
            new BitmapImage(new Uri("Assets/Block-Z.png", UriKind.Relative)) 
        };


        private readonly Image[,] ImageControl;
        private GameState gameState = new GameState();

        private Image[,] SetupGameCanvas(GameGrid grid)
        {

            Image[,] imageControls = new Image[grid.Rows, grid.Columns];
            int cellSize = 25;

            for (int r = 0; r < grid.Rows; r++) {
                for (int c = 0; c < grid.Columns; c++) {

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



        public MainWindow()
        {
            InitializeComponent();
            ImageControl = SetupGameCanvas(gameState.grid);
        }
        private void drawGird(GameGrid grid)
        {
            for (int r =0; r< grid.Rows; r++)
            {
                for (int c =0; c< grid.Columns; c++)
                {
                    int id = grid[r, c];
                    ImageControl[r,c].Source=tileImages[id];
                }
            }
        }

        private void DrawBlock(Block block)
        {
            foreach(Position P in block.tilePosition())
            {
                ImageControl[P.Row, P.Column].Source = tileImages[block.ID];
            }
        }

        private void Draw(GameState gameState)
        {
            drawGird(gameState.grid);
            DrawBlock(gameState.CurrentBlock);
            
        }

        private async Task gameloop()
        {
            Draw(gameState);
            while (!gameState.gameover) 
            {
                await Task.Delay(500);
                gameState.movedown();
                Draw(gameState);
            }


        }


        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (gameState.gameover)
            {
                return;
            }
            switch (e.Key)
            {
                case Key.Right:
                    gameState.moveright();
                    break;
                case Key.Left:
                    gameState.moveleft();
                    break;
                case Key.Down: 
                    gameState.movedown();
                    break;
                case Key.Up:
                    gameState.rotateblockCW();
                    break;
                case Key.RightShift: 
                    gameState.rotateblockCCW();
                    break;
                default:
                    return;
            }
            Draw(gameState);
        }

        private async void GameCanvas_Loaded(object sender, RoutedEventArgs e)
        {
            await gameloop();
        }

        private void PlayAgain_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}