using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WarPigs.SharedXP.Harmony
{
    /// <summary>
    /// Patches GameManager.Update to make sure all players are at the same level.
    /// Note that players are only progessed by one level per check
    /// </summary>
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch("Update")]
    public class GameManager_Update
    {
        /// <summary>
        /// Number of seconds between Level checks
        /// </summary>
        const float UpdatePeriod = 30.0f;

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
                Log.Out("FPHI: Server Level Check");

                //Iterate through players to determine the max level
                int maxLevel = 0;
                foreach (EntityPlayer player in GameManager.Instance.World.Players.list)// this list is only of active players
                {
                    Log.Out($"FPHI: ---{player.GetDebugName()}: {player.Progression.Level}");
                    maxLevel = Math.Max(player.Progression.Level, maxLevel);
                }

                //Now that we know the max level, iterate the players again and make sure
                //they're at that level
                foreach (EntityPlayer player in GameManager.Instance.World.Players.list)// this list is only of active players
                {
                    if(player.Progression.Level < maxLevel)
                    {
                        var expToNextLevel = player.Progression.ExpToNextLevel;
                        Log.Out($"FPHI: ---Adding {expToNextLevel} XP to {player.GetDebugName()} to Progress");
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
    }
}
