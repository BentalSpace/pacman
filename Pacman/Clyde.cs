using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pacman {
    class Clyde : Enemy {
        int scatterPosX = 0;
        int scatterPosY = 30;

        int playerX = 13;
        int playerY = 23;
        int[,] playerCircle;

        string dir = "L";
        (string lastDir, bool isMoving, int posX, int posY, int moveX, int moveY) moveItem = ("L", false, 15, 11, 0, 0);

        public bool isChangeFirst = false;

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
                for(int j = 0; j < 4; j++) {
                    playerCircle[i,j] = 
                }
            }
        }
        public override void ChaseCheck() {
            
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
