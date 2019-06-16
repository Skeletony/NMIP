using Terraria.ModLoader;

namespace NMIP.Items.Materials
{
    public class MoltenBones : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Molten Bone");
            Tooltip.SetDefault("Skeletal pieces of magmatic creatures.");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 22;
            item.height = 20;
            item.rare = 8;
        }
    }
}