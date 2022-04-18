using System.Reflection;

namespace Harmony
{
    public class SharedXPMod : IModApi
    {
        public void InitMod(Mod _modInstance)
        {
            var harmony = new HarmonyLib.Harmony(GetType().ToString());
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}