using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReponseMartı
{
    public static class LockCar
    {
        internal static void Lock(InteractableVehicle vehicle, ref bool shouldAllow)
        {
            var değer = Class1.Instance.Configuration.Instance.Martı.FirstOrDefault(e => e.MartıID == vehicle.instanceID);

            if (değer == null)
            {
                return;
            }

            shouldAllow = false;
        }
    }
}
