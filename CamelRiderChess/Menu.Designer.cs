namespace CamelRiderChess
{
    partial class Menu
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
            this.RobotButton = new System.Windows.Forms.Button();
            this.FriendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // RobotButton
            // 
            this.RobotButton.Location = new System.Drawing.Point(291, 60);
            this.RobotButton.Name = "RobotButton";
            this.RobotButton.Size = new System.Drawing.Size(231, 105);
            this.RobotButton.TabIndex = 0;
            this.RobotButton.Text = "Play with a robot";
            this.RobotButton.UseVisualStyleBackColor = true;
            this.RobotButton.Click += new System.EventHandler(this.RobotButton_Click);
            // 
            // FriendButton
            // 
            this.FriendButton.Location = new System.Drawing.Point(291, 203);
            this.FriendButton.Name = "FriendButton";
            this.FriendButton.Size = new System.Drawing.Size(231, 105);
            this.FriendButton.TabIndex = 1;
            this.FriendButton.Text = "Play with a friend";
            this.FriendButton.UseVisualStyleBackColor = true;
            this.FriendButton.Click += new System.EventHandler(this.FriendButton_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.FriendButton);
            this.Controls.Add(this.RobotButton);
            this.Name = "Menu";
            this.Text = "Menu";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RobotButton;
        private System.Windows.Forms.Button FriendButton;
    }
}