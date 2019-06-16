using Terraria.ModLoader;

namespace NMIP.Items.Materials
{
    public class BloodCotton : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Cotton full of blood inside.");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 24;
            item.height = 20;
            item.rare = 8;
        }
    }
}