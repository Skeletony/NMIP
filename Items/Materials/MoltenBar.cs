using Terraria.ModLoader;
using Terraria.ID;

namespace NMIP.Items.Materials
{
    public class MoltenBar : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Igneous Bar");
            Tooltip.SetDefault("Hot hot hot hot!");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 30;
            item.height = 24;
            item.rare = 6;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "MoltenRock", 3);
            recipe.AddIngredient(mod, "MoltenSoul");

            recipe.AddTile(TileID.AdamantiteForge);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}