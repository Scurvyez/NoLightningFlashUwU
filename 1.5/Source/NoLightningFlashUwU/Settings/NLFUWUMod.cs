using System;
using UnityEngine;
using Verse;

namespace NoLightningFlashUwU
{
    public class NLFUWUMod : Mod
    {
        public static NLFUWUMod Mod;
        
        private NLFUWUSettings _settings;
        private const float _elementHeight = 25f;
        private const float _elementSpacing = 20f;
        private const int _sliderMin = 0;
        private const int _sliderMax = 60;
        
        public NLFUWUMod(ModContentPack content) : base(content)
        {
            Mod = this;
            _settings = GetSettings<NLFUWUSettings>();
        }
        
        public override string SettingsCategory()
        {
            return "NLFUWU_ModName".Translate();
        }
        
        public override void DoSettingsWindowContents(Rect inRect)
        {
            base.DoSettingsWindowContents(inRect);
            float halfWidth = inRect.width / 2f;

            Rect leftRect = new (inRect.x, inRect.y, halfWidth - 10f, inRect.height);
            Rect rightRect = new (halfWidth + 10f, inRect.y, halfWidth - 10f, inRect.height);

            DrawLeftSideSettings(leftRect);
            DrawRightSideSettings(rightRect);
            
            _settings.Write();
        }
        
        private void DrawLeftSideSettings(Rect leftRect)
        {
            Listing_Standard listLeft = new ();
            listLeft.Begin(leftRect);
            
            DrawSettingWithSlider(leftRect, listLeft,
                "NLFUWU_LightningFlashDurationMaxLabel".Translate().Colorize(NLFUWULog.MessageMsgCol)
                + _settings.lightningFlashDurationMax.ToString("F2"),
                "NLFUWU_LightningFlashDurationMaxToolTip".Translate(),
                ref _settings.lightningFlashDurationMax,
                _sliderMin, _sliderMax,
                AssetManager.LightningFlash);

            listLeft.End();
        }
        
        private void DrawRightSideSettings(Rect rightRect)
        {
            Listing_Standard listRight = new ();
            listRight.Begin(rightRect);
            
            DrawSettingWithToggle(rightRect, listRight,
                "NLFUWU_LightningShadowFlashLabel".Translate().Colorize(NLFUWULog.MessageMsgCol),
                "NLFUWU_LightningShadowFlashToolTip".Translate(),
                ref _settings.lightningShadowFlashToggle,
                AssetManager.LightningShadowsFlash);
            
            listRight.End();
        }
        
        private static void DrawSettingWithSlider<T>(Rect inRect, Listing_Standard list, string labelText, string tooltipText, ref T settingValue, T minValue, T maxValue, Texture2D texture)
            where T : struct, IConvertible
        {
            float settingFloat = Convert.ToSingle(settingValue);
            float minFloat = Convert.ToSingle(minValue);
            float maxFloat = Convert.ToSingle(maxValue);
            
            Rect labelRect = new (0, list.CurHeight, inRect.width, _elementHeight);
            Widgets.Label(labelRect, labelText);
            TooltipHandler.TipRegion(labelRect, tooltipText);
            
            list.Gap(_elementSpacing);
            
            float sliderWidth = inRect.width / 2;
            Rect sliderRect = new (0, list.CurHeight, sliderWidth, _elementHeight);
            settingFloat = Widgets.HorizontalSlider(sliderRect, settingFloat, minFloat, maxFloat, true);
            settingValue = (T)Convert.ChangeType(settingFloat, typeof(T));
            
            list.Gap(_elementSpacing * 2f);
            
            if (texture == null) return;
            Rect textureRect = new (0, list.CurHeight, inRect.width, inRect.width);
            GUI.DrawTexture(textureRect, texture);
        }
        
        private static void DrawSettingWithToggle(Rect inRect, Listing_Standard list, string labelText, string tooltipText, ref bool settingValue, Texture2D texture)
        {
            Rect labelRect = new (0, list.CurHeight, inRect.width, _elementHeight);
            Widgets.Label(labelRect, labelText);
            TooltipHandler.TipRegion(labelRect, tooltipText);
    
            list.Gap(_elementSpacing);

            Rect toggleRect = new (0, list.CurHeight, _elementHeight, _elementHeight);
            Widgets.Checkbox(toggleRect.x, toggleRect.y, ref settingValue);

            list.Gap(_elementSpacing * 2f);

            if (texture == null) return;
            Rect textureRect = new (0, list.CurHeight, inRect.width, inRect.width);
            GUI.DrawTexture(textureRect, texture);
        }
    }
}