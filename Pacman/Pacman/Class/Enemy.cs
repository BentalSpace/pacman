using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Pacman {
    class Enemy {
        int redPosX = 0;
        int redPosY = 0;
        double distanceU = 0;
        double distanceD = 0;
        double distanceL = 0;
        double distanceR = 0;
        string redLastDir = "L";

        bool isChaseScatter = false; // true일땐 추격 / false일땐 해산

        Panel self;
        Panel red;

        GameManager manager = new GameManager();
        Player player = new Player();
        Map map = new Map();
        public Enemy(Panel self, Panel red) {
            this.self = self;
            this.red = red;
        }
        public void BlinkyChase() {
            //유클리드 거리 계산
            if (redLastDir != "D") // 위 이동
                if(map.ground[redPosY-1, redPosX] != 1) {
                    int x = redPosX - player.posX;
                    int y = (redPosY-1) - player.posY;
                    distanceU = Math.Pow(x, 2) + Math.Pow(y, 2);
                }
            if(redLastDir != "U") // 아래 이동
                if(map.ground[redPosY +1, redPosX] != 1) {
                    int x = redPosX - player.posX;
                    int y = (redPosY + 1) - player.posY;
                    distanceD = Math.Pow(x, 2) + Math.Pow(y, 2);
                }
            if(redLastDir != "R") // 왼쪽 이동
                if(map.ground[redPosY, redPosX-1] != 1) {
                    int x = (redPosX - 1) - player.posX;
                    int y = redPosY - player.posY;
                    distanceL = Math.Pow(x, 2) + Math.Pow(y, 2);
                }
            if(redLastDir != "L") // 오른쪽 이동
                if(map.ground[redPosY, redPosX+1] != 1) {
                    int x = (redPosX + 1) - player.posX;
                    int y = redPosY - player.posY;
                    distanceR = Math.Pow(x, 2) + Math.Pow(y, 2);
                }
            //가장 작은 수를 찾아서, 그 방향으로 이동한다.
        }
        public void BlinkyScatter() {
            //1시
        }

        public void PosCheak() {
            double x1 = red.Location.X - 10;
            double x2 = Math.Ceiling(x1 / 35);
            redPosX = (int)x2;
            double y1 = red.Location.Y - 70;
            double y2 = Math.Ceiling(y1 / 35);
            redPosY = (int)y2;
        }
        public void ChaseScatterChange() {
            switch (manager.level) {
                case 1:
                    Thread.Sleep(7000);
                    isChaseScatter = true;
                    Thread.Sleep(20000);
                    isChaseScatter = false;
                    Thread.Sleep(7000);
                    isChaseScatter = true;
                    Thread.Sleep(20000);
                    isChaseScatter = false;
                    Thread.Sleep(5000);
                    isChaseScatter = true;
                    Thread.Sleep(20000);
                    isChaseScatter = false;
                    Thread.Sleep(5000);
                    isChaseScatter = true;
                    break;
                case 2:
                case 3:
                case 4:
                    Thread.Sleep(7000);
                    MessageBox.Show("TeEeeest");
                    isChaseScatter = true;
                    Thread.Sleep(20000);
                    isChaseScatter = false;
                    Thread.Sleep(7000);
                    isChaseScatter = true;
                    Thread.Sleep(20000);
                    isChaseScatter = false;
                    Thread.Sleep(5000);
                    isChaseScatter = true;
                    Thread.Sleep(1033140);
                    isChaseScatter = false;
                    Thread.Sleep(10);
                    isChaseScatter = true;
                    break;
                default:
                    Thread.Sleep(5000);
                    isChaseScatter = true;
                    Thread.Sleep(20000);
                    isChaseScatter = false;
                    Thread.Sleep(5000);
                    isChaseScatter = true;
                    Thread.Sleep(20000);
                    isChaseScatter = false;
                    Thread.Sleep(5000);
                    isChaseScatter = true;
                    Thread.Sleep(1037140);
                    isChaseScatter = false;
                    Thread.Sleep(10);
                    isChaseScatter = true;
                    break;
            }
        }
        public void test() {
            MessageBox.Show("Test");
        }
    }
}
