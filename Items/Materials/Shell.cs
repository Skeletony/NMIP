using Terraria.ModLoader;

namespace NMIP.Items.Materials
{
    public class Shell : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shattered Core");
            Tooltip.SetDefault("A shattered piece of a core.");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 28;
            item.height = 34;
            item.rare = 6;
        }
    }
}