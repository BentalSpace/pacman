using System;
using System.Windows.Forms;

namespace Pacman {
    class Inky : Enemy {
        int scatterPosX = 27;
        int scatterPosY = 30;

        int posX = 13;
        int posY = 11;
        new int savePos = 0;
        string dir = "L";
        (string lastDir, bool isPosMove) moveItem = ("L", false);

        bool isMoving = false;
        public bool isChangeFirst = false;

        Panel self;
        Panel inky;

        Map map = new Map();
        public Inky(Panel self, Panel inky) : base(self, inky) {
            this.self = self;
            this.inky = inky;
        }
        public void ScatterCheck() {
            posX = base.PosCheckX();
            posY = base.PosCheckY();

            if (isMoving) {
                moveItem = base.EnemyMove(dir, savePos, inky, posX, posY);
                if (moveItem.isPosMove) {
                    isMoving = base.PosMove(dir, inky, posX, posY);
                }
                return;
            }

            double min = Double.MaxValue;

            if (moveItem.lastDir != "D" || isChangeFirst)
                if (map.groundWL[posY - 1, posX] != 1) {
                    int x = posX - scatterPosX;
                    int y = (posY - 1) - scatterPosY;
                    distanceU = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    min = distanceU;
                    savePos = inky.Location.Y;
                    dir = "U";
                }
            if (moveItem.lastDir != "U" || isChangeFirst)
                if (map.groundWL[posY + 1, posX] != 1) {
                    int x = posX - scatterPosX;
                    int y = (posY + 1) - scatterPosY;
                    distanceD = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    if (min > distanceD) {
                        min = distanceD;
                        savePos = inky.Location.Y;
                        dir = "D";
                    }
                }
            if (moveItem.lastDir != "R" || isChangeFirst)
                if (map.groundWL[posY, posX - 1] != 1) {
                    int x = (posX - 1) - scatterPosX;
                    int y = posY - scatterPosY;
                    distanceL = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    if (min > distanceL) {
                        min = distanceL;
                        savePos = inky.Location.X;
                        dir = "L";
                    }
                }
            if (moveItem.lastDir != "L" || isChangeFirst)
                if (map.groundWL[posY, posX + 1] != 1) {
                    int x = (posX + 1) - scatterPosX;
                    int y = posY - scatterPosY;
                    distanceR = Math.Sqrt(Math.Pow(x, 2) + Math.Pow(y, 2));
                    if (min > distanceR) {
                        savePos = inky.Location.X;
                        dir = "R";
                    }
                }
            isMoving = true;
            isChangeFirst = false;
            moveItem = base.EnemyMove(dir, savePos, inky, posX, posY);
        }
    }
}
