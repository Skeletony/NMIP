using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Localization;

namespace NMIP.Items.Materials
{
    public class IceBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Hailstone Bar");
            Tooltip.SetDefault("Freezes everything it touches... or so the legend says.");
        }

        public override void SetDefaults()
        {
            item.width = 30;
            item.height = 24;
            item.maxStack = 999;
            item.rare = 7;
            item.value = 10000;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "IceOre", 3);

            recipe.AddTile(TileID.AdamantiteForge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
