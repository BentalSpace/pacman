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
        Enemy enemy;
        Enemy classBlinky;
        

        public pacmanGame() {
            InitializeComponent();
            player = new Player(pacman, blinky);
            manager = new GameManager(pacman, lblScore);
            enemy = new Enemy(pacman, blinky);
            classBlinky = new Blinky(pacman, blinky);
        }

        private void pacmanGame_KeyDown(object sender, KeyEventArgs e) {
            player.PlayerMoveDirSet(e);
        }

        private void GameTimer_Tick(object sender, EventArgs e) {
            player.PosCheak();
            if(!player.isCurveMove)
                player.PlayerMove();
            player.CurveCheck();
            player.CurveMove();
            manager.GameControl();
            enemy.PosCheak();
           // if(!enemy.posChange)
                enemy.BlinkyScatter();
            //if (enemy.posChange)
            //    enemy.PosMove();
            foreach (Control x in this.Controls) {
                if (x is Panel) {
                    manager.ItemEat(x);
                    manager.ItemReCreate(x);
                }
            }
            manager.GameReStart();
        }
    }
}
