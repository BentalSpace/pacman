using System;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace Pacman {
    class GameManager {
        int scoreInt = 0;
        int eatItem = 0;
        public int level = 1;

        Player player;
        Map map = new Map();

        Label score;

        public GameManager(Player player, Label score) {
            this.player = player;
            this.score = score;
        }
        public GameManager() {

        }

        public void ItemEat() {
            if(map.ground[player.posY, player.posX] == 2) {
                map.ground[player.posY, player.posX] = 0;
                scoreInt += 10;
                eatItem++;
            }
            if(map.ground[player.posY, player.posX] == 3) {
                map.ground[player.posY, player.posX] = 0;
                scoreInt += 50;
                eatItem++;
                //플레이어 식사모드 변환
            }
            score.Text = scoreInt.ToString();
            if (eatItem >= 244)
                GameReStart();
        }
        public void itemCreate(Graphics g) {
            Image item = Image.FromFile(Application.StartupPath + @"\images\item.png");
            Image sItem = Image.FromFile(Application.StartupPath + @"\images\superItem.png");

            for(int i = 1; i < map.ground.GetLength(0); i++) {
                for(int j = 1; j < map.ground.GetLength(1); j++) {
                    if (map.ground[i, j] == 2)
                        g.DrawImage(item, j * 35 + 10, i * 35 + 70);
                    else if (map.ground[i, j] == 3)
                        g.DrawImage(sItem, j * 35 + 5, i * 35 + 65);
                }
            }
        }
        public void GameControl() { // 아이템 총 갯수 244
            if (eatItem == 244) { // 블링키 처음위치 (460,431)
                //self.Left = 460;
                //self.Top = 851;
                player.DirClear();
            }
        }
        void GameReStart() {
            //플레이어 포지션 초기화 / 몬스터들 포지션 초기화
            map = new Map();
            eatItem = 0;
            Thread.Sleep(1000);
        }
    }
}
