using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarPigs.SharedXP
{
    [HarmonyPatch(typeof(GameManager))]
    [HarmonyPatch(nameof(GameManager.PlayerSpawnedInWorld))]
    public class GameManager_PlayerSpawnedInWorld
    {
        private static void Postfix(GameManager __instance)
        {
            //Make sure the game is running
            if (!__instance.gameStateManager.IsGameStarted()) return;

            //Make sure we're a dedicated server
            if (!GameManager.IsDedicatedServer) return;

            //Make sure we have players logged in
            if (GameManager.Instance.World.Players.list.Count == 0) return;

            Log.Out($"FPHI: PlayerSpawnedInWorld");

        }
    }
}
