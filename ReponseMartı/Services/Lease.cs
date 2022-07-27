using Rocket.Unturned.Chat;
using Rocket.Unturned.Events;
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
    public static class Lease
    {
        internal static void Lea(UnturnedPlayer player, UnturnedPlayerEvents.PlayerGesture gesture)
        {
            ushort EffectID = 9876;
            short AltEffectID = 44;

            if (gesture == UnturnedPlayerEvents.PlayerGesture.PunchRight)
            {
                RaycastInfo Car = Class1.Instance.TraceRay(player, 2048f, RayMasks.VEHICLE);
                InteractableVehicle vehicle = Car.vehicle;

                var değer = Class1.Instance.Configuration.Instance.Martı.FirstOrDefault(Martılar => Martılar.MartıID == vehicle.instanceID);

           
                if (değer.MartıSahip != player.CSteamID)
                {
                    if (değer.Sahipli)
                    {

                        UnturnedChat.Say("Bu Martı Sahipli");
                        return;
                    }
                    if (değer.MartıKilit)
                    {
                        UnturnedChat.Say("Bu Martı Kilitli");
                        return;
                    }
                    Class1.Instance.Kiralayan.Add(new Class1.Kiralarken { Kiralayan = player.CSteamID, KiralanacakMartı = vehicle.instanceID });
                    Class1.Instance.Configuration.Save();
                    player.Player.enablePluginWidgetFlag(EPluginWidgetFlags.Modal);

                    Class1.Görünürlük(44, "K", player.CSteamID, true);
                    EffectManager.sendUIEffectText(AltEffectID, player.CSteamID, true, "K-Benzin", vehicle.asset.fuel.ToString());
                    EffectManager.sendUIEffectText(AltEffectID, player.CSteamID, true, "K-Hasar", vehicle.asset.health.ToString());
                    EffectManager.sendUIEffectText(AltEffectID, player.CSteamID, true, "K-Ücret", $"{değer.MartıÜcret}/3s");
                }
                else
                {
                    player.Player.enablePluginWidgetFlag(EPluginWidgetFlags.Modal);

                    Class1.Görünürlük(44, "K1", player.CSteamID, true);
                    EffectManager.sendUIEffectText(AltEffectID, player.CSteamID, true, "K1-Benzin", vehicle.asset.fuel.ToString());
                    EffectManager.sendUIEffectText(AltEffectID, player.CSteamID, true, "K1-Hasar", vehicle.asset.health.ToString());
                    EffectManager.sendUIEffectText(AltEffectID, player.CSteamID, true, "K1-Ücret", $"{değer.MartıToplamÜcret}");

                }


            }
        }


    }
}
