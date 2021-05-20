using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Pacman {
    class GameManager {
        int scoreInt = 0;
        int eatItem = 0;
        public int level = 1;

        bool gameClear = false;

        Player player = new Player();
        Label score;
        Panel self;

        public GameManager(Panel self, Label score) {
            this.self = self;
            this.score = score;
        }
        public GameManager() {

        }

        public void ItemEat(Control x) {
            if ((string)x.Tag == "item" && x.Visible == true)
                if (self.Bounds.IntersectsWith(x.Bounds)) {
                    x.Visible = false;
                    scoreInt += 10;
                    eatItem++;
                    score.Text = scoreInt.ToString();
                }
            if((string)x.Tag == "superItem" && x.Visible == true)
                if (self.Bounds.IntersectsWith(x.Bounds)) {
                    x.Visible = false;
                    scoreInt += 50;
                    eatItem++;
                    score.Text = scoreInt.ToString();
                }
        }
        public void GameControl() { // 아이템 총 갯수 244
            if (eatItem == 244) {
                self.Left = 460;
                self.Top = 851;
                player.DirClear();

                eatItem = 0;
                gameClear = true;
            }
        }
        public void ItemReCreate(Control x) {
            if (!gameClear)
                return;
            if ((string)x.Tag == "item" || (string)x.Tag == "superItem")
                x.Visible = true;
        }
        public void GameReStart() {
            if (!gameClear)
                return;
            Thread.Sleep(1000);
            gameClear = false;
        }
    }
}
