using System;
using System.Collections.Generic;
using System.Text;
using OthelloLogic;

namespace OthelloUI
{
    public class GameManager
    {
        private OtheloGame m_Game;
        private FormOtheloGame m_FormGame;
        private FormGameSettings m_FormSettings;

        public GameManager()
        {
            m_FormSettings = new FormGameSettings();
            m_FormSettings.ShowDialog();
            initializeLogicGame();
            m_FormGame = new FormOtheloGame(m_FormSettings.IsTwoPlayer, m_FormSettings.SelectedBoardSize, m_Game);
            m_FormGame.ShowDialog();
        }

        private void initializeLogicGame()
        {
            int boardSize = m_FormSettings.SelectedBoardSize;
            List<Player> players = new List<Player>();

            players.Add(new Player("Player 1", Game.ePlayerId.Player1));
            if (m_FormSettings.IsTwoPlayer)
            {
                players.Add(new Player("Player 2", Game.ePlayerId.Player2));
            }
            else
            {
                players.Add(new Player("Computer", Game.ePlayerId.Computer));
            }

            m_Game = new OtheloGame(players, boardSize);
        }
    }
}
