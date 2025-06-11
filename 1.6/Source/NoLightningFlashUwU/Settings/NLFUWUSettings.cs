using Verse;

namespace NoLightningFlashUwU
{
    public class NLFUWUSettings : ModSettings
    {
        private static NLFUWUSettings _instance;
        
        public NLFUWUSettings()
        {
            _instance = this;
        }
        
        public static int LightningFlashDurationMax => _instance.lightningFlashDurationMax;
        public static bool LightningShadowFlashToggle => _instance.lightningShadowFlashToggle;
        
        public int lightningFlashDurationMax = 60;
        public bool lightningShadowFlashToggle = false;

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(ref lightningFlashDurationMax, "lightningFlashDurationMax", 60);
            Scribe_Values.Look(ref lightningShadowFlashToggle, "lightningShadowFlashToggle", false);
        }
    }
}