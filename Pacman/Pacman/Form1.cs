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
        public pacmanGame() {
            InitializeComponent();
            player = new Player(pacman);
            manager = new GameManager(pacman, lblScore);
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
            //player.HitboxMove();
            //player.Teleport();
            //foreach (Control x in this.Controls) {
            //    if (x is Panel) {
            //        manager.ItemEat(x);
            //        if (player.gameStart)
            //            player.BlockWall(x);
            //    }
            //}
        }
    }
}
