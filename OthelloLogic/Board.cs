namespace OthelloLogic
{
    public class Board
    {
        private int m_Size;
        private BoardCell[,] m_GameBoard;

        public Board(int i_Size)
        {
            m_Size = i_Size;
            m_GameBoard = new BoardCell[i_Size, i_Size];
        }

        public BoardCell[,] BoardCells
        {
            get { return m_GameBoard; }
        }

        public int BoardSize
        {
            get { return m_Size; }
        }

        public bool IsFull()
        {
            bool result = true;

            for (int i = 0; i < m_Size; i++)
            {
                for (int j = 0; j < m_Size; j++)
                {
                    if (m_GameBoard[i, j].Sign == BoardCell.eSigns.Empty)
                    {
                        result = false;
                    }
                }
            }

            return result;
        }
    }
}
