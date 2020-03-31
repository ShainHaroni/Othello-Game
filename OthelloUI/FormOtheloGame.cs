using System;
using System.Collections.Generic;
using System.Windows.Forms;
using OthelloLogic;

namespace OthelloUI
{
    public partial class FormOtheloGame : Form
    {
        private const int k_PictureBoxSize = 60;
        private const int k_SpaceFromLeft = 30;
        private const int k_SpaceFromTop = 30;
        private int m_BoardSize;
        private int m_RedWins = 0;
        private int m_YellowWins = 0;
        private bool m_IsTwoPlayers = false;
        private OtheloGame m_Game;
        private PictureBoxCell[,] m_PictureBoxesBoard;

        public FormOtheloGame(bool i_IsTwoPlayers, int i_BoardSize, OtheloGame i_Game)
        {
            m_Game = i_Game;
            m_BoardSize = i_BoardSize;
            m_IsTwoPlayers = i_IsTwoPlayers;
            m_PictureBoxesBoard = new PictureBoxCell[m_BoardSize, m_BoardSize];
            initializePictureBoxBoard(m_Game.CurrentGame);
            InitializeComponent();
        }

        private void FormOtheloGame_Load(object sender, EventArgs e)
        {
            updateTitle();
            showScore();
            showValidCells();
        }

        private void showValidCells()
        {
            List<CellPoint> validCells = m_Game.FindComputerMoves();
            
            foreach (CellPoint cell in validCells)
            {
                m_PictureBoxesBoard[cell.Row, cell.Col].Image = Properties.Resources.Valid;
            }
        }

        private void updateTitle()
        {
            this.Text = "Othelo - " + m_Game.GetCurrentPlayerName() + "Turn";
        }

        private void showScore()
        {
            m_Game.CurrentGame.CalculateScore();
        }

        private void initializePictureBoxBoard(Game i_Game)
        {
            int top = k_SpaceFromTop;
            int left = k_SpaceFromLeft;

            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    m_PictureBoxesBoard[i, j] = initializePictureBox(i, j, top, left);

                    if (i_Game.GameBoard.BoardCells[i, j].Sign == BoardCell.eSigns.SecondPlayerSign)
                    {
                        m_PictureBoxesBoard[i, j].Image = Properties.Resources.CoinYellow;
                        m_PictureBoxesBoard[i, j].Enabled = false;
                    }
                    else if (i_Game.GameBoard.BoardCells[i, j].Sign == BoardCell.eSigns.FirstPlayerSign)
                    {
                        m_PictureBoxesBoard[i, j].Image = Properties.Resources.CoinRed;
                        m_PictureBoxesBoard[i, j].Enabled = false;
                    }
                    
                    this.Controls.Add(m_PictureBoxesBoard[i, j]);
                    m_PictureBoxesBoard[i, j].Click += PictureBox_Click;
                    left += k_PictureBoxSize;
                }

                left = k_SpaceFromLeft;
                top += k_PictureBoxSize;
            }
        }

        private PictureBoxCell initializePictureBox(int i_Row, int i_Col, int i_Top, int i_Left)
        {
            PictureBoxCell pictureBoxCell = new PictureBoxCell(i_Row, i_Col);
            pictureBoxCell.Left = i_Left;
            pictureBoxCell.Top = i_Top;
            pictureBoxCell.Width = k_PictureBoxSize;
            pictureBoxCell.Height = k_PictureBoxSize;
            pictureBoxCell.BorderStyle = BorderStyle.Fixed3D;
            pictureBoxCell.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxCell.BackgroundImage = Properties.Resources.Background;

            return pictureBoxCell;
        }

        private void updatePictureBoxBoard(Game i_Game)
        {
            for (int i = 0; i < m_BoardSize; i++)
            {
                for (int j = 0; j < m_BoardSize; j++)
                {
                    if (i_Game.GameBoard.BoardCells[i, j].Sign == BoardCell.eSigns.SecondPlayerSign)
                    {
                        m_PictureBoxesBoard[i, j].Image = Properties.Resources.CoinYellow;
                        m_PictureBoxesBoard[i, j].Enabled = false;
                    }
                    else if (i_Game.GameBoard.BoardCells[i, j].Sign == BoardCell.eSigns.FirstPlayerSign)
                    {
                        m_PictureBoxesBoard[i, j].Image = Properties.Resources.CoinRed;
                        m_PictureBoxesBoard[i, j].Enabled = false;
                    }
                    else
                    {
                        m_PictureBoxesBoard[i, j].Image = null;
                        m_PictureBoxesBoard[i, j].Enabled = true;
                    }
                }
            }
        }

        private void winsCounter()
        {
            if (m_Game.CurrentGame.FirstPlayerScore > m_Game.CurrentGame.SecondPlayerScore)
            {
                m_RedWins++;
            }
            else if (m_Game.CurrentGame.FirstPlayerScore < m_Game.CurrentGame.SecondPlayerScore)
            {
                m_YellowWins++;
            }
        }

        public void EndGame()
        {
            string endGameMessage;
            DialogResult answer;

            if (m_Game.CurrentGame != null)
            {
                winsCounter();
                updatePictureBoxBoard(m_Game.CurrentGame);
                endGameMessage = string.Format(
@"{0} ({1}/{2})
Would you like another round?",
m_Game.GetWinnerName(), 
m_RedWins,
m_YellowWins);
                answer = MessageBox.Show(endGameMessage, "Othelo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (answer == DialogResult.Yes)
                {
                    m_Game.CurrentGame.InitializeBoard();
                    m_Game.CurrentGame.CalculateScore();
                    showScore();
                    m_Game.CurrentPlayerToPlay = Game.ePlayerId.Player1;
                    this.updatePictureBoxBoard(m_Game.CurrentGame);
                    showValidCells();
                    updateTitle();
                    this.Refresh();
                }
                else
                {
                    m_RedWins = 0;
                    m_YellowWins = 0;
                    this.Close();
                }
            }
        }

        private void PictureBox_Click(object sender, EventArgs e)
        {
            PictureBoxCell cell = sender as PictureBoxCell;
            List<CellPoint> cellsToReplace = new List<CellPoint>();

            if (m_Game.TryMove(cell.Row, cell.Col, out cellsToReplace) && cellsToReplace.Count > 0)
            {
                m_Game.UpdateBoard(cellsToReplace);
                m_Game.ChangeTurn();
            }

            if (m_Game.IsThereArePossibleMoves())
            {
                if (m_Game.CurrentPlayerToPlay == Game.ePlayerId.Computer)
                {
                    m_Game.MakeComputerMoves();
                }
            }
            else 
            {
                m_Game.ChangeTurn();
                if (!m_Game.IsThereArePossibleMoves())
                {
                    showScore();
                    EndGame();
                    return;
                }
            }

            showScore();
            this.updatePictureBoxBoard(m_Game.CurrentGame);
            showValidCells();
            updateTitle();
            this.Refresh();
            if (m_Game.CurrentGame.GameBoard.IsFull())
            {
                EndGame();
            }
        }
    }
}
