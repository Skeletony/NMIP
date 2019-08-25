using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace NMIP.Walls
{
    public class ToxinStoneWall : ModWall
    {
        public override void SetDefaults()
        {
            dustType = 277;
            AddMapEntry(new Color(113, 92, 54));
            Terraria.ID.WallID.Sets.Conversion.Stone[Type] = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

    }
}