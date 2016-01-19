﻿namespace Poker
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

        //private ProgressBar _asd = new ProgressBar();
        public int Nm;

        private readonly Panel _playerPanel = new Panel();
        private readonly Panel _firstBotPanel = new Panel();
        private readonly Panel _secondBotPanel = new Panel();
        private readonly Panel _thirdBotPanel = new Panel();
        private readonly Panel _fourthBotPanel = new Panel();
        private readonly Panel _fifthBotPanel = new Panel();

        private int _call = 500;

        private int _foldedPlayers = 5;

        public int Chips = 10000;
        public int FirstBotChips = 10000;
        public int SecondBotChips = 10000;
        public int ThirdBotChips = 10000;
        public int FourthBotChips = 10000;
        public int FifthBotChips = 10000;

        private double _type;

        private double _rounds;

        private double _firstBotPower;
        private double _secondBotPower;
        private double _thirdBotPower;
        private double _fourthBotPower;
        private double _fifthBotPower;
        private double _playerPower;

        private double _raise;

        private double _playerType = -1;
        private double _firstBotType = -1;
        private double _secondBotType = -1;
        private double _thirdBotType = -1;
        private double _fourthBotType = -1;
        private double _fifthBotType = -1;

        private bool _firstBotTurn;
        private bool _secondBotTurn;
        private bool _thirdBotTurn;
        private bool _fourthBotTurn;
        private bool _fifthBotTurn;

        private bool _firstBotFoldedTurn;
        private bool _secodBotFoldedTurn;
        private bool _thirdBotFoldedTurn;
        private bool _fourthBotFoldedTurn;
        private bool _fifthBotFoldedTurn;

        private bool _playerFolded;
        private bool _firstBotFolded;
        private bool _secondBotFolded;
        private bool _thirdBotFolded;
        private bool _fourthBotFolded;
        private bool _fifthBotFolded;

        private bool _intsadded;

        private bool _changed;

        private int _playerCall;
        private int _firstBotCall;
        private int _secondBotCall;
        private int _thirdBotCall;
        private int _fourthBotCall;
        private int _fifthBotCall;

        private int _playerRaise;
        private int _firstBotRaise;
        private int _secondBotRaise;
        private int _thirdBotRaise;
        private int _fourthBotRaise;
        private int _fifthBotRaise;

        private int _height;
        private int _width;

        private int _winners;

        private int _flop = 1;

        private int _turn = 2;

        private int _river = 3;

        private int _end = 4;

        private int _maxLeft = 6;

        private int _last = 126;

        private int _raisedTurn = 1;

        private readonly List<bool?> _bools = new List<bool?>();
        private readonly List<Type> _win = new List<Type>();
        private readonly List<string> _checkWinners = new List<string>();
        private readonly List<int> _ints = new List<int>();

        private bool _foldedPlayerTurn;

        private bool _playerTurn = true;

        private bool _restart;

        private bool _raising;

        private Type _sorted;

        private string[] _imageLocation = Directory.GetFiles(
            "Assets\\Cards", "*.png",
            SearchOption.TopDirectoryOnly);
        /*string[] ImgLocation ={
                   "Assets\\Cards\\33.png","Assets\\Cards\\22.png",
                    "Assets\\Cards\\29.png","Assets\\Cards\\21.png",
                    "Assets\\Cards\\36.png","Assets\\Cards\\17.png",
                    "Assets\\Cards\\40.png","Assets\\Cards\\16.png",
                    "Assets\\Cards\\5.png","Assets\\Cards\\47.png",
                    "Assets\\Cards\\37.png","Assets\\Cards\\13.png",
                    
                    "Assets\\Cards\\12.png",
                    "Assets\\Cards\\8.png","Assets\\Cards\\18.png",
                    "Assets\\Cards\\15.png","Assets\\Cards\\27.png"};*/
        private readonly int[] _reserve = new int[17];

        private readonly Image[] _deck = new Image[52];

        private readonly PictureBox[] _holder = new PictureBox[52];

        private readonly Timer _timer = new Timer();
        private readonly Timer _updates = new Timer();

        private int _timeTillNextTurn = 60;

        private int i;

        private int _bigBlind = 500;
        private int _smallBlind = 250;

        private int up = 10000000;

        private int _turnCount;

        #endregion

        public Form1()
        {
            //bools.Add(PFturn); bools.Add(B1Fturn); bools.Add(B2Fturn); bools.Add(B3Fturn); bools.Add(B4Fturn); bools.Add(B5Fturn);
            _call = _bigBlind;

            MaximizeBox = false;
            MinimizeBox = false;

            _updates.Start();

            InitializeComponent();

            _width = Width;
            _height = Height;

            Shuffle();

            textboxPot.Enabled = false;
            textboxChips.Enabled = false;

            textboxFirstBotChips.Enabled = false;
            textboxSecondBotChips.Enabled = false;
            textbokThirdBotChips.Enabled = false;
            textboxFourthBotChips.Enabled = false;
            textboxFifthBotChips.Enabled = false;

            textboxChips.Text = @"Chips : " + Chips;
            textboxFirstBotChips.Text = @"Chips : " + FirstBotChips;
            textboxSecondBotChips.Text = @"Chips : " + SecondBotChips;
            textbokThirdBotChips.Text = @"Chips : " + ThirdBotChips;
            textboxFourthBotChips.Text = @"Chips : " + FourthBotChips;
            textboxFifthBotChips.Text = @"Chips : " + FifthBotChips;

            _timer.Interval = (1*1*1000);
            _timer.Tick += timer_Tick;
            _updates.Interval = (1*1*100);
            _updates.Tick += Update_Tick;

            textboxBigBlind.Visible = true;
            textboxSmallBlind.Visible = true;

            buttonBigBlind.Visible = true;
            buttonSmallBlind.Visible = true;

            textboxBigBlind.Visible = true;
            textboxSmallBlind.Visible = true;

            buttonBigBlind.Visible = true;
            buttonSmallBlind.Visible = true;

            textboxBigBlind.Visible = false;
            textboxSmallBlind.Visible = false;

            buttonBigBlind.Visible = false;
            buttonSmallBlind.Visible = false;

            textboxRaise.Text = (_bigBlind*2).ToString();
        }

        private async Task Shuffle()
        {
            _bools.Add(_foldedPlayerTurn);
            _bools.Add(_firstBotFoldedTurn);
            _bools.Add(_secodBotFoldedTurn);
            _bools.Add(_thirdBotFoldedTurn);
            _bools.Add(_fourthBotFoldedTurn);
            _bools.Add(_fifthBotFoldedTurn);

            buttonCall.Enabled = false;
            buttonRaise.Enabled = false;
            buttonFold.Enabled = false;
            buttonCheck.Enabled = false;

            MaximizeBox = false;
            MinimizeBox = false;

            var check = false;

            var backImage = new Bitmap("Assets\\Back\\Back.png");

            var horizontal = 580;
            var vertical = 480;

            var random = new Random();

            for (i = _imageLocation.Length; i > 0; i--)
            {
                var position = random.Next(i);

                var image = _imageLocation[position];

                _imageLocation[position] = _imageLocation[i - 1];
                _imageLocation[i - 1] = image;
            }

            for (i = 0; i < 17; i++)
            {
                _deck[i] = Image.FromFile(_imageLocation[i]);

                var charsToRemove = new string[] {"Assets\\Cards\\", ".png"};
                foreach (var c in charsToRemove)
                {
                    _imageLocation[i] = _imageLocation[i].Replace(c, string.Empty);
                }

                _reserve[i] = int.Parse(_imageLocation[i]) - 1;

                _holder[i] = new PictureBox
                {
                    SizeMode = PictureBoxSizeMode.StretchImage,
                    Height = 130,
                    Width = 80
                };

                Controls.Add(_holder[i]);
                _holder[i].Name = "pb" + i;

                await Task.Delay(200);

                #region Throwing Cards

                if (i < 2)
                {
                    if (_holder[0].Tag != null)
                    {
                        _holder[1].Tag = _reserve[1];
                    }
                    _holder[0].Tag = _reserve[0];
                    _holder[i].Image = _deck[i];
                    _holder[i].Anchor = (AnchorStyles.Bottom);
                    //Holder[i].Dock = DockStyle.Top;
                    _holder[i].Location = new Point(horizontal, vertical);

                    horizontal += _holder[i].Width;

                    Controls.Add(_playerPanel);

                    _playerPanel.Location = new Point(_holder[0].Left - 10, _holder[0].Top - 10);
                    _playerPanel.BackColor = Color.DarkBlue;
                    _playerPanel.Height = 150;
                    _playerPanel.Width = 180;
                    _playerPanel.Visible = false;
                }

                if (FirstBotChips > 0)
                {
                    _foldedPlayers--;

                    if (i >= 2 && i < 4)
                    {
                        if (_holder[2].Tag != null)
                        {
                            _holder[3].Tag = _reserve[3];
                        }

                        _holder[2].Tag = _reserve[2];

                        if (!check)
                        {
                            horizontal = 15;
                            vertical = 420;
                        }

                        check = true;

                        _holder[i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                        _holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        _holder[i].Location = new Point(horizontal, vertical);

                        horizontal += _holder[i].Width;

                        _holder[i].Visible = true;

                        Controls.Add(_firstBotPanel);

                        _firstBotPanel.Location = new Point(
                            _holder[2].Left - 10,
                            _holder[2].Top - 10);
                        _firstBotPanel.BackColor = Color.DarkBlue;
                        _firstBotPanel.Height = 150;
                        _firstBotPanel.Width = 180;
                        _firstBotPanel.Visible = false;

                        if (i == 3)
                        {
                            check = false;
                        }
                    }
                }

                if (SecondBotChips > 0)
                {
                    _foldedPlayers--;

                    if (i >= 4 && i < 6)
                    {
                        if (_holder[4].Tag != null)
                        {
                            _holder[5].Tag = _reserve[5];
                        }

                        _holder[4].Tag = _reserve[4];

                        if (!check)
                        {
                            horizontal = 75;
                            vertical = 65;
                        }

                        check = true;

                        _holder[i].Anchor = (AnchorStyles.Top | AnchorStyles.Left);
                        _holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        _holder[i].Location = new Point(horizontal, vertical);

                        horizontal += _holder[i].Width;

                        _holder[i].Visible = true;

                        Controls.Add(_secondBotPanel);

                        _secondBotPanel.Location = new Point(
                            _holder[4].Left - 10,
                            _holder[4].Top - 10);
                        _secondBotPanel.BackColor = Color.DarkBlue;
                        _secondBotPanel.Height = 150;
                        _secondBotPanel.Width = 180;
                        _secondBotPanel.Visible = false;

                        if (i == 5)
                        {
                            check = false;
                        }
                    }
                }

                if (ThirdBotChips > 0)
                {
                    _foldedPlayers--;

                    if (i >= 6 && i < 8)
                    {
                        if (_holder[6].Tag != null)
                        {
                            _holder[7].Tag = _reserve[7];
                        }

                        _holder[6].Tag = _reserve[6];

                        if (!check)
                        {
                            horizontal = 590;
                            vertical = 25;
                        }

                        check = true;

                        _holder[i].Anchor = (AnchorStyles.Top);
                        _holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        _holder[i].Location = new Point(horizontal, vertical);

                        horizontal += _holder[i].Width;

                        _holder[i].Visible = true;

                        Controls.Add(_thirdBotPanel);

                        _thirdBotPanel.Location = new Point(
                            _holder[6].Left - 10,
                            _holder[6].Top - 10);
                        _thirdBotPanel.BackColor = Color.DarkBlue;
                        _thirdBotPanel.Height = 150;
                        _thirdBotPanel.Width = 180;
                        _thirdBotPanel.Visible = false;

                        if (i == 7)
                        {
                            check = false;
                        }
                    }
                }

                if (FourthBotChips > 0)
                {
                    _foldedPlayers--;

                    if (i >= 8 && i < 10)
                    {
                        if (_holder[8].Tag != null)
                        {
                            _holder[9].Tag = _reserve[9];
                        }

                        _holder[8].Tag = _reserve[8];

                        if (!check)
                        {
                            horizontal = 1115;
                            vertical = 65;
                        }

                        check = true;

                        _holder[i].Anchor = (AnchorStyles.Top | AnchorStyles.Right);
                        _holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        _holder[i].Location = new Point(horizontal, vertical);

                        horizontal += _holder[i].Width;

                        _holder[i].Visible = true;

                        Controls.Add(_fourthBotPanel);

                        _fourthBotPanel.Location = new Point(
                            _holder[8].Left - 10,
                            _holder[8].Top - 10);
                        _fourthBotPanel.BackColor = Color.DarkBlue;
                        _fourthBotPanel.Height = 150;
                        _fourthBotPanel.Width = 180;
                        _fourthBotPanel.Visible = false;

                        if (i == 9)
                        {
                            check = false;
                        }
                    }
                }

                if (FifthBotChips > 0)
                {
                    _foldedPlayers--;

                    if (i >= 10 && i < 12)
                    {
                        if (_holder[10].Tag != null)
                        {
                            _holder[11].Tag = _reserve[11];
                        }

                        _holder[10].Tag = _reserve[10];

                        if (!check)
                        {
                            horizontal = 1160;
                            vertical = 420;
                        }

                        check = true;

                        _holder[i].Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
                        _holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];
                        _holder[i].Location = new Point(horizontal, vertical);

                        horizontal += _holder[i].Width;

                        _holder[i].Visible = true;

                        Controls.Add(_fifthBotPanel);

                        _fifthBotPanel.Location = new Point(
                            _holder[10].Left - 10,
                            _holder[10].Top - 10);
                        _fifthBotPanel.BackColor = Color.DarkBlue;
                        _fifthBotPanel.Height = 150;
                        _fifthBotPanel.Width = 180;
                        _fifthBotPanel.Visible = false;

                        if (i == 11)
                        {
                            check = false;
                        }
                    }
                }

                if (i >= 12)
                {
                    _holder[12].Tag = _reserve[12];

                    if (i > 12)
                    {
                        _holder[13].Tag = _reserve[13];
                    }

                    if (i > 13)
                    {
                        _holder[14].Tag = _reserve[14];
                    }

                    if (i > 14)
                    {
                        _holder[15].Tag = _reserve[15];
                    }

                    if (i > 15)
                    {
                        _holder[16].Tag = _reserve[16];
                    }

                    if (!check)
                    {
                        horizontal = 410;
                        vertical = 265;
                    }

                    check = true;

                    if (_holder[i] != null)
                    {
                        _holder[i].Anchor = AnchorStyles.None;
                        _holder[i].Image = backImage;
                        //Holder[i].Image = Deck[i];

                        _holder[i].Location = new Point(horizontal, vertical);

                        horizontal += 110;
                    }
                }

                #endregion

                if (FirstBotChips <= 0)
                {
                    _firstBotFoldedTurn = true;

                    _holder[2].Visible = false;
                    _holder[3].Visible = false;
                }
                else
                {
                    _firstBotFoldedTurn = false;

                    if (i == 3)
                    {
                        if (_holder[3] != null)
                        {
                            _holder[2].Visible = true;
                            _holder[3].Visible = true;
                        }
                    }
                }

                if (SecondBotChips <= 0)
                {
                    _secodBotFoldedTurn = true;

                    _holder[4].Visible = false;
                    _holder[5].Visible = false;
                }
                else
                {
                    _secodBotFoldedTurn = false;

                    if (i == 5)
                    {
                        if (_holder[5] != null)
                        {
                            _holder[4].Visible = true;
                            _holder[5].Visible = true;
                        }
                    }
                }

                if (ThirdBotChips <= 0)
                {
                    _thirdBotFoldedTurn = true;

                    _holder[6].Visible = false;
                    _holder[7].Visible = false;
                }
                else
                {
                    _thirdBotFoldedTurn = false;

                    if (i == 7)
                    {
                        if (_holder[7] != null)
                        {
                            _holder[6].Visible = true;
                            _holder[7].Visible = true;
                        }
                    }
                }

                if (FourthBotChips <= 0)
                {
                    _fourthBotFoldedTurn = true;

                    _holder[8].Visible = false;
                    _holder[9].Visible = false;
                }
                else
                {
                    _fourthBotFoldedTurn = false;

                    if (i == 9)
                    {
                        if (_holder[9] != null)
                        {
                            _holder[8].Visible = true;
                            _holder[9].Visible = true;
                        }
                    }
                }

                if (FifthBotChips <= 0)
                {
                    _fifthBotFoldedTurn = true;

                    _holder[10].Visible = false;
                    _holder[11].Visible = false;
                }
                else
                {
                    _fifthBotFoldedTurn = false;

                    if (i == 11)
                    {
                        if (_holder[11] != null)
                        {
                            _holder[10].Visible = true;
                            _holder[11].Visible = true;
                        }
                    }
                }

                if (i != 16)
                {
                    continue;
                }

                if (!_restart)
                {
                    MaximizeBox = true;
                    MinimizeBox = true;
                }

                _timer.Start();
            }

            if (_foldedPlayers == 5)
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
                        break;
                    case DialogResult.Ignore:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                _foldedPlayers = 5;
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

            if (!_foldedPlayerTurn)
            {
                if (_playerTurn)
                {
                    FixCall(
                        playerStatus,
                        ref _playerCall,
                        ref _playerRaise,
                        1);

                    //MessageBox.Show("Player's Turn");

                    progressBarTimer.Visible = true;
                    progressBarTimer.Value = 1000;

                    _timeTillNextTurn = 60;
                    up = 10000000;
                    _timer.Start();

                    buttonRaise.Enabled = true;
                    buttonCall.Enabled = true;
                    buttonRaise.Enabled = true;
                    buttonRaise.Enabled = true;
                    buttonFold.Enabled = true;

                    _turnCount++;

                    FixCall(
                        playerStatus,
                        ref _playerCall,
                        ref _playerRaise,
                        2);
                }
            }

            if (_foldedPlayerTurn || !_playerTurn)
            {
                await AllIn();

                if (_foldedPlayerTurn && !_playerFolded)
                {
                    if (buttonCall.Text.Contains("All in") == false 
                        || buttonRaise.Text.Contains("All in") == false)
                    {
                        _bools.RemoveAt(0);
                        _bools.Insert(0, null);

                        _maxLeft--;

                        _playerFolded = true;
                    }
                }

                await CheckRaise(0, 0);

                progressBarTimer.Visible = false;

                buttonRaise.Enabled = false;
                buttonCall.Enabled = false;
                buttonRaise.Enabled = false;
                buttonRaise.Enabled = false;
                buttonFold.Enabled = false;

                _timer.Stop();

                _firstBotTurn = true;

                if (!_firstBotFoldedTurn)
                {
                    if (_firstBotTurn)
                    {
                        FixCall(
                            firstBotStatus,
                            ref _firstBotCall,
                            ref _firstBotRaise, 1);
                        FixCall(
                            firstBotStatus,
                            ref _firstBotCall,
                            ref _firstBotRaise, 2);

                        Rules(
                            2,
                            3,
                            "Bot 1",
                            ref _firstBotType,
                            ref _firstBotPower,
                            _firstBotFoldedTurn);

                        MessageBox.Show(@"Bot 1's Turn");

                        AI(
                            2,
                            3,
                            ref FirstBotChips,
                            ref _firstBotTurn,
                            ref _firstBotFoldedTurn,
                            firstBotStatus,
                            0,
                            _firstBotPower,
                            _firstBotType);

                        _turnCount++;

                        _last = 1;

                        _firstBotTurn = false;
                        _secondBotTurn = true;
                    }
                }

                if (_firstBotFoldedTurn && !_firstBotFolded)
                {
                    _bools.RemoveAt(1);
                    _bools.Insert(1, null);

                    _maxLeft--;

                    _firstBotFolded = true;
                }

                if (_firstBotFoldedTurn || !_firstBotTurn)
                {
                    await CheckRaise(1, 1);

                    _secondBotTurn = true;
                }

                if (!_secodBotFoldedTurn)
                {
                    if (_secondBotTurn)
                    {
                        FixCall(
                            secondBotStatus,
                            ref _secondBotCall,
                            ref _secondBotRaise,
                            1);
                        FixCall(
                            secondBotStatus,
                            ref _secondBotCall,
                            ref _secondBotRaise,
                            2);

                        Rules(
                            4,
                            5,
                            "Bot 2",
                            ref _secondBotType,
                            ref _secondBotPower,
                            _secodBotFoldedTurn);

                        MessageBox.Show(@"Bot 2's Turn");

                        AI(
                            4,
                            5,
                            ref SecondBotChips,
                            ref _secondBotTurn,
                            ref _secodBotFoldedTurn,
                            secondBotStatus,
                            1,
                            _secondBotPower,
                            _secondBotType);

                        _turnCount++;

                        _last = 2;

                        _secondBotTurn = false;
                        _thirdBotTurn = true;
                    }
                }

                if (_secodBotFoldedTurn && !_secondBotFolded)
                {
                    _bools.RemoveAt(2);
                    _bools.Insert(2, null);

                    _maxLeft--;

                    _secondBotFolded = true;
                }

                if (_secodBotFoldedTurn || !_secondBotTurn)
                {
                    await CheckRaise(2, 2);

                    _thirdBotTurn = true;
                }

                if (!_thirdBotFoldedTurn)
                {
                    if (_thirdBotTurn)
                    {
                        FixCall(
                            thirdBotStatus,
                            ref _thirdBotCall,
                            ref _thirdBotRaise, 1);
                        FixCall(
                            thirdBotStatus,
                            ref _thirdBotCall,
                            ref _thirdBotRaise, 2);

                        Rules(
                            6,
                            7,
                            "Bot 3",
                            ref _thirdBotType,
                            ref _thirdBotPower,
                            _thirdBotFoldedTurn);

                        MessageBox.Show(@"Bot 3's Turn");

                        AI(
                            6,
                            7,
                            ref ThirdBotChips,
                            ref _thirdBotTurn,
                            ref _thirdBotFoldedTurn,
                            thirdBotStatus,
                            2,
                            _thirdBotPower,
                            _thirdBotType);

                        _turnCount++;

                        _last = 3;

                        _thirdBotTurn = false;
                        _fourthBotTurn = true;
                    }
                }

                if (_thirdBotFoldedTurn && !_thirdBotFolded)
                {
                    _bools.RemoveAt(3);
                    _bools.Insert(3, null);

                    _maxLeft--;

                    _thirdBotFolded = true;
                }

                if (_thirdBotFoldedTurn || !_thirdBotTurn)
                {
                    await CheckRaise(3, 3);

                    _fourthBotTurn = true;
                }

                if (!_fourthBotFoldedTurn)
                {
                    if (_fourthBotTurn)
                    {
                        FixCall(
                            fourthBotStatus,
                            ref _fourthBotCall,
                            ref _fourthBotRaise, 1);
                        FixCall(
                            fourthBotStatus,
                            ref _fourthBotCall,
                            ref _fourthBotRaise, 2);

                        Rules(
                            8,
                            9,
                            "Bot 4",
                            ref _fourthBotType,
                            ref _fourthBotPower,
                            _fourthBotFoldedTurn);

                        MessageBox.Show(@"Bot 4's Turn");

                        AI(
                            8,
                            9,
                            ref FourthBotChips,
                            ref _fourthBotTurn,
                            ref _fourthBotFoldedTurn,
                            fourthBotStatus,
                            3,
                            _fourthBotPower,
                            _fourthBotType);

                        _turnCount++;

                        _last = 4;

                        _fourthBotTurn = false;
                        _fifthBotTurn = true;
                    }
                }

                if (_fourthBotFoldedTurn && !_fourthBotFolded)
                {
                    _bools.RemoveAt(4);
                    _bools.Insert(4, null);

                    _maxLeft--;

                    _fourthBotFolded = true;
                }

                if (_fourthBotFoldedTurn || !_fourthBotTurn)
                {
                    await CheckRaise(4, 4);

                    _fifthBotTurn = true;
                }

                if (!_fifthBotFoldedTurn)
                {
                    if (_fifthBotTurn)
                    {
                        FixCall(
                            fifthBotStatus,
                            ref _fifthBotCall,
                            ref _fifthBotRaise,
                            1);
                        FixCall(
                            fifthBotStatus,
                            ref _fifthBotCall,
                            ref _fifthBotRaise,
                            2);

                        Rules(
                            10,
                            11,
                            "Bot 5",
                            ref _fifthBotType,
                            ref _fifthBotPower,
                            _fifthBotFoldedTurn);

                        MessageBox.Show(@"Bot 5's Turn");

                        AI(
                            10,
                            11,
                            ref FifthBotChips,
                            ref _fifthBotTurn,
                            ref _fifthBotFoldedTurn,
                            fifthBotStatus,
                            4,
                            _fifthBotPower,
                            _fifthBotType);

                        _turnCount++;

                        _last = 5;

                        _fifthBotTurn = false;
                    }
                }

                if (_fifthBotFoldedTurn && !_fifthBotFolded)
                {
                    _bools.RemoveAt(5);
                    _bools.Insert(5, null);

                    _maxLeft--;

                    _fifthBotFolded = true;
                }

                if (_fifthBotFoldedTurn || !_fifthBotTurn)
                {
                    await CheckRaise(5, 5);

                    _playerTurn = true;
                }

                if (_foldedPlayerTurn && !_playerFolded)
                {
                    if (buttonCall.Text.Contains("All in") == false 
                        || buttonRaise.Text.Contains("All in") == false)
                    {
                        _bools.RemoveAt(0);
                        _bools.Insert(0, null);

                        _maxLeft--;

                        _playerFolded = true;
                    }
                }

                #endregion

                await AllIn();

                if (!_restart)
                {
                    await Turns();
                }

                _restart = false;
            }
        }

        private void Rules(
            int c1,
            int c2,
            string currentText,
            ref double current,
            ref double power,
            bool foldedTurn)
        {
            if (c1 == 0 && c2 == 1)
            {
            }

            if (!foldedTurn || c1 == 0 
                && c2 == 1 
                && playerStatus.Text.Contains("Fold") == false)
            {
                #region Variables

                var done = false;

                var vf = false;

                var straight1 = new int[5];
                var straight = new int[7];

                straight[0] = _reserve[c1];
                straight[1] = _reserve[c2];
                straight1[0] = straight[2] = _reserve[12];
                straight1[1] = straight[3] = _reserve[13];
                straight1[2] = straight[4] = _reserve[14];
                straight1[3] = straight[5] = _reserve[15];
                straight1[4] = straight[6] = _reserve[16];

                var a = straight.Where(o => o%4 == 0).ToArray();
                var b = straight.Where(o => o%4 == 1).ToArray();
                var c = straight.Where(o => o%4 == 2).ToArray();
                var d = straight.Where(o => o%4 == 3).ToArray();

                var st1 = a.Select(o => o/4).Distinct().ToArray();
                var st2 = b.Select(o => o/4).Distinct().ToArray();
                var st3 = c.Select(o => o/4).Distinct().ToArray();
                var st4 = d.Select(o => o/4).Distinct().ToArray();

                Array.Sort(straight);
                Array.Sort(st1);
                Array.Sort(st2);
                Array.Sort(st3);
                Array.Sort(st4);

                #endregion

                for (i = 0; i < 16; i++)
                {
                    if (_reserve[i] == int.Parse(_holder[c1].Tag.ToString()) 
                        && _reserve[i + 1] == int.Parse(_holder[c2].Tag.ToString()))
                    {
                        //Pair from Hand current = 1

                        rPairFromHand(ref current, ref power);

                        #region Pair or Two Pair from Table current = 2 || 0

                        rPairTwoPair(ref current, ref power);

                        #endregion

                        #region Two Pair current = 2

                        rTwoPair(ref current, ref power);

                        #endregion

                        #region Three of a kind current = 3

                        rThreeOfAKind(ref current, ref power, straight);

                        #endregion

                        #region Straight current = 4

                        rStraight(ref current, ref power, straight);

                        #endregion

                        #region Flush current = 5 || 5.5

                        RFlush(ref current, ref power, ref vf, straight1);

                        #endregion

                        #region Full House current = 6

                        RFullHouse(ref current, ref power, ref done, straight);

                        #endregion

                        #region Four of a Kind current = 7

                        RFourOfAKind(ref current, ref power, straight);

                        #endregion

                        #region Straight Flush current = 8 || 9

                        RStraightFlush(ref current, ref power, st1, st2, st3, st4);

                        #endregion

                        #region High Card current = -1

                        rHighCard(ref current, ref power);

                        #endregion
                    }
                }
            }
        }

        private void RStraightFlush(
            ref double current,
            ref double power,
            IReadOnlyList<int> st1,
            IReadOnlyList<int> st2,
            IReadOnlyList<int> st3,
            IReadOnlyList<int> st4)
        {
            if (current >= -1)
            {
                if (st1.Count >= 5)
                {
                    if (st1[0] + 4 == st1[4])
                    {
                        current = 8;

                        var max = st1.Concat(new[] {int.MinValue}).Max();

                        power = (max)/4 + current*100;

                        _win.Add(new Type() {Power = power, Current = 8});

                        _sorted = 
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (st1[0] == 0 
                        && st1[1] == 9 
                        && st1[2] == 10 
                        && st1[3] == 11 
                        && st1[0] + 12 == st1[4])
                    {
                        current = 9;

                        power = (st1.Max())/4 + current*100;

                        _win.Add(new Type() {Power = power, Current = 9});

                        _sorted = 
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (st2.Count >= 5)
                {
                    if (st2[0] + 4 == st2[4])
                    {
                        current = 8;

                        power = (st2.Max())/4 + current*100;

                        _win.Add(new Type() {Power = power, Current = 8});

                        _sorted = 
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (st2[0] == 0 
                        && st2[1] == 9 
                        && st2[2] == 10 
                        && st2[3] == 11 
                        && st2[0] + 12 == st2[4])
                    {
                        current = 9;

                        power = (st2.Max())/4 + current*100;

                        _win.Add(new Type() {Power = power, Current = 9});

                        _sorted = 
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (st3.Count >= 5)
                {
                    if (st3[0] + 4 == st3[4])
                    {
                        current = 8;

                        power = (st3.Max())/4 + current*100;

                        _win.Add(new Type() {Power = power, Current = 8});

                        _sorted =
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (st3[0] == 0 
                        && st3[1] == 9 
                        && st3[2] == 10 
                        && st3[3] == 11 
                        && st3[0] + 12 == st3[4])
                    {
                        current = 9;

                        power = (st3.Max())/4 + current*100;

                        _win.Add(new Type() {Power = power, Current = 9});

                        _sorted = 
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (st4.Count >= 5)
                {
                    if (st4[0] + 4 == st4[4])
                    {
                        current = 8;

                        power = (st4.Max())/4 + current*100;

                        _win.Add(new Type() {Power = power, Current = 8});

                        _sorted =
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (st4[0] == 0 
                        && st4[1] == 9 
                        && st4[2] == 10 
                        && st4[3] == 11 
                        && st4[0] + 12 == st4[4])
                    {
                        current = 9;

                        power = (st4.Max())/4 + current*100;

                        _win.Add(new Type() {Power = power, Current = 9});

                        _sorted =
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }
            }
        }

        private void RFourOfAKind(
            ref double current,
            ref double power,
            IReadOnlyList<int> straight)
        {
            if (current >= -1)
            {
                for (var j = 0; j <= 3; j++)
                {
                    if (straight[j]/4 == straight[j + 1]/4 
                        && straight[j]/4 == straight[j + 2]/4 
                        && straight[j]/4 == straight[j + 3]/4)
                    {
                        current = 7;

                        power = (straight[j]/4)*4 + current*100;

                        _win.Add(new Type() {Power = power, Current = 7});

                        _sorted =
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (straight[j]/4 == 0 
                        && straight[j + 1]/4 == 0 
                        && straight[j + 2]/4 == 0 
                        && straight[j + 3]/4 == 0)
                    {
                        current = 7;

                        power = 13*4 + current*100;

                        _win.Add(new Type() {Power = power, Current = 7});

                        _sorted =
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }
            }
        }

        private void RFullHouse(
            ref double current,
            ref double power,
            ref bool done,
            int[] straight)
        {
            if (current >= -1)
            {
                _type = power;
                for (var j = 0; j <= 12; j++)
                {
                    var fh = straight.Where(o => o/4 == j).ToArray();

                    if (fh.Length == 3 || done)
                    {
                        if (fh.Length == 2)
                        {
                            if (fh.Max()/4 == 0)
                            {
                                current = 6;

                                power = 13*2 + current*100;

                                _win.Add(new Type() {Power = power, Current = 6});

                                _sorted =
                                    _win
                                    .OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();

                                break;
                            }

                            if (fh.Max()/4 > 0)
                            {
                                current = 6;

                                power = fh.Max()/4*2 + current*100;

                                _win.Add(new Type() {Power = power, Current = 6});

                                _sorted = 
                                    _win
                                    .OrderByDescending(op1 => op1.Current)
                                    .ThenByDescending(op1 => op1.Power)
                                    .First();

                                break;
                            }
                        }

                        if (!done)
                        {
                            if (fh.Max()/4 == 0)
                            {
                                power = 13;

                                done = true;

                                j = -1;
                            }
                            else
                            {
                                power = fh.Max()/4;

                                done = true;

                                j = -1;
                            }
                        }
                    }
                }

                if (current != 6)
                {
                    power = _type;
                }
            }
        }

        private void RFlush(
            ref double current,
            ref double power,
            ref bool vf,
            int[] straight1)
        {
            if (current >= -1)
            {
                var f1 = straight1.Where(o => o%4 == 0).ToArray();
                var f2 = straight1.Where(o => o%4 == 1).ToArray();
                var f3 = straight1.Where(o => o%4 == 2).ToArray();
                var f4 = straight1.Where(o => o%4 == 3).ToArray();

                if (f1.Length == 3 || f1.Length == 4)
                {
                    if (_reserve[i]%4 == _reserve[i + 1]%4 && _reserve[i]%4 == f1[0]%4)
                    {
                        if (_reserve[i]/4 > f1.Max()/4)
                        {
                            current = 5;

                            power = _reserve[i] + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted = 
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }

                        if (_reserve[i + 1]/4 > f1.Max()/4)
                        {
                            current = 5;

                            power = _reserve[i + 1] + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted = 
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else if (_reserve[i]/4 < f1.Max()/4 && _reserve[i + 1]/4 < f1.Max()/4)
                        {
                            current = 5;

                            power = f1.Max() + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted =
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }
                }

                if (f1.Length == 4) //different cards in hand
                {
                    if (_reserve[i]%4 != _reserve[i + 1]%4 && _reserve[i]%4 == f1[0]%4)
                    {
                        if (_reserve[i]/4 > f1.Max()/4)
                        {
                            current = 5;

                            power = _reserve[i] + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted =
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            current = 5;

                            power = f1.Max() + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted = 
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }

                    if (_reserve[i + 1]%4 != _reserve[i]%4 && _reserve[i + 1]%4 == f1[0]%4)
                    {
                        if (_reserve[i + 1]/4 > f1.Max()/4)
                        {
                            current = 5;

                            power = _reserve[i + 1] + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted =
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            current = 5;

                            power = f1.Max() + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted = 
                                _win.OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }
                }

                if (f1.Length == 5)
                {
                    if (_reserve[i]%4 == f1[0]%4 && _reserve[i]/4 > f1.Min()/4)
                    {
                        current = 5;

                        power = _reserve[i] + current*100;

                        _win.Add(new Type() {Power = power, Current = 5});

                        _sorted = 
                            _win.OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }

                    if (_reserve[i + 1]%4 == f1[0]%4 && _reserve[i + 1]/4 > f1.Min()/4)
                    {
                        current = 5;

                        power = _reserve[i + 1] + current*100;

                        _win.Add(new Type() {Power = power, Current = 5});

                        _sorted = 
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }
                    else if (_reserve[i]/4 < f1.Min()/4 && _reserve[i + 1]/4 < f1.Min())
                    {
                        current = 5;

                        power = f1.Max() + current*100;

                        _win.Add(new Type() {Power = power, Current = 5});

                        _sorted = 
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }
                }

                if (f2.Length == 3 || f2.Length == 4)
                {
                    if (_reserve[i]%4 == _reserve[i + 1]%4 && _reserve[i]%4 == f2[0]%4)
                    {
                        if (_reserve[i]/4 > f2.Max()/4)
                        {
                            current = 5;

                            power = _reserve[i] + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted =
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }

                        if (_reserve[i + 1]/4 > f2.Max()/4)
                        {
                            current = 5;

                            power = _reserve[i + 1] + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted = 
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else if (_reserve[i]/4 < f2.Max()/4 && _reserve[i + 1]/4 < f2.Max()/4)
                        {
                            current = 5;

                            power = f2.Max() + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted = 
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }
                }

                if (f2.Length == 4) //different cards in hand
                {
                    if (_reserve[i]%4 != _reserve[i + 1]%4 && _reserve[i]%4 == f2[0]%4)
                    {
                        if (_reserve[i]/4 > f2.Max()/4)
                        {
                            current = 5;

                            power = _reserve[i] + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted = 
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            current = 5;

                            power = f2.Max() + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted = 
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }

                    if (_reserve[i + 1]%4 != _reserve[i]%4 && _reserve[i + 1]%4 == f2[0]%4)
                    {
                        if (_reserve[i + 1]/4 > f2.Max()/4)
                        {
                            current = 5;

                            power = _reserve[i + 1] + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted = 
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            current = 5;

                            power = f2.Max() + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted = 
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }
                }

                if (f2.Length == 5)
                {
                    if (_reserve[i]%4 == f2[0]%4 && _reserve[i]/4 > f2.Min()/4)
                    {
                        current = 5;

                        power = _reserve[i] + current*100;

                        _win.Add(new Type() {Power = power, Current = 5});

                        _sorted =
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }

                    if (_reserve[i + 1]%4 == f2[0]%4 && _reserve[i + 1]/4 > f2.Min()/4)
                    {
                        current = 5;

                        power = _reserve[i + 1] + current*100;

                        _win.Add(new Type() {Power = power, Current = 5});

                        _sorted = 
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }
                    else if (_reserve[i]/4 < f2.Min()/4 && _reserve[i + 1]/4 < f2.Min())
                    {
                        current = 5;

                        power = f2.Max() + current*100;

                        _win.Add(new Type() {Power = power, Current = 5});

                        _sorted = 
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }
                }

                if (f3.Length == 3 || f3.Length == 4)
                {
                    if (_reserve[i]%4 == _reserve[i + 1]%4 && _reserve[i]%4 == f3[0]%4)
                    {
                        if (_reserve[i]/4 > f3.Max()/4)
                        {
                            current = 5;

                            power = _reserve[i] + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted = 
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }

                        if (_reserve[i + 1]/4 > f3.Max()/4)
                        {
                            current = 5;

                            power = _reserve[i + 1] + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted =
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else if (_reserve[i]/4 < f3.Max()/4 && _reserve[i + 1]/4 < f3.Max()/4)
                        {
                            current = 5;

                            power = f3.Max() + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted = 
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }
                }

                if (f3.Length == 4) //different cards in hand
                {
                    if (_reserve[i]%4 != _reserve[i + 1]%4 && _reserve[i]%4 == f3[0]%4)
                    {
                        if (_reserve[i]/4 > f3.Max()/4)
                        {
                            current = 5;

                            power = _reserve[i] + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted = 
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            current = 5;

                            power = f3.Max() + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted = 
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }

                    if (_reserve[i + 1]%4 != _reserve[i]%4 && _reserve[i + 1]%4 == f3[0]%4)
                    {
                        if (_reserve[i + 1]/4 > f3.Max()/4)
                        {
                            current = 5;

                            power = _reserve[i + 1] + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted = 
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            current = 5;

                            power = f3.Max() + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted = 
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }
                }

                if (f3.Length == 5)
                {
                    if (_reserve[i]%4 == f3[0]%4 && _reserve[i]/4 > f3.Min()/4)
                    {
                        current = 5;

                        power = _reserve[i] + current*100;

                        _win.Add(new Type() {Power = power, Current = 5});

                        _sorted = 
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }

                    if (_reserve[i + 1]%4 == f3[0]%4 && _reserve[i + 1]/4 > f3.Min()/4)
                    {
                        current = 5;

                        power = _reserve[i + 1] + current*100;

                        _win.Add(new Type() {Power = power, Current = 5});

                        _sorted =
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }
                    else if (_reserve[i]/4 < f3.Min()/4 && _reserve[i + 1]/4 < f3.Min())
                    {
                        current = 5;

                        power = f3.Max() + current*100;

                        _win.Add(new Type() {Power = power, Current = 5});

                        _sorted = 
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }
                }

                if (f4.Length == 3 || f4.Length == 4)
                {
                    if (_reserve[i]%4 == _reserve[i + 1]%4 && _reserve[i]%4 == f4[0]%4)
                    {
                        if (_reserve[i]/4 > f4.Max()/4)
                        {
                            current = 5;

                            power = _reserve[i] + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted =
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }

                        if (_reserve[i + 1]/4 > f4.Max()/4)
                        {
                            current = 5;

                            power = _reserve[i + 1] + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted =
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else if (_reserve[i]/4 < f4.Max()/4 && _reserve[i + 1]/4 < f4.Max()/4)
                        {
                            current = 5;

                            power = f4.Max() + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted = 
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }
                }

                if (f4.Length == 4) //different cards in hand
                {
                    if (_reserve[i]%4 != _reserve[i + 1]%4 && _reserve[i]%4 == f4[0]%4)
                    {
                        if (_reserve[i]/4 > f4.Max()/4)
                        {
                            current = 5;

                            power = _reserve[i] + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted = 
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            current = 5;

                            power = f4.Max() + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted = 
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }

                    if (_reserve[i + 1]%4 != _reserve[i]%4 && _reserve[i + 1]%4 == f4[0]%4)
                    {
                        if (_reserve[i + 1]/4 > f4.Max()/4)
                        {
                            current = 5;

                            power = _reserve[i + 1] + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted = 
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                        else
                        {
                            current = 5;

                            power = f4.Max() + current*100;

                            _win.Add(new Type() {Power = power, Current = 5});

                            _sorted = 
                                _win
                                .OrderByDescending(op1 => op1.Current)
                                .ThenByDescending(op1 => op1.Power)
                                .First();

                            vf = true;
                        }
                    }
                }

                if (f4.Length == 5)
                {
                    if (_reserve[i]%4 == f4[0]%4 && _reserve[i]/4 > f4.Min()/4)
                    {
                        current = 5;

                        power = _reserve[i] + current*100;

                        _win.Add(new Type() {Power = power, Current = 5});

                        _sorted = 
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }

                    if (_reserve[i + 1]%4 == f4[0]%4 && _reserve[i + 1]/4 > f4.Min()/4)
                    {
                        current = 5;

                        power = _reserve[i + 1] + current*100;

                        _win.Add(new Type() {Power = power, Current = 5});

                        _sorted = 
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }
                    else if (_reserve[i]/4 < f4.Min()/4 && _reserve[i + 1]/4 < f4.Min())
                    {
                        current = 5;

                        power = f4.Max() + current*100;

                        _win.Add(new Type() {Power = power, Current = 5});

                        _sorted = 
                            _win.
                            OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();

                        vf = true;
                    }
                }
                //ace
                if (f1.Length > 0)
                {
                    if (_reserve[i]/4 == 0 && _reserve[i]%4 == f1[0]%4 && vf && f1.Length > 0)
                    {
                        current = 5.5;

                        power = 13 + current*100;

                        _win.Add(new Type() {Power = power, Current = 5.5});

                        _sorted = 
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (_reserve[i + 1]/4 == 0 && _reserve[i + 1]%4 == f1[0]%4 && vf && f1.Length > 0)
                    {
                        current = 5.5;

                        power = 13 + current*100;

                        _win.Add(new Type() {Power = power, Current = 5.5});

                        _sorted = 
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (f2.Length > 0)
                {
                    if (_reserve[i]/4 == 0 && _reserve[i]%4 == f2[0]%4 && vf && f2.Length > 0)
                    {
                        current = 5.5;

                        power = 13 + current*100;

                        _win.Add(new Type() {Power = power, Current = 5.5});

                        _sorted = 
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (_reserve[i + 1]/4 == 0 && _reserve[i + 1]%4 == f2[0]%4 && vf && f2.Length > 0)
                    {
                        current = 5.5;

                        power = 13 + current*100;

                        _win.Add(new Type() {Power = power, Current = 5.5});

                        _sorted = 
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (f3.Length > 0)
                {
                    if (_reserve[i]/4 == 0 && _reserve[i]%4 == f3[0]%4 && vf && f3.Length > 0)
                    {
                        current = 5.5;

                        power = 13 + current*100;

                        _win.Add(new Type() {Power = power, Current = 5.5});

                        _sorted = 
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (_reserve[i + 1]/4 == 0 && _reserve[i + 1]%4 == f3[0]%4 && vf && f3.Length > 0)
                    {
                        current = 5.5;

                        power = 13 + current*100;

                        _win.Add(new Type() {Power = power, Current = 5.5});

                        _sorted =
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }

                if (f4.Length > 0)
                {
                    if (_reserve[i]/4 == 0 && _reserve[i]%4 == f4[0]%4 && vf && f4.Length > 0)
                    {
                        current = 5.5;

                        power = 13 + current*100;

                        _win.Add(new Type() {Power = power, Current = 5.5});

                        _sorted = 
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }

                    if (_reserve[i + 1]/4 == 0 && _reserve[i + 1]%4 == f4[0]%4 && vf)
                    {
                        current = 5.5;

                        power = 13 + current*100;

                        _win.Add(new Type() {Power = power, Current = 5.5});

                        _sorted = 
                            _win
                            .OrderByDescending(op1 => op1.Current)
                            .ThenByDescending(op1 => op1.Power)
                            .First();
                    }
                }
            }
        }

        private void rStraight(ref double current, ref double Power, int[] Straight)
        {
            if (current >= -1)
            {
                var op = Straight.Select(o => o/4).Distinct().ToArray();
                for (int j = 0; j < op.Length - 4; j++)
                {
                    if (op[j] + 4 == op[j + 4])
                    {
                        if (op.Max() - 4 == op[j])
                        {
                            current = 4;
                            Power = op.Max() + current*100;
                            _win.Add(new Type() {Power = Power, Current = 4});
                            _sorted = _win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                        else
                        {
                            current = 4;
                            Power = op[j + 4] + current*100;
                            _win.Add(new Type() {Power = Power, Current = 4});
                            _sorted = _win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                        }
                    }
                    if (op[j] == 0 && op[j + 1] == 9 && op[j + 2] == 10 && op[j + 3] == 11 && op[j + 4] == 12)
                    {
                        current = 4;
                        Power = 13 + current*100;
                        _win.Add(new Type() {Power = Power, Current = 4});
                        _sorted = _win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                    }
                }
            }
        }

        private void rThreeOfAKind(ref double current, ref double Power, int[] Straight)
        {
            if (current >= -1)
            {
                for (int j = 0; j <= 12; j++)
                {
                    var fh = Straight.Where(o => o/4 == j).ToArray();
                    if (fh.Length == 3)
                    {
                        if (fh.Max()/4 == 0)
                        {
                            current = 3;
                            Power = 13*3 + current*100;
                            _win.Add(new Type() {Power = Power, Current = 3});
                            _sorted = _win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            current = 3;
                            Power = fh[0]/4 + fh[1]/4 + fh[2]/4 + current*100;
                            _win.Add(new Type() {Power = Power, Current = 3});
                            _sorted = _win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                }
            }
        }

        private void rTwoPair(ref double current, ref double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    if (_reserve[i]/4 != _reserve[i + 1]/4)
                    {
                        for (int k = 1; k <= max; k++)
                        {
                            if (tc - k < 12)
                            {
                                max--;
                            }
                            if (tc - k >= 12)
                            {
                                if (_reserve[i]/4 == _reserve[tc]/4 && _reserve[i + 1]/4 == _reserve[tc - k]/4 || _reserve[i + 1]/4 == _reserve[tc]/4 && _reserve[i]/4 == _reserve[tc - k]/4)
                                {
                                    if (!msgbox)
                                    {
                                        if (_reserve[i]/4 == 0)
                                        {
                                            current = 2;
                                            Power = 13*4 + (_reserve[i + 1]/4)*2 + current*100;
                                            _win.Add(new Type() {Power = Power, Current = 2});
                                            _sorted = _win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (_reserve[i + 1]/4 == 0)
                                        {
                                            current = 2;
                                            Power = 13*4 + (_reserve[i]/4)*2 + current*100;
                                            _win.Add(new Type() {Power = Power, Current = 2});
                                            _sorted = _win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (_reserve[i + 1]/4 != 0 && _reserve[i]/4 != 0)
                                        {
                                            current = 2;
                                            Power = (_reserve[i]/4)*2 + (_reserve[i + 1]/4)*2 + current*100;
                                            _win.Add(new Type() {Power = Power, Current = 2});
                                            _sorted = _win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                    }
                                    msgbox = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void rPairTwoPair(ref double current, ref double Power)
        {
            if (current >= -1)
            {
                bool msgbox = false;
                bool msgbox1 = false;
                for (int tc = 16; tc >= 12; tc--)
                {
                    int max = tc - 12;
                    for (int k = 1; k <= max; k++)
                    {
                        if (tc - k < 12)
                        {
                            max--;
                        }
                        if (tc - k >= 12)
                        {
                            if (_reserve[tc]/4 == _reserve[tc - k]/4)
                            {
                                if (_reserve[tc]/4 != _reserve[i]/4 && _reserve[tc]/4 != _reserve[i + 1]/4 && current == 1)
                                {
                                    if (!msgbox)
                                    {
                                        if (_reserve[i + 1]/4 == 0)
                                        {
                                            current = 2;
                                            Power = (_reserve[i]/4)*2 + 13*4 + current*100;
                                            _win.Add(new Type() {Power = Power, Current = 2});
                                            _sorted = _win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (_reserve[i]/4 == 0)
                                        {
                                            current = 2;
                                            Power = (_reserve[i + 1]/4)*2 + 13*4 + current*100;
                                            _win.Add(new Type() {Power = Power, Current = 2});
                                            _sorted = _win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (_reserve[i + 1]/4 != 0)
                                        {
                                            current = 2;
                                            Power = (_reserve[tc]/4)*2 + (_reserve[i + 1]/4)*2 + current*100;
                                            _win.Add(new Type() {Power = Power, Current = 2});
                                            _sorted = _win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                        if (_reserve[i]/4 != 0)
                                        {
                                            current = 2;
                                            Power = (_reserve[tc]/4)*2 + (_reserve[i]/4)*2 + current*100;
                                            _win.Add(new Type() {Power = Power, Current = 2});
                                            _sorted = _win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                        }
                                    }
                                    msgbox = true;
                                }
                                if (current == -1)
                                {
                                    if (!msgbox1)
                                    {
                                        if (_reserve[i]/4 > _reserve[i + 1]/4)
                                        {
                                            if (_reserve[tc]/4 == 0)
                                            {
                                                current = 0;
                                                Power = 13 + _reserve[i]/4 + current*100;
                                                _win.Add(new Type() {Power = Power, Current = 1});
                                                _sorted = _win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = _reserve[tc]/4 + _reserve[i]/4 + current*100;
                                                _win.Add(new Type() {Power = Power, Current = 1});
                                                _sorted = _win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                        }
                                        else
                                        {
                                            if (_reserve[tc]/4 == 0)
                                            {
                                                current = 0;
                                                Power = 13 + _reserve[i + 1] + current*100;
                                                _win.Add(new Type() {Power = Power, Current = 1});
                                                _sorted = _win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                            else
                                            {
                                                current = 0;
                                                Power = _reserve[tc]/4 + _reserve[i + 1]/4 + current*100;
                                                _win.Add(new Type() {Power = Power, Current = 1});
                                                _sorted = _win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                                            }
                                        }
                                    }
                                    msgbox1 = true;
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
                if (_reserve[i]/4 == _reserve[i + 1]/4)
                {
                    if (!msgbox)
                    {
                        if (_reserve[i]/4 == 0)
                        {
                            current = 1;
                            Power = 13*4 + current*100;
                            _win.Add(new Type() {Power = Power, Current = 1});
                            _sorted = _win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                        else
                        {
                            current = 1;
                            Power = (_reserve[i + 1]/4)*4 + current*100;
                            _win.Add(new Type() {Power = Power, Current = 1});
                            _sorted = _win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                        }
                    }
                    msgbox = true;
                }
                for (int tc = 16; tc >= 12; tc--)
                {
                    if (_reserve[i + 1]/4 == _reserve[tc]/4)
                    {
                        if (!msgbox)
                        {
                            if (_reserve[i + 1]/4 == 0)
                            {
                                current = 1;
                                Power = 13*4 + _reserve[i]/4 + current*100;
                                _win.Add(new Type() {Power = Power, Current = 1});
                                _sorted = _win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                current = 1;
                                Power = (_reserve[i + 1]/4)*4 + _reserve[i]/4 + current*100;
                                _win.Add(new Type() {Power = Power, Current = 1});
                                _sorted = _win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                        }
                        msgbox = true;
                    }
                    if (_reserve[i]/4 == _reserve[tc]/4)
                    {
                        if (!msgbox)
                        {
                            if (_reserve[i]/4 == 0)
                            {
                                current = 1;
                                Power = 13*4 + _reserve[i + 1]/4 + current*100;
                                _win.Add(new Type() {Power = Power, Current = 1});
                                _sorted = _win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
                            }
                            else
                            {
                                current = 1;
                                Power = (_reserve[tc]/4)*4 + _reserve[i + 1]/4 + current*100;
                                _win.Add(new Type() {Power = Power, Current = 1});
                                _sorted = _win.OrderByDescending(op => op.Current).ThenByDescending(op => op.Power).First();
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
                if (_reserve[i]/4 > _reserve[i + 1]/4)
                {
                    current = -1;
                    Power = _reserve[i]/4;
                    _win.Add(new Type() {Power = Power, Current = -1});
                    _sorted = _win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                else
                {
                    current = -1;
                    Power = _reserve[i + 1]/4;
                    _win.Add(new Type() {Power = Power, Current = -1});
                    _sorted = _win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
                }
                if (_reserve[i]/4 == 0 || _reserve[i + 1]/4 == 0)
                {
                    current = -1;
                    Power = 13;
                    _win.Add(new Type() {Power = Power, Current = -1});
                    _sorted = _win.OrderByDescending(op1 => op1.Current).ThenByDescending(op1 => op1.Power).First();
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
                if (_holder[j].Visible)
                    _holder[j].Image = _deck[j];
            }
            if (current == _sorted.Current)
            {
                if (Power == _sorted.Power)
                {
                    _winners++;
                    _checkWinners.Add(currentText);
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
            if (currentText == lastly) //lastfixed
            {
                if (_winners > 1)
                {
                    if (_checkWinners.Contains("Player"))
                    {
                        Chips += int.Parse(textboxPot.Text)/_winners;
                        textboxChips.Text = Chips.ToString();
                        //pPanel.Visible = true;
                    }
                    if (_checkWinners.Contains("Bot 1"))
                    {
                        FirstBotChips += int.Parse(textboxPot.Text)/_winners;
                        textboxFirstBotChips.Text = FirstBotChips.ToString();
                        //b1Panel.Visible = true;
                    }
                    if (_checkWinners.Contains("Bot 2"))
                    {
                        SecondBotChips += int.Parse(textboxPot.Text)/_winners;
                        textboxSecondBotChips.Text = SecondBotChips.ToString();
                        //b2Panel.Visible = true;
                    }
                    if (_checkWinners.Contains("Bot 3"))
                    {
                        ThirdBotChips += int.Parse(textboxPot.Text)/_winners;
                        textbokThirdBotChips.Text = ThirdBotChips.ToString();
                        //b3Panel.Visible = true;
                    }
                    if (_checkWinners.Contains("Bot 4"))
                    {
                        FourthBotChips += int.Parse(textboxPot.Text)/_winners;
                        textboxFourthBotChips.Text = FourthBotChips.ToString();
                        //b4Panel.Visible = true;
                    }
                    if (_checkWinners.Contains("Bot 5"))
                    {
                        FifthBotChips += int.Parse(textboxPot.Text)/_winners;
                        textboxFifthBotChips.Text = FifthBotChips.ToString();
                        //b5Panel.Visible = true;
                    }
                    //await Finish(1);
                }
                if (_winners == 1)
                {
                    if (_checkWinners.Contains("Player"))
                    {
                        Chips += int.Parse(textboxPot.Text);
                        //await Finish(1);
                        //pPanel.Visible = true;
                    }
                    if (_checkWinners.Contains("Bot 1"))
                    {
                        FirstBotChips += int.Parse(textboxPot.Text);
                        //await Finish(1);
                        //b1Panel.Visible = true;
                    }
                    if (_checkWinners.Contains("Bot 2"))
                    {
                        SecondBotChips += int.Parse(textboxPot.Text);
                        //await Finish(1);
                        //b2Panel.Visible = true;
                    }
                    if (_checkWinners.Contains("Bot 3"))
                    {
                        ThirdBotChips += int.Parse(textboxPot.Text);
                        //await Finish(1);
                        //b3Panel.Visible = true;
                    }
                    if (_checkWinners.Contains("Bot 4"))
                    {
                        FourthBotChips += int.Parse(textboxPot.Text);
                        //await Finish(1);
                        //b4Panel.Visible = true;
                    }
                    if (_checkWinners.Contains("Bot 5"))
                    {
                        FifthBotChips += int.Parse(textboxPot.Text);
                        //await Finish(1);
                        //b5Panel.Visible = true;
                    }
                }
            }
        }

        async Task CheckRaise(int currentTurn, int raiseTurn)
        {
            if (_raising)
            {
                _turnCount = 0;
                _raising = false;
                _raisedTurn = currentTurn;
                _changed = true;
            }
            else
            {
                if (_turnCount >= _maxLeft - 1 || !_changed && _turnCount == _maxLeft)
                {
                    if (currentTurn == _raisedTurn - 1 || !_changed && _turnCount == _maxLeft || _raisedTurn == 0 && currentTurn == 5)
                    {
                        _changed = false;
                        _turnCount = 0;
                        _raise = 0;
                        _call = 0;
                        _raisedTurn = 123;
                        _rounds++;
                        if (!_foldedPlayerTurn)
                            playerStatus.Text = "";
                        if (!_firstBotFoldedTurn)
                            firstBotStatus.Text = "";
                        if (!_secodBotFoldedTurn)
                            secondBotStatus.Text = "";
                        if (!_thirdBotFoldedTurn)
                            thirdBotStatus.Text = "";
                        if (!_fourthBotFoldedTurn)
                            fourthBotStatus.Text = "";
                        if (!_fifthBotFoldedTurn)
                            fifthBotStatus.Text = "";
                    }
                }
            }
            if (_rounds == _flop)
            {
                for (int j = 12; j <= 14; j++)
                {
                    if (_holder[j].Image != _deck[j])
                    {
                        _holder[j].Image = _deck[j];
                        _playerCall = 0;
                        _playerRaise = 0;
                        _firstBotCall = 0;
                        _firstBotRaise = 0;
                        _secondBotCall = 0;
                        _secondBotRaise = 0;
                        _thirdBotCall = 0;
                        _thirdBotRaise = 0;
                        _fourthBotCall = 0;
                        _fourthBotRaise = 0;
                        _fifthBotCall = 0;
                        _fifthBotRaise = 0;
                    }
                }
            }
            if (_rounds == _turn)
            {
                for (int j = 14; j <= 15; j++)
                {
                    if (_holder[j].Image != _deck[j])
                    {
                        _holder[j].Image = _deck[j];
                        _playerCall = 0;
                        _playerRaise = 0;
                        _firstBotCall = 0;
                        _firstBotRaise = 0;
                        _secondBotCall = 0;
                        _secondBotRaise = 0;
                        _thirdBotCall = 0;
                        _thirdBotRaise = 0;
                        _fourthBotCall = 0;
                        _fourthBotRaise = 0;
                        _fifthBotCall = 0;
                        _fifthBotRaise = 0;
                    }
                }
            }
            if (_rounds == _river)
            {
                for (int j = 15; j <= 16; j++)
                {
                    if (_holder[j].Image != _deck[j])
                    {
                        _holder[j].Image = _deck[j];
                        _playerCall = 0;
                        _playerRaise = 0;
                        _firstBotCall = 0;
                        _firstBotRaise = 0;
                        _secondBotCall = 0;
                        _secondBotRaise = 0;
                        _thirdBotCall = 0;
                        _thirdBotRaise = 0;
                        _fourthBotCall = 0;
                        _fourthBotRaise = 0;
                        _fifthBotCall = 0;
                        _fifthBotRaise = 0;
                    }
                }
            }
            if (_rounds == _end && _maxLeft == 6)
            {
                string fixedLast = "qwerty";
                if (!playerStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Player";
                    Rules(0, 1, "Player", ref _playerType, ref _playerPower, _foldedPlayerTurn);
                }
                if (!firstBotStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 1";
                    Rules(2, 3, "Bot 1", ref _firstBotType, ref _firstBotPower, _firstBotFoldedTurn);
                }
                if (!secondBotStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 2";
                    Rules(4, 5, "Bot 2", ref _secondBotType, ref _secondBotPower, _secodBotFoldedTurn);
                }
                if (!thirdBotStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 3";
                    Rules(6, 7, "Bot 3", ref _thirdBotType, ref _thirdBotPower, _thirdBotFoldedTurn);
                }
                if (!fourthBotStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 4";
                    Rules(8, 9, "Bot 4", ref _fourthBotType, ref _fourthBotPower, _fourthBotFoldedTurn);
                }
                if (!fifthBotStatus.Text.Contains("Fold"))
                {
                    fixedLast = "Bot 5";
                    Rules(10, 11, "Bot 5", ref _fifthBotType, ref _fifthBotPower, _fifthBotFoldedTurn);
                }
                Winner(_playerType, _playerPower, "Player", Chips, fixedLast);
                Winner(_firstBotType, _firstBotPower, "Bot 1", FirstBotChips, fixedLast);
                Winner(_secondBotType, _secondBotPower, "Bot 2", SecondBotChips, fixedLast);
                Winner(_thirdBotType, _thirdBotPower, "Bot 3", ThirdBotChips, fixedLast);
                Winner(_fourthBotType, _fourthBotPower, "Bot 4", FourthBotChips, fixedLast);
                Winner(_fifthBotType, _fifthBotPower, "Bot 5", FifthBotChips, fixedLast);
                _restart = true;
                _playerTurn = true;
                _foldedPlayerTurn = false;
                _firstBotFoldedTurn = false;
                _secodBotFoldedTurn = false;
                _thirdBotFoldedTurn = false;
                _fourthBotFoldedTurn = false;
                _fifthBotFoldedTurn = false;
                if (Chips <= 0)
                {
                    AddChips f2 = new AddChips();
                    f2.ShowDialog();
                    if (f2.a != 0)
                    {
                        Chips = f2.a;
                        FirstBotChips += f2.a;
                        SecondBotChips += f2.a;
                        ThirdBotChips += f2.a;
                        FourthBotChips += f2.a;
                        FifthBotChips += f2.a;
                        _foldedPlayerTurn = false;
                        _playerTurn = true;
                        buttonRaise.Enabled = true;
                        buttonFold.Enabled = true;
                        buttonCheck.Enabled = true;
                        buttonRaise.Text = "Raise";
                    }
                }
                _playerPanel.Visible = false;
                _firstBotPanel.Visible = false;
                _secondBotPanel.Visible = false;
                _thirdBotPanel.Visible = false;
                _fourthBotPanel.Visible = false;
                _fifthBotPanel.Visible = false;
                _playerCall = 0;
                _playerRaise = 0;
                _firstBotCall = 0;
                _firstBotRaise = 0;
                _secondBotCall = 0;
                _secondBotRaise = 0;
                _thirdBotCall = 0;
                _thirdBotRaise = 0;
                _fourthBotCall = 0;
                _fourthBotRaise = 0;
                _fifthBotCall = 0;
                _fifthBotRaise = 0;
                _last = 0;
                _call = _bigBlind;
                _raise = 0;
                _imageLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
                _bools.Clear();
                _rounds = 0;
                _playerPower = 0;
                _playerType = -1;
                _type = 0;
                _firstBotPower = 0;
                _secondBotPower = 0;
                _thirdBotPower = 0;
                _fourthBotPower = 0;
                _fifthBotPower = 0;
                _firstBotType = -1;
                _secondBotType = -1;
                _thirdBotType = -1;
                _fourthBotType = -1;
                _fifthBotType = -1;
                _ints.Clear();
                _checkWinners.Clear();
                _winners = 0;
                _win.Clear();
                _sorted.Current = 0;
                _sorted.Power = 0;
                for (int os = 0; os < 17; os++)
                {
                    _holder[os].Image = null;
                    _holder[os].Invalidate();
                    _holder[os].Visible = false;
                }
                textboxPot.Text = "0";
                playerStatus.Text = "";
                await Shuffle();
                await Turns();
            }
        }

        void FixCall(Label status, ref int cCall, ref int cRaise, int options)
        {
            if (_rounds != 4)
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
                    if (cRaise != _raise && cRaise <= _raise)
                    {
                        _call = Convert.ToInt32(_raise) - cRaise;
                    }
                    if (cCall != _call || cCall <= _call)
                    {
                        _call = _call - cCall;
                    }
                    if (cRaise == _raise && _raise > 0)
                    {
                        _call = 0;
                        buttonCall.Enabled = false;
                        buttonCall.Text = "Callisfuckedup";
                    }
                }
            }
        }

        async Task AllIn()
        {
            #region All in

            if (Chips <= 0 && !_intsadded)
            {
                if (playerStatus.Text.Contains("Raise"))
                {
                    _ints.Add(Chips);
                    _intsadded = true;
                }
                if (playerStatus.Text.Contains("Call"))
                {
                    _ints.Add(Chips);
                    _intsadded = true;
                }
            }
            _intsadded = false;
            if (FirstBotChips <= 0 && !_firstBotFoldedTurn)
            {
                if (!_intsadded)
                {
                    _ints.Add(FirstBotChips);
                    _intsadded = true;
                }
                _intsadded = false;
            }
            if (SecondBotChips <= 0 && !_secodBotFoldedTurn)
            {
                if (!_intsadded)
                {
                    _ints.Add(SecondBotChips);
                    _intsadded = true;
                }
                _intsadded = false;
            }
            if (ThirdBotChips <= 0 && !_thirdBotFoldedTurn)
            {
                if (!_intsadded)
                {
                    _ints.Add(ThirdBotChips);
                    _intsadded = true;
                }
                _intsadded = false;
            }
            if (FourthBotChips <= 0 && !_fourthBotFoldedTurn)
            {
                if (!_intsadded)
                {
                    _ints.Add(FourthBotChips);
                    _intsadded = true;
                }
                _intsadded = false;
            }
            if (FifthBotChips <= 0 && !_fifthBotFoldedTurn)
            {
                if (!_intsadded)
                {
                    _ints.Add(FifthBotChips);
                    _intsadded = true;
                }
            }
            if (_ints.ToArray().Length == _maxLeft)
            {
                await Finish(2);
            }
            else
            {
                _ints.Clear();
            }

            #endregion

            var abc = _bools.Count(x => x == false);

            #region LastManStanding

            if (abc == 1)
            {
                int index = _bools.IndexOf(false);
                if (index == 0)
                {
                    Chips += int.Parse(textboxPot.Text);
                    textboxChips.Text = Chips.ToString();
                    _playerPanel.Visible = true;
                    MessageBox.Show("Player Wins");
                }
                if (index == 1)
                {
                    FirstBotChips += int.Parse(textboxPot.Text);
                    textboxChips.Text = FirstBotChips.ToString();
                    _firstBotPanel.Visible = true;
                    MessageBox.Show("Bot 1 Wins");
                }
                if (index == 2)
                {
                    SecondBotChips += int.Parse(textboxPot.Text);
                    textboxChips.Text = SecondBotChips.ToString();
                    _secondBotPanel.Visible = true;
                    MessageBox.Show("Bot 2 Wins");
                }
                if (index == 3)
                {
                    ThirdBotChips += int.Parse(textboxPot.Text);
                    textboxChips.Text = ThirdBotChips.ToString();
                    _thirdBotPanel.Visible = true;
                    MessageBox.Show("Bot 3 Wins");
                }
                if (index == 4)
                {
                    FourthBotChips += int.Parse(textboxPot.Text);
                    textboxChips.Text = FourthBotChips.ToString();
                    _fourthBotPanel.Visible = true;
                    MessageBox.Show("Bot 4 Wins");
                }
                if (index == 5)
                {
                    FifthBotChips += int.Parse(textboxPot.Text);
                    textboxChips.Text = FifthBotChips.ToString();
                    _fifthBotPanel.Visible = true;
                    MessageBox.Show("Bot 5 Wins");
                }
                for (int j = 0; j <= 16; j++)
                {
                    _holder[j].Visible = false;
                }
                await Finish(1);
            }
            _intsadded = false;

            #endregion

            #region FiveOrLessLeft

            if (abc < 6 && abc > 1 && _rounds >= _end)
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
            _playerPanel.Visible = false;
            _firstBotPanel.Visible = false;
            _secondBotPanel.Visible = false;
            _thirdBotPanel.Visible = false;
            _fourthBotPanel.Visible = false;
            _fifthBotPanel.Visible = false;
            _call = _bigBlind;
            _raise = 0;
            _foldedPlayers = 5;
            _type = 0;
            _rounds = 0;
            _firstBotPower = 0;
            _secondBotPower = 0;
            _thirdBotPower = 0;
            _fourthBotPower = 0;
            _fifthBotPower = 0;
            _playerPower = 0;
            _playerType = -1;
            _raise = 0;
            _firstBotType = -1;
            _secondBotType = -1;
            _thirdBotType = -1;
            _fourthBotType = -1;
            _fifthBotType = -1;
            _firstBotTurn = false;
            _secondBotTurn = false;
            _thirdBotTurn = false;
            _fourthBotTurn = false;
            _fifthBotTurn = false;
            _firstBotFoldedTurn = false;
            _secodBotFoldedTurn = false;
            _thirdBotFoldedTurn = false;
            _fourthBotFoldedTurn = false;
            _fifthBotFoldedTurn = false;
            _playerFolded = false;
            _firstBotFolded = false;
            _secondBotFolded = false;
            _thirdBotFolded = false;
            _fourthBotFolded = false;
            _fifthBotFolded = false;
            _foldedPlayerTurn = false;
            _playerTurn = true;
            _restart = false;
            _raising = false;
            _playerCall = 0;
            _firstBotCall = 0;
            _secondBotCall = 0;
            _thirdBotCall = 0;
            _fourthBotCall = 0;
            _fifthBotCall = 0;
            _playerRaise = 0;
            _firstBotRaise = 0;
            _secondBotRaise = 0;
            _thirdBotRaise = 0;
            _fourthBotRaise = 0;
            _fifthBotRaise = 0;
            _height = 0;
            _width = 0;
            _winners = 0;
            _flop = 1;
            _turn = 2;
            _river = 3;
            _end = 4;
            _maxLeft = 6;
            _last = 123;
            _raisedTurn = 1;
            _bools.Clear();
            _checkWinners.Clear();
            _ints.Clear();
            _win.Clear();
            _sorted.Current = 0;
            _sorted.Power = 0;
            textboxPot.Text = "0";
            _timeTillNextTurn = 60;
            up = 10000000;
            _turnCount = 0;
            playerStatus.Text = "";
            firstBotStatus.Text = "";
            secondBotStatus.Text = "";
            thirdBotStatus.Text = "";
            fourthBotStatus.Text = "";
            fifthBotStatus.Text = "";
            if (Chips <= 0)
            {
                AddChips f2 = new AddChips();
                f2.ShowDialog();
                if (f2.a != 0)
                {
                    Chips = f2.a;
                    FirstBotChips += f2.a;
                    SecondBotChips += f2.a;
                    ThirdBotChips += f2.a;
                    FourthBotChips += f2.a;
                    FifthBotChips += f2.a;
                    _foldedPlayerTurn = false;
                    _playerTurn = true;
                    buttonRaise.Enabled = true;
                    buttonFold.Enabled = true;
                    buttonCheck.Enabled = true;
                    buttonRaise.Text = "Raise";
                }
            }
            _imageLocation = Directory.GetFiles("Assets\\Cards", "*.png", SearchOption.TopDirectoryOnly);
            for (int os = 0; os < 17; os++)
            {
                _holder[os].Image = null;
                _holder[os].Invalidate();
                _holder[os].Visible = false;
            }
            await Shuffle();
            //await Turns();
        }

        void FixWinners()
        {
            _win.Clear();
            _sorted.Current = 0;
            _sorted.Power = 0;
            string fixedLast = "qwerty";
            if (!playerStatus.Text.Contains("Fold"))
            {
                fixedLast = "Player";
                Rules(0, 1, "Player", ref _playerType, ref _playerPower, _foldedPlayerTurn);
            }
            if (!firstBotStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 1";
                Rules(2, 3, "Bot 1", ref _firstBotType, ref _firstBotPower, _firstBotFoldedTurn);
            }
            if (!secondBotStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 2";
                Rules(4, 5, "Bot 2", ref _secondBotType, ref _secondBotPower, _secodBotFoldedTurn);
            }
            if (!thirdBotStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 3";
                Rules(6, 7, "Bot 3", ref _thirdBotType, ref _thirdBotPower, _thirdBotFoldedTurn);
            }
            if (!fourthBotStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 4";
                Rules(8, 9, "Bot 4", ref _fourthBotType, ref _fourthBotPower, _fourthBotFoldedTurn);
            }
            if (!fifthBotStatus.Text.Contains("Fold"))
            {
                fixedLast = "Bot 5";
                Rules(10, 11, "Bot 5", ref _fifthBotType, ref _fifthBotPower, _fifthBotFoldedTurn);
            }
            Winner(_playerType, _playerPower, "Player", Chips, fixedLast);
            Winner(_firstBotType, _firstBotPower, "Bot 1", FirstBotChips, fixedLast);
            Winner(_secondBotType, _secondBotPower, "Bot 2", SecondBotChips, fixedLast);
            Winner(_thirdBotType, _thirdBotPower, "Bot 3", ThirdBotChips, fixedLast);
            Winner(_fourthBotType, _fourthBotPower, "Bot 4", FourthBotChips, fixedLast);
            Winner(_fifthBotType, _fifthBotPower, "Bot 5", FifthBotChips, fixedLast);
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
                _holder[c1].Visible = false;
                _holder[c2].Visible = false;
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
            if (botPower <= 327 && botPower >= 321) //10  8
            {
                Smooth(ref sChips, ref sTurn, ref sFTurn, sStatus, name, tCall, tRaise);
            }
            if (botPower < 321 && botPower >= 303) //7 2
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
            if (botPower <= 409 && botPower >= 407) //10  8
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
            _raising = false;
            sStatus.Text = "Fold";
            sTurn = false;
            sFTurn = true;
        }

        private void Check(ref bool cTurn, Label cStatus)
        {
            cStatus.Text = "Check";
            cTurn = false;
            _raising = false;
        }

        private void Call(ref int sChips, ref bool sTurn, Label sStatus)
        {
            _raising = false;
            sTurn = false;
            sChips -= _call;
            sStatus.Text = "Call " + _call;
            textboxPot.Text = (int.Parse(textboxPot.Text) + _call).ToString();
        }

        private void Raised(ref int sChips, ref bool sTurn, Label sStatus)
        {
            sChips -= Convert.ToInt32(_raise);
            sStatus.Text = "Raise " + _raise;
            textboxPot.Text = (int.Parse(textboxPot.Text) + Convert.ToInt32(_raise)).ToString();
            _call = Convert.ToInt32(_raise);
            _raising = true;
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
            if (_call <= 0)
            {
                Check(ref sTurn, sStatus);
            }
            if (_call > 0)
            {
                if (rnd == 1)
                {
                    if (_call <= RoundN(sChips, n))
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
                    if (_call <= RoundN(sChips, n1))
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
                if (_raise == 0)
                {
                    _raise = _call*2;
                    Raised(ref sChips, ref sTurn, sStatus);
                }
                else
                {
                    if (_raise <= RoundN(sChips, n))
                    {
                        _raise = _call*2;
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
            if (_rounds < 2)
            {
                if (_call <= 0)
                {
                    Check(ref sTurn, sStatus);
                }
                if (_call > 0)
                {
                    if (_call >= RoundN(sChips, n1))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (_raise > RoundN(sChips, n))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (!sFTurn)
                    {
                        if (_call >= RoundN(sChips, n) && _call <= RoundN(sChips, n1))
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (_raise <= RoundN(sChips, n) && _raise >= (RoundN(sChips, n))/2)
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (_raise <= (RoundN(sChips, n))/2)
                        {
                            if (_raise > 0)
                            {
                                _raise = RoundN(sChips, n);
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                            else
                            {
                                _raise = _call*2;
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                        }
                    }
                }
            }
            if (_rounds >= 2)
            {
                if (_call > 0)
                {
                    if (_call >= RoundN(sChips, n1 - rnd))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (_raise > RoundN(sChips, n - rnd))
                    {
                        Fold(ref sTurn, ref sFTurn, sStatus);
                    }
                    if (!sFTurn)
                    {
                        if (_call >= RoundN(sChips, n - rnd) && _call <= RoundN(sChips, n1 - rnd))
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (_raise <= RoundN(sChips, n - rnd) && _raise >= (RoundN(sChips, n - rnd))/2)
                        {
                            Call(ref sChips, ref sTurn, sStatus);
                        }
                        if (_raise <= (RoundN(sChips, n - rnd))/2)
                        {
                            if (_raise > 0)
                            {
                                _raise = RoundN(sChips, n - rnd);
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                            else
                            {
                                _raise = _call*2;
                                Raised(ref sChips, ref sTurn, sStatus);
                            }
                        }
                    }
                }
                if (_call <= 0)
                {
                    _raise = RoundN(sChips, r - rnd);
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
            if (_call <= 0)
            {
                Check(ref botTurn, botStatus);
            }
            else
            {
                if (_call >= RoundN(botChips, n))
                {
                    if (botChips > _call)
                    {
                        Call(ref botChips, ref botTurn, botStatus);
                    }
                    else if (botChips <= _call)
                    {
                        _raising = false;
                        botTurn = false;
                        botChips = 0;
                        botStatus.Text = "Call " + botChips;
                        textboxPot.Text = (int.Parse(textboxPot.Text) + botChips).ToString();
                    }
                }
                else
                {
                    if (_raise > 0)
                    {
                        if (botChips >= _raise*2)
                        {
                            _raise *= 2;
                            Raised(ref botChips, ref botTurn, botStatus);
                        }
                        else
                        {
                            Call(ref botChips, ref botTurn, botStatus);
                        }
                    }
                    else
                    {
                        _raise = _call*2;
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
                _foldedPlayerTurn = true;
                await Turns();
            }
            if (_timeTillNextTurn > 0)
            {
                _timeTillNextTurn--;
                progressBarTimer.Value = (_timeTillNextTurn/6)*100;
            }
        }

        private void Update_Tick(object sender, object e)
        {
            if (Chips <= 0)
            {
                textboxChips.Text = "Chips : 0";
            }
            if (FirstBotChips <= 0)
            {
                textboxFirstBotChips.Text = "Chips : 0";
            }
            if (SecondBotChips <= 0)
            {
                textboxSecondBotChips.Text = "Chips : 0";
            }
            if (ThirdBotChips <= 0)
            {
                textbokThirdBotChips.Text = "Chips : 0";
            }
            if (FourthBotChips <= 0)
            {
                textboxFourthBotChips.Text = "Chips : 0";
            }
            if (FifthBotChips <= 0)
            {
                textboxFifthBotChips.Text = "Chips : 0";
            }
            textboxChips.Text = "Chips : " + Chips.ToString();
            textboxFirstBotChips.Text = "Chips : " + FirstBotChips.ToString();
            textboxSecondBotChips.Text = "Chips : " + SecondBotChips.ToString();
            textbokThirdBotChips.Text = "Chips : " + ThirdBotChips.ToString();
            textboxFourthBotChips.Text = "Chips : " + FourthBotChips.ToString();
            textboxFifthBotChips.Text = "Chips : " + FifthBotChips.ToString();
            if (Chips <= 0)
            {
                _playerTurn = false;
                _foldedPlayerTurn = true;
                buttonCall.Enabled = false;
                buttonRaise.Enabled = false;
                buttonFold.Enabled = false;
                buttonCheck.Enabled = false;
            }
            if (up > 0)
            {
                up--;
            }
            if (Chips >= _call)
            {
                buttonCall.Text = "Call " + _call.ToString();
            }
            else
            {
                buttonCall.Text = "All in";
                buttonRaise.Enabled = false;
            }
            if (_call > 0)
            {
                buttonCheck.Enabled = false;
            }
            if (_call <= 0)
            {
                buttonCheck.Enabled = true;
                buttonCall.Text = "Call";
                buttonCall.Enabled = false;
            }
            if (Chips <= 0)
            {
                buttonRaise.Enabled = false;
            }
            int parsedValue;

            if (textboxRaise.Text != "" && int.TryParse(textboxRaise.Text, out parsedValue))
            {
                if (Chips <= int.Parse(textboxRaise.Text))
                {
                    buttonRaise.Text = "All in";
                }
                else
                {
                    buttonRaise.Text = "Raise";
                }
            }
            if (Chips < _call)
            {
                buttonRaise.Enabled = false;
            }
        }

        private async void bFold_Click(object sender, EventArgs e)
        {
            playerStatus.Text = "Fold";
            _playerTurn = false;
            _foldedPlayerTurn = true;
            await Turns();
        }

        private async void bCheck_Click(object sender, EventArgs e)
        {
            if (_call <= 0)
            {
                _playerTurn = false;
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
            Rules(0, 1, "Player", ref _playerType, ref _playerPower, _foldedPlayerTurn);
            if (Chips >= _call)
            {
                Chips -= _call;
                textboxChips.Text = "Chips : " + Chips.ToString();
                if (textboxPot.Text != "")
                {
                    textboxPot.Text = (int.Parse(textboxPot.Text) + _call).ToString();
                }
                else
                {
                    textboxPot.Text = _call.ToString();
                }
                _playerTurn = false;
                playerStatus.Text = "Call " + _call;
                _playerCall = _call;
            }
            else if (Chips <= _call && _call > 0)
            {
                textboxPot.Text = (int.Parse(textboxPot.Text) + Chips).ToString();
                playerStatus.Text = "All in " + Chips;
                Chips = 0;
                textboxChips.Text = "Chips : " + Chips.ToString();
                _playerTurn = false;
                buttonFold.Enabled = false;
                _playerCall = Chips;
            }
            await Turns();
        }

        private async void bRaise_Click(object sender, EventArgs e)
        {
            Rules(0, 1, "Player", ref _playerType, ref _playerPower, _foldedPlayerTurn);
            int parsedValue;
            if (textboxRaise.Text != "" && int.TryParse(textboxRaise.Text, out parsedValue))
            {
                if (Chips > _call)
                {
                    if (_raise*2 > int.Parse(textboxRaise.Text))
                    {
                        textboxRaise.Text = (_raise*2).ToString();
                        MessageBox.Show("You must raise atleast twice as the current raise !");
                        return;
                    }
                    else
                    {
                        if (Chips >= int.Parse(textboxRaise.Text))
                        {
                            _call = int.Parse(textboxRaise.Text);
                            _raise = int.Parse(textboxRaise.Text);
                            playerStatus.Text = "Raise " + _call.ToString();
                            textboxPot.Text = (int.Parse(textboxPot.Text) + _call).ToString();
                            buttonCall.Text = "Call";
                            Chips -= int.Parse(textboxRaise.Text);
                            _raising = true;
                            _last = 0;
                            _playerRaise = Convert.ToInt32(_raise);
                        }
                        else
                        {
                            _call = Chips;
                            _raise = Chips;
                            textboxPot.Text = (int.Parse(textboxPot.Text) + Chips).ToString();
                            playerStatus.Text = "Raise " + _call.ToString();
                            Chips = 0;
                            _raising = true;
                            _last = 0;
                            _playerRaise = Convert.ToInt32(_raise);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("This is a number only field");
                return;
            }
            _playerTurn = false;
            await Turns();
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            if (textboxAdd.Text == "")
            {
            }
            else
            {
                Chips += int.Parse(textboxAdd.Text);
                FirstBotChips += int.Parse(textboxAdd.Text);
                SecondBotChips += int.Parse(textboxAdd.Text);
                ThirdBotChips += int.Parse(textboxAdd.Text);
                FourthBotChips += int.Parse(textboxAdd.Text);
                FifthBotChips += int.Parse(textboxAdd.Text);
            }
            textboxChips.Text = "Chips : " + Chips.ToString();
        }

        private void bOptions_Click(object sender, EventArgs e)
        {
            textboxBigBlind.Text = _bigBlind.ToString();
            textboxSmallBlind.Text = _smallBlind.ToString();
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
                textboxSmallBlind.Text = _smallBlind.ToString();
                return;
            }
            if (!int.TryParse(textboxSmallBlind.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                textboxSmallBlind.Text = _smallBlind.ToString();
                return;
            }
            if (int.Parse(textboxSmallBlind.Text) > 100000)
            {
                MessageBox.Show("The maximum of the Small Blind is 100 000 $");
                textboxSmallBlind.Text = _smallBlind.ToString();
            }
            if (int.Parse(textboxSmallBlind.Text) < 250)
            {
                MessageBox.Show("The minimum of the Small Blind is 250 $");
            }
            if (int.Parse(textboxSmallBlind.Text) >= 250 && int.Parse(textboxSmallBlind.Text) <= 100000)
            {
                _smallBlind = int.Parse(textboxSmallBlind.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void bBB_Click(object sender, EventArgs e)
        {
            int parsedValue;
            if (textboxBigBlind.Text.Contains(",") || textboxBigBlind.Text.Contains("."))
            {
                MessageBox.Show("The Big Blind can be only round number !");
                textboxBigBlind.Text = _bigBlind.ToString();
                return;
            }
            if (!int.TryParse(textboxSmallBlind.Text, out parsedValue))
            {
                MessageBox.Show("This is a number only field");
                textboxSmallBlind.Text = _bigBlind.ToString();
                return;
            }
            if (int.Parse(textboxBigBlind.Text) > 200000)
            {
                MessageBox.Show("The maximum of the Big Blind is 200 000");
                textboxBigBlind.Text = _bigBlind.ToString();
            }
            if (int.Parse(textboxBigBlind.Text) < 500)
            {
                MessageBox.Show("The minimum of the Big Blind is 500 $");
            }
            if (int.Parse(textboxBigBlind.Text) >= 500 && int.Parse(textboxBigBlind.Text) <= 200000)
            {
                _bigBlind = int.Parse(textboxBigBlind.Text);
                MessageBox.Show("The changes have been saved ! They will become available the next hand you play. ");
            }
        }

        private void Layout_Change(object sender, LayoutEventArgs e)
        {
            _width = this.Width;
            _height = this.Height;
        }

        #endregion
    }
}