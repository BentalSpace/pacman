using System;
using System.Windows.Forms;

namespace Pacman {
    class Player {
        int speed = 3;
        string newDir = null;
        string agoDir = null;
        public int posX = 13;
        public int posY = 23;
        int savePosX = -1;
        int savePosY = -1;

        public bool isCurveMove = false;
        bool isLeft = false;
        bool isRight = false;
        bool isUp = false;
        bool isDown = false;
        bool isInput = false;

        Panel self;
        Panel red;

        Map map = new Map();

        public Player(Panel self, Panel red) {
            this.self = self;
            this.red = red;
        }
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
        public void PosCheak() {
            double x1 = self.Location.X - 10;
            double x2 = Math.Ceiling(x1 / 35);
            posX = (int)x2;
            double y1 = self.Location.Y - 70;
            double y2 = Math.Ceiling(y1 / 35);
            posY = (int)y2;
        }
        public void PlayerMove() {
            if (isUp) {
                if (map.ground[posY - 1, posX] == 1) {
                    if (self.Location.Y >= (posY * 35 + 70) - 23)
                        self.Top -= speed;
                    return;
                }
                else
                    self.Top -= speed;
            }
            if (isDown) {
                if (map.ground[posY + 1, posX] == 1) {
                    if (self.Location.Y <= (posY * 35 + 70) - 25)
                        self.Top += speed;
                    return;
                }
                else
                    self.Top += speed;
            }
            if (isLeft) {
                if (posX == 0 && posY == 14) {
                    self.Left -= speed;
                    if (self.Location.X <= -20)
                        self.Left = 955;
                    return;
                }
                if (map.ground[posY, posX - 1] == 1) {
                    if (self.Location.X >= (posX * 35 + 10) - 23)
                        self.Left -= speed;
                    return;
                }
                else
                    self.Left -= speed;
            }
            if (isRight) {
                if (posX == 27 && posY == 14) {
                    self.Left += speed;
                    if (self.Location.X >= 955)
                        self.Left = -20;
                    return;
                }
                if (map.ground[posY, posX + 1] == 1) {
                    if (self.Location.X <= (posX * 35 + 10) - 25)
                        self.Left += speed;
                    return;
                }
                else
                    self.Left += speed;
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
                        if (self.Location.Y >= (posY * 35 + 70) - 23)
                            self.Top -= speed;
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
                        if (self.Location.Y <= (posY * 35 + 70) - 25)
                            self.Top += speed;
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
                        if (self.Location.X >= (posX * 35 + 10) - 23)
                            self.Left -= speed;
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
                        if (self.Location.X <= (posX * 35 + 10) - 25)
                            self.Left += speed;
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
        //public void HitboxMove() {
        //    switch (newDir) {
        //        case "U":
        //            hitbox.Top -= 3;
        //            break;
        //        case "D":
        //            hitbox.Top += 3;
        //            break;
        //        case "L":
        //            hitbox.Left -= 3;
        //            break;
        //        case "R":
        //            hitbox.Left += 3;
        //            break;
        //        }
        //        //isInput = false;
        //        savePoint.Add(hitbox);
        //        hitbox.Location = self.Location;
        //}
        //public void PlayerBlock(Control x) {
        //    if((string)x.Tag == "wall") {
        //        if (hitbox.Bounds.IntersectsWith(x.Bounds)) { }
        //            //멈추는 로직
        //    }
        //}
    }
}
