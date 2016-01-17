namespace Poker
{
    partial class Form1
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
            this.buttonFold = new System.Windows.Forms.Button();
            this.buttonCheck = new System.Windows.Forms.Button();
            this.buttonCall = new System.Windows.Forms.Button();
            this.buttonRaise = new System.Windows.Forms.Button();
            this.progressBarTimer = new System.Windows.Forms.ProgressBar();
            this.textboxChips = new System.Windows.Forms.TextBox();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.textboxAdd = new System.Windows.Forms.TextBox();
            this.textboxFifthBotChips = new System.Windows.Forms.TextBox();
            this.textboxFourthBotChips = new System.Windows.Forms.TextBox();
            this.textbokThirdBotChips = new System.Windows.Forms.TextBox();
            this.textboxSecondBotChips = new System.Windows.Forms.TextBox();
            this.textboxFirstBotChips = new System.Windows.Forms.TextBox();
            this.textboxPot = new System.Windows.Forms.TextBox();
            this.buttonOptions = new System.Windows.Forms.Button();
            this.buttonBigBlind = new System.Windows.Forms.Button();
            this.textboxSmallBlind = new System.Windows.Forms.TextBox();
            this.buttonSmallBlind = new System.Windows.Forms.Button();
            this.textboxBigBlind = new System.Windows.Forms.TextBox();
            this.fifthBotStatus = new System.Windows.Forms.Label();
            this.fourthBotStatus = new System.Windows.Forms.Label();
            this.thirdBotStatus = new System.Windows.Forms.Label();
            this.firstBotStatus = new System.Windows.Forms.Label();
            this.playerStatus = new System.Windows.Forms.Label();
            this.secondBotStatus = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.textboxRaise = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // bFold
            // 
            this.buttonFold.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonFold.Font = new System.Drawing.Font("Microsoft Sans Serif", 17F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonFold.Location = new System.Drawing.Point(335, 660);
            this.buttonFold.Name = "bFold";
            this.buttonFold.Size = new System.Drawing.Size(130, 62);
            this.buttonFold.TabIndex = 0;
            this.buttonFold.Text = "Fold";
            this.buttonFold.UseVisualStyleBackColor = true;
            this.buttonFold.Click += new System.EventHandler(this.bFold_Click);
            // 
            // bCheck
            // 
            this.buttonCheck.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCheck.Location = new System.Drawing.Point(494, 660);
            this.buttonCheck.Name = "bCheck";
            this.buttonCheck.Size = new System.Drawing.Size(134, 62);
            this.buttonCheck.TabIndex = 2;
            this.buttonCheck.Text = "Check";
            this.buttonCheck.UseVisualStyleBackColor = true;
            this.buttonCheck.Click += new System.EventHandler(this.bCheck_Click);
            // 
            // bCall
            // 
            this.buttonCall.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonCall.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonCall.Location = new System.Drawing.Point(667, 661);
            this.buttonCall.Name = "bCall";
            this.buttonCall.Size = new System.Drawing.Size(126, 62);
            this.buttonCall.TabIndex = 3;
            this.buttonCall.Text = "Call";
            this.buttonCall.UseVisualStyleBackColor = true;
            this.buttonCall.Click += new System.EventHandler(this.bCall_Click);
            // 
            // bRaise
            // 
            this.buttonRaise.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.buttonRaise.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonRaise.Location = new System.Drawing.Point(835, 661);
            this.buttonRaise.Name = "bRaise";
            this.buttonRaise.Size = new System.Drawing.Size(124, 62);
            this.buttonRaise.TabIndex = 4;
            this.buttonRaise.Text = "Raise";
            this.buttonRaise.UseVisualStyleBackColor = true;
            this.buttonRaise.Click += new System.EventHandler(this.bRaise_Click);
            // 
            // pbTimer
            // 
            this.progressBarTimer.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.progressBarTimer.BackColor = System.Drawing.SystemColors.Control;
            this.progressBarTimer.Location = new System.Drawing.Point(335, 631);
            this.progressBarTimer.Maximum = 1000;
            this.progressBarTimer.Name = "pbTimer";
            this.progressBarTimer.Size = new System.Drawing.Size(667, 23);
            this.progressBarTimer.TabIndex = 5;
            this.progressBarTimer.Value = 1000;
            // 
            // tbChips
            // 
            this.textboxChips.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textboxChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textboxChips.Location = new System.Drawing.Point(755, 553);
            this.textboxChips.Name = "tbChips";
            this.textboxChips.Size = new System.Drawing.Size(163, 23);
            this.textboxChips.TabIndex = 6;
            this.textboxChips.Text = "Chips : 0";
            // 
            // bAdd
            // 
            this.buttonAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.buttonAdd.Location = new System.Drawing.Point(12, 697);
            this.buttonAdd.Name = "bAdd";
            this.buttonAdd.Size = new System.Drawing.Size(75, 25);
            this.buttonAdd.TabIndex = 7;
            this.buttonAdd.Text = "AddChips";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.bAdd_Click);
            // 
            // tbAdd
            // 
            this.textboxAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textboxAdd.Location = new System.Drawing.Point(93, 700);
            this.textboxAdd.Name = "tbAdd";
            this.textboxAdd.Size = new System.Drawing.Size(125, 20);
            this.textboxAdd.TabIndex = 8;
            // 
            // tbBotChips5
            // 
            this.textboxFifthBotChips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxFifthBotChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textboxFifthBotChips.Location = new System.Drawing.Point(1012, 553);
            this.textboxFifthBotChips.Name = "tbBotChips5";
            this.textboxFifthBotChips.Size = new System.Drawing.Size(152, 23);
            this.textboxFifthBotChips.TabIndex = 9;
            this.textboxFifthBotChips.Text = "Chips : 0";
            // 
            // tbBotChips4
            // 
            this.textboxFourthBotChips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textboxFourthBotChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textboxFourthBotChips.Location = new System.Drawing.Point(970, 81);
            this.textboxFourthBotChips.Name = "tbBotChips4";
            this.textboxFourthBotChips.Size = new System.Drawing.Size(123, 23);
            this.textboxFourthBotChips.TabIndex = 10;
            this.textboxFourthBotChips.Text = "Chips : 0";
            // 
            // tbBotChips3
            // 
            this.textbokThirdBotChips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textbokThirdBotChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textbokThirdBotChips.Location = new System.Drawing.Point(755, 81);
            this.textbokThirdBotChips.Name = "tbBotChips3";
            this.textbokThirdBotChips.Size = new System.Drawing.Size(125, 23);
            this.textbokThirdBotChips.TabIndex = 11;
            this.textbokThirdBotChips.Text = "Chips : 0";
            // 
            // tbBotChips2
            // 
            this.textboxSecondBotChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textboxSecondBotChips.Location = new System.Drawing.Point(276, 81);
            this.textboxSecondBotChips.Name = "tbBotChips2";
            this.textboxSecondBotChips.Size = new System.Drawing.Size(133, 23);
            this.textboxSecondBotChips.TabIndex = 12;
            this.textboxSecondBotChips.Text = "Chips : 0";
            // 
            // tbBotChips1
            // 
            this.textboxFirstBotChips.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textboxFirstBotChips.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textboxFirstBotChips.Location = new System.Drawing.Point(181, 553);
            this.textboxFirstBotChips.Name = "tbBotChips1";
            this.textboxFirstBotChips.Size = new System.Drawing.Size(142, 23);
            this.textboxFirstBotChips.TabIndex = 13;
            this.textboxFirstBotChips.Text = "Chips : 0";
            // 
            // tbPot
            // 
            this.textboxPot.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.textboxPot.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.textboxPot.Location = new System.Drawing.Point(606, 212);
            this.textboxPot.Name = "tbPot";
            this.textboxPot.Size = new System.Drawing.Size(125, 23);
            this.textboxPot.TabIndex = 14;
            this.textboxPot.Text = "0";
            // 
            // bOptions
            // 
            this.buttonOptions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonOptions.Location = new System.Drawing.Point(12, 12);
            this.buttonOptions.Name = "bOptions";
            this.buttonOptions.Size = new System.Drawing.Size(75, 36);
            this.buttonOptions.TabIndex = 15;
            this.buttonOptions.Text = "BB/SB";
            this.buttonOptions.UseVisualStyleBackColor = true;
            this.buttonOptions.Click += new System.EventHandler(this.bOptions_Click);
            // 
            // bBB
            // 
            this.buttonBigBlind.Location = new System.Drawing.Point(12, 254);
            this.buttonBigBlind.Name = "bBB";
            this.buttonBigBlind.Size = new System.Drawing.Size(75, 23);
            this.buttonBigBlind.TabIndex = 16;
            this.buttonBigBlind.Text = "Big Blind";
            this.buttonBigBlind.UseVisualStyleBackColor = true;
            this.buttonBigBlind.Click += new System.EventHandler(this.bBB_Click);
            // 
            // tbSB
            // 
            this.textboxSmallBlind.Location = new System.Drawing.Point(12, 228);
            this.textboxSmallBlind.Name = "tbSB";
            this.textboxSmallBlind.Size = new System.Drawing.Size(75, 20);
            this.textboxSmallBlind.TabIndex = 17;
            this.textboxSmallBlind.Text = "250";
            // 
            // bSB
            // 
            this.buttonSmallBlind.Location = new System.Drawing.Point(12, 199);
            this.buttonSmallBlind.Name = "bSB";
            this.buttonSmallBlind.Size = new System.Drawing.Size(75, 23);
            this.buttonSmallBlind.TabIndex = 18;
            this.buttonSmallBlind.Text = "Small Blind";
            this.buttonSmallBlind.UseVisualStyleBackColor = true;
            this.buttonSmallBlind.Click += new System.EventHandler(this.bSB_Click);
            // 
            // tbBB
            // 
            this.textboxBigBlind.Location = new System.Drawing.Point(12, 283);
            this.textboxBigBlind.Name = "tbBB";
            this.textboxBigBlind.Size = new System.Drawing.Size(75, 20);
            this.textboxBigBlind.TabIndex = 19;
            this.textboxBigBlind.Text = "500";
            // 
            // b5Status
            // 
            this.fifthBotStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.fifthBotStatus.Location = new System.Drawing.Point(1012, 579);
            this.fifthBotStatus.Name = "b5Status";
            this.fifthBotStatus.Size = new System.Drawing.Size(152, 32);
            this.fifthBotStatus.TabIndex = 26;
            // 
            // b4Status
            // 
            this.fourthBotStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fourthBotStatus.Location = new System.Drawing.Point(970, 107);
            this.fourthBotStatus.Name = "b4Status";
            this.fourthBotStatus.Size = new System.Drawing.Size(123, 32);
            this.fourthBotStatus.TabIndex = 27;
            // 
            // b3Status
            // 
            this.thirdBotStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.thirdBotStatus.Location = new System.Drawing.Point(755, 107);
            this.thirdBotStatus.Name = "b3Status";
            this.thirdBotStatus.Size = new System.Drawing.Size(125, 32);
            this.thirdBotStatus.TabIndex = 28;
            // 
            // b1Status
            // 
            this.firstBotStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.firstBotStatus.Location = new System.Drawing.Point(181, 579);
            this.firstBotStatus.Name = "b1Status";
            this.firstBotStatus.Size = new System.Drawing.Size(142, 32);
            this.firstBotStatus.TabIndex = 29;
            // 
            // pStatus
            // 
            this.playerStatus.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.playerStatus.Location = new System.Drawing.Point(755, 579);
            this.playerStatus.Name = "pStatus";
            this.playerStatus.Size = new System.Drawing.Size(163, 32);
            this.playerStatus.TabIndex = 30;
            // 
            // b2Status
            // 
            this.secondBotStatus.Location = new System.Drawing.Point(276, 107);
            this.secondBotStatus.Name = "b2Status";
            this.secondBotStatus.Size = new System.Drawing.Size(133, 32);
            this.secondBotStatus.TabIndex = 31;
            // 
            // label1
            // 
            this.label.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label.Location = new System.Drawing.Point(654, 188);
            this.label.Name = "label1";
            this.label.Size = new System.Drawing.Size(31, 21);
            this.label.TabIndex = 0;
            this.label.Text = "Pot";
            // 
            // tbRaise
            // 
            this.textboxRaise.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.textboxRaise.Location = new System.Drawing.Point(965, 703);
            this.textboxRaise.Name = "tbRaise";
            this.textboxRaise.Size = new System.Drawing.Size(108, 20);
            this.textboxRaise.TabIndex = 0;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::Poker.Properties.Resources.poker_table___Copy;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1350, 729);
            this.Controls.Add(this.textboxRaise);
            this.Controls.Add(this.label);
            this.Controls.Add(this.secondBotStatus);
            this.Controls.Add(this.playerStatus);
            this.Controls.Add(this.firstBotStatus);
            this.Controls.Add(this.thirdBotStatus);
            this.Controls.Add(this.fourthBotStatus);
            this.Controls.Add(this.fifthBotStatus);
            this.Controls.Add(this.textboxBigBlind);
            this.Controls.Add(this.buttonSmallBlind);
            this.Controls.Add(this.textboxSmallBlind);
            this.Controls.Add(this.buttonBigBlind);
            this.Controls.Add(this.buttonOptions);
            this.Controls.Add(this.textboxPot);
            this.Controls.Add(this.textboxFirstBotChips);
            this.Controls.Add(this.textboxSecondBotChips);
            this.Controls.Add(this.textbokThirdBotChips);
            this.Controls.Add(this.textboxFourthBotChips);
            this.Controls.Add(this.textboxFifthBotChips);
            this.Controls.Add(this.textboxAdd);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.textboxChips);
            this.Controls.Add(this.progressBarTimer);
            this.Controls.Add(this.buttonRaise);
            this.Controls.Add(this.buttonCall);
            this.Controls.Add(this.buttonCheck);
            this.Controls.Add(this.buttonFold);
            this.DoubleBuffered = true;
            this.Name = "Form1";
            this.Text = "GLS Texas Poker";
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.Layout_Change);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonFold;
        private System.Windows.Forms.Button buttonCheck;
        private System.Windows.Forms.Button buttonCall;
        private System.Windows.Forms.Button buttonRaise;
        private System.Windows.Forms.ProgressBar progressBarTimer;
        private System.Windows.Forms.TextBox textboxChips;
        private System.Windows.Forms.Button buttonAdd;
        private System.Windows.Forms.TextBox textboxAdd;
        private System.Windows.Forms.TextBox textboxFifthBotChips;
        private System.Windows.Forms.TextBox textboxFourthBotChips;
        private System.Windows.Forms.TextBox textbokThirdBotChips;
        private System.Windows.Forms.TextBox textboxSecondBotChips;
        private System.Windows.Forms.TextBox textboxFirstBotChips;
        private System.Windows.Forms.TextBox textboxPot;
        private System.Windows.Forms.Button buttonOptions;
        private System.Windows.Forms.Button buttonBigBlind;
        private System.Windows.Forms.TextBox textboxSmallBlind;
        private System.Windows.Forms.Button buttonSmallBlind;
        private System.Windows.Forms.TextBox textboxBigBlind;
        private System.Windows.Forms.Label fifthBotStatus;
        private System.Windows.Forms.Label fourthBotStatus;
        private System.Windows.Forms.Label thirdBotStatus;
        private System.Windows.Forms.Label firstBotStatus;
        private System.Windows.Forms.Label playerStatus;
        private System.Windows.Forms.Label secondBotStatus;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.TextBox textboxRaise;



    }
}

