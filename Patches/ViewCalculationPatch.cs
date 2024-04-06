using HarmonyLib;

namespace ViewMultiplier.Patches
{
    [HarmonyPatch(typeof(ContentBuffer))]
    internal class ViewCalculationPatch
    {
        internal static float MultiplierValue
        {
            get { return (float)Plugin.ViewMultiplierCFG?.Bind("Settings", "ViewMultiplier", 2, "Multiplies views by this integer. Default value is 2.").Value; }
        }

        [HarmonyPatch(nameof(ContentBuffer.GetScore))]
        [HarmonyPostfix]
        static void MultiplierPatch(ref float __result)
        {
            float multVal = MultiplierValue;
            __result *= multVal;
        }
    }
}
