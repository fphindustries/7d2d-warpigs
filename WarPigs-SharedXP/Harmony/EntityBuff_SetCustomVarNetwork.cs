using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarPigs.SharedXP.Harmony
{
    [HarmonyPatch(typeof(EntityBuffs))]
    [HarmonyPatch(nameof(EntityBuffs.SetCustomVarNetwork))]
    public class EntityBuff_SetCustomVarNetwork
    {
        private static bool Prefix(EntityBuffs __instance, string _name, float _value)
        {
            if (_name.StartsWith("_xp"))
            {
                Log.Out($"EntityBuff_SetCustomVarNetwork> {_name} {_value}");
            }
            return true;
        }

    }
}
