using Rocket.Unturned.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReponseMartı
{
    public static class Exit
    {
        internal static void exit(UnturnedPlayer player)
        {
            var Ayarlar1 = Class1.Instance;

            if (Ayarlar1.Martıdakiler.Contains(player.CSteamID))
            {
                Ayarlar1.Martıdakiler.Remove(player.CSteamID);
                return;
            }
        }
    }
}
