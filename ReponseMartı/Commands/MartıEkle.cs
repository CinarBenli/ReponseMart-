using Rocket.API;
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

namespace ReponseMartı.Commands
{
    class MartıEkle : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "martıekle";

        public string Help => "Baktığınız Aracı Martı Aracı Yapar";

        public string Syntax => "/martıekle";

        public List<string> Aliases => new List<string> { "" };

        public List<string> Permissions => new List<string> { "reponse.martı.ekle" };

        public void Execute(IRocketPlayer caller, string[] command)
        {
            UnturnedPlayer player = (UnturnedPlayer)caller;
            RaycastInfo Car = Class1.Instance.TraceRay(player, 2048f, RayMasks.VEHICLE);
            var ücret = Convert.ToUInt16(command[0]);
            InteractableVehicle vehicle = Car.vehicle;
            var VehicleID = vehicle.instanceID;
            var Ayarlar = Class1.Instance.Configuration.Instance;
            var değer = Ayarlar.Martı.FirstOrDefault(e => e.MartıID == VehicleID);


            if (command.Count() != 0)
            {
                if (değer != null)
                {
                    UnturnedChat.Say("Bu Araç Zaten Martı!");
                    return;
                }

                if (vehicle.isLocked)
                {
                    UnturnedChat.Say("Aracın Kilitli Olmaması Lazım!");
                    return;
                }

                UnturnedChat.Say("Başarılı Bir Şekilde Martıya DÖnüştü!");
                Ayarlar.Martı.Add(new Martılar { MartıID = VehicleID, MartıKilit = false, MartıSahip = CSteamID.Nil, Sahipli = false, MartıÜcret = ücret, MartıToplamÜcret = 0 });

            }
            else
            {
                UnturnedChat.Say("Yanlış Kullanım");
            }

        }

    
    }
}
