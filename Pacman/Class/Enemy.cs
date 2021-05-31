using System;
using System.Windows.Forms;
using System.Drawing;

namespace Pacman {
    abstract class Enemy {
        int speed = 3;
        int redPosX = 13;
        int redPosY = 11;
        public int savePos = 0;
        protected double distanceU = 0;
        protected double distanceD = 0;
        protected double distanceL = 0;
        protected double distanceR = 0;

        public bool posChange = false;
        bool isMoving = false;
        public bool isChaseScatter = true; // true일땐 추격 / false일땐 해산

        Panel self;
        Panel enemy;

        GameManager manager = new GameManager();
        Map map = new Map();

        public Enemy(Panel self, Panel enemy) {
            this.self = self;
            this.enemy = enemy;
        }
        protected (string, bool) EnemyMove(string dir, int savePos, Panel enemy, int posX, int posY) {
            //string lastDir = null;
            (string lastDir, bool isPosMove) outItem = (null, false);
            switch (dir) {
                case "U":
                    if (savePos - 30 >= enemy.Location.Y)
                        outItem.isPosMove = true;
                    else {
                        enemy.Top -= speed;
                    }
                    outItem.lastDir = "U";
                    break;
                case "D":
                    if (savePos + 30 <= enemy.Location.Y)
                        outItem.isPosMove = true;
                    else {
                        enemy.Top += speed;
                    }
                    outItem.lastDir = "D";
                    break;
                case "L":
                    if (savePos - 30 >= enemy.Location.X)
                        outItem.isPosMove = true;
                    else {
                        enemy.Left -= speed;
                    }
                    outItem.lastDir = "L";
                    break;
                case "R":
                    if (savePos + 30 <= enemy.Location.X)
                        outItem.isPosMove = true;
                    else {
                        enemy.Left += speed;
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
                    if (enemy.Location.X <= (posX * 35 + 10) - 25)
                        enemy.Left += speed;
                    else
                        parentIsMoving = false;
                    break;
            }
            return parentIsMoving;
        }

        public abstract void enemyDraw(Graphics g);

        protected int PosCheckX() {
            int posX = 0;
            double x1 = enemy.Location.X - 10;
            double x2 = Math.Ceiling(x1 / 35);
            if (redPosX != (int)x2)
                posChange = true;
            posX = (int)x2;

            return posX;
        }
        protected int PosCheckY() {
            int posY = 0;
            double y1 = enemy.Location.Y - 70;
            double y2 = Math.Ceiling(y1 / 35);
            if (redPosY != (int)y2)
                posChange = true;
            posY = (int)y2;

            return posY;
        }
    }
}
