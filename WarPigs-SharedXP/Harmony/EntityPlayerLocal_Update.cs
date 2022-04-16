using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace WarPigs.SharedXP.Harmony
{
    [HarmonyPatch(typeof(EntityPlayerLocal))]
    [HarmonyPatch("Update")]
    public class EntityPlayerLocal_Update
    {
        const float UpdatePeriod = 5.0f;

        private static string[] _experiences = { "_xpFromHarvesting", "_xpFromCrafting", "_xpFromKill", "_xpFromLoot", "_xpFromQuest", "_xpFromRepairBlock", "_xpFromUpgradeBlock", "_xpFromSelling" };
        private static float _nextUpdateTime = 0.0f;

        private static void Postfix(EntityPlayerLocal __instance)
        {
            if (Time.time > _nextUpdateTime)
            {
                Log.Out("XP Check");
                EntityPlayerLocal localPlayer = null;
                List<EntityPlayerLocal> localPlayers = GameManager.Instance.World.GetLocalPlayers();
                if (localPlayers != null && localPlayers.Count > 0)
                {
                    localPlayer = localPlayers[0];
                }

                if (localPlayer == null)
                {
                    Log.Out("Unable to find local player");
                    return;
                }

                foreach (var experience in _experiences)
                {
                    float maxXp = 0.0f;
                    foreach (EntityPlayer player in GameManager.Instance.World.Players.list)// this list is only of active players
                    {
                        var playerXp = player.GetCVar(experience);
                        maxXp = Math.Max(maxXp, playerXp);
                    }
                    var localXp = localPlayer.GetCVar(experience);
                    if(localXp < maxXp)
                    {
                        var newXp = Convert.ToInt32(maxXp - localXp);
                        Log.Out($"Adding {newXp} {experience} to Local Player");
                        localPlayer.Progression.AddLevelExp(newXp, experience, Progression.XPTypes.Other, false);
                    }

                }
                _nextUpdateTime = Time.time + UpdatePeriod;
            }
        }
    }
}
