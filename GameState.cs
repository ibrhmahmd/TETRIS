using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class GameState
    {

        public int Score { get; private set; }
        private Block _currentBlock;
        public Block CurrentBlock
        {
            get => _currentBlock;
            private set
            {
                _currentBlock = value;
                _currentBlock.reset();
                for (int i = 0; i < 2; i++)
                {
                    _currentBlock.Move(1, 0);
                    if (!fits())
                    {
                        _currentBlock.Move(-1, 0);
                    }
                }
            }
        }





        public GameGrid grid { get;}
        public Queue Queue { get; } 
        public bool gameover { get; private set; }


        public GameState()
        {
            grid = new GameGrid(22,10); 
            Queue = new Queue();
            CurrentBlock  = Queue.UpdateNext();
        }


        private bool fits()
        {
            foreach (Position P in CurrentBlock.tilePosition())
            {
                if (!grid.IsEmpty(P.Row, P.Column))
                {
                    gameover = false;
                }
            }
            return  true;
        }



        public void rotateblockCW()
        {

            CurrentBlock.Rotate90Wis();
            if (!fits())
            {
                CurrentBlock.Rotate90AntiWis();
            }
        }



        public void rotateblockCCW()
        {

            CurrentBlock.Rotate90AntiWis();
            if (!fits())
            {
                CurrentBlock.Rotate90Wis();
            }
        }

        public void moveleft()
        {
            CurrentBlock.Move(0, -1);
            if (!fits())
            {
                CurrentBlock.Move(0, 1);
            }
        }


        public void moveright()
        {
            CurrentBlock.Move(0, 1);
            if (!fits())
            {
                CurrentBlock.Move(0,-1);
            }
        }

        public bool isgameover()
        {
            return !(grid.IsRowEmpty(0)&&grid.IsRowEmpty(1));
        }
         

        public void placeblock()
        {
            foreach (Position P in CurrentBlock.tilePosition())
            {
                grid[P.Row, P.Column] = CurrentBlock.ID;
            }
            Score += grid.ClearFullRows();

            if (isgameover())
            {
                gameover= true;
            }
            else
            {
                CurrentBlock = Queue.UpdateNext();

            }

        }

        public void movedown()
        {
            CurrentBlock.Move(1, 0);
            if (!fits())
            {
                CurrentBlock.Move(-1, 0);
                placeblock();
            }


        }

    }
}
