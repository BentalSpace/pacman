using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pacman {
    class GameManager {
        int scoreInt = 0;

        Player player = new Player();
        Label score;
        Panel self;

        public GameManager(Panel self, Label score) {
            this.self = self;
            this.score = score;
        }

        public void ItemEat(Control x) {
            if ((string)x.Tag == "item" && x.Visible == true)
                if (self.Bounds.IntersectsWith(x.Bounds)) {
                    x.Visible = false;
                    scoreInt += 10;
                    score.Text = scoreInt.ToString();
                }
            if((string)x.Tag == "superItem" && x.Visible == true)
                if (self.Bounds.IntersectsWith(x.Bounds)) {
                    x.Visible = false;
                    scoreInt += 50;
                    score.Text = scoreInt.ToString();
                }
        }

        public void GameStart(Control x) { // 시작 로케이션 값 = 460, 851
            //스타팅 설정.
            self.Left = 460;
            self.Top = 851;
            //아이템 다시 생성
            if ((string)x.Tag == "item" || (string)x.Tag == "superItem")
                x.Visible = true;
        }
    }
}
