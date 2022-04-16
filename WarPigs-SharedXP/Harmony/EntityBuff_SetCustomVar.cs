using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarPigs.SharedXP.Harmony
{

    [HarmonyPatch(typeof(EntityBuffs))]
    [HarmonyPatch(nameof(EntityBuffs.SetCustomVar))]
    public class EntityBuff_SetCustomVar
    {
        private static bool Prefix(EntityBuffs __instance, string _name, float _value, bool _netSync)
        {
            if (_name.StartsWith("_xp"))
            {
                Log.Out($"EntityBuff_SetCustomVar> {_name} {_value} {_netSync}");
            }
            return true;
        }
    }
}
