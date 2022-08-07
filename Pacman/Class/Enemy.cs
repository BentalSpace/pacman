using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pacman {
    abstract class Enemy {
        int speed = 3;
        public int savePos = 0;
        protected double distanceU = 0;
        protected double distanceD = 0;
        protected double distanceL = 0;
        protected double distanceR = 0;

        public bool posChange = false;
        public bool isChaseScatter = true; // true일땐 추격 / false일땐 해산
        public bool isFrightenedMode = false;

        public Enemy() {
        }
        protected (string, bool, int, int, int, int) EnemyMove(string dir, int posX, int posY, int moveX, int moveY) {
            //string lastDir = null;
            (string lastDir, bool isMoving, int posX, int posY, int moveX, int moveY) outItem = (null, true, posX, posY, moveX, moveY);
            switch (dir) {
                case "U":
                    //if (savePos - 30 >= enemy.Location.Y) // 저장해둔 포지션에서 -30 만큼 이동 했다면
                    if (outItem.moveY <= -35) {
                        outItem.isMoving = false;
                        outItem.moveY += 35;
                        outItem.posY--;
                    }
                    else {
                        //enemy.Top -= speed;
                        outItem.moveY -= speed;
                    }
                    outItem.lastDir = "U";
                    break;
                case "D":
                    if (outItem.moveY >= 35) {
                        outItem.isMoving = false;
                        outItem.moveY -= 35;
                        outItem.posY++;
                    }
                    else {
                        outItem.moveY += speed;
                    }
                    outItem.lastDir = "D";
                    break;
                case "L":
                    if (outItem.moveX <= -35) {
                        outItem.isMoving = false;
                        outItem.moveX += 35;
                        outItem.posX--;
                    }
                    else {
                        outItem.moveX -= speed;
                    }
                    outItem.lastDir = "L";
                    break;
                case "R":
                    if (outItem.moveX >= 35) {
                        outItem.isMoving = false;
                        outItem.moveX -= 35;
                        outItem.posX++;
                    }
                    else {
                        outItem.moveX += speed;
                    }
                    outItem.lastDir = "R";
                    break;
            }
            return outItem;
        }
        protected bool PosMove(string dir, Panel enemy, int posX, int posY) {
            bool parentIsMoving = true;
            switch (dir) {
                case "U":
                    if (enemy.Location.Y >= (posY * 35 + 70) - 23)
                        enemy.Top -= speed;
                    else
                        parentIsMoving = false;
                    break;
                case "D":
                    if (enemy.Location.Y <= (posY * 35 + 70) - 25)
                        enemy.Top += speed;
                    else
                        parentIsMoving = false;
                    break;
                case "L":
                    if (enemy.Location.X >= (posX * 35 + 10) - 23)
                        enemy.Left -= speed;
                    else
                        parentIsMoving = false;
                    break;
                case "R":
                    if (enemy.Location.X <= (posX * 35 + 10) - 25) // 현재 포지션은 -25의 위치
                        enemy.Left += speed;
                    else
                        parentIsMoving = false;
                    break;
            }
            return parentIsMoving;
        }

        public abstract void ChaseCheck();
        public abstract void ScatterCheck();
        public abstract void homeMove();
        public abstract void goHome();
        public void Teleport(string dir, int posX, int posY) {
            if (dir == "R") {
                if (posX == 27 && posY == 14) {
                    posX = 0;
                }
            }
        }
        public abstract void enemyDraw(Graphics g);

        //protected int PosCheckX() {
        //    int posX = 0;
        //    double x1 = enemy.Location.X - 10;
        //    double x2 = Math.Ceiling(x1 / 35);
        //    if (redPosX != (int)x2)
        //        posChange = true;
        //    posX = (int)x2;

        //    return posX;
        //}
        //protected int PosCheckY() {
        //    int posY = 0;
        //    double y1 = enemy.Location.Y - 70;
        //    double y2 = Math.Ceiling(y1 / 35);
        //    if (redPosY != (int)y2)
        //        posChange = true;
        //    posY = (int)y2;

        //    return posY;
        //}
    }
}
