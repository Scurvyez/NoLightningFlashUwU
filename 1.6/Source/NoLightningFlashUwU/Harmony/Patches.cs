using System.Reflection;
using HarmonyLib;
using RimWorld;
using UnityEngine;
using Verse;

namespace NoLightningFlashUwU
{
    [StaticConstructorOnStartup]
    public static class Patches
    {
        private static int customFlashDuration;
        private static bool suppressShadowsFlash;
        
        static Patches()
        {
            Harmony harmony = new (id: "rimworld.scurvyez.nolightningflashuwu");
            
            harmony.Patch(original: AccessTools.Constructor(typeof(WeatherEvent_LightningFlash), new[] { typeof(Map) }),
                postfix: new HarmonyMethod(typeof(Patches), nameof(WeatherEvent_LightningFlashConstructor_Postfix)));

            harmony.Patch(original: AccessTools.PropertyGetter(typeof(WeatherEvent_LightningFlash), "SkyTargetLerpFactor"),
                prefix: new HarmonyMethod(typeof(Patches), nameof(WeatherEvent_LightningFlashSkyTargetLerpFactor_Prefix)));
            
            harmony.Patch(original: AccessTools.PropertyGetter(typeof(WeatherEvent_LightningFlash), "OverrideShadowVector"),
                prefix: new HarmonyMethod(typeof(Patches), nameof(WeatherEvent_LightningFlashOverrideShadowVector_Prefix)));
        }
        
        private static void WeatherEvent_LightningFlashConstructor_Postfix(WeatherEvent_LightningFlash __instance)
        {
            customFlashDuration = Rand.Range(0, NLFUWUSettings.LightningFlashDurationMax);
            suppressShadowsFlash = NLFUWUSettings.LightningShadowFlashToggle;
        }
        
        private static bool WeatherEvent_LightningFlashSkyTargetLerpFactor_Prefix(WeatherEvent_LightningFlash __instance, ref float __result)
        {
            FieldInfo ageField = AccessTools.Field(typeof(WeatherEvent_LightningFlash), "age");
            int age = (int)ageField.GetValue(__instance);
            
            if (age <= 3)
            {
                __result = (float)age / 3f;
            }
            __result = 1f - (float)age / (float)customFlashDuration;
            return false;
        }
        
        private static bool WeatherEvent_LightningFlashOverrideShadowVector_Prefix(ref Vector2? __result)
        {
            if (!suppressShadowsFlash) return true;
            __result = null;
            return false;
        }
    }
}