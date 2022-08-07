using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Pacman {
    class GameManager {
        int scoreInt = 0;
        int eatItem = 0;
        public int level = 1;

        Player player;
        Blinky blinky;
        Pinky pinky;
        Inky inky;
        Clyde clyde;
        Map map = new Map();

        Label score;

        public GameManager(Player player, Label score, Blinky blinky, Pinky pinky, Inky inky, Clyde clyde) {
            this.player = player;
            this.score = score;
            this.blinky = blinky;
            this.pinky = pinky;
            this.inky = inky;
            this.clyde = clyde;
        }

        public void ItemEat() {
            //그냥 아이템 먹기
            if (map.ground[player.posY, player.posX] == 2) {
                map.ground[player.posY, player.posX] = 0;
                scoreInt += 10;
                eatItem++;
            }

            // 슈퍼아이템 먹기
            if (map.ground[player.posY, player.posX] == 3) {
                map.ground[player.posY, player.posX] = 0;
                scoreInt += 50;
                eatItem++;
                player.isEatMode = true;
                blinky.oneCatch = false;
                pinky.oneCatch = false;
                clyde.oneCatch = false;
                inky.oneCatch = false;
                player.eatModeTimer = 0;
                //blinky.isChangeFirst = true;
                //pinky.isChangeFirst = true;
                //inky.isChangeFirst = true;
                //clyde.isChangeFirst = true;
            }
            score.Text = scoreInt.ToString();
            if (eatItem >= 244)
                GameRestart();
        }
        public void itemCreate(Graphics g) {
            Image item = Image.FromFile(Application.StartupPath + @"\images\item.png");
            Image sItem = Image.FromFile(Application.StartupPath + @"\images\superItem.png");

            for (int i = 1; i < map.ground.GetLength(0); i++) {
                for (int j = 1; j < map.ground.GetLength(1); j++) {
                    if (map.ground[i, j] == 2)
                        g.DrawImage(item, j * 35 + 10, i * 35 + 70);
                    else if (map.ground[i, j] == 3)
                        g.DrawImage(sItem, j * 35 + 5, i * 35 + 65);
                }
            }
        }
        public void pacmanEnemyTouch() { // 플레이어의 목숨은 3개이며, 죽으면 플레이어 포지션 초기값으로 이동, 에너미들 포지션도 초기값으로 이동
            // 슈퍼아이템을 먹었을 때, 
            if (player.isEatMode) {
                if (player.posX == blinky.PosX && player.posY == blinky.PosY) {
                    blinky.isEaten = true;
                }
                if (player.posX == pinky.PosX && player.posY == pinky.PosY) {
                    pinky.isEaten = true;
                }
                if (player.posX == inky.PosX && player.posY == inky.PosY) {
                    inky.isEaten = true;
                }
                if (player.posX == clyde.PosX && player.posY == clyde.PosY) {
                    clyde.isEaten = true;
                }
            }

            // 슈퍼아이템을 먹지 않았을 때
            else {
                if (player.posX == blinky.PosX && player.posY == blinky.PosY) {
                    player.isDie = true; // 애니메이션을 위한 bool값
                    charPosClear();
                }
                if (player.posX == pinky.PosX && player.posY == pinky.PosY) {
                    player.isDie = true;
                    charPosClear();
                }
                if (player.posX == inky.PosX && player.posY == inky.PosY) {
                    player.isDie = true;
                    charPosClear();
                }
                if (player.posX == clyde.PosX && player.posY == clyde.PosY) {
                    player.isDie = true;
                    charPosClear();
                }
            }
        }
        private void charPosClear() {
            player.posX = 13;
            player.posY = 23;

            blinky.PosX = 13;
            blinky.PosY = 11;

            pinky.PosX = 13; //여기부터 초기값 에너미 집안으로 바꿔줘야 함
            pinky.PosY = 14;

            inky.PosX = 11;
            inky.PosY = 14;

            clyde.PosX = 16;
            clyde.PosY = 14;

            pinky.isHome = true;
            inky.isHome = true;
            clyde.isHome = true;
        }
        public void GameRestart() { // 아이템 총 갯수 244
            if (eatItem == 244) { // 블링키 처음위치 (460,431)
                charPosClear();
                player.DirClear();
                GameRestart();
            }
        }
        private void GameClear() {
            map = new Map();
            eatItem = 0;
            Thread.Sleep(1000);
        }
        public void playerEatMode() {
            player.eatModeTimer++;

            if (player.eatModeTimer >= 490) {
                player.isEatMode = false;
            }
        }
    }
}
