using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarPigs.SharedXP.Harmony
{
    [HarmonyPatch(typeof(NetPackageEntityAddExpClient))]
    [HarmonyPatch(nameof(NetPackageEntityAddExpClient.ProcessPackage))]

    public class NetPackageEntityAddExpClient_Patch
    {
        private static bool Prefix(NetPackageEntityAddExpClient __instance)
        {
            Log.Out($">>>>>>>>>>>>>AddExpClient");

            // If I did not want the method we are patching to run at all, we would return false.
            return true;
        }
    }
}
