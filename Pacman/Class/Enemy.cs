using System;
using System.Windows.Forms;

namespace Pacman {
    class Enemy {
        const int scatterPosX = 21;
        const int scatterPosY = 5;

        int speed = 3;
        int redPosX = 13;
        int redPosY = 11;
        public int savePos = 0;
        protected double distanceU = 0;
        protected double distanceD = 0;
        protected double distanceL = 0;
        protected double distanceR = 0;
        string redLastDir = "L";
       
        string dir = "L";

        public bool posChange = false;
        bool isMoving = false;
        bool isChaseScatter = false; // true일땐 추격 / false일땐 해산

        Panel self;
        Panel red;

        //GameManager manager = new GameManager();
        Map map = new Map();

        public Enemy(Panel self, Panel red) {
            this.self = self;
            this.red = red;
        }
        public void BlinkyChaseCheak() {
            if (isMoving) {
                //BlinkyChaseMove();
                return;
            }

            double min = Double.MaxValue;
            double x1 = self.Location.X - 10;
            double x2 = Math.Ceiling(x1 / 35);
            int playerX = (int)x2;
            double y1 = self.Location.Y - 70;
            double y2 = Math.Ceiling(y1 / 35);
            int playerY = (int)y2;

            //유클리드 거리 계산
            if (redLastDir != "D") // 위 이동
                if (map.groundWL[redPosY - 1, redPosX] != 1) {
                    int x = redPosX - playerX;
                    int y = (redPosY - 1) - playerY;
                    distanceU = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    min = distanceU;
                    savePos = red.Location.Y;
                    dir = "U";
                }
            if (redLastDir != "U") // 아래 이동
                if (map.groundWL[redPosY + 1, redPosX] != 1) {
                    int x = redPosX - playerX;
                    int y = (redPosY + 1) - playerY;
                    distanceD = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    if (min > distanceD) {
                        min = distanceD;
                        savePos = red.Location.Y;
                        dir = "D";
                    }
                }
            if (redLastDir != "R") // 왼쪽 이동
                if (map.groundWL[redPosY, redPosX - 1] != 1) {
                    int x = (redPosX - 1) - playerX;
                    int y = redPosY - playerY;
                    distanceL = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    if (min > distanceL) {
                        min = distanceL;
                        savePos = red.Location.X;
                        dir = "L";
                    }
                }
            if (redLastDir != "L") // 오른쪽 이동
                if (map.groundWL[redPosY, redPosX + 1] != 1) {
                    int x = (redPosX + 1) - playerX;
                    int y = redPosY - playerY;
                    distanceR = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    if (min > distanceR) {
                        savePos = red.Location.X;
                        dir = "R";
                    }
                }
            isMoving = true;
        }
        protected (string, bool) EnemyMove(string dir, int savePos, Panel enemy, int posX, int posY) {
            //string lastDir = null;
            (string lastDir, bool isPosMove) outItem = (null, false);
            switch (dir) {
                case "U":
                    if (savePos - 30 >= enemy.Location.Y)
                        outItem.isPosMove = true;
                    else {
                        enemy.Top -= speed;
                    }
                    outItem.lastDir = "U";
                    break;
                case "D":
                    if (savePos + 30 <= enemy.Location.Y)
                        outItem.isPosMove = true;
                    else {
                        enemy.Top += speed;
                    }
                    outItem.lastDir = "D";
                    break;
                case "L":
                    if (savePos - 30 >= enemy.Location.X)
                        outItem.isPosMove = true;
                    else {
                        enemy.Left -= speed;
                    }
                    outItem.lastDir = "L";
                    break;
                case "R":
                    if (savePos + 30 <= enemy.Location.X)
                        outItem.isPosMove = true;
                    else {
                        enemy.Left += speed;
                    }
                    outItem.lastDir = "R";
                    break;
            }
            return outItem;
        }
        protected bool PosMove(string dir, Panel enemy, int posX, int posY) {
            bool parentIsMoving = true;
            switch (dir) {
                case "U":
                    if (enemy.Location.Y >= (posY * 35 + 70) - 23)
                        enemy.Top -= speed;
                    else
                        parentIsMoving = false;
                    break;
                case "D":
                    if (enemy.Location.Y <= (posY * 35 + 70) - 25)
                        enemy.Top += speed;
                    else
                        parentIsMoving = false;
                    break;
                case "L":
                    if (enemy.Location.X >= (posX * 35 + 10) - 23)
                        enemy.Left -= speed;
                    else
                        parentIsMoving = false;
                    break;
                case "R":
                    if (enemy.Location.X <= (posX * 35 + 10) - 25)
                        enemy.Left += speed;
                    else
                        parentIsMoving = false;
                    break;
            }
            return parentIsMoving;
        }
        public void BlinkyScatter() {
            if (isMoving) {
                //BlinkyChaseMove();
                return;
            }
            //1시 y축 1 - 5 / x축 21 - 26 (21,5)위치를 기준으로 잡으면 될듯
            double min = Double.MaxValue;

            if (map.groundWL[redPosY-1, redPosX] != 1) {
                int x = redPosX - scatterPosX;
                int y = (redPosY - 1) - scatterPosX;
                distanceU = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                min = distanceU;
                savePos = red.Location.Y;
                dir = "U";
            }
            if(map.groundWL[redPosY+1, redPosX] != 1) {
                int x = redPosX - scatterPosX;
                int y = (redPosY + 1) - scatterPosY;
                distanceD = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                if (min > distanceD) {
                    min = distanceD;
                    savePos = red.Location.Y;
                    dir = "D";
                }
            }
            if(map.groundWL[redPosY, redPosX - 1] != 1) {
                int x = (redPosX + 1) - scatterPosX;
                int y = redPosY - scatterPosY;
                distanceL = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                if (min > distanceL) {
                    min = distanceL;
                    savePos = red.Location.X;
                    dir = "L";
                }
            }
            if(map.groundWL[redPosY, redPosX + 1] != 1) {
                int x = (redPosX - 1) - scatterPosX;
                int y = redPosY - scatterPosY;
                distanceR = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                if (min > distanceR) {
                    savePos = red.Location.X;
                    dir = "R";
                }
            }
            isMoving = true;
            //BlinkyChaseMove();
        }

