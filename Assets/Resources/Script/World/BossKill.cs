using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BossKill
{
    public class BossKill : MonoBehaviour
    {
        public static void Exe()
        {
            if (WorldData.distance == 2) Win();
            else advance();
        }

        private static void Win()
        {
            Nortice_GainSystem.AddNotricePoint_AscensionSucces();

            if (Ascension.currentAscension != 0)
                Nortice_GainSystem.AddNotricePoint_AscensionModifier();

            var windowAscension = WindowInfo.Instance.OpenOrCloseWindow(WindowInfo.type.Ascension) as Window_Ascension;

            windowAscension.info = new EndOfRunInfo(EndOfRunInfo.state.win);
        }

        private static void advance()
        {
            WorldLoad.GenerateWorldAndMiniMap(WorldData.distance + 1);
        }
    }
}
