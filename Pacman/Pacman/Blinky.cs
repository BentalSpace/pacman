using System;
using System.Windows.Forms;

namespace Pacman {
    class Blinky : Enemy {
        int posX = 13;
        int posY = 11;

        public Blinky(Panel player, Panel blinky)
            : base(player, blinky) {

        }
        
    }
}
