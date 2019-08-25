using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace NMIP.Walls
{
    public class ToxicsandHardenedWall : ModWall
    {
        public override void SetDefaults()
        {
            dustType = 277;
            AddMapEntry(new Color(53, 43, 38));
            Terraria.ID.WallID.Sets.Conversion.HardenedSand[Type] = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

    }
}