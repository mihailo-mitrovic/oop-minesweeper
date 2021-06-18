using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;
using System.Windows.Media;

namespace Minesweeper
{   
    enum GameStatus
    {
        InProgress,
        Failed,
        Completed
    }
    class Engine
    {
        int dimensions;
        int bombcount;
        GameStatus status;
        List<Panel> AllPanels = new List<Panel>();
        List<Panel> MinedPanels = new List<Panel>();
        List<Panel> RevealedPanels= new List<Panel>();
        bool[,] zeros;
        Panel[,] panel;
        Panel[,] PanelInitialisation()
        {
            Panel[,] p = new Panel[dimensions, dimensions];
            int id = 1;
            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    p[i, j] = new Panel(i,j,id);
                    AllPanels.Add(p[i, j]);
                    id++;
                }
            }
            return p;
        }
        void MineGeneration()
        {
            bool[,] bombe = new bool[dimensions,dimensions];
            int brojac = bombcount;
            while(brojac != 0)
            {
                var rand = new Random();
                var a = rand.Next(dimensions);
                var b = rand.Next(dimensions);
                if (!bombe[a,b])
                {
                    bombe[a, b] = true;
                    MinedPanels.Add(panel[a, b]);
                    brojac--;
                }
            }
            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    if (bombe[i, j])
                    {
                        panel[i, j].IsMine = true;
                    }
                }
            }
            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    panel[i, j].NeigborMines = NeighbourMine(panel[i, j]);
                }
            }
        }
        void Zeros()
        {
            for (int i = 0; i < dimensions; i++)
            {
                for (int j = 0; j < dimensions; j++)
                {
                    if (panel[i, j].NeigborMines == 0)
                    {
                        zeros[i, j] = true;
                    }
                }
            }
        }
        public void Flag(GameButton b)
        {
            Panel p = panel[b.X, b.Y];
            if (!p.IsRevealed)
            {
                if (p.IsFlagged)
                {
                    p.IsFlagged = false;
                    b.Content = "";
                }
                else
                {
                    p.IsFlagged = true;
                    b.Content = "F";
                }
            }
        }
        public bool Reveal(GameButton b)
        {
            Panel p = panel[b.X, b.Y];
            if (!p.IsFlagged)
            {
                if (p.IsMine)
                {
                    status = GameStatus.Failed;
                }
                else if(!p.IsRevealed)
                {
                    p.IsRevealed = true;
                    RevealedPanels.Add(p);
                    if (p.NeigborMines == 0)
                    {
                        RevealZeros(p);
                    }
                    if (WinCondition())
                    {
                        status = GameStatus.Completed;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool Reveal(Panel p)
        {
            if (!p.IsFlagged)
            {
                if (p.IsMine)
                {
                    status = GameStatus.Failed;
                }
                else if (!p.IsRevealed)
                {
                    p.IsRevealed = true;
                    RevealedPanels.Add(p);
                    if (p.NeigborMines == 0)
                    {
                        RevealZeros(p);
                    }
                    if (WinCondition())
                    {
                        status = GameStatus.Completed;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public List<Panel> NeighbourFields(Panel p)
        {
            List<Panel> P = new List<Panel>();
            for (int i = p.X - 1; i <= p.X + 1; i++)
            {
                for (int j = p.Y - 1; j <= p.Y + 1; j++)
                {
                    if ((i < dimensions) && (i >= 0) && (j < dimensions) && (j >= 0))
                    {
                        P.Add(panel[i,j]);
                    }
                }
            }
            P.Remove(p);
            return P;
        }
        public int NeighbourMine(Panel p)
        {
            List<Panel> P = NeighbourFields(p);
            int b = 0;
            foreach (var panel in P)
            {
                if (panel.IsMine)
                {
                    b++;
                }
            }
            return b;
        }
        public void RevealZeros(Panel p)
        {
            
        }
        public void EndGame()
        {
            Environment.Exit(0);
        }
        public Engine(int d, int b)
        {
            dimensions = d;
            bombcount = b;
            status = GameStatus.InProgress;
            panel = PanelInitialisation();
            MineGeneration();
            zeros = new bool[d, d];
            Zeros();
        }
        public GameStatus GetStatus()
        {
            return status;
        }
        public Panel[,] GetPanels()
        {
            return panel;
        }
        public bool MiddleClick(GameButton b)
        {
            Panel p = panel[b.X, b.Y];
            int pi = NeighbourFields(p).Where(x => x.IsFlagged).Count();
            if (p.NeigborMines == pi && p.IsRevealed)
            {
                foreach (var P in NeighbourFields(p))
                {
                    Reveal(P);
                }
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool PanelMined(GameButton b)
        {
            Panel p = panel[b.X, b.Y];
            return p.IsMine;
        }
        private bool WinCondition()
        {
            return RevealedPanels.Count == AllPanels.Count - MinedPanels.Count;
        }
    }
}
