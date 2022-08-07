using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pacman {
    public partial class pacmanGame : Form {
        Player player;
        GameManager manager;

        Blinky classBlinky;
        Pinky classPinky;
        Inky classInky;
        Clyde classClyde;

        int[,] delayTimer;
        int levelIndex;
        int timerIndex;
        int delayIndex;

        bool isFirst = true;
        bool isChaseScatter; // false일때 Scatter, true일때 chase

        public pacmanGame() {
            InitializeComponent();
            player = new Player();
            classBlinky = new Blinky(player);
            classPinky = new Pinky(player);
            classInky = new Inky(player, classBlinky);
            classClyde = new Clyde(player);
            manager = new GameManager(player, lblScore, classBlinky, classPinky, classInky, classClyde);

            delayTimer = new int[,] {
            { 420,1200,420,1200,300,1200,300,999999999 },
            { 420,1200,420,1200,300,61983,10, 999999999 },
            { 300,1200,300,1200,300,62223,10, 999999999 }
            };
            levelIndex = 0;
            timerIndex = 0;
            delayIndex = 0;

            isChaseScatter = false;
        }

        private void pacmanGame_KeyDown(object sender, KeyEventArgs e) {
            player.PlayerMoveDirSet(e);
        }

        private void GameTimer_Tick(object sender, EventArgs e) {
            //player.PosCheak();
            player.CurveCheck();
            player.PlayerMove();
            player.CurveMove();
            switch (levelIndex) {
                case 0:
                    if (delayIndex >= delayTimer[0, timerIndex]) {
                        isChaseScatter = !isChaseScatter;
                        timerIndex++;
                        levelIndex++;
                        delayIndex = 0;
                        classClyde.isScatterMode = false;
                    }
                    break;
                case 1:
                case 2:
                case 3:
                    if (delayIndex >= delayTimer[1, timerIndex]) {
                        isChaseScatter = !isChaseScatter;
                        levelIndex++;
                        timerIndex++;
                        delayIndex = 0;
                        classClyde.isScatterMode = false;
                    }
                    break;
                default:
                    if (delayIndex >= delayTimer[2, timerIndex]) {
                        isChaseScatter = !isChaseScatter;
                        timerIndex++;
                        delayIndex = 0;
                        classClyde.isScatterMode = false;
                    }
                    break;
            }
            delayIndex++;
            lblTemp.Text = (delayIndex).ToString();

            //manager.GameControl();


            classPinky.PlayerMoveCheck();
            classClyde.PlayerCircle();
            classClyde.PacmanNearCheck();
            classInky.PlayerMoveCheck();

            //classBlinky.Teleport(classBlinky.dir, classBlinky.PosX, classBlinky.PosY);
            //classPinky.Teleport(classPinky.dir, classPinky.PosX, classPinky.PosY);
            //classClyde.Teleport(classClyde.dir, classClyde.PosX, classClyde.PosY);
            //classInky.Teleport(classInky.dir, classInky.PosX, classInky.PosY);

            if (player.isEatMode) {
                manager.playerEatMode();
            }

            if (isChaseScatter) {
                classBlinky.ChaseCheck();
                classPinky.ChaseCheck();
                classClyde.ChaseCheck();
                classInky.ChaseCheck();
            }

            else if (!isChaseScatter) {
                classBlinky.ScatterCheck();
                classPinky.ScatterCheck();
                classClyde.ScatterCheck();
                classInky.ScatterCheck();
            }
            manager.ItemEat();
            manager.pacmanEnemyTouch();
            
            Invalidate();
        }

        private void pacmanGame_Paint(object sender, PaintEventArgs e) {
            manager.itemCreate(e.Graphics);
            player.playerDraw(e.Graphics);
            classBlinky.enemyDraw(e.Graphics);
            classPinky.enemyDraw(e.Graphics);
            classInky.enemyDraw(e.Graphics);
            classClyde.enemyDraw(e.Graphics);
            //45,105
        }

        private void pacmanGame_Load(object sender, EventArgs e) {
            GameTimer.Interval = 1000 / 60;
        }
    }
}
