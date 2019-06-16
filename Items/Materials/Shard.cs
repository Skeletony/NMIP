using Terraria.ModLoader;

namespace NMIP.Items.Materials
{
    public class Shard : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Mythical Fragment");
            Tooltip.SetDefault("A fragment imbued with the magic of Elves.");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 12;
            item.height = 14;
            item.rare = 3;
        }
    }
}