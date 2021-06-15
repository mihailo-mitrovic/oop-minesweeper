using System;
using System.Collections.Generic;
using System.Text;

namespace Minesweeper
{
    public class Panel
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Id { get; set; }
        public bool IsMine { get; set; }
        public bool IsRevealed { get; set; }
        public bool IsFlagged { get; set; }
        public int NeigborMines { get; set; }

        public Panel(int x, int y,int id)
        {
            X = x;
            Y = y;
            Id = id;
            IsRevealed = false;
        }
    }
}
