using Terraria.ModLoader;

namespace NMIP.Items.Materials
{
    public class MoltenRock : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Igneous Stone");
            Tooltip.SetDefault("Hot hot hot!");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 20;
            item.height = 26;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.rare = 6;
            item.consumable = true;
            item.createTile = mod.TileType("MoltenRock");
        }
    }
}