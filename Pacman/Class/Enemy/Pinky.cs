using System;
using System.Windows.Forms;
using System.Drawing;

namespace Pacman {
    class Pinky : Enemy {
        int scatterPosX = 2;
        int scatterPosY = 2;

        int playerAgoX = 0;
        int playerAgoY = 0;
        int playerX = 13;
        int playerY = 23;

        string dir = "L";
        (string lastDir, bool isMoving, int posX, int posY, int moveX, int moveY) moveItem = ("L", false, 12, 11, 0, 0);

        public bool isChangeFirst = false;
        bool isUp = false;
        bool isDown = false;
        bool isLeft = true;
        bool isRight = false;

        Map map = new Map();

        Label temp;
        public Pinky(Label temp) : base() {
            this.temp = temp;
        }
        private void DirClear() {
            isUp = false;
            isDown = false;
            isLeft = true;
            isRight = false;
        }
        //public void PlayerMoveCheck() {
        //    playerAgoX = playerX;
        //    playerAgoY = playerY;

        //    double x1 = self.Location.X - 10;
        //    double x2 = Math.Ceiling(x1 / 35);
        //    playerX = (int)x2;
        //    double y1 = self.Location.Y - 70;
        //    double y2 = Math.Ceiling(y1 / 35);
        //    playerY = (int)y2;

        //    if (playerX < playerAgoX) {
        //        DirClear();
        //        isLeft = true;
        //    }
        //    else if (playerX > playerAgoX) {
        //        DirClear();
        //        isRight = true;
        //    }
        //    else if (playerY < playerAgoY) {
        //        DirClear();
        //        isUp = true;
        //    }
        //    else if (playerY > playerAgoY) {
        //        DirClear();
        //        isDown = true;
        //    }
        //}
        public void ChaseCheck() {

            string playerTemp = temp.Text;
            string[] playerPos = temp.Text.Split(',');

            playerX = Int32.Parse(playerPos[0]);
            playerY = Int32.Parse(playerPos[1]);

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
        public void ScatterCheck() {

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

        public override void enemyDraw(Graphics g) {
            Image imagePinky = Image.FromFile("G:\\Git\\pacman\\images\\pinkyR " + 1 + ".png");

            g.DrawImage(imagePinky, moveItem.posX * 35 - 10 + moveItem.moveX, moveItem.posY * 35 + 45 + moveItem.moveY);
        }
    }
}
