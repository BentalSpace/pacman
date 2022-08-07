using System;
using System.Drawing;
using System.Windows.Forms;

namespace Pacman {
    class Inky : Enemy {
        Random rand = new Random();
        int scatterPosX = 27;
        int scatterPosY = 30;

        int playerX = 13;
        int playerY = 23;
        int playerAgoX = 0;
        int playerAgoY = 0;
        int targetPosX = 0;
        int targetPosY = 0;

        public string dir = "D";
        (string lastDir, bool isMoving, int posX, int posY, int moveX, int moveY) moveItem = ("L", false, 11, 14, 0, 0);
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
        private bool isUp = false;

        private int delayIndex = 0;

        Map map = new Map();
        Player player;
        Blinky blinky;


        public Inky(Player player, Blinky blinky) : base() {
            this.player = player;
            this.blinky = blinky;
        }

        // 팩맨의 두칸 앞을 기준으로 블링키의 위치를 대칭시킨곳이 목표지점

        public void PlayerMoveCheck() {
            playerAgoX = playerX;
            playerAgoY = playerY;

            playerX = player.posX;
            playerY = player.posY;
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

            int temp;
            if (player.isUp) {
                if (playerX <= blinky.PosX) { // 블링키의 위치가 더 오른쪽에 있다면
                    temp = blinky.PosX - playerX;
                    targetPosX = playerX - temp;
                }
                else if (playerX > blinky.PosX) {
                    temp = playerX - blinky.PosX;
                    targetPosX = playerX + temp;
                }
                if ((playerY - 2) <= blinky.PosY) {
                    temp = blinky.PosY - (playerY - 2);
                    targetPosY = (playerY - 2) - temp;
                }
                else if ((playerY - 2) > blinky.PosY) {
                    temp = (playerY - 2) - blinky.PosY;
                    targetPosY = (playerY - 2) + temp;
                }
            }
            else if (player.isDown) {
                if (playerX <= blinky.PosX) {
                    temp = blinky.PosX - playerX;
                    targetPosX = playerX - temp;
                }
                else if (playerX > blinky.PosX) {
                    temp = playerX - blinky.PosX;
                    targetPosX = playerX + temp;
                }
                if ((playerY + 2) <= blinky.PosY) {
                    temp = blinky.PosY - (playerY + 2);
                    targetPosY = (playerY + 2) - temp;
                }
                else if ((playerY + 2) > blinky.PosY) {
                    temp = (playerY + 2) - blinky.PosY;
                    targetPosY = (playerY + 2) + temp;
                }
            }
            else if (player.isLeft) {
                if ((playerX - 2) <= blinky.PosX) {
                    temp = blinky.PosX - (playerX - 2);
                    targetPosX = (playerX - 2) - temp;
                }
                else if ((playerX - 2) > blinky.PosX) {
                    temp = (playerX - 2) - blinky.PosX;
                    targetPosX = (playerX - 2) + temp;
                }
                if ((playerY) <= blinky.PosY) {
                    temp = blinky.PosY - playerY;
                    targetPosY = playerY - temp;
                }
                else if (playerY > blinky.PosY) {
                    temp = playerY - blinky.PosY;
                    targetPosY = playerY + temp;
                }
            }
            else if (player.isRight) {
                if ((playerX + 2) <= blinky.PosX) {
                    temp = blinky.PosX - (playerX + 2);
                    targetPosX = (playerX + 2) - temp;
                }
                else if ((playerX + 2) > blinky.PosX) {
                    temp = (playerX + 2) - blinky.PosX;
                    targetPosX = (playerX + 2) + temp;
                }
                if ((playerY) <= blinky.PosY) {
                    temp = blinky.PosY - playerY;
                    targetPosY = playerY - temp;
                }
                else if (playerY > blinky.PosY) {
                    temp = playerY - blinky.PosY;
                    targetPosY = playerY + temp;
                }
            }
            // 위 타켓 포지션 체크
            if (moveItem.isMoving) {
                moveItem = base.EnemyMove(dir, moveItem.posX, moveItem.posY, moveItem.moveX, moveItem.moveY);
                return;
            }

            double min = Double.MaxValue;

            if (moveItem.lastDir != "D" || isChangeFirst) // 위 이동
                if (map.groundWL[moveItem.posY - 1, moveItem.posX] != 1) {
                    int x = moveItem.posX - targetPosX;
                    int y = (moveItem.posY - 1) - targetPosY;
                    distanceU = (x * x) + (y * y);
                    min = distanceU;
                    dir = "U";
                }
            if (moveItem.lastDir != "L" || isChangeFirst) // 오른쪽 이동
                if (map.groundWL[moveItem.posY, moveItem.posX + 1] != 1) {
                    int x = (moveItem.posX + 1) - targetPosX;
                    int y = moveItem.posY - targetPosY;
                    distanceR = (x * x) + (y * y);
                    if (min > distanceR) {
                        min = distanceR;
                        dir = "R";
                    }
                }
            if (moveItem.lastDir != "U" || isChangeFirst) // 아래 이동
                if (map.groundWL[moveItem.posY + 1, moveItem.posX] != 1) {
                    int x = moveItem.posX - targetPosX;
                    int y = (moveItem.posY + 1) - targetPosY;
                    distanceD = (x * x) + (y * y);
                    if (min > distanceD) {
                        min = distanceD;
                        dir = "D";
                    }
                }
            if (moveItem.lastDir != "R" || isChangeFirst) // 왼쪽 이동
                if (map.groundWL[moveItem.posY, moveItem.posX - 1] != 1) {
                    int x = (moveItem.posX - 1) - targetPosX;
                    int y = moveItem.posY - targetPosY;
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
            
            if(PosY <= 11) {
                isHome = false;
                return;
            }
            delayIndex++;
            if (moveItem.isMoving) {
                moveItem = base.EnemyMove(dir, moveItem.posX, moveItem.posY, moveItem.moveX, moveItem.moveY);
                return;
            }
            if (delayIndex < 300) {
                if (PosY == 13 && isUp) {
                    dir = "D";
                    isUp = false;
                }
                else if (PosY == 15 && !isUp) {
                    dir = "U";
                    isUp = true;
                }
            }
            else if (delayIndex >= 300) {
                // 집 탈출 시작
                if (PosY != 14 && PosX == 11) {
                    if (PosY == 13)
                        dir = "D";
                    else if (PosY == 15)
                        dir = "U";
                }
                // y좌표 14로 보낸다
                // x좌표를 11 -> 12 -> 13 이동 후 y좌표 14 -> 13 -> 12 -> 11 이후 homeMove 종료
                else {
                    if (PosY == 14 && PosX == 11)
                        dir = "R";
                    if (PosY == 14 && PosX == 13)
                        dir = "U";
                }
            }

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
            if (isHome) {
                homeMove();
                return;
            }
            if (isEaten) {
                goHome();
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
            Image imageInky;
            if (isEaten) {
                switch (dir) {
                    case "U":
                        imageInky = Image.FromFile(Application.StartupPath + @"\images\goHomeU.png");
                        break;
                    case "D":
                        imageInky = Image.FromFile(Application.StartupPath + @"\images\goHomeD.png");
                        break;
                    case "L":
                        imageInky = Image.FromFile(Application.StartupPath + @"\images\goHomeL.png");
                        break;
                    default:
                        imageInky = Image.FromFile(Application.StartupPath + @"\images\goHomeR.png");
                        break;
                }
            }
            else if (player.isEatMode && !oneCatch) {
                if (player.eatModeTimer >= 350)
                    imageInky = Image.FromFile(Application.StartupPath + @"\images\FrightenedEnd " + i + ".png");
                else
                    imageInky = Image.FromFile(Application.StartupPath + @"\images\Frightened " + i + ".png");
            }

            else
                switch (dir) {
                    case "U":
                        imageInky = Image.FromFile(Application.StartupPath + @"\images\inkyU " + i + ".png");
                        break;
                    case "D":
                        imageInky = Image.FromFile(Application.StartupPath + @"\images\inkyD " + i + ".png");
                        break;
                    case "L":
                        imageInky = Image.FromFile(Application.StartupPath + @"\images\inkyL " + i + ".png");
                        break;
                    default:
                        imageInky = Image.FromFile(Application.StartupPath + @"\images\inkyR " + i + ".png");
                        break;
                }

            if (count % 5 == 0)
                i = i + 1 == 3 ? 1 : 2;

            g.DrawImage(imageInky, moveItem.posX * 35 - 10 + moveItem.moveX, moveItem.posY * 35 + 45 + moveItem.moveY);
        }
    }
}
