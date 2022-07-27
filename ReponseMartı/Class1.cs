using Rocket.Core.Commands;
using Rocket.Core.Plugins;
using Rocket.Unturned;
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
using Rocket.API;


namespace ReponseMartı
{
    public class Class1 : RocketPlugin<Config>
    {
        public static Class1 Instance;
        public List<CSteamID> Martıdakiler = new List<CSteamID>();
        public List<Kiralarken> Kiralayan = new List<Kiralarken>();

        public class Kiralarken
        {
            public CSteamID Kiralayan;
            public uint KiralanacakMartı;
        }
        protected override void Load()
        {
            Instance = this;

            U.Events.OnPlayerDisconnected += Exit.exit;
            UnturnedPlayerEvents.OnPlayerUpdateGesture += Lease.Lea;
            VehicleManager.onEnterVehicleRequested += InCar.In;
            VehicleManager.onExitVehicleRequested += OutCar.Out;
            U.Events.OnPlayerConnected += Join;
            VehicleManager.OnToggleVehicleLockRequested += LockCar.Lock;
            EffectManager.onEffectButtonClicked += Kiralama;
            base.Load();
        }
        [RocketCommand("test","test","test",Rocket.API.AllowedCaller.Both)]
        public void Executes(IRocketPlayer caller, string[] com)
        {
            UnturnedPlayer pl = (UnturnedPlayer)caller;
            pl.Experience += 2000;
        }
        public static void Görünürlük(short Id, string Name, CSteamID Pl, bool cancel)
        {
            EffectManager.sendUIEffectVisibility(Id, Pl, true, Name, cancel);
        }
        private void Join(UnturnedPlayer player)
        {
            ushort EffectID = 9877;
            short AltEffectID = 44;
            EffectManager.sendUIEffect(EffectID, AltEffectID, player.CSteamID, true);
            Class1.Görünürlük(44, "K3", player.CSteamID, false);
        }

        private void Kiralama(Player player, string buttonName)
        {

            UnturnedPlayer pl = UnturnedPlayer.FromPlayer(player);
            switch (buttonName)
            {
                case "K-KİRALA":
                    var değer = Kiralayan.FirstOrDefault(ki => ki.Kiralayan == pl.CSteamID);
                    var değer2 = Configuration.Instance.Martı.FirstOrDefault(e => e.MartıSahip == pl.CSteamID);
                    if (değer == null)
                    {
                        UnturnedChat.Say("Bir Hata Oluştu. Lütfen Tekrar Deneyiniz.");
                        return;
                    }
                    else if (değer2 != null)
                    {
                        UnturnedChat.Say("Zaten Bir Martıya Sahipsin, Martıyı Bırakmak İçin /martım Yazınız.");
                        return;
                    }
                    var car = VehicleManager.getVehicle(değer.KiralanacakMartı);
                    VehicleManager.ServerForcePassengerIntoVehicle(player,car);
                    Martıdakiler.Add(pl.CSteamID);
                    Configuration.Save();
                    var değers = Configuration.Instance.Martı.FirstOrDefault(e => e.MartıID == değer.KiralanacakMartı);
                    değers.MartıSahip = pl.CSteamID;
                    değers.Sahipli = true;
                    değers.MartıKilit = false;
                    Kiralayan.Remove(değer);
                    Configuration.Save();
                    pl.Player.disablePluginWidgetFlag(EPluginWidgetFlags.Modal);
                    StartCoroutine(Tests(pl.CSteamID));
                    Class1.Görünürlük(44, "K", pl.CSteamID, false);
                    break;
                case "K-VAZGEÇ":
                    var değerss = Kiralayan.FirstOrDefault(ki => ki.Kiralayan == pl.CSteamID);
                    if (değerss == null)
                    {
                        UnturnedChat.Say("Bir Hata Oluştu. Lütfen Tekrar Deneyiniz.");
                        return;
                    }

                    Kiralayan.Remove(değerss);
                    Configuration.Save();
                    Class1.Görünürlük(44, "K", pl.CSteamID, false);
                    pl.Player.disablePluginWidgetFlag(EPluginWidgetFlags.Modal);
                    break;
                case "K1-ÖDE":
                    var K1Öde = Configuration.Instance.Martı.FirstOrDefault(ki => ki.MartıSahip == pl.CSteamID);

                    if (pl.Experience < K1Öde.MartıToplamÜcret)
                    {
                        UnturnedChat.Say("Yeterli Paran Bulunmamakta!");
                    }
                    else
                    {
                        pl.Experience -= (uint)K1Öde.MartıToplamÜcret;
                        K1Öde.Sahipli = false;
                        K1Öde.MartıToplamÜcret = 0;
                        K1Öde.MartıKilit = false;
                        K1Öde.MartıSahip = CSteamID.Nil;
                        Martıdakiler.Remove(pl.CSteamID);
                        Configuration.Save();
                        Class1.Görünürlük(44, "K1", pl.CSteamID, false);
                        Class1.Görünürlük(44, "K3", pl.CSteamID, false);
                      
                        pl.Player.disablePluginWidgetFlag(EPluginWidgetFlags.Modal);
                    }
                    break;
                case "K1-BİN":
                    var K1Bin = Configuration.Instance.Martı.FirstOrDefault(ki => ki.MartıSahip == pl.CSteamID);

                    var cars = VehicleManager.getVehicle(K1Bin.MartıID);

                    VehicleManager.ServerForcePassengerIntoVehicle(player, cars);
                    Martıdakiler.Add(pl.CSteamID);
                    K1Bin.MartıKilit = false;
                    StartCoroutine(Tests(pl.CSteamID));
                    Configuration.Save();
                    Class1.Görünürlük(44, "K1", pl.CSteamID, false);
                    pl.Player.disablePluginWidgetFlag(EPluginWidgetFlags.Modal);
                    break;
                case "K1-VAZGEÇ":

                    Görünürlük(44, "K1", pl.CSteamID, false);
                    pl.Player.disablePluginWidgetFlag(EPluginWidgetFlags.Modal);
                    break;
                case "K2-KİLİTLE":
                    var K2Kilitle = Configuration.Instance.Martı.FirstOrDefault(ki => ki.MartıSahip == pl.CSteamID);
                    K2Kilitle.MartıKilit = true;
                    Martıdakiler.Remove(pl.CSteamID);
                    Configuration.Save();
                    if (pl.IsInVehicle)
                    {
                        pl.CurrentVehicle.forceRemovePlayer(out byte seat, pl.CSteamID, out Vector3 pos, out byte angle);
                        VehicleManager.sendExitVehicle(pl.CurrentVehicle, seat, pos, angle, true);
                        Class1.Görünürlük(44, "K2", pl.CSteamID, false);
                        Class1.Görünürlük(44, "K3", pl.CSteamID, false);
                        pl.Player.disablePluginWidgetFlag(EPluginWidgetFlags.Modal);
                    }
                    else
                    {
                        UnturnedChat.Say("Bir Hata Oluştu. Lütfen Tekrar Deneyiniz.");
                    }

                    break;
                case "K2-VAZGEÇ":

                    Görünürlük(44, "K2", pl.CSteamID, false);
                    pl.Player.disablePluginWidgetFlag(EPluginWidgetFlags.Modal);
                    break;
                case "K2-ÖDE":
                    var K2Öde = Configuration.Instance.Martı.FirstOrDefault(ki => ki.MartıSahip == pl.CSteamID);

                    if (pl.Experience < K2Öde.MartıToplamÜcret)
                    {
                        UnturnedChat.Say("Yeterli Paran Bulunmamakta!");
                    }
                    else
                    {
                        pl.Experience -= (uint)K2Öde.MartıToplamÜcret;
                        K2Öde.Sahipli = false;
                        K2Öde.MartıToplamÜcret = 0;
                        K2Öde.MartıKilit = false;
                        K2Öde.MartıSahip = CSteamID.Nil;
                        Martıdakiler.Remove(pl.CSteamID);
                        Configuration.Save();
                        Class1.Görünürlük(44, "K2", pl.CSteamID, false);
                        Class1.Görünürlük(44, "K3", pl.CSteamID, false);
                        if (pl.IsInVehicle)
                        {
                            pl.CurrentVehicle.forceRemovePlayer(out byte seat, pl.CSteamID, out Vector3 pos, out byte angle);
                            VehicleManager.sendExitVehicle(pl.CurrentVehicle, seat, pos, angle, true);
                        }
                        else
                        {
                            UnturnedChat.Say("Bir Hata Oluştu. Lütfen Tekrar Deneyiniz.");
                        }
                        pl.Player.disablePluginWidgetFlag(EPluginWidgetFlags.Modal);
                    }
                    break;
            }
        }
        public IEnumerator<WaitForSeconds> Tests(CSteamID pl)
        {
            var değer = Configuration.Instance.Martı.FirstOrDefault(e => e.MartıSahip == pl);
            Class1.Görünürlük(44, "K3", pl, true);
            EffectManager.sendUIEffectText(44, pl, true, "K3-PARA", değer.MartıToplamÜcret.ToString());
            while (Martıdakiler.Contains(pl))
            {
                yield return new WaitForSeconds(3);
                değer.MartıToplamÜcret += değer.MartıÜcret;
                EffectManager.sendUIEffectText(44, pl, true, "K3-PARA", değer.MartıToplamÜcret.ToString());
                Configuration.Save();

            }
        }

