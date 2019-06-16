using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NMIP.Tiles
{
    public class IceOre : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            soundType = 21;
            soundStyle = 2;
            dustType = 135;
            drop = mod.ItemType("IceOre");
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Hailstone");
            AddMapEntry(new Color(165, 244, 241));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}