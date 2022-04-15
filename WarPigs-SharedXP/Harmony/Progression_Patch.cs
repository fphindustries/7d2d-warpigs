using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarPigs.SharedXP.Harmony
{
    [HarmonyPatch(typeof(Progression))]
    [HarmonyPatch("AddLevelExp")]
    public class Progression_Patch
    {
        private static bool Prefix(Progression __instance)
        {
            Log.Out($">>>>>>> AddLevelExp <<<<<<<<<<<<<");

            // If I did not want the method we are patching to run at all, we would return false.
            return true;
        }
    }
}
