using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace NMIP.Items.Accessories
{
    class SKExpertDrop : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Slime Boots");
            Tooltip.SetDefault("'Jumping yeah, jumping yeah, everybody!'"
                + "\nGreatly increases jump height"
                + "\nIncreases movment speed by 3%");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 24;
            item.value = 30000;
            item.rare = -12;
            item.value = Item.buyPrice(0, 3, 0, 0);
            item.accessory = true;
            item.expert = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.jumpSpeedBoost += 6f;
            player.moveSpeed += 0.3f;
            player.maxRunSpeed += 1.5f;
        }
    }
}
