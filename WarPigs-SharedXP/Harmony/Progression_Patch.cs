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

        private static void Postfix(Progression __instance, int _exp, string _cvarXPName, Progression.XPTypes _xpType, bool useBonus)
        {
            foreach (EntityPlayer player in GameManager.Instance.World.Players.list)// this list is only of active players
            {
                //if (player != __instance.parent)
                //{
                    var playerXp = player.GetCVar(_cvarXPName);
                    Log.Out($">> {player.GetDebugName()}: {_cvarXPName}: {playerXp}");
                    //if (playerXp < maxExperience)
                    //{
                    //    var delta = maxExperience - playerXp;
                    //    Log.Out($"Adding {delta} {_cvarXPName} xp to {player.EntityName}");

                    //    NetPackageEntityAddExpClient package = NetPackageManager.GetPackage<NetPackageEntityAddExpClient>().Setup(player.entityId, Convert.ToInt32(delta), _xpType);
                    //    SingletonMonoBehaviour<ConnectionManager>.Instance.SendPackage(package, false, player.entityId, -1, -1, -1);

                    //    player.Progression.AddLevelExp(Convert.ToInt32(delta), _cvarXPName, _xpType, useBonus);
                    //}
                //}
            }
        }
    }
}
