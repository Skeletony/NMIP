using Terraria.ModLoader;

namespace NMIP.Items.Materials
{
    public class AncientMachinery : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rusted Plating");
            Tooltip.SetDefault("A rusted old material. Maybe it could be reused...?");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 26;
            item.height = 24;
            item.rare = 5;
        }
    }
}