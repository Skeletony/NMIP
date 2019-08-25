using Terraria;
using Terraria.ModLoader;

namespace NMIP.Backgrounds
{
    public class ToxinUgBgStyle : ModUgBgStyle
	{
		public override bool ChooseBgStyle()
		{
			return !Main.gameMenu && Main.player[Main.myPlayer].GetModPlayer<NMIPPlayer>(mod).zoneToxin;
		}

		public override void FillTextureArray(int[] textureSlots)
		{
			textureSlots[0] = mod.GetBackgroundSlot("Backgrounds/ToxinUnderground1");
			textureSlots[1] = mod.GetBackgroundSlot("Backgrounds/ToxinUnderground");
			textureSlots[2] = mod.GetBackgroundSlot("Backgrounds/ToxinCavern1");
			textureSlots[3] = mod.GetBackgroundSlot("Backgrounds/ToxinCavern");
		}
	}
}