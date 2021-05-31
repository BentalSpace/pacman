using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pacman {
    class Blinky : Enemy {
        int scatterPosX = 24;
        int scatterPosY = 2;

        int posX = 13;
        int posY = 11;
        int moveX = 0;
        int moveY = 0;
        new int savePos = 0;
        string dir = "L";
        (string lastDir, bool isPosMove) moveItem = ("L", false);

        bool isMoving = false;
        public bool isChangeFirst = false;

        Panel self;
        Panel blinky;

        Map map = new Map();
        Player player = new Player();


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
            //double x1 = self.Location.X - 10;
            //double x2 = Math.Ceiling(x1 / 35);
            //int playerX = (int)x2;
            //double y1 = self.Location.Y - 70;
            //double y2 = Math.Ceiling(y1 / 35);
            //int playerY = (int)y2;

            int playerX = player.POSX;
            int playerY = player.POSY;

            if (moveItem.lastDir != "D" || isChangeFirst) // 위 이동
                if (map.groundWL[posY - 1, posX] != 1) {
                    int x = posX - playerX;
                    int y = (posY - 1) - playerY;
                    distanceU = (x * x) + (y * y);
                    min = distanceU;
                    savePos = blinky.Location.Y;
                    dir = "U";
                }
            if (moveItem.lastDir != "L" || isChangeFirst) // 오른쪽 이동
                if (map.groundWL[posY, posX + 1] != 1) {
                    int x = (posX + 1) - playerX;
                    int y = posY - playerY;
                    distanceR = (x * x) + (y * y);
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
                    distanceD = (x * x) + (y * y);
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
                    distanceL = (x * x) + (y * y);
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
                    distanceU = (x * x) + (y * y);
                    min = distanceU;
                    savePos = blinky.Location.Y;
                    dir = "U";
                }
            if (moveItem.lastDir != "U" || isChangeFirst)
                if (map.groundWL[posY + 1, posX] != 1) {
                    int x = posX - scatterPosX;
                    int y = (posY + 1) - scatterPosY;
                    distanceD = (x * x) + (y * y);
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
                    distanceL = (x * x) + (y * y);
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
                    distanceR = (x * x) + (y * y);
                    if (min > distanceR) {
                        savePos = blinky.Location.X;
                        dir = "R";
                    }
                }
            isMoving = true;
            isChangeFirst = false;
            moveItem = base.EnemyMove(dir, savePos, blinky, posX, posY);
        }
        public override void enemyDraw(Graphics g) {
            Image imageBlinky = Image.FromFile("G:\\Git\\pacman\\images\\blinkyR " + 1 + ".png");

            g.DrawImage(imageBlinky, posX * 35 - 10 + moveX, posY * 35 + 45 + moveY);
        }
    }
}
