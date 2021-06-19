using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Timers;
using Timer = System.Timers.Timer;

namespace Minesweeper
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public static Timer timer;
        //int counter = 0;
        public MainWindow()
        {
            //timer = new Timer(1000);
            //timer.AutoReset = true;
            //timer.Enabled = true;
            //timer.Elapsed += new ElapsedEventHandler(handleTimerElapsed);
            InitializeComponent();
            int dimensions = new int();
            int bombcount = new int();
            List<GameButton> list = new List<GameButton>();
            ApplyButton.Click += new RoutedEventHandler(ApplyButton_Click);
            void ApplyButton_Click(object sender, RoutedEventArgs e)
            {
                dimensions = int.Parse(TextBoxDimensions.Text);
                bombcount = int.Parse(TextBoxMines.Text);
                Engine engine = new Engine(dimensions, bombcount);
                for (int i = 0; i < dimensions; i++)
                {
                    grid.RowDefinitions.Add(new RowDefinition());
                    grid.ColumnDefinitions.Add(new ColumnDefinition());
                }
                InitializeGrid();
                RefreshGUI();
                void HandleLeft(object sender, RoutedEventArgs e)
                {
                    GameButton b = (GameButton)sender;
                    bool B = engine.Reveal(b);
                    if (B)
                    {
                        GameStatusCheck();
                    }
                }
                void HandleClick(object sender, MouseEventArgs e)
                {
                    GameButton b = (GameButton)sender;

                    if (e.RightButton == MouseButtonState.Pressed)
                    {
                        engine.Flag(b);
                        RefreshGUI();
                    }
                    if (e.MiddleButton == MouseButtonState.Pressed)
                    {
                        bool middle = engine.MiddleClick(b);
                        Panel p = engine.GetPanels()[b.X, b.Y];
                        if (middle)
                        {
                            GameStatusCheck();
                        }
                    }
                }
                void RefreshGUI()
                {
                    foreach (var item in engine.GetPanels())
                    {
                        GameButton button = list.Where(x => x.X == item.X && x.Y == item.Y).Cast<GameButton>().Single();
                        if (item.IsRevealed)
                        {
                            button.Background = Brushes.White;
                            button.Content = item.NeigborMines;
                        }
                        else if (item.IsFlagged)
                        {
                            button.Background = Brushes.Blue;
                            button.Content = "F";
                        }
                        else if (!item.IsFlagged && !item.IsRevealed)
                        {
                            button.Background = Brushes.Gray;
                        }
                    }
                }
                void ShowAllMines()
                {
                    List<Panel> P = engine.GetMinedPanels();
                    foreach (var item in P)
                    {
                        GameButton button = (GameButton)list.Where(x => x.X == item.X && x.Y == item.Y).Cast<GameButton>().Single();
                        button.Background = Brushes.Red;
                        button.Content = "*";
                    }
                }
                void GameStatusCheck()
                {
                    RefreshGUI();
                    if (engine.GetStatus() == GameStatus.Completed)
                    {
                        MessageBoxResult result = MessageBox.Show("Pobeda. Nova igra ?", "Minesweeper", MessageBoxButton.YesNo);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                engine = new Engine(dimensions, bombcount);
                                InitializeGrid();
                                break;
                            case MessageBoxResult.No:
                                engine.EndGame();
                                break;
                        }
                    }
                    if (engine.GetStatus() == GameStatus.Failed)
                    {
                        ShowAllMines();
                        MessageBoxResult result = MessageBox.Show("Poraz. Nova igra ?", "Minesweeper", MessageBoxButton.YesNo);
                        switch (result)
                        {
                            case MessageBoxResult.Yes:
                                engine = new Engine(dimensions, bombcount);
                                InitializeGrid();
                                break;
                            case MessageBoxResult.No:
                                engine.EndGame();
                                break;
                        }
                    }
                    RefreshGUI();
                }
                void InitializeGrid()
                {
                    list.Clear();   
                    for (int i = 0; i < dimensions; i++)
                    {
                        for (int j = 0; j < dimensions; j++)
                        {
                            var b = new GameButton();
                            b.X = i;
                            b.Y = j;
                            b.MouseDown += new MouseButtonEventHandler(HandleClick);
                            b.Click += new RoutedEventHandler(HandleLeft);
                            b.Background = Brushes.Gray;
                            Grid.SetColumn(b, j);
                            Grid.SetRow(b, i);
                            grid.Children.Add(b);
                            list.Add(b);
                        }
                    }
                }
            }
        }

        //private void handleTimerElapsed(object sender, ElapsedEventArgs e)
        //{
        //    this.Dispatcher.Invoke(() =>
        //    {
        //        label.Content = counter;
        //    });
        //    counter++;
        //}
    }
}
