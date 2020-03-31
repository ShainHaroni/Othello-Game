using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OthelloUI
{
    public partial class FormGameSettings : Form
    {
        private const int k_DefaultBoardSize = 6;
        private const int k_MaxBoardSize = 12;
        private int m_BoardSize;
        private bool m_IsTwoPlayers = false;

        public FormGameSettings()
        {
            InitializeComponent();
        }

        public int SelectedBoardSize
        {
            get { return m_BoardSize; }
        }

        public bool IsTwoPlayer
        {
            get { return m_IsTwoPlayers; }
        }

        private void buttonBoardSize_Click(object sender, EventArgs e)
        {
            string buttonText;

            m_BoardSize = m_BoardSize == k_MaxBoardSize ? k_DefaultBoardSize : (m_BoardSize + 2);
            buttonText = string.Format(
@"Board Size {0}x{0} (Click to increase)",
m_BoardSize,
m_BoardSize);
            (sender as Button).Text = buttonText;
        }

        private void FormGameSettings_Load(object sender, EventArgs e)
        {
            m_BoardSize = k_DefaultBoardSize;
        }

        private void buttonVsComputer_Click(object sender, EventArgs e)
        {
            m_IsTwoPlayers = false;
            this.Visible = false;
            this.Close();
        }

        private void buttonVsFriend_Click(object sender, EventArgs e)
        {
            m_IsTwoPlayers = true;
            this.Visible = false;
            this.Close();
        }

        private void FormGameSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.Cancel)
            {
                Application.Exit();
            }
        }
    }
}
