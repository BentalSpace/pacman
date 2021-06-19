using System;
using System.Windows.Forms;
using System.Drawing;

namespace Pacman {
    class Pinky : Enemy {
        Random rand = new Random();
        int scatterPosX = 2;
        int scatterPosY = 2;

        int playerAgoX = 0;
        int playerAgoY = 0;
        int playerX = 13;
        int playerY = 23;

        string dir = "L";
        (string lastDir, bool isMoving, int posX, int posY, int moveX, int moveY) moveItem = ("L", false, 12, 11, 0, 0);
        public int PosX {
            get { return moveItem.posX; }
            set { moveItem.posX = value; }
        }
        public int PosY {
            get { return moveItem.posY; }
            set { moveItem.posY = value; }
        }

        public bool isChangeFirst = false;
        bool isUp = false;
        bool isDown = false;
        bool isLeft = true;
        bool isRight = false;

        Map map = new Map();
        Player player;

        public Pinky(Player player) : base() {
            this.player = player;
        }
        private void DirClear() {
            isUp = false;
            isDown = false;
            isLeft = false;
            isRight = false;
        }
        public void PlayerMoveCheck() {
            playerAgoX = playerX;
            playerAgoY = playerY;

            playerX = player.posX;
            playerY = player.posY;

            if (playerX < playerAgoX) {
                DirClear();
                isLeft = true;
            }
            else if (playerX > playerAgoX) {
                DirClear();
                isRight = true;
            }
            else if (playerY < playerAgoY) {
                DirClear();
                isUp = true;
            }
            else if (playerY > playerAgoY) {
                DirClear();
                isDown = true;
            }
        }
        public override void ChaseCheck() {

            if (moveItem.isMoving) {
                moveItem = base.EnemyMove(dir, moveItem.posX, moveItem.posY, moveItem.moveX, moveItem.moveY);
                return;
            }
            double min = Double.MaxValue;

            if (moveItem.lastDir != "D" || isChangeFirst) // 위 이동
                if (map.groundWL[moveItem.posY - 1, moveItem.posX] != 1) {
                    int x = 0;
                    int y = 0;
                    if (isUp) {
                        x = moveItem.posX - playerX;
                        y = (moveItem.posY - 1) - (playerY - 4);
                    }
                    if (isDown) {
                        x = moveItem.posX - playerX;
                        y = (moveItem.posY - 1) - (playerY + 4);
                    }
                    if (isLeft) {
                        x = moveItem.posX - (playerX - 4);
                        y = (moveItem.posY - 1) - playerY;
                    }
                    if (isRight) {
                        x = moveItem.posX - (playerX + 4);
                        y = (moveItem.posY - 1) - playerY;
                    }
                    distanceU = (x * x) + (y * y);
                    min = distanceU;
                    dir = "U";
                }
            if (moveItem.lastDir != "L" || isChangeFirst) // 오른쪽 이동
                if (map.groundWL[moveItem.posY, moveItem.posX + 1] != 1) {
                    int x = 0;
                    int y = 0;
                    if (isUp) {
                        x = (moveItem.posX + 1) - playerX;
                        y = moveItem.posY - (playerY - 4);
                    }
                    if (isDown) {
                        x = (moveItem.posX + 1) - playerX;
                        y = moveItem.posY - (playerY + 4);
                    }
                    if (isLeft) {
                        x = (moveItem.posX + 1) - (playerX - 4);
                        y = moveItem.posY - playerY;
                    }
                    if (isRight) {
                        x = (moveItem.posX + 1) - (playerX + 4);
                        y = moveItem.posY - playerY;
                    }
                    distanceR = (x * x) + (y * y);
                    if (min > distanceR) {
                        min = distanceR;
                        dir = "R";
                    }
                }
            if (moveItem.lastDir != "U" || isChangeFirst) // 아래 이동
                if (map.groundWL[moveItem.posY + 1, moveItem.posX] != 1) {
                    int x = 0;
                    int y = 0;
                    if (isUp) {
                        x = moveItem.posX - playerX;
                        y = (moveItem.posY + 1) - (playerY - 4);
                    }
                    if (isDown) {
                        x = moveItem.posX - playerX;
                        y = (moveItem.posY + 1) - (playerY + 4);
                    }
                    if (isLeft) {
                        x = moveItem.posX - (playerX - 4);
                        y = (moveItem.posY + 1) - playerY;
                    }
                    if (isRight) {
                        x = moveItem.posX - (playerX + 4);
                        y = (moveItem.posY + 1) - playerY;
                    }
                    distanceD = (x * x) + (y * y);
                    if (min > distanceD) {
                        min = distanceD;
                        dir = "D";
                    }
                }
            if (moveItem.lastDir != "R" || isChangeFirst) // 왼쪽 이동
                if (map.groundWL[moveItem.posY, moveItem.posX - 1] != 1) {
                    int x = 0;
                    int y = 0;
                    if (isUp) {
                        x = (moveItem.posX - 1) - playerX;
                        y = moveItem.posY - (playerY - 4);
                    }
                    if (isDown) {
                        x = (moveItem.posX - 1) - playerX;
                        y = moveItem.posY - (playerY + 4);
                    }
                    if (isLeft) {
                        x = (moveItem.posX - 1) - (playerX - 4);
                        y = moveItem.posY - playerY;
                    }
                    if (isRight) {
                        x = (moveItem.posX - 1) - (playerX + 4);
                        y = moveItem.posY - playerY;
                    }
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

            if (moveItem.lastDir != "D" || isChangeFirst)
                if (map.groundWL[moveItem.posY - 1, moveItem.posX] != 1) {
                    int x = moveItem.posX - scatterPosX;
                    int y = (moveItem.posY - 1) - scatterPosY;
                    distanceU = (x * x) + (y * y);
                    min = distanceU;
                    dir = "U";
                }
            if (moveItem.lastDir != "U" || isChangeFirst)
                if (map.groundWL[moveItem.posY + 1, moveItem.posX] != 1) {
                    int x = moveItem.posX - scatterPosX;
                    int y = (moveItem.posY + 1) - scatterPosY;
                    distanceD = (x * x) + (y * y);
                    if (min > distanceD) {
                        min = distanceD;
                        dir = "D";
                    }
                }
            if (moveItem.lastDir != "R" || isChangeFirst)
                if (map.groundWL[moveItem.posY, moveItem.posX - 1] != 1) {
                    int x = (moveItem.posX - 1) - scatterPosX;
                    int y = moveItem.posY - scatterPosY;
                    distanceL = (x * x) + (y * y);
                    if (min > distanceL) {
                        min = distanceL;
                        dir = "L";
                    }
                }
            if (moveItem.lastDir != "L" || isChangeFirst)
                if (map.groundWL[moveItem.posY, moveItem.posX + 1] != 1) {
                    int x = (moveItem.posX + 1) - scatterPosX;
                    int y = moveItem.posY - scatterPosY;
                    distanceR = (x * x) + (y * y);
                    if (min > distanceR) {
                        dir = "R";
                    }
                }
            moveItem.isMoving = true;
            isChangeFirst = false;
            moveItem = base.EnemyMove(dir, moveItem.posX, moveItem.posY, moveItem.moveX, moveItem.moveY);
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
                    if (count == dirNum) {
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
            Image imagePinky = Image.FromFile(Application.StartupPath + @"\images\pinkyR " + 1 + ".png");

            g.DrawImage(imagePinky, moveItem.posX * 35 - 10 + moveItem.moveX, moveItem.posY * 35 + 45 + moveItem.moveY);
        }
    }
}
