namespace Poker
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    /// <summary>
    /// Form1 class initialize all the fields
    /// </summary>
    public partial class Form1 : Form
    {
        #region Variables

        private readonly List<bool?> playersInGameStatuses = new List<bool?>();
        private readonly List<Hand> winningHands = new List<Hand>();
        private readonly List<string> winnersStringMsgs = new List<string>();
        private readonly List<int> potContributions = new List<int>();

        private readonly Panel playerPanel = new Panel();
        private readonly Panel firstBotPanel = new Panel();
        private readonly Panel secondBotPanel = new Panel();
        private readonly Panel thirdBotPanel = new Panel();
        private readonly Panel fourthBotPanel = new Panel();
        private readonly Panel fifthBotPanel = new Panel();

        private readonly int[] all17CardsInPlay = new int[17];

        private readonly Image[] deckImageArr = new Image[52];

        private readonly PictureBox[] deckPictureBoxArr = new PictureBox[52];

        private readonly Timer timer = new Timer();

        private readonly Timer updates = new Timer();

        private int amountToCall = 500;

        private int foldedPlayers = 5;

        private int playerChips = 10000;
        private int firstBotChips = 10000;
        private int secondBotChips = 10000;
        private int thirdBotChips = 10000;
        private int fourthBotChips = 10000;
        private int fifthBotChips = 10000;

        private double type;
        private double rounds;

        private double firstBotPower;
        private double secondBotPower;
        private double thirdBotPower;
        private double fourthBotPower;
        private double fifthBotPower;
        private double playerPower;

        private double raiseAmount;

        private double playerType = -1;
        private double firstBotType = -1;
        private double secondBotType = -1;
        private double thirdBotType = -1;
        private double fourthBotType = -1;
        private double fifthBotType = -1;

        private bool playerTurn = true;
        private bool firstBotTurn;
        private bool secondBotTurn;
        private bool thirdBotTurn;
        private bool fourthBotTurn;
        private bool fifthBotTurn;
        
        private bool playerFoldedTurn;
        private bool firstBotFoldedTurn;
        private bool secodBotFoldedTurn;
        private bool thirdBotFoldedTurn;
        private bool fourthBotFoldedTurn;
        private bool fifthBotFoldedTurn;

        private bool playerFolded;
        private bool firstBotFolded;
        private bool secondBotFolded;
        private bool thirdBotFolded;
        private bool fourthBotFolded;
        private bool fifthBotFolded;

        private bool potIsNotEmpty;

        private bool changedRound;

        private int playerCallAmount;
        private int firstBotCallAmount;
        private int secondBotCallAmount;
        private int thirdBotCallAmount;
        private int fourthBotCallAmount;
        private int fifthBotCallAmount;

        private int playerRaiseAmount;
        private int firstBotRaiseAmount;
        private int secondBotRaiseAmount;
        private int thirdBotRaiseAmount;
        private int fourthBotRaiseAmount;
        private int fifthBotRaiseAmount;

        private int height;
        private int width;

        private int winnersCount;

        private int flop = 1;
        private int turn = 2;
        private int river = 3;
        private int end = 4;
        private int playersLeftToAct = 6;
        private int lastOneToAct = 126;
        private int raisedTurn = 1;

        private bool restart;
        private bool raising;

        private Hand sorted;

        private string[] cardImagePaths = Directory.GetFiles("Resources\\Cards", "*.png", SearchOption.TopDirectoryOnly);
        //private string[] ImgLocation =
        //    {
        //           "Resources\\Cards\\33.png", "Resources\\Cards\\22.png",
        //            "Resources\\Cards\\29.png", "Resources\\Cards\\21.png",
        //            "Resources\\Cards\\36.png", "Resources\\Cards\\17.png",
        //            "Resources\\Cards\\40.png", "Resources\\Cards\\16.png",
        //            "Resources\\Cards\\5.png", "Resources\\Cards\\47.png",
        //            "Resources\\Cards\\37.png", "Resources\\Cards\\13.png",
        //            "Resources\\Cards\\12.png", "Resources\\Cards\\18.png",
        //            "Resources\\Cards\\8.png", "Resources\\Cards\\27.png",
        //            "Resources\\Cards\\15.png"
        //};

        private int timeTillNextTurn = 60;
        private int bigBlind = 500;
        private int smallBlind = 250;
        private int up = 10000000;
        private int turnCount;

        //initializing i instead of doing it over and over again??
        private int i;

        #endregion

        public Form1()
        {
           // playersInGameStatuses.Add(PFturn); playersInGameStatuses.Add(B1Fturn); playersInGameStatuses.Add(B2Fturn); playersInGameStatuses.Add(B3Fturn); playersInGameStatuses.Add(B4Fturn); playersInGameStatuses.Add(B5Fturn);
            this.amountToCall = bigBlind;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            this.updates.Start();
            
            this.InitializeComponent();
            
            this.width = Width;
            this.height = Height;
             
            Shuffle();

            this.textboxPot.Enabled = false;
            this.textboxPlayerChips.Enabled = false;
            
            this.textboxFirstBotChips.Enabled = false;
            this.textboxSecondBotChips.Enabled = false;
            this.textbokThirdBotChips.Enabled = false;
            this.textboxFourthBotChips.Enabled = false;
            this.textboxFifthBotChips.Enabled = false;
            
            this.textboxPlayerChips.Text = @"Chips : " + this.playerChips;
            this.textboxFirstBotChips.Text = @"Chips : " + this.firstBotChips;
            this.textboxSecondBotChips.Text = @"Chips : " + this.secondBotChips;
            this.textbokThirdBotChips.Text = @"Chips : " + this.thirdBotChips;
            this.textboxFourthBotChips.Text = @"Chips : " + this.fourthBotChips;
            this.textboxFifthBotChips.Text = @"Chips : " + this.fifthBotChips;

            this.timer.Interval = 1000;
            this.timer.Tick += timer_Tick;
            this.updates.Interval = 100;
            this.updates.Tick += Update_Tick;

           this.textboxSmallBlind.Visible = true;
           this.textboxBigBlind.Visible = true;
           this.buttonBigBlind.Visible = true;
           this.buttonSmallBlind.Visible = true;
           this.textboxBigBlind.Visible = true;
           this.textboxSmallBlind.Visible = true;
           this.buttonBigBlind.Visible = true;
           this.buttonSmallBlind.Visible = true;
           this.textboxBigBlind.Visible = false;
           this.textboxSmallBlind.Visible = false;
           this.buttonBigBlind.Visible = false;
           this.buttonSmallBlind.Visible = false;
           this.textboxRaise.Text = (bigBlind*2).ToString();
        }

        private async Task Shuffle()
        {
           this.playersInGameStatuses.Add(this.playerFoldedTurn);
           this.playersInGameStatuses.Add(this.firstBotFoldedTurn);
           this.playersInGameStatuses.Add(this.secodBotFoldedTurn);
           this.playersInGameStatuses.Add(this.thirdBotFoldedTurn);
           this.playersInGameStatuses.Add(this.fourthBotFoldedTurn);
           this.playersInGameStatuses.Add(this.fifthBotFoldedTurn);

           this.buttonCall.Enabled = false;
           this.buttonRaise.Enabled = false;
           this.buttonFold.Enabled = false;
           this.buttonCheck.Enabled = false;

           this.MaximizeBox = false;
           this.MinimizeBox = false;

            bool check = false;

            var backImage = new Bitmap("Resources\\Back\\Back.png");

            int horizontal = 580;
            int vertical = 480;

            Random random = new Random();

            for (i = cardImagePaths.Length; i > 0; i--)
            {
                var position = random.Next(i);

                string cardImagePath = cardImagePaths[position];

                cardImagePaths[position] = cardImagePaths[i - 1];
                cardImagePaths[i - 1] = cardImagePath;
            }

            for (i = 0; i < 17; i++)
            {
                deckImageArr[i] = Image.FromFile(cardImagePaths[i]);

                var charsToRemove = new string[] { "Resources\\Cards\\", ".png" };

                foreach (var c in charsToRemove)
                {
                    cardImagePaths[i] = cardImagePaths[i].Replace(c, string.Empty);
                }

                all17CardsInPlay[i] = int.Parse(cardImagePaths[i]) - 1;

                deckPictureBoxArr[i] = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Height = 130,
                    Width = 80
                };

                Controls.Add(deckPictureBoxArr[i]);
                deckPictureBoxArr[i].Name = "pb" + i;

                await Task.Delay(200);

                #region Throwing Cards

                if (i < 2)
                {
                    if (deckPictureBoxArr[0].Tag != null)
                    {
                        deckPictureBoxArr[1].Tag = all17CardsInPlay[1];
                    }
                    deckPictureBoxArr[0].Tag = all17CardsInPlay[0];
                    deckPictureBoxArr[i].Image = deckImageArr[i];
                    deckPictureBoxArr[i].Anchor = AnchorStyles.Bottom;
                   ////deckPictureBoxArr[i].Dock = DockStyle.Top;
                    deckPictureBoxArr[i].Location = new Point(horizontal, vertical);

                    horizontal += deckPictureBoxArr[i].Width;

                    Controls.Add(playerPanel);

                    playerPanel.Location = new Point(deckPictureBoxArr[0].Left - 10, deckPictureBoxArr[0].Top - 10);
                    playerPanel.BackColor = Color.DarkBlue;
                    playerPanel.Height = 150;
                    playerPanel.Width = 180;
                    playerPanel.Visible = false;
                }

                if (firstBotChips > 0)
                {
                    foldedPlayers--;

                    if (i >= 2 && i < 4)
                    {
                        if (deckPictureBoxArr[2].Tag != null)
                        {
                            deckPictureBoxArr[3].Tag = all17CardsInPlay[3];
                        }

                        deckPictureBoxArr[2].Tag = all17CardsInPlay[2];

                        if (!check)
                        {
                            horizontal = 15;
                            vertical = 420;
                        }

                        check = true;

                        deckPictureBoxArr[i].Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                        deckPictureBoxArr[i].Image = backImage;
                       ////deckPictureBoxArr[i].Image = deckImageArr[i];
                        deckPictureBoxArr[i].Location = new Point(horizontal, vertical);

                        horizontal += deckPictureBoxArr[i].Width;

                        deckPictureBoxArr[i].Visible = true;

                        Controls.Add(firstBotPanel);

                        firstBotPanel.Location = new Point(deckPictureBoxArr[2].Left - 10, deckPictureBoxArr[2].Top - 10);
                        firstBotPanel.BackColor = Color.DarkBlue;
                        firstBotPanel.Height = 150;
                        firstBotPanel.Width = 180;
                        firstBotPanel.Visible = false;

                        if (i == 3)
                        {
                            check = false;
                        }
                    }
                }

                if (secondBotChips > 0)
                {
                    foldedPlayers--;

                    if (i >= 4 && i < 6)
                    {
                        if (deckPictureBoxArr[4].Tag != null)
                        {
                            deckPictureBoxArr[5].Tag = all17CardsInPlay[5];
                        }

                        deckPictureBoxArr[4].Tag = all17CardsInPlay[4];

                        if (!check)
                        {
                            horizontal = 75;
                            vertical = 65;
                        }

                        check = true;

                        deckPictureBoxArr[i].Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        deckPictureBoxArr[i].Image = backImage;
                       ////deckPictureBoxArr[i].Image = deckImageArr[i];
                        deckPictureBoxArr[i].Location = new Point(horizontal, vertical);

                        horizontal += deckPictureBoxArr[i].Width;

                        deckPictureBoxArr[i].Visible = true;

                        Controls.Add(secondBotPanel);

                        secondBotPanel.Location = new Point(deckPictureBoxArr[4].Left - 10, deckPictureBoxArr[4].Top - 10);
                        secondBotPanel.BackColor = Color.DarkBlue;
                        secondBotPanel.Height = 150;
                        secondBotPanel.Width = 180;
                        secondBotPanel.Visible = false;

                        if (i == 5)
                        {
                            check = false;
                        }
                    }
                }

                if (thirdBotChips > 0)
                {
                    foldedPlayers--;

                    if (i >= 6 && i < 8)
                    {
                        if (deckPictureBoxArr[6].Tag != null)
                        {
                            deckPictureBoxArr[7].Tag = all17CardsInPlay[7];
                        }

                        deckPictureBoxArr[6].Tag = all17CardsInPlay[6];

                        if (!check)
                        {
                            horizontal = 590;
                            vertical = 25;
                        }

                        check = true;

                        deckPictureBoxArr[i].Anchor = AnchorStyles.Top;
                        deckPictureBoxArr[i].Image = backImage;
                       ////deckPictureBoxArr[i].Image = deckImageArr[i];
                        deckPictureBoxArr[i].Location = new Point(horizontal, vertical);

                        horizontal += deckPictureBoxArr[i].Width;

                        deckPictureBoxArr[i].Visible = true;

                        Controls.Add(thirdBotPanel);

                        thirdBotPanel.Location = new Point(deckPictureBoxArr[6].Left - 10, deckPictureBoxArr[6].Top - 10);
                        thirdBotPanel.BackColor = Color.DarkBlue;
                        thirdBotPanel.Height = 150;
                        thirdBotPanel.Width = 180;
                        thirdBotPanel.Visible = false;

                        if (i == 7)
                        {
                            check = false;
                        }
                    }
                }

                if (fourthBotChips > 0)
                {
                    foldedPlayers--;

                    if (i >= 8 && i < 10)
                    {
                        if (deckPictureBoxArr[8].Tag != null)
                        {
                            deckPictureBoxArr[9].Tag = all17CardsInPlay[9];
                        }

                        deckPictureBoxArr[8].Tag = all17CardsInPlay[8];
                        if (!check)
                        {
                            horizontal = 1115;
                            vertical = 65;
                        }

                        check = true;

                        deckPictureBoxArr[i].Anchor = AnchorStyles.Top | AnchorStyles.Right;
                        deckPictureBoxArr[i].Image = backImage;
                        ////deckPictureBoxArr[i].Image = deckImageArr[i];
                        deckPictureBoxArr[i].Location = new Point(horizontal, vertical);

                        horizontal += deckPictureBoxArr[i].Width;

                        deckPictureBoxArr[i].Visible = true;

                        Controls.Add(fourthBotPanel);

                        fourthBotPanel.Location = new Point(deckPictureBoxArr[8].Left - 10, deckPictureBoxArr[8].Top - 10);
                        fourthBotPanel.BackColor = Color.DarkBlue;
                        fourthBotPanel.Height = 150;
                        fourthBotPanel.Width = 180;
                        fourthBotPanel.Visible = false;

                        if (i == 9)
                        {
                            check = false;
                        }
                    }
                }

                if (fifthBotChips > 0)
                {
                    foldedPlayers--;

                    if (i >= 10 && i < 12)
                    {
                        if (deckPictureBoxArr[10].Tag != null)
                        {
                            deckPictureBoxArr[11].Tag = all17CardsInPlay[11];
                        }

                        deckPictureBoxArr[10].Tag = all17CardsInPlay[10];

                        if (!check)
                        {
                            horizontal = 1160;
                            vertical = 420;
                        }

                        check = true;

                        deckPictureBoxArr[i].Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                        deckPictureBoxArr[i].Image = backImage;
                        ////deckPictureBoxArr[i].Image = deckImageArr[i];
                        deckPictureBoxArr[i].Location = new Point(horizontal, vertical);

                        horizontal += deckPictureBoxArr[i].Width;

                        deckPictureBoxArr[i].Visible = true;

                        Controls.Add(fifthBotPanel);

                        fifthBotPanel.Location = new Point(deckPictureBoxArr[10].Left - 10, deckPictureBoxArr[10].Top - 10);
                        fifthBotPanel.BackColor = Color.DarkBlue;
                        fifthBotPanel.Height = 150;
                        fifthBotPanel.Width = 180;
                        fifthBotPanel.Visible = false;

                        if (i == 11)
                        {
                            check = false;
                        }
                    }
                }

                if (i >= 12)
                {
                    deckPictureBoxArr[12].Tag = all17CardsInPlay[12];

                    if (i > 12)
                    {
                        deckPictureBoxArr[13].Tag = all17CardsInPlay[13];
                    }

                    if (i > 13)
                    {
                        deckPictureBoxArr[14].Tag = all17CardsInPlay[14];
                    }

                    if (i > 14)
                    {
                        deckPictureBoxArr[15].Tag = all17CardsInPlay[15];
                    }

                    if (i > 15)
                    {
                        deckPictureBoxArr[16].Tag = all17CardsInPlay[16];
                    }

                    if (!check)
                    {
                        horizontal = 410;
                        vertical = 265;
                    }

                    check = true;

                    if (deckPictureBoxArr[i] != null)
                    {
                        deckPictureBoxArr[i].Anchor = AnchorStyles.None;
                        deckPictureBoxArr[i].Image = backImage;
                        ////deckPictureBoxArr[i].Image = deckImageArr[i];

                        deckPictureBoxArr[i].Location = new Point(horizontal, vertical);

                        horizontal += 110;
                    }
                }

                #endregion

                if (firstBotChips <= 0)
                {
                    firstBotFoldedTurn = true;

                    deckPictureBoxArr[2].Visible = false;
                    deckPictureBoxArr[3].Visible = false;
                }
                else
                {
                    firstBotFoldedTurn = false;

                    if (i == 3)
                    {
                        if (deckPictureBoxArr[3] != null)
                        {
                            deckPictureBoxArr[2].Visible = true;
                            deckPictureBoxArr[3].Visible = true;
                        }
                    }
                }

                if (secondBotChips <= 0)
                {
                    secodBotFoldedTurn = true;

                    deckPictureBoxArr[4].Visible = false;
                    deckPictureBoxArr[5].Visible = false;
                }
                else
                {
                    secodBotFoldedTurn = false;

                    if (i == 5)
                    {
                        if (deckPictureBoxArr[5] != null)
                        {
                            deckPictureBoxArr[4].Visible = true;
                            deckPictureBoxArr[5].Visible = true;
                        }
                    }
                }

                if (thirdBotChips <= 0)
                {
                    thirdBotFoldedTurn = true;

                    deckPictureBoxArr[6].Visible = false;
                    deckPictureBoxArr[7].Visible = false;
                }
                else
                {
                    thirdBotFoldedTurn = false;

                    if (i == 7)
                    {
                        if (deckPictureBoxArr[7] != null)
                        {
                            deckPictureBoxArr[6].Visible = true;
                            deckPictureBoxArr[7].Visible = true;
                        }
                    }
                }

                if (fourthBotChips <= 0)
                {
                    fourthBotFoldedTurn = true;

                    deckPictureBoxArr[8].Visible = false;
                    deckPictureBoxArr[9].Visible = false;
                }
                else
                {
                    fourthBotFoldedTurn = false;

                    if (i == 9)
                    {
                        if (deckPictureBoxArr[9] != null)
                        {
                            deckPictureBoxArr[8].Visible = true;
                            deckPictureBoxArr[9].Visible = true;
                        }
                    }
                }

                if (fifthBotChips <= 0)
                {
                    fifthBotFoldedTurn = true;

                    deckPictureBoxArr[10].Visible = false;
                    deckPictureBoxArr[11].Visible = false;
                }
                else
                {
                    fifthBotFoldedTurn = false;

                    if (i == 11)
                    {
                        if (deckPictureBoxArr[11] != null)
                        {
                            deckPictureBoxArr[10].Visible = true;
                            deckPictureBoxArr[11].Visible = true;
                        }
                    }
                }

                if (i != 16)
                {
                    continue;
                }

                if (!restart)
                {
                    MaximizeBox = true;
                    MinimizeBox = true;
                }

                timer.Start();
            }

            if (foldedPlayers == 5)
            {
                var dialogResult = MessageBox.Show(
                    @"Would You Like To Play Again ?",
                    @"You Won , Congratulations ! ",
                    MessageBoxButtons.YesNo);
                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        Application.Restart();
                        break;
                    case DialogResult.No:
                        Application.Exit();
                        break;
                    case DialogResult.None:
                        break;
                    case DialogResult.OK:
                        break;
                    case DialogResult.Cancel:
                        break;
                    case DialogResult.Abort:
                        break;
                    case DialogResult.Retry:
                        Application.Restart();
                        break;
                    case DialogResult.Ignore:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                foldedPlayers = 5;
            }

            if (i == 17)
            {
                buttonRaise.Enabled = true;
                buttonCall.Enabled = true;
                buttonRaise.Enabled = true;
                buttonRaise.Enabled = true;
                buttonFold.Enabled = true;
            }
        }

        private async Task Turns()
        {
            #region Rotating

            if (!playerFoldedTurn)
            {
                if (playerTurn)
                {
                    FixCall(playerStatus, ref playerCallAmount, ref playerRaiseAmount, 1);

                    MessageBox.Show("Player's Turn");

                    progressBarTimer.Visible = true;
                    progressBarTimer.Value = 1000;

                    timeTillNextTurn = 60;
                    up = 10000000;
                    timer.Start();

                    buttonRaise.Enabled = true;
                    buttonCall.Enabled = true;
                    buttonRaise.Enabled = true;
                    buttonRaise.Enabled = true;
                    buttonFold.Enabled = true;

                    turnCount++;

                    FixCall(playerStatus, ref playerCallAmount, ref playerRaiseAmount, 2);
                }
            }

            if (playerFoldedTurn || !playerTurn)
            {
                await AllIn();

                if (playerFoldedTurn && !playerFolded)
                {
                    if (buttonCall.Text.Contains("All in") == false 
                        || buttonRaise.Text.Contains("All in") == false)
                    {
                        playersInGameStatuses.RemoveAt(0);
                        playersInGameStatuses.Insert(0, null);

                        playersLeftToAct--;

                        playerFolded = true;
                    }
                }

                await CheckRaise(0, 0);

                progressBarTimer.Visible = false;

                buttonRaise.Enabled = false;
                buttonCall.Enabled = false;
                buttonRaise.Enabled = false;
                buttonRaise.Enabled = false;
                buttonFold.Enabled = false;

                timer.Stop();

                firstBotTurn = true;

                if (!firstBotFoldedTurn)
                {
                    if (firstBotTurn)
                    {
                        FixCall(firstBotStatus, ref firstBotCallAmount, ref firstBotRaiseAmount, 1);
                        FixCall(firstBotStatus, ref firstBotCallAmount, ref firstBotRaiseAmount, 2);

                        Rules(2, 3, "Bot 1", ref firstBotType, ref firstBotPower, firstBotFoldedTurn);                     

                        AI(2, 3, ref firstBotChips, ref firstBotTurn, ref firstBotFoldedTurn, firstBotStatus, 0, firstBotPower, firstBotType);

                        turnCount++;

                        lastOneToAct = 1;

                        firstBotTurn = false;
                        secondBotTurn = true;
                    }
                }

                if (firstBotFoldedTurn && !firstBotFolded)
                {
                    playersInGameStatuses.RemoveAt(1);
                    playersInGameStatuses.Insert(1, null);

                    playersLeftToAct--;

                    firstBotFolded = true;
                }

                if (firstBotFoldedTurn || !firstBotTurn)
                {
                    await CheckRaise(1, 1);

                    secondBotTurn = true;
                }

                if (!secodBotFoldedTurn)
                {
                    if (secondBotTurn)
                    {
                        FixCall(secondBotStatus, ref secondBotCallAmount, ref secondBotRaiseAmount, 1);
                        FixCall(secondBotStatus, ref secondBotCallAmount, ref secondBotRaiseAmount, 2);

                        Rules(4, 5, "Bot 2", ref secondBotType, ref secondBotPower, secodBotFoldedTurn);                    

                        AI(4, 5, ref secondBotChips, ref secondBotTurn, ref secodBotFoldedTurn, secondBotStatus, 1,secondBotPower, secondBotType);

                        turnCount++;
                        lastOneToAct = 2;
                        secondBotTurn = false;
                        thirdBotTurn = true;
                    }
                }

                if (secodBotFoldedTurn && !secondBotFolded)
                {
                    playersInGameStatuses.RemoveAt(2);
                    playersInGameStatuses.Insert(2, null);
                    playersLeftToAct--;
                    secondBotFolded = true;
                }

                if (secodBotFoldedTurn || !secondBotTurn)
                {
                    await CheckRaise(2, 2);
                    thirdBotTurn = true;
                }

                if (!thirdBotFoldedTurn)
                {
                    if (thirdBotTurn)
                    {
                        FixCall(thirdBotStatus, ref thirdBotCallAmount, ref thirdBotRaiseAmount, 1);

                        FixCall(thirdBotStatus, ref thirdBotCallAmount, ref thirdBotRaiseAmount, 2);

                        Rules(6, 7, "Bot 3", ref thirdBotType, ref thirdBotPower, thirdBotFoldedTurn);

                        AI(6, 7, ref thirdBotChips, ref thirdBotTurn, ref thirdBotFoldedTurn, thirdBotStatus, 2, thirdBotPower, thirdBotType);

                        turnCount++;
                        lastOneToAct = 3;
                        thirdBotTurn = false;
                        fourthBotTurn = true;
                    }
                }

                if (thirdBotFoldedTurn && !thirdBotFolded)
                {
                    playersInGameStatuses.RemoveAt(3);
                    playersInGameStatuses.Insert(3, null);
                    playersLeftToAct--;
                    thirdBotFolded = true;
                }

                if (thirdBotFoldedTurn || !thirdBotTurn)
                {
                    await CheckRaise(3, 3);
                    fourthBotTurn = true;
                }

                if (!fourthBotFoldedTurn)
                {
                    if (fourthBotTurn)
                    {
                        FixCall( fourthBotStatus, ref fourthBotCallAmount, ref fourthBotRaiseAmount, 1);
                        FixCall(fourthBotStatus, ref fourthBotCallAmount, ref fourthBotRaiseAmount, 2);

                        Rules(8, 9, "Bot 4", ref fourthBotType, ref fourthBotPower, fourthBotFoldedTurn);

                        AI(8, 9, ref fourthBotChips, ref fourthBotTurn, ref fourthBotFoldedTurn, fourthBotStatus, 3, fourthBotPower, fourthBotType);

                        turnCount++;
                        lastOneToAct = 4;
                        fourthBotTurn = false;
                        fifthBotTurn = true;
                    }
                }

                if (fourthBotFoldedTurn && !fourthBotFolded)
                {
                    playersInGameStatuses.RemoveAt(4);
                    playersInGameStatuses.Insert(4, null);

                    playersLeftToAct--;

                    fourthBotFolded = true;
                }

                if (fourthBotFoldedTurn || !fourthBotTurn)
                {
                    await CheckRaise(4, 4);

                    fifthBotTurn = true;
                }

                if (!fifthBotFoldedTurn)
                {
                    if (fifthBotTurn)
                    {
                        FixCall(fifthBotStatus, ref fifthBotCallAmount, ref fifthBotRaiseAmount, 1);
                        FixCall(fifthBotStatus, ref fifthBotCallAmount, ref fifthBotRaiseAmount, 2);

                        Rules(10, 11, "Bot 5", ref fifthBotType, ref fifthBotPower, fifthBotFoldedTurn);

                        AI(10, 11, ref fifthBotChips, ref fifthBotTurn, ref fifthBotFoldedTurn, fifthBotStatus, 4, fifthBotPower, fifthBotType);

                        turnCount++;
                        lastOneToAct = 5;
                        fifthBotTurn = false;
                    }
                }

                if (fifthBotFoldedTurn && !fifthBotFolded)
                {
                    playersInGameStatuses.RemoveAt(5);
                    playersInGameStatuses.Insert(5, null);

                    playersLeftToAct--;

                    fifthBotFolded = true;
                }

                if (fifthBotFoldedTurn || !fifthBotTurn)
                {
                    await CheckRaise(5, 5);

                    playerTurn = true;
                }

                if (playerFoldedTurn && !playerFolded)
                {
                    if (buttonCall.Text.Contains("All in") == false 
                        || buttonRaise.Text.Contains("All in") == false)
                    {
                        playersInGameStatuses.RemoveAt(0);
                        playersInGameStatuses.Insert(0, null);

                        playersLeftToAct--;

                        playerFolded = true;
                    }
                }

                #endregion

                await AllIn();

                if (!restart)
                {
                    await Turns();
                }

                restart = false;
            }
        }

        private void Rules(int c1, int c2, string currentBot, ref double handStrength, ref double currentBotPower, bool foldedTurn)
        {
            if (!foldedTurn || c1 == 0 && c2 == 1 && playerStatus.Text.Contains("Fold") == false)
            {
                #region Variables

                var done = false;

                var vf = false;

                var cardsOnTable = new int[5];
                var playerCards = new int[7];

                playerCards[0] = all17CardsInPlay[c1];
                playerCards[1] = all17CardsInPlay[c2];

                cardsOnTable[0] = playerCards[2] = all17CardsInPlay[12];
                cardsOnTable[1] = playerCards[3] = all17CardsInPlay[13];
                cardsOnTable[2] = playerCards[4] = all17CardsInPlay[14];
                cardsOnTable[3] = playerCards[5] = all17CardsInPlay[15];
                cardsOnTable[4] = playerCards[6] = all17CardsInPlay[16];

                var clubs = playerCards.Where(o => o % 4 == 0).ToArray();
                var diamonds = playerCards.Where(o => o % 4 == 1).ToArray();
                var hearts = playerCards.Where(o => o % 4 == 2).ToArray();
                var spades = playerCards.Where(o => o % 4 == 3).ToArray();

                var clubCards = clubs.Select(o => o / 4).Distinct().ToArray();
                var diamondCards = diamonds.Select(o => o / 4).Distinct().ToArray();
                var heartCards = hearts.Select(o => o / 4).Distinct().ToArray();
                var spadeCards = spades.Select(o => o / 4).Distinct().ToArray();

                Array.Sort(playerCards);
                Array.Sort(clubCards);
                Array.Sort(diamondCards);
                Array.Sort(heartCards);
                Array.Sort(spadeCards);

                #endregion

                for (i = 0; i < 16; i++)
                {
                    if (all17CardsInPlay[i] == int.Parse(deckPictureBoxArr[c1].Tag.ToString()) 
                        && all17CardsInPlay[i + 1] == int.Parse(deckPictureBoxArr[c2].Tag.ToString()))
                    {
                        //Pair from Hand handStrength = 1

                        rPairFromHand(ref handStrength, ref currentBotPower);

                        #region Pair or Two Pair from Table handStrength = 2 || 0

                        RulePairTwoPair(ref handStrength, ref currentBotPower);

                        #endregion

                        #region Two Pair handStrength = 2

                        RuleTwoPair(ref handStrength, ref currentBotPower);

                        #endregion

                        #region Three of a kind handStrength = 3

                        RuleThreeOfAKind(ref handStrength, ref currentBotPower, playerCards);

                        #endregion

                        #region Straight handStrength = 4

                        RuleStraight(ref handStrength, ref currentBotPower, playerCards);

                        #endregion

                        #region Flush handStrength = 5 || 5.5

                        RuleFlush(ref handStrength, ref currentBotPower, ref vf, cardsOnTable);

                        #endregion

                        #region Full House handStrength = 6

                        RuleFullHouse(ref handStrength, ref currentBotPower, ref done, playerCards);

                        #endregion

                        #region Four of a Kind handStrength = 7

                        RuleFourOfAKind(ref handStrength, ref currentBotPower, playerCards);

                        #endregion

                        #region Straight Flush handStrength = 8 || 9

                        RuleStraightFlush(ref handStrength, ref currentBotPower, clubCards, diamondCards, heartCards, spadeCards);

                        #endregion

                        #region High Card handStrength = -1

                        rHighCard(ref handStrength, ref currentBotPower);

                        #endregion
                    }
                }
            }
        }

        private void RuleStraightFlush(
            ref double handStrength,
            ref double handStrengthScore,
            IReadOnlyList<int> clubCards,
            IReadOnlyList<int> diamondCards,
            IReadOnlyList<int> heartCards,
            IReadOnlyList<int> spadeCards)
        {
            if (handStrength >= -1)
            {
                if (clubCards.Count >= 5)
                {
                    if (clubCards[0] + 4 == clubCards[4])
                    {
                        handStrength = 8;

                        var max = clubCards.Concat(new[] {int.MinValue}).Max();

                        handStrengthScore = max / 4 + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 8
                        });

                        sorted = 
                            winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();
                    }

                    if (clubCards[0] == 0 
                        && clubCards[1] == 9 
                        && clubCards[2] == 10 
                        && clubCards[3] == 11 
                        && clubCards[0] + 12 == clubCards[4])
                    {
                        handStrength = 9;

                        var max = clubCards.Concat(new[] {int.MinValue}).Max();

                        handStrengthScore = max / 4 + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 9
                        });

                        sorted = winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();
                    }
                }

                if (diamondCards.Count >= 5)
                {
                    if (diamondCards[0] + 4 == diamondCards[4])
                    {
                        handStrength = 8;

                        var max = diamondCards.Concat(new[] {int.MinValue}).Max();

                        handStrengthScore = max / 4 + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 8
                        });

                        sorted = winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();
                    }

                    if (diamondCards[0] == 0 
                        && diamondCards[1] == 9 
                        && diamondCards[2] == 10 
                        && diamondCards[3] == 11 
                        && diamondCards[0] + 12 == diamondCards[4])
                    {
                        handStrength = 9;

                        var max = diamondCards.Concat(new[] {int.MinValue}).Max();

                        handStrengthScore = max / 4 + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 9
                        });

                        sorted = winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();
                    }
                }

                if (heartCards.Count >= 5)
                {
                    if (heartCards[0] + 4 == heartCards[4])
                    {
                        handStrength = 8;

                        var max = heartCards.Concat(new[] {int.MinValue}).Max();

                        handStrengthScore = max / 4 + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 8
                        });

                        sorted = winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();
                    }

                    if (heartCards[0] == 0 
                        && heartCards[1] == 9 
                        && heartCards[2] == 10 
                        && heartCards[3] == 11 
                        && heartCards[0] + 12 == heartCards[4])
                    {
                        handStrength = 9;

                        var max = heartCards.Concat(new[] {int.MinValue}).Max();

                        handStrengthScore = max / 4 + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 9
                        });

                        sorted = winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();
                    }
                }

                if (spadeCards.Count >= 5)
                {
                    if (spadeCards[0] + 4 == spadeCards[4])
                    {
                        handStrength = 8;

                        var max = spadeCards.Concat(new[] {int.MinValue}).Max();

                        handStrengthScore = max / 4 + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 8
                        });

                        sorted = winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();
                    }

                    if (spadeCards[0] == 0 
                        && spadeCards[1] == 9 
                        && spadeCards[2] == 10 
                        && spadeCards[3] == 11 
                        && spadeCards[0] + 12 == spadeCards[4])
                    {
                        handStrength = 9;

                        var max = spadeCards.Concat(new[] {int.MinValue}).Max();

                        handStrengthScore = max / 4 + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 9
                        });

                        sorted = winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();
                    }
                }
            }
        }

        private void RuleFourOfAKind(
            ref double handStrength,
            ref double handStrengthScore,
            IReadOnlyList<int> playerCards)
        {
            if (handStrength >= -1)
            {
                for (var j = 0; j <= 3; j++)
                {
                    if (playerCards[j] / 4 == playerCards[j + 1] / 4 
                        && playerCards[j] / 4 == playerCards[j + 2] / 4 
                        && playerCards[j] / 4 == playerCards[j + 3] / 4)
                    {
                        handStrength = 7;

                        handStrengthScore = (playerCards[j] / 4) * 4 + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 7
                        });

                        sorted = winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();
                    }

                    if (playerCards[j] / 4 == 0 
                        && playerCards[j + 1] / 4 == 0 
                        && playerCards[j + 2] / 4 == 0 
                        && playerCards[j + 3] / 4 == 0)
                    {
                        handStrength = 7;

                        handStrengthScore = 13 * 4 + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 7
                        });

                        sorted = winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();
                    }
                }
            }
        }

        private void RuleFullHouse(
            ref double handStrength,
            ref double handStrengthScore,
            ref bool done,
            int[] playerCards)
        {
            if (handStrength >= -1)
            {
                type = handStrengthScore;

                for (var j = 0; j <= 12; j++)
                {
                    var fullHouse = playerCards.Where(o => o / 4 == j).ToArray();

                    if (fullHouse.Length == 3 || done)
                    {
                        if (fullHouse.Length == 2)
                        {
                            if (fullHouse.Max() / 4 == 0)
                            {
                                handStrength = 6;

                                handStrengthScore = 13 * 2 + handStrength * 100;

                                winningHands.Add(new Hand()
                                {
                                    HandStrengthScore = handStrengthScore,
                                    HandStrength = 6
                                });

                                sorted = winningHands
                                    .OrderByDescending(op1 => op1.HandStrength)
                                    .ThenByDescending(op1 => op1.HandStrengthScore)
                                    .First();

                                break;
                            }

                            if (fullHouse.Max() / 4 > 0)
                            {
                                handStrength = 6;

                                handStrengthScore = fullHouse.Max() / 4 * 2 + handStrength * 100;

                                winningHands.Add(new Hand()
                                {
                                    HandStrengthScore = handStrengthScore,
                                    HandStrength = 6
                                });

                                sorted = winningHands
                                    .OrderByDescending(op1 => op1.HandStrength)
                                    .ThenByDescending(op1 => op1.HandStrengthScore)
                                    .First();

                                break;
                            }
                        }

                        if (!done)
                        {
                            if (fullHouse.Max() / 4 == 0)
                            {
                                handStrengthScore = 13;

                                done = true;

                                j = -1;
                            }
                            else
                            {
                                handStrengthScore = fullHouse.Max()/4;

                                done = true;

                                j = -1;
                            }
                        }
                    }
                }

                if (handStrength != 6)
                {
                    handStrengthScore = type;
                }
            }
        }

        private void RuleFlush(
            ref double handStrength,
            ref double handStrengthScore,
            ref bool vf,
            int[] straight1)
        {
            if (handStrength >= -1)
            {
                var flushOfClubs = straight1.Where(o => o % 4 == 0).ToArray();
                var flushOfDiamonds = straight1.Where(o => o % 4 == 1).ToArray();
                var flushOfHearts = straight1.Where(o => o % 4 == 2).ToArray();
                var flushOfSpades = straight1.Where(o => o % 4 == 3).ToArray();

                if (flushOfClubs.Length == 3 || flushOfClubs.Length == 4)
                {
                    if (all17CardsInPlay[i] % 4 == all17CardsInPlay[i + 1] % 4 
                        && all17CardsInPlay[i] % 4 == flushOfClubs[0] % 4)
                    {
                        if (all17CardsInPlay[i] / 4 > flushOfClubs.Max() / 4)
                        {
                            handStrength = 5;

                            handStrengthScore = all17CardsInPlay[i] + handStrength*100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }

                        if (all17CardsInPlay[i + 1] / 4 > flushOfClubs.Max() / 4)
                        {
                            handStrength = 5;

                            handStrengthScore = all17CardsInPlay[i + 1] + handStrength*100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                        else if (all17CardsInPlay[i] / 4 < flushOfClubs.Max() / 4 
                            && all17CardsInPlay[i + 1] / 4 < flushOfClubs.Max() / 4)
                        {
                            handStrength = 5;

                            handStrengthScore = flushOfClubs.Max() + handStrength*100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                    }
                }
                //different cards in hand
                if (flushOfClubs.Length == 4) 
                {
                    if (all17CardsInPlay[i] % 4 != all17CardsInPlay[i + 1] % 4 
                        && all17CardsInPlay[i] % 4 == flushOfClubs[0] % 4)
                    {
                        if (all17CardsInPlay[i] / 4 > flushOfClubs.Max() / 4)
                        {
                            handStrength = 5;

                            handStrengthScore = all17CardsInPlay[i] + handStrength*100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            handStrength = 5;

                            handStrengthScore = flushOfClubs.Max() + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                    }

                    if (all17CardsInPlay[i + 1] % 4 != all17CardsInPlay[i] % 4 
                        && all17CardsInPlay[i + 1] % 4 == flushOfClubs[0] % 4)
                    {
                        if (all17CardsInPlay[i + 1] / 4 > flushOfClubs.Max() / 4)
                        {
                            handStrength = 5;

                            handStrengthScore = all17CardsInPlay[i + 1] + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            handStrength = 5;

                            handStrengthScore = flushOfClubs.Max() + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                    }
                }

                if (flushOfClubs.Length == 5)
                {
                    if (all17CardsInPlay[i] % 4 == flushOfClubs[0] % 4 
                        && all17CardsInPlay[i] / 4 > flushOfClubs.Min() / 4)
                    {
                        handStrength = 5;

                        handStrengthScore = all17CardsInPlay[i] + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 5
                        });

                        sorted = winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();

                        vf = true;
                    }

                    if (all17CardsInPlay[i + 1] % 4 == flushOfClubs[0] % 4 
                        && all17CardsInPlay[i + 1] / 4 > flushOfClubs.Min() / 4)
                    {
                        handStrength = 5;

                        handStrengthScore = all17CardsInPlay[i + 1] + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore, 
                            HandStrength = 5
                        });

                        sorted = winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();

                        vf = true;
                    }
                    else if (all17CardsInPlay[i] / 4 < flushOfClubs.Min() / 4 
                        && all17CardsInPlay[i + 1] / 4 < flushOfClubs.Min())
                    {
                        handStrength = 5;

                        handStrengthScore = flushOfClubs.Max() + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 5
                        });

                        sorted = winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();

                        vf = true;
                    }
                }

                if (flushOfDiamonds.Length == 3 || flushOfDiamonds.Length == 4)
                {
                    if (all17CardsInPlay[i] % 4 == all17CardsInPlay[i + 1] % 4 
                        && all17CardsInPlay[i] % 4 == flushOfDiamonds[0] % 4)
                    {
                        if (all17CardsInPlay[i] / 4 > flushOfDiamonds.Max() / 4)
                        {
                            handStrength = 5;

                            handStrengthScore = all17CardsInPlay[i] + handStrength*100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }

                        if (all17CardsInPlay[i + 1] / 4 > flushOfDiamonds.Max() / 4)
                        {
                            handStrength = 5;

                            handStrengthScore = all17CardsInPlay[i + 1] + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                        else if (all17CardsInPlay[i] / 4 < flushOfDiamonds.Max() / 4 
                            && all17CardsInPlay[i + 1] / 4 < flushOfDiamonds.Max() / 4)
                        {
                            handStrength = 5;

                            handStrengthScore = flushOfDiamonds.Max() + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                    }
                }
                //different cards in hand
                if (flushOfDiamonds.Length == 4) 
                {
                    if (all17CardsInPlay[i] % 4 != all17CardsInPlay[i + 1] % 4 
                        && all17CardsInPlay[i] % 4 == flushOfDiamonds[0] % 4)
                    {
                        if (all17CardsInPlay[i] / 4 > flushOfDiamonds.Max() / 4)
                        {
                            handStrength = 5;

                            handStrengthScore = all17CardsInPlay[i] + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            handStrength = 5;

                            handStrengthScore = flushOfDiamonds.Max() + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                    }

                    if (all17CardsInPlay[i + 1] % 4 != all17CardsInPlay[i] % 4 
                        && all17CardsInPlay[i + 1] % 4 == flushOfDiamonds[0] % 4)
                    {
                        if (all17CardsInPlay[i + 1] / 4 > flushOfDiamonds.Max() / 4)
                        {
                            handStrength = 5;

                            handStrengthScore = all17CardsInPlay[i + 1] + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            handStrength = 5;

                            handStrengthScore = flushOfDiamonds.Max() + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                    }
                }

                if (flushOfDiamonds.Length == 5)
                {
                    if (all17CardsInPlay[i] % 4 == flushOfDiamonds[0] % 4 
                        && all17CardsInPlay[i] / 4 > flushOfDiamonds.Min() / 4)
                    {
                        handStrength = 5;

                        handStrengthScore = all17CardsInPlay[i] + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 5
                        });

                        sorted = winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();

                        vf = true;
                    }

                    if (all17CardsInPlay[i + 1] % 4 == flushOfDiamonds[0] % 4 
                        && all17CardsInPlay[i + 1] / 4 > flushOfDiamonds.Min() / 4)
                    {
                        handStrength = 5;

                        handStrengthScore = all17CardsInPlay[i + 1] + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 5
                        });

                        sorted = winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();

                        vf = true;
                    }
                    else if (all17CardsInPlay[i] / 4 < flushOfDiamonds.Min() / 4 
                        && all17CardsInPlay[i + 1] / 4 < flushOfDiamonds.Min())
                    {
                        handStrength = 5;

                        handStrengthScore = flushOfDiamonds.Max() + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 5
                        });

                        sorted = winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();

                        vf = true;
                    }
                }

                if (flushOfHearts.Length == 3 || flushOfHearts.Length == 4)
                {
                    if (all17CardsInPlay[i] % 4 == all17CardsInPlay[i + 1] % 4 
                        && all17CardsInPlay[i] % 4 == flushOfHearts[0] % 4)
                    {
                        if (all17CardsInPlay[i] / 4 > flushOfHearts.Max() / 4)
                        {
                            handStrength = 5;

                            handStrengthScore = all17CardsInPlay[i] + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }

                        if (all17CardsInPlay[i + 1] / 4 > flushOfHearts.Max() / 4)
                        {
                            handStrength = 5;

                            handStrengthScore = all17CardsInPlay[i + 1] + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                        else if (all17CardsInPlay[i] / 4 < flushOfHearts.Max() / 4 
                            && all17CardsInPlay[i + 1] / 4 < flushOfHearts.Max() / 4)
                        {
                            handStrength = 5;

                            handStrengthScore = flushOfHearts.Max() + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                    }
                }
                //different cards in hand
                if (flushOfHearts.Length == 4) 
                {
                    if (all17CardsInPlay[i] % 4 != all17CardsInPlay[i + 1] % 4 
                        && all17CardsInPlay[i] % 4 == flushOfHearts[0] % 4)
                    {
                        if (all17CardsInPlay[i] / 4 > flushOfHearts.Max() / 4)
                        {
                            handStrength = 5;

                            handStrengthScore = all17CardsInPlay[i] + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            handStrength = 5;

                            handStrengthScore = flushOfHearts.Max() + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = 
                                winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                    }

                    if (all17CardsInPlay[i + 1] % 4 != all17CardsInPlay[i] % 4 
                        && all17CardsInPlay[i + 1] % 4 == flushOfHearts[0] % 4)
                    {
                        if (all17CardsInPlay[i + 1] / 4 > flushOfHearts.Max() / 4)
                        {
                            handStrength = 5;

                            handStrengthScore = all17CardsInPlay[i + 1] + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            handStrength = 5;

                            handStrengthScore = flushOfHearts.Max() + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                    }
                }

                if (flushOfHearts.Length == 5)
                {
                    if (all17CardsInPlay[i] % 4 == flushOfHearts[0] % 4 
                        && all17CardsInPlay[i] / 4 > flushOfHearts.Min() / 4)
                    {
                        handStrength = 5;

                        handStrengthScore = all17CardsInPlay[i] + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 5
                        });

                        sorted = winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();

                        vf = true;
                    }

                    if (all17CardsInPlay[i + 1] % 4 == flushOfHearts[0] % 4 
                        && all17CardsInPlay[i + 1] / 4 > flushOfHearts.Min() / 4)
                    {
                        handStrength = 5;

                        handStrengthScore = all17CardsInPlay[i + 1] + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 5
                        });

                        sorted = winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();

                        vf = true;
                    }
                    else if (all17CardsInPlay[i] / 4 < flushOfHearts.Min() / 4 
                        && all17CardsInPlay[i + 1] / 4 < flushOfHearts.Min())
                    {
                        handStrength = 5;

                        handStrengthScore = flushOfHearts.Max() + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 5
                        });

                        sorted = winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();

                        vf = true;
                    }
                }

                if (flushOfSpades.Length == 3 || flushOfSpades.Length == 4)
                {
                    if (all17CardsInPlay[i] % 4 == all17CardsInPlay[i + 1] % 4 
                        && all17CardsInPlay[i] % 4 == flushOfSpades[0] % 4)
                    {
                        if (all17CardsInPlay[i] / 4 > flushOfSpades.Max() / 4)
                        {
                            handStrength = 5;

                            handStrengthScore = all17CardsInPlay[i] + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }

                        if (all17CardsInPlay[i + 1] / 4 > flushOfSpades.Max() / 4)
                        {
                            handStrength = 5;

                            handStrengthScore = all17CardsInPlay[i + 1] + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                        else if (all17CardsInPlay[i] / 4 < flushOfSpades.Max() / 4 
                            && all17CardsInPlay[i + 1] / 4 < flushOfSpades.Max() / 4)
                        {
                            handStrength = 5;

                            handStrengthScore = flushOfSpades.Max() + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                    }
                }
                //different cards in hand
                if (flushOfSpades.Length == 4) 
                {
                    if (all17CardsInPlay[i] % 4 != all17CardsInPlay[i + 1] % 4 
                        && all17CardsInPlay[i] % 4 == flushOfSpades[0] % 4)
                    {
                        if (all17CardsInPlay[i] / 4 > flushOfSpades.Max() / 4)
                        {
                            handStrength = 5;

                            handStrengthScore = all17CardsInPlay[i] + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            handStrength = 5;

                            handStrengthScore = flushOfSpades.Max() + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                    }

                    if (all17CardsInPlay[i + 1] % 4 != all17CardsInPlay[i] % 4 
                        && all17CardsInPlay[i + 1] % 4 == flushOfSpades[0] % 4)
                    {
                        if (all17CardsInPlay[i + 1] / 4 > flushOfSpades.Max() / 4)
                        {
                            handStrength = 5;

                            handStrengthScore = all17CardsInPlay[i + 1] + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = 
                                winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            handStrength = 5;

                            handStrengthScore = flushOfSpades.Max() + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 5
                            });

                            sorted = 
                                winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();

                            vf = true;
                        }
                    }
                }

                if (flushOfSpades.Length == 5)
                {
                    if (all17CardsInPlay[i] % 4 == flushOfSpades[0] % 4 
                        && all17CardsInPlay[i] / 4 > flushOfSpades.Min() / 4)
                    {
                        handStrength = 5;

                        handStrengthScore = all17CardsInPlay[i] + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 5
                        });

                        sorted = 
                            winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();

                        vf = true;
                    }

                    if (all17CardsInPlay[i + 1] % 4 == flushOfSpades[0] % 4 
                        && all17CardsInPlay[i + 1] / 4 > flushOfSpades.Min() / 4)
                    {
                        handStrength = 5;

                        handStrengthScore = all17CardsInPlay[i + 1] + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 5
                        });

                        sorted = 
                            winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();

                        vf = true;
                    }
                    else if (all17CardsInPlay[i] / 4 < flushOfSpades.Min() / 4 
                        && all17CardsInPlay[i + 1] / 4 < flushOfSpades.Min())
                    {
                        handStrength = 5;

                        handStrengthScore = flushOfSpades.Max() + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 5
                        });

                        sorted = 
                            winningHands.
                            OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();

                        vf = true;
                    }
                }
                if (flushOfClubs.Length > 0)
                {
                    if (all17CardsInPlay[i] / 4 == 0 
                        && all17CardsInPlay[i] % 4 == flushOfClubs[0] % 4 
                        && vf 
                        && flushOfClubs.Length > 0)
                    {
                        handStrength = 5.5;

                        handStrengthScore = 13 + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 5.5
                        });

                        sorted = 
                            winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();
                    }

                    if (all17CardsInPlay[i + 1] / 4 == 0 
                        && all17CardsInPlay[i + 1] % 4 == flushOfClubs[0] % 4 
                        && vf 
                        && flushOfClubs.Length > 0)
                    {
                        handStrength = 5.5;

                        handStrengthScore = 13 + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 5.5
                        });

                        sorted = 
                            winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();
                    }
                }

                if (flushOfDiamonds.Length > 0)
                {
                    if (all17CardsInPlay[i] / 4 == 0 
                        && all17CardsInPlay[i] % 4 == flushOfDiamonds[0] % 4 
                        && vf 
                        && flushOfDiamonds.Length > 0)
                    {
                        handStrength = 5.5;

                        handStrengthScore = 13 + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 5.5
                        });

                        sorted = 
                            winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();
                    }

                    if (all17CardsInPlay[i + 1] / 4 == 0 
                        && all17CardsInPlay[i + 1] % 4 == flushOfDiamonds[0] % 4 
                        && vf 
                        && flushOfDiamonds.Length > 0)
                    {
                        handStrength = 5.5;

                        handStrengthScore = 13 + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 5.5
                        });

                        sorted = 
                            winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();
                    }
                }

                if (flushOfHearts.Length > 0)
                {
                    if (all17CardsInPlay[i] / 4 == 0 
                        && all17CardsInPlay[i] % 4 == flushOfHearts[0] % 4 
                        && vf 
                        && flushOfHearts.Length > 0)
                    {
                        handStrength = 5.5;

                        handStrengthScore = 13 + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 5.5
                        });

                        sorted = 
                            winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();
                    }

                    if (all17CardsInPlay[i + 1] / 4 == 0 
                        && all17CardsInPlay[i + 1] % 4 == flushOfHearts[0] % 4 
                        && vf 
                        && flushOfHearts.Length > 0)
                    {
                        handStrength = 5.5;

                        handStrengthScore = 13 + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 5.5
                        });

                        sorted =
                            winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();
                    }
                }

                if (flushOfSpades.Length > 0)
                {
                    if (all17CardsInPlay[i] / 4 == 0 
                        && all17CardsInPlay[i] % 4 == flushOfSpades[0] % 4 
                        && vf 
                        && flushOfSpades.Length > 0)
                    {
                        handStrength = 5.5;

                        handStrengthScore = 13 + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 5.5
                        });

                        sorted = 
                            winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();
                    }

                    if (all17CardsInPlay[i + 1] / 4 == 0 
                        && all17CardsInPlay[i + 1] % 4 == flushOfSpades[0] % 4 
                        && vf)
                    {
                        handStrength = 5.5;

                        handStrengthScore = 13 + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 5.5
                        });

                        sorted = 
                            winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();
                    }
                }
            }
        }

        private void RuleStraight(ref double handStrength, ref double handStrengthScore, int[] playerCards)
        {
            if (handStrength >= -1)
            {
                var openEnded = playerCards.Select(o => o / 4).Distinct().ToArray();
                for (var j = 0; j < openEnded.Length - 4; j++)
                {
                    if (openEnded[j] + 4 == openEnded[j + 4])
                    {
                        if (openEnded.Max() - 4 == openEnded[j])
                        {
                            handStrength = 4;

                            handStrengthScore = openEnded.Max() + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 4
                            });

                            sorted = 
                                winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();
                        }
                        else
                        {
                            handStrength = 4;

                            handStrengthScore = openEnded[j + 4] + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 4
                            });

                            sorted = 
                                winningHands
                                .OrderByDescending(op1 => op1.HandStrength)
                                .ThenByDescending(op1 => op1.HandStrengthScore)
                                .First();
                        }
                    }

                    if (openEnded[j] == 0 
                        && openEnded[j + 1] == 9 
                        && openEnded[j + 2] == 10 
                        && openEnded[j + 3] == 11 
                        && openEnded[j + 4] == 12)
                    {
                        handStrength = 4;

                        handStrengthScore = 13 + handStrength * 100;

                        winningHands.Add(new Hand()
                        {
                            HandStrengthScore = handStrengthScore,
                            HandStrength = 4
                        });

                        sorted = 
                            winningHands
                            .OrderByDescending(op1 => op1.HandStrength)
                            .ThenByDescending(op1 => op1.HandStrengthScore)
                            .First();
                    }
                }
            }
        }

        private void RuleThreeOfAKind(
            ref double handStrength,
            ref double handStrengthScore,
            int[] playerCards)
        {
            if (handStrength >= -1)
            {
                for (var j = 0; j <= 12; j++)
                {
                    var fullHouse = playerCards.Where(o => o / 4 == j).ToArray();
                    if (fullHouse.Length == 3)
                    {
                        if (fullHouse.Max() / 4 == 0)
                        {
                            handStrength = 3;

                            handStrengthScore = 13 * 3 + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 3
                            });

                            sorted = 
                                winningHands
                                .OrderByDescending(op => op.HandStrength)
                                .ThenByDescending(op => op.HandStrengthScore)
                                .First();
                        }
                        else
                        {
                            handStrength = 3;

                            handStrengthScore = 
                                fullHouse[0] / 4 + fullHouse[1] / 4 + fullHouse[2] / 4 + handStrength * 100;

                            winningHands.Add(new Hand()
                            {
                                HandStrengthScore = handStrengthScore,
                                HandStrength = 3
                            });

                            sorted = 
                                winningHands
                                .OrderByDescending(op => op.HandStrength)
                                .ThenByDescending(op => op.HandStrengthScore)
                                .First();
                        }
                    }
                }
            }
        }

        private void RuleTwoPair(
            ref double handStrength,
            ref double handStrengthScore)
        {
            if (handStrength >= -1)
            {
                bool messageBox = false;

                const int maxIndex = 16;
                const int minIndex = 12;

                for (var communityCardsIndex = maxIndex; communityCardsIndex >= minIndex; communityCardsIndex--)
                {
                    int max = communityCardsIndex - minIndex;

                    if (all17CardsInPlay[i] / 4 != all17CardsInPlay[i + 1] / 4)
                    {
                        for (int k = 1; k <= max; k++)
                        {
                            if (communityCardsIndex - k < 12)
                            {
                                max--;
                            }

                            if (communityCardsIndex - k >= 12)
                            {
                                if (all17CardsInPlay[i] / 4 == all17CardsInPlay[communityCardsIndex] / 4 
                                    && all17CardsInPlay[i + 1] / 4 == all17CardsInPlay[communityCardsIndex - k] / 4 
                                    || all17CardsInPlay[i + 1] / 4 == all17CardsInPlay[communityCardsIndex] / 4 
                                    && all17CardsInPlay[i] / 4 == all17CardsInPlay[communityCardsIndex - k] / 4)
                                {
                                    if (!messageBox)
                                    {
                                        if (all17CardsInPlay[i] / 4 == 0)
                                        {
                                            handStrength = 2;

                                            handStrengthScore = 13 * 4 + (all17CardsInPlay[i + 1] / 4) * 2 + handStrength * 100;

                                            winningHands.Add(new Hand()
                                            {
                                                HandStrengthScore = handStrengthScore,
                                                HandStrength = 2
                                            });

                                            sorted = 
                                                winningHands
                                                .OrderByDescending(op => op.HandStrength)
                                                .ThenByDescending(op => op.HandStrengthScore)
                                                .First();
                                        }

                                        if (all17CardsInPlay[i + 1] / 4 == 0)
                                        {
                                            handStrength = 2;

                                            handStrengthScore = 13 * 4 + (all17CardsInPlay[i] / 4) * 2 + handStrength * 100;

                                            winningHands.Add(new Hand()
                                            {
                                                HandStrengthScore = handStrengthScore,
                                                HandStrength = 2
                                            });

                                            sorted = 
                                                winningHands
                                                .OrderByDescending(op => op.HandStrength)
                                                .ThenByDescending(op => op.HandStrengthScore)
                                                .First();
                                        }

                                        if (all17CardsInPlay[i + 1] / 4 != 0 && all17CardsInPlay[i] / 4 != 0)
                                        {
                                            handStrength = 2;

                                            handStrengthScore = (all17CardsInPlay[i]/4) * 2 + (all17CardsInPlay[i + 1] / 4) * 2 + handStrength * 100;

                                            winningHands.Add(new Hand()
                                            {
                                                HandStrengthScore = handStrengthScore,
                                                HandStrength = 2
                                            });

                                            sorted = 
                                                winningHands
                                                .OrderByDescending(op => op.HandStrength)
                                                .ThenByDescending(op => op.HandStrengthScore)
                                                .First();
                                        }
                                    }

                                    messageBox = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void RulePairTwoPair(
            ref double handStrength,
            ref double handStrengthScore)
        {
            if (handStrength >= -1)
            {
                bool messageBox = false;
                bool otherMessageBox = false;

                const int maxIndex = 16;
                const int minIndex = 12;

                for (int communityCardsIndex = maxIndex; communityCardsIndex >= minIndex; communityCardsIndex--)
                {
                    int max = communityCardsIndex - minIndex;
                    for (int k = 1; k <= max; k++)
                    {
                        if (communityCardsIndex - k < 12)
                        {
                            max--;
                        }
                        if (communityCardsIndex - k >= 12)
                        {
                            if (all17CardsInPlay[communityCardsIndex] / 4 == all17CardsInPlay[communityCardsIndex - k] / 4)
                            {
                                if (all17CardsInPlay[communityCardsIndex] / 4 != all17CardsInPlay[i] / 4 
                                    && all17CardsInPlay[communityCardsIndex] / 4 != all17CardsInPlay[i + 1] / 4 
                                    && handStrength == 1)
                                {
                                    if (!messageBox)
                                    {
                                        if (all17CardsInPlay[i + 1] / 4 == 0)
                                        {
                                            handStrength = 2;

                                            handStrengthScore = (all17CardsInPlay[i] / 4) * 2 + 13 * 4 + handStrength * 100;

                                            winningHands.Add(new Hand()
                                            {
                                                HandStrengthScore = handStrengthScore,
                                                HandStrength = 2
                                            });

                                            sorted = 
                                                winningHands
                                                .OrderByDescending(op => op.HandStrength)
                                                .ThenByDescending(op => op.HandStrengthScore)
                                                .First();
                                        }

                                        if (all17CardsInPlay[i] / 4 == 0)
                                        {
                                            handStrength = 2;

                                            handStrengthScore = (all17CardsInPlay[i + 1] / 4) * 2 + 13 * 4 + handStrength * 100;

                                            winningHands.Add(new Hand()
                                            {
                                                HandStrengthScore = handStrengthScore,
                                                HandStrength = 2
                                            });

                                            sorted = 
                                                winningHands
                                                .OrderByDescending(op => op.HandStrength)
                                                .ThenByDescending(op => op.HandStrengthScore)
                                                .First();
                                        }

                                        if (all17CardsInPlay[i + 1] / 4 != 0)
                                        {
                                            handStrength = 2;

                                            handStrengthScore = 
                                                (all17CardsInPlay[communityCardsIndex] / 4) * 2 + (all17CardsInPlay[i + 1] / 4) * 2 + handStrength * 100;

                                            winningHands.Add(new Hand()
                                            {
                                                HandStrengthScore = handStrengthScore,
                                                HandStrength = 2
                                            });

                                            sorted = 
                                                winningHands
                                                .OrderByDescending(op => op.HandStrength)
                                                .ThenByDescending(op => op.HandStrengthScore)
                                                .First();
                                        }
                                        if (all17CardsInPlay[i] / 4 != 0)
                                        {
                                            handStrength = 2;

                                            handStrengthScore = 
                                                (all17CardsInPlay[communityCardsIndex] / 4) * 2 + (all17CardsInPlay[i] / 4) * 2 + handStrength * 100;

                                            winningHands.Add(new Hand()
                                            {
                                                HandStrengthScore = handStrengthScore,
                                                HandStrength = 2
                                            });

                                            sorted = 
                                                winningHands
                                                .OrderByDescending(op => op.HandStrength)
                                                .ThenByDescending(op => op.HandStrengthScore)
                                                .First();
                                        }
                                    }

                                    messageBox = true;
                                }

                                if (handStrength == -1)
                                {
                                    if (!otherMessageBox)
                                    {
                                        if (all17CardsInPlay[i] / 4 > all17CardsInPlay[i + 1] / 4)
                                        {
                                            if (all17CardsInPlay[communityCardsIndex] / 4 == 0)
                                            {
                                                handStrength = 0;

                                                handStrengthScore = 13 + all17CardsInPlay[i] / 4 + handStrength * 100;

                                                winningHands.Add(new Hand()
                                                {
                                                    HandStrengthScore = handStrengthScore,
                                                    HandStrength = 1
                                                });

                                                sorted = 
                                                    winningHands
                                                    .OrderByDescending(op => op.HandStrength)
                                                    .ThenByDescending(op => op.HandStrengthScore)
                                                    .First();
                                            }
                                            else
                                            {
                                                handStrength = 0;

                                                handStrengthScore = all17CardsInPlay[communityCardsIndex] / 4 + all17CardsInPlay[i] / 4 + handStrength * 100;

                                                winningHands.Add(new Hand()
                                                {
                                                    HandStrengthScore = handStrengthScore,
                                                    HandStrength = 1
                                                });

                                                sorted = 
                                                    winningHands
                                                    .OrderByDescending(op => op.HandStrength)
                                                    .ThenByDescending(op => op.HandStrengthScore)
                                                    .First();
                                            }
                                        }
                                        else
                                        {
                                            if (all17CardsInPlay[communityCardsIndex] / 4 == 0)
                                            {
                                                handStrength = 0;

                                                handStrengthScore = 13 + all17CardsInPlay[i + 1] + handStrength * 100;

                                                winningHands.Add(new Hand()
                                                {
                                                    HandStrengthScore = handStrengthScore,
                                                    HandStrength = 1
                                                });

                                                sorted = 
                                                    winningHands
                                                    .OrderByDescending(op => op.HandStrength)
                                                    .ThenByDescending(op => op.HandStrengthScore)
                                                    .First();
                                            }
                                            else
                                            {
                                                handStrength = 0;

                                                handStrengthScore = all17CardsInPlay[communityCardsIndex] / 4 + all17CardsInPlay[i + 1] / 4 + handStrength * 100;

                                                winningHands.Add(new Hand()
                                                {
                                                    HandStrengthScore = handStrengthScore,
                                                    HandStrength = 1
                                                });

                                                sorted = winningHands
                                                    .OrderByDescending(op => op.HandStrength)
                                                    .ThenByDescending(op => op.HandStrengthScore)
                                                    .First();
                                            }
                                        }
                                    }

                                    otherMessageBox = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void rPairFromHand(ref double handStrength, ref double HandStrengthScore)
        {
            if (handStrength >= -1)
            {
                bool msgbox = false;
                if (all17CardsInPlay[i]/4 == all17CardsInPlay[i + 1]/4)
                {
                    if (!msgbox)
                    {
                        if (all17CardsInPlay[i]/4 == 0)
                        {
                            handStrength = 1;
                            HandStrengthScore = 13*4 + handStrength*100;
                            winningHands.Add(new Hand() {HandStrengthScore = HandStrengthScore, HandStrength = 1});
                            sorted = winningHands.OrderByDescending(op => op.HandStrength).ThenByDescending(op => op.HandStrengthScore).First();
                        }
                        else
                        {
                            handStrength = 1;
                            HandStrengthScore = (all17CardsInPlay[i + 1]/4)*4 + handStrength*100;
                            winningHands.Add(new Hand() {HandStrengthScore = HandStrengthScore, HandStrength = 1});
                            sorted = winningHands.OrderByDescending(op => op.HandStrength).ThenByDescending(op => op.HandStrengthScore).First();
                        }
                    }
                    msgbox = true;
                }
                for (int communityCardsIndex = 16; communityCardsIndex >= 12; communityCardsIndex--)
                {
                    if (all17CardsInPlay[i + 1]/4 == all17CardsInPlay[communityCardsIndex]/4)
                    {
                        if (!msgbox)
                        {
                            if (all17CardsInPlay[i + 1]/4 == 0)
                            {
                                handStrength = 1;
                                HandStrengthScore = 13*4 + all17CardsInPlay[i]/4 + handStrength*100;
                                winningHands.Add(new Hand() {HandStrengthScore = HandStrengthScore, HandStrength = 1});
                                sorted = winningHands.OrderByDescending(op => op.HandStrength).ThenByDescending(op => op.HandStrengthScore).First();
                            }
                            else
                            {
                                handStrength = 1;
                                HandStrengthScore = (all17CardsInPlay[i + 1]/4)*4 + all17CardsInPlay[i]/4 + handStrength*100;
                                winningHands.Add(new Hand() {HandStrengthScore = HandStrengthScore, HandStrength = 1});
                                sorted = winningHands.OrderByDescending(op => op.HandStrength).ThenByDescending(op => op.HandStrengthScore).First();
                            }
                        }
                        msgbox = true;
                    }
                    if (all17CardsInPlay[i]/4 == all17CardsInPlay[communityCardsIndex]/4)
                    {
                        if (!msgbox)
                        {
                            if (all17CardsInPlay[i]/4 == 0)
                            {
                                handStrength = 1;
                                HandStrengthScore = 13*4 + all17CardsInPlay[i + 1]/4 + handStrength*100;
                                winningHands.Add(new Hand() {HandStrengthScore = HandStrengthScore, HandStrength = 1});
                                sorted = winningHands.OrderByDescending(op => op.HandStrength).ThenByDescending(op => op.HandStrengthScore).First();
                            }
                            else
                            {
                                handStrength = 1;
                                HandStrengthScore = (all17CardsInPlay[communityCardsIndex]/4)*4 + all17CardsInPlay[i + 1]/4 + handStrength*100;
                                winningHands.Add(new Hand() {HandStrengthScore = HandStrengthScore, HandStrength = 1});
                                sorted = winningHands.OrderByDescending(op => op.HandStrength).ThenByDescending(op => op.HandStrengthScore).First();
                            }
                        }
                        msgbox = true;
                    }
                }
            }
        }

        private void rHighCard(ref double handStrength, ref double HandStrengthScore)
        {
            if (handStrength == -1)
            {
                if (all17CardsInPlay[i]/4 > all17CardsInPlay[i + 1]/4)
                {
                    handStrength = -1;
                    HandStrengthScore = all17CardsInPlay[i]/4;
                    winningHands.Add(new Hand() {HandStrengthScore = HandStrengthScore, HandStrength = -1});
                    sorted = winningHands.OrderByDescending(op1 => op1.HandStrength).ThenByDescending(op1 => op1.HandStrengthScore).First();
                }
                else
                {
                    handStrength = -1;
                    HandStrengthScore = all17CardsInPlay[i + 1]/4;
                    winningHands.Add(new Hand() {HandStrengthScore = HandStrengthScore, HandStrength = -1});
                    sorted = winningHands.OrderByDescending(op1 => op1.HandStrength).ThenByDescending(op1 => op1.HandStrengthScore).First();
                }
                if (all17CardsInPlay[i]/4 == 0 || all17CardsInPlay[i + 1]/4 == 0)
                {
                    handStrength = -1;
                    HandStrengthScore = 13;
                    winningHands.Add(new Hand() {HandStrengthScore = HandStrengthScore, HandStrength = -1});
                    sorted = winningHands.OrderByDescending(op1 => op1.HandStrength).ThenByDescending(op1 => op1.HandStrengthScore).First();
                }
            }
        }

        void AwardWinner(double handStrength, double HandStrengthScore, string currentText, int playerChips, string lastly)
        {
            if (lastly == " ")
            {
                lastly = "Bot 5";
            }
            for (int j = 0; j <= 16; j++)
            {
                //await Task.Delay(5);
                if (deckPictureBoxArr[j].Visible)
                {
                    deckPictureBoxArr[j].Image = deckImageArr[j];
                }       
            }
            if (handStrength == sorted.HandStrength)
            {
                if (HandStrengthScore == sorted.HandStrengthScore)
                {
                    winnersCount++;
                    winnersStringMsgs.Add(currentText);
                    if (handStrength == -1)
                    {
                        MessageBox.Show(currentText + " High Card ");
                    }

                    if (handStrength == 1 || handStrength == 0)
                    {
                        MessageBox.Show(currentText + " Pair ");
                    }

                    if (handStrength == 2)
                    {
                        MessageBox.Show(currentText + " Two Pair ");
                    }

                    if (handStrength == 3)
                    {
                        MessageBox.Show(currentText + " Three of a Kind ");
                    }

                    if (handStrength == 4)
                    {
                        MessageBox.Show(currentText + " Straight ");
                    }

                    if (handStrength == 5 || handStrength == 5.5)
                    {
                        MessageBox.Show(currentText + " Flush ");
                    }

                    if (handStrength == 6)
                    {
                        MessageBox.Show(currentText + " Full House ");
                    }

                    if (handStrength == 7)
                    {
                        MessageBox.Show(currentText + " Four of a Kind ");
                    }

                    if (handStrength == 8)
                    {
                        MessageBox.Show(currentText + " Straight Flush ");
                    }

                    if (handStrength == 9)
                    {
                        MessageBox.Show(currentText + " Royal Flush ! ");
                    }
                }
            }
            //lastfixed
            if (currentText == lastly) 
            {
                if (winnersCount > 1)
                {
                    if (winnersStringMsgs.Contains("Player"))
                    {
                        this.playerChips += int.Parse(textboxPot.Text)/ winnersCount;
                        textboxPlayerChips.Text = this.playerChips.ToString();
                        ////pPanel.Visible = true;
                    }

                    if (winnersStringMsgs.Contains("Bot 1"))
                    {
                        firstBotChips += int.Parse(textboxPot.Text)/winnersCount;
                        textboxFirstBotChips.Text = firstBotChips.ToString();
                        ////b1Panel.Visible = true;
                    }

                    if (winnersStringMsgs.Contains("Bot 2"))
                    {
                        secondBotChips += int.Parse(textboxPot.Text)/winnersCount;
                        textboxSecondBotChips.Text = secondBotChips.ToString();
                        ////b2Panel.Visible = true;
                    }

                    if (winnersStringMsgs.Contains("Bot 3"))
                    {
                        thirdBotChips += int.Parse(textboxPot.Text)/winnersCount;
                        textbokThirdBotChips.Text = thirdBotChips.ToString();
                        ////b3Panel.Visible = true;
                    }

                    if (winnersStringMsgs.Contains("Bot 4"))
                    {
                        fourthBotChips += int.Parse(textboxPot.Text)/winnersCount;
                        textboxFourthBotChips.Text = fourthBotChips.ToString();
                        //b4Panel.Visible = true;
                    }

                    if (winnersStringMsgs.Contains("Bot 5"))
                    {
                        fifthBotChips += int.Parse(textboxPot.Text)/winnersCount;
                        textboxFifthBotChips.Text = fifthBotChips.ToString();
                        //b5Panel.Visible = true;
                    }

                    //await Finish(1);
                }
                if (winnersCount == 1)
                {
                    if (winnersStringMsgs.Contains("Player"))
                    {
                        this.playerChips += int.Parse(textboxPot.Text);
                        //await Finish(1);
                        //pPanel.Visible = true;
                    }
                    if (winnersStringMsgs.Contains("Bot 1"))
                    {
                        firstBotChips += int.Parse(textboxPot.Text);
                        //await Finish(1);
                        //b1Panel.Visible = true;
                    }
                    if (winnersStringMsgs.Contains("Bot 2"))
                    {
                        secondBotChips += int.Parse(textboxPot.Text);
                        //await Finish(1);
                        //b2Panel.Visible = true;
                    }
                    if (winnersStringMsgs.Contains("Bot 3"))
                    {
                        thirdBotChips += int.Parse(textboxPot.Text);
                        //await Finish(1);
                        //b3Panel.Visible = true;
                    }
                    if (winnersStringMsgs.Contains("Bot 4"))
                    {
                        fourthBotChips += int.Parse(textboxPot.Text);
                        //await Finish(1);
                        //b4Panel.Visible = true;
                    }
                    if (winnersStringMsgs.Contains("Bot 5"))
                    {
                        fifthBotChips += int.Parse(textboxPot.Text);
                        //await Finish(1);
                        //b5Panel.Visible = true;
                    }
                }
            }
        }

        async Task CheckRaise(int currentTurn, int raiseTurn)
        {
            if (raising)
            {
                turnCount = 0;
                raising = false;
                raisedTurn = currentTurn;
                changedRound = true;
            }
            else
            {
                if (turnCount >= playersLeftToAct - 1 || !changedRound && turnCount == playersLeftToAct)
                {
                    if (currentTurn == raisedTurn - 1 || !changedRound && turnCount == playersLeftToAct || raisedTurn == 0 && currentTurn == 5)
                    {
                        changedRound = false;
                        turnCount = 0;
                        raiseAmount = 0;
                        amountToCall = 0;
                        raisedTurn = 123;
                        rounds++;
                        if (!playerFoldedTurn)
                        {
                            playerStatus.Text = string.Empty;
                        }
                            
                        if (!firstBotFoldedTurn)
                        {
                            firstBotStatus.Text = string.Empty;
                        }
                           
                        if (!secodBotFoldedTurn)
                        {
                            secondBotStatus.Text = string.Empty;
                        }
                           
                        if (!thirdBotFoldedTurn)
                        {
                            thirdBotStatus.Text = string.Empty;
                        }
                           
                        if (!fourthBotFoldedTurn)
                        {
                            fourthBotStatus.Text = string.Empty;
                        }
                           
                        if (!fifthBotFoldedTurn)
                        {
                            fifthBotStatus.Text = string.Empty;
                        }              
                    }
                }
            }
            if (rounds == flop)
            {
                for (int j = 12; j <= 14; j++)
                {
                    if (deckPictureBoxArr[j].Image != deckImageArr[j])
                    {
                        deckPictureBoxArr[j].Image = deckImageArr[j];
                        playerCallAmount = 0;
                        playerRaiseAmount = 0;
                        firstBotCallAmount = 0;
                        firstBotRaiseAmount = 0;
                        secondBotCallAmount = 0;
                        secondBotRaiseAmount = 0;
                        thirdBotCallAmount = 0;
                        thirdBotRaiseAmount = 0;
                        fourthBotCallAmount = 0;
                        fourthBotRaiseAmount = 0;
                        fifthBotCallAmount = 0;
                        fifthBotRaiseAmount = 0;
                    }
                }
            }
            if (rounds == turn)
            {
                for (int j = 14; j <= 15; j++)
                {
                    if (deckPictureBoxArr[j].Image != deckImageArr[j])
                    {
                        deckPictureBoxArr[j].Image = deckImageArr[j];
                        playerCallAmount = 0;
                        playerRaiseAmount = 0;
                        firstBotCallAmount = 0;
                        firstBotRaiseAmount = 0;
                        secondBotCallAmount = 0;
                        secondBotRaiseAmount = 0;
                        thirdBotCallAmount = 0;
                        thirdBotRaiseAmount = 0;
                        fourthBotCallAmount = 0;
                        fourthBotRaiseAmount = 0;
                        fifthBotCallAmount = 0;
                        fifthBotRaiseAmount = 0;
                    }
                }
            }
            if (rounds == river)
            {
                for (int j = 15; j <= 16; j++)
                {
                    if (deckPictureBoxArr[j].Image != deckImageArr[j])
                    {
                        deckPictureBoxArr[j].Image = deckImageArr[j];
                        playerCallAmount = 0;
                        playerRaiseAmount = 0;
                        firstBotCallAmount = 0;
                        firstBotRaiseAmount = 0;
                        secondBotCallAmount = 0;
                        secondBotRaiseAmount = 0;
                        thirdBotCallAmount = 0;
                        thirdBotRaiseAmount = 0;
                        fourthBotCallAmount = 0;
                        fourthBotRaiseAmount = 0;
                        fifthBotCallAmount = 0;
                        fifthBotRaiseAmount = 0;
                    }
                }
            }
            if (rounds == end && playersLeftToAct == 6)
            {
                string fixedLast = "qwerty";
                if (!playerStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Player";
                    Rules(0, 1, "Player", ref playerType, ref playerPower, playerFoldedTurn);
                }
                if (!firstBotStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 1";
                    Rules(2, 3, "Bot 1", ref firstBotType, ref firstBotPower, firstBotFoldedTurn);
                }
                if (!secondBotStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 2";
                    Rules(4, 5, "Bot 2", ref secondBotType, ref secondBotPower, secodBotFoldedTurn);
                }
                if (!thirdBotStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 3";
                    Rules(6, 7, "Bot 3", ref thirdBotType, ref thirdBotPower, thirdBotFoldedTurn);
                }
                if (!fourthBotStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 4";
                    Rules(8, 9, "Bot 4", ref fourthBotType, ref fourthBotPower, fourthBotFoldedTurn);
                }
                if (!fifthBotStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 5";
                    Rules(10, 11, "Bot 5", ref fifthBotType, ref fifthBotPower, fifthBotFoldedTurn);
                }
                AwardWinner(playerType, playerPower, "Player", playerChips, fixedLast);
                AwardWinner(firstBotType, firstBotPower, "Bot 1", firstBotChips, fixedLast);
                AwardWinner(secondBotType, secondBotPower, "Bot 2", secondBotChips, fixedLast);
                AwardWinner(thirdBotType, thirdBotPower, "Bot 3", thirdBotChips, fixedLast);
                AwardWinner(fourthBotType, fourthBotPower, "Bot 4", fourthBotChips, fixedLast);
                AwardWinner(fifthBotType, fifthBotPower, "Bot 5", fifthBotChips, fixedLast);
                restart = true;
                playerTurn = true;
                playerFoldedTurn = false;
                firstBotFoldedTurn = false;
                secodBotFoldedTurn = false;
                thirdBotFoldedTurn = false;
                fourthBotFoldedTurn = false;
                fifthBotFoldedTurn = false;
                if (playerChips <= 0)
                {
                    AddChips f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.a != 0)
                    {
                        playerChips = f2.a;
                        firstBotChips += f2.a;
                        secondBotChips += f2.a;
                        thirdBotChips += f2.a;
                        fourthBotChips += f2.a;
                        fifthBotChips += f2.a;
                        playerFoldedTurn = false;
                        playerTurn = true;
                        buttonRaise.Enabled = true;
                        buttonFold.Enabled = true;
                        buttonCheck.Enabled = true;
                        buttonRaise.Text = "Raise";
                    }
                }
                playerPanel.Visible = false;
                firstBotPanel.Visible = false;
                secondBotPanel.Visible = false;
                thirdBotPanel.Visible = false;
                fourthBotPanel.Visible = false;
                fifthBotPanel.Visible = false;
                playerCallAmount = 0;
                playerRaiseAmount = 0;
                firstBotCallAmount = 0;
                firstBotRaiseAmount = 0;
                secondBotCallAmount = 0;
                secondBotRaiseAmount = 0;
                thirdBotCallAmount = 0;
                thirdBotRaiseAmount = 0;
                fourthBotCallAmount = 0;
                fourthBotRaiseAmount = 0;
                fifthBotCallAmount = 0;
                fifthBotRaiseAmount = 0;
                lastOneToAct = 0;
                amountToCall = bigBlind;
                raiseAmount = 0;
                cardImagePaths = Directory.GetFiles("Resources\\Cards", "*.png", SearchOption.TopDirectoryOnly);
                playersInGameStatuses.Clear();
                rounds = 0;
                playerPower = 0;
                playerType = -1;
                type = 0;
                firstBotPower = 0;
                secondBotPower = 0;
                thirdBotPower = 0;
                fourthBotPower = 0;
                fifthBotPower = 0;
                firstBotType = -1;
                secondBotType = -1;
                thirdBotType = -1;
                fourthBotType = -1;
                fifthBotType = -1;
                potContributions.Clear();
                winnersStringMsgs.Clear();
                winnersCount = 0;
                winningHands.Clear();
                sorted.HandStrength = 0;
                sorted.HandStrengthScore = 0;
                for (int os = 0; os < 17; os++)
                {
                    deckPictureBoxArr[os].Image = null;
                    deckPictureBoxArr[os].Invalidate();
                    deckPictureBoxArr[os].Visible = false;
                }
                textboxPot.Text = "0";
                playerStatus.Text = string.Empty;
                await Shuffle();
                await Turns();
            }
        }

        void FixCall(Label status, ref int cCall, ref int cRaise, int options)
        {
            if (rounds != 4)
            {
                if (options == 1)
                {
                    if (status.Text.Contains("Raise"))
                    {
                        var changeRaise = status.Text.Substring(6);
                        cRaise = int.Parse(changeRaise);
                    }
                    if (status.Text.Contains("Call"))
                    {
                        var changeCall = status.Text.Substring(5);
                        cCall = int.Parse(changeCall);
                    }
                    if (status.Text.Contains("Check"))
                    {
                        cRaise = 0;
                        cCall = 0;
                    }
                }
                if (options == 2)
                {
                    if (cRaise != raiseAmount && cRaise <= raiseAmount)
                    {
                        amountToCall = Convert.ToInt32(raiseAmount) - cRaise;
                    }

                    if (cCall != amountToCall || cCall <= amountToCall)
                    {
                        amountToCall = amountToCall - cCall;
                    }

                    if (cRaise == raiseAmount && raiseAmount > 0)
                    {
                        amountToCall = 0;
                        buttonCall.Enabled = false;
                        buttonCall.Text = "Callisfuckedup";
                    }
                }
            }
        }

        async Task AllIn()
        {
            #region All in

            if (playerChips <= 0 && !potIsNotEmpty)
            {
                if (playerStatus.Text.Contains("Raise"))
                {
                    potContributions.Add(playerChips);
                    potIsNotEmpty = true;
                }

                if (playerStatus.Text.Contains("Call"))
                {
                    potContributions.Add(playerChips);
                    potIsNotEmpty = true;
                }
            }
            potIsNotEmpty = false;
            if (firstBotChips <= 0 && !firstBotFoldedTurn)
            {
                if (!potIsNotEmpty)
                {
                    potContributions.Add(firstBotChips);
                    potIsNotEmpty = true;
                }

                potIsNotEmpty = false;
            }
            if (secondBotChips <= 0 && !secodBotFoldedTurn)
            {
                if (!potIsNotEmpty)
                {
                    potContributions.Add(secondBotChips);
                    potIsNotEmpty = true;
                }

                potIsNotEmpty = false;
            }
            if (thirdBotChips <= 0 && !thirdBotFoldedTurn)
            {
                if (!potIsNotEmpty)
                {
                    potContributions.Add(thirdBotChips);
                    potIsNotEmpty = true;
                }

                potIsNotEmpty = false;
            }
            if (fourthBotChips <= 0 && !fourthBotFoldedTurn)
            {
                if (!potIsNotEmpty)
                {
                    potContributions.Add(fourthBotChips);
                    potIsNotEmpty = true;
                }

                potIsNotEmpty = false;
            }
            if (fifthBotChips <= 0 && !fifthBotFoldedTurn)
            {
                if (!potIsNotEmpty)
                {
                    potContributions.Add(fifthBotChips);
                    potIsNotEmpty = true;
                }
            }
            if (potContributions.ToArray().Length == playersLeftToAct)
            {
                await Finish(2);
            }
            else
            {
                potContributions.Clear();
            }

            #endregion

            var abc = playersInGameStatuses.Count(x => x == false);

            #region LastManStanding

            if (abc == 1)
            {
                int index = playersInGameStatuses.IndexOf(false);
                if (index == 0)
                {
                    playerChips += int.Parse(textboxPot.Text);
                    textboxPlayerChips.Text = playerChips.ToString();
                    playerPanel.Visible = true;
                    MessageBox.Show("Player Wins");
                }

                if (index == 1)
                {
                    firstBotChips += int.Parse(textboxPot.Text);
                    textboxPlayerChips.Text = firstBotChips.ToString();
                    firstBotPanel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }

                if (index == 2)
                {
                    secondBotChips += int.Parse(textboxPot.Text);
                    textboxPlayerChips.Text = secondBotChips.ToString();
                    secondBotPanel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }

                if (index == 3)
                {
                    thirdBotChips += int.Parse(textboxPot.Text);
                    textboxPlayerChips.Text = thirdBotChips.ToString();
                    thirdBotPanel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }

                if (index == 4)
                {
                    fourthBotChips += int.Parse(textboxPot.Text);
                    textboxPlayerChips.Text = fourthBotChips.ToString();
                    fourthBotPanel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }

                if (index == 5)
                {
                    fifthBotChips += int.Parse(textboxPot.Text);
                    textboxPlayerChips.Text = fifthBotChips.ToString();
                    fifthBotPanel.Visible = true;
                    MessageBox.Show("Bot 5 Wins");
                }

                for (int j = 0; j <= 16; j++)
                {
                    deckPictureBoxArr[j].Visible = false;
                }
                await Finish(1);
            }

            potIsNotEmpty = false;

            #endregion

            #region FiveOrLessLeft

            if (abc < 6 && abc > 1 && rounds >= end)
            {
                await Finish(2);
            }

            #endregion
        }

        async Task Finish(int n)
        {
            if (n == 2)
            {
                FixWinners();
            }
            playerPanel.Visible = false;
            firstBotPanel.Visible = false;
            secondBotPanel.Visible = false;
            thirdBotPanel.Visible = false;
            fourthBotPanel.Visible = false;
            fifthBotPanel.Visible = false;
            amountToCall = bigBlind;
            raiseAmount = 0;
            foldedPlayers = 5;
            type = 0;
            rounds = 0;
            firstBotPower = 0;
            secondBotPower = 0;
            thirdBotPower = 0;
            fourthBotPower = 0;
            fifthBotPower = 0;
            playerPower = 0;
            playerType = -1;
            raiseAmount = 0;
            firstBotType = -1;
            secondBotType = -1;
            thirdBotType = -1;
            fourthBotType = -1;
            fifthBotType = -1;
            firstBotTurn = false;
            secondBotTurn = false;
            thirdBotTurn = false;
            fourthBotTurn = false;
            fifthBotTurn = false;
            firstBotFoldedTurn = false;
            secodBotFoldedTurn = false;
            thirdBotFoldedTurn = false;
            fourthBotFoldedTurn = false;
            fifthBotFoldedTurn = false;
            playerFolded = false;
            firstBotFolded = false;
            secondBotFolded = false;
            thirdBotFolded = false;
            fourthBotFolded = false;
            fifthBotFolded = false;
            playerFoldedTurn = false;
            playerTurn = true;
            restart = false;
            raising = false;
            playerCallAmount = 0;
            firstBotCallAmount = 0;
            secondBotCallAmount = 0;
            thirdBotCallAmount = 0;
            fourthBotCallAmount = 0;
            fifthBotCallAmount = 0;
            playerRaiseAmount = 0;
            firstBotRaiseAmount = 0;
            secondBotRaiseAmount = 0;
            thirdBotRaiseAmount = 0;
            fourthBotRaiseAmount = 0;
            fifthBotRaiseAmount = 0;
            height = 0;
            width = 0;
            winnersCount = 0;
            flop = 1;
            turn = 2;
            river = 3;
            end = 4;
            playersLeftToAct = 6;
            lastOneToAct = 123;
            raisedTurn = 1;
            playersInGameStatuses.Clear();
            winnersStringMsgs.Clear();
            potContributions.Clear();
            winningHands.Clear();
            sorted.HandStrength = 0;
            sorted.HandStrengthScore = 0;
            textboxPot.Text = "0";
            timeTillNextTurn = 60;
            up = 10000000;
            turnCount = 0;
            playerStatus.Text = string.Empty;
            firstBotStatus.Text = string.Empty;
            secondBotStatus.Text = string.Empty;
            thirdBotStatus.Text = string.Empty;
            fourthBotStatus.Text = string.Empty;
            fifthBotStatus.Text = string.Empty;
            if (playerChips <= 0)
            {
                AddChips f2 = new AddChips();
                f2.ShowDialog();
                if (f2.a != 0)
                {
                    playerChips = f2.a;
                    firstBotChips += f2.a;
                    secondBotChips += f2.a;
                    thirdBotChips += f2.a;
                    fourthBotChips += f2.a;
                    fifthBotChips += f2.a;
                    playerFoldedTurn = false;
                    playerTurn = true;
                    buttonRaise.Enabled = true;
                    buttonFold.Enabled = true;
                    buttonCheck.Enabled = true;
                    buttonRaise.Text = "Raise";
                }
            }
            cardImagePaths = Directory.GetFiles("Resources\\Cards", "*.png", SearchOption.TopDirectoryOnly);
            for (int os = 0; os < 17; os++)
            {
                deckPictureBoxArr[os].Image = null;
                deckPictureBoxArr[os].Invalidate();
                deckPictureBoxArr[os].Visible = false;
            }
            await Shuffle();
            //await Turns();
        }

        void FixWinners()
        {
            winningHands.Clear();
            sorted.HandStrength = 0;
            sorted.HandStrengthScore = 0;
            string fixedLast = "qwerty";
            if (!playerStatus.Text.Contains("Fold"))
            {
                fixedLast = "Player";
                Rules(0, 1, "Player", ref playerType, ref playerPower, playerFoldedTurn);
            }

            if (!firstBotStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 1";
                Rules(2, 3, "Bot 1", ref firstBotType, ref firstBotPower, firstBotFoldedTurn);
            }

            if (!secondBotStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 2";
                Rules(4, 5, "Bot 2", ref secondBotType, ref secondBotPower, secodBotFoldedTurn);
            }

            if (!thirdBotStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 3";
                Rules(6, 7, "Bot 3", ref thirdBotType, ref thirdBotPower, thirdBotFoldedTurn);
            }

            if (!fourthBotStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 4";
                Rules(8, 9, "Bot 4", ref fourthBotType, ref fourthBotPower, fourthBotFoldedTurn);
            }

            if (!fifthBotStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 5";
                Rules(10, 11, "Bot 5", ref fifthBotType, ref fifthBotPower, fifthBotFoldedTurn);
            }

            AwardWinner(playerType, playerPower, "Player", playerChips, fixedLast);
            AwardWinner(firstBotType, firstBotPower, "Bot 1", firstBotChips, fixedLast);
            AwardWinner(secondBotType, secondBotPower, "Bot 2", secondBotChips, fixedLast);
            AwardWinner(thirdBotType, thirdBotPower, "Bot 3", thirdBotChips, fixedLast);
            AwardWinner(fourthBotType, fourthBotPower, "Bot 4", fourthBotChips, fixedLast);
            AwardWinner(fifthBotType, fifthBotPower, "Bot 5", fifthBotChips, fixedLast);
        }

        void AI(int c1, int c2, ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower, double botCurrent)
        {
            if (!sFTurn)
            {
                if (botCurrent == -1)
                {
                    HighCard(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }

                if (botCurrent == 0)
                {
                    PairTable(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }

                if (botCurrent == 1)
                {
                    PairHand(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }

                if (botCurrent == 2)
                {
                    TwoPair(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower);
                }

                if (botCurrent == 3)
                {
                    ThreeOfAKind(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }

                if (botCurrent == 4)
                {
                    Straight(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }

                if (botCurrent == 5 || botCurrent == 5.5)
                {
                    Flush(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }

                if (botCurrent == 6)
                {
                    FullHouse(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }

                if (botCurrent == 7)
                {
                    FourOfAKind(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }

                if (botCurrent == 8 || botCurrent == 9)
                {
                    StraightFlush(ref sChips, ref sTurn, ref sFTurn, sStatus, name, botPower);
                }
            }
            if (sFTurn)
            {
                deckPictureBoxArr[c1].Visible = false;
                deckPictureBoxArr[c2].Visible = false;
            }
        }

        private void HighCard(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            HP(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower, 20, 25);
        }

        private void PairTable(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            HP(ref sChips, ref sTurn, ref sFTurn, sStatus, botPower, 16, 25);
        }

        private void PairHand(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(10, 16);
            int rRaise = rPair.Next(10, 13);
            if (botPower <= 199 && botPower >= 140)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 6, rRaise);
            }

            if (botPower <= 139 && botPower >= 128)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 7, rRaise);
            }

            if (botPower < 128 && botPower >= 101)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 9, rRaise);
            }
        }

        private void TwoPair(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower)
        {
            Random rPair = new Random();
            int rCall = rPair.Next(6, 11);
            int rRaise = rPair.Next(6, 11);
            if (botPower <= 290 && botPower >= 246)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 3, rRaise);
            }

            if (botPower <= 244 && botPower >= 234)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 4, rRaise);
            }

            if (botPower < 234 && botPower >= 201)
            {
                PH(ref sChips, ref sTurn, ref sFTurn, sStatus, rCall, 4, rRaise);
            }
        }

        private void ThreeOfAKind(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random tk = new Random();
            int tCall = tk.Next(3, 7);
            int tRaise = tk.Next(4, 8);
            if (botPower <= 390 && botPower >= 330)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise);
            }

            //10  8
            if (botPower <= 327 && botPower >= 321) 
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise);
            }

            //7 2
            if (botPower < 321 && botPower >= 303) 
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise);
            }
        }

        private void Straight(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random str = new Random();
            int sCall = str.Next(3, 6);
            int sRaise = str.Next(3, 8);
            if (botPower <= 480 && botPower >= 410)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise);
            }

            //10  8
            if (botPower <= 409 && botPower >= 407) 
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise);
            }

            if (botPower < 407 && botPower >= 404)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sCall, sRaise);
            }
        }

        private void Flush(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random fsh = new Random();
            int fCall = fsh.Next(2, 6);
            int fRaise = fsh.Next(3, 7);
            Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fCall, fRaise);
        }

        private void FullHouse(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random flh = new Random();
            int fhCall = flh.Next(1, 5);
            int fhRaise = flh.Next(2, 6);
            if (botPower <= 626 && botPower >= 620)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fhCall, fhRaise);
            }

            if (botPower < 620 && botPower >= 602)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fhCall, fhRaise);
            }
        }

        private void FourOfAKind(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random fk = new Random();
            int fkCall = fk.Next(1, 4);
            int fkRaise = fk.Next(2, 5);
            if (botPower <= 752 && botPower >= 704)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, fkCall, fkRaise);
            }
        }

        private void StraightFlush(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int name, double botPower)
        {
            Random sf = new Random();
            int sfCall = sf.Next(1, 3);
            int sfRaise = sf.Next(1, 3);
            if (botPower <= 913 && botPower >= 804)
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, sfCall, sfRaise);
            }
        }

        private void Fold(ref bool sTurn, ref bool sFTurn, Label sStatus)
        {
            raising = false;
            sStatus.Text = "Fold";
            sTurn = false;
            sFTurn = true;
        }

        private void Check(ref bool cTurn, Label cStatus)
        {
            cStatus.Text = "Check";
            cTurn = false;
            raising = false;
        }

        private void Call(ref int sChips, ref bool sTurn, Label sStatus)
        {
            raising = false;
            sTurn = false;
            sChips -= amountToCall;
            sStatus.Text = "Call " + amountToCall;
            textboxPot.Text = (int.Parse(textboxPot.Text) + amountToCall).ToString();
        }

        private void Raised(ref int sChips, ref bool sTurn, Label sStatus)
        {
            sChips -= Convert.ToInt32(raiseAmount);
            sStatus.Text = "Raise " + raiseAmount;
            textboxPot.Text = (int.Parse(textboxPot.Text) + Convert.ToInt32(raiseAmount)).ToString();
            amountToCall = Convert.ToInt32(raiseAmount);
            raising = true;
            sTurn = false;
        }

        private static double RoundN(int sChips, int n)
        {
            double a = Math.Round((sChips/n)/100d, 0)*100;
            return a;
        }

        private void HP(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, double botPower, int n, int n1)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 4);
            if (amountToCall <= 0)
            {
                Check(ref sTurn, sStatus);
            }
            if (amountToCall > 0)
            {
                if (rnd == 1)
                {
                    if (amountToCall <= RoundN(sChips, n))
                    {
                        Call(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }
                if (rnd == 2)
                {
                    if (amountToCall <= RoundN(sChips, n1))
                    {
                        Call(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }
            }
            if (rnd == 3)
            {
                if (raiseAmount == 0)
                {
                    raiseAmount = amountToCall * 2;
                    Raised(ref sChips, ref sTurn, sStatus);
                }
                else
                {
                    if (raiseAmount <= RoundN(sChips, n))
                    {
                        raiseAmount = amountToCall * 2;
                        Raised(ref sChips, ref sTurn, sStatus);
                    }
                    else
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                }
            }
            if (sChips <= 0)
            {
                sFTurn = true;
            }
        }

        private void PH(ref int sChips, ref bool sTurn, ref bool sFTurn, Label sStatus, int n, int n1, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (rounds < 2)
            {
                if (amountToCall <= 0)
                {
                    Check(ref sTurn, sStatus);
                }
                if (amountToCall > 0)
                {
                    if (amountToCall >= RoundN(sChips, n1))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }

                    if (raiseAmount > RoundN(sChips, n))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }

                    if (!sFTurn)
                    {
                        if (amountToCall >= RoundN(sChips, n) && amountToCall <= RoundN(sChips, n1))
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }

                        if (raiseAmount <= RoundN(sChips, n) && raiseAmount >= (RoundN(sChips, n))/2)
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }

                        if (raiseAmount <= (RoundN(sChips, n))/2)
                        {
                            if (raiseAmount > 0)
                            {
                                raiseAmount = RoundN(sChips, n);
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                            else
                            {
                                raiseAmount = amountToCall * 2;
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                        }
                    }
                }
            }
            if (rounds >= 2)
            {
                if (amountToCall > 0)
                {
                    if (amountToCall >= RoundN(sChips, n1 - rnd))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }

                    if (raiseAmount > RoundN(sChips, n - rnd))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }

                    if (!sFTurn)
                    {
                        if (amountToCall >= RoundN(sChips, n - rnd) && amountToCall <= RoundN(sChips, n1 - rnd))
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }

                        if (raiseAmount <= RoundN(sChips, n - rnd) && raiseAmount >= (RoundN(sChips, n - rnd))/2)
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }

                        if (raiseAmount <= (RoundN(sChips, n - rnd))/2)
                        {
                            if (raiseAmount > 0)
                            {
                                raiseAmount = RoundN(sChips, n - rnd);
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                            else
                            {
                                raiseAmount = amountToCall * 2;
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                        }
                    }
                }
                if (amountToCall <= 0)
                {
                    raiseAmount = RoundN(sChips, r - rnd);
                    Raised(ref sChips, ref sTurn, sStatus);
                }
            }

            if (sChips <= 0)
            {
                sFTurn = true;
            }
        }

        void Smooth(ref int botChips, ref bool botTurn, ref bool botFTurn, Label botStatus, int name, int n, int r)
        {
            Random rand = new Random();
            int rnd = rand.Next(1, 3);
            if (amountToCall <= 0)
            {
                Check(ref botTurn, botStatus);
            }
            else
            {
                if (amountToCall >= RoundN(botChips, n))
                {
                    if (botChips > amountToCall)
                    {
                        Call(ref botChips, ref botTurn, botStatus);
                    }
                    else if (botChips <= amountToCall)
                    {
                        raising = false;
                        botTurn = false;
                        botChips = 0;
                        botStatus.Text = "Call " + botChips;
                        textboxPot.Text = (int.Parse(textboxPot.Text) + botChips).ToString();
                    }
                }
                else
                {
                    if (raiseAmount > 0)
                    {
                        if (botChips >= raiseAmount*2)
                        {
                            raiseAmount *= 2;
                            Raised(ref botChips, ref botTurn, botStatus);
                        }
                        else
                        {
                            Call(ref botChips, ref botTurn, botStatus);
                        }
                    }
                    else
                    {
                        raiseAmount = amountToCall * 2;
                        Raised(ref botChips, ref botTurn, botStatus);
                    }
                }
            }

            if (botChips <= 0)
            {
                botFTurn = true;
            }
        }

        #region UI

        private async void timer_Tick(object sender, object e)
        {
            if (progressBarTimer.Value <= 0)
            {
                playerFoldedTurn = true;
                await Turns();
            }

            if (timeTillNextTurn > 0)
            {
                timeTillNextTurn--;
                progressBarTimer.Value = (timeTillNextTurn/6)*100;
            }
        }

        private void Update_Tick(object sender, object e)
        {
            if (playerChips <= 0)
            {
                textboxPlayerChips.Text = "Chips : 0";
            }

            if (firstBotChips <= 0)
            {
                textboxFirstBotChips.Text = "Chips : 0";
            }

            if (secondBotChips <= 0)
            {
                textboxSecondBotChips.Text = "Chips : 0";
            }

            if (thirdBotChips <= 0)
            {
                textbokThirdBotChips.Text = "Chips : 0";
            }

            if (fourthBotChips <= 0)
            {
                textboxFourthBotChips.Text = "Chips : 0";
            }

            if (fifthBotChips <= 0)
            {
                textboxFifthBotChips.Text = "Chips : 0";
            }

            textboxPlayerChips.Text = "Chips : " + playerChips.ToString();
            textboxFirstBotChips.Text = "Chips : " + firstBotChips.ToString();
            textboxSecondBotChips.Text = "Chips : " + secondBotChips.ToString();
            textbokThirdBotChips.Text = "Chips : " + thirdBotChips.ToString();
            textboxFourthBotChips.Text = "Chips : " + fourthBotChips.ToString();
            textboxFifthBotChips.Text = "Chips : " + fifthBotChips.ToString();
            if (playerChips <= 0)
            {
                playerTurn = false;
                playerFoldedTurn = true;
                buttonCall.Enabled = false;
                buttonRaise.Enabled = false;
                buttonFold.Enabled = false;
                buttonCheck.Enabled = false;
            }

            if (up > 0)
            {
                up--;
            }

            if (playerChips >= amountToCall)
            {
                buttonCall.Text = "Call " + amountToCall.ToString();
            }
            else
            {
                buttonCall.Text = "All in";
                buttonRaise.Enabled = false;
            }

            if (amountToCall > 0)
            {
                buttonCheck.Enabled = false;
            }

            if (amountToCall <= 0)
            {
                buttonCheck.Enabled = true;
                buttonCall.Text = "Call";
                buttonCall.Enabled = false;
            }

            if (playerChips <= 0)
            {
                buttonRaise.Enabled = false;
            }

            int parsedValue;

            if (textboxRaise.Text != string.Empty && int.TryParse(textboxRaise.Text, out parsedValue))
            {
                if (playerChips <= int.Parse(textboxRaise.Text))
                {
                    buttonRaise.Text = "All in";
                }
                else
                {
                    buttonRaise.Text = "Raise";
                }
            }

            if (playerChips < amountToCall)
            {
                buttonRaise.Enabled = false;
            }
        }

        private async void bFold_Click(object sender, EventArgs e)
        {
            playerStatus.Text = "Fold";
            playerTurn = false;
            playerFoldedTurn = true;
            await Turns();
        }

        private async void bCheck_Click(object sender, EventArgs e)
        {
            if (amountToCall <= 0)
            {
                playerTurn = false;
                playerStatus.Text = "Check";
            }
            else
            {
                //pStatus.Text = "All in " + Chips;
                buttonCheck.Enabled = false;
            }
            await Turns();
        }

        private async void bCall_Click(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", ref playerType, ref playerPower, playerFoldedTurn);
            if (playerChips >= amountToCall)
            {
                playerChips -= amountToCall;
                textboxPlayerChips.Text = "Chips : " + playerChips.ToString();
                if (textboxPot.Text != string.Empty)
                {
                    textboxPot.Text = (int.Parse(textboxPot.Text) + amountToCall).ToString();
                }

                else
                {
                    textboxPot.Text = amountToCall.ToString();
                }

                playerTurn = false;
                playerStatus.Text = "Call " + amountToCall;
                playerCallAmount = amountToCall;
            }
            else if (playerChips <= amountToCall && amountToCall > 0)
            {
                textboxPot.Text = (int.Parse(textboxPot.Text) + playerChips).ToString();
                playerStatus.Text = "All in " + playerChips;
                playerChips = 0;
                textboxPlayerChips.Text = "Chips : " + playerChips.ToString();
                playerTurn = false;
                buttonFold.Enabled = false;
                playerCallAmount = playerChips;
            }

            await Turns();
        }

        private async void bRaise_Click(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", ref playerType, ref playerPower, playerFoldedTurn);
            int parsedValue;
            if (textboxRaise.Text != string.Empty && int.TryParse(textboxRaise.Text, out parsedValue))
            {
                if (playerChips > amountToCall)
                {
                    if (raiseAmount*2 > int.Parse(textboxRaise.Text))
                    {
                        textboxRaise.Text = (raiseAmount*2).ToString();
                        MessageBox.Show("You must raiseAmount atleast twice as the handStrength raiseAmount !");
                        return;
                    }

                    else
                    {
                        if (playerChips >= int.Parse(textboxRaise.Text))
                        {
                            amountToCall = int.Parse(textboxRaise.Text);
                            raiseAmount = int.Parse(textboxRaise.Text);
                            playerStatus.Text = "Raise " + amountToCall.ToString();
                            textboxPot.Text = (int.Parse(textboxPot.Text) + amountToCall).ToString();
                            buttonCall.Text = "Call";
                            playerChips -= int.Parse(textboxRaise.Text);
                            raising = true;
                            lastOneToAct = 0;
                            playerRaiseAmount = Convert.ToInt32(raiseAmount);
                        }

                        else
                        {
                            amountToCall = playerChips;
                            raiseAmount = playerChips;
                            textboxPot.Text = (int.Parse(textboxPot.Text) + playerChips).ToString();
                            playerStatus.Text = "Raise " + amountToCall.ToString();
                            playerChips = 0;
                            raising = true;
                            lastOneToAct = 0;
                            playerRaiseAmount = Convert.ToInt32(raiseAmount);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("This is a number only field");
                return;
            }

            playerTurn = false;
            await Turns();
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            if (!(textboxAdd.Text == string.Empty))
            {
                playerChips += int.Parse(textboxAdd.Text);
                firstBotChips += int.Parse(textboxAdd.Text);
                secondBotChips += int.Parse(textboxAdd.Text);
                thirdBotChips += int.Parse(textboxAdd.Text);
                fourthBotChips += int.Parse(textboxAdd.Text);
                fifthBotChips += int.Parse(textboxAdd.Text);
            }
           
            textboxPlayerChips.Text = "Chips : " + playerChips.ToString();
        }

        private void bOptions_Click(object sender, EventArgs e)
        {
            textboxBigBlind.Text = bigBlind.ToString();
            textboxSmallBlind.Text = smallBlind.ToString();
            if (textboxBigBlind.Visible == false)
            {
                textboxBigBlind.Visible = true;
                textboxSmallBlind.Visible = true;
                buttonBigBlind.Visible = true;
                buttonSmallBlind.Visible = true;
            }
            else
            {
                textboxBigBlind.Visible = false;
                textboxSmallBlind.Visible = false;
                buttonBigBlind.Visible = false;
                buttonSmallBlind.Visible = false;
            }
        }

        private void bSB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (textboxSmallBlind.Text.Contains(",") || textboxSmallBlind.Text.Contains("."))
            {
                MessageBox.Show("The Small Blind can be only round number !");
                textboxSmallBlind.Text = smallBlind.ToString();
                return;
            }

            if (!int.TryParse(textboxSmallBlind.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                textboxSmallBlind.Text = smallBlind.ToString();
                return;
            }

            if (int.Parse(textboxSmallBlind.Text) > 100000)
            {
                MessageBox.Show("The maximum of the Small Blind is 100 000 $");
                textboxSmallBlind.Text = smallBlind.ToString();
            }

            if (int.Parse(textboxSmallBlind.Text) < 250)
            {
                MessageBox.Show("The minimum of the Small Blind is 250 $");
            }

            if (int.Parse(textboxSmallBlind.Text) >= 250 && int.Parse(textboxSmallBlind.Text) <= 100000)
            {
                smallBlind = int.Parse(textboxSmallBlind.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void bBB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (textboxBigBlind.Text.Contains(",") || textboxBigBlind.Text.Contains("."))
            {
                MessageBox.Show("The Big Blind can be only round number !");
                textboxBigBlind.Text = bigBlind.ToString();
                return;
            }

            if (!int.TryParse(textboxSmallBlind.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                textboxSmallBlind.Text = bigBlind.ToString();
                return;
            }

            if (int.Parse(textboxBigBlind.Text) > 200000)
            {
                MessageBox.Show("The maximum of the Big Blind is 200 000");
                textboxBigBlind.Text = bigBlind.ToString();
            }

            if (int.Parse(textboxBigBlind.Text) < 500)
            {
                MessageBox.Show("The minimum of the Big Blind is 500 $");
            }

            if (int.Parse(textboxBigBlind.Text) >= 500 && int.Parse(textboxBigBlind.Text) <= 200000)
            {
                bigBlind = int.Parse(textboxBigBlind.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void Layout_Change(object sender, LayoutEventArgs e)
        {
            width = this.Width;
            height = this.Height;
        }

        #endregion
    }
}