using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WarPigs.SharedXP.Harmony
{
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch("Update")]
    public class GameManager_Update
    {
        const float UpdatePeriod = 5.0f;

        private static string[] _experiences = { "_xpFromHarvesting", "_xpFromCrafting", "_xpFromKill", "_xpFromLoot", "_xpFromQuest", "_xpFromRepairBlock", "_xpFromUpgradeBlock", "_xpFromSelling" };
        private static float _nextUpdateTime = 0.0f;

        private static void Postfix(GameManager __instance)
        {
            if (!GameManager.IsDedicatedServer) return;

            if (Time.time > _nextUpdateTime)
            {
                Log.Out("Server XP Check");

                foreach (var experience in _experiences)
                {
                    float maxXp = 0.0f;
                    foreach (EntityPlayer player in GameManager.Instance.World.Players.list)// this list is only of active players
                    {
                        var playerXp = player.GetCVar(experience);
                        Log.Out($"--{player.GetDebugName()}: {playerXp}");
                        maxXp = Math.Max(maxXp, playerXp);

                    }
                    //var localXp = localPlayer.GetCVar(experience);
                    Log.Out($"--MaxXP: {maxXp}");
                    //Log.Out($"->{localPlayer.GetDebugName()}: {localXp}");
                    //if (localXp < maxXp)
                    //{
                    //    var newXp = Convert.ToInt32(maxXp - localXp);
                    //    Log.Out($"Adding {newXp} {experience} to Local Player");
                    //    localPlayer.Progression.AddLevelExp(newXp, experience, GetXPTypeFromCvar(experience), false);
                    //}

                }
                _nextUpdateTime = Time.time + UpdatePeriod;
            }

        }
        private static Progression.XPTypes GetXPTypeFromCvar(string cvar)
        {
            switch (cvar)
            {
                case "_xpFromHarvesting": return Progression.XPTypes.Harvesting;
                case "_xpFromCrafting": return Progression.XPTypes.Crafting;
                case "_xpFromKill": return Progression.XPTypes.Kill;
                case "_xpFromLoot": return Progression.XPTypes.Looting;
                case "_xpFromQuest": return Progression.XPTypes.Quest;
                case "_xpFromRepairBlock": return Progression.XPTypes.Repairing;
                case "_xpFromUpgradeBlock": return Progression.XPTypes.Upgrading;
                case "_xpFromSelling": return Progression.XPTypes.Selling;
                default:
                    return Progression.XPTypes.Other;
            }
        }

    }
}
