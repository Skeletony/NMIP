using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace NMIP.Walls
{
    public class ToxicsandstoneWall : ModWall
    {
        public override void SetDefaults()
        {
            dustType = 277;
            AddMapEntry(new Color(86, 64, 51));
            Terraria.ID.WallID.Sets.Conversion.Sandstone[Type] = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

    }
}