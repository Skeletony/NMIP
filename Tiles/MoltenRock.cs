using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace NMIP.Tiles
{
    public class MoltenRock : ModTile
    {
        public override void SetDefaults()
        {
            Main.tileSolid[Type] = true;
            Main.tileMergeDirt[Type] = true;
            soundType = 21;
            soundStyle = 2;
            dustType = 6;
            drop = mod.ItemType("MoltenRock");
            ModTranslation name = CreateMapEntryName();
            name.SetDefault("Igneous Stone");
            AddMapEntry(new Color(248, 136, 52));
            minPick = 180;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }
    }
}