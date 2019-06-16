using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace NMIP.Items.Placeables
{
    public class IceOre : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hailstone");
            Tooltip.SetDefault("An underground ore made out ice and hail.");
        }

        public override void SetDefaults()
        {
            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 15;
            item.useTime = 10;
            item.useStyle = 1;
            item.rare = 7;
            item.consumable = true;
            item.value = 10000;
            item.createTile = mod.TileType("IceOre");
        }
    }
}
