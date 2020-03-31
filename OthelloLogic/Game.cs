using System.Collections.Generic;

namespace OthelloLogic
{
    public class Game
    {
        public enum ePlayerId
        {
            Player1,
            Player2,
            Computer,
            None,
        }

        public enum eMessage
        {
            MoveNotValid,
            EndTurn,
            EndGame,
        }

        private Board m_GameBoard;
        private List<Player> m_Players;

        public Game(List<Player> i_Players, int i_BoardSize)
        {
            m_Players = new List<Player>(2);
            m_Players = i_Players;
            m_GameBoard = new Board(i_BoardSize);
            InitializeBoard();
        }

        public ePlayerId FirstPlayerId
        {
            get { return m_Players[(int)ePlayerId.Player1].Id; }
        }

        public ePlayerId SecondPlayerId
        {
            get { return m_Players[(int)ePlayerId.Player2].Id; }
        }

        public string SecondPlayerName
        {
            get { return m_Players[OtheloGame.k_SecondPlayer].Name; }
        }

        public int FirstPlayerScore
        {
            get { return m_Players[OtheloGame.k_FirstPlayer].Score; }
        }

        public int SecondPlayerScore
        {
            get { return m_Players[OtheloGame.k_SecondPlayer].Score; }
        }

        public Board GameBoard
        {
            get { return m_GameBoard; }
        }

        public List<Player> Players
        {
            get { return m_Players; }
        }

        public void CalculateScore()
        {
            int xCounter = 0;
            int oCounter = 0;

            foreach (BoardCell cell in m_GameBoard.BoardCells)
            {
                if (cell.Owner == m_Players[OtheloGame.k_FirstPlayer].Id)
                {
                    xCounter++;
                }
                else if (cell.Owner == m_Players[OtheloGame.k_SecondPlayer].Id)
                {
                    oCounter++;
                }
            }

            m_Players[OtheloGame.k_FirstPlayer].Score = xCounter;
            m_Players[OtheloGame.k_SecondPlayer].Score = oCounter;
        }

        public void InitializeBoard()
        {
            for (int i = 0; i < m_GameBoard.BoardSize; i++)
            {
                for (int j = 0; j < m_GameBoard.BoardSize; j++)
                {
                    if ((i == (m_GameBoard.BoardSize / 2) - 1 && j == i) ||
                        (i == (m_GameBoard.BoardSize / 2) && j == i))
                    {
                        m_GameBoard.BoardCells[i, j] = new BoardCell();
                        m_GameBoard.BoardCells[i, j].Sign = BoardCell.eSigns.SecondPlayerSign;
                        m_GameBoard.BoardCells[i, j].Owner = m_Players[OtheloGame.k_SecondPlayer].Id;
                        m_Players[OtheloGame.k_SecondPlayer].AddCell(new CellPoint(i, j));
                    }
                    else if ((i == j - 1 && j == m_GameBoard.BoardSize / 2) ||
                        (i == m_GameBoard.BoardSize / 2 && j == i - 1))
                    {
                        m_GameBoard.BoardCells[i, j] = new BoardCell();
                        m_GameBoard.BoardCells[i, j].Sign = BoardCell.eSigns.FirstPlayerSign;
                        m_GameBoard.BoardCells[i, j].Owner = m_Players[OtheloGame.k_FirstPlayer].Id;
                        m_Players[OtheloGame.k_FirstPlayer].AddCell(new CellPoint(i, j));
                    }
                    else
                    {
                        m_GameBoard.BoardCells[i, j] = new BoardCell();
                    }
                }
            }
        }
    }
}
