using Terraria.ModLoader;
using Terraria.ID;

namespace NMIP.Items.Materials
{
    public class Bard : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Elvian Bar");
            Tooltip.SetDefault("Metal of the Elves.");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.width = 30;
            item.height = 24;
            item.rare = 3;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(mod, "Ored", 2);

            recipe.AddTile(TileID.Furnaces);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}