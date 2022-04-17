using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarPigs.SharedXP.Harmony
{
 //   [HarmonyPatch(typeof(EntityBuffs))]
 //   [HarmonyPatch(nameof(EntityBuffs.SetCustomVarNetwork))]
 //   public class EntityBuff_SetCustomVarNetwork
 //   {
	//	//     private static bool Prefix(EntityBuffs __instance, string _name, float _value)
	//	//     {
	//	//         if (_name.StartsWith("_xp"))
	//	//         {
	//	//	if (!__instance.parent.isEntityRemote)
	//	//	{
	//	//		return false;
	//	//	}
	//	//	if (_name[0] == '.')
	//	//	{
	//	//		return false;
	//	//	}
	//	//	if (SingletonMonoBehaviour<ConnectionManager>.Instance.IsServer)
	//	//	{
	//	//		SingletonMonoBehaviour<ConnectionManager>.Instance.SendPackage(NetPackageManager.GetPackage<NetPackageModifyCVar>().Setup(__instance.parent, _name, _value), true, -1, -1, -1, -1);
	//	//		return false;
	//	//	}
	//	//	SingletonMonoBehaviour<ConnectionManager>.Instance.SendToServer(NetPackageManager.GetPackage<NetPackageModifyCVar>().Setup(__instance.parent, _name, _value), true);
	//	//	return false;
	//	//}
	//	//return true;
	//	//     }

	//	private static void Postfix(EntityBuffs __instance, string _name, float _value)
 //       {
	//		Log.Out($"Sent Network CVAR: {_name} {_value}");
	//	}


	//}
}
