using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace Pacman {
    class Player {
        public int speed;
        public string newDir = null;
        string agoDir = null;
        public int posX = 13;
        public int posY = 0;
        int savePos = 0;

        public bool isLeft = false;
        public bool isRight = false;
        public bool isUp = false;
        public bool isDown =false;
        public bool gameStart = false;
        public bool isCurveMove = false;
        bool isInput = false;

        Panel self;
        //Panel hitbox;

        Map map = new Map();


        public Player(Panel self) {
            this.self = self;
           // self.Location = hitbox.Location;
        }
        public Player() {

        }
        private void DirClear() {
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
            speed = 3;
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
            gameStart = true;
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
            if(isUp){
                if (map.ground[posY-1, posX] == 1) {
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
            if(isLeft){
                if (map.ground[posY, posX - 1] == 1) {
                    if (self.Location.X >= (posX * 35 + 10) - 23) {
                        self.Left -= speed;
                    }
                    else
                        return;
                }
                else
                    self.Left -= speed;
            }
            if (isRight) {
                if (map.ground[posY, posX + 1] == 1) {
                    if (self.Location.X <= (posX * 35 + 10) - 25) {
                        self.Left += speed;
                    }
                        return;
                }
                else
                    self.Left += speed;
            }
        }
        int k = 0;
        public void CurveCheck() {

            int i;
            switch (agoDir) {
                case "U":
                    if (isDown) {

                    }
                    break;
                case "L":
                    if (isUp) {
                        if (isInput) {
                            //왼쪽으로 계속 가다가, 위쪽길이 보이면 그쪽으로 올라가야 함.
                            //내 현재 포지션을 기준으로 왼쪽으로 계속 보내면서 위쪽에 길이 있는지 확인.
                            //while (true) {
                            //    if (map.ground[posY - 1, posX - i] != 1) { //현재 포지션 기준으로 왼쪽으로 계속 움직이며 위쪽을 확인해줌.
                            //        savePos = posX - i;
                            //        break;
                            //    }
                            //    if (posX == i) {
                            //        i = -1;
                            //        break;
                            //    }
                            //    i++;
                            //}
                            for(i = 0; i <= posX; i++) {
                                if(map.ground[posY-1, posX-i] != 1) {
                                    savePos = posX - i;
                                    break;
                                }
                            }
                            if (i == -1) {
                                break;
                            }
                            isInput = false;
                        }
                        isLeft = true;
                        isUp = false;
                    }
                    if (isDown) {
                        for (i = 0; i <= posX; i++) {
                            if (map.ground[posY + 1, posX - i] != 1) {
                                savePos = posX - i;
                                break;
                            }
                        }
                        isInput = false;
                        isLeft = true;
                        isDown = false;
                    }
                    break;
            }
        }
        private void CurveCheckPrivate(int i) {
            switch (agoDir) {
                case "U":
                    break;
                case "D":
                    break;
                case "L":
                    if (isUp) {
                        for (i = 0; i <= posX; i++) {
                            if (map.ground[posY - 1, posX - i] != 1) {
                                savePos = posX - i;
                                break;
                            }
                        }
                    }
                    if (isDown) {
                        for (i = 0; i <= posX; i++) {
                            if (map.ground[posY + 1, posX - i] != 1) {
                                savePos = posX - i;
                                break;
                            }
                        }
                    }
                    break;
                case "R":
                    //new

                    break;
            }
        }
        public void CurveMove() {
            if (posX == savePos) {
                isCurveMove = true;
                switch (agoDir) {
                    case "U":
                        break;
                    case "D":
                        break;
                    case "L":
                        if (self.Location.X >= (posX * 35 + 10) - 23) {
                            self.Left -= speed;
                        }
                        else {
                            isCurveMove = false;
                            DirClear();
                            savePos = 0;
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
        //public void Teleport() {
        //    if(isLeft)
        //        if (hitbox.Location.X <= -20)
        //            hitbox.Left = 955;
        //    if(isRight)
        //        if (hitbox.Location.X >= 950)
        //            hitbox.Left = -10;
        //}
    }
}
