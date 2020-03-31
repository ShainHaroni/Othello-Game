using System;
using System.Collections.Generic;

namespace OthelloLogic
{
    public class OtheloGame
    {
        internal const int k_FirstPlayer = 0;
        internal const int k_SecondPlayer = 1;
        private Game.ePlayerId m_CurrentPlayerTurn = Game.ePlayerId.None;
        private Game m_Othelo;

        public OtheloGame(List<Player> i_Players, int i_BoardSize)
        {
            m_Othelo = new Game(i_Players, i_BoardSize);
            m_CurrentPlayerTurn = Game.ePlayerId.Player1;
        }

        public Game.ePlayerId CurrentPlayerToPlay
        {
            get { return m_CurrentPlayerTurn; }
            set { m_CurrentPlayerTurn = value; }
        }

        public Game CurrentGame
        {
            get { return m_Othelo; }
        }

        public string GetCurrentPlayerName()
        {
            string playerCoinTurn = string.Empty;
            if (m_Othelo.Players[k_FirstPlayer].Id == m_CurrentPlayerTurn)
            {
                playerCoinTurn = "Red's ";
            }
            else
            {
                playerCoinTurn = "Yellow's ";
            }

            return playerCoinTurn;
        }

        public void UpdateBoard(List<CellPoint> i_CellsToReplace)
        {
            foreach (CellPoint cell in i_CellsToReplace)
            {
                addOccupiedCellToPlayer(cell.Row, cell.Col);
                removeOccupiedCellFromOpponent(cell.Row, cell.Col);
                m_Othelo.GameBoard.BoardCells[cell.Row, cell.Col].Owner = m_CurrentPlayerTurn;
                m_Othelo.GameBoard.BoardCells[cell.Row, cell.Col].Sign = CurrentPlayerSign;
            }
        }

        public bool TryMove(int i_Row, int i_Col, out List<CellPoint> io_CellsToReplace)
        {
            bool isValid;
            BoardCell.eSigns sign;
            List<CellPoint> currentCells = new List<CellPoint>();

            io_CellsToReplace = new List<CellPoint>();
            isValid = isValidCell(i_Row, i_Col);
            if (isValid == true)
            {
                for (int indentRow = -1; indentRow < 2; indentRow++)
                {
                    for (int indentCol = -1; indentCol < 2; indentCol++)
                    {
                        if (indentRow != 0 || indentCol != 0)
                        {
                            sign = m_Othelo.GameBoard.BoardCells[i_Row, i_Col].Sign;
                            if (sign == BoardCell.eSigns.Empty)
                            {
                                currentCells = checkMove(i_Row, i_Col, indentRow, indentCol);
                            }

                            if (currentCells != null)
                            {
                                io_CellsToReplace.AddRange(currentCells);
                            }
                        }
                    }
                }
            }

            return isValid;
        }

        private bool isValidCell(int i_Row, int i_Col)
        {
            int size = m_Othelo.GameBoard.BoardSize;
            bool result = false;
            Game.ePlayerId cellId;

            if ((i_Col >= 0 && i_Col < size) && (i_Row >= 0 && i_Row < size))
            {
                for (int i = i_Row - 1; i < i_Row + 2; i++)
                {
                    for (int j = i_Col - 1; j < i_Col + 2; j++)
                    {
                        if ((i >= 0 && j >= 0 && i < size && j < size) || (i == i_Row && j == i_Col))
                        {
                            cellId = m_Othelo.GameBoard.BoardCells[i, j].Owner;
                            if (cellId == Game.ePlayerId.None)
                            {
                                result = true;
                            }
                        }
                    }
                }
            }

            return result;
        }

        public void ChangeTurn()
        {
            CurrentPlayerToPlay = CurrentPlayerToPlay == CurrentGame.FirstPlayerId ? CurrentGame.SecondPlayerId : CurrentGame.FirstPlayerId;
        }

        public bool IsThereArePossibleMoves()
        {
            bool result = true;

            if ((FindComputerMoves().Count == 0) || (FindComputerMoves() == null))
            {
                result = false;
            }

            return result;
        }

