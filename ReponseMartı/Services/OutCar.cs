using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ReponseMartı
{
    public static class OutCar
    {
        internal static void Out(Player player, InteractableVehicle vehicle, ref bool shouldAllow, ref Vector3 pendingLocation, ref float pendingYaw)
        {
            UnturnedPlayer pl = UnturnedPlayer.FromPlayer(player);
            var Ayarlar = Class1.Instance.Configuration.Instance;
            var değer = Ayarlar.Martı.FirstOrDefault(martı => martı.MartıID == vehicle.instanceID);

            if (değer == null)
            {
                return;
            }
            shouldAllow = false;

            pl.Player.enablePluginWidgetFlag(EPluginWidgetFlags.Modal);
            Class1.Görünürlük(44, "K2", pl.CSteamID, true);
            EffectManager.sendUIEffectText(44, pl.CSteamID, true, "K2-Benzin", vehicle.asset.fuel.ToString());
            EffectManager.sendUIEffectText(44, pl.CSteamID, true, "K2-Hasar", vehicle.asset.health.ToString());
            EffectManager.sendUIEffectText(44, pl.CSteamID, true, "K2-Ücret", $"{değer.MartıToplamÜcret}");

     
        }
    }
}
