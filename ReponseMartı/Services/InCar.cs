using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ReponseMartı
{
    public static class InCar
    {
        internal static void In(Player player, InteractableVehicle vehicle, ref bool shouldAllow)
        {
            UnturnedPlayer pl = UnturnedPlayer.FromPlayer(player);
            var Ayarlar = Class1.Instance.Configuration.Instance;
            var değer = Ayarlar.Martı.FirstOrDefault(martı => martı.MartıID == vehicle.instanceID);
            
            if (değer == null)
            {
                return;
            }

            shouldAllow = false;           


        }
       

    }
}
