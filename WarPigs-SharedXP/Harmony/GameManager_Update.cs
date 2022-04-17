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
        const float UpdatePeriod = 30.0f;

        private static string[] _experiences = { "_xpFromHarvesting", "_xpFromCrafting", "_xpFromKill", "_xpFromLoot", "_xpFromQuest", "_xpFromRepairBlock", "_xpFromUpgradeBlock", "_xpFromSelling", "_xpOther", "_xpFromParty" };
        private static float _nextUpdateTime = 0.0f;

        private static void Postfix(GameManager __instance)
        {
            //Make sure the game is urnning
            if (!__instance.gameStateManager.IsGameStarted()) return;

            //Make sure we're a dedicated server
            if (!GameManager.IsDedicatedServer) return;

            //Make sure we have players logged in
            if (GameManager.Instance.World.Players.list.Count == 0) return;

            if (Time.time > _nextUpdateTime)
            {
                Log.Out("Server XP Check");

                int maxLevel = 0;
                foreach (EntityPlayer player in GameManager.Instance.World.Players.list)// this list is only of active players
                {
                    Log.Out($"---{player.GetDebugName()}: {player.Progression.Level}");
                    maxLevel = Math.Max(player.Progression.Level, maxLevel);
                }
                foreach (EntityPlayer player in GameManager.Instance.World.Players.list)// this list is only of active players
                {
                    if(player.Progression.Level < maxLevel)
                    {
                        var expToNextLevel = player.Progression.ExpToNextLevel;
                        Log.Out($"---Adding {expToNextLevel} XP to {player.GetDebugName()} to Progress");
                        var clientInfo = SingletonMonoBehaviour<ConnectionManager>.Instance.Clients.ForEntityId(player.entityId);
                        if (clientInfo != null)
                        {
                            clientInfo.SendPackage(NetPackageManager.GetPackage<NetPackageEntityAddExpClient>().Setup(player.entityId, expToNextLevel, Progression.XPTypes.Other));
                        }
                    }
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
                case "_xpFromParty": return Progression.XPTypes.Party;
                default:
                    return Progression.XPTypes.Other;
            }
        }

    }
}
