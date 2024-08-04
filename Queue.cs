using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class Queue
    {
        public Block[] blocks = new Block[] {
             new IBlock(),
             new OBlock(),
             new SBlock(),
             new LBlock(),
             new ZBlock(),
             new TBlock(),
             new JBlock(),
        };

        public Random random { get; set; }
        public Block NextBlock { get; set; }

        public Queue()
        {
            random = new Random(); // Initialize the random object
            NextBlock = RandomBlock();
        }

        public Block RandomBlock()
        {
            return blocks[random.Next(blocks.Length)];
        }

        public Block UpdateNext()
        {
            Block block = NextBlock;
            do
            {
                NextBlock = RandomBlock();
            }
            while (block.ID == NextBlock.ID);
            return block;
        }
    }
}
