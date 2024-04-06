using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using HarmonyLib;
using ViewMultiplier.Patches;

namespace ViewMultiplier;

[BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
public class Plugin : BaseUnityPlugin
{
    private readonly Harmony harmony = new(PluginInfo.PLUGIN_GUID);

    public static ConfigFile? ViewMultiplierCFG;
    public static ManualLogSource? VMLogger;

    private void Awake()
    {
        VMLogger = base.Logger;
        VMLogger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");
        
        ViewMultiplierCFG = base.Config;
        ViewMultiplierCFG?.Bind("Settings", "ViewMultiplier", 2, "Multiplies views by this integer. Default value is 2.");

        VMLogger.LogInfo("Config file is loaded!");

        harmony.PatchAll(typeof(Plugin));
        harmony.PatchAll(typeof(ViewCalculationPatch));
    }
}
