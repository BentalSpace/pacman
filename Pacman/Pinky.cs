using System;
using System.Windows.Forms;

namespace Pacman {
    class Pinky : Enemy {
        int scatterPosX = 24;
        int scatterPosY = 2;

        int playerAgoX = 0;
        int playerAgoY = 0;
        int playerX = 0;
        int playerY = 0;

        int posX = 13;
        int posY = 11;
        new int savePos = 0;
        string dir = "L";
        (string lastDir, bool isPosMove) moveItem = ("L", false);

        bool isMoving = false;
        public bool isChangeFirst = false;
        bool isUp = false;
        bool isDown = false;
        bool isLeft = false;
        bool isRight = false;

        Panel self;
        Panel pinky;

        Map map = new Map();
        public Pinky(Panel self, Panel pinky) : base(self, pinky) {
            this.self = self;
            this.pinky = pinky;
        }
        private void DirClear() {
            isUp = false;
            isDown = false;
            isLeft = true;
            isRight = false;
        }
        public void PlayerMoveCheck() {
            playerAgoX = playerX;
            playerAgoY = playerY;

            double x1 = self.Location.X - 10;
            double x2 = Math.Ceiling(x1 / 35);
            playerX = (int)x2;
            double y1 = self.Location.Y - 70;
            double y2 = Math.Ceiling(y1 / 35);
            playerY = (int)y2;

            if (playerX < playerAgoX) {
                DirClear();
                isLeft = true;
            }
            else if(playerX > playerAgoX) {
                DirClear();
                isRight = true;
            }
            else if(playerY < playerAgoY) {
                DirClear();
                isUp = true;
            }
            else if(playerY > playerAgoY) {
                DirClear();
                isDown = true;
            }
        }
        public void ChaseCheck() {
            posX = base.PosCheckX();
            posY = base.PosCheckY();

            if (isMoving) {
                moveItem = base.EnemyMove(dir, savePos, pinky, posX, posY);
                if (moveItem.isPosMove) {
                    isMoving = base.PosMove(dir, pinky, posX, posY);
                }
                return;
            }

            double min = Double.MaxValue;

            if (moveItem.lastDir != "D" || isChangeFirst) // 위 이동
                if (map.groundWL[posY - 1, posX] != 1) {
                    int x = 0;
                    int y = 0;
                    if (isUp) {
                        x = posX - playerX;
                        y = (posY - 1) - (playerY - 4);
                    }
                    if (isDown) {
                        x = posX - playerX;
                        y = (posY - 1) - (playerY + 4);
                    }
                    if (isLeft) {
                        x = posX - (playerX - 4);
                        y = (posY - 1) - playerY;
                    }
                    if (isRight) {
                        x = posX - (playerX + 4);
                        y = (posY - 1) - playerY;
                    }
                    distanceU = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    min = distanceU;
                    savePos = pinky.Location.Y;
                    dir = "U";
                }
            if (moveItem.lastDir != "L" || isChangeFirst) // 오른쪽 이동
                if (map.groundWL[posY, posX + 1] != 1) {
                    int x = 0;
                    int y = 0;
                    if (isUp) {
                        x = (posX + 1) - playerX;
                        y = posY - (playerY - 4);
                    }
                    if (isDown) {
                        x = (posX + 1) - playerX;
                        y = posY - (playerY + 4);
                    }
                    if (isLeft) {
                        x = (posX + 1) - (playerX - 4);
                        y = posY - playerY;
                    }
                    if (isRight) {
                        x = (posX + 1) - (playerX + 4);
                        y = posY - playerY;
                    }
                    distanceR = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    if (min > distanceR) {
                        min = distanceR;
                        savePos = pinky.Location.X;
                        dir = "R";
                    }
                }
            if (moveItem.lastDir != "U" || isChangeFirst) // 아래 이동
                if (map.groundWL[posY + 1, posX] != 1) {
                    int x = 0;
                    int y = 0;
                    if (isUp) {
                        x = posX - playerX;
                        y = (posY + 1) - (playerY - 4);
                    }
                    if (isDown) {
                        x = posX - playerX;
                        y = (posY + 1) - (playerY + 4);
                    }
                    if (isLeft) {
                        x = posX - (playerX - 4);
                        y = (posY + 1) - playerY;
                    }
                    if (isRight) {
                        x = posX - (playerX + 4);
                        y = (posY + 1) - playerY;
                    }
                    distanceD = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    if (min > distanceD) {
                        min = distanceD;
                        savePos = pinky.Location.Y;
                        dir = "D";
                    }
                }
            if (moveItem.lastDir != "R" || isChangeFirst) // 왼쪽 이동
                if (map.groundWL[posY, posX - 1] != 1) {
                    int x = 0;
                    int y = 0;
                    if (isUp) {
                        x = (posX - 1) - playerX;
                        y = posY - (playerY - 4);
                    }
                    distanceL = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    if (min > distanceL) {
                        savePos = blinky.Location.X;
                        dir = "L";
                    }
                }
        }
    }
}
