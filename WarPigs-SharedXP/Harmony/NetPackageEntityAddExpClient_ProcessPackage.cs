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
    public class NetPackageEntityAddExpClient_ProcessPackage
    {
        private static bool Prefix(NetPackageEntityAddExpClient __instance, World _world, GameManager _callbacks, int ___entityId, int ___xp, int ___xpType)
        {
            Log.Out("NetPackageEntityAddExpClient_ProcessPackage");
			if (_world == null)
			{
				return true;
			}
            EntityAlive entityAlive = (EntityAlive)_world.GetEntity(___entityId);
            if (entityAlive != null)
            {
                Progression.XPTypes xpType = (Progression.XPTypes)___xpType;
                string cvarXPName = GetCvarFromXPType(xpType);

                Log.Out($"Adding XP: Entity: {entityAlive.GetDebugName()} cvarXPName: {cvarXPName} xpType: {xpType} XP: {___xp}");
                entityAlive.Progression.AddLevelExp(___xp, cvarXPName, xpType, false);
                return false;
			}
			else
            {
				Log.Out($"ProcessPackage: Entity Was Null");
            }

			return true;
        }


        private static string GetCvarFromXPType(Progression.XPTypes xpType)
        {
            switch (xpType)
            {
                case Progression.XPTypes.Kill: return "_xpFromKill";
                case Progression.XPTypes.Harvesting: return "_xpFromHarvesting";
                case Progression.XPTypes.Upgrading: return "_xpFromUpgradeBlock";
                case Progression.XPTypes.Crafting: return "_xpFromCrafting";
                case Progression.XPTypes.Selling: return "_xpFromSelling";
                case Progression.XPTypes.Quest: return "_xpFromQuest";
                case Progression.XPTypes.Looting: return "_xpFromLoot";
                case Progression.XPTypes.Party: return "_xpFromParty";
                case Progression.XPTypes.Repairing: return "_xpFromRepairBlock";
                case Progression.XPTypes.Other:
                case Progression.XPTypes.Debug: 
                case Progression.XPTypes.Max: 
                default:
                    return "_xpOther";
            }
        }
    }
}
