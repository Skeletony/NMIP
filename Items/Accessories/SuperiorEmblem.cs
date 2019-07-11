using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace NMIP.Items.Accessories
{
    class SuperiorEmblem : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("+10 defense"
                + "\n+20% damage"
                + "\n+Increases life regeneration");
        }

        public override void SetDefaults()
        {
            item.width = 38;
            item.height = 32;
            item.value = 30000;
            item.rare = 10;
            item.accessory = true;
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen += 5;
            player.meleeDamage += 0.2f;
            player.statDefense += 10;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.RangerEmblem);
            recipe.AddIngredient(ItemID.SorcererEmblem);
            recipe.AddIngredient(ItemID.WarriorEmblem);
            recipe.AddIngredient(ItemID.SummonerEmblem);
            recipe.AddIngredient(ItemID.BandofRegeneration);

            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
