using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarPigs.SharedXP.Harmony
{
    //[HarmonyPatch(typeof(NetPackageModifyCVar))]
    //[HarmonyPatch("ProcessPackage")]
    //public class NetPackageModifyCVar_ProcessPackage
    //{
    //    //private static bool Prefix(NetPackageModifyCVar __instance, World _world, GameManager _callbacks, int ___m_entityId, string ___cvarName, float ___value)
    //    //{
    //    //    Log.Out($"---CVar Update: {___m_entityId} {___cvarName} {___value}");
    //    //    //if (!GameManager.IsDedicatedServer) return true;

    //    //    if(___cvarName.StartsWith("_xp"))
    //    //    {
    //    //        EntityAlive entityAlive = _world.GetEntity(___m_entityId) as EntityAlive;
    //    //        if (entityAlive != null)
    //    //        {
    //    //            Log.Out($"===CVar Update from {entityAlive.GetDebugName()}: {___cvarName}: {___value}");
    //    //        }
    //    //    }
    //    //    return true;
    //    //}

    //    private static void Postfix(NetPackageModifyCVar __instance, World _world, GameManager _callbacks, int ___m_entityId, string ___cvarName, float ___value)
    //    {
    //        Log.Out($"---Post CVar Update: {___m_entityId} {___cvarName} {___value}");
    //    }

    //}
}
