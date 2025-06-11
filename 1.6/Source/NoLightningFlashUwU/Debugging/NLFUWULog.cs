using UnityEngine;
using Verse;

namespace NoLightningFlashUwU
{
    public class NLFUWULog
    {
        public static Color ErrorMsgCol = new (0.4f, 0.54902f, 1.0f);
        public static Color WarningMsgCol = new (0.70196f, 0.4f, 1.0f);
        public static Color MessageMsgCol = new (0.4f, 1.0f, 0.54902f);

        public static void Error(string msg)
        {
            Log.Error("[No Lightning Flash UwU] ".Colorize(ErrorMsgCol) + msg);
        }

        public static void Warning(string msg)
        {
            Log.Warning("[No Lightning Flash UwU] ".Colorize(WarningMsgCol) + msg);
        }

        public static void Message(string msg)
        {
            Log.Message("[No Lightning Flash UwU] ".Colorize(MessageMsgCol) + msg);
        }
    }
}
