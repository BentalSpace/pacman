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
        double distanceU = 0;
        double distanceD = 0;
        double distanceL = 0;
        double distanceR = 0;
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
                BlinkyChaseMove();
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
            BlinkyChaseMove();
        }
        private void BlinkyChaseMove() {
            switch (dir) {
                case "U":
                    if (savePos - 30 >= red.Location.Y)
                        PosMove();
                    else {
                        red.Top -= speed;
                        redLastDir = "U";
                    }
                    break;
                case "D":
                    if (savePos + 30 <= red.Location.Y)
                        PosMove();
                    else {
                        red.Top += speed;
                        redLastDir = "D";
                    }
                    break;
                case "L":
                    if (savePos - 30 >= red.Location.X)
                        PosMove();
                    else {
                        red.Left -= speed;
                        redLastDir = "L";
                    }
                    break;
                case "R":
                    if (savePos + 30 <= red.Location.X)
                        PosMove();
                    else {
                        red.Left += speed;
                        redLastDir = "R";
                    }
                    break;
            }
        }
        public void PosMove() {
            switch (dir) {
                case "U":
                    if (red.Location.Y >= (redPosY * 35 + 70) - 23)
                        red.Top -= speed;
                    else
                        isMoving = false;
                        //posChange = false;
                    break;
                case "D":
                    if (red.Location.Y <= (redPosY * 35 + 70) - 25)
                        red.Top += speed;
                    else
                        isMoving = false;
                    //posChange = false;
                    break;
                case "L":
                    if (red.Location.X >= (redPosX * 35 + 10) - 23)
                        red.Left -= speed;
                    else
                        isMoving = false;
                    //posChange = false;
                    break;
                case "R":
                    if (red.Location.X <= (redPosX * 35 + 10) - 25)
                        red.Left += speed;
                    else
                        isMoving = false;
                    //posChange = false;
                    break;
            }
        }
        public void BlinkyScatter() {
            if (isMoving) {
                BlinkyChaseMove();
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
            BlinkyChaseMove();
        }

        public void PosCheak() {
            double x1 = red.Location.X - 10;
            double x2 = Math.Ceiling(x1 / 35);
            if (redPosX != (int)x2)
                posChange = true;
            redPosX = (int)x2;

            double y1 = red.Location.Y - 70;
            double y2 = Math.Ceiling(y1 / 35);
            if (redPosY != (int)y2)
                posChange = true;
            redPosY = (int)y2;
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
