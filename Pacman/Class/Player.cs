using System;
using System.Windows.Forms;
using System.Drawing;

namespace Pacman {
    class Player {
        float speed = 3.5f;
        string newDir = null;
        string agoDir = null;
        public int posX {
            get; set;
        } = 13;
        public int posY {
            get; set;
        } = 23;
        int savePosX = -1;
        int savePosY = -1;
        float moveX = 0; // 34까지 올라가야 함.
        float moveY = 0; // 34까지 올라가야 함.

        bool isCurveMove = false;
        public bool isLeft { get; private set; } = false;
        public bool isRight { get; private set; } = false;
        public bool isUp { get; private set; } = false;
        public bool isDown { get; private set; } = false;
        public bool isEatMode { get; set; } = false;
        public int eatModeTimer { get; set; }
        public bool isDie = false;
        bool isInput = false;

        //Label lblPlayerPos;

        Map map = new Map();

        //public Player(Label lblPlayerPos) {
        //    this.lblPlayerPos = lblPlayerPos;
        //}
        public Player() {

        }
        public void DirClear() {
            isLeft = false;
            isRight = false;
            isUp = false;
            isDown = false;
        }
        public void PlayerMoveDirSet(KeyEventArgs e) {
            if (isUp)
                agoDir = "U";
            if (isDown)
                agoDir = "D";
            if (isLeft)
                agoDir = "L";
            if (isRight)
                agoDir = "R";
            DirClear();
            switch (e.KeyCode) {
                case Keys.Up:
                    isUp = true;
                    newDir = "U";
                    break;
                case Keys.Down:
                    isDown = true;
                    newDir = "D";
                    break;
                case Keys.Left:
                    isLeft = true;
                    newDir = "L";
                    break;
                case Keys.Right:
                    isRight = true;
                    newDir = "R";
                    break;
                default:
                    break;
            }
            isInput = true;
        }
        public void PlayerMove() {
            if (isCurveMove)
                return;

            if (isUp) {
                if (map.ground[posY - 1, posX] == 1) {
                    return;
                }
                moveY -= speed;
                if(moveY <= -35) {
                    moveY = 0;
                    posY--;
                }
            }
            if (isDown) {
                if (map.ground[posY + 1, posX] == 1) {
                    return;
                }
                moveY += speed;
                if(moveY >= 35) {
                    moveY = 0;
                    posY++;
                }
            }
            if (isLeft) {
                if (posX == 0 && posY == 14) {
                    posX = 27;
                }
                if (map.ground[posY, posX - 1] == 1) {
                    return;
                }
                moveX -= speed;
                if(moveX <= -35) {
                    moveX = 0;
                    posX--;
                }
            }
            if (isRight) {
                if (posX == 27 && posY == 14) {
                    posX = 0;
                }
                if (map.ground[posY, posX + 1] == 1) {
                    return;
                }
                moveX += speed;
                if(moveX >= 35) {
                    moveX = 0;
                    posX++;
                }
            }
        }
        public void CurveCheck() { // 커브를 찾는 함수
            if (!isInput)
                return;
            int i;
            switch (agoDir) {
                case "U":
                    if (isLeft) {
                        for (i = 0; i <= posY; i++) {
                            if (map.ground[posY - i, posX - 1] != 1) {
                                savePosY = posY - i;
                                break;
                            }
                        }
                        isInput = false;
                        DirClear();
                        isUp = true;
                    }
                    if (isRight) {
                        for (i = 0; i <= posY; i++) {
                            if (map.ground[posY - i, posX + 1] != 1) {
                                savePosY = posY - i;
                                break;
                            }
                        }
                        isInput = false;
                        DirClear();
                        isUp = true;
                    }
                    break;

                case "D":
                    if (isLeft) {
                        for (i = 0; posY + i <= 30; i++) {
                            if (map.ground[posY + i, posX - 1] != 1) {
                                savePosY = posY + i;
                                break;
                            }
                        }
                        isInput = false;
                        DirClear();
                        isDown = true;
                    }
                    if (isRight) {
                        for (i = 0; posY + i <= 30; i++) {
                            if (map.ground[posY + i, posX + 1] != 1) {
                                savePosY = posY + i;
                                break;
                            }
                        }
                        isInput = false;
                        DirClear();
                        isDown = true;
                    }
                    break;

                case "L":
                    if (isUp) {
                        for (i = 0; i <= posX; i++) {
                            if (map.ground[posY - 1, posX - i] != 1) {
                                savePosX = posX - i;
                                break;
                            }
                        }
                        isInput = false;
                        DirClear();
                        isLeft = true;
                    }
                    if (isDown) {
                        for (i = 0; i <= posX; i++) {
                            if (map.ground[posY + 1, posX - i] != 1) {
                                savePosX = posX - i;
                                break;
                            }
                        }
                        isInput = false;
                        DirClear();
                        isLeft = true;
                    }
                    break;

                case "R":
                    if (isUp) {
                        for (i = 0; i + posX <= 27; i++) {
                            if (map.ground[posY - 1, posX + i] != 1) {
                                savePosX = posX + i;
                                break;
                            }
                        }
                        isInput = false;
                        DirClear();
                        isRight = true;
                    }
                    if (isDown) {
                        for (i = 0; i + posX <= 27; i++) {
                            if (map.ground[posY + 1, posX + i] != 1) {
                                savePosX = posX + i;
                                break;
                            }
                        }
                        isInput = false;
                        DirClear();
                        isRight = true;
                    }
                    break;
            }
        }
        public void CurveMove() { // 찾은 커브로 움직이는 함수
            if (posX == savePosX || posY == savePosY) {
                isCurveMove = true;
                switch (agoDir) {
                    case "U":
                        //if (self.Location.Y >= (posY * 35 + 70) - 23)
                        moveY -= speed;
                        if (moveY <= -35) {
                            moveY = 0;
                            posY--;
                        }
                        else {
                            isCurveMove = false;
                            DirClear();
                            savePosY = -1;
                            switch (newDir) {
                                case "L":
                                    isLeft = true;
                                    agoDir = null;
                                    break;
                                case "R":
                                    isRight = true;
                                    agoDir = null;
                                    break;
                            }
                        }
                        break;

                    case "D":
                        //if (self.Location.Y <= (posY * 35 + 70) - 25)
                        moveY += speed;
                        if (moveY >= 35) {
                            moveY = 0;
                            posY++;
                        }
                        else {
                            isCurveMove = false;
                            DirClear();
                            savePosY = -1;
                            switch (newDir) {
                                case "L":
                                    isLeft = true;
                                    agoDir = null;
                                    break;
                                case "R":
                                    isRight = true;
                                    agoDir = null;
                                    break;
                            }
                        }
                        break;

                    case "L":
                        //if (self.Location.X >= (posX * 35 + 10) - 23)
                        moveX -= speed;
                        if (moveX <= -35) {
                            moveX = 0;
                            posX--;
                        }
                        else {
                            isCurveMove = false;
                            DirClear();
                            savePosX = -1;
                            switch (newDir) {
                                case "U":
                                    isUp = true;
                                    agoDir = null;
                                    break;
                                case "D":
                                    isDown = true;
                                    agoDir = null;
                                    break;
                            }
                        }
                        break;

                    case "R":
                        //if (self.Location.X <= (posX * 35 + 10) - 25)
                        moveX += speed;
                        if (moveX >= 35) {
                            moveX = 0;
                            posX++;
                        }
                        else {
                            isCurveMove = false;
                            DirClear();
                            savePosX = -1;
                            switch (newDir) {
                                case "U":
                                    isUp = true;
                                    agoDir = null;
                                    break;
                                case "D":
                                    isDown = true;
                                    agoDir = null;
                                    break;
                            }
                        }
                        break;
                }
            }
            else
                return;
        }
        int i = 1;
        int count = 0;
        bool animplus = true;
        public void playerDraw(Graphics g) {
            count++;
            Image imagePlayer;
            if (i == 1)
                imagePlayer = Image.FromFile(Application.StartupPath + @"\images\pacman 1.png");
            else {
                if (isRight)
                    imagePlayer = Image.FromFile(Application.StartupPath + @"\images\pacmanR " + i + ".png");
                else if (isLeft)
                    imagePlayer = Image.FromFile(Application.StartupPath + @"\images\pacmanL " + i + ".png");
                else if (isUp)
                    imagePlayer = Image.FromFile(Application.StartupPath + @"\images\pacmanU " + i + ".png");
                else
                    imagePlayer = Image.FromFile(Application.StartupPath + @"\images\pacmanD " + i + ".png");
            }

            if (count % 5 == 0) {
                if (animplus) {
                    i++;
                    if (i >= 3)
                        animplus = false;
                }
                else {
                    i--;
                    if (i <= 1)
                        animplus = true;
                }
            }

            g.DrawImage(imagePlayer, (posX * 35 - 10 + moveX), (posY * 35 + 45 + moveY)); // 35씩 더할때까지, 같은방향으로 움직이면 됨.
        }
    }
}
