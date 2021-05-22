using System;
using System.Windows.Forms;

namespace Pacman {
    class Blinky : Enemy {
        int scatterPosX = 24;
        int scatterPosY = 2;

        int posX = 13;
        int posY = 11;
        new int savePos = 0;
        string dir = "L";
        (string lastDir, bool isPosMove) moveItem = ("L", false);

        bool isMoving = false;
        public bool isChangeFirst = false;

        Panel self;
        Panel blinky;

        Map map = new Map();

        public Blinky(Panel self, Panel blinky)
            : base(self, blinky) {
            this.self = self;
            this.blinky = blinky;
        }
        public void ChaseCheck() {
            posX = base.PosCheckX();
            posY = base.PosCheckY();

            if (isMoving) {
                moveItem = base.EnemyMove(dir, savePos, blinky, posX, posY);
                if (moveItem.isPosMove) {
                    isMoving = base.PosMove(dir, blinky, posX, posY);
                }
                return;
            }
            double min = Double.MaxValue;

            //플레이어 위치 확인
            double x1 = self.Location.X - 10;
            double x2 = Math.Ceiling(x1 / 35);
            int playerX = (int)x2;
            double y1 = self.Location.Y - 70;
            double y2 = Math.Ceiling(y1 / 35);
            int playerY = (int)y2;

            if (moveItem.lastDir != "D" || isChangeFirst) // 위 이동
                if (map.groundWL[posY - 1, posX] != 1) {
                    int x = posX - playerX;
                    int y = (posY - 1) - playerY;
                    distanceU = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    min = distanceU;
                    savePos = blinky.Location.Y;
                    dir = "U";
                }
            if (moveItem.lastDir != "L" || isChangeFirst) // 오른쪽 이동
                if (map.groundWL[posY, posX + 1] != 1) {
                    int x = (posX + 1) - playerX;
                    int y = posY - playerY;
                    distanceR = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    if (min > distanceR) {
                        min = distanceR;
                        savePos = blinky.Location.X;
                        dir = "R";
                    }
                }
            if (moveItem.lastDir != "U" || isChangeFirst) // 아래 이동
                if (map.groundWL[posY + 1, posX] != 1) {
                    int x = posX - playerX;
                    int y = (posY + 1) - playerY;
                    distanceD = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    if (min > distanceD) {
                        min = distanceD;
                        savePos = blinky.Location.Y;
                        dir = "D";
                    }
                }
            if (moveItem.lastDir != "R" || isChangeFirst) // 왼쪽 이동
                if (map.groundWL[posY, posX - 1] != 1) {
                    int x = (posX - 1) - playerX;
                    int y = posY - playerY;
                    distanceL = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    if (min > distanceL) {
                        savePos = blinky.Location.X;
                        dir = "L";
                    }
                }
            isMoving = true;
            isChangeFirst = false;
            moveItem = base.EnemyMove(dir, savePos, blinky, posX, posY);
        }

        public void ScatterCheck() {
            posX = base.PosCheckX();
            posY = base.PosCheckY();

            if (isMoving) {
                moveItem = base.EnemyMove(dir, savePos, blinky, posX, posY);
                if (moveItem.isPosMove) {
                    isMoving = base.PosMove(dir, blinky, posX, posY);
                }
                return;
            }

            double min = Double.MaxValue;

            if (moveItem.lastDir != "D" || isChangeFirst)
                if (map.groundWL[posY - 1, posX] != 1) {
                    int x = posX - scatterPosX;
                    int y = (posY - 1) - scatterPosY;
                    distanceU = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    min = distanceU;
                    savePos = blinky.Location.Y;
                    dir = "U";
                }
            if (moveItem.lastDir != "U" || isChangeFirst)
                if (map.groundWL[posY + 1, posX] != 1) {
                    int x = posX - scatterPosX;
                    int y = (posY + 1) - scatterPosY;
                    distanceD = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    if (min > distanceD) {
                        min = distanceD;
                        savePos = blinky.Location.Y;
                        dir = "D";
                    }
                }
            if (moveItem.lastDir != "R" || isChangeFirst)
                if (map.groundWL[posY, posX - 1] != 1) {
                    int x = (posX - 1) - scatterPosX;
                    int y = posY - scatterPosY;
                    distanceL = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    if (min > distanceL) {
                        min = distanceL;
                        savePos = blinky.Location.X;
                        dir = "L";
                    }
                }
            if (moveItem.lastDir != "L" || isChangeFirst)
                if (map.groundWL[posY, posX + 1] != 1) {
                    int x = (posX + 1) - scatterPosX;
                    int y = posY - scatterPosY;
                    distanceR = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    if (min > distanceR) {
                        savePos = blinky.Location.X;
                        dir = "R";
                    }
                }
            isMoving = true;
            isChangeFirst = false;
            moveItem = base.EnemyMove(dir, savePos, blinky, posX, posY);
        }
    }
}
