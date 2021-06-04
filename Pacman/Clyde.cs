using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pacman {
    class Clyde : Enemy {
        int scatterPosX = 0;
        int scatterPosY = 30;

        int playerX = 13;
        int playerY = 23;
        int[,] playerCircleX = new int[8, 4];
        int[,] playerCircleY = new int[8, 4];

        string dir = "L";
        (string lastDir, bool isMoving, int posX, int posY, int moveX, int moveY) moveItem = ("L", false, 15, 3, 0, 0);

        public bool isChangeFirst = false;
        bool isScatterMode = false;

        Map map = new Map();

        Label lblPlayerPos;
        public Clyde(Label lblPlayerPos) : base() {
            this.lblPlayerPos = lblPlayerPos;
        }
        //팩맨반경 8칸 밖에 있으면, 팩맨의 현재 위치를 표적으로 삼지만, 8칸 안으로 들어가면, 해산모드가 된다.
        public void PlayerCircle() {
            string[] playerPos = lblPlayerPos.Text.Split(',');
            playerX = Int32.Parse(playerPos[0]);
            playerY = Int32.Parse(playerPos[1]);

            for(int i = 0; i < 8; i++) {
                //우 -> 하
                playerCircleX[i, 0] = playerX + 8 - i <= 27 ? playerX + 8 - i : 27;
                playerCircleY[i, 0] = playerY + i <= 30 ? playerY + i : 30;
                //하 -> 좌
                playerCircleX[i, 1] = playerX - i >= 0 ? playerX - i : 0;
                playerCircleY[i, 1] = playerY + 8 - i <= 30 ? playerY + 8 - i : 30;
                //좌 -> 상
                playerCircleX[i, 2] = playerX - 8 + i >= 0 ? playerX - 8 + i : 0;
                playerCircleY[i, 2] = playerY - i >= 0 ? playerY - i : 0;
                //상 -> 우
                playerCircleX[i, 3] = playerX + i <= 27 ? playerX + i : 27;
                playerCircleY[i, 3] = playerY - 8 + i >= 0 ? playerY - 8 + i : 0;
            }
        }
        int k = 0;
        public void PacmanNearCheck() {
            //같은 y축 있는, x축으로 비교를 해야 한다. ( 다시 짜라 )
            for(int i = 0; i < 8; i++) {
                if(moveItem.posX <= playerCircleX[i,0] && moveItem.posY >= playerCircleY[i, 0]) {
                    if (k == 0)
                        MessageBox.Show(k++ + "/" + i + "Test");
                    isScatterMode = true;
                    return;
                }
                //if(moveItem.posX <= playerCircleX[i,1] && moveItem.posY >= playerCircleY[i, 1]) {
                //    isScatterMode = true;
                //    return;
                //}
                //if(moveItem.posX <= playerCircleX[i,2] && moveItem.posY >= playerCircleY[i, 2]) {
                //    isScatterMode = true;
                //    return;
                //}
                //if(moveItem.posX >= playerCircleX[i,3] && moveItem.posY <= playerCircleY[i, 3]) {
                //    isScatterMode = true;
                //    return;
                //}
            }
        }
        public override void ChaseCheck() {
            if (isScatterMode) {
                ScatterCheck();
                return;
            }
            if (moveItem.isMoving) {
                moveItem = base.EnemyMove(dir, moveItem.posX, moveItem.posY, moveItem.moveX, moveItem.moveY);
                return;
            }

            double min = Double.MaxValue;
            //lblPlayerPos.Text = "Test";

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
            Image imageClyde = Image.FromFile("G:\\Git\\pacman\\images\\clydeR " + 1 + ".png");

            g.DrawImage(imageClyde, moveItem.posX * 35 - 10 + moveItem.moveX, moveItem.posY * 35 + 45 + moveItem.moveY);
        }
    }
}
