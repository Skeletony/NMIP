using Terraria.ModLoader;

namespace NMIP.Items.Materials
{
    public class FrostFragment : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Frozen Flame");
            Tooltip.SetDefault("A flame that is frozen into ice.");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 24;
            item.height = 34;
            item.rare = 6;
        }
    }
}