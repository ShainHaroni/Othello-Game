namespace OthelloUI
{
    public partial class FormGameSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }

            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonBoardSize = new System.Windows.Forms.Button();
            this.buttonVsComputer = new System.Windows.Forms.Button();
            this.buttonVsFriend = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonBoardSize
            // 
            this.buttonBoardSize.AutoSize = true;
            this.buttonBoardSize.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonBoardSize.Location = new System.Drawing.Point(12, 12);
            this.buttonBoardSize.Name = "buttonBoardSize";
            this.buttonBoardSize.Size = new System.Drawing.Size(394, 49);
            this.buttonBoardSize.TabIndex = 0;
            this.buttonBoardSize.Text = "Board Size: 6x6 (Click to increase)";
            this.buttonBoardSize.UseVisualStyleBackColor = true;
            this.buttonBoardSize.Click += new System.EventHandler(this.buttonBoardSize_Click);
            // 
            // buttonVsComputer
            // 
            this.buttonVsComputer.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonVsComputer.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonVsComputer.Location = new System.Drawing.Point(12, 67);
            this.buttonVsComputer.Name = "buttonVsComputer";
            this.buttonVsComputer.Size = new System.Drawing.Size(160, 61);
            this.buttonVsComputer.TabIndex = 1;
            this.buttonVsComputer.Text = "Play against the computer";
            this.buttonVsComputer.UseVisualStyleBackColor = true;
            this.buttonVsComputer.Click += new System.EventHandler(this.buttonVsComputer_Click);
            // 
            // buttonVsFriend
            // 
            this.buttonVsFriend.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonVsFriend.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonVsFriend.Location = new System.Drawing.Point(178, 67);
            this.buttonVsFriend.Name = "buttonVsFriend";
            this.buttonVsFriend.Size = new System.Drawing.Size(228, 61);
            this.buttonVsFriend.TabIndex = 2;
            this.buttonVsFriend.Text = "Play against your friend";
            this.buttonVsFriend.UseVisualStyleBackColor = true;
            this.buttonVsFriend.Click += new System.EventHandler(this.buttonVsFriend_Click);
            // 
            // FormGameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(418, 148);
            this.Controls.Add(this.buttonVsFriend);
            this.Controls.Add(this.buttonVsComputer);
            this.Controls.Add(this.buttonBoardSize);
            this.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FormGameSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Othelo - Game Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormGameSettings_FormClosing);
            this.Load += new System.EventHandler(this.FormGameSettings_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonBoardSize;
        private System.Windows.Forms.Button buttonVsComputer;
        private System.Windows.Forms.Button buttonVsFriend;
    }
}