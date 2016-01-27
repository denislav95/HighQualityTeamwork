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

        private readonly List<bool?> bools = new List<bool?>();
        private readonly List<Type> win = new List<Type>();
        private readonly List<string> checkWinners = new List<string>();
        private readonly List<int> ints = new List<int>();

        private readonly Panel playerPanel = new Panel();
        private readonly Panel firstBotPanel = new Panel();
        private readonly Panel secondBotPanel = new Panel();
        private readonly Panel thirdBotPanel = new Panel();
        private readonly Panel fourthBotPanel = new Panel();
        private readonly Panel fifthBotPanel = new Panel();

        private readonly int[] reserve = new int[17];

        private readonly Image[] deck = new Image[52];

        private readonly PictureBox[] holder = new PictureBox[52];

        private readonly Timer timer = new Timer();

        private readonly Timer updates = new Timer();

        private int call = 500;

        private int foldedPlayers = 5;

        private int chips = 10000;
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

        private double raise;

        private double playerType = -1;
        private double firstBotType = -1;
        private double secondBotType = -1;
        private double thirdBotType = -1;
        private double fourthBotType = -1;
        private double fifthBotType = -1;

        private bool firstBotTurn;
        private bool secondBotTurn;
        private bool thirdBotTurn;
        private bool fourthBotTurn;
        private bool fifthBotTurn;

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

        private bool intsadded;

        private bool changed;

        private int playerCall;
        private int firstBotCall;
        private int secondBotCall;
        private int thirdBotCall;
        private int fourthBotCall;
        private int fifthBotCall;

        private int playerRaise;
        private int firstBotRaise;
        private int secondBotRaise;
        private int thirdBotRaise;
        private int fourthBotRaise;
        private int fifthBotRaise;

        private int height;
        private int width;

        private int winners;

        private int flop = 1;

        private int turn = 2;

        private int river = 3;

        private int end = 4;

        private int maxLeft = 6;

        private int last = 126;

        private int raisedTurn = 1;

        private bool foldedPlayerTurn;

        private bool playerTurn = true;

        private bool restart;

        private bool raising;

        private Type sorted;

        private string[] imageLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
        private string[] ImgLocation =
            {
                   "Assets\\Cards\\33.png", "Assets\\Cards\\22.png",
                    "Assets\\Cards\\29.png", "Assets\\Cards\\21.png",
                    "Assets\\Cards\\36.png", "Assets\\Cards\\17.png",
                    "Assets\\Cards\\40.png", "Assets\\Cards\\16.png",
                    "Assets\\Cards\\5.png", "Assets\\Cards\\47.png",
                    "Assets\\Cards\\37.png", "Assets\\Cards\\13.png",
                    "Assets\\Cards\\12.png", "Assets\\Cards\\18.png",
                    "Assets\\Cards\\8.png", "Assets\\Cards\\27.png",
                    "Assets\\Cards\\15.png"
        };

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
           // bools.Add(PFturn); bools.Add(B1Fturn); bools.Add(B2Fturn); bools.Add(B3Fturn); bools.Add(B4Fturn); bools.Add(B5Fturn);
            this.call = bigBlind;
            
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            
            this.updates.Start();
            
            this.InitializeComponent();
            
            this.width = Width;
            this.height = Height;
             
             Shuffle();

            this.textboxPot.Enabled = false;
            this.textboxChips.Enabled = false;
            
            this.textboxFirstBotChips.Enabled = false;
            this.textboxSecondBotChips.Enabled = false;
            this.textbokThirdBotChips.Enabled = false;
            this.textboxFourthBotChips.Enabled = false;
            this.textboxFifthBotChips.Enabled = false;
            
            this.textboxChips.Text = @"Chips : " + this.chips;
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
           this.bools.Add(this.foldedPlayerTurn);
           this.bools.Add(this.firstBotFoldedTurn);
           this.bools.Add(this.secodBotFoldedTurn);
           this.bools.Add(this.thirdBotFoldedTurn);
           this.bools.Add(this.fourthBotFoldedTurn);
           this.bools.Add(this.fifthBotFoldedTurn);

           this.buttonCall.Enabled = false;
           this.buttonRaise.Enabled = false;
           this.buttonFold.Enabled = false;
           this.buttonCheck.Enabled = false;

           this.MaximizeBox = false;
           this.MinimizeBox = false;

            var check = false;

            var backImage = new Bitmap("Assets\\Back\\Back.png");

            var horizontal = 580;
            var vertical = 480;

            var random = new Random();

            for (i = imageLocation.Length; i > 0; i--)
            {
                var position = random.Next(i);

                var image = imageLocation[position];

                imageLocation[position] = imageLocation[i - 1];
                imageLocation[i - 1] = image;
            }

            for (i = 0; i < 17; i++)
            {
                deck[i] = Image.FromFile(imageLocation[i]);

                var charsToRemove = new string[] {"Assets\\Cards\\", ".png"};

                foreach (var c in charsToRemove)
                {
                    imageLocation[i] = imageLocation[i].Replace(c, string.Empty);
                }

                reserve[i] = int.Parse(imageLocation[i]) - 1;

                holder[i] = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Height = 130,
                    Width = 80
                };

                Controls.Add(holder[i]);
                holder[i].Name = "pb" + i;

                await Task.Delay(200);

                #region Throwing Cards

                if (i < 2)
                {
                    if (holder[0].Tag != null)
                    {
                        holder[1].Tag = reserve[1];
                    }
                    holder[0].Tag = reserve[0];
                    holder[i].Image = deck[i];
                    holder[i].Anchor = AnchorStyles.Bottom;
                   ////holder[i].Dock = DockStyle.Top;
                    holder[i].Location = new Point(horizontal, vertical);

                    horizontal += holder[i].Width;

                    Controls.Add(playerPanel);

                    playerPanel.Location = new Point(holder[0].Left - 10, holder[0].Top - 10);
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
                        if (holder[2].Tag != null)
                        {
                            holder[3].Tag = reserve[3];
                        }

                        holder[2].Tag = reserve[2];

                        if (!check)
                        {
                            horizontal = 15;
                            vertical = 420;
                        }

                        check = true;

                        holder[i].Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
                        holder[i].Image = backImage;
                       ////holder[i].Image = deck[i];
                        holder[i].Location = new Point(horizontal, vertical);

                        horizontal += holder[i].Width;

                        holder[i].Visible = true;

                        Controls.Add(firstBotPanel);

                        firstBotPanel.Location = new Point(holder[2].Left - 10, holder[2].Top - 10);
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
                        if (holder[4].Tag != null)
                        {
                            holder[5].Tag = reserve[5];
                        }

                        holder[4].Tag = reserve[4];

                        if (!check)
                        {
                            horizontal = 75;
                            vertical = 65;
                        }

                        check = true;

                        holder[i].Anchor = AnchorStyles.Top | AnchorStyles.Left;
                        holder[i].Image = backImage;
                       ////holder[i].Image = deck[i];
                        holder[i].Location = new Point(horizontal, vertical);

                        horizontal += holder[i].Width;

                        holder[i].Visible = true;

                        Controls.Add(secondBotPanel);

                        secondBotPanel.Location = new Point(holder[4].Left - 10, holder[4].Top - 10);
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
                        if (holder[6].Tag != null)
                        {
                            holder[7].Tag = reserve[7];
                        }

                        holder[6].Tag = reserve[6];

                        if (!check)
                        {
                            horizontal = 590;
                            vertical = 25;
                        }

                        check = true;

                        holder[i].Anchor = AnchorStyles.Top;
                        holder[i].Image = backImage;
                       ////holder[i].Image = deck[i];
                        holder[i].Location = new Point(horizontal, vertical);

                        horizontal += holder[i].Width;

                        holder[i].Visible = true;

                        Controls.Add(thirdBotPanel);

                        thirdBotPanel.Location = new Point(holder[6].Left - 10, holder[6].Top - 10);
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
                        if (holder[8].Tag != null)
                        {
                            holder[9].Tag = reserve[9];
                        }

                        holder[8].Tag = reserve[8];
                        if (!check)
                        {
                            horizontal = 1115;
                            vertical = 65;
                        }

                        check = true;

                        holder[i].Anchor = AnchorStyles.Top | AnchorStyles.Right;
                        holder[i].Image = backImage;
                        ////holder[i].Image = deck[i];
                        holder[i].Location = new Point(horizontal, vertical);

                        horizontal += holder[i].Width;

                        holder[i].Visible = true;

                        Controls.Add(fourthBotPanel);

                        fourthBotPanel.Location = new Point(holder[8].Left - 10, holder[8].Top - 10);
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
                        if (holder[10].Tag != null)
                        {
                            holder[11].Tag = reserve[11];
                        }

                        holder[10].Tag = reserve[10];

                        if (!check)
                        {
                            horizontal = 1160;
                            vertical = 420;
                        }

                        check = true;

                        holder[i].Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
                        holder[i].Image = backImage;
                        ////holder[i].Image = deck[i];
                        holder[i].Location = new Point(horizontal, vertical);

                        horizontal += holder[i].Width;

                        holder[i].Visible = true;

                        Controls.Add(fifthBotPanel);

                        fifthBotPanel.Location = new Point(holder[10].Left - 10, holder[10].Top - 10);
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
                    holder[12].Tag = reserve[12];

                    if (i > 12)
                    {
                        holder[13].Tag = reserve[13];
                    }

                    if (i > 13)
                    {
                        holder[14].Tag = reserve[14];
                    }

                    if (i > 14)
                    {
                        holder[15].Tag = reserve[15];
                    }

                    if (i > 15)
                    {
                        holder[16].Tag = reserve[16];
                    }

                    if (!check)
                    {
                        horizontal = 410;
                        vertical = 265;
                    }

                    check = true;

                    if (holder[i] != null)
                    {
                        holder[i].Anchor = AnchorStyles.None;
                        holder[i].Image = backImage;
                        ////holder[i].Image = deck[i];

                        holder[i].Location = new Point(horizontal, vertical);

                        horizontal += 110;
                    }
                }

                #endregion

                if (firstBotChips <= 0)
                {
                    firstBotFoldedTurn = true;

                    holder[2].Visible = false;
                    holder[3].Visible = false;
                }
                else
                {
                    firstBotFoldedTurn = false;

                    if (i == 3)
                    {
                        if (holder[3] != null)
                        {
                            holder[2].Visible = true;
                            holder[3].Visible = true;
                        }
                    }
                }

                if (secondBotChips <= 0)
                {
                    secodBotFoldedTurn = true;

                    holder[4].Visible = false;
                    holder[5].Visible = false;
                }
                else
                {
                    secodBotFoldedTurn = false;

                    if (i == 5)
                    {
                        if (holder[5] != null)
                        {
                            holder[4].Visible = true;
                            holder[5].Visible = true;
                        }
                    }
                }

                if (thirdBotChips <= 0)
                {
                    thirdBotFoldedTurn = true;

                    holder[6].Visible = false;
                    holder[7].Visible = false;
                }
                else
                {
                    thirdBotFoldedTurn = false;

                    if (i == 7)
                    {
                        if (holder[7] != null)
                        {
                            holder[6].Visible = true;
                            holder[7].Visible = true;
                        }
                    }
                }

                if (fourthBotChips <= 0)
                {
                    fourthBotFoldedTurn = true;

                    holder[8].Visible = false;
                    holder[9].Visible = false;
                }
                else
                {
                    fourthBotFoldedTurn = false;

                    if (i == 9)
                    {
                        if (holder[9] != null)
                        {
                            holder[8].Visible = true;
                            holder[9].Visible = true;
                        }
                    }
                }

                if (fifthBotChips <= 0)
                {
                    fifthBotFoldedTurn = true;

                    holder[10].Visible = false;
                    holder[11].Visible = false;
                }
                else
                {
                    fifthBotFoldedTurn = false;

                    if (i == 11)
                    {
                        if (holder[11] != null)
                        {
                            holder[10].Visible = true;
                            holder[11].Visible = true;
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

            if (!foldedPlayerTurn)
            {
                if (playerTurn)
                {
                    FixCall(playerStatus, ref playerCall, ref playerRaise, 1);

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

                    FixCall(playerStatus, ref playerCall, ref playerRaise, 2);
                }
            }

            if (foldedPlayerTurn || !playerTurn)
            {
                await AllIn();

                if (foldedPlayerTurn && !playerFolded)
                {
                    if (buttonCall.Text.Contains("All in") == false 
                        || buttonRaise.Text.Contains("All in") == false)
                    {
                        bools.RemoveAt(0);
                        bools.Insert(0, null);

                        maxLeft--;

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
                        FixCall(firstBotStatus, ref firstBotCall, ref firstBotRaise, 1);
                        FixCall(firstBotStatus, ref firstBotCall, ref firstBotRaise, 2);

                        Rules(2, 3, "Bot 1", ref firstBotType, ref firstBotPower, firstBotFoldedTurn);                     

                        AI(2, 3, ref firstBotChips, ref firstBotTurn, ref firstBotFoldedTurn, firstBotStatus, 0, firstBotPower, firstBotType);

                        turnCount++;

                        last = 1;

                        firstBotTurn = false;
                        secondBotTurn = true;
                    }
                }

                if (firstBotFoldedTurn && !firstBotFolded)
                {
                    bools.RemoveAt(1);
                    bools.Insert(1, null);

                    maxLeft--;

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
                        FixCall(secondBotStatus, ref secondBotCall, ref secondBotRaise, 1);
                        FixCall(secondBotStatus, ref secondBotCall, ref secondBotRaise, 2);

                        Rules(4, 5, "Bot 2", ref secondBotType, ref secondBotPower, secodBotFoldedTurn);                    

                        AI(4, 5, ref secondBotChips, ref secondBotTurn, ref secodBotFoldedTurn, secondBotStatus, 1,secondBotPower, secondBotType);

                        turnCount++;
                        last = 2;
                        secondBotTurn = false;
                        thirdBotTurn = true;
                    }
                }

                if (secodBotFoldedTurn && !secondBotFolded)
                {
                    bools.RemoveAt(2);
                    bools.Insert(2, null);
                    maxLeft--;
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
                        FixCall(thirdBotStatus, ref thirdBotCall, ref thirdBotRaise, 1);

                        FixCall(thirdBotStatus, ref thirdBotCall, ref thirdBotRaise, 2);

                        Rules(6, 7, "Bot 3", ref thirdBotType, ref thirdBotPower, thirdBotFoldedTurn);

                        AI(6, 7, ref thirdBotChips, ref thirdBotTurn, ref thirdBotFoldedTurn, thirdBotStatus, 2, thirdBotPower, thirdBotType);

                        turnCount++;
                        last = 3;
                        thirdBotTurn = false;
                        fourthBotTurn = true;
                    }
                }

                if (thirdBotFoldedTurn && !thirdBotFolded)
                {
                    bools.RemoveAt(3);
                    bools.Insert(3, null);
                    maxLeft--;
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
                        FixCall( fourthBotStatus, ref fourthBotCall, ref fourthBotRaise, 1);
                        FixCall(fourthBotStatus, ref fourthBotCall, ref fourthBotRaise, 2);

                        Rules(8, 9, "Bot 4", ref fourthBotType, ref fourthBotPower, fourthBotFoldedTurn);

                        AI(8, 9, ref fourthBotChips, ref fourthBotTurn, ref fourthBotFoldedTurn, fourthBotStatus, 3, fourthBotPower, fourthBotType);

                        turnCount++;
                        last = 4;
                        fourthBotTurn = false;
                        fifthBotTurn = true;
                    }
                }

                if (fourthBotFoldedTurn && !fourthBotFolded)
                {
                    bools.RemoveAt(4);
                    bools.Insert(4, null);

                    maxLeft--;

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
                        FixCall(fifthBotStatus, ref fifthBotCall, ref fifthBotRaise, 1);
                        FixCall(fifthBotStatus, ref fifthBotCall, ref fifthBotRaise, 2);

                        Rules(10, 11, "Bot 5", ref fifthBotType, ref fifthBotPower, fifthBotFoldedTurn);

                        AI(10, 11, ref fifthBotChips, ref fifthBotTurn, ref fifthBotFoldedTurn, fifthBotStatus, 4, fifthBotPower, fifthBotType);

                        turnCount++;
                        last = 5;
                        fifthBotTurn = false;
                    }
                }

                if (fifthBotFoldedTurn && !fifthBotFolded)
                {
                    bools.RemoveAt(5);
                    bools.Insert(5, null);

                    maxLeft--;

                    fifthBotFolded = true;
                }

                if (fifthBotFoldedTurn || !fifthBotTurn)
                {
                    await CheckRaise(5, 5);

                    playerTurn = true;
                }

                if (foldedPlayerTurn && !playerFolded)
                {
                    if (buttonCall.Text.Contains("All in") == false 
                        || buttonRaise.Text.Contains("All in") == false)
                    {
                        bools.RemoveAt(0);
                        bools.Insert(0, null);

                        maxLeft--;

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

        private void Rules(int c1, int c2, string currentBot, ref double current, ref double currentBotPower, bool foldedTurn)
        {
            if (!foldedTurn || c1 == 0 && c2 == 1 && playerStatus.Text.Contains("Fold") == false)
            {
                #region Variables

                var done = false;

                var vf = false;

                var cardsOnTable = new int[5];
                var playerCards = new int[7];

                playerCards[0] = reserve[c1];
                playerCards[1] = reserve[c2];

                cardsOnTable[0] = playerCards[2] = reserve[12];
                cardsOnTable[1] = playerCards[3] = reserve[13];
                cardsOnTable[2] = playerCards[4] = reserve[14];
                cardsOnTable[3] = playerCards[5] = reserve[15];
                cardsOnTable[4] = playerCards[6] = reserve[16];

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
                    if (reserve[i] == int.Parse(holder[c1].Tag.ToString()) 
                        && reserve[i + 1] == int.Parse(holder[c2].Tag.ToString()))
                    {
                        //Pair from Hand current = 1

                        rPairFromHand(ref current, ref currentBotPower);

                        #region Pair or Two Pair from Table current = 2 || 0

                        RulePairTwoPair(ref current, ref currentBotPower);

                        #endregion

                        #region Two Pair current = 2

                        RuleTwoPair(ref current, ref currentBotPower);

                        #endregion

                        #region Three of a kind current = 3

                        RuleThreeOfAKind(ref current, ref currentBotPower, playerCards);

                        #endregion

                        #region Straight current = 4

                        RuleStraight(ref current, ref currentBotPower, playerCards);

                        #endregion

                        #region Flush current = 5 || 5.5

                        RuleFlush(ref current, ref currentBotPower, ref vf, cardsOnTable);

                        #endregion

                        #region Full House current = 6

                        RuleFullHouse(ref current, ref currentBotPower, ref done, playerCards);

                        #endregion

                        #region Four of a Kind current = 7

                        RuleFourOfAKind(ref current, ref currentBotPower, playerCards);

                        #endregion

                        #region Straight Flush current = 8 || 9

                        RuleStraightFlush(ref current, ref currentBotPower, clubCards, diamondCards, heartCards, spadeCards);

                        #endregion

                        #region High Card current = -1

                        rHighCard(ref current, ref currentBotPower);

                        #endregion
                    }
                }
            }
        }

        private void RuleStraightFlush(
            ref double current,
            ref double power,
            IReadOnlyList<int> clubCards,
            IReadOnlyList<int> diamondCards,
            IReadOnlyList<int> heartCards,
            IReadOnlyList<int> spadeCards)
        {
            if (current >= -1)
            {
                if (clubCards.Count >= 5)
                {
                    if (clubCards[0] + 4 == clubCards[4])
                    {
                        current = 8;

                        var max = clubCards.Concat(new[] {int.MinValue}).Max();

                        power = max / 4 + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 8
                        });

                        sorted = 
                            win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (clubCards[0] == 0 
                        && clubCards[1] == 9 
                        && clubCards[2] == 10 
                        && clubCards[3] == 11 
                        && clubCards[0] + 12 == clubCards[4])
                    {
                        current = 9;

                        var max = clubCards.Concat(new[] {int.MinValue}).Max();

                        power = max / 4 + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 9
                        });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (diamondCards.Count >= 5)
                {
                    if (diamondCards[0] + 4 == diamondCards[4])
                    {
                        current = 8;

                        var max = diamondCards.Concat(new[] {int.MinValue}).Max();

                        power = max / 4 + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 8
                        });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (diamondCards[0] == 0 
                        && diamondCards[1] == 9 
                        && diamondCards[2] == 10 
                        && diamondCards[3] == 11 
                        && diamondCards[0] + 12 == diamondCards[4])
                    {
                        current = 9;

                        var max = diamondCards.Concat(new[] {int.MinValue}).Max();

                        power = max / 4 + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 9
                        });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (heartCards.Count >= 5)
                {
                    if (heartCards[0] + 4 == heartCards[4])
                    {
                        current = 8;

                        var max = heartCards.Concat(new[] {int.MinValue}).Max();

                        power = max / 4 + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 8
                        });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (heartCards[0] == 0 
                        && heartCards[1] == 9 
                        && heartCards[2] == 10 
                        && heartCards[3] == 11 
                        && heartCards[0] + 12 == heartCards[4])
                    {
                        current = 9;

                        var max = heartCards.Concat(new[] {int.MinValue}).Max();

                        power = max / 4 + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 9
                        });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (spadeCards.Count >= 5)
                {
                    if (spadeCards[0] + 4 == spadeCards[4])
                    {
                        current = 8;

                        var max = spadeCards.Concat(new[] {int.MinValue}).Max();

                        power = max / 4 + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 8
                        });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (spadeCards[0] == 0 
                        && spadeCards[1] == 9 
                        && spadeCards[2] == 10 
                        && spadeCards[3] == 11 
                        && spadeCards[0] + 12 == spadeCards[4])
                    {
                        current = 9;

                        var max = spadeCards.Concat(new[] {int.MinValue}).Max();

                        power = max / 4 + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 9
                        });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }
            }
        }

        private void RuleFourOfAKind(
            ref double current,
            ref double power,
            IReadOnlyList<int> straight)
        {
            if (current >= -1)
            {
                for (var j = 0; j <= 3; j++)
                {
                    if (straight[j] / 4 == straight[j + 1] / 4 
                        && straight[j] / 4 == straight[j + 2] / 4 
                        && straight[j] / 4 == straight[j + 3] / 4)
                    {
                        current = 7;

                        power = (straight[j] / 4) * 4 + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 7
                        });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (straight[j] / 4 == 0 
                        && straight[j + 1] / 4 == 0 
                        && straight[j + 2] / 4 == 0 
                        && straight[j + 3] / 4 == 0)
                    {
                        current = 7;

                        power = 13 * 4 + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 7
                        });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }
            }
        }

        private void RuleFullHouse(
            ref double current,
            ref double power,
            ref bool done,
            int[] straight)
        {
            if (current >= -1)
            {
                type = power;

                for (var j = 0; j <= 12; j++)
                {
                    var fullHouse = straight.Where(o => o / 4 == j).ToArray();

                    if (fullHouse.Length == 3 || done)
                    {
                        if (fullHouse.Length == 2)
                        {
                            if (fullHouse.Max() / 4 == 0)
                            {
                                current = 6;

                                power = 13 * 2 + current * 100;

                                win.Add(new Type()
                                {
                                    Power = power,
                                    Current = 6
                                });

                                sorted = win
                                    .OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();

                                break;
                            }

                            if (fullHouse.Max() / 4 > 0)
                            {
                                current = 6;

                                power = fullHouse.Max() / 4 * 2 + current * 100;

                                win.Add(new Type()
                                {
                                    Power = power,
                                    Current = 6
                                });

                                sorted = win
                                    .OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();

                                break;
                            }
                        }

                        if (!done)
                        {
                            if (fullHouse.Max() / 4 == 0)
                            {
                                power = 13;

                                done = true;

                                j = -1;
                            }
                            else
                            {
                                power = fullHouse.Max()/4;

                                done = true;

                                j = -1;
                            }
                        }
                    }
                }

                if (current != 6)
                {
                    power = type;
                }
            }
        }

        private void RuleFlush(
            ref double current,
            ref double power,
            ref bool vf,
            int[] straight1)
        {
            if (current >= -1)
            {
                var flushOfClubs = straight1.Where(o => o % 4 == 0).ToArray();
                var flushOfDiamonds = straight1.Where(o => o % 4 == 1).ToArray();
                var flushOfHearts = straight1.Where(o => o % 4 == 2).ToArray();
                var flushOfSpades = straight1.Where(o => o % 4 == 3).ToArray();

                if (flushOfClubs.Length == 3 || flushOfClubs.Length == 4)
                {
                    if (reserve[i] % 4 == reserve[i + 1] % 4 
                        && reserve[i] % 4 == flushOfClubs[0] % 4)
                    {
                        if (reserve[i] / 4 > flushOfClubs.Max() / 4)
                        {
                            current = 5;

                            power = reserve[i] + current*100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }

                        if (reserve[i + 1] / 4 > flushOfClubs.Max() / 4)
                        {
                            current = 5;

                            power = reserve[i + 1] + current*100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else if (reserve[i] / 4 < flushOfClubs.Max() / 4 
                            && reserve[i + 1] / 4 < flushOfClubs.Max() / 4)
                        {
                            current = 5;

                            power = flushOfClubs.Max() + current*100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }
                }
                //different cards in hand
                if (flushOfClubs.Length == 4) 
                {
                    if (reserve[i] % 4 != reserve[i + 1] % 4 
                        && reserve[i] % 4 == flushOfClubs[0] % 4)
                    {
                        if (reserve[i] / 4 > flushOfClubs.Max() / 4)
                        {
                            current = 5;

                            power = reserve[i] + current*100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            current = 5;

                            power = flushOfClubs.Max() + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }

                    if (reserve[i + 1] % 4 != reserve[i] % 4 
                        && reserve[i + 1] % 4 == flushOfClubs[0] % 4)
                    {
                        if (reserve[i + 1] / 4 > flushOfClubs.Max() / 4)
                        {
                            current = 5;

                            power = reserve[i + 1] + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            current = 5;

                            power = flushOfClubs.Max() + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }
                }

                if (flushOfClubs.Length == 5)
                {
                    if (reserve[i] % 4 == flushOfClubs[0] % 4 
                        && reserve[i] / 4 > flushOfClubs.Min() / 4)
                    {
                        current = 5;

                        power = reserve[i] + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 5
                        });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }

                    if (reserve[i + 1] % 4 == flushOfClubs[0] % 4 
                        && reserve[i + 1] / 4 > flushOfClubs.Min() / 4)
                    {
                        current = 5;

                        power = reserve[i + 1] + current * 100;

                        win.Add(new Type()
                        {
                            Power = power, 
                            Current = 5
                        });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }
                    else if (reserve[i] / 4 < flushOfClubs.Min() / 4 
                        && reserve[i + 1] / 4 < flushOfClubs.Min())
                    {
                        current = 5;

                        power = flushOfClubs.Max() + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 5
                        });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }
                }

                if (flushOfDiamonds.Length == 3 || flushOfDiamonds.Length == 4)
                {
                    if (reserve[i] % 4 == reserve[i + 1] % 4 
                        && reserve[i] % 4 == flushOfDiamonds[0] % 4)
                    {
                        if (reserve[i] / 4 > flushOfDiamonds.Max() / 4)
                        {
                            current = 5;

                            power = reserve[i] + current*100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }

                        if (reserve[i + 1] / 4 > flushOfDiamonds.Max() / 4)
                        {
                            current = 5;

                            power = reserve[i + 1] + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else if (reserve[i] / 4 < flushOfDiamonds.Max() / 4 
                            && reserve[i + 1] / 4 < flushOfDiamonds.Max() / 4)
                        {
                            current = 5;

                            power = flushOfDiamonds.Max() + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }
                }
                //different cards in hand
                if (flushOfDiamonds.Length == 4) 
                {
                    if (reserve[i] % 4 != reserve[i + 1] % 4 
                        && reserve[i] % 4 == flushOfDiamonds[0] % 4)
                    {
                        if (reserve[i] / 4 > flushOfDiamonds.Max() / 4)
                        {
                            current = 5;

                            power = reserve[i] + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            current = 5;

                            power = flushOfDiamonds.Max() + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }

                    if (reserve[i + 1] % 4 != reserve[i] % 4 
                        && reserve[i + 1] % 4 == flushOfDiamonds[0] % 4)
                    {
                        if (reserve[i + 1] / 4 > flushOfDiamonds.Max() / 4)
                        {
                            current = 5;

                            power = reserve[i + 1] + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            current = 5;

                            power = flushOfDiamonds.Max() + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }
                }

                if (flushOfDiamonds.Length == 5)
                {
                    if (reserve[i] % 4 == flushOfDiamonds[0] % 4 
                        && reserve[i] / 4 > flushOfDiamonds.Min() / 4)
                    {
                        current = 5;

                        power = reserve[i] + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 5
                        });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }

                    if (reserve[i + 1] % 4 == flushOfDiamonds[0] % 4 
                        && reserve[i + 1] / 4 > flushOfDiamonds.Min() / 4)
                    {
                        current = 5;

                        power = reserve[i + 1] + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 5
                        });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }
                    else if (reserve[i] / 4 < flushOfDiamonds.Min() / 4 
                        && reserve[i + 1] / 4 < flushOfDiamonds.Min())
                    {
                        current = 5;

                        power = flushOfDiamonds.Max() + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 5
                        });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }
                }

                if (flushOfHearts.Length == 3 || flushOfHearts.Length == 4)
                {
                    if (reserve[i] % 4 == reserve[i + 1] % 4 
                        && reserve[i] % 4 == flushOfHearts[0] % 4)
                    {
                        if (reserve[i] / 4 > flushOfHearts.Max() / 4)
                        {
                            current = 5;

                            power = reserve[i] + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }

                        if (reserve[i + 1] / 4 > flushOfHearts.Max() / 4)
                        {
                            current = 5;

                            power = reserve[i + 1] + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else if (reserve[i] / 4 < flushOfHearts.Max() / 4 
                            && reserve[i + 1] / 4 < flushOfHearts.Max() / 4)
                        {
                            current = 5;

                            power = flushOfHearts.Max() + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }
                }
                //different cards in hand
                if (flushOfHearts.Length == 4) 
                {
                    if (reserve[i] % 4 != reserve[i + 1] % 4 
                        && reserve[i] % 4 == flushOfHearts[0] % 4)
                    {
                        if (reserve[i] / 4 > flushOfHearts.Max() / 4)
                        {
                            current = 5;

                            power = reserve[i] + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            current = 5;

                            power = flushOfHearts.Max() + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = 
                                win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }

                    if (reserve[i + 1] % 4 != reserve[i] % 4 
                        && reserve[i + 1] % 4 == flushOfHearts[0] % 4)
                    {
                        if (reserve[i + 1] / 4 > flushOfHearts.Max() / 4)
                        {
                            current = 5;

                            power = reserve[i + 1] + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            current = 5;

                            power = flushOfHearts.Max() + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }
                }

                if (flushOfHearts.Length == 5)
                {
                    if (reserve[i] % 4 == flushOfHearts[0] % 4 
                        && reserve[i] / 4 > flushOfHearts.Min() / 4)
                    {
                        current = 5;

                        power = reserve[i] + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 5
                        });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }

                    if (reserve[i + 1] % 4 == flushOfHearts[0] % 4 
                        && reserve[i + 1] / 4 > flushOfHearts.Min() / 4)
                    {
                        current = 5;

                        power = reserve[i + 1] + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 5
                        });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }
                    else if (reserve[i] / 4 < flushOfHearts.Min() / 4 
                        && reserve[i + 1] / 4 < flushOfHearts.Min())
                    {
                        current = 5;

                        power = flushOfHearts.Max() + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 5
                        });

                        sorted = win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }
                }

                if (flushOfSpades.Length == 3 || flushOfSpades.Length == 4)
                {
                    if (reserve[i] % 4 == reserve[i + 1] % 4 
                        && reserve[i] % 4 == flushOfSpades[0] % 4)
                    {
                        if (reserve[i] / 4 > flushOfSpades.Max() / 4)
                        {
                            current = 5;

                            power = reserve[i] + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }

                        if (reserve[i + 1] / 4 > flushOfSpades.Max() / 4)
                        {
                            current = 5;

                            power = reserve[i + 1] + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else if (reserve[i] / 4 < flushOfSpades.Max() / 4 
                            && reserve[i + 1] / 4 < flushOfSpades.Max() / 4)
                        {
                            current = 5;

                            power = flushOfSpades.Max() + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }
                }
                //different cards in hand
                if (flushOfSpades.Length == 4) 
                {
                    if (reserve[i] % 4 != reserve[i + 1] % 4 
                        && reserve[i] % 4 == flushOfSpades[0] % 4)
                    {
                        if (reserve[i] / 4 > flushOfSpades.Max() / 4)
                        {
                            current = 5;

                            power = reserve[i] + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            current = 5;

                            power = flushOfSpades.Max() + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }

                    if (reserve[i + 1] % 4 != reserve[i] % 4 
                        && reserve[i + 1] % 4 == flushOfSpades[0] % 4)
                    {
                        if (reserve[i + 1] / 4 > flushOfSpades.Max() / 4)
                        {
                            current = 5;

                            power = reserve[i + 1] + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = 
                                win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            current = 5;

                            power = flushOfSpades.Max() + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 5
                            });

                            sorted = 
                                win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }
                }

                if (flushOfSpades.Length == 5)
                {
                    if (reserve[i] % 4 == flushOfSpades[0] % 4 
                        && reserve[i] / 4 > flushOfSpades.Min() / 4)
                    {
                        current = 5;

                        power = reserve[i] + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 5
                        });

                        sorted = 
                            win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }

                    if (reserve[i + 1] % 4 == flushOfSpades[0] % 4 
                        && reserve[i + 1] / 4 > flushOfSpades.Min() / 4)
                    {
                        current = 5;

                        power = reserve[i + 1] + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 5
                        });

                        sorted = 
                            win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }
                    else if (reserve[i] / 4 < flushOfSpades.Min() / 4 
                        && reserve[i + 1] / 4 < flushOfSpades.Min())
                    {
                        current = 5;

                        power = flushOfSpades.Max() + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 5
                        });

                        sorted = 
                            win.
                            OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }
                }
                if (flushOfClubs.Length > 0)
                {
                    if (reserve[i] / 4 == 0 
                        && reserve[i] % 4 == flushOfClubs[0] % 4 
                        && vf 
                        && flushOfClubs.Length > 0)
                    {
                        current = 5.5;

                        power = 13 + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 5.5
                        });

                        sorted = 
                            win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (reserve[i + 1] / 4 == 0 
                        && reserve[i + 1] % 4 == flushOfClubs[0] % 4 
                        && vf 
                        && flushOfClubs.Length > 0)
                    {
                        current = 5.5;

                        power = 13 + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 5.5
                        });

                        sorted = 
                            win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (flushOfDiamonds.Length > 0)
                {
                    if (reserve[i] / 4 == 0 
                        && reserve[i] % 4 == flushOfDiamonds[0] % 4 
                        && vf 
                        && flushOfDiamonds.Length > 0)
                    {
                        current = 5.5;

                        power = 13 + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 5.5
                        });

                        sorted = 
                            win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (reserve[i + 1] / 4 == 0 
                        && reserve[i + 1] % 4 == flushOfDiamonds[0] % 4 
                        && vf 
                        && flushOfDiamonds.Length > 0)
                    {
                        current = 5.5;

                        power = 13 + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 5.5
                        });

                        sorted = 
                            win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (flushOfHearts.Length > 0)
                {
                    if (reserve[i] / 4 == 0 
                        && reserve[i] % 4 == flushOfHearts[0] % 4 
                        && vf 
                        && flushOfHearts.Length > 0)
                    {
                        current = 5.5;

                        power = 13 + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 5.5
                        });

                        sorted = 
                            win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (reserve[i + 1] / 4 == 0 
                        && reserve[i + 1] % 4 == flushOfHearts[0] % 4 
                        && vf 
                        && flushOfHearts.Length > 0)
                    {
                        current = 5.5;

                        power = 13 + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 5.5
                        });

                        sorted =
                            win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (flushOfSpades.Length > 0)
                {
                    if (reserve[i] / 4 == 0 
                        && reserve[i] % 4 == flushOfSpades[0] % 4 
                        && vf 
                        && flushOfSpades.Length > 0)
                    {
                        current = 5.5;

                        power = 13 + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 5.5
                        });

                        sorted = 
                            win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (reserve[i + 1] / 4 == 0 
                        && reserve[i + 1] % 4 == flushOfSpades[0] % 4 
                        && vf)
                    {
                        current = 5.5;

                        power = 13 + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 5.5
                        });

                        sorted = 
                            win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }
            }
        }

        private void RuleStraight(ref double current, ref double power, int[] straight)
        {
            if (current >= -1)
            {
                var openEnded = straight.Select(o => o / 4).Distinct().ToArray();
                for (var j = 0; j < openEnded.Length - 4; j++)
                {
                    if (openEnded[j] + 4 == openEnded[j + 4])
                    {
                        if (openEnded.Max() - 4 == openEnded[j])
                        {
                            current = 4;

                            power = openEnded.Max() + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 4
                            });

                            sorted = 
                                win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();
                        }
                        else
                        {
                            current = 4;

                            power = openEnded[j + 4] + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 4
                            });

                            sorted = 
                                win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();
                        }
                    }

                    if (openEnded[j] == 0 
                        && openEnded[j + 1] == 9 
                        && openEnded[j + 2] == 10 
                        && openEnded[j + 3] == 11 
                        && openEnded[j + 4] == 12)
                    {
                        current = 4;

                        power = 13 + current * 100;

                        win.Add(new Type()
                        {
                            Power = power,
                            Current = 4
                        });

                        sorted = 
                            win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }
            }
        }

        private void RuleThreeOfAKind(
            ref double current,
            ref double power,
            int[] straight)
        {
            if (current >= -1)
            {
                for (var j = 0; j <= 12; j++)
                {
                    var fullHouse = straight.Where(o => o / 4 == j).ToArray();
                    if (fullHouse.Length == 3)
                    {
                        if (fullHouse.Max() / 4 == 0)
                        {
                            current = 3;

                            power = 13 * 3 + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 3
                            });

                            sorted = 
                                win
                                .OrderByDescending(op => op.Current)
                                .ThenByDescending(op => op.Power)
                                .First();
                        }
                        else
                        {
                            current = 3;

                            power = 
                                fullHouse[0] / 4 + fullHouse[1] / 4 + fullHouse[2] / 4 + current * 100;

                            win.Add(new Type()
                            {
                                Power = power,
                                Current = 3
                            });

                            sorted = 
                                win
                                .OrderByDescending(op => op.Current)
                                .ThenByDescending(op => op.Power)
                                .First();
                        }
                    }
                }
            }
        }

        private void RuleTwoPair(
            ref double current,
            ref double power)
        {
            if (current >= -1)
            {
                bool messageBox = false;

                const int maxIndex = 16;
                const int minIndex = 12;

                for (var indexOfCardsInTable = maxIndex; indexOfCardsInTable >= minIndex; indexOfCardsInTable--)
                {
                    int max = indexOfCardsInTable - minIndex;

                    if (reserve[i] / 4 != reserve[i + 1] / 4)
                    {
                        for (int k = 1; k <= max; k++)
                        {
                            if (indexOfCardsInTable - k < 12)
                            {
                                max--;
                            }

                            if (indexOfCardsInTable - k >= 12)
                            {
                                if (reserve[i] / 4 == reserve[indexOfCardsInTable] / 4 
                                    && reserve[i + 1] / 4 == reserve[indexOfCardsInTable - k] / 4 
                                    || reserve[i + 1] / 4 == reserve[indexOfCardsInTable] / 4 
                                    && reserve[i] / 4 == reserve[indexOfCardsInTable - k] / 4)
                                {
                                    if (!messageBox)
                                    {
                                        if (reserve[i] / 4 == 0)
                                        {
                                            current = 2;

                                            power = 13 * 4 + (reserve[i + 1] / 4) * 2 + current * 100;

                                            win.Add(new Type()
                                            {
                                                Power = power,
                                                Current = 2
                                            });

                                            sorted = 
                                                win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }

                                        if (reserve[i + 1] / 4 == 0)
                                        {
                                            current = 2;

                                            power = 13 * 4 + (reserve[i] / 4) * 2 + current * 100;

                                            win.Add(new Type()
                                            {
                                                Power = power,
                                                Current = 2
                                            });

                                            sorted = 
                                                win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }

                                        if (reserve[i + 1] / 4 != 0 && reserve[i] / 4 != 0)
                                        {
                                            current = 2;

                                            power = (reserve[i]/4) * 2 + (reserve[i + 1] / 4) * 2 + current * 100;

                                            win.Add(new Type()
                                            {
                                                Power = power,
                                                Current = 2
                                            });

                                            sorted = 
                                                win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
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
            ref double current,
            ref double power)
        {
            if (current >= -1)
            {
                bool messageBox = false;
                bool otherMessageBox = false;

                const int maxIndex = 16;
                const int minIndex = 12;

                for (int indexOfCardsInTable = maxIndex; indexOfCardsInTable >= minIndex; indexOfCardsInTable--)
                {
                    int max = indexOfCardsInTable - minIndex;
                    for (int k = 1; k <= max; k++)
                    {
                        if (indexOfCardsInTable - k < 12)
                        {
                            max--;
                        }
                        if (indexOfCardsInTable - k >= 12)
                        {
                            if (reserve[indexOfCardsInTable] / 4 == reserve[indexOfCardsInTable - k] / 4)
                            {
                                if (reserve[indexOfCardsInTable] / 4 != reserve[i] / 4 
                                    && reserve[indexOfCardsInTable] / 4 != reserve[i + 1] / 4 
                                    && current == 1)
                                {
                                    if (!messageBox)
                                    {
                                        if (reserve[i + 1] / 4 == 0)
                                        {
                                            current = 2;

                                            power = (reserve[i] / 4) * 2 + 13 * 4 + current * 100;

                                            win.Add(new Type()
                                            {
                                                Power = power,
                                                Current = 2
                                            });

                                            sorted = 
                                                win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }

                                        if (reserve[i] / 4 == 0)
                                        {
                                            current = 2;

                                            power = (reserve[i + 1] / 4) * 2 + 13 * 4 + current * 100;

                                            win.Add(new Type()
                                            {
                                                Power = power,
                                                Current = 2
                                            });

                                            sorted = 
                                                win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }

                                        if (reserve[i + 1] / 4 != 0)
                                        {
                                            current = 2;

                                            power = 
                                                (reserve[indexOfCardsInTable] / 4) * 2 + (reserve[i + 1] / 4) * 2 + current * 100;

                                            win.Add(new Type()
                                            {
                                                Power = power,
                                                Current = 2
                                            });

                                            sorted = 
                                                win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }
                                        if (reserve[i] / 4 != 0)
                                        {
                                            current = 2;

                                            power = 
                                                (reserve[indexOfCardsInTable] / 4) * 2 + (reserve[i] / 4) * 2 + current * 100;

                                            win.Add(new Type()
                                            {
                                                Power = power,
                                                Current = 2
                                            });

                                            sorted = 
                                                win
                                                .OrderByDescending(op => op.Current)
                                                .ThenByDescending(op => op.Power)
                                                .First();
                                        }
                                    }

                                    messageBox = true;
                                }

                                if (current == -1)
                                {
                                    if (!otherMessageBox)
                                    {
                                        if (reserve[i] / 4 > reserve[i + 1] / 4)
                                        {
                                            if (reserve[indexOfCardsInTable] / 4 == 0)
                                            {
                                                current = 0;

                                                power = 13 + reserve[i] / 4 + current * 100;

                                                win.Add(new Type()
                                                {
                                                    Power = power,
                                                    Current = 1
                                                });

                                                sorted = 
                                                    win
                                                    .OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                            }
                                            else
                                            {
                                                current = 0;

                                                power = reserve[indexOfCardsInTable] / 4 + reserve[i] / 4 + current * 100;

                                                win.Add(new Type()
                                                {
                                                    Power = power,
                                                    Current = 1
                                                });

                                                sorted = 
                                                    win
                                                    .OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                            }
                                        }
                                        else
                                        {
                                            if (reserve[indexOfCardsInTable] / 4 == 0)
                                            {
                                                current = 0;

                                                power = 13 + reserve[i + 1] + current * 100;

                                                win.Add(new Type()
                                                {
                                                    Power = power,
                                                    Current = 1
                                                });

                                                sorted = 
                                                    win
                                                    .OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
                                                    .First();
                                            }
                                            else
                                            {
                                                current = 0;

                                                power = reserve[indexOfCardsInTable] / 4 + reserve[i + 1] / 4 + current * 100;

                                                win.Add(new Type()
                                                {
                                                    Power = power,
                                                    Current = 1
                                                });

                                                sorted = win
                                                    .OrderByDescending(op => op.Current)
                                                    .ThenByDescending(op => op.Power)
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

        private void rPairFromHand(ref double current, ref double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                if (reserve[i]/4 == reserve[i + 1]/4)
                {
                    if (!msgbox)
                    {
                        if (reserve[i]/4 == 0)
                        {
                            current = 1;
                            Power = 13*4 + current*100;
                            win.Add(new Type() {Power = Power, Current = 1});
                            sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            current = 1;
                            Power = (reserve[i + 1]/4)*4 + current*100;
                            win.Add(new Type() {Power = Power, Current = 1});
                            sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                    msgbox = true;
                }
                for (int tc = 16; tc >= 12; tc--)
                {
                    if (reserve[i + 1]/4 == reserve[tc]/4)
                    {
                        if (!msgbox)
                        {
                            if (reserve[i + 1]/4 == 0)
                            {
                                current = 1;
                                Power = 13*4 + reserve[i]/4 + current*100;
                                win.Add(new Type() {Power = Power, Current = 1});
                                sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                current = 1;
                                Power = (reserve[i + 1]/4)*4 + reserve[i]/4 + current*100;
                                win.Add(new Type() {Power = Power, Current = 1});
                                sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                    if (reserve[i]/4 == reserve[tc]/4)
                    {
                        if (!msgbox)
                        {
                            if (reserve[i]/4 == 0)
                            {
                                current = 1;
                                Power = 13*4 + reserve[i + 1]/4 + current*100;
                                win.Add(new Type() {Power = Power, Current = 1});
                                sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                current = 1;
                                Power = (reserve[tc]/4)*4 + reserve[i + 1]/4 + current*100;
                                win.Add(new Type() {Power = Power, Current = 1});
                                sorted = win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                }
            }
        }

        private void rHighCard(ref double current, ref double Power)
        {
            if (current == -1)
            {
                if (reserve[i]/4 > reserve[i + 1]/4)
                {
                    current = -1;
                    Power = reserve[i]/4;
                    win.Add(new Type() {Power = Power, Current = -1});
                    sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                else
                {
                    current = -1;
                    Power = reserve[i + 1]/4;
                    win.Add(new Type() {Power = Power, Current = -1});
                    sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                if (reserve[i]/4 == 0 || reserve[i + 1]/4 == 0)
                {
                    current = -1;
                    Power = 13;
                    win.Add(new Type() {Power = Power, Current = -1});
                    sorted = win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
            }
        }

        void Winner(double current, double Power, string currentText, int chips, string lastly)
        {
            if (lastly == " ")
            {
                lastly = "Bot 5";
            }
            for (int j = 0; j <= 16; j++)
            {
                //await Task.Delay(5);
                if (holder[j].Visible)
                {
                    holder[j].Image = deck[j];
                }       
            }
            if (current == sorted.Current)
            {
                if (Power == sorted.Power)
                {
                    winners++;
                    checkWinners.Add(currentText);
                    if (current == -1)
                    {
                        MessageBox.Show(currentText + " High Card ");
                    }

                    if (current == 1 || current == 0)
                    {
                        MessageBox.Show(currentText + " Pair ");
                    }

                    if (current == 2)
                    {
                        MessageBox.Show(currentText + " Two Pair ");
                    }

                    if (current == 3)
                    {
                        MessageBox.Show(currentText + " Three of a Kind ");
                    }

                    if (current == 4)
                    {
                        MessageBox.Show(currentText + " Straight ");
                    }

                    if (current == 5 || current == 5.5)
                    {
                        MessageBox.Show(currentText + " Flush ");
                    }

                    if (current == 6)
                    {
                        MessageBox.Show(currentText + " Full House ");
                    }

                    if (current == 7)
                    {
                        MessageBox.Show(currentText + " Four of a Kind ");
                    }

                    if (current == 8)
                    {
                        MessageBox.Show(currentText + " Straight Flush ");
                    }

                    if (current == 9)
                    {
                        MessageBox.Show(currentText + " Royal Flush ! ");
                    }
                }
            }
            //lastfixed
            if (currentText == lastly) 
            {
                if (winners > 1)
                {
                    if (checkWinners.Contains("Player"))
                    {
                        this.chips += int.Parse(textboxPot.Text)/ winners;
                        textboxChips.Text = this.chips.ToString();
                        ////pPanel.Visible = true;
                    }

                    if (checkWinners.Contains("Bot 1"))
                    {
                        firstBotChips += int.Parse(textboxPot.Text)/winners;
                        textboxFirstBotChips.Text = firstBotChips.ToString();
                        ////b1Panel.Visible = true;
                    }

                    if (checkWinners.Contains("Bot 2"))
                    {
                        secondBotChips += int.Parse(textboxPot.Text)/winners;
                        textboxSecondBotChips.Text = secondBotChips.ToString();
                        ////b2Panel.Visible = true;
                    }

                    if (checkWinners.Contains("Bot 3"))
                    {
                        thirdBotChips += int.Parse(textboxPot.Text)/winners;
                        textbokThirdBotChips.Text = thirdBotChips.ToString();
                        ////b3Panel.Visible = true;
                    }

                    if (checkWinners.Contains("Bot 4"))
                    {
                        fourthBotChips += int.Parse(textboxPot.Text)/winners;
                        textboxFourthBotChips.Text = fourthBotChips.ToString();
                        //b4Panel.Visible = true;
                    }

                    if (checkWinners.Contains("Bot 5"))
                    {
                        fifthBotChips += int.Parse(textboxPot.Text)/winners;
                        textboxFifthBotChips.Text = fifthBotChips.ToString();
                        //b5Panel.Visible = true;
                    }

                    //await Finish(1);
                }
                if (winners == 1)
                {
                    if (checkWinners.Contains("Player"))
                    {
                        this.chips += int.Parse(textboxPot.Text);
                        //await Finish(1);
                        //pPanel.Visible = true;
                    }
                    if (checkWinners.Contains("Bot 1"))
                    {
                        firstBotChips += int.Parse(textboxPot.Text);
                        //await Finish(1);
                        //b1Panel.Visible = true;
                    }
                    if (checkWinners.Contains("Bot 2"))
                    {
                        secondBotChips += int.Parse(textboxPot.Text);
                        //await Finish(1);
                        //b2Panel.Visible = true;
                    }
                    if (checkWinners.Contains("Bot 3"))
                    {
                        thirdBotChips += int.Parse(textboxPot.Text);
                        //await Finish(1);
                        //b3Panel.Visible = true;
                    }
                    if (checkWinners.Contains("Bot 4"))
                    {
                        fourthBotChips += int.Parse(textboxPot.Text);
                        //await Finish(1);
                        //b4Panel.Visible = true;
                    }
                    if (checkWinners.Contains("Bot 5"))
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
                changed = true;
            }
            else
            {
                if (turnCount >= maxLeft - 1 || !changed && turnCount == maxLeft)
                {
                    if (currentTurn == raisedTurn - 1 || !changed && turnCount == maxLeft || raisedTurn == 0 && currentTurn == 5)
                    {
                        changed = false;
                        turnCount = 0;
                        raise = 0;
                        call = 0;
                        raisedTurn = 123;
                        rounds++;
                        if (!foldedPlayerTurn)
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
                    if (holder[j].Image != deck[j])
                    {
                        holder[j].Image = deck[j];
                        playerCall = 0;
                        playerRaise = 0;
                        firstBotCall = 0;
                        firstBotRaise = 0;
                        secondBotCall = 0;
                        secondBotRaise = 0;
                        thirdBotCall = 0;
                        thirdBotRaise = 0;
                        fourthBotCall = 0;
                        fourthBotRaise = 0;
                        fifthBotCall = 0;
                        fifthBotRaise = 0;
                    }
                }
            }
            if (rounds == turn)
            {
                for (int j = 14; j <= 15; j++)
                {
                    if (holder[j].Image != deck[j])
                    {
                        holder[j].Image = deck[j];
                        playerCall = 0;
                        playerRaise = 0;
                        firstBotCall = 0;
                        firstBotRaise = 0;
                        secondBotCall = 0;
                        secondBotRaise = 0;
                        thirdBotCall = 0;
                        thirdBotRaise = 0;
                        fourthBotCall = 0;
                        fourthBotRaise = 0;
                        fifthBotCall = 0;
                        fifthBotRaise = 0;
                    }
                }
            }
            if (rounds == river)
            {
                for (int j = 15; j <= 16; j++)
                {
                    if (holder[j].Image != deck[j])
                    {
                        holder[j].Image = deck[j];
                        playerCall = 0;
                        playerRaise = 0;
                        firstBotCall = 0;
                        firstBotRaise = 0;
                        secondBotCall = 0;
                        secondBotRaise = 0;
                        thirdBotCall = 0;
                        thirdBotRaise = 0;
                        fourthBotCall = 0;
                        fourthBotRaise = 0;
                        fifthBotCall = 0;
                        fifthBotRaise = 0;
                    }
                }
            }
            if (rounds == end && maxLeft == 6)
            {
                string fixedLast = "qwerty";
                if (!playerStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Player";
                    Rules(0, 1, "Player", ref playerType, ref playerPower, foldedPlayerTurn);
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
                Winner(playerType, playerPower, "Player", chips, fixedLast);
                Winner(firstBotType, firstBotPower, "Bot 1", firstBotChips, fixedLast);
                Winner(secondBotType, secondBotPower, "Bot 2", secondBotChips, fixedLast);
                Winner(thirdBotType, thirdBotPower, "Bot 3", thirdBotChips, fixedLast);
                Winner(fourthBotType, fourthBotPower, "Bot 4", fourthBotChips, fixedLast);
                Winner(fifthBotType, fifthBotPower, "Bot 5", fifthBotChips, fixedLast);
                restart = true;
                playerTurn = true;
                foldedPlayerTurn = false;
                firstBotFoldedTurn = false;
                secodBotFoldedTurn = false;
                thirdBotFoldedTurn = false;
                fourthBotFoldedTurn = false;
                fifthBotFoldedTurn = false;
                if (chips <= 0)
                {
                    AddChips f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.a != 0)
                    {
                        chips = f2.a;
                        firstBotChips += f2.a;
                        secondBotChips += f2.a;
                        thirdBotChips += f2.a;
                        fourthBotChips += f2.a;
                        fifthBotChips += f2.a;
                        foldedPlayerTurn = false;
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
                playerCall = 0;
                playerRaise = 0;
                firstBotCall = 0;
                firstBotRaise = 0;
                secondBotCall = 0;
                secondBotRaise = 0;
                thirdBotCall = 0;
                thirdBotRaise = 0;
                fourthBotCall = 0;
                fourthBotRaise = 0;
                fifthBotCall = 0;
                fifthBotRaise = 0;
                last = 0;
                call = bigBlind;
                raise = 0;
                imageLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
                bools.Clear();
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
                ints.Clear();
                checkWinners.Clear();
                winners = 0;
                win.Clear();
                sorted.Current = 0;
                sorted.Power = 0;
                for (int os = 0; os < 17; os++)
                {
                    holder[os].Image = null;
                    holder[os].Invalidate();
                    holder[os].Visible = false;
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
                    if (cRaise != raise && cRaise <= raise)
                    {
                        call = Convert.ToInt32(raise) - cRaise;
                    }

                    if (cCall != call || cCall <= call)
                    {
                        call = call - cCall;
                    }

                    if (cRaise == raise && raise > 0)
                    {
                        call = 0;
                        buttonCall.Enabled = false;
                        buttonCall.Text = "Callisfuckedup";
                    }
                }
            }
        }

        async Task AllIn()
        {
            #region All in

            if (chips <= 0 && !intsadded)
            {
                if (playerStatus.Text.Contains("Raise"))
                {
                    ints.Add(chips);
                    intsadded = true;
                }

                if (playerStatus.Text.Contains("Call"))
                {
                    ints.Add(chips);
                    intsadded = true;
                }
            }
            intsadded = false;
            if (firstBotChips <= 0 && !firstBotFoldedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(firstBotChips);
                    intsadded = true;
                }

                intsadded = false;
            }
            if (secondBotChips <= 0 && !secodBotFoldedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(secondBotChips);
                    intsadded = true;
                }

                intsadded = false;
            }
            if (thirdBotChips <= 0 && !thirdBotFoldedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(thirdBotChips);
                    intsadded = true;
                }

                intsadded = false;
            }
            if (fourthBotChips <= 0 && !fourthBotFoldedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(fourthBotChips);
                    intsadded = true;
                }

                intsadded = false;
            }
            if (fifthBotChips <= 0 && !fifthBotFoldedTurn)
            {
                if (!intsadded)
                {
                    ints.Add(fifthBotChips);
                    intsadded = true;
                }
            }
            if (ints.ToArray().Length == maxLeft)
            {
                await Finish(2);
            }
            else
            {
                ints.Clear();
            }

            #endregion

            var abc = bools.Count(x => x == false);

            #region LastManStanding

            if (abc == 1)
            {
                int index = bools.IndexOf(false);
                if (index == 0)
                {
                    chips += int.Parse(textboxPot.Text);
                    textboxChips.Text = chips.ToString();
                    playerPanel.Visible = true;
                    MessageBox.Show("Player Wins");
                }

                if (index == 1)
                {
                    firstBotChips += int.Parse(textboxPot.Text);
                    textboxChips.Text = firstBotChips.ToString();
                    firstBotPanel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }

                if (index == 2)
                {
                    secondBotChips += int.Parse(textboxPot.Text);
                    textboxChips.Text = secondBotChips.ToString();
                    secondBotPanel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }

                if (index == 3)
                {
                    thirdBotChips += int.Parse(textboxPot.Text);
                    textboxChips.Text = thirdBotChips.ToString();
                    thirdBotPanel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }

                if (index == 4)
                {
                    fourthBotChips += int.Parse(textboxPot.Text);
                    textboxChips.Text = fourthBotChips.ToString();
                    fourthBotPanel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }

                if (index == 5)
                {
                    fifthBotChips += int.Parse(textboxPot.Text);
                    textboxChips.Text = fifthBotChips.ToString();
                    fifthBotPanel.Visible = true;
                    MessageBox.Show("Bot 5 Wins");
                }

                for (int j = 0; j <= 16; j++)
                {
                    holder[j].Visible = false;
                }
                await Finish(1);
            }

            intsadded = false;

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
            call = bigBlind;
            raise = 0;
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
            raise = 0;
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
            foldedPlayerTurn = false;
            playerTurn = true;
            restart = false;
            raising = false;
            playerCall = 0;
            firstBotCall = 0;
            secondBotCall = 0;
            thirdBotCall = 0;
            fourthBotCall = 0;
            fifthBotCall = 0;
            playerRaise = 0;
            firstBotRaise = 0;
            secondBotRaise = 0;
            thirdBotRaise = 0;
            fourthBotRaise = 0;
            fifthBotRaise = 0;
            height = 0;
            width = 0;
            winners = 0;
            flop = 1;
            turn = 2;
            river = 3;
            end = 4;
            maxLeft = 6;
            last = 123;
            raisedTurn = 1;
            bools.Clear();
            checkWinners.Clear();
            ints.Clear();
            win.Clear();
            sorted.Current = 0;
            sorted.Power = 0;
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
            if (chips <= 0)
            {
                AddChips f2 = new AddChips();
                f2.ShowDialog();
                if (f2.a != 0)
                {
                    chips = f2.a;
                    firstBotChips += f2.a;
                    secondBotChips += f2.a;
                    thirdBotChips += f2.a;
                    fourthBotChips += f2.a;
                    fifthBotChips += f2.a;
                    foldedPlayerTurn = false;
                    playerTurn = true;
                    buttonRaise.Enabled = true;
                    buttonFold.Enabled = true;
                    buttonCheck.Enabled = true;
                    buttonRaise.Text = "Raise";
                }
            }
            imageLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
            for (int os = 0; os < 17; os++)
            {
                holder[os].Image = null;
                holder[os].Invalidate();
                holder[os].Visible = false;
            }
            await Shuffle();
            //await Turns();
        }

        void FixWinners()
        {
            win.Clear();
            sorted.Current = 0;
            sorted.Power = 0;
            string fixedLast = "qwerty";
            if (!playerStatus.Text.Contains("Fold"))
            {
                fixedLast = "Player";
                Rules(0, 1, "Player", ref playerType, ref playerPower, foldedPlayerTurn);
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

            Winner(playerType, playerPower, "Player", chips, fixedLast);
            Winner(firstBotType, firstBotPower, "Bot 1", firstBotChips, fixedLast);
            Winner(secondBotType, secondBotPower, "Bot 2", secondBotChips, fixedLast);
            Winner(thirdBotType, thirdBotPower, "Bot 3", thirdBotChips, fixedLast);
            Winner(fourthBotType, fourthBotPower, "Bot 4", fourthBotChips, fixedLast);
            Winner(fifthBotType, fifthBotPower, "Bot 5", fifthBotChips, fixedLast);
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
                holder[c1].Visible = false;
                holder[c2].Visible = false;
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
            sChips -= call;
            sStatus.Text = "Call " + call;
            textboxPot.Text = (int.Parse(textboxPot.Text) + call).ToString();
        }

        private void Raised(ref int sChips, ref bool sTurn, Label sStatus)
        {
            sChips -= Convert.ToInt32(raise);
            sStatus.Text = "Raise " + raise;
            textboxPot.Text = (int.Parse(textboxPot.Text) + Convert.ToInt32(raise)).ToString();
            call = Convert.ToInt32(raise);
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
            if (call <= 0)
            {
                Check(ref sTurn, sStatus);
            }
            if (call > 0)
            {
                if (rnd == 1)
                {
                    if (call <= RoundN(sChips, n))
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
                    if (call <= RoundN(sChips, n1))
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
                if (raise == 0)
                {
                    raise = call*2;
                    Raised(ref sChips, ref sTurn, sStatus);
                }
                else
                {
                    if (raise <= RoundN(sChips, n))
                    {
                        raise = call*2;
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
                if (call <= 0)
                {
                    Check(ref sTurn, sStatus);
                }
                if (call > 0)
                {
                    if (call >= RoundN(sChips, n1))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }

                    if (raise > RoundN(sChips, n))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }

                    if (!sFTurn)
                    {
                        if (call >= RoundN(sChips, n) && call <= RoundN(sChips, n1))
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }

                        if (raise <= RoundN(sChips, n) && raise >= (RoundN(sChips, n))/2)
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }

                        if (raise <= (RoundN(sChips, n))/2)
                        {
                            if (raise > 0)
                            {
                                raise = RoundN(sChips, n);
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                            else
                            {
                                raise = call*2;
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                        }
                    }
                }
            }
            if (rounds >= 2)
            {
                if (call > 0)
                {
                    if (call >= RoundN(sChips, n1 - rnd))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }

                    if (raise > RoundN(sChips, n - rnd))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }

                    if (!sFTurn)
                    {
                        if (call >= RoundN(sChips, n - rnd) && call <= RoundN(sChips, n1 - rnd))
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }

                        if (raise <= RoundN(sChips, n - rnd) && raise >= (RoundN(sChips, n - rnd))/2)
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }

                        if (raise <= (RoundN(sChips, n - rnd))/2)
                        {
                            if (raise > 0)
                            {
                                raise = RoundN(sChips, n - rnd);
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                            else
                            {
                                raise = call*2;
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                        }
                    }
                }
                if (call <= 0)
                {
                    raise = RoundN(sChips, r - rnd);
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
            if (call <= 0)
            {
                Check(ref botTurn, botStatus);
            }
            else
            {
                if (call >= RoundN(botChips, n))
                {
                    if (botChips > call)
                    {
                        Call(ref botChips, ref botTurn, botStatus);
                    }
                    else if (botChips <= call)
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
                    if (raise > 0)
                    {
                        if (botChips >= raise*2)
                        {
                            raise *= 2;
                            Raised(ref botChips, ref botTurn, botStatus);
                        }
                        else
                        {
                            Call(ref botChips, ref botTurn, botStatus);
                        }
                    }
                    else
                    {
                        raise = call*2;
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
                foldedPlayerTurn = true;
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
            if (chips <= 0)
            {
                textboxChips.Text = "Chips : 0";
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

            textboxChips.Text = "Chips : " + chips.ToString();
            textboxFirstBotChips.Text = "Chips : " + firstBotChips.ToString();
            textboxSecondBotChips.Text = "Chips : " + secondBotChips.ToString();
            textbokThirdBotChips.Text = "Chips : " + thirdBotChips.ToString();
            textboxFourthBotChips.Text = "Chips : " + fourthBotChips.ToString();
            textboxFifthBotChips.Text = "Chips : " + fifthBotChips.ToString();
            if (chips <= 0)
            {
                playerTurn = false;
                foldedPlayerTurn = true;
                buttonCall.Enabled = false;
                buttonRaise.Enabled = false;
                buttonFold.Enabled = false;
                buttonCheck.Enabled = false;
            }

            if (up > 0)
            {
                up--;
            }

            if (chips >= call)
            {
                buttonCall.Text = "Call " + call.ToString();
            }
            else
            {
                buttonCall.Text = "All in";
                buttonRaise.Enabled = false;
            }

            if (call > 0)
            {
                buttonCheck.Enabled = false;
            }

            if (call <= 0)
            {
                buttonCheck.Enabled = true;
                buttonCall.Text = "Call";
                buttonCall.Enabled = false;
            }

            if (chips <= 0)
            {
                buttonRaise.Enabled = false;
            }

            int parsedValue;

            if (textboxRaise.Text != string.Empty && int.TryParse(textboxRaise.Text, out parsedValue))
            {
                if (chips <= int.Parse(textboxRaise.Text))
                {
                    buttonRaise.Text = "All in";
                }
                else
                {
                    buttonRaise.Text = "Raise";
                }
            }

            if (chips < call)
            {
                buttonRaise.Enabled = false;
            }
        }

        private async void bFold_Click(object sender, EventArgs e)
        {
            playerStatus.Text = "Fold";
            playerTurn = false;
            foldedPlayerTurn = true;
            await Turns();
        }

        private async void bCheck_Click(object sender, EventArgs e)
        {
            if (call <= 0)
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
            Rules(0, 1, "Player", ref playerType, ref playerPower, foldedPlayerTurn);
            if (chips >= call)
            {
                chips -= call;
                textboxChips.Text = "Chips : " + chips.ToString();
                if (textboxPot.Text != string.Empty)
                {
                    textboxPot.Text = (int.Parse(textboxPot.Text) + call).ToString();
                }

                else
                {
                    textboxPot.Text = call.ToString();
                }

                playerTurn = false;
                playerStatus.Text = "Call " + call;
                playerCall = call;
            }
            else if (chips <= call && call > 0)
            {
                textboxPot.Text = (int.Parse(textboxPot.Text) + chips).ToString();
                playerStatus.Text = "All in " + chips;
                chips = 0;
                textboxChips.Text = "Chips : " + chips.ToString();
                playerTurn = false;
                buttonFold.Enabled = false;
                playerCall = chips;
            }

            await Turns();
        }

        private async void bRaise_Click(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", ref playerType, ref playerPower, foldedPlayerTurn);
            int parsedValue;
            if (textboxRaise.Text != string.Empty && int.TryParse(textboxRaise.Text, out parsedValue))
            {
                if (chips > call)
                {
                    if (raise*2 > int.Parse(textboxRaise.Text))
                    {
                        textboxRaise.Text = (raise*2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }

                    else
                    {
                        if (chips >= int.Parse(textboxRaise.Text))
                        {
                            call = int.Parse(textboxRaise.Text);
                            raise = int.Parse(textboxRaise.Text);
                            playerStatus.Text = "Raise " + call.ToString();
                            textboxPot.Text = (int.Parse(textboxPot.Text) + call).ToString();
                            buttonCall.Text = "Call";
                            chips -= int.Parse(textboxRaise.Text);
                            raising = true;
                            last = 0;
                            playerRaise = Convert.ToInt32(raise);
                        }

                        else
                        {
                            call = chips;
                            raise = chips;
                            textboxPot.Text = (int.Parse(textboxPot.Text) + chips).ToString();
                            playerStatus.Text = "Raise " + call.ToString();
                            chips = 0;
                            raising = true;
                            last = 0;
                            playerRaise = Convert.ToInt32(raise);
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
                chips += int.Parse(textboxAdd.Text);
                firstBotChips += int.Parse(textboxAdd.Text);
                secondBotChips += int.Parse(textboxAdd.Text);
                thirdBotChips += int.Parse(textboxAdd.Text);
                fourthBotChips += int.Parse(textboxAdd.Text);
                fifthBotChips += int.Parse(textboxAdd.Text);
            }
           
            textboxChips.Text = "Chips : " + chips.ToString();
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