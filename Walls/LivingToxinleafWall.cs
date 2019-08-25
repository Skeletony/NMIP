using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace NMIP.Walls
{
    public class LivingToxinleafWall : ModWall
    {
        public override void SetDefaults()
        {
            dustType = 277;
            AddMapEntry(new Color(125, 154, 88));
        }

        public override void NumDust(int i, int j, bool fail, ref int num)
        {
            num = fail ? 1 : 3;
        }

    }
}