namespace OthelloLogic
{
    public class BoardCell
    {
        public enum eSigns
        {
            Empty = ' ',
            FirstPlayerSign = 'X',
            SecondPlayerSign = 'O',
            CellSeperator = '=',
        }

        private eSigns m_Sign = eSigns.Empty;
        private Game.ePlayerId m_CellOwner = Game.ePlayerId.None;

        public eSigns Sign
        {
            get { return m_Sign; }
            set { m_Sign = value; }
        }

        public Game.ePlayerId Owner
        {
            get { return m_CellOwner; }
            set { m_CellOwner = value; }
        }
    }
}