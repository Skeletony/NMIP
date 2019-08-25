using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace NMIP.Walls
{
    public class ToxicJungleWall : ModWall
    {
        public override void SetDefaults()
        {
            dustType = 277;
            AddMapEntry(new Color(86, 104, 76));
            Terraria.ID.WallID.Sets.Conversion.Grass[Type] = true;
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

    }
}