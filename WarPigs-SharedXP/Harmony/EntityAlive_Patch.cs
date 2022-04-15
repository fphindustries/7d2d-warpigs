using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarPigs.SharedXP.Harmony
{
    [HarmonyPatch(typeof(EntityAlive))]
    [HarmonyPatch("OnEntityDeath")]
    public class EntityAlive_Patch
    {
        private static bool Prefix(EntityAlive __instance)
        {
            Log.Out($">>>>>>> OnEntityDeath <<<<<<<<<<<<<");

            // If I did not want the method we are patching to run at all, we would return false.
            return true;
        }
    }
}