        protected int PosCheckX() {
            int posX = 0;
            double x1 = red.Location.X - 10;
            double x2 = Math.Ceiling(x1 / 35);
            if (redPosX != (int)x2)
                posChange = true;
            posX = (int)x2;

            return posX;
        }
        protected int PosCheckY() {
            int posY = 0;
            double y1 = red.Location.Y - 70;
            double y2 = Math.Ceiling(y1 / 35);
            if (redPosY != (int)y2)
                posChange = true;
            posY = (int)y2;

            return posY;
        }
        //public void ChaseScatterChange() {
        //    switch (manager.level) {
        //        case 1:
        //            Thread.Sleep(7000);
        //            isChaseScatter = true;
        //            Thread.Sleep(20000);
        //            isChaseScatter = false;
        //            Thread.Sleep(7000);
        //            isChaseScatter = true;
        //            Thread.Sleep(20000);
        //            isChaseScatter = false;
        //            Thread.Sleep(5000);
        //            isChaseScatter = true;
        //            Thread.Sleep(20000);
        //            isChaseScatter = false;
        //            Thread.Sleep(5000);
        //            isChaseScatter = true;
        //            break;
        //        case 2:
        //        case 3:
        //        case 4:
        //            Thread.Sleep(7000);
        //            MessageBox.Show("TeEeeest");
        //            isChaseScatter = true;
        //            Thread.Sleep(20000);
        //            isChaseScatter = false;
        //            Thread.Sleep(7000);
        //            isChaseScatter = true;
        //            Thread.Sleep(20000);
        //            isChaseScatter = false;
        //            Thread.Sleep(5000);
        //            isChaseScatter = true;
        //            Thread.Sleep(1033140);
        //            isChaseScatter = false;
        //            Thread.Sleep(10);
        //            isChaseScatter = true;
        //            break;
        //        default:
        //            Thread.Sleep(5000);
        //            isChaseScatter = true;
        //            Thread.Sleep(20000);
        //            isChaseScatter = false;
        //            Thread.Sleep(5000);
        //            isChaseScatter = true;
        //            Thread.Sleep(20000);
        //            isChaseScatter = false;
        //            Thread.Sleep(5000);
        //            isChaseScatter = true;
        //            Thread.Sleep(1037140);
        //            isChaseScatter = false;
        //            Thread.Sleep(10);
        //            isChaseScatter = true;
        //            break;
        //    }
        //}
    }
}
