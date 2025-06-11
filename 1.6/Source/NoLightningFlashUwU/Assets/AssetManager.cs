using UnityEngine;
using Verse;

namespace NoLightningFlashUwU
{
    [StaticConstructorOnStartup]
    public static class AssetManager
    {
        public static readonly Texture2D LightningFlash = ContentFinder<Texture2D>.Get("NoLightningFlashUwU/UI/LightningFlash");
        public static readonly Texture2D LightningShadowsFlash = ContentFinder<Texture2D>.Get("NoLightningFlashUwU/UI/LightningShadowsFlash");
    }
}