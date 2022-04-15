using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarPigs.SharedXP.Harmony
{
    [HarmonyPatch(typeof(NetPackageEntityAddExpServer))]
    [HarmonyPatch(nameof(NetPackageEntityAddExpServer.ProcessPackage))]

    public class NetPackageEntityAddExpServer_Patch
    {
        private static bool Prefix(NetPackageEntityAddExpServer __instance)
        {
            Log.Out($">>>>>>>>>>>>>>>AddExpServer");

            // If I did not want the method we are patching to run at all, we would return false.
            return true;
        }
    }
}
