using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarPigs.SharedXP.Harmony
{
    [HarmonyPatch(typeof(NetPackageEntityAddExpClient))]
    [HarmonyPatch("ProcessPackage")]

    public class NetPackageEntityAddExpClient_Patch
    {
        private static bool Prefix(NetPackageEntityAddExpClient __instance, World _world, GameManager _callbacks, int ___entityId, int ___xp, int ___xpType)
        {
            Log.Out($"AddExpClient> Entity: {___entityId} XP: {___xp} Type: {___xpType}");

            // If I did not want the method we are patching to run at all, we would return false.
            return true;
        }
    }
}
