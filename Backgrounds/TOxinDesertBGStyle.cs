﻿using Terraria;
using Terraria.ModLoader;

namespace NMIP.Backgrounds
{
    class TOxinDesertBgStyle : ModSurfaceBgStyle
    {
        public override bool ChooseBgStyle()
        {
            return !Main.gameMenu && Main.LocalPlayer.GetModPlayer<NMIPPlayer>(mod).zoneToxin && Main.LocalPlayer.ZoneDesert;
        }

        public override void ModifyFarFades(float[] fades, float transitionSpeed)
        {
            for (int i = 0; i < fades.Length; i++)
            {
                if (i == Slot)
                {
                    fades[i] += transitionSpeed;
                    if (fades[i] > 1f)
                    {
                        fades[i] = 1f;
                    }
                }
                else
                {
                    fades[i] -= transitionSpeed;
                    if (fades[i] < 0f)
                    {
                        fades[i] = 0f;
                    }
                }
            }
        }

        public override int ChooseFarTexture()
        {
            return mod.GetBackgroundSlot("Backgrounds/ToxinDesertBG");
        }
        
    }
}