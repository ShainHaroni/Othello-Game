using System.Collections.Generic;

namespace OthelloLogic
{
    public class Player
    {
        private readonly List<CellPoint> m_OccupiedCells = null;

        private string m_Name;
        private int m_Score;
        private Game.ePlayerId m_Id;

        public Player(string i_Name, Game.ePlayerId i_PlayerId)
        {
            m_Name = i_Name;
            m_Id = i_PlayerId;
            m_OccupiedCells = new List<CellPoint>();
        }

        public string Name
        {
            get { return m_Name; }
        }

        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        public Game.ePlayerId Id
        {
            get { return m_Id; }
        }

        public void AddCell(CellPoint i_CellToAdd)
        {
            m_OccupiedCells.Add(i_CellToAdd);
        }

        public void RemoveCell(CellPoint i_CellToRemove)
        {
            m_OccupiedCells.Remove(i_CellToRemove);
        }

        public void Occupie(CellPoint i_CellToOccupie)
        {
            AddCell(i_CellToOccupie);
        }
    }
}
