using System;
using System.Drawing;
using System.Windows.Forms;

//클라이드 - ScatterMode 사이클 추격으로 변경될때, 같이 바꿔줘야 함.
namespace Pacman {
    class Clyde : Enemy {
        int scatterPosX = 0;
        int scatterPosY = 30;

        int playerX = 13;
        int playerY = 23;
        int[,] playerCircleX = new int[8, 2];
        int[,] playerCircleY = new int[8, 2];

        string dir = "L";
        (string lastDir, bool isMoving, int posX, int posY, int moveX, int moveY) moveItem = ("L", false, 15, 3, 0, 0);

        public bool isChangeFirst = false;
        bool isScatterMode = false;

        Map map = new Map();
        Player player;

        public Clyde(Player player) : base() {
            this.player = player;
        }
        //팩맨반경 8칸 밖에 있으면, 팩맨의 현재 위치를 표적으로 삼지만, 8칸 안으로 들어가면, 해산모드가 된다.
        public void PlayerCircle() {
            playerX = player.posX;
            playerY = player.posY;

            for (int i = 1; i <= 8; i++) {
                //맵의 크기에 맞게 조정

                //아래쪽
                playerCircleX[i - 1, 0] = playerX + 8 - i <= 27 ? playerX + 8 - i : 27;
                playerCircleX[i - 1, 1] = playerX - 8 + i >= 0 ? playerX - 8 + i : 0;
                playerCircleY[i - 1, 0] = playerY + i <= 30 ? playerY + i : 30;

                //위쪽
                playerCircleY[i - 1, 1] = playerY - i >= 0 ? playerY - i : 0;
            }
            //가운데
            //플레이어 y를 기준으로 x축 +8 / -8;
        }
        public void PacmanNearCheck() {
            //같은 y축 있는, x축으로 비교를 해야 한다. ( 다시 짜라 )
            //가운데
            if (moveItem.posX <= playerX + 8 && moveItem.posX >= playerX - 8 && moveItem.posY == playerY) {
                isScatterMode = true;
                return;
            }
            for (int i = 0; i < 8; i++) {
                if (moveItem.posX <= playerCircleX[i, 0] && moveItem.posX >= playerCircleX[i, 1] 
                    && (moveItem.posY == playerCircleY[i, 0] || moveItem.posY == playerCircleY[i, 1])) {
                    isScatterMode = true;
                    return;
                }
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
            Image imageClyde = Image.FromFile(Application.StartupPath + @"\images\clydeR " + 1 + ".png");

            g.DrawImage(imageClyde, moveItem.posX * 35 - 10 + moveItem.moveX, moveItem.posY * 35 + 45 + moveItem.moveY);
        }
    }
}
