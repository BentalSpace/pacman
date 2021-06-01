using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman {
    public partial class pacmanGame : Form {
        Player player;
        GameManager manager;

        Blinky classBlinky;
        //Pinky classPinky;
        //Inky classInky;
        //Clyde classClyde;

        int[,] delayTimer = new int[,] {
            { 7000,20000,7000,20000,5000,20000,5000 },
            { 7000,20000,7000,20000,5000,1033140,10 },
            { 5000,20000,5000,20000,5000,1037140,10 }
        };
        public int delayIndex = 0;

        bool isFirst = true;
        

        public pacmanGame() {
            InitializeComponent();
            player = new Player(pacman);
            manager = new GameManager(pacman, lblScore);
            classBlinky = new Blinky(pacman, blinky);
            //classPinky = new Pinky(pacman, pinky);
            //classInky = new Inky(pacman, inky);
            //classClyde = new Clyde(pacman, clyde);
        }

        private void pacmanGame_KeyDown(object sender, KeyEventArgs e) {
            player.PlayerMoveDirSet(e);
        }

        private void GameTimer_Tick(object sender, EventArgs e) {
            //player.PosCheak();
            player.CurveCheck();
            if (!player.isCurveMove)
                player.PlayerMove();
            player.CurveMove();
            manager.GameControl();

            //classInky.ScatterCheck();
            //classClyde.ScatterCheck();

            //classPinky.PlayerMoveCheck();

            classBlinky.ChaseCheck();
            //classPinky.ChaseCheck();

            //classBlinky.ScatterCheck();
            //classPinky.ScatterCheck();
            foreach (Control x in this.Controls) {
                if (x is Panel) {
                    manager.ItemEat(x);
                    manager.ItemReCreate(x);
                }
            }
            manager.GameReStart();
            Invalidate();
        }
        private void ChaseScatterTimer_Tick(object sender, EventArgs e) {
            //switch (manager.level) {
            //    case 1:
            //        enemy.isChaseScatter = !enemy.isChaseScatter;
            //        if(!isFirst)
            //            classBlinky.isChangeFirst = true;
            //        isFirst = false;
            //        ChaseScatterTimer.Interval = delayIndex >= 7 ? 999999999 : delayTimer[0, delayIndex++];
            //        break;
            //    case 2:
            //    case 3:
            //    case 4:
            //        break;
            //    case 5:
            //        break;
            //}
        }

        private void pacmanGame_Paint(object sender, PaintEventArgs e) {
            player.playerDraw(e.Graphics);
            classBlinky.enemyDraw(e.Graphics);
        }
    }
}
