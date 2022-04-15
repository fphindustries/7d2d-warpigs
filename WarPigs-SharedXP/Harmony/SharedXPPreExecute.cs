using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;

namespace WarPigs.SharedXP.Harmony
{
    [HarmonyPatch(typeof(XUiC_MainMenu))]
    [HarmonyPatch("OnOpen")]
    public class SampleProjectPrefix
    {
        // The __instance is a reference to the class you are patching. 
        // To access a private field of the class, add it to the parameter line, adding three underscore (___) to the variable name.
        // To access a private field, and to change its value, pass it by reference by adding ref to it.
        private static bool Prefix(XUiC_MainMenu __instance, XUiC_SimpleButton ___btnNewGame, ref XUiC_SimpleButton ___btnContinueGame)
        {
            Log.Out($"SampleProject Prefix Example: Am I opened? {__instance.IsOpen}");
            Log.Out($"btnNewGame's Label: {___btnNewGame.Label}");
            Log.Out("OnOpenPrefix(): I happen before the method starts.");

            // If I did not want the method we are patching to run at all, we would return false.
            return true;
        }
    }

    [HarmonyPatch(typeof(XUiC_MainMenu))]
    [HarmonyPatch("OnOpen")]
    public class SampleProjectPostfix
    {
        // A Postfix can have a return type of void, or it can have a return type of the method you are patching.
        private static void Postfix(XUiC_MainMenu __instance)
        {
            Log.Out($"SampleProject Postfix Example: Am I opened? {__instance.IsOpen}");
            Log.Out("OnOpenPostfix(): I happen after the method is done!");
        }
    }

    //[HarmonyPatch(typeof(Progression))]
    //[HarmonyPatch("AddLevelExp")]
    //public class SharedXPPreExecute
    //{
    //    private static void Postfix(Progression __instance, int _exp, string _cvarXPName, Progression.XPTypes _xpType, bool useBonus)
    //    {
    //        if (_xpType == Progression.XPTypes.Debug) return;

    //        float maxExperience = 0;
    //        foreach (EntityPlayer player in GameManager.Instance.World.Players.list)// this list is only of active players
    //        {
    //            maxExperience = Math.Max(player.GetCVar(_cvarXPName), maxExperience);
    //        }
    //        if (maxExperience > 0)
    //        {
    //            Log.Out($"Max {_cvarXPName} Experience: {maxExperience}");

    //            foreach (EntityPlayer player in GameManager.Instance.World.Players.list)// this list is only of active players
    //            {
    //                if (player != __instance.parent )
    //                {
    //                    var playerXp = player.GetCVar(_cvarXPName);
    //                    if (playerXp < maxExperience)
    //                    {
    //                        var delta = maxExperience - playerXp;
    //                        Log.Out($"Adding {delta} {_cvarXPName} xp to {player.EntityName}");

    //                        NetPackageEntityAddExpClient package = NetPackageManager.GetPackage<NetPackageEntityAddExpClient>().Setup(player.entityId, Convert.ToInt32(delta), _xpType);
    //                        SingletonMonoBehaviour<ConnectionManager>.Instance.SendPackage(package, false, player.entityId, -1, -1, -1);

    //                        //player.Progression.AddLevelExp(Convert.ToInt32(delta), _cvarXPName, _xpType, useBonus);
    //                    }
    //                }
    //            }
    //        }
    //    }
    //}
}
