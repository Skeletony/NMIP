using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace NMIP.Items.Placeables
{
    public class Ored : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elvian Ore");
            Tooltip.SetDefault("Ore found in the ancient lands of the Elves... but that's the past.");
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
            item.rare = 3;
            //item.consumable = true;
            //item.createTile = mod.TileType("Ored");
        }
    }
}
