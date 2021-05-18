using System;
using System.Windows.Forms;

namespace Pacman {
    class Blinky : Enemy {
        Panel player;
        Panel blinky;
        public Blinky(Panel player, Panel blinky)
            : base(player, blinky) {

        }

    }
}
