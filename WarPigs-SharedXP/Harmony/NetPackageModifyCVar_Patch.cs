using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarPigs.SharedXP.Harmony
{
    [HarmonyPatch(typeof(NetPackageModifyCVar))]
    [HarmonyPatch(nameof(NetPackageModifyCVar.ProcessPackage))]
    internal class NetPackageModifyCVar_Patch
    {
        private static bool Prefix(NetPackageModifyCVar __instance)
        {
            Log.Out($">>>>>>> NetPackageModifyCVar.ProcessPackage <<<<<<<<<<<<<");

            // If I did not want the method we are patching to run at all, we would return false.
            return true;
        }
    }
}
