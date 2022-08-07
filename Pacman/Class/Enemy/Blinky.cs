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

        public string dir = "L";
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
        public bool isHome = false;
        public bool isEaten { get; set; } = false;
        public bool oneCatch = false;

        Map map = new Map();
        Player player;

        public Blinky(Player player)
            : base() {
            this.player = player;
        }
        public override void ChaseCheck() {
            
            if (isEaten) {
                goHome();
                return;
            }
            if (isHome) {
                homeMove();
                return;
            }
            if (player.isEatMode && !oneCatch) {
                FrightenedMode();
                return;
            }
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
            if (isEaten) {
                goHome();
                return;
            }
            if (isHome) {
                homeMove();
                return;
            }
            if (player.isEatMode && !oneCatch) {
                FrightenedMode();
                return;
            }

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
        public override void homeMove() {
            if (PosY <= 11) {
                isHome = false;
                return;
            }
            
            if (moveItem.isMoving) {
                moveItem = base.EnemyMove(dir, moveItem.posX, moveItem.posY, moveItem.moveX, moveItem.moveY);
                return;
            }

            dir = "U";

            moveItem.isMoving = true;
            isChangeFirst = false;
            moveItem = base.EnemyMove(dir, moveItem.posX, moveItem.posY, moveItem.moveX, moveItem.moveY);
        }
        public override void goHome() {
            oneCatch = true;
            if (moveItem.isMoving) {
                moveItem = base.EnemyMove(dir, moveItem.posX, moveItem.posY, moveItem.moveX, moveItem.moveY);
                return;
            }

            if (PosX == 13 && (PosY == 11 || PosY == 12 || PosY == 13)) {
                dir = "D";
                moveItem.isMoving = true;
                isChangeFirst = false;
                moveItem = base.EnemyMove(dir, moveItem.posX, moveItem.posY, moveItem.moveX, moveItem.moveY);
                return;
            }
            if(PosX == 13 && PosY == 14) {
                isEaten = false;
                isHome = true;
                homeMove();
                return;
            }

            double min = Double.MaxValue;

            if (moveItem.lastDir != "D" || isChangeFirst) // 위 이동
                if (map.groundWL[moveItem.posY - 1, moveItem.posX] != 1) {
                    int x = moveItem.posX - 13;
                    int y = (moveItem.posY - 1) - 11;
                    distanceU = (x * x) + (y * y);
                    min = distanceU;
                    dir = "U";
                }
            if (moveItem.lastDir != "L" || isChangeFirst) // 오른쪽 이동
                if (map.groundWL[moveItem.posY, moveItem.posX + 1] != 1) {
                    int x = (moveItem.posX + 1) - 13;
                    int y = moveItem.posY - 11;
                    distanceR = (x * x) + (y * y);
                    if (min > distanceR) {
                        min = distanceR;
                        dir = "R";
                    }
                }
            if (moveItem.lastDir != "U" || isChangeFirst) // 아래 이동
                if (map.groundWL[moveItem.posY + 1, moveItem.posX] != 1) {
                    int x = moveItem.posX - 13;
                    int y = (moveItem.posY + 1) - 11;
                    distanceD = (x * x) + (y * y);
                    if (min > distanceD) {
                        min = distanceD;
                        dir = "D";
                    }
                }
            if (moveItem.lastDir != "R" || isChangeFirst) // 왼쪽 이동
                if (map.groundWL[moveItem.posY, moveItem.posX - 1] != 1) {
                    int x = (moveItem.posX - 1) - 13;
                    int y = moveItem.posY - 11;
                    distanceL = (x * x) + (y * y);
                    if (min > distanceL) {
                        dir = "L";
                    }
                }
            
            moveItem.isMoving = true;
            isChangeFirst = false;
            moveItem = base.EnemyMove(dir, moveItem.posX, moveItem.posY, moveItem.moveX, moveItem.moveY);
        }
        public void FrightenedMode() {
            if (isEaten) {
                goHome();
                return;
            }
            if (isHome) {
                homeMove();
                return;
            }
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

        int i = 1;
        int count = 0;
        public override void enemyDraw(Graphics g) {
            count++;
            Image imageBlinky;
            if (isEaten) {
                switch (dir) {
                    case "U":
                        imageBlinky = Image.FromFile(Application.StartupPath + @"\images\goHomeU.png");
                        break;
                    case "D":
                        imageBlinky = Image.FromFile(Application.StartupPath + @"\images\goHomeD.png");
                        break;
                    case "L":
                        imageBlinky = Image.FromFile(Application.StartupPath + @"\images\goHomeL.png");
                        break;
                    default:
                        imageBlinky = Image.FromFile(Application.StartupPath + @"\images\goHomeR.png");
                        break;
                }
            }
            else if (player.isEatMode && !oneCatch) {
                if (player.eatModeTimer >= 350)
                    imageBlinky = Image.FromFile(Application.StartupPath + @"\images\FrightenedEnd " + i + ".png");
                else
                    imageBlinky = Image.FromFile(Application.StartupPath + @"\images\Frightened " + i + ".png");
            }
            else
                switch (dir) {
                    case "U":
                        imageBlinky = Image.FromFile(Application.StartupPath + @"\images\blinkyU " + i + ".png");
                        break;
                    case "D":
                        imageBlinky = Image.FromFile(Application.StartupPath + @"\images\blinkyD " + i + ".png");
                        break;
                    case "L":
                        imageBlinky = Image.FromFile(Application.StartupPath + @"\images\blinkyL " + i + ".png");
                        break;
                    default:
                        imageBlinky = Image.FromFile(Application.StartupPath + @"\images\blinkyR " + i + ".png");
                        break;
                }


            if (count % 5 == 0)
                i = i + 1 == 3 ? 1 : 2;
            g.DrawImage(imageBlinky, moveItem.posX * 35 - 10 + moveItem.moveX, moveItem.posY * 35 + 45 + moveItem.moveY);
        }
    }
}
