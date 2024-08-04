using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public class OBlock : Block
    {
        public Position[][] tiles = new Position[][]{
            new Position[] {new(0,0), new(0,1), new(1,0), new(1,1)}
        };
        public override int ID => 4;
        protected override Position StartOffset => new Position(-1, 3);
        protected override Position[][] Tiles => tiles;

    }
}
