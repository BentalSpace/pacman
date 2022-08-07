using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pacman {
    class Pinky : Enemy {
        Random rand = new Random();
        int scatterPosX = 2;
        int scatterPosY = 2;

        int playerAgoX = 0;
        int playerAgoY = 0;
        int playerX = 13;
        int playerY = 23;

        public string dir = "L";
        (string lastDir, bool isMoving, int posX, int posY, int moveX, int moveY) moveItem = ("L", false, 13, 14, 0, 0);
        public int PosX {
            get { return moveItem.posX; }
            set { moveItem.posX = value; }
        }
        public int PosY {
            get { return moveItem.posY; }
            set { moveItem.posY = value; }
        }

        public bool isChangeFirst = false;
        public bool isEaten = false;
        public bool isHome = true;
        public bool oneCatch = false;
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
            if (isHome) {
                homeMove();
                return;
            }
            if (isEaten) {
                goHome();
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
            if (isHome) {
                homeMove();
                return;
            }
            if (isEaten) {
                goHome();
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
        public override void homeMove() {
            // y = 13 ~ 15
            // x = 13 ~ 14
            // 밖으로 나가려면 x = 13, y = 11 의 위치로 나가게끔 해준다.
            // 핑키는 기본적으로 집에 붙어있지 않으므로, dir = up만 있으면 되고, 바로 밖으로 빠져 나갈 수 있게끔 해주면 된다.
            // 움직여야 함.
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
            if (PosX == 13 && PosY == 14) {
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
            Image imagePinky;
            if (isEaten) {
                switch (dir) {
                    case "U":
                        imagePinky = Image.FromFile(Application.StartupPath + @"\images\goHomeU.png");
                        break;
                    case "D":
                        imagePinky = Image.FromFile(Application.StartupPath + @"\images\goHomeD.png");
                        break;
                    case "L":
                        imagePinky = Image.FromFile(Application.StartupPath + @"\images\goHomeL.png");
                        break;
                    default:
                        imagePinky = Image.FromFile(Application.StartupPath + @"\images\goHomeR.png");
                        break;
                }
            }
            else if (player.isEatMode && !oneCatch) {
                if (player.eatModeTimer >= 350)
                    imagePinky = Image.FromFile(Application.StartupPath + @"\images\FrightenedEnd " + i + ".png");
                else
                    imagePinky = Image.FromFile(Application.StartupPath + @"\images\Frightened " + i + ".png");
            }
            else
                switch (dir) {
                    case "U":
                        imagePinky = Image.FromFile(Application.StartupPath + @"\images\pinkyU " + i + ".png");
                        break;
                    case "D":
                        imagePinky = Image.FromFile(Application.StartupPath + @"\images\pinkyD " + i + ".png");
                        break;
                    case "L":
                        imagePinky = Image.FromFile(Application.StartupPath + @"\images\pinkyL " + i + ".png");
                        break;
                    default:
                        imagePinky = Image.FromFile(Application.StartupPath + @"\images\pinkyR " + i + ".png");
                        break;
                }

            if (count % 5 == 0)
                i = i + 1 == 3 ? 1 : 2;

            g.DrawImage(imagePinky, moveItem.posX * 35 - 10 + moveItem.moveX, moveItem.posY * 35 + 45 + moveItem.moveY);
        }
    }
}
