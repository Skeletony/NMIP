using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NMIP.Tiles
{
    class CosmicOre : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            soundType = 21;
            soundStyle = 2;
            dustType = mod.DustType("StarDust");
            drop = mod.ItemType("CosmicOre");
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Cosmic Ore");
            AddMapEntry(new Color(247, 125, 255));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}