        [RocketCommand("martım","martım","martım", AllowedCaller.Player)]
        public void martım(IRocketPlayer caller, string[] args)
        {
            UnturnedPlayer pl = (UnturnedPlayer)caller;
            var buttonname = args[0];
            var değer = Configuration.Instance.Martı.FirstOrDefault(e => e.MartıSahip == pl.CSteamID);

            switch (buttonname)
            {
                case "öde":
                    if (pl.IsInVehicle)
                    {
                        UnturnedChat.Say("İşlemi Gerçekleştirmek İçin Martıdan İnmelisin");
                        return;
                    }
                    else if (değer == null)
                    {
                        UnturnedChat.Say("Martın Bulunmamaktadır");
                        return;
                    }

                    if (pl.Experience >= değer.MartıÜcret)
                    {
                        pl.Experience -= (uint)değer.MartıÜcret;
                        değer.Sahipli = false;
                        değer.MartıSahip = CSteamID.Nil;
                        değer.MartıKilit = false;
                        değer.MartıToplamÜcret = 0;
                        UnturnedChat.Say("İşlem Başarılı.");

                    }
                    else
                    {
                        UnturnedChat.Say("Yeterli Paran Bulunmamaktadır.");
                    }
                    break;
                case "Sorgula":
                    if (pl.IsInVehicle)
                    {
                        UnturnedChat.Say("İşlemi Gerçekleştirmek İçin Martıdan İnmelisin");
                        return;
                    }
                    else if (değer == null)
                    {
                        UnturnedChat.Say("Martın Bulunmamaktadır");
                        return;
                    }
                    UnturnedChat.Say($"Martının Ücreti: {değer.MartıToplamÜcret} - Martının 3 Saniyelik Fiyatı: {değer.MartıÜcret}");
                    break;
                
             }
           
        }
        public RaycastInfo TraceRay(UnturnedPlayer player, float distance, int masks)
        {
            return DamageTool.raycast(new Ray(player.Player.look.aim.position, player.Player.look.aim.forward), distance, masks);
        }
    }
}
