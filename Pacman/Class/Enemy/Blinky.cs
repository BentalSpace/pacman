using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pacman {
    class Blinky : Enemy {
        Random rand = new Random();
        int scatterPosX = 24;
        int scatterPosY = 2;

        int playerX = 13;
        int playerY = 23;

        string dir = "L";
        (string lastDir, bool isMoving, int posX, int posY, int moveX, int moveY) moveItem = ("L", false, 13, 11, 0, 0);
        public int PosX {
            get { return moveItem.posX; }
            set { moveItem.posX = value; }
        }
        
        public int PosY {
            get { return moveItem.posY; }
            set { moveItem.posY = value; }
        } 

        public bool isChangeFirst = false;

        Map map = new Map();
        Player player;

        public Blinky(Player player)
            : base() {
            this.player = player;
        }
        public override void ChaseCheck() {
            if (moveItem.isMoving) {
                moveItem = base.EnemyMove(dir, moveItem.posX, moveItem.posY, moveItem.moveX, moveItem.moveY);
                return;
            }
            double min = Double.MaxValue;
            //플레이어 위치 확인
            playerX = player.posX;
            playerY = player.posY;

            if (moveItem.lastDir != "D" || isChangeFirst) // 위 이동
                if (map.groundWL[moveItem.posY - 1, moveItem.posX] != 1) {
                    int x = moveItem.posX - playerX;
                    int y = (moveItem.posY - 1) - playerY;
                    distanceU = (x * x) + (y * y);
                    min = distanceU;
                    dir = "U";
                }
            if (moveItem.lastDir != "L" || isChangeFirst) // 오른쪽 이동
                if (map.groundWL[moveItem.posY, moveItem.posX + 1] != 1) {
                    int x = (moveItem.posX + 1) - playerX;
                    int y = moveItem.posY - playerY;
                    distanceR = (x * x) + (y * y);
                    if (min > distanceR) {
                        min = distanceR;
                        dir = "R";
                    }
                }
            if (moveItem.lastDir != "U" || isChangeFirst) // 아래 이동
                if (map.groundWL[moveItem.posY + 1, moveItem.posX] != 1) {
                    int x = moveItem.posX - playerX;
                    int y = (moveItem.posY + 1) - playerY;
                    distanceD = (x * x) + (y * y);
                    if (min > distanceD) {
                        min = distanceD;
                        dir = "D";
                    }
                }
            if (moveItem.lastDir != "R" || isChangeFirst) // 왼쪽 이동
                if (map.groundWL[moveItem.posY, moveItem.posX - 1] != 1) {
                    int x = (moveItem.posX - 1) - playerX;
                    int y = moveItem.posY - playerY;
                    distanceL = (x * x) + (y * y);
                    if (min > distanceL) {
                        dir = "L";
                    }
                }
            moveItem.isMoving = true;
            isChangeFirst = false;
            moveItem = base.EnemyMove(dir, moveItem.posX, moveItem.posY, moveItem.moveX, moveItem.moveY);
        }

        public override void ScatterCheck() {

            if (moveItem.isMoving) {
                moveItem = base.EnemyMove(dir, moveItem.posX, moveItem.posY, moveItem.moveX, moveItem.moveY);
                return;
            }

            double min = Double.MaxValue;
            int posX = moveItem.posX;
            int posY = moveItem.posY;

            if (moveItem.lastDir != "D" || isChangeFirst)
                if (map.groundWL[posY - 1, posX] != 1) {
                    int x = posX - scatterPosX;
                    int y = (posY - 1) - scatterPosY;
                    distanceU = (x * x) + (y * y);
                    min = distanceU;
                    dir = "U";
                }
            if (moveItem.lastDir != "U" || isChangeFirst)
                if (map.groundWL[posY + 1, posX] != 1) {
                    int x = posX - scatterPosX;
                    int y = (posY + 1) - scatterPosY;
                    distanceD = (x * x) + (y * y);
                    if (min > distanceD) {
                        min = distanceD;
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
                        dir = "L";
                    }
                }
            if (moveItem.lastDir != "L" || isChangeFirst)
                if (map.groundWL[posY, posX + 1] != 1) {
                    int x = (posX + 1) - scatterPosX;
                    int y = posY - scatterPosY;
                    distanceR = (x * x) + (y * y);
                    if (min > distanceR) {
                        dir = "R";
                    }
                }
            moveItem.isMoving = true;
            isChangeFirst = false;
            moveItem = base.EnemyMove(dir, posX, posY, moveItem.moveX, moveItem.moveY);
        }
        public void FrightenedMode() {
            if (moveItem.isMoving) {
                moveItem = base.EnemyMove(dir, moveItem.posX, moveItem.posY, moveItem.moveX, moveItem.moveY);
                return;
            }

            int count = 0;

            int posX = moveItem.posX;
            int posY = moveItem.posY;

            if (moveItem.lastDir != "D" || isChangeFirst)
                if (map.groundWL[posY - 1, posX] != 1) {
                    count++;
                }
            if (moveItem.lastDir != "U" || isChangeFirst)
                if (map.groundWL[posY + 1, posX] != 1) {
                    count++;
                }
            if (moveItem.lastDir != "R" || isChangeFirst)
                if (map.groundWL[posY, posX - 1] != 1) {
                    count++;
                }
            if (moveItem.lastDir != "L" || isChangeFirst)
                if (map.groundWL[posY, posX + 1] != 1) {
                    count++;
                }
            int dirNum = rand.Next(0, count);
            count = 0;
            if (moveItem.lastDir != "D" || isChangeFirst)
                if (map.groundWL[posY - 1, posX] != 1) {
                    if(count == dirNum) {
                        dir = "U";
                    }
                    count++;
                }
            if (moveItem.lastDir != "U" || isChangeFirst)
                if (map.groundWL[posY + 1, posX] != 1) {
                    if (count == dirNum) {
                        dir = "D";
                    }
                    count++;
                }
            if (moveItem.lastDir != "R" || isChangeFirst)
                if (map.groundWL[posY, posX - 1] != 1) {
                    if (count == dirNum) {
                        dir = "L";
                    }
                    count++;
                }
            if (moveItem.lastDir != "L" || isChangeFirst)
                if (map.groundWL[posY, posX + 1] != 1) {
                    if (count == dirNum) {
                        dir = "R";
                    }
                }
            moveItem.isMoving = true;
            isChangeFirst = false;
            moveItem = base.EnemyMove(dir, posX, posY, moveItem.moveX, moveItem.moveY);
        }
        public override void enemyDraw(Graphics g) {
            //Image imageBlinky = Image.FromFile("G:\\Git\\pacman\\images\\blinkyR " + 1 + ".png");
            Image imageBlinky1 = Image.FromFile(Application.StartupPath + @"\images\blinkyR " + 1 + ".png");

            g.DrawImage(imageBlinky1, moveItem.posX * 35 - 10 + moveItem.moveX, moveItem.posY * 35 + 45 + moveItem.moveY);
        }
    }
}
