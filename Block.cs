using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public abstract class Block
    {
        protected abstract Position[][] Tiles { get; }
        protected abstract Position StartOffset { get; }
        public abstract int ID { get;}
        public int RotationState;
        public Position CurrentOffset;




        public Block()
        {
            // Ensure StartOffset is initialized in derived classes
            if (StartOffset != null)
            {
                CurrentOffset = new Position(StartOffset.Row, StartOffset.Column);
            }
            else
            {
                // Handle the case where StartOffset is not initialized
                CurrentOffset = new Position(0, 0); // Default value or handle as needed
            }
        }

        public IEnumerable<Position> tilePosition()
        {
            foreach (Position P in Tiles[RotationState])
            {
                yield return new Position(P.Row+ CurrentOffset.Row, P.Column+ CurrentOffset.Column );
            }// adds the row offset and the columns offset to the current rotation state 
        }

        public void Rotate90Wis()
        {
           RotationState = (RotationState + 1) % Tiles.Length; 
        }

        public void Rotate90AntiWis()
        {
            if (RotationState !=0 )
                   RotationState = Tiles.Length-1; 

            else
            { RotationState--; }
        }


        public void Move(int MovRow, int MovColumn)
        {
            CurrentOffset.Row += MovRow;
            CurrentOffset.Column += MovColumn;
        }

        public void reset()
        {
            CurrentOffset.Row = 0;
            CurrentOffset.Column = 0;
            RotationState = 0;
        }
    }
}