        private List<CellPoint> checkMove(int i_Row, int i_Col, int i_IndentRow, int i_IndentCol)
        {
            List<CellPoint> currentDiscs = new List<CellPoint>();
            Game.ePlayerId currPlayerId = m_CurrentPlayerTurn;
            int size = m_Othelo.GameBoard.BoardSize;
            Game.ePlayerId cellId;
            int i = i_Row + i_IndentRow;
            int j = i_Col + i_IndentCol;

            while (i >= 0 && i < size && j >= 0 && j < size)
            {
                cellId = m_Othelo.GameBoard.BoardCells[i, j].Owner;
                if (cellId == currPlayerId)
                {
                    return currentDiscs;
                }
                else if (cellId == Game.ePlayerId.None)
                {
                    return null;
                }
                else
                {
                    currentDiscs.Add(new CellPoint(i, j));
                    currentDiscs.Add(new CellPoint(i_Row, i_Col));
                }

                i += i_IndentRow;
                j += i_IndentCol;
            }

            return null;
        }

        public BoardCell.eSigns CurrentPlayerSign
        {
            get { return m_CurrentPlayerTurn == Game.ePlayerId.Player1 ? BoardCell.eSigns.FirstPlayerSign : BoardCell.eSigns.SecondPlayerSign; }
        }

        private void addOccupiedCellToPlayer(int i_Row, int i_Col)
        {
            CellPoint toAdd = new CellPoint(i_Row, i_Col);

            if (m_Othelo.Players[k_FirstPlayer].Id == m_CurrentPlayerTurn)
            {
                m_Othelo.Players[k_FirstPlayer].Occupie(toAdd);
            }
            else
            {
                m_Othelo.Players[k_SecondPlayer].Occupie(toAdd);
            }
        }

        private void removeOccupiedCellFromOpponent(int i_Row, int i_Col)
        {
            CellPoint toRemove = new CellPoint(i_Row, i_Col);

            if (m_Othelo.Players[k_SecondPlayer].Id != m_CurrentPlayerTurn)
            {
                m_Othelo.Players[k_FirstPlayer].RemoveCell(toRemove);
            }
            else
            {
                m_Othelo.Players[k_SecondPlayer].RemoveCell(toRemove);
            }
        }

        public List<CellPoint> FindComputerMoves()
        {
            List<CellPoint> possibleMoves = new List<CellPoint>();
            List<CellPoint> legalCells = new List<CellPoint>();

            for (int i = 0; i < CurrentGame.GameBoard.BoardSize; i++)
            {
                for (int j = 0; j < CurrentGame.GameBoard.BoardSize; j++)
                {
                    TryMove(i, j, out possibleMoves);
                    if (possibleMoves.Count > 0)
                    {
                        legalCells.Add(new CellPoint(i, j));
                    }
                }
            }

            return legalCells;
        }

        public void RandInstruction(List<CellPoint> i_Moves)
        {
            Random rnd = new Random();
            int length = i_Moves.Count;
            int rndIndex = rnd.Next(0, length);
            List<CellPoint> cellsToUpdate = new List<CellPoint>();

            if (length > 0)
            {
                TryMove(i_Moves[rndIndex].Row, i_Moves[rndIndex].Col, out cellsToUpdate);
            }

            if (cellsToUpdate.Count > 0)
            {
                UpdateBoard(cellsToUpdate);
            }

            m_CurrentPlayerTurn = m_Othelo.Players[k_FirstPlayer].Id;
        }

        public void MakeComputerMoves()
        {
            this.RandInstruction(this.FindComputerMoves());
            while (!this.IsThereArePossibleMoves() && !this.CurrentGame.GameBoard.IsFull())
            {
                this.RandInstruction(this.FindComputerMoves());
            }
        }

        public string GetWinnerName()
        {
            string name = string.Empty;

            if (m_Othelo.Players[0].Score > m_Othelo.Players[1].Score)
            {
                name = string.Format(
@"Red Won! ({0}/{1})",
m_Othelo.FirstPlayerScore,
m_Othelo.SecondPlayerScore);
            }
            else
            {
                if (m_Othelo.Players[0].Score == m_Othelo.Players[1].Score)
                {
                    name = string.Format(
@"The game is tie. ({0}/{1})",
m_Othelo.FirstPlayerScore,
m_Othelo.SecondPlayerScore);
                }
                else
                {
                    name = string.Format(
@"Yellow Won! ({0}/{1})",
m_Othelo.FirstPlayerScore,
m_Othelo.SecondPlayerScore);
                }
            }

            return name;
        }
    }
}